using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vinar
{
    class Transcriber
    {
        private Credentials.TranscriptionCredentials credentials;

        public event EventHandler LoadStarted;
        public event EventHandler UploadStarted;
        public event EventHandler TranscriptionStarted;
        public event EventHandler<ProgressEventArgs> ProgressUpdated;
        public event EventHandler<TranscriptionEventArgs> TranscriptionCompleted;
        public event EventHandler<TranscriptionErrorEventArgs> ErrorOccurred;

        /**
         * Transcriber uses Azure Video Indexer services to transcribe the video
         * at the given path into subtitles
         */
        public Transcriber(Credentials.TranscriptionCredentials _credentials)
        {
            credentials = _credentials;
        }

        public void Transcribe(string videoPath)
        {
            try
            {
                LoadStarted?.Invoke(this, new EventArgs());

                System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

                // Create HTTP client
                var handler = new HttpClientHandler();
                handler.AllowAutoRedirect = false;
                var client = new HttpClient(handler);

                // Obtain access token
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.Key);
                var accountAccessTokenRequestResult = client.GetAsync($"{credentials.UrlBaseAuth}/AccessToken?allowEdit=true").Result;
                var accountAccessToken = accountAccessTokenRequestResult.Content.ReadAsStringAsync().Result.Replace("\"", "");
                client.DefaultRequestHeaders.Remove("Ocp-Apim-Subscription-Key");

                Debug.WriteLine("Uploading...");

                // Get video from file
                FileStream video = File.OpenRead(videoPath);
                byte[] buffer = new byte[video.Length];
                video.Read(buffer, 0, buffer.Length);

                // Create content
                var content = new MultipartFormDataContent();
                content.Add(new ByteArrayContent(buffer));

                UploadStarted?.Invoke(this, new EventArgs());

                // Build upload request
                string name = Path.GetFileName(videoPath);
                string externalId = "123";
                string uploadRequestUrl = $"{credentials.UrlBase}/Videos?accessToken={accountAccessToken}&name={name}&privacy=private&externalId={externalId}";

                // Send upload request
                var uploadRequestResult = client.PostAsync(uploadRequestUrl, content).Result;
                var uploadResult = uploadRequestResult.Content.ReadAsStringAsync().Result;

                // Get video ID from upload result
                string videoId = JsonConvert.DeserializeObject<dynamic>(uploadResult)["id"];

                TranscriptionStarted?.Invoke(this, new EventArgs());

                Debug.WriteLine("Uploaded");
                Debug.WriteLine("Video ID: " + videoId);

                // Obtain video access token
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.Key);
                var videoTokenRequestResult = client.GetAsync($"{credentials.UrlBaseAuth}/Videos/{videoId}/AccessToken?allowEdit=true").Result;
                var videoAccessToken = videoTokenRequestResult.Content.ReadAsStringAsync().Result.Replace("\"", "");

                // Wait for video index to finish
                while (true)
                {
                    Debug.WriteLine("Sleepy time...");

                    Thread.Sleep(2000);
                    Debug.WriteLine("Checking state...");

                    // Get processing state
                    var videoGetIndexRequestResult = client.GetAsync($"{credentials.UrlBase}/Videos/{videoId}/Index?accessToken={videoAccessToken}&language=English").Result;
                    var videoGetIndexResult = videoGetIndexRequestResult.Content.ReadAsStringAsync().Result;

                    var jObject = JObject.Parse(videoGetIndexResult);
                    string processingState = (string)jObject["state"];

                    Debug.WriteLine("");
                    Debug.WriteLine("State:");
                    Debug.WriteLine(processingState);

                    // If job is finished
                    if (processingState == "Processed")
                    {
                        //left in for future testing needs 
                        // Debug.WriteLine("");
                        // Debug.WriteLine("Full JSON:");
                        //Debug.WriteLine(videoGetIndexResult);

                        // Get raw transcript
                        JArray transArray = jObject["videos"][0]["insights"]["transcript"].ToObject<JArray>();

                        // Produce list of subtitles
                        var subtitles = new List<SubtitleEntry>();

                        foreach (var subtitle in transArray)
                        {
                            Debug.WriteLine(subtitle);

                            string text = (string)subtitle["text"];
                            string stime = subtitle["instances"][0]["start"].ToString();
                            TimeSpan timespan = TimeSpan.ParseExact(stime, "d:hh:mm:ss.ff", CultureInfo.InvariantCulture);

                            subtitles.Add(new SubtitleEntry { Timestamp = timespan, Content = text });
                        }

                        TranscriptionCompleted?.Invoke(this, new TranscriptionEventArgs() { Subtitles = subtitles });

                        Debug.WriteLine("-----------------");

                        return;
                    }
                    else
                    {
                        string processingProgress = (string)jObject["videos"][0]["processingProgress"];
                        int percent = int.Parse(processingProgress.Substring(0, processingProgress.Length - 1));

                        ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = percent });
                    }
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, new TranscriptionErrorEventArgs() { Error = ex });
            }
        }

        public class ProgressEventArgs : EventArgs
        {
            public int Percent { get; set; }
        }

        public class TranscriptionEventArgs : EventArgs
        {
            public List<SubtitleEntry> Subtitles { get; set; }
        }

        public class TranscriptionErrorEventArgs : EventArgs
        {
            public Exception Error { get; set; }
        }
    }
}

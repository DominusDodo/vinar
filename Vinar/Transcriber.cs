using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace Vinar
{
    class Transcriber
    {
        private string videoPath;

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
        public Transcriber(string _videoPath)
        {
            videoPath = _videoPath;
        }

        public void Transcribe()
        {
            LoadStarted?.Invoke(this, new EventArgs());

            var credentials = Credentials.Load("../../../../credentials.yml");

            System.Net.ServicePointManager.SecurityProtocol |= System.Net.SecurityProtocolType.Tls12;

            // Create HTTP client
            var handler = new HttpClientHandler();
            handler.AllowAutoRedirect = false;
            var client = new HttpClient(handler);

            // Obtain access token
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.ApiKey);
            var accountAccessTokenRequestResult = client.GetAsync($"{credentials.UrlBase}/AccessToken?allowEdit=true").Result;
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
            string uploadRequestUrl = $"{credentials.UrlBase}/Videos?accessToken={accountAccessToken}&name={name}&privacy=private";

            // Send upload request
            var uploadRequestResult = client.PostAsync(uploadRequestUrl, content).Result;
            var uploadResult = uploadRequestResult.Content.ReadAsStringAsync().Result;

            // Get video ID from upload result
            string videoId = JsonConvert.DeserializeObject<dynamic>(uploadResult)["id"];

            TranscriptionStarted?.Invoke(this, new EventArgs());

            Debug.WriteLine("Uploaded");
            Debug.WriteLine("Video ID: " + videoId);

            // Obtain video access token
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", credentials.ApiKey);
            var videoTokenRequestResult = client.GetAsync($"{credentials.UrlBase}/Video/{videoId}/AccessToken?allowEdit=true").Result;
            var videoAccessToken = videoTokenRequestResult.Content.ReadAsStringAsync().Result.Replace("\"", "");

            // Wait for video index to finish
            while (true)
            {
                Debug.WriteLine("Sleepy time...");

                Thread.Sleep(5000);
                Debug.WriteLine("Checking state...");

                // Get processing state
                var videoGetIndexRequestResult = client.GetAsync($"{credentials.UrlBase}/Video/{videoId}/Index?accessToken={videoAccessToken}&language=English").Result;
                var videoGetIndexResult = videoGetIndexRequestResult.Content.ReadAsStringAsync().Result;
                string processingState = JsonConvert.DeserializeObject<dynamic>(videoGetIndexResult)["state"];

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
                    var jObject = JObject.Parse(videoGetIndexResult);
                    JArray transArray = jObject["videos"][0]["insights"]["transcript"].ToObject<JArray>();

                    // Produce list of subtitles
                    var subtitles = new List<Tuple<DateTime, String>>();

                    foreach (var subtitle in transArray)
                    {
                        Debug.WriteLine(subtitle);

                        string text = (string)subtitle["text"];
                        DateTime datetime = DateTime.Parse((string)subtitle["instances"][0]["start"]);

                        subtitles.Add(new Tuple<DateTime, String>(datetime, text));
                    }

                    TranscriptionCompleted?.Invoke(this, new TranscriptionEventArgs() { Subtitles = subtitles });

                    Debug.WriteLine("-----------------");
                }
                else
                {
                    var jObject = JObject.Parse(videoGetIndexResult);

                    string processingProgress = JsonConvert.DeserializeObject<dynamic>(videoGetIndexResult)["processingProgress"];
                    int percent = int.Parse(processingProgress.Substring(0, processingProgress.Length - 1));

                    ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = percent });
                }
            }
        }

        public class Credentials
        {
            public string AccountId = "";
            public string ApiUrl = "";
            public string Location = "";
            public string ApiKey = "";

            public string UrlBase
            {
                get { return $"{ApiUrl}/auth/{Location}/Accounts/{AccountId}"; }
            }

            public static Credentials Load(string path)
            {
                string yml = File.ReadAllText(path);

                var deserializer = new DeserializerBuilder()
                    .WithNamingConvention(CamelCaseNamingConvention.Instance)
                    .Build();

                return deserializer.Deserialize<Credentials>(yml);
            }
        }

        public class ProgressEventArgs : EventArgs
        {
            public int Percent { get; set; }
        }

        public class TranscriptionEventArgs : EventArgs
        {
            public IEnumerable<Tuple<DateTime, String>> Subtitles { get; set; }
        }

        public class TranscriptionErrorEventArgs : EventArgs
        {
            public Exception Error { get; set; }
        }
    }
}

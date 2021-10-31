using Microsoft.CognitiveServices.Speech;
using SoxSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Xabe.FFmpeg;
using Xabe.FFmpeg.Downloader;
using Xabe.FFmpeg.Events;

namespace Vinar
{
    class Narrator
    {
        private Credentials.NarrationCredentials credentials;
        private List<SubtitleEntry> subtitles = new List<SubtitleEntry>();
        private List<float> offsets = new List<float>();
        private string directory;
        private string inputVideoPath;
        private string outputVideoPath;

        public event EventHandler SynthesisStarted;
        public event EventHandler AlignmentStarted;
        public event EventHandler CombinationStarted;
        public event EventHandler NarrationCompleted;
        public event EventHandler<ProgressEventArgs> ProgressUpdated;
        public event EventHandler<NarrationErrorEventArgs> ErrorOccurred;

        /**
         * Narrator uses Azure Cognitive services to narrate the video,
         * then divides it at bookmark timings and restitches it to align
         * with subtitle times
         */
        public Narrator(Credentials.NarrationCredentials _credentials)
        {
            credentials = _credentials;
        }

        public void Narrate(List<SubtitleEntry> subtitles, string inputVideoPath, string outputVideoPath)
        {
            this.subtitles = subtitles;
            this.inputVideoPath = inputVideoPath;
            this.outputVideoPath = outputVideoPath;

            Task.Run(async () => {
                try
                {
                    await FFmpegDownloader.GetLatestVersion(FFmpegVersion.Official);
                    FFmpeg.SetExecutablesPath(@"./");

                    directory = Path.Combine(Path.GetTempPath(), "Vinar", Path.GetRandomFileName());
                    Directory.CreateDirectory(directory);

                    await Synthesise();
                    await Align();
                    await Combine();
                }
                catch (Exception ex)
                {
                    ErrorOccurred?.Invoke(this, new NarrationErrorEventArgs() { Error = ex });
                }
            });
        }

        public async Task Combine()
        {
            CombinationStarted?.Invoke(this, new EventArgs());
            ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = 0 });

            if (File.Exists(outputVideoPath)) File.Delete(outputVideoPath);

            IMediaInfo video = await FFmpeg.GetMediaInfo(inputVideoPath);
            IMediaInfo audio = await FFmpeg.GetMediaInfo(Path.Combine(directory, "output.wav"));
            IStream audioStream = audio.AudioStreams.FirstOrDefault()?.SetChannels(2);
            IStream videoStream = video.VideoStreams.FirstOrDefault();

            var conversion = FFmpeg.Conversions.New()
                .AddStream(videoStream, audioStream)
                .SetOutput(outputVideoPath);

            conversion.OnProgress += (object sender, ConversionProgressEventArgs args) =>
            {
                ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = args.Percent });
            };

            await conversion.Start();

            Directory.Delete(directory, true);

            NarrationCompleted?.Invoke(this, new EventArgs());
        }

        public async Task Align()
        {
            AlignmentStarted?.Invoke(this, new EventArgs());
            ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = 0 });

            Console.WriteLine("Alignment started");

            string inputPath = Path.Combine(directory, "input.wav");
            IMediaInfo info = await FFmpeg.GetMediaInfo(inputPath).ConfigureAwait(false);
            Func<IAudioStream> stream = () => info.AudioStreams.First();

            var parts = new List<IAudioStream>();
            var partDurations = new List<TimeSpan>();

            for (int i = 0; i < offsets.Count; i++)
            {
                Console.WriteLine($"Part {i} started");

                var ts = offsets[i];
                var start = TimeSpan.FromMilliseconds(ts);

                var end = info.Duration;

                if (i + 1 < offsets.Count)
                {
                    ts = offsets[i + 1];
                    end = TimeSpan.FromMilliseconds(ts);
                }

                var part = stream().Split(start, end - start);

                Console.WriteLine($"Part {i} converting");

                parts.Add(part);
                partDurations.Add(end - start);

                Console.WriteLine($"Part {i} ended");
            }


            var silentStream = stream().Split(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(1));

            string silencePath = Path.Combine(directory, "silence.wav");
            Console.WriteLine(silencePath);

            await FFmpeg.Conversions.New()
                .AddStream(silentStream)
                .SetOutput(silencePath)
                .Start();

            IMediaInfo sinfo = await FFmpeg.GetMediaInfo(silencePath).ConfigureAwait(false);
            Func<IAudioStream> sstream = () => sinfo.AudioStreams.First();
            Func<double, IAudioStream> silence = d => changeSpeed(sstream(), 1 / d);


            var allParts = new List<IAudioStream>();

            var elapsed = TimeSpan.Zero;

            for (int i = 0; i < parts.Count; i++)
            {
                var ts = subtitles[i].Timestamp;
                var start = new TimeSpan(0, ts.Hour, ts.Minute, ts.Second, ts.Millisecond);

                if (i > 0)
                {
                    ts = subtitles[i].Timestamp;
                    new TimeSpan(0, ts.Hour, ts.Minute, ts.Second, ts.Millisecond);

                    allParts.Add(silence((start - elapsed).TotalSeconds));
                    elapsed = elapsed.Add(start - elapsed);
                }
                else
                {
                    allParts.Add(silence(start.TotalSeconds));
                    elapsed = elapsed.Add(start);
                }

                var part = parts[i];

                if (i + 1 < parts.Count)
                {
                    ts = subtitles[i + 1].Timestamp;
                    var end = new TimeSpan(0, ts.Hour, ts.Minute, ts.Second, ts.Millisecond);

                    // If timeslot has positive duration
                    if (end > start)
                    {
                        var duration = partDurations[i];

                        // If part is longer than timeslot
                        if (duration > end - start)
                        {
                            // Speed it up
                            double m = duration.TotalSeconds / (end - start).TotalSeconds;
                            part = changeSpeed(part, m);
                            duration = TimeSpan.FromSeconds(duration.TotalSeconds / 2);
                        }
                        allParts.Add(part);
                        elapsed = elapsed.Add(duration);
                    }
                }
                else
                {
                    allParts.Add(part);
                    elapsed = elapsed.Add(partDurations[i]);
                }

                ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = 100 * (i + 1) / offsets.Count });
            }

            var inputsString = "";

            for (int i = 0; i < allParts.Count; i++)
            {
                string partPath = Path.Combine(directory, $"part{i}.wav");
                Console.WriteLine(partPath);

                await FFmpeg.Conversions.New()
                    .AddStream(allParts[i])
                    .SetOutput(partPath)
                    .Start();

                inputsString += $"file '{partPath}'\r\n";
            }

            string outputPath = Path.Combine(directory, "output.wav");
            Console.WriteLine(outputPath);

            File.WriteAllText("parts.txt", inputsString);

            using (Process p = new Process())
            {
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.StartInfo.FileName = "ffmpeg.exe";
                p.StartInfo.Arguments = $"-f concat -safe 0 -i parts.txt -c copy {outputPath}";
                p.Start();
                p.WaitForExit();
            }
        }

        private IAudioStream changeSpeed(IAudioStream stream, double multiplicator)
        {
            while (multiplicator > 2.0)
            {
                stream = stream.ChangeSpeed(2.0);
                multiplicator /= 2.0;
            }

            while (multiplicator < 0.5)
            {
                stream = stream.ChangeSpeed(0.5);
                multiplicator /= 0.5;
            }

            return stream.ChangeSpeed(multiplicator);
        }

        public async Task Synthesise()
        {
            SynthesisStarted?.Invoke(this, new EventArgs());
            ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = 0 });

            string ssml = generateSsml(subtitles);
            Console.WriteLine(ssml);

            // Derived from: https://docs.microsoft.com/en-us/azure/cognitive-services/speech-service/get-started-text-to-speech?tabs=script%2Cwindowsinstall&pivots=programming-language-csharp

            var config = SpeechConfig.FromSubscription(credentials.Key, credentials.Location);

            using (var synthesizer = new SpeechSynthesizer(config, null))
            {
                synthesizer.BookmarkReached += (s, e) =>
                {
                    offsets.Add(e.AudioOffset / 10000);

                    int percent = 100 * offsets.Count / subtitles.Count;
                    ProgressUpdated?.Invoke(this, new ProgressEventArgs() { Percent = percent });
                };

                var result = await synthesizer.SpeakSsmlAsync(ssml);
                var stream = AudioDataStream.FromResult(result);
                await stream.SaveToWaveFileAsync(Path.Combine(directory, "input.wav"));

                Console.WriteLine("Completed synthesis");
            }
        }

        private string generateSsml(List<SubtitleEntry> subtitles)
        {
            using (var sw = new StringWriter())
            {
                XmlWriterSettings settings = new XmlWriterSettings();
                settings.Indent = true;

                using (var xw = XmlWriter.Create(sw, settings))
                {
                    string url = $"http://www.w3.org/2001/10/synthesis";

                    // Set up xml doc 
                    xw.WriteStartElement("speak");
                    xw.WriteAttributeString("version", "1.0");
                    xw.WriteAttributeString("xmlns", "speak", null, url);
                    xw.WriteAttributeString("xml", "lang", null, "en-us");

                    xw.WriteStartElement("voice");
                    xw.WriteAttributeString("name", "en-US-ChristopherNeural");

                    xw.WriteStartElement("break");
                    xw.WriteAttributeString("time", "1s");
                    xw.WriteEndElement();

                    int i = 0;

                    //write in the speech to the xml file as a sentence
                    foreach (var subtitle in subtitles)
                    {
                        xw.WriteStartElement("p");
                        xw.WriteString(subtitle.Content);
                        xw.WriteEndElement();

                        xw.WriteStartElement("bookmark");
                        xw.WriteAttributeString("mark", $"{i++}");
                        xw.WriteEndElement();
                    }

                    xw.WriteEndElement();
                    xw.WriteEndDocument();

                    xw.Close();

                    return sw.ToString();
                }
            }
        }

        public class ProgressEventArgs : EventArgs
        {
            public int Percent { get; set; }
        }

        public class NarrationErrorEventArgs : EventArgs
        {
            public Exception Error { get; set; }
        }
    }
}

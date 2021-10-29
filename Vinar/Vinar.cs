using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Vinar.Transcriber;

namespace Vinar
{
    public partial class Vinar : Form
    {
        private string videoFilename;
        private string subtitleFilename;
        private Subtitle focused;

        public Vinar()
        {
            InitializeComponent();

            String[] subtitleStrings = { "A", "B", "C\r\nd", "B", "C\r\nd", "B", "C\r\nd", "B", "C\r\nd", "B", "C\r\ne", "Q", "F\r\nh" };

            foreach (String subtitleString in subtitleStrings)
            {
                createSubtitle(new DateTime(), subtitleString);
            }

            repositionSubtitles();
        }

        private void createSubtitle(DateTime timestamp, string content)
        {
            Subtitle subtitle = new Subtitle();
            subtitle.Width = panelSubtitles.Width - subtitle.Margin.Horizontal;
            subtitle.Anchor |= AnchorStyles.Right;
            subtitle.Timestamp = timestamp;
            subtitle.Content = content;
            subtitle.SizeChanged += (object sender, EventArgs e) => repositionSubtitles();
            subtitle.TextBoxGotFocus += (object sender, EventArgs e) => setFocused(subtitle, true);
            subtitle.TextBoxLostFocus += (object sender, EventArgs e) => setFocused(subtitle, false);
            panelSubtitles.Controls.Add(subtitle);
        }

        private void setFocused(Subtitle subtitle, bool got)
        {
            ToolStripMenuItem[] activeWhileEditing =
            {
                insertBeforeToolStripMenuItem,
                insertAfterToolStripMenuItem,
                deleteToolStripMenuItem,
                mergeWithNextToolStripMenuItem,
                mergeWithPreviousToolStripMenuItem
            };

            if (got)
            {
                foreach (var menuItem in activeWhileEditing)
                {
                    menuItem.Enabled = true;
                }

                focused = subtitle;
            }
            else if (focused == subtitle)
            {
                foreach (var menuItem in activeWhileEditing)
                {
                    menuItem.Enabled = false;
                }

                focused = null;
            }
        }

        private void repositionSubtitles()
        {
            int y = 0;

            foreach (Control control in panelSubtitles.Controls)
            {
                Subtitle subtitle = (Subtitle)control;
                subtitle.Location = new Point(subtitle.Margin.Left, y + subtitle.Margin.Top);
                y += subtitle.Height;
            }
        }

        private void openSubtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogSubtitles.ShowDialog() == DialogResult.OK)
            {
                subtitleFilename = openFileDialogSubtitles.FileName;
                Regex rx = new Regex(@"(\d+):(\d+):(\d+).(\d+)\s+(.*)", RegexOptions.Compiled);

                panelSubtitles.Controls.Clear();

                try
                {
                    foreach (string line in File.ReadLines(subtitleFilename))
                    {
                        Match match = rx.Match(line);

                        int h = int.Parse(match.Groups[1].Value);
                        int m = int.Parse(match.Groups[2].Value);
                        int s = int.Parse(match.Groups[3].Value);
                        int ms = int.Parse(match.Groups[4].Value);
                        DateTime timestamp = new DateTime(2021, 1, 1, h, m, s, ms * 10);

                        string content = match.Groups[5].Value;
                        content = content.Replace("\\r\\n", "\r\n");
                        content = content.Replace("\\r", "\r\n");
                        content = content.Replace("\\n", "\r\n");
                        content = content.Replace("\\\\", "\\");

                        createSubtitle(timestamp, content);
                    }

                    repositionSubtitles();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Failed to load subtitles");
                }
            }
        }

        private void openVideoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogVideo.ShowDialog() == DialogResult.OK)
            {
                videoFilename = openFileDialogVideo.FileName;
                axWindowsMediaPlayer.URL = videoFilename;

                transcribeToolStripMenuItem.Enabled = true;
            }
        }

        private void axWindowsMediaPlayer_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            double t = e.newPosition;
            int h = (int)t / 3600;
            int m = (int)t % 3600 / 60 % 60;
            int s = (int)t % 60;
            int ms = (int)(t * 1000) % 1000;
            DateTime timestamp = new DateTime(2021, 1, 1, h, m, s, ms);
            //((Subtitle)panelSubtitles.Controls[0]).Timestamp = timestamp;
        }

        private void timerPlayback_Tick(object sender, EventArgs e)
        {
            double t = axWindowsMediaPlayer.Ctlcontrols.currentPosition;
            int h = (int)t / 3600;
            int m = (int)t % 3600 / 60 % 60;
            int s = (int)t % 60;
            int ms = (int)(t * 1000) % 1000;
            DateTime timestamp = new DateTime(2021, 1, 1, h, m, s, ms);
            //((Subtitle)panelSubtitles.Controls[1]).Timestamp = timestamp;
        }

        private void axWindowsMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (e.newState == 3 || e.newState == 2)
            {
                timerPlayback.Enabled = true;
            }
            else
            {
                timerPlayback.Enabled = false;
            }
        }

        private void Vinar_Load(object sender, EventArgs e)
        {
            //var credentials = VideoIndexer.Credentials.Load("../../../../credentials.yml");
            DateTime dt = DateTime.Parse("0:12:34.56");
            MessageBox.Show($"{dt.Millisecond}", "Credentials");
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all subtitles?", "Clear subtitles", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                panelSubtitles.Controls.Clear();
            }
        }

        private void transcribeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string message = "Do you want to generate a transcript? You will not be able make changes until this is finished or cancelled.";
            var result = MessageBox.Show(message, "Generate transcript", MessageBoxButtons.OKCancel);

            if (result == DialogResult.OK)
            {
                // Disable everything except the transcription cancellation menu item

                ToolStripMenuItem[] inactiveWhileTranscribing =
                {
                    fileToolStripMenuItem,
                    editToolStripMenuItem,
                    insertBeforeToolStripMenuItem,
                    insertAfterToolStripMenuItem,
                    deleteToolStripMenuItem,
                    mergeWithNextToolStripMenuItem,
                    mergeWithPreviousToolStripMenuItem
                };

                panelSubtitles.Enabled = false;
                axWindowsMediaPlayer.Enabled = false;
                
                foreach (var menuItem in inactiveWhileTranscribing)
                {
                    menuItem.Enabled = false;
                }

                transcribeToolStripMenuItem.Text = "Cancel transcription";
                transcribeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;

                var transcriber = new Transcriber(videoFilename);

                transcriber.LoadStarted += (object sender1, EventArgs e1) => toolStripStatusLabel.Text = "Loading video...";
                transcriber.UploadStarted += (object sender1, EventArgs e1) => toolStripStatusLabel.Text = "Uploading video...";
                transcriber.TranscriptionStarted += (object sender1, EventArgs e1) => toolStripStatusLabel.Text = "Transcribing video...";
                transcriber.ProgressUpdated += (object sender1, ProgressEventArgs e1) => toolStripProgressBar.Value = e1.Percent;

                transcriber.TranscriptionCompleted += (object sender1, TranscriptionEventArgs e1) =>
                {
                    panelSubtitles.Controls.Clear();

                    foreach (var subtitle in e1.Subtitles)
                    {
                        createSubtitle(subtitle.Item1, subtitle.Item2);
                    }

                    panelSubtitles.Enabled = true;
                    axWindowsMediaPlayer.Enabled = true;

                    foreach (var menuItem in inactiveWhileTranscribing)
                    {
                        menuItem.Enabled = true;
                    }

                    transcribeToolStripMenuItem.Text = "Transcribe";
                    transcribeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;

                    toolStripStatusLabel.Text = "Ready";
                };

                Task.Run(() => transcriber.Transcribe());
            }
        }

        private void insertBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panelSubtitles.Controls.Remove(focused);
        }
    }
}

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
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using static Vinar.Transcriber;
using static Vinar.Narrator;

namespace Vinar
{
    public partial class Vinar : Form
    {
        private string videoFilename;
        private string subtitleFilename;
        private Subtitle focused;
        private Subtitle editedTimestamp;
        private Credentials credentials;

        public Vinar()
        {
            InitializeComponent();
        }

        private void Vinar_Load(object sender, EventArgs e)
        {
            credentials = Credentials.Load("../../../../../credentials.yml");
        }

        private Subtitle createSubtitle(TimeSpan timestamp, string content)
        {
            Subtitle subtitle = new Subtitle();
            subtitle.Width = panelSubtitles.Width - subtitle.Margin.Horizontal;
            subtitle.Anchor |= AnchorStyles.Right;
            subtitle.Timestamp = timestamp;
            subtitle.Content = content;
            subtitle.SizeChanged += (object sender, EventArgs e) => repositionSubtitles();
            subtitle.TextBoxGotFocus += (object sender, EventArgs e) => setFocused(subtitle);
            subtitle.TextBoxLostFocus += (object sender, EventArgs e) => setFocused(null);
            subtitle.TimestampDoubleClicked += (object sender, EventArgs e) => editTimestamp(subtitle);

            panelSubtitles.Controls.Add(subtitle);

            return subtitle;
        }

        private void enableIfFocused(bool disable = false)
        {
            ToolStripMenuItem[] activeWhileEditing =
            {
                insertBeforeToolStripMenuItem,
                insertAfterToolStripMenuItem,
                deleteToolStripMenuItem,
                mergeWithNextToolStripMenuItem,
                mergeWithPreviousToolStripMenuItem,
                splitToolStripMenuItem
            };

            if (focused == null || disable)
            {
                foreach (var menuItem in activeWhileEditing)
                {
                    menuItem.Enabled = false;
                }
            }
            else
            {
                foreach (var menuItem in activeWhileEditing)
                {
                    menuItem.Enabled = true;
                }

                if (panelSubtitles.GetNextControl(focused, false) == null)
                {
                    mergeWithPreviousToolStripMenuItem.Enabled = false;
                }

                if (panelSubtitles.GetNextControl(focused, true) == null)
                {
                    mergeWithNextToolStripMenuItem.Enabled = false;
                }
            }

            transcribeToolStripMenuItem.Enabled = videoFilename != null;

            clearToolStripMenuItem.Enabled = !disable;
        }

        private void setFocused(Subtitle subtitle)
        {
            focused = subtitle;
            if (editedTimestamp != null) return;
            enableIfFocused();
        }

        private void editTimestamp(Subtitle subtitle)
        {
            if (editedTimestamp == subtitle)
            {
                editedTimestamp = null;
                subtitle.TimestampEditing = false;
                enableIfFocused();
            }
            else
            {
                editedTimestamp = subtitle;
                subtitle.TimestampEditing = true;
                enableIfFocused(true);
            }
        }

        private void repositionSubtitles()
        {
            int y = 0;
            int i = 0;

            foreach (Control control in panelSubtitles.Controls)
            {
                Subtitle subtitle = (Subtitle)control;
                subtitle.Location = new Point(subtitle.Margin.Left, y + subtitle.Margin.Top);
                subtitle.TabIndex = i++;
                y += subtitle.Height;
            }

            panelSubtitles.ScrollControlIntoView(focused);
        }

        private List<SubtitleEntry> produceSubtitles()
        {
            var subtitles = new List<SubtitleEntry>();

            foreach (var control in panelSubtitles.Controls)
            {
                var subtitle = (Subtitle)control;
                subtitles.Add(new SubtitleEntry { Timestamp = subtitle.Timestamp, Content = subtitle.Content });
            }

            return subtitles;
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

        private void openSubtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openFileDialogSubtitles.ShowDialog() == DialogResult.OK)
            {
                panelSubtitles.Controls.Clear();

                try
                {
                    string yml = File.ReadAllText(openFileDialogSubtitles.FileName);

                    var deserializer = new DeserializerBuilder()
                        .WithNamingConvention(CamelCaseNamingConvention.Instance)
                        .Build();

                    var subtitles = deserializer.Deserialize<List<SubtitleEntry>>(yml);

                    foreach (var subtitle in subtitles)
                    {
                        createSubtitle(subtitle.Timestamp, subtitle.Content.Replace("\n", "\r\n"));
                    }

                    repositionSubtitles();
                }
                catch (IOException ex)
                {
                    MessageBox.Show(ex.Message, "Failed to load subtitles");
                }
            }
        }

        private void saveSubtitles()
        {
            var serializer = new SerializerBuilder()
                .WithNamingConvention(CamelCaseNamingConvention.Instance)
                .Build();

            var yaml = serializer.Serialize(produceSubtitles());

            File.WriteAllText(subtitleFilename, yaml);
        }

        private void saveSubtitlesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (subtitleFilename == null)
            {
                if (saveFileDialogSubtitles.ShowDialog() != DialogResult.OK) return;
                subtitleFilename = saveFileDialogSubtitles.FileName;
            }

            saveSubtitles();
        }

        private void saveSubtitlesAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogSubtitles.ShowDialog() != DialogResult.OK) return;
            subtitleFilename = saveFileDialogSubtitles.FileName;

            saveSubtitles();
        }

        private void createNarrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (saveFileDialogVideo.ShowDialog() != DialogResult.OK) return;
            subtitleFilename = saveFileDialogSubtitles.FileName;

            var narrator = new Narrator(credentials.Narration);

            narrator.SynthesisStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
                toolStripStatusLabel.Text = "Synthesising speech..."
            ));
            narrator.AlignmentStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
                toolStripStatusLabel.Text = "Aligning speech..."
            ));
            narrator.CombinationStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
                toolStripStatusLabel.Text = "Combining audio and video..."
            ));
            narrator.ProgressUpdated += (object sender1, Narrator.ProgressEventArgs e1) => this.Invoke(new Action(() =>
            {
                if (e1.Percent > 100) Console.WriteLine(e1.Percent);
                else toolStripProgressBar.Value = e1.Percent;
            }));
            narrator.ErrorOccurred += (object sender1, NarrationErrorEventArgs e1) => this.Invoke(new Action(() =>
                MessageBox.Show(e1.Error.Message, "An error occurred")
            ));
            narrator.NarrationCompleted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
            {
                panelSubtitles.Enabled = true;
                axWindowsMediaPlayer.Enabled = true;

                fileToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
                enableIfFocused();

                toolStripStatusLabel.Text = "Done";
                toolStripProgressBar.Value = 0;
            }));

            narrator.Narrate(produceSubtitles(), videoFilename, saveFileDialogVideo.FileName);
        }

        private void axWindowsMediaPlayer_PositionChange(object sender, AxWMPLib._WMPOCXEvents_PositionChangeEvent e)
        {
            if (editedTimestamp == null) return;

            double t = e.newPosition;
            editedTimestamp.Timestamp = TimeSpan.FromMilliseconds(t);
        }

        private void timerPlayback_Tick(object sender, EventArgs e)
        {
            if (editedTimestamp == null) return;

            double t = axWindowsMediaPlayer.Ctlcontrols.currentPosition;
            editedTimestamp.Timestamp = TimeSpan.FromMilliseconds(t);
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

            if (result != DialogResult.OK) return;

            // Disable everything except the transcription cancellation menu item

            ToolStripMenuItem[] inactiveWhileTranscribing =
            {
                fileToolStripMenuItem,
                editToolStripMenuItem,
                insertBeforeToolStripMenuItem,
                insertAfterToolStripMenuItem,
                deleteToolStripMenuItem,
                mergeWithNextToolStripMenuItem,
                mergeWithPreviousToolStripMenuItem,
                splitToolStripMenuItem
            };

            panelSubtitles.Enabled = false;
            axWindowsMediaPlayer.Enabled = false;

            fileToolStripMenuItem.Enabled = false;
            editToolStripMenuItem.Enabled = false;
            enableIfFocused(true);

            transcribeToolStripMenuItem.Text = "Cancel transcription";
            transcribeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.Shift | Keys.T;

            var transcriber = new Transcriber(credentials.Transcription);

            transcriber.LoadStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
                toolStripStatusLabel.Text = "Loading video..."
            ));
            transcriber.UploadStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() =>
                toolStripStatusLabel.Text = "Uploading video..."
            ));
            transcriber.TranscriptionStarted += (object sender1, EventArgs e1) => this.Invoke(new Action(() => 
                toolStripStatusLabel.Text = "Transcribing video..."
            ));
            transcriber.ProgressUpdated += (object sender1, Transcriber.ProgressEventArgs e1) => this.Invoke(new Action(() =>
                toolStripProgressBar.Value = e1.Percent
            ));
            transcriber.ErrorOccurred += (object sender1, TranscriptionErrorEventArgs e1) => this.Invoke(new Action(() =>
                MessageBox.Show(e1.Error.Message, "An error occurred")
            ));
            transcriber.TranscriptionCompleted += (object sender1, TranscriptionEventArgs e1) => this.Invoke(new Action(() =>
            {
                panelSubtitles.Controls.Clear();

                foreach (var subtitle in e1.Subtitles)
                {
                    createSubtitle(subtitle.Timestamp, subtitle.Content);
                }

                repositionSubtitles();

                panelSubtitles.Enabled = true;
                axWindowsMediaPlayer.Enabled = true;

                fileToolStripMenuItem.Enabled = true;
                editToolStripMenuItem.Enabled = true;
                enableIfFocused();

                transcribeToolStripMenuItem.Text = "Transcribe";
                transcribeToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.T;

                toolStripStatusLabel.Text = "Done";
                toolStripProgressBar.Value = 0;
            }));

            Task.Run(() => transcriber.Transcribe(videoFilename));
        }

        private void insertBeforeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = panelSubtitles.Controls.IndexOf(focused);
            var subtitle = createSubtitle(focused.Timestamp, "");
            subtitle.Focus();

            panelSubtitles.Controls.SetChildIndex(subtitle, index);
            repositionSubtitles();
        }

        private void insertAfterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = panelSubtitles.Controls.IndexOf(focused);
            var subtitle = createSubtitle(focused.Timestamp, "");
            subtitle.Focus();

            panelSubtitles.Controls.SetChildIndex(subtitle, index + 1);
            repositionSubtitles();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var next = (Subtitle)panelSubtitles.GetNextControl(focused, true);
            var prev = (Subtitle)panelSubtitles.GetNextControl(focused, false);

            panelSubtitles.Controls.Remove(focused);
            (next ?? prev)?.Focus();
            repositionSubtitles();
        }

        private void mergeWithNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var next = (Subtitle)panelSubtitles.GetNextControl(focused, true);
            int cursor = focused.ContentCursor;
            focused.Content = (focused.Content.Trim() + "\r\n" + next.Content.Trim()).Trim();
            focused.ContentCursor = cursor;

            panelSubtitles.Controls.Remove(next);
            repositionSubtitles();
        }

        private void mergeWithPreviousToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var prev = (Subtitle)panelSubtitles.GetNextControl(focused, false);
            prev.Content = (prev.Content.Trim() + "\r\n" + focused.Content.Trim()).Trim();

            panelSubtitles.Controls.Remove(focused);
            prev.Focus();
            repositionSubtitles();
        }

        private void splitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int index = panelSubtitles.Controls.IndexOf(focused);
            int cursor = focused.ContentCursor;
            var subtitle = createSubtitle(focused.Timestamp, focused.Content.Substring(cursor).Trim());
            focused.Content = focused.Content.Substring(0, cursor).Trim();
            focused.ContentCursor = cursor;

            panelSubtitles.Controls.SetChildIndex(subtitle, index + 1);
            repositionSubtitles();
        }
    }
}

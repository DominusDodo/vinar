using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinar
{
    public partial class CredentialsEditor : Form
    {

        public string Password
        {
            get { return textBoxPassword.Text; }
            set { textBoxPassword.Text = value; }
        }

        public Credentials Credentials
        {
            get { return getCredentials(); }
            set { setCredentials(value); }
        }

        public CredentialsEditor()
        {
            InitializeComponent();
        }

        private Credentials getCredentials()
        {
            var credentials = new Credentials();
            credentials.Transcription.Url = textBoxTranscriptionUrl.Text;
            credentials.Transcription.Location = textBoxTranscriptionLocation.Text;
            credentials.Transcription.Id = textBoxTranscriptionId.Text;
            credentials.Transcription.Key = textBoxTranscriptionKey.Text;
            credentials.Narration.Location = textBoxNarrationLocation.Text;
            credentials.Narration.Key = textBoxNarrationKey.Text;

            return credentials;
        }

        private void setCredentials(Credentials credentials)
        {
            textBoxTranscriptionUrl.Text = credentials.Transcription.Url;
            textBoxTranscriptionLocation.Text = credentials.Transcription.Location;
            textBoxTranscriptionId.Text = credentials.Transcription.Id;
            textBoxTranscriptionKey.Text = credentials.Transcription.Key;
            textBoxNarrationLocation.Text = credentials.Narration.Location;
            textBoxNarrationKey.Text = credentials.Narration.Key;
        }

        private void buttonKeepChanges_Click(object sender, EventArgs e)
        {
            var credentials = getCredentials();

            DialogResult = DialogResult.OK;
            Close();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void checkAll()
        {
            TextBox[] textboxes =
            {
                textBoxTranscriptionUrl,
                textBoxTranscriptionLocation,
                textBoxTranscriptionId,
                textBoxTranscriptionKey,
                textBoxNarrationLocation,
                textBoxNarrationKey,
                textBoxPassword
            };

            bool filled = textboxes.All(tb => !String.IsNullOrWhiteSpace(tb.Text));

            buttonKeepChanges.Enabled = filled && checkUrl();
        }

        private bool checkUrl()
        {
            return Regex.Match(textBoxTranscriptionUrl.Text, @"https://(\w+)(\.\w+)+").Success;
        }

        private void textBoxTranscriptionUrl_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxTranscriptionUrl.Text))
            {
                errorProvider.SetError(textBoxTranscriptionUrl, "Transcription service URL cannot be empty");
            }
            else if (!checkUrl())
            {
                errorProvider.SetError(textBoxTranscriptionUrl, "Transcription service URL should be similar to https://domain.com");
            }
            else
            {
                errorProvider.SetError(textBoxTranscriptionUrl, String.Empty);
            }

            checkAll();
        }

        private void textBoxTranscriptionLocation_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxTranscriptionLocation.Text))
            {
                errorProvider.SetError(textBoxTranscriptionLocation, "Transcription service location cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxTranscriptionLocation, String.Empty);
            }

            checkAll();
        }

        private void textBoxTranscriptionId_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxTranscriptionId.Text))
            {
                errorProvider.SetError(textBoxTranscriptionId, "Transcription service account ID cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxTranscriptionId, String.Empty);
            }

            checkAll();
        }

        private void textBoxTranscriptionKey_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxTranscriptionKey.Text))
            {
                errorProvider.SetError(textBoxTranscriptionKey, "Transcription service key cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxTranscriptionKey, String.Empty);
            }

            checkAll();
        }

        private void textBoxNarrationLocation_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxNarrationLocation.Text))
            {
                errorProvider.SetError(textBoxNarrationLocation, "Narration service location cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxNarrationLocation, String.Empty);
            }

            checkAll();
        }

        private void textBoxNarrationKey_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxNarrationKey.Text))
            {
                errorProvider.SetError(textBoxNarrationKey, "Narration service key cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxNarrationKey, String.Empty);
            }

            checkAll();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(textBoxPassword.Text))
            {
                errorProvider.SetError(textBoxPassword, "Credentials password cannot be empty");
            }
            else
            {
                errorProvider.SetError(textBoxPassword, String.Empty);
            }

            checkAll();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Vinar
{
    public partial class CredentialsPasswordEntry : Form
    {
        public string Password
        {
            get { return textBoxPassword.Text; }
        }

        public CredentialsPasswordEntry()
        {
            InitializeComponent();
        }

        private void buttonUnlock_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            buttonUnlock.Enabled = !String.IsNullOrEmpty(textBoxPassword.Text);
        }
    }
}

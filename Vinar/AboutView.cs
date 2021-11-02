using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vinar.Properties;

namespace Vinar
{
    public partial class AboutView : Form
    {
        private Dictionary<string, string> licences = new Dictionary<string, string> {
            ["Effortless.Net.Encryption"] = Resources.Effortless_Net_Encryption,
            ["FFmpeg"] = Resources.FFmpeg,
            ["Microsoft.CognitiveServices.Speech"] = Resources.Microsoft_CognitiveService_Speech,
            ["Newtonsoft.Json"] = Resources.Newtonsoft_Json,
            ["Xabe.FFmpeg"] = Resources.Xabe_FFmpeg,
            ["Xabe.FFmpeg.Downloader"] = Resources.Xabe_FFmpeg_Downloader,
            ["YamlDotNet"] = Resources.YamlDotNet,
        };

        public AboutView()
        {
            InitializeComponent();
        }

        private void AboutView_Load(object sender, EventArgs e)
        {
            foreach (var name in licences.Keys)
            {
                listBoxDependencies.Items.Add(name);
            }
        }

        private void listBoxDependencies_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = (string)listBoxDependencies.SelectedItem;
            string licence = licences[name];

            textBoxLicence.Text = licence;
        }
    }
}

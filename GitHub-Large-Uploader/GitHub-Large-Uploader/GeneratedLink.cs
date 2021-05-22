using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Jackson_Download_Manager_API;

namespace GitHub_Large_Uploader
{
    public partial class GeneratedLink : Form
    {
        public GeneratedLink()
        {
            InitializeComponent();
        }

        public string GeneratedLinkString { get; set; }
        private void button2_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(DownloadCodeTextBox.Text);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(LinkTextBox.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void GeneratedLink_Load(object sender, EventArgs e)
        {
            LinkTextBox.Text = Form1.GenerateLinkDetails;
            DownloadCodeTextBox.Text = await GenerateDownloadCode.GenerateCodeFromGitHubCloneLink(LinkTextBox.Text);
        }
    }
}

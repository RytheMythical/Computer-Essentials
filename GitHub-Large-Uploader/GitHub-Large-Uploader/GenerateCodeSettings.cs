using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GitHub_Large_Uploader
{
    public partial class GenerateCodeSettings : Form
    {
        public GenerateCodeSettings()
        {
            InitializeComponent();
        }

        public static string GitHubUsername = "";
        private void button1_Click(object sender, EventArgs e)
        {
            GitHubUsername = textBox1.Text;
            Close();
        }
    }
}

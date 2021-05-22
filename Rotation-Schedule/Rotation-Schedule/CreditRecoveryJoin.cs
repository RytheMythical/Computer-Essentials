using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rotation_Schedule
{
    public partial class CreditRecoveryJoin : Form
    {
        public CreditRecoveryJoin()
        {
            InitializeComponent();
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Load += CreditRecoveryJoin_Load;
        }

        private async void CreditRecoveryJoin_Load(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\CreditRecoveryMeetingLink.txt"))
            {
                if (File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\CreditRecoveryMeetingLink.txt") != "")
                {
                    textBox1.Text = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\CreditRecoveryMeetingLink.txt");
                    for (int i = 15; i >= 0; i--)
                    {
                        button1.Text = "Join (" + i + ")";
                        await Task.Delay(1000);
                    }
                    Form1.CreditRecoveryMeetLink = textBox1.Text;
                    Close();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (RememberLinkCheckBox.Checked)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\CreditRecoveryMeetingLink.txt",textBox1.Text);
            }
            Form1.CreditRecoveryMeetLink = textBox1.Text;
            Close();
        }
    }
}

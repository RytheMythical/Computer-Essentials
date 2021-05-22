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
    public partial class Automation_Settings : Form
    {
        public Automation_Settings()
        {
            InitializeComponent();
        }

        public static bool Base64 = false;
        public static bool Encrypt = false;
        public static string DecryptionKey = "";
        private void DoneButton_Click(object sender, EventArgs e)
        {
            if (Base64CheckBox.Checked == true)
            {
                Base64 = true;
            }
            else
            {
                Base64 = false;
            }

            if (EncryptFilesCheckBox.Checked == true)
            {
                if (DecryptionKeyTextBox.Text == "")
                {
                    MessageBox.Show("Decryption key could not be blank");
                }
                else
                {
                    Encrypt = true;
                    DecryptionKey = DecryptionKeyTextBox.Text;
                }
            }
            else
            {
                Encrypt = false;
            }
            Close();
        }

        private void EncryptFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (EncryptFilesCheckBox.Checked == true)
            {
                DecryptionKeyTextBox.Enabled = true;
            }
            else
            {
                DecryptionKeyTextBox.Enabled = false;
            }
        }

        private void Automation_Settings_Load(object sender, EventArgs e)
        {
            EncryptFilesCheckBox.Checked = Encrypt;
            Base64CheckBox.Checked = Base64;
            DecryptionKeyTextBox.Text = DecryptionKey;
        }
    }
}

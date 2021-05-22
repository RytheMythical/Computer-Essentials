using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Notifier
{
    public partial class History : Form
    {
        public History()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(listBox1.SelectedItem.ToString());
        }
        StorageCode Storage = new StorageCode();
        private string FTPUsername = "epiz_26544505";
        private string FTPPassword = "lmNUkQD5XdiJE";
        private void History_Load(object sender, EventArgs e)
        {
            ShowIcon = false;
            //ShowInTaskbar = false;
            TopMost = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            foreach (string s in Storage.ClipboardHistory)
            {
                listBox1.Items.Add(s);
            }

            try
            {
                if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"))
                {
                    foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"))
                    {
                        listBox1.Items.Add(readLine.ToString());
                    }

                    foreach (var listBox1Item in listBox1.Items)
                    {
                        foreach (var box1Item in listBox1.Items)
                        {
                            if (listBox1Item.ToString() == box1Item.ToString())
                            {
                                listBox1.Items.Remove(listBox1Item);
                            }
                        }
                    }
                    File.Delete(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt");
                    foreach (var listBox1Item in listBox1.Items)
                    {
                        if (!File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"))
                        {
                            File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt", listBox1Item.ToString());
                        }
                        else
                        {
                            File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt", File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt") + "\n" + listBox1Item.ToString());
                        }
                    }
                    UploadStringToFTP("ftp://ftpupload.net/htdocs/ClipboardStorage/" + Environment.UserName + ".txt", File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"));

                }
                else
                {
                    File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt", DownloadStringFromFTP("ftp://ftpupload.net/htdocs/ClipboardStorage/" + Environment.UserName + ".txt"));
                }
            }
            catch (Exception)
            {

                
            }
        }
        private string DownloadStringFromFTP(string link)
        {
            string Return = "";
            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(FTPUsername, FTPPassword);
                    Return = client.DownloadString(new Uri(link));
                }
            }
            catch
            {

            }

            return Return;
        }
        private void UploadStringToFTP(string link, string content)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential(FTPUsername, FTPPassword);
                    client.UploadString(new Uri(link), content);
                    while (client.IsBusy)
                    {
                        Task.Delay(10);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"))
                {
                    File.Delete(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt");
                }
                listBox1.Items.Clear();
            }
            catch (Exception)
            {

            }
        }
    }
}

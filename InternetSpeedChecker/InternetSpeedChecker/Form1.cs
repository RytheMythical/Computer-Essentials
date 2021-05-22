using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Diagnostics;
using System.IO;
using InternetSpeedChecker.Properties;
using SpeedTestAPI;

namespace InternetSpeedChecker
{

    public partial class Form1 : Form
    {
        private class WebClient : System.Net.WebClient
        {
            public int Timeout { get; set; }

            protected override WebRequest GetWebRequest(Uri uri)
            {
                WebRequest client = base.GetWebRequest(uri);
                client.Timeout = Timeout;
                ((HttpWebRequest) client).ReadWriteTimeout = Timeout;
                return client;
            }
        }
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load1;
            listBox1.SelectedValueChanged += (sender, args) =>
            {
                AutoDismiss = false;
            };

        }
        bool AutoDismiss = true;
        private async void Form1_Load1(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            TopMost = true;
            MaximizeBox = false;
            Visible = false;
            string SpeedTestEXE = Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest" + Path.GetRandomFileName().Replace(".", "") + ".exe";
            File.WriteAllBytes(SpeedTestEXE,Resources.speedtest);
            using (var p = new Process())
            {
                p.StartInfo.FileName = SpeedTestEXE;
                p.StartInfo.Arguments = "-u MB/s -fjson";
                p.StartInfo.UseShellExecute = false;
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo.RedirectStandardError = true;
                p.StartInfo.RedirectStandardOutput = true;
                p.Start();
                string Output = p.StandardOutput.ReadToEnd();
                var SpeedTest = SpeedTestAPI.SpeedTest.FromJson(Output);
                string DownloadSpeed = (SpeedTest.Download.Bandwidth / 1024d / 1024d).ToString("0.00");
                string UploadSpeed = (SpeedTest.Upload.Bandwidth / 1024d / 1024d).ToString("0.00");
                DownloadLabel.Text = "Download: " + DownloadSpeed + " MB/s";
                UploadLabel.Text = "Upload: " + UploadSpeed + " MB/s";

                void AddList(string Stuff)
                {
                    listBox1.Items.Add(Stuff);
                }
                AddList("ISP: " + SpeedTest.Isp);
                AddList("Using VPN: " + SpeedTest.Interface.IsVpn);
                AddList("PUBLIC IP: " + SpeedTest.Server.Ip);
                AddList("Internal IP: " + SpeedTest.Interface.InternalIp);
                AddList("Location of server: " + SpeedTest.Server.Location);
                AddList("MAC Address: " + SpeedTest.Interface.MacAddr);
                AddList("Timestamp: " + SpeedTest.Timestamp.Date.ToString("F"));
                PingLabel.Text = "PING: " + SpeedTest.Ping.Latency.ToString("0.00");
            }

            for (int i = 10; i >= 0; i--)
            {
                Text = "Closing in.. " + i;
                await Task.Delay(1000);
                if (AutoDismiss == false)
                {
                    Text = "Internet Details";
                    break;
                }
            }

            if (AutoDismiss == true)
            {
                Application.Exit();
            }
        }

        long bytes;
        bool Connected = false;
        bool KeepDownloading = true;
        string Speed = string.Empty;
        Stopwatch sw = new Stopwatch();
        Stopwatch timeoutdew = new Stopwatch();
        Timer Timehui = new Timer();
        private int count = 3;
        bool FirstTime = true;
        WebClient client = new WebClient();
        string FileName = String.Empty;
        string User = System.Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
        private async void Form1_Load(object sender, EventArgs e)
        {
            //if (!Directory.Exists(
            //    @"C:\Users\335384137\Documents\GitHub\InternetSpeedChecker\InternetSpeedChecker\bin\Debug"))
            //{
               
            //}

            //DismissLabel.Visible = false;
            //TopMost = true;
            //ControlBox = false;
            //ShowInTaskbar = false;
            //SpeedLabel.Visible = false;
            //while (KeepDownloading == true)
            //{
            //    await Task.Delay(1000);
            //    client.DownloadProgressChanged += Client_DownloadProgressChanged;
            //            client.DownloadFileCompleted += Client_DownloadFileCompleted;
            //            client.Timeout = 10000;
            //            sw.Start();
            //            try
            //            {
            //                Console.WriteLine("Downloading");
            //                int times = 0;
            //                timeoutdew.Start();
            //                Timehui.Interval = 10000;
            //                Timehui.Start();
            //                Console.WriteLine(Timehui.Interval);
            //                Timehui.Tick += Timehui_Tick1;
            //                FileName = User + "\\Documents\\SpeedCheck.txt";
            //        if(File.Exists(User + "\\Documents\\SpeedCheck.txt"))
            //        {
            //            File.Delete(User + "\\Documents\\SpeedCheck.txt");
            //        }
            //        Visible = false;
            //                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/CNTowerGUN/VHJhaW4gU2ltIDIwMTg/master/DewHui.part003.rar.jer"), User + "\\Documents\\SpeedCheck.txt");
            //                while (client.IsBusy)
            //                {
            //                    await Task.Delay(10);
            //                }
            //        Timehui.Stop();
            //            }
            //            catch (Exception ex)
            //            {
            //                Console.WriteLine(ex);
            //                SpeedLabel.Visible = false;
            //                MainLabel.Text = "Internet may not be available...";
            //                await Task.Delay(3000);
            //                Visible = false;
            //            }
                    

                    
            //        if (Connected == true)
            //        {
            //            KeepDownloading = false;
            //            Console.WriteLine("Connected to internet");
            //        }
            //        else
            //        { 
            //            Console.WriteLine("Not connected");
            //            if (!Cancel == true)
            //            {
            //                KeepDownloading = true;
            //            }
            //        }
            //}
            //Visible = true;
            //DismissLabel.Visible = false;
            //MainLabel.Text = "Your Average Internet Speed:";
            //SpeedLabel.Text = Speed + " MB/S";
            //await Task.Delay(3000);
            //MainLabel.Text = "Enjoy Using Rogers Internet";
            //await Task.Delay(3000);
            //Application.Exit();
        }

        private bool Cancel = false;
        private void Timehui_Tick1(object sender, EventArgs e)
        {
            Console.WriteLine("Tick");
            client.CancelAsync();
            KeepDownloading = false;
            //MainLabel.Text = "Internet Connection Is Slow";
            DismissLabel.Visible = true;
            ControlBox = true;
        }

        private void Timehui_Tick(object sender, EventArgs e)
        {
            
        }

        private async void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            sw.Reset();
            if(e.Error != null)
            { 
                DismissLabel.Visible = true;
                //SpeedLabel.Visible = false;
                //MainLabel.Text = "Your Internet Is Slow";
                Console.WriteLine("Slow Internet");
                FirstTime = false;
                await Task.Delay(3000);
                ///Visible = false;
                FileInfo dew = new FileInfo(FileName);
                string FileSize = dew.Length.ToString();
                if (FileSize == TotalBytes)
                {
                    Console.WriteLine("Download Succeeded");
                }
                else
                {
                    Console.WriteLine("Incomplete File Download");
                    try
                    {
                        File.Delete(FileName);
                    }
                    catch
                    {

                    }
                }
            }
            else if (e.Cancelled)
            {
                Cancel = true;
                KeepDownloading = false;
                FileInfo dew = new FileInfo(FileName);
                string FileSize = dew.Length.ToString();
                if (FileSize == TotalBytes)
                {
                    Console.WriteLine("Download Succeeded");
                }
                else
                {
                    Console.WriteLine("Incomplete File Download");
                    try
                    {
                        File.Delete(FileName);
                    }
                    catch
                    {

                    }
                }
            }
            else
            {
                Visible = true;
                Connected = true;
            }
        }

        private string TotalBytes = string.Empty;
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
        private async void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously
        {
            TotalBytes = e.TotalBytesToReceive.ToString();
            //SpeedLabel.Visible = true;
            //progressBar1.Value = e.ProgressPercentage;
            Speed = string.Format("{0}", (e.BytesReceived/ 1024d / 1024d / sw.Elapsed.TotalSeconds).ToString("0.00"));
            //SpeedLabel.Text = Speed + " MB/s";
        }

        private void DismissLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                string Item = listBox1.SelectedItem.ToString();
                Clipboard.SetText(Item);
            }
        }
    }
}

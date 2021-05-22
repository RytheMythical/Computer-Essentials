using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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
using UsefulTools;

namespace Loader
{
    public partial class Main_Loader : Form
    {
        public Main_Loader()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            await Internet.CheckInternet();
            //if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) +
            //                 "\\OntarioNewCasesReporter.exe"))
            //{
            //    File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) +
            //                                          "\\OntarioNewCasesReporter.exe");
            //}

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Coronavirus-Announcement/master/OntarioAnnouncer/OntarioAnnouncer/bin/Debug/OntarioAnnouncer.exe"),Environment.GetEnvironmentVariable("TEMP") + "\\NewOntarioCasesReporter.exe" );
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\NewOntarioCasesReporter.exe");
            Application.Exit();
        }
    }

    public class dWebHook : IDisposable
    {
        private readonly WebClient dWebClient;
        private static NameValueCollection discord = new NameValueCollection();
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }

        public dWebHook()
        {
            dWebClient = new WebClient();
        }


        public void SendMessage(string msgSend)
        {
            discord.Add("username", UserName);
            discord.Add("avatar_url", ProfilePicture);
            discord.Add("content", msgSend);

            dWebClient.UploadValues(WebHook, discord);
        }

        public void Dispose()
        {
            dWebClient.Dispose();
        }
    }
}

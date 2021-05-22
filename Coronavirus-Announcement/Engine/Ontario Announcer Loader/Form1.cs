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
using UsefulTools;

namespace Ontario_Announcer_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string ThisFile = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\AnnouncerLoader.exe";
        string DetailedCoronavirusLink = "https://raw.githubusercontent.com/EpicGamesGun/Coronavirus-Announcement/master/OntarioAnnouncer/OntarioAnnouncer/bin/Debug/OntarioAnnouncer.exe";
        string TempName = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".exe";

        private async void Form1_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Visible = false;
            try
            {
                await Internet.CheckInternet();
                if (!File.Exists(ThisFile))
                {
                    File.Copy(Application.ExecutablePath, ThisFile, true);
                }

                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(TempName), TempName);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
                Process.Start(TempName);
            }
            catch
            {
                
            }
            Application.Exit();
        }
    }
}

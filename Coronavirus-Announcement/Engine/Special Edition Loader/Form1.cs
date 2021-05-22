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

namespace Special_Edition_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        string SpecialEditionFile = Environment.GetEnvironmentVariable("TEMP") + "\\SpecialEdition";
        string DetailedCheckLoaderLink = "https://raw.githubusercontent.com/EpicGamesGun/Coronavirus-Announcement/master/Engine/Ontario%20Announcer%20Loader/bin/Debug/Ontario%20Announcer%20Loader.exe";
        string CoronavirusLink = "https://raw.githubusercontent.com/EpicGamesGun/Coronavirus-Announcement/master/Engine/Engine/bin/Debug/Engine.exe";
        string DetailedCoronavirusLink = "https://raw.githubusercontent.com/EpicGamesGun/Coronavirus-Announcement/master/OntarioAnnouncer/OntarioAnnouncer/bin/Debug/OntarioAnnouncer.exe";
        string TempName = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".exe";
        string LoaderName = Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\AnnouncerLoader.exe";
        private async void Form1_Load(object sender, EventArgs e)
        {
            Text = "Coronavirus Tracker";
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            ShowIcon = false;
            File.WriteAllText(SpecialEditionFile, "ha");
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (o, args) =>
                {
                    progressBar1.Value = args.ProgressPercentage;
                };
                client.DownloadFileAsync(new Uri(CoronavirusLink), TempName);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Visible = false;
            await Task.Factory.StartNew(() =>
            {
                Process.Start(TempName).WaitForExit();
            });
            File.Delete(SpecialEditionFile);
            Application.Exit();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            ShowIcon = false;
            File.WriteAllText(SpecialEditionFile, "ha");
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (o, args) =>
                {
                    progressBar1.Value = args.ProgressPercentage;
                };
                client.DownloadFileAsync(new Uri(DetailedCoronavirusLink), TempName);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            if (checkBox1.Checked == true)
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (o, args) =>
                    {
                        progressBar1.Value = args.ProgressPercentage;
                    };
                    client.DownloadFileAsync(new Uri(DetailedCheckLoaderLink), LoaderName);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
            Visible = false;
            await Task.Factory.StartNew(() =>
            {
                Process.Start(TempName).WaitForExit();
            });
            Application.Exit();
        }
    }
}

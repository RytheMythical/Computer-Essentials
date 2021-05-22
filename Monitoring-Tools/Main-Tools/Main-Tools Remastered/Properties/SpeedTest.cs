using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using Main_Tools;

namespace Main_Tools_Remastered.Properties
{
    public class SpeedTest
    {
        public string InternetServiceProvider { get; set; }

        public string UploadSpeed { get; set; }
        public string DownloadSpeed{ get; set; }
        public string Ping{ get; set; }
        public string ResultURL{ get; set; }
        public string Error{ get; set; }

        public bool NetGearedSet { get; set; }
        public async Task Start()
        {
            string URL = "";
            string ISP = "";
            if (!Directory.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest"))
            {
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest");
            }

            try
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Main.zip",
                    Resources.SpeedTest1);
                ZipFile.ExtractToDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Main.zip",
                    Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest");
            }
            catch (Exception exception)
            {

            }
            string LicenseFile = Environment.GetEnvironmentVariable("TEMP") +
                                 "\\AppData\\Roaming\\Ookla\\Speedtest CLI\\speedtest-cli.ini";
            if (!File.Exists(LicenseFile))
            {
                await RunCommandHidden("cd \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\SpeedTest\necho yes|speedtest.exe");
            }
            await RunCommandHidden("cd \"" + Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest" +
                                   "\"\n > " +
                                   Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt (" +
                                   "\necho YES|speedtest.exe\n)");

            foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
                                                        "\\SpeedTest\\Log.txt"))
            {
                if (readLine.Contains("Result URL"))
                {
                    URL = readLine.Replace("Result URL:", "");
                    URL = URL.Replace(" ", "");
                }

                if (readLine.Contains("ISP"))
                {
                    ISP = readLine.Replace("       ISP: ", "");
                    ISP = readLine.Replace(" ", "");
                }
            }

            if (ISP.Contains("Carry") || ISP.Contains("carry"))
            {
                await PlaySoundSync(Resources.CarryTelecom);
            }
            else if (ISP.Contains("Rogers") || ISP.Contains("rogers"))
            {
                await PlaySoundSync(Resources.RogersISP);
            }
            else if (ISP.Contains("Fido") || ISP.Contains("fido"))
            {
                await PlaySoundSync(Resources.Fido);
            }
            else if (ISP.Contains("Bell") || ISP.Contains("bell"))
            {
                await PlaySoundSync(Resources.BellCanada);
            }
            else if (ISP.Contains("Virgin Mobile") || ISP.Contains("Virgin") || ISP.Contains("virgin"))
            {
                await PlaySoundSync(Resources.VirginMobile);
            }
            else if (ISP == "")
            {
                await PlaySoundSync(Resources.NewNotConnectedToInternet);
            }
            else
            {
                await PlaySoundSync(Resources.NewConnectedToVPN);
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ISP", ISP);
            InternetServiceProvider = ISP;
            string ServiceProvider(string input)
            {
                string Return = "";
                if (input == "")
                {
                    Return = "NO INTERNET";
                }
                else
                {
                    Return = input;
                }

                return Return;
            }

            SoundPlayer dew = new SoundPlayer(Resources.CompletedSpeedTest_SP);
            await Task.Factory.StartNew(() =>
            {
                dew.PlaySync();
            });

            foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
                                                    "\\SpeedTest\\Log.txt"))
            {
                if(readLine.Contains("Download"))
                {
                    DownloadSpeed = readLine.Split(':')[1].Trim();
                    DownloadSpeed = DownloadSpeed.Split('(')[0].Trim();
                    DownloadSpeed.Replace("Mbps", "");
                }
                if(readLine.Contains("Upload"))
                {
                    UploadSpeed = readLine.Split(':')[1].Trim();
                    UploadSpeed = UploadSpeed.Split('(')[0].Trim();
                    UploadSpeed.Replace("Mbps", "");
                }
                if(readLine.Contains("Latency"))
                {
                    Ping = readLine.Split(':')[1].Trim();
                    Ping = Ping.Split('(')[0].Trim();
                    Ping.Replace("Mbps", "");
                }
                if(readLine.Contains(" Result URL"))
                {
                    ResultURL = readLine.Replace(" Result URL:", "");
                }
            }
            string NetGeared()
            {
                string Return = ""; 
                if(NetGearedSet == true)
                {
                    Return = "NETGEARED";
                }
                else
                {
                    Return = "ROGERSED";
                }
                return Return;
            }
            TextFile Email = new TextFile();
            Random EmailFiller = new Random();
            string PingerNumber = "##" + EmailFiller.Next(111, 999).ToString() + "##";
            await Email.SendEmail("kelvin01570@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("cntowergun@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("anthonyk102@hotmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("wendy09387@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));

            await Email.SendEmail("cntowergun@gmail.com", "Error that caused popup " + PingerNumber, "Reason that the popup fired", Error);
        }

        private bool Exit = false;
        private async Task RunCommandHidden(string Command)
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
            //File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }
        private async void PlaySound(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            dew.Play();
        }

        private async Task PlaySoundSync(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }
        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }
    }
}

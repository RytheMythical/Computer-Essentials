using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Main_Tools_Remastered.Properties
{
    public class MainFunctions
    {
        public async Task PleaseContactSound()
        {
            string URL = "";
            string ISP = "";
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
            await PlaySoundSync(Resources.PleaseContact);
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
                await PlaySoundSync(Resources.NotConnected);
            }
            else
            {
                await PlaySoundSync(Resources.ConnectedToVPN);
            }
            await PlaySoundSync(Resources.IfYouStillDoNotHaveInternet);
        }
        public async Task PlaySoundSync(Stream Location)
        {
            TimeSpan start = new TimeSpan(8, 0, 0); //10 o'clock
            TimeSpan end = new TimeSpan(23, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                //match found
                SoundPlayer d = new SoundPlayer(Location);
                await Task.Factory.StartNew(() =>
                {
                    d.PlaySync();
                });
            }
            else
            {
                
            }
        }

        public async Task FireContactISP()
        {
            if(File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"))
            {
                string ISP = "";
                foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"))
                {
                    if(readLine.Contains("ISP"))
                    {
                        ISP = readLine.Split(':')[1].Trim().Replace(" ", "");
                        break;
                    }
                }
                await PlaySoundSync(Resources.PleaseContact);
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
                await PlaySoundSync(Resources.DontHaveInternet);
            }
        }
        public async Task<bool> CheckInternet()
        {
            bool Return = false;
            string CheckString = "";

            BackgroundWorker Checker = new BackgroundWorker();
            Checker.DoWork += (sender, args) =>
            {
                try
                {
                    CheckString = new WebClient().DownloadString("http://www.msftncsi.com/ncsi.txt");
                }
                catch
                {
                    Return = false;
                }
            };
            Checker.RunWorkerAsync();
            while (Checker.IsBusy)
            {
                await Task.Delay(10);
            }
            Checker.Dispose();
            if (CheckString == "Microsoft NCSI")
            {
                Return = true;
            }
            else
            {
                Return = false;
            }

            return Return;
        }

        public async Task<bool> CheckNetGear()
        {
            bool Return = false;
            string CheckNetGear = "";
            BackgroundWorker Checker = new BackgroundWorker();
            Checker.DoWork += (sender, args) =>
            {
                try
                {
                    CheckNetGear = new WebClient().DownloadString("http://192.168.0.1/webpages/login.html?t=1577151147715");
                }
                catch (Exception e)
                {
                    CheckNetGear = "";
                }
            };
            Checker.RunWorkerAsync();
            while (Checker.IsBusy)
            {
                await Task.Delay(10);
            }
            Checker.Dispose();
            if(CheckNetGear.Contains("<title>Opening...</title>"))
            {
                Return = true;
            }
            else
            {
                Return = false;
            }
            return Return;
        }

        public async Task<string> CheckIP()
        {
            string Return = ""; 
            BackgroundWorker Checker = new BackgroundWorker();
            Checker.DoWork += (sender, args) =>
            {
                try
                {
                    Return = new WebClient().DownloadString("http://icanhazip.com");
                }
                catch
                {

                }
            };
            Checker.RunWorkerAsync();
            while (Checker.IsBusy)
            {
                await Task.Delay(10);
            }
            Checker.Dispose();
            return Return;
        }
    }
}

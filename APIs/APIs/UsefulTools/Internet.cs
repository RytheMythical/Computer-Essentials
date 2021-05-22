using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Internet
    { 
        public static async Task CheckInternet()
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

        public static void BitsTransferDownload(string DownloadLink, string DownloadName)
        {
            string[] GetFileLink = {  "@echo off",
                "cd C:\\Program Files\\Software Store Update Files",
                "set \"URL=" + DownloadLink + "\"",
                "set \"SaveAs=" + DownloadName + "\"",
                "powershell \"Import-Module BitsTransfer; Start-BitsTransfer '%URL%' '%SaveAs%'\"" };
            File.WriteAllLines("C:\\DownloadFile.bat", GetFileLink);
            var DownloadStart = Process.Start("C:\\DownloadFile.bat");
            DownloadStart.WaitForExit();
            File.Delete("C:\\DownloadFile.bat");
        }
    }
}

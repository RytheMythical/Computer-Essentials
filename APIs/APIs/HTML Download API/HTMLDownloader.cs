using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Threading.Tasks;

namespace HTML_Download_API
{
    public class HTMLDownloader
    {
        public string Link { get; set; }

        private string ResponseFile = Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponseFile.txt";

        private string RespondedFile = Environment.GetEnvironmentVariable("TEMP") + "\\GotHTML.txt";
        public async Task DownloadHTML(string link, string path)
        {
            File.WriteAllText(ResponseFile,link);

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/APIs/main/APIs/HTMLDownloadResponder/bin/Debug/HTMLDownloadResponder.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponder.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }

                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponder.exe")
                        .WaitForExit();
                });
            }

            if (File.Exists(path))
            {
                File.Delete(path);
            }
            File.Move(Environment.GetEnvironmentVariable("TEMP") + "\\GotHTML.txt",path);
        }

        public async Task<string> GetHTMLFromLink(string link)
        {
            File.WriteAllText(ResponseFile, link);

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/APIs/main/APIs/HTMLDownloadResponder/bin/Debug/HTMLDownloadResponder.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponder.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }

                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponder.exe")
                            .WaitForExit();
                    }
                    catch
                    {

                    }
                });
            }

            string Return = File.ReadAllText(RespondedFile);
            File.Delete(RespondedFile);
            return Return;
        }
    }
}

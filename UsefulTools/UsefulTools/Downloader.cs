using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Downloader
    {
        ///PARTS ARE NUMBER OF PARTS + 1///
        public static string User = System.Environment.GetEnvironmentVariable("USERPROFILE");

        public static async Task DownloadMulti(string Database, string Filename, string Parts, bool Decode, bool Decrypt, string Username)
        {
            string[] Stuff = { Database, Filename, Parts, Decode.ToString().ToLower(), Decrypt.ToString().ToLower(), Username };
            File.WriteAllLines(User + "\\AppData\\Local\\Temp\\SilentInstallD.txt", Stuff);
            Encode(User + "\\AppData\\Local\\Temp\\SilentInstallD.txt", User + "\\AppData\\Local\\Temp\\SilentInstall.txt");
            using (var client = new WebClient())
            {
                bool Started = false;
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/CNTowerGUN/JacksonDownloadManager/master/JacksonDownloadManager/bin/Debug/JacksonDownloadManager.exe"), User + "\\AppData\\Local\\Temp\\Download.exe");
                client.DownloadFileCompleted += async (sender, args) =>
                {
                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(User + "\\AppData\\Local\\Temp\\Download.exe").WaitForExit();
                    });
                    Started = true;
                };
                while (Started == false)
                {
                    await Task.Delay(10);
                }

                Started = false;
            }
        }

        public static void Encode(string Input, string Output)
        {
            RunCommand("certutil -encode \"" + Input + "\" \"" + Output + "\"");
        }
        public static void Decode(string Input, string Output)
        {
            RunCommand("certutil -decode \"" + Input + "\" \"" + Output + "\"");
        }

        public static void RunCommand(string Command)
        {
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat", CommandChut);
            var C = Process.Start(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
            C.WaitForExit();
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class FileDrop
    {
        public static async Task DownloadFileFromCode(string Code, string Password)
        {
            async Task ForceAdmin(string path)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Temp\\FileToRun.txt", path);
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GetAdminRights/master/GetAdminRights/bin/Debug/GetAdminRights.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe").WaitForExit();
                });

            }

            string ThePassword = "";
            if (Password == "")
            {
                ThePassword = "false";
            }
            else
            {
                ThePassword = Password;
            }
            string[] ThingsToWrite = { Code, ThePassword };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\DownloadFileDrop.txt", ThingsToWrite);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/File-Transfer/master/Main%20File%20Transfer%20App/Main%20File%20Transfer%20App/bin/Debug/Main%20File%20Transfer%20App.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            bool NeedAdmin = false;
            try
            {
                File.WriteAllText("C:\\TestAdmin.txt", "");
                File.Delete("C:\\TestAdmin.txt");
            }
            catch (Exception e)
            {
                NeedAdmin = true;
            }

            if (NeedAdmin == false)
            {
                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe").WaitForExit();
                });
            }
            else
            {
                await ForceAdmin(Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe");
                while (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\DoneDownloading.txt"))
                {
                    await Task.Delay(10);
                }
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\DoneDownloading.txt");
            }
        }
        public static async Task<string> UploadFileAndReturnCode(string Filepath, bool Encryption, string password)
        {
            async Task ForceAdmin(string path)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Temp\\FileToRun.txt", path);
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GetAdminRights/master/GetAdminRights/bin/Debug/GetAdminRights.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe").WaitForExit();
                });

            }
            string EncryptionF = "";
            string PasswordF = "";
            if (Encryption == true)
            {
                EncryptionF = "true";
                PasswordF = password;
            }
            else
            {
                EncryptionF = "false";
            }

            string[] ThingsToWrite = { Filepath, EncryptionF, PasswordF };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\UploadFileDrop.txt", ThingsToWrite);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/File-Transfer/master/Main%20File%20Transfer%20App/Main%20File%20Transfer%20App/bin/Debug/Main%20File%20Transfer%20App.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            bool NeedAdmin = false;
            try
            {
                File.WriteAllText("C:\\TestAdmin.txt", "");
                File.Delete("C:\\TestAdmin.txt");
            }
            catch (Exception e)
            {
                NeedAdmin = true;
            }

            if (NeedAdmin == false)
            {
                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe").WaitForExit();
                });
            }
            else
            {
                await ForceAdmin(Environment.GetEnvironmentVariable("TEMP") + "\\FileCodeGenerator.exe");
                while (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\ReturnFileCode.txt"))
                {
                    await Task.Delay(10);
                }
            }
            string Returner = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\ReturnFileCode.txt");
            File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\ReturnFileCode.txt");
            return Returner;
        }
    }
}

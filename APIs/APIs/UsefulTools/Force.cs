using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Force
    {
        public static async Task ForceAdmin(string path)
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

        public static async Task ForceDelete(bool SuperMode, string path)
        {
            if (SuperMode == true)
            {
                await Command.RunCommandHidden(
                    "\"C:\\Program Files (x86)\\IObit\\IObit Unlocker\\IObitUnlocker.exe\" /Delete /Advanced \"" +
                    path + "\"");
            }
            else
            {
                await Command.RunCommandHidden(
                    "\"C:\\Program Files (x86)\\IObit\\IObit Unlocker\\IObitUnlocker.exe\" /Delete /Normal \"" +
                    path + "\"");
            }
        }
    }
}

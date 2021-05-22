using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace GitHub_Large_Uploader_API
{
    public class GitHubUploader
    {
        public bool SmartMode { get; set; }
        public bool GenerateCodeForDownloader { get; set; }

        public string Username { get; set; }
        public string SourceDirectory { get; set; }
        public string GitHubDirectory { get; set; }
        public bool ShutdownWhenFinished { get; set; }
        public bool Base64AllFiles { get; set; }
        public bool EncryptAllFiles { get; set; }
        public string DecryptionKey { get; set; }

        public async Task SaveConfiguration(string Path)
        {
            string[] Stuff =
            {
                SourceDirectory, GitHubDirectory,
                SmartMode.ToString().ToLower(), ShutdownWhenFinished.ToString().ToLower(),
                Base64AllFiles.ToString().ToLower(), EncryptAllFiles.ToString().ToLower(),
                DecryptionKey,Username,GenerateCodeForDownloader.ToString().ToLower()
            };
            File.WriteAllLines(Path, Stuff);
        }

        public async Task LoadConfiguration(string Path, bool DeleteOriginal)
        {
            if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt"))
            {
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt");
            }
            File.Copy(Path, Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt");
            if (DeleteOriginal == true)
            {
                File.Delete(Path);
            }
        }

        public async Task StartUploader(bool ConfigurationLoaded)
        {
            if (ConfigurationLoaded == false)
            {
                string[] Stuff =
                {
                SourceDirectory.ToString().ToLower(), GitHubDirectory.ToString().ToLower(),
                SmartMode.ToString().ToLower(), ShutdownWhenFinished.ToString().ToLower(),
                Base64AllFiles.ToString().ToLower(), EncryptAllFiles.ToString().ToLower(),
                DecryptionKey,Username,GenerateCodeForDownloader.ToString().ToLower()
                };
                File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt", Stuff);
            }
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/GitHub-Large-Uploader/bin/Debug/GitHub-Large-Uploader.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderResponder.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            await Task.Factory.StartNew(() =>
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderResponder.exe").WaitForExit();
            });
        }
    }
}

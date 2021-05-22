using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Files
    {
        
        public static async Task Download(string link, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(link), filename);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
        }
        public static void Move(string source, string target)
        {
            if (!Directory.Exists(source))
            {
                throw new System.IO.DirectoryNotFoundException("Source directory couldn't be found.");
            }

            if (Directory.Exists(target))
            {
                throw new System.IO.IOException("Target directory already exists.");
            }

            DirectoryInfo sourceInfo = Directory.CreateDirectory(source);
            DirectoryInfo targetInfo = Directory.CreateDirectory(target);

            if (sourceInfo.FullName == targetInfo.FullName)
            {
                throw new System.IO.IOException("Source and target directories are the same.");
            }

            Stack<DirectoryInfo> sourceDirectories = new Stack<DirectoryInfo>();
            sourceDirectories.Push(sourceInfo);

            Stack<DirectoryInfo> targetDirectories = new Stack<DirectoryInfo>();
            targetDirectories.Push(targetInfo);

            while (sourceDirectories.Count > 0)
            {
                DirectoryInfo sourceDirectory = sourceDirectories.Pop();
                DirectoryInfo targetDirectory = targetDirectories.Pop();

                foreach (FileInfo file in sourceDirectory.GetFiles())
                {
                    file.CopyTo(Path.Combine(targetDirectory.FullName, file.Name), overwrite: true);
                }

                foreach (DirectoryInfo subDirectory in sourceDirectory.GetDirectories())
                {
                    sourceDirectories.Push(subDirectory);
                    targetDirectories.Push(targetDirectory.CreateSubdirectory(subDirectory.Name));
                }
            }

            sourceInfo.Delete(true);
        }
        public static async Task StartHiddenProcess(string path)
        {
            bool Exit = false;
            Process dew = new Process();
            dew.StartInfo.FileName = path;
            dew.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            dew.EnableRaisingEvents = true;
            dew.Exited += (sender, args) =>
            {
                Exit = true;
            };
            dew.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }
            Exit = false;
        }
        public static void DeleteDirectory(string path)
        {
            foreach (string directory in Directory.GetDirectories(path))
            {
                DeleteDirectory(directory);
            }

            try
            {
                Directory.Delete(path, true);
            }
            catch (IOException)
            {
                Directory.Delete(path, true);
            }
            catch (UnauthorizedAccessException)
            {
                Directory.Delete(path, true);
            }
        }
        public static async Task InstallWindowsISO(string Database, string SaveLocation)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\APIWindows.txt", Database + "\n" + SaveLocation);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/MediaCreationTool/master/MediaCreationTool/bin/Debug/MediaCreationTool.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\WindowsMediaCreationTool.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            await Task.Factory.StartNew(() =>
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\WindowsMediaCreationTool.exe")
                    .WaitForExit();
            });
        }
    }
}

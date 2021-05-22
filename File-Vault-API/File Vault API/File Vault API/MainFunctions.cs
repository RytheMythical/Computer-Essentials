using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace File_Vault_API
{
    public class MainFunctions
    {
        private string WinRARPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WinRARInstallation";

        public string DefaultVeracryptEXEPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VeraCryptInstallation\\VeraCrypt.exe";
        public async Task MountVeracrypt(string Path, string Password, string PIM, string Letter)
        {
            Console.WriteLine("Running: " + "\"" + DefaultVeracryptEXEPath + "\" /q /v \"" + Path + "\" /l " + Letter + " /a /p " + Password + " /pim " + PIM + "");
            await RunCommandHidden("\"" + DefaultVeracryptEXEPath + "\" /q /v \"" + Path + "\" /l " + Letter + " /a /p " + Password + " /pim " + PIM + "");
        }
        public async Task DismountVeracrypt(string Letter)
        {
            Console.WriteLine("Running: " + "\"" + DefaultVeracryptEXEPath + "\" /q /d " + Letter + " /s");
            await RunCommandHidden("\"" + DefaultVeracryptEXEPath + "\" /q /d " + Letter + " /s");
        }

        public async Task CreateVeracryptVolume(string Path, string Password, string Size, string PIM)
        {
            Console.WriteLine("Running: " + "\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VeraCryptInstallation\\VeraCrypt Format.exe\" /create \"" + Path + "\" /password " + Password + " /pim " + PIM + " /hash sha512 /encryption serpent /filesystem FAT /size " + Size + "M /force /silent /quick");
            await RunCommandHidden("\"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VeraCryptInstallation\\VeraCrypt Format.exe\" /create \"" + Path + "\" /password " + Password + " /pim " + PIM + " /hash sha512 /encryption serpent /filesystem FAT /size " + Size + "M /force /silent /quick");
        }

        public char[] getAvailableDriveLetters()
        {
            List<char> availableDriveLetters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                availableDriveLetters.Remove((drives[i].Name).ToLower()[0]);
            }

            return availableDriveLetters.ToArray();
        }

        public string GetRandomDriveLetter1()
        {
            string Return = "";
            char[] AvailableDriveLetter = getAvailableDriveLetters();
            foreach (char c in AvailableDriveLetter)
            {
                Console.WriteLine(c);
                Return = c.ToString();
            }

            return Return;
        }

        public string GetRandomDriveLetter2()
        {
            var i = 0;
            string Return = "";
            char[] AvailableDriveLetter = getAvailableDriveLetters();
            foreach (char c in AvailableDriveLetter)
            {
                if (i == 2)
                {
                    Return = c.ToString();
                }
                i++;
            }

            return Return;
        }

        public string GetRandomDriveLetter3()
        {
            string Return = "";
            var i = 0;
            char[] AvailableDriveLetter = getAvailableDriveLetters();
            foreach (char c in AvailableDriveLetter)
            {
                if (i == 3)
                {
                    Return = c.ToString();
                }

                i++;
            }

            return Return;
        }

        private bool Exit = false;
        public async Task RunCommandHidden(string Command)
        {
            string hui = Path.GetRandomFileName().Replace(".", "");
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        public string UniqueHashing(string inputstring)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Unique-Hasher/master/Unique-Hasher/bin/Debug/Unique-Hasher.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe");
                while (client.IsBusy)
                {
                    Task.Delay(10);
                }
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt", inputstring);

            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe").WaitForExit();

            return File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt").ElementAtOrDefault(0);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt");
        }

        public void Rar(string Archive, string Files, string Password)
        {
            string[] Run = { "cd \"" + WinRARPath + "\"", "rar a -p" + Password + " \"" + Archive + "\"" + " " + "\"" + Files + "\"" };
            File.WriteAllLines("C:\\Unrar.bat", Run);
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Unrar.bat";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
        }

        public void DeleteDirectory(string path)
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
        public void Unrar(string Archive, string Output,string Password)
        {
            string[] Run = { "cd \"" + WinRARPath + "\"", "unrar x -p" + Password + " \"" + Archive + "\"" + " " + "\"" + Output + "\"" };
            File.WriteAllLines("C:\\Unrar.bat", Run);
            Process p = new Process();
            p.StartInfo.FileName = "C:\\Unrar.bat";
            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            p.Start();
            p.WaitForExit();
        }
        public async Task CreateVHD(string size, string path, string letter)
        {
            string[] Script =
            {
                "create vdisk file=\"" + path + "\" maximum=" + size +
                " type=expandable",
                "select vdisk file=\"" + path + "\"", "attach vdisk", "detail vdisk",
                "convert mbr", "create partition primary", "format fs=fat label=\"install\" quick",
                "assign letter=" + letter
            };
            File.WriteAllLines("C:" + "\\DiskPartScript.txt", Script);
            await RunCommandHidden("cd \"" + "C:\\" + "\"" + "\ndiskpart /s \"C:\\DiskPartScript.txt\"");
        }

        public async Task DismountVHD(string path)
        {

            string[] UnMountVdisk = { "select vdisk file=" + path, "detach vdisk" };
            File.WriteAllLines("C:" + "\\UnMountScript.txt", UnMountVdisk);
            string[] RunScript = { "diskpart /s \"" + "C:" + "\\UnMountScript.txt\"" };
            File.WriteAllLines("C:" + "\\Unmount.bat", RunScript);
            await RunCommandHidden("diskpart /s \"" + "C:" + "\\UnMountScript.txt\"");
        }

        public async Task MountVHD(string path, string letter)
        {
            string[] MountDiskScript =
            {
                "select vdisk file=" + path, "attach vdisk", "select partition 1",
                "assign letter=" + letter
            };
            File.WriteAllLines("C:\\MountDiskScript.txt", MountDiskScript);
            await RunCommandHidden("diskpart /s \"" + "C:" + "\\MountDiskScript.txt\"");
        }
    }
}

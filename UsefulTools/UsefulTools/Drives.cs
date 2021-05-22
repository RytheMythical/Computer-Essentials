using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Drives
    {
        public static char[] getAvailableDriveLetters()
        {
            List<char> availableDriveLetters = new List<char>() { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

            DriveInfo[] drives = DriveInfo.GetDrives();

            for (int i = 0; i < drives.Length; i++)
            {
                availableDriveLetters.Remove((drives[i].Name).ToLower()[0]);
            }

            return availableDriveLetters.ToArray();
        }

        public static string GetRandomDriveLetter1()
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

        public static string GetRandomDriveLetter2()
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

        public static string GetRandomDriveLetter3()
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

        public static async Task CreateVHD(string size, string path, string letter)
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
            await Command.RunCommandHidden("cd \"" + "C:\\" + "\"" + "\ndiskpart /s \"C:\\DiskPartScript.txt\"");
        }

        public static async Task DismountVHD(string path)
        {

            string[] UnMountVdisk = { "select vdisk file=" + path, "detach vdisk" };
            File.WriteAllLines("C:" + "\\UnMountScript.txt", UnMountVdisk);
            string[] RunScript = { "diskpart /s \"" + "C:" + "\\UnMountScript.txt\"" };
            File.WriteAllLines("C:" + "\\Unmount.bat", RunScript);
            await Task.Factory.StartNew(() => { Process.Start("C:" + "\\Unmount.bat").WaitForExit(); });
        }

        public static async Task MountVHD(string path, string letter)
        {
            string[] MountDiskScript =
            {
                "select vdisk file=" + path, "attach vdisk", "select partition 1",
                "assign letter=" + letter
            };
            File.WriteAllLines("C:\\MountDiskScript.txt", MountDiskScript);
            await Command.RunCommandHidden("diskpart /s \"" + "C:" + "\\MountDiskScript.txt\"");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UsefulTools;

namespace WinRAR_API
{
    public static class RARFunctions
    {
        static RARFunctions()
        {

        }
        private static async Task Download(string Link, string Path)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(Link),Path);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
        }

        private static async Task<string> DownloadString(string Link)
        {
            string Return = "";
            using (var client = new WebClient())
            {
                Return = client.DownloadString(Link);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            return Return;
        }

        public enum SizeFormat
        {
            MegaBytes,
            GigaBytes,
            Bytes,
            KiloBytes
        }

        public static async Task CreateFromVeraCrypt(string path, int splitsize, bool DeleteOriginal,string ExtractPath, SizeFormat sizeformat)
        {
            Directory.CreateDirectory("C:\\RARDirectory");
            string[] Stuff =
            {
                ";The comment below contains SFX script commands", "", "Path=" + ExtractPath, "Silent=2", "Overwrite=1"
            };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt",Stuff);
            string Comment = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt");
            string OriginalPath = Path.GetDirectoryName(path);
            if (File.Exists("C:\\" + Path.GetFileName(path)))
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            File.Move(path, "C:\\" + Path.GetFileName(path));
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt", Comment);

            string SizeFormatString()
            {
                string Return = "";
                if (sizeformat == SizeFormat.Bytes)
                {
                    Return = "b";
                }
                else if (sizeformat == SizeFormat.MegaBytes)
                {
                    Return = "m";
                }
                else if (sizeformat == SizeFormat.GigaBytes)
                {
                    Return = "g";
                }
                else if (sizeformat == SizeFormat.KiloBytes)
                {
                    Return = "k";
                }

                return Return;
            }
            await Command.RunCommandHidden("cd \"C:\\Program Files\\WinRAR\"\nrar a -m1 -v" + splitsize.ToString() + SizeFormatString() + " -sfx -z\"" + Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt" + "\" \"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "\" \"C:\\" + Path.GetFileName(path) + "\"");
            if (DeleteOriginal == true)
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            else
            {
                File.Move("C:\\" + Path.GetFileName(path), path);
            }
        }

        public static async Task CreateFromVeraCrypt(string path, int splitsize, bool DeleteOriginal, string ExtractPath,int CompressionLevel, SizeFormat sizeformat)
        {
            Directory.CreateDirectory("C:\\RARDirectory");
            string[] Stuff =
            {
                ";The comment below contains SFX script commands", "", "Path=" + ExtractPath, "Silent=2", "Overwrite=1"
            };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt", Stuff);
            string Comment = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt");
            string OriginalPath = Path.GetDirectoryName(path);
            if (File.Exists("C:\\" + Path.GetFileName(path)))
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            File.Move(path, "C:\\" + Path.GetFileName(path));
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt", Comment);

            string SizeFormatString()
            {
                string Return = "";
                if (sizeformat == SizeFormat.Bytes)
                {
                    Return = "b";
                }
                else if (sizeformat == SizeFormat.MegaBytes)
                {
                    Return = "m";
                }
                else if (sizeformat == SizeFormat.GigaBytes)
                {
                    Return = "g";
                }
                else if (sizeformat == SizeFormat.KiloBytes)
                {
                    Return = "k";
                }

                return Return;
            }
            await Command.RunCommandHidden("cd \"C:\\Program Files\\WinRAR\"\nrar a -m" + CompressionLevel.ToString() + " -v" + splitsize.ToString() + SizeFormatString() + " -sfx -z\"" + Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt" + "\" \"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "\" \"C:\\" + Path.GetFileName(path) + "\"");
            if (DeleteOriginal == true)
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            else
            {
                File.Move("C:\\" + Path.GetFileName(path), path);
            }
        }
        public static async Task CreateFromVeraCrypt(string path,int splitsize,bool DeleteOriginal,SizeFormat sizeformat)
        {
            Directory.CreateDirectory("C:\\RARDirectory");
            string Comment = await DownloadString("https://raw.githubusercontent.com/EpicGamesGun/APIs/main/Resources/WinRAR%20Comments/AddLargeVeraCryptFile.txt");
            string OriginalPath = Path.GetDirectoryName(path);
            if (File.Exists("C:\\" + Path.GetFileName(path)))
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            File.Move(path, "C:\\" + Path.GetFileName(path));
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt",Comment);

            string SizeFormatString()
            {
                string Return = "";
                if (sizeformat == SizeFormat.Bytes)
                {
                    Return = "b";
                }
                else if (sizeformat == SizeFormat.MegaBytes)
                {
                    Return = "m";
                }
                else if (sizeformat == SizeFormat.GigaBytes)
                {
                    Return = "g";
                }
                else if (sizeformat == SizeFormat.KiloBytes)
                {
                    Return = "k";
                }

                return Return;
            }
            await Command.RunCommandHidden("cd \"C:\\Program Files\\WinRAR\"\nrar a -m1 -v" + splitsize.ToString() + SizeFormatString() + " -sfx -z\"" + Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt" + "\" \"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "\" \"C:\\" + Path.GetFileName(path) + "\"");
            if (DeleteOriginal == true)
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            else
            {
                File.Move("C:\\" + Path.GetFileName(path),path);
            }
        }

        public static async Task CreateFromVeraCrypt(string path, int splitsize, bool DeleteOriginal,int CompressionLevel, SizeFormat sizeformat)
        {
            Directory.CreateDirectory("C:\\RARDirectory");
            string Comment = await DownloadString("https://raw.githubusercontent.com/EpicGamesGun/APIs/main/Resources/WinRAR%20Comments/AddLargeVeraCryptFile.txt");
            string OriginalPath = Path.GetDirectoryName(path);
            if (File.Exists("C:\\" + Path.GetFileName(path)))
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            File.Move(path, "C:\\" + Path.GetFileName(path));
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt", Comment);

            string SizeFormatString()
            {
                string Return = "";
                if (sizeformat == SizeFormat.Bytes)
                {
                    Return = "b";
                }
                else if (sizeformat == SizeFormat.MegaBytes)
                {
                    Return = "m";
                }
                else if (sizeformat == SizeFormat.GigaBytes)
                {
                    Return = "g";
                }
                else if (sizeformat == SizeFormat.KiloBytes)
                {
                    Return = "k";
                }

                return Return;
            }
            await Command.RunCommandHidden("cd \"C:\\Program Files\\WinRAR\"\nrar a -m" + CompressionLevel.ToString() + " -v" + splitsize.ToString() + SizeFormatString() + " -sfx -z\"" + Environment.GetEnvironmentVariable("TEMP") + "\\Comment.txt" + "\" \"" + Path.GetDirectoryName(path) + "\\" + Path.GetFileNameWithoutExtension(path) + "\" \"C:\\" + Path.GetFileName(path) + "\"");
            if (DeleteOriginal == true)
            {
                File.Delete("C:\\" + Path.GetFileName(path));
            }
            else
            {
                File.Move("C:\\" + Path.GetFileName(path), path);
            }
        }
    }

    public class RAR
    {
        public RAR()
        {

        }

        public static string ArchiveSavePath { get; set; }
        public static string FilesToAdd { get; set; }

        public static string Switches
        {
            get;
            set;
        }

        List<bool> CommandSwitches = new List<bool>();
        public static async Task Start()
        {
            await Command.RunCommandHidden("cd \"C:\\Program Files\\WinRAR\" rar a " + Switches + " \"" +
                                           ArchiveSavePath + "\" \"" + FilesToAdd + "\"");
        }
    }
}

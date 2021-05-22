using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Chocolatey
    {
        public static async Task ChocolateyDownload(string software)
        {
            await Command.RunCommandHidden("cd \"C:\\ProgramData\\chocolatey\"\nchoco.exe install " + software + " -y");
        }

        public static async Task InstallChocolatey()
        {
            if (!Directory.Exists("C:\\ProgramData\\chocolatey"))
            {
                await Command.RunCommandHidden(
                    "@\"%SystemRoot%\\System32\\WindowsPowerShell\\v1.0\\powershell.exe\" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command \" [System.Net.ServicePointManager]::SecurityProtocol = 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))\" && SET \"PATH=%PATH%;%ALLUSERSPROFILE%\\chocolatey\\bin\"");
            }
        }
    }
}

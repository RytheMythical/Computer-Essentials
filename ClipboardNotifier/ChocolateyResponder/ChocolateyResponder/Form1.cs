using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChocolateyResponder
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            string Clipboard = System.Windows.Forms.Clipboard.GetText();
            Clipboard = Clipboard.Replace("choco install", "");
            await InstallChocolatey();
            await ChocolateyDownload(Clipboard);
            Application.Exit();
        }

        public async Task<string> ChocoInstall(string software)
        {
            string Return = "";
            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\ChocoResponder.exe"))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Chocolatey-API/master/Chocolatey-API%20Responder/Chocolatey-API%20Responder/bin/Debug/Chocolatey-API%20Responder.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\ChocoResponder.exe");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }

            string OldClipboard = Clipboard.GetText();
            Clipboard.SetText("choco install " + software);
            Process p = new Process();
            p.StartInfo.FileName = Environment.GetEnvironmentVariable("TEMP") + "\\ChocoResponder.exe";
            p.EnableRaisingEvents = true;
            bool ExitedSoftware = false;
            p.Exited += (sender, args) =>
            {
                ExitedSoftware = true;
            };
            p.Start();
            while (!Convert.ToBoolean(Clipboard.GetText() == "choco RecievedRequest"))
            {
                await Task.Delay(10);
            }

            Clipboard.SetText(OldClipboard);
            while (ExitedSoftware == false)
            {
                await Task.Delay(10);
            }

            if (Clipboard.GetText() == "AlreadyInstalled")
            {
                Return = "AlreadyInstalled";
            }
            else if (Clipboard.GetText() == "Failed")
            {
                Return = "Failed";
            }
            else if (Clipboard.GetText() == "Success")
            {
                Return = "Success";
            }
            else
            {
                Return = "Failed";
            }

            return Return;
        }
        private async Task ChocolateyDownload(string software)
        {
            string OldClipBoard = Clipboard.GetText();
            string Installation = await ChocoInstall(software);
        }

        private async Task InstallChocolatey()
        {
            if (!Directory.Exists("C:\\ProgramData\\chocolatey"))
            {
                await RunCommandHidden(
                    "@\"%SystemRoot%\\System32\\WindowsPowerShell\\v1.0\\powershell.exe\" -NoProfile -InputFormat None -ExecutionPolicy Bypass -Command \" [System.Net.ServicePointManager]::SecurityProtocol = 3072; iex ((New-Object System.Net.WebClient).DownloadString('https://chocolatey.org/install.ps1'))\" && SET \"PATH=%PATH%;%ALLUSERSPROFILE%\\chocolatey\\bin\"");
            }
        }

        private bool Exit = false;
        public async Task RunCommandHidden(string Command)
        {
            Random dew = new Random();
            int hui = dew.Next(0000, 9999);
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }
    }
}

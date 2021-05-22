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

namespace Unique_Hasher
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string MainFile = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                  "\\Hashing.txt";
        private async void Form1_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Visible = false;
            try
            {
                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt"))
                {
                    string ToHash = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt").ElementAtOrDefault(0);

                    ToHash = GetHashString(ToHash);
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashingPrepare.txt", ToHash);
                    await RunCommandHidden("certutil -encode \"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashingPrepare.txt" + "\" \"" + Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedFirst.txt\"");
                    string SecondToHash = File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedFirst.txt").ElementAtOrDefault(1);
                    SecondToHash = GetHashString(SecondToHash);
                    SecondToHash = GetHashString256(SecondToHash);
                    File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt", SecondToHash);
                    ///DELETE LEFTOVERS///
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt");
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashingPrepare.txt");
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedFirst.txt");
                    ///END OF LEFTOVERS///
                }
                else
                {
                    Application.Exit();
                }
            }
            catch (Exception)
            {

            }
            Application.Exit();
        }

        private bool Exit = false;
        public async Task RunCommandHidden(string Command)
        {
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA512.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static byte[] GetHash256(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString256(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }
}

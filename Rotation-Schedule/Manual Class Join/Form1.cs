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

namespace Manual_Class_Join
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void JoinButton_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\" + ClassList.SelectedItem.ToString().Replace(" ","")))
                {
                    string Link = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\" + ClassList.SelectedItem.ToString().Replace(" ",""));
                    File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt", Link);
                    Visible = false;
                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(PathChut).WaitForExit();
                    });
                    File.Delete(PathChut);
                    Visible = true;
                    await Task.Delay(2000);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                JoinButton.Text = "Not Ready!";
                await Task.Delay(3000);
                JoinButton.Text = "Join Class";
                Console.WriteLine(ex);
            }
        }
        string PathChut = "";
        private async void Form1_Load(object sender, EventArgs e)
        {
            CustomJoin.Enabled = false;
            JoinButton.Enabled = false;
            PathChut = Environment.GetEnvironmentVariable("TEMP") + "\\MEETLOADER" + new Random().Next(1111, 9999).ToString() + ".exe";
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (o, args) =>
                {
                    progressBar1.Value = args.ProgressPercentage;
                };
                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Meets%20Loader/bin/Debug/Meets%20Loader.exe"), PathChut);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            DirectoryInfo d = new DirectoryInfo(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations");
            var i = 1;
            foreach (var fileInfo in d.GetFiles())
            {
                if (fileInfo.Name == "Period" + i)
                {
                    ClassList.Items.Add("Period " + i);
                    i++;
                }
            }
            JoinButton.Enabled = true;
            ClassList.Enabled = true;
            CustomJoin.Enabled = true;
        }

        private async void CustomJoin_Click(object sender, EventArgs e)
        {
            try
            {
                if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\" + ClassList.SelectedItem.ToString().Replace(" ", "")))
                {
                    string Link = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\" + ClassList.SelectedItem.ToString().Replace(" ", ""));
                    File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt", CustomMeetLinkBox.Text);
                    Visible = false;
                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(PathChut).WaitForExit();
                    });
                    File.Delete(PathChut);
                    Visible = true;
                    await Task.Delay(2000);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                JoinButton.Text = "Not Ready!";
                await Task.Delay(3000);
                JoinButton.Text = "Join Class";
                Console.WriteLine(ex);
            }
        }
    }
}

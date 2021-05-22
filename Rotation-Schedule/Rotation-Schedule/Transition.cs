using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rotation_Schedule.Properties;

namespace Rotation_Schedule
{
    public partial class TransitionFrom : Form
    {
        public TransitionFrom()
        {
            FormBorderStyle = FormBorderStyle.None;
            InitializeComponent();
            Closing += Transition_Closing;
            Load += TransitionFrom_Load;
            MouseEnter += (sender, args) =>
            {
                TopMost = false;
            };
        }
        bool Flasing = false;

        private async Task Flash()
        {
            while (true)
            {
                Random r = new Random();
                await Task.Delay(10);
                if (Flasing == true)
                {
                    BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(50);
                    label1.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(50);
                    label1.ForeColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(50);
                    TransitionTo.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(50);
                    TransitionTo.ForeColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(50);
                    TransitioningLabel.BackColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(100);
                    TransitioningLabel.ForeColor = Color.FromArgb(r.Next(0, 256), r.Next(0, 256), r.Next(0, 256));
                    await Task.Delay(100);
                }
            }
        }
        private async void TransitionFrom_Load(object sender, EventArgs e)
        {
            while (true)
            {
                pictureBox1.Visible = false;
                await Task.Delay(400);
                pictureBox1.Visible = true;
                await Task.Delay(400);
            }
        }

        bool CloseThing = true;
        private void Transition_Closing(object sender, CancelEventArgs e)
        {
            if (CloseThing == false)
            {
                e.Cancel = true;
            }
        }


        private async void Transition_Load(object sender, EventArgs e)
        {
            Flash();
            bool Christmas = Form1.ChristmasMusic;
            TopMost = true;
            CloseThing = false;
            TransitioningLabel.Text = Form1.TransitionFrom;
            TransitionTo.Text = Form1.TransitionTo;
            if (!File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Christmas.wav"))
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (o, args) =>
                    {
                        progressBar1.Value = args.ProgressPercentage;
                    };
                    client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Christmas.wav?inline=false"), Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Christmas.wav");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }

            if (!File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\FullTrack.wav"))
            {
                using (var client = new WebClient())
                {
                    client.DownloadProgressChanged += (o, args) =>
                    {
                        progressBar1.Value = args.ProgressPercentage;
                    };
                    client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/FullWarheadSoundTrack.wav?inline=false"), Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\FullTrack.wav");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
            progressBar1.Value = 0;
            BackgroundWorker MusicPlayer = new BackgroundWorker();
            bool Release = false;
            MusicPlayer.DoWork += (o, args) =>
            {
                if (!Christmas)
                {
                    new SoundPlayer(Resources.TenMinuteAlarm).PlaySync();
                }
                else
                {
                    new SoundPlayer(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\FullTrack.wav").PlaySync();
                }
            };
            MusicPlayer.RunWorkerAsync();
            int MusicTime = Christmas ? 180 + 47 : 180 + 30;
            progressBar1.Maximum = MusicTime;
            for (int i = 0; i < MusicTime; i++)
            {
                progressBar1.Value++;
                await Task.Delay(1000);
                if (i >= 0 && i <= 50)
                {
                    BackColor = Color.Green;
                }
                else if (i >= 50 && i <= 100)
                {
                    BackColor = Color.Yellow;
                }
                else if (i >= 100 && i <= 150)
                {
                    BackColor = Color.Red;
                }
                else if (i >= 150 && i <= 180)
                {
                    BackColor = Color.Purple;
                }
                else if (i >= 180)
                {
                    Flasing = true;
                }
            }
            CloseThing = true;
            label1.Text = "Good luck!";
            await Task.Delay(3000);
            Close();
        }
    }
}

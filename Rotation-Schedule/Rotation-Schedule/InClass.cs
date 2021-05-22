using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;
using Rotation_Schedule.Properties;

namespace Rotation_Schedule
{
    public partial class InClass : Form
    {
        public InClass()
        {
            InitializeComponent();
            //Closing += InClass_Closing;
            Load += InClass_Load1;
        }

        bool Flashing = false;

        private List<DateTime> StoredDateTimesList = new List<DateTime>();

        private DateTime[] StoredDateTime
        {
            get
            {
                return StoredDateTimesList.OfType<DateTime>().ToArray();
            }
        }

        private bool CheckDateTime(DateTime Date)
        {
            return StoredDateTime.Contains(Date);
        }
        private static void ConvertMp3ToWav(string _inPath_, string _outPath_)
        {
            using (Mp3FileReader mp3 = new Mp3FileReader(_inPath_))
            {
                using (WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(mp3))
                {
                    WaveFileWriter.CreateWaveFile(_outPath_, pcm);
                }
            }
        }
        private async Task Flash()
        {
            while (true)
            {
                if (Flashing == true)
                {
                    BackColor = Color.Green; 
                    await Task.Delay(500);
                    BackColor = Color.Yellow;
                    await Task.Delay(500);
                    BackColor = Color.Red;
                    await Task.Delay(500);
                }
                await Task.Delay(10);
            }
        }
        private async void InClass_Load1(object sender, EventArgs e)
        {
            
        }

        bool CloseH = false;
        private async void InClass_Closing(object sender, CancelEventArgs e)
        {
            if (!CloseH) e.Cancel = true;
        }

        public string Period = Form1.CurrentPeriod;
        //public TimeSpan PeriodLength = Form1.PeriodTime;
        int MinutesCountdown = 0;
        public async Task PlayMP3File(byte[] PathHui)
        {
            try
            {
                string Temp1 = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".mp3";
                string Temp2 = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".wav";
                File.WriteAllBytes(Temp1,PathHui);
                ConvertMp3ToWav(Temp1,Temp2);
                await new Form1().PlaySoundSync(Temp2, true);
                File.Delete(Temp1);
                File.Delete(Temp2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        bool _1 = false;
        bool _2 = false;
        bool _3 = false;
        bool _4 = false;
        bool _5 = false;
        bool _6 = false;
        bool _7 = false;
        bool _8 = false;
        bool _9 = false;
        bool _10 = false;
        bool _11 = false;
        bool _12 = false;

        bool FaceToFace = Form1.FaceToFace;
        private async void InClass_Load(object sender, EventArgs e)
        {
            Flash();
            BackgroundWorker Counter = new BackgroundWorker();
            Counter.DoWork += async (o, args) =>
            {
                while (true)
                {
                    await Task.Delay(TimeSpan.FromMinutes(1));
                    MinutesCountdown--;
                }
            };
            Counter.RunWorkerAsync();
            TimeSpan PeriodTime = TimeSpan.FromMinutes(Int32.Parse(Form1.PeriodTime));
            Console.WriteLine(PeriodTime.ToString("g"));
            double Seconds = PeriodTime.TotalSeconds;
            progressBar1.Maximum = (int)Seconds;
            Console.WriteLine("SECONDS: " + Seconds);
            MinutesCountdown = Int32.Parse(Form1.PeriodTime);
            TitleLabel.Text = "Period: " + Period + "\n" + Form1.PeriodName;
            FormBorderStyle = FormBorderStyle.None;
            await PlayMP3File(Resources.WelcomeToClass);
            for (int i = 0; i < Seconds; i++)
            {
                try
                {
                    progressBar1.Value++;
                    await Task.Delay(1000);
                    RemainingTimeLabel.Text = "Remaining Time: " + MinutesCountdown + " minutes";
                    if (MinutesCountdown == 75 && !_9)
                    {
                        _9 = true;
                        await PlayMP3File(Resources._75minutesleft);
                    }
                    if (MinutesCountdown == 70 && !_10)
                    {
                        _10 = true;
                        await PlayMP3File(Resources._70MinutesLeft);
                    }
                    if(MinutesCountdown == 60 && !_12)
                    {
                        _12 = true;
                        await PlayMP3File(Resources._1HourLeft);
                    }
                    if (MinutesCountdown == 50 && !_11)
                    {
                        _11 = true;
                        await PlayMP3File(Resources._50MinutesLeft);
                    }
                    if (MinutesCountdown == 40 && _1 == false)
                    {
                        _1 = true;
                        await PlayMP3File(Resources._40MinutesLeft);
                    }

                    if (MinutesCountdown == 30 && _7 == false)
                    {
                        _7 = true;
                        await PlayMP3File(Resources._30MinutesLeft);
                    }
                    if (MinutesCountdown >= 30 && !_2)
                    {
                        BackColor = Color.Green;
                    }
                    else if (MinutesCountdown <= 30 && MinutesCountdown >= 10 && !_3)
                    {
                        _3 = true;
                        BackColor = Color.Yellow;
                    }
                    else if (MinutesCountdown == 20 && !_6)
                    {
                        _6 = true;
                        await PlayMP3File(Resources._20MinutesLeft);
                    }
                    else if (MinutesCountdown == 15 && !_8 && !FaceToFace)
                    {
                        _8 = true;
                        await PlayMP3File(Resources.AsyncBegins);
                        await new Form1().PlaySoundSync(Resources.SecondAlarm, true);
                    }
                    else if (MinutesCountdown <= 10 && MinutesCountdown >= 5 && !_4)
                    {
                        _4 = true;
                        BackColor = Color.DeepPink;
                        await PlayMP3File(Resources._10MinutesLeft);
                    }
                    else if (MinutesCountdown <= 5 && !_5)
                    {
                        _5 = true;
                        await PlayMP3File(Resources._5MinutesLeft);
                        Flashing = true;
                    }

                    else if (MinutesCountdown == 0)
                    {
                        break;
                    }
                }
                catch (Exception)
                {
                    break;
                }
            }
            RemainingTimeLabel.Text = "Class Ended.";
            await PlayMP3File(Resources.ClassEnded);
            CloseH = true;
            Close();
        }
    }
}

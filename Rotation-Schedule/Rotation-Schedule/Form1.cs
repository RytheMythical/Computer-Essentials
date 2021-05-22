using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AudioSwitcher.AudioApi.CoreAudio;
using AudioSwitcher.AudioApi.Sandbox;
using GitHub_Database_Downloader_API;
using NAudio.Wave;
using Rotation_Schedule.Properties;
using UsefulTools;
using WPFFolderBrowser;

namespace Rotation_Schedule
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Closing += Form1_Closing;
        }

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
        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }
        string GrabLink = Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt";
        string GoogleMeetClient = Environment.GetEnvironmentVariable("TEMP") + "\\MeetClient" + Path.GetRandomFileName() + ".exe";

        public static bool FaceToFace
        {
            get
            {
                return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\FaceToFace.txt");
            }
        }

        public async Task JoinGoogleMeet(string Link)
        {
            try
            {
                File.WriteAllText(GrabLink, Link);
                Process p = new Process();
                p.StartInfo.FileName = GoogleMeetClient;
                p.EnableRaisingEvents = true;
                string OldMeetClient = GoogleMeetClient;
                p.Exited += async (sender, args) =>
                {
                    try
                    {
                        await PlaySoundSync(Resources.DingDong, true);
                        File.Delete(OldMeetClient);
                    }
                    catch
                    {

                    }
                };
                p.Start();
                GoogleMeetClient = Environment.GetEnvironmentVariable("TEMP") + "\\CLIENT" + new Random().Next(1111, 9999).ToString() + ".exe";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Meets%20Loader/bin/Debug/Meets%20Loader.exe"), GoogleMeetClient);
                }
                //await Task.Delay(3000);
                //Process.Start(Link);
                //string MacroDirectory = Path.GetTempPath() + "\\MacroRunner.exe";
                //string MacroInstructions = Path.GetTempPath() + "\\MacroInstructions.mmmacro";
                //File.WriteAllBytes(MacroDirectory, Resources.MiniMouseMacro);
                //File.WriteAllBytes(MacroInstructions, Resources.Auto_join_class);
                //await Task.Factory.StartNew(() =>
                //{
                //    Process.Start(MacroDirectory, MacroInstructions);
                //});
            }
            catch (Exception)
            {
                Process.Start(Link);
            }
        }

        public async Task JoinZoomMeeting(string Link)
        {
            try
            {
                Process.Start(Link);
            }
            catch
            {

            }
        }

        private bool SelectedPeriods = false;

        private async Task ChangeTransition(string one, string two, bool Christmas)
        {
            TransitionFrom = one;
            TransitionTo = two;
            ChristmasMusic = Christmas;
            await MaxVolume();
            new TransitionFrom().ShowDialog();
            await NormalVolume();
        }

        public static string TransitionFrom = "";
        public static string TransitionTo = "";
        public static string PeriodTime = "";
        public static bool ChristmasMusic = false;

        public string GetAnnouncement(string Period)
        {
            string Return = "";
            if (Period == "1")
            {
                Return = PeriodNames.PeriodAnnouncements[0];
            }
            else if (Period == "2")
            {
                Return = PeriodNames.PeriodAnnouncements[1];
            }
            else if (Period == "3")
            {
                Return = PeriodNames.PeriodAnnouncements[2];
            }
            else if (Period == "4")
            {
                Return = PeriodNames.PeriodAnnouncements[3];
            }
            return Return;
        }

        public static string CreditRecoveryMeetLink = "";

        private async void Form1_Load(object sender, EventArgs e)
        {
            Console.WriteLine("Started");
            Visible = true;
            TopMost = false;
            //await ChangeTransition("Lunch/Transportation", "2");
            foreach (string s in RotationChecks.GetRotation())
            {
                Console.WriteLine(s);
            }

            // Early Initialization Variables //
            TopMost = false;
            bool[] ShownList = new bool[1000];
            FormBorderStyle = FormBorderStyle.None;
            ShowInTaskbar = false;
            button1.Visible = true;
            string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SecondSemesterFix"))
            {
                DirectoryInfo Old = new DirectoryInfo(PeriodPath);
                foreach (FileInfo fileInfo in Old.GetFiles())
                {
                    fileInfo.Delete();
                }
                Files.DeleteDirectory(PeriodPath);
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SecondSemesterFix", "");
            }
            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe"))
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe", Resources.nircmd);
            }
            if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations"))
            {
                Directory.CreateDirectory(PeriodPath);
                //button1.Visible = true;
                Console.WriteLine("No periods set");
                while (SelectedPeriods == false)
                {
                    await Task.Delay(10);
                }

                void SetPeriod(string Link, int Period)
                {
                    File.WriteAllText(PeriodPath + "\\Period" + Period.ToString(), Link);
                }

                void SetName(string Name, int Period)
                {
                    File.WriteAllText(PeriodPath + "\\PeriodName" + Period.ToString(),Name);
                }
                SetPeriod(P1Link.Text, 1);
                SetPeriod(P2Link.Text, 2);
                SetPeriod(P3Link.Text, 3);
                SetPeriod(P4Link.Text, 4);
                SetName(P1Name.Text,1);
                SetName(P2Name.Text,2);
                SetName(P3Name.Text,3);
                SetName(P4Name.Text,4);

                if (PeriodAnnoucementsTextBox.Text != "")
                {
                    File.WriteAllText(PeriodPath + "\\PeriodAnnouncements.txt",PeriodAnnoucementsTextBox.Text);
                }
               
            }
            button1.Visible = false;
            Console.WriteLine("Waiting for period recordings path...");
            if (File.Exists(PeriodPath + "\\RecordingsSavings.txt"))
            {
                while (!Directory.Exists(File.ReadAllText(PeriodPath + "\\RecordingsSavings.txt")))
                {
                    await Task.Delay(10);
                    if (File.Exists(PeriodPath + "\\RecordingsSavings.txt"))
                    {
                        tabControl1.Visible = false;
                    }
                } 
            }
            if (!File.Exists(PeriodPath + "\\RecordingsSavings.txt"))
            {
                while (!File.Exists(PeriodPath + "\\RecordingsSavings.txt"))
                {
                    await Task.Delay(10);
                }
            }
            ImportButton.Enabled = true;
            if (File.Exists(File.ReadAllText(PeriodPath + "\\RecordingsSavings.txt") + "\\Configuration.ini"))
            {
                File.Delete(File.ReadAllText(PeriodPath + "\\RecordingsSavings.txt") + "\\Configuration.ini");
            }
            ZipFile.CreateFromDirectory(PeriodPath, File.ReadAllText(PeriodPath + "\\RecordingsSavings.txt") + "\\Configuration.ini");
            Visible = false;
            TopMost = true;
            ControlBox = false;
            tabControl1.Visible = false;
            if (!File.Exists(GoogleMeetClient))
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Meets%20Loader/bin/Debug/Meets%20Loader.exe"), GoogleMeetClient);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }

            string[] Rotation = RotationChecks.GetRotation();
            await ShowSchedule();
            // Initialization Variables //
            bool Shown1 = false;
            bool Shown2 = false;
            bool Shown3 = false;
            bool CreditRecoverySP1 = false;
            bool CreditRecoverySP2 = false;
            bool Used1 = false;
            bool Used2 = false;
            bool Play1 = false;
            bool Play2 = false;
            bool Play3 = false;
            bool Play4 = false;
            bool Play5 = false;
            bool Play6 = false;
            bool Play7 = false;
            TopMost = true;

            while (true)
            {

                try
                {
                    var CurrentDateTime = DateTime.Now;
                    await Task.Delay(10);
                    // Check Class Bell Times //
                    if (PeriodChecks.CheckPeriod1 && PeriodChecks.JoinedPeriod1 == false || PeriodChecks.CheckPeriod1SVS && PeriodChecks.JoinedPeriod1 == false)
                    {
                        await PlaySoundSync(Resources.Bell, true);
                        await ShowPopup("Period " + Rotation[0] + " is now starting");
                        PeriodChecks.JoinedPeriod1 = true;
                        await StartBandicamRecord();
                        int Minutes = FaceToFace ? 150 : 60;
                        await JoinClass(Rotation[0],Minutes);
                        await ShowSchedule(B1Label, "Current Class Info:");
                        if (PeriodNames.CheckPeriodAnnouncementExists)
                        {
                            await PlaySoundSync(GetAnnouncement(Rotation[0]));
                        }
                        await NormalVolume();
                    }
                    else if (PeriodChecks.CheckPeriod2 && PeriodChecks.JoinedPeriod2 == false || PeriodChecks.CheckPeriod2SVS && PeriodChecks.JoinedPeriod2 == false)
                    {
                        await PlaySoundSync(Resources.Bell, true);
                        await ShowPopup("Period " + Rotation[1] + " is now starting");
                        PeriodChecks.JoinedPeriod2 = true;
                        await StartBandicamRecord();
                        int Minutes = 40;
                        if (!FaceToFace) Minutes = 75;
                        await JoinClass(Rotation[1],Minutes);
                        await ShowSchedule(B2Label, "Current Class Info:");
                        if (PeriodNames.CheckPeriodAnnouncementExists)
                        {
                            await PlaySoundSync(GetAnnouncement(Rotation[1]));
                        }
                        await NormalVolume();
                    }
                    else if (PeriodChecks.CheckPeriod3 && PeriodChecks.JoinedPeriod3 == false || PeriodChecks.CheckPeriod3SVS && PeriodChecks.JoinedPeriod3 == false)
                    {
                        await PlaySoundSync(Resources.Bell, true);
                        await ShowPopup("Period " + Rotation[2] + " is now starting");
                        PeriodChecks.JoinedPeriod3 = true;
                        await StartBandicamRecord();
                        int Minutes = 40;
                        Minutes = FaceToFace ? 40 : 75;
                        if (!FaceToFace) Minutes = 75;
                        await JoinClass(Rotation[2],Minutes);
                        await ShowSchedule(B3Label, "Current Class Info:");
                        if (PeriodNames.CheckPeriodAnnouncementExists)
                        {
                            await PlaySoundSync(GetAnnouncement(Rotation[2]));
                        }
                        await NormalVolume();
                    }
                    else if (PeriodChecks.CheckPeriod4 && PeriodChecks.JoinedPeriod4 == false || PeriodChecks.CheckPeriod4SVS && PeriodChecks.JoinedPeriod4 == false)
                    {
                        await PlaySoundSync(Resources.Bell, true);
                        await ShowPopup("Period " + Rotation[3] + " is now starting");
                        PeriodChecks.JoinedPeriod4 = true;
                        await StartBandicamRecord();
                        int Minutes = 40;
                        if (!FaceToFace) Minutes = 60;
                        await JoinClass(Rotation[3],Minutes);
                        await ShowSchedule(B4Label, "Current Class Info:");
                        if (PeriodNames.CheckPeriodAnnouncementExists)
                        {
                            await PlaySoundSync(GetAnnouncement(Rotation[3]));
                        }
                        await NormalVolume();
                    }
                    if((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday  || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && CreditRecoverySP1 == false)
                    {
                        if (DateTime.Now.Hour == 15 && DateTime.Now.Minute >= 15)
                        {
                            CreditRecoverySP1 = true;
                            await PlaySoundSync(Resources.Bell, true);
                            while (CreditRecoveryMeetLink == "")
                            {
                                CreditRecoveryJoin join = new CreditRecoveryJoin();
                                join.ShowDialog();
                            }
                            await ShowPopup("Period " + Rotation[3] + " is now starting");
                            PeriodChecks.JoinedPeriod4 = true;
                            await StartBandicamRecord();
                            int Minutes = 75;
                            await JoinClass("5", Minutes);
                            await ShowPopup("Welcome to your credit recovery class!");
                            await NormalVolume();
                        }
                    }
                    if ((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && CreditRecoverySP2 == false)
                    {
                        if (DateTime.Now.Hour == 16 && DateTime.Now.Minute >= 30)
                        {
                            CreditRecoverySP2 = true;
                            await PlaySoundSync(Resources.Bell, true);
                            await MaxVolume();
                            await PlayCompressedSound(Resources.OnlineClassesEnded);
                            await StopBandicamRecord("5");
                            if (FaceToFace == true)
                            {
                                await ShowPopup("THANK YOU FOR TAKING CREDIT RECOVERY CLASS");
                            }
                            else
                            {
                                await ShowPopup("THANK YOU FOR TAKING CREDIT RECOVERY CLASS");
                            }
                            await PlaySoundSync(Resources.HomeDepot,true);
                        }
                    }
                    // Check For Break Times //
                    string BreakTime()
                    {
                        string Return = "";
                        if (FaceToFace == true)
                        {
                            Return = "10";
                        }
                        else
                        {
                            Return = "5";
                        }
                        return Return;
                    }
                    if (PeriodBreakChecks.CheckStartOfDay && PeriodBreakChecks.StartOfDayChecked == false)
                    {
                        PeriodBreakChecks.StartOfDayChecked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync(Resources.TenMinuteBreak,true);
                        await MaxVolume();
                        await PlayCompressedSound(Resources.MorningAnnouncement);
                        await NormalVolume();
                        if (FaceToFace == true)
                        {
                            await ShowPopup("Please prepare for your online classes, they should\nbe starting in about 10 minutes\nyour first class of the day is\na period " + Rotation[0] + " class\nit will last from 8:20 AM - 10:50 AM.");
                        }
                        else
                        {
                            await ShowPopup("Please prepare for your online classes, they should\nbe starting in about 10 minutes\nyour first class of the day is\na period " + Rotation[0] + " class\nit will last from 8:20 AM - 9:35 AM.");
                        }
                        await ShowSchedule();
                        await ShowSchedule(B1Label, "Your next class info:");
                    }
                    if (PeriodBreakChecks.CheckPeriod1 && PeriodBreakChecks.Period1Checked == false || PeriodBreakChecks.CheckPeriod1SVS && PeriodBreakChecks.Period1Checked == false)
                    {
                        PeriodBreakChecks.Period1Checked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync(Resources.TenMinuteBreak,true);
                        await StopBandicamRecord(Rotation[0]);
                        await ShowPopup("Period " + Rotation[0] + " should end now, you have a " + BreakTime() + "\nminute break, enjoy!");
                        await ShowSchedule();
                        await ShowSchedule(B1Label, "Your next class info:");
                        await MaxVolume();
                        if (FaceToFace == true)
                        {
                            await PlayCompressedSound(Resources.LunchTime);
                        }
                        else
                        {
                            
                        }
                        await NormalVolume();
                    }
                    // Period 2 Break (Prepare for next learning block if face to face)//
                    else if (PeriodBreakChecks.CheckPeriod2 && PeriodBreakChecks.Period2Checked == false || PeriodBreakChecks.CheckPeriod2SVS && PeriodBreakChecks.Period2Checked == false)
                    {
                        PeriodBreakChecks.Period2Checked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync( Resources.TenMinuteBreak,true);
                        if (!FaceToFace)
                        {
                            await StopBandicamRecord(Rotation[1]);
                        }

                        if (FaceToFace)
                        {
                            await ShowPopup("Period " + Rotation[1] + " should end now, you have a " + BreakTime() + "\nminute break, enjoy!\nLunch is over, please prepare for your afternoon online classes\nThank you for taking your morning Face-To-Face classes");
                        }
                        else
                        {
                            await ShowPopup("Period " + Rotation[1] + " has ended, this is your lunch time, doordash!");
                            await PlayCompressedSound(Resources.LunchTime);
                            await PlaySoundSync(Resources.HomeDepot, true);
                        }

                        await ShowSchedule(B2Label, "Your next class info:");
                        await PlaySoundSync(Resources.AfternoonClasses,true);
                    }
                    else if (PeriodBreakChecks.CheckPeriod3 && PeriodBreakChecks.Period3Checked == false || PeriodBreakChecks.CheckPeriod3SVS && PeriodBreakChecks.Period3Checked == false)
                    {
                        PeriodBreakChecks.Period3Checked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync( Resources.TenMinuteBreak,true);
                        await StopBandicamRecord(Rotation[1]);
                        await ShowPopup("Period " + Rotation[2] + " should end now, you have a " + BreakTime() + "\nminute break, enjoy!");
                        await ShowSchedule(B3Label, "Your next class info:");
                        if(FaceToFace == false)
                        {
                            await PlaySoundSync(Resources.AfternoonClass,true);
                            await PlaySoundSync(Resources.AfternoonContinued, true);
                        }
                        else
                        {
                            using (var client = new WebClient())
                            {
                                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/Engine/Engine/bin/Debug/Engine.exe"), Path.GetTempPath() + "\\AnnouncerSP.exe");
                                while (client.IsBusy)
                                {
                                    await Task.Delay(10);
                                }
                                Process.Start(Path.GetTempPath() + "\\AnnouncerSP.exe");
                            }
                        }
                    }
                    else if (PeriodBreakChecks.CheckPeriod4 && PeriodBreakChecks.Period4Checked == false || PeriodBreakChecks.CheckPeriod4SVS && PeriodBreakChecks.Period4Checked == false)
                    {
                        PeriodBreakChecks.Period4Checked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync( Resources.TenMinuteBreak,true);
                        await StopBandicamRecord(Rotation[2]);
                        await ShowPopup("Period " + Rotation[3] + " should end now, you have a " + BreakTime() + "\nminute break, enjoy!");
                        await ShowSchedule(B4Label, "Your next class info:");
                        if (FaceToFace == true)
                        {
                            await ShowPopup("This is your last class of the day enjoy\n(50 minutes of class remaining)!");
                        }
                        else
                        {
                            await ShowPopup("This is your last class of the day enjoy\n(75 minutes of class remaining)!");
                        }
                        await PlaySoundSync(Resources.LastClass, true);
                    }
                    else if (PeriodBreakChecks.CheckEndOfDay && PeriodBreakChecks.EndOfDayChecked == false || PeriodBreakChecks.CheckEndOfDaySVS && PeriodBreakChecks.EndOfDayChecked == false)
                    {
                        PeriodBreakChecks.EndOfDayChecked = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync(true, Resources.TenMinuteBreak);
                        await StopBandicamRecord(Rotation[3]);
                        await ShowPopup("Period " + Rotation[3] + " should end now, you have a " + BreakTime() + "\nminute break, enjoy!");
                        await ShowPopup("This is your last class, enjoy 10 minute break");
                    }
                    else if ((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && !Play7 && DateTime.Now.Hour == 15 && DateTime.Now.Minute == 5)
                    {
                        Play7 = true;
                        await PlaySoundSync(Resources.Bell, true);
                        await PlaySoundSync(true, Resources.TenMinuteBreak);
                        await ShowPopup("Your credit recovery class will start soon.");
                        await PlaySoundSync(Resources.CreditRecoveryStarting10Minutes,true);
                    }
                    // First Period Remaining Times //
                    if (FaceToFace == true)
                    {
                        if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 30 && ShownList[1] == false)
                        {
                            ShownList[1] = true;
                            await ShowPopup("2 hours of class left.");
                        }
                        if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 0 && Shown1 == false)
                        {
                            Shown1 = true;
                            await ShowPopup("1 hour and 50 minutes of class left.");
                        }
                        if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 30 && Shown2 == false)
                        {
                            Shown2 = true;
                            await ShowPopup("1 hour and 20 minutes of class left.");
                        }
                        if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 0 && Shown3 == false)
                        {
                            Shown3 = true;
                            await ShowPopup("50 minutes of class left.");
                        }

                        if (DateTime.Now.Hour == 10 && DateTime.Now.Minute == 30 && ShownList[0] == false)
                        {
                            ShownList[0] = true;
                            await ShowPopup("20 minutes of class left.\nOntario should be reporting COVID-19 cases shortly.");
                        }
                    }
                    // Music //
                    if (FaceToFace == true)
                    {
                        if (DateTime.Now.Hour == 6 && DateTime.Now.Minute >= 50 && DateTime.Now.Second > 30 && Play5 == false)
                        {
                            Play5 = true;
                            await MaxVolume();
                            await PlaySoundSync(Resources.MorningF2F,true);
                            //await ChangeTransition("Sleep", "COVID-19\nScreening");
                            Process.Start("https://docs.google.com/forms/d/e/1FAIpQLSedNWLgRdQKVfNqT4gwYrq0PEJqj2vnOL5GHqfopjwnakC-0g/viewform");
                            //await PlaySoundSync(Resources.Bell,true);
                            //await PlaySoundSync(Resources.FirstAlarm,true);
                            //await PlaySoundSync(Resources.SecondAlarm, true);
                            //await PlaySoundSync(Resources.LoudBellAlarmSound, true);
                            //await PlaySoundSync(Resources.NuclearAlarm, true);
                            await ShowSchedule(B1Label, "Wake up now");
                            await PlaySoundSync(Resources.HomeDepot,true);
                            await NormalVolume();
                        }
                        if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 15 && DateTime.Now.Second > 30 && Play1 == false)
                        {
                            Play1 = true;
                            await StartBandicam();
                            await PlayChristmasMusic();
                        }

                        if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 16 && DateTime.Now.Second > 30 && Play2 == false)
                        {
                            Play2 = true;
                            await StartBandicam();
                            await ChangeTransition("Lunch/Transport","2",false);
                            using(var client = new WebClient())
                            {
                                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/Engine/Engine/bin/Debug/Engine.exe"),Path.GetTempPath() + "\\AnnouncerSP.exe");
                                while (client.IsBusy)
                                {
                                    await Task.Delay(10);
                                }
                                Process.Start(Path.GetTempPath() + "\\AnnouncerSP.exe");
                            }
                        }

                        if (DateTime.Now.Hour == 13 && DateTime.Now.Minute == 5 && DateTime.Now.Second > 30 && Play3 == false)
                        {
                            Play3 = true;
                            await StartBandicam();
                            await ChangeTransition("2","3",false);
                        }

                        if (DateTime.Now.Hour == 13 && DateTime.Now.Minute == 56 && DateTime.Now.Second > 30 && Play4 == false)
                        {
                            Play4 = true;
                            await StartBandicam();
                            await ChangeTransition("3","4",false);
                        }
                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute == 45 && ShownList[4] == false)
                        {
                            ShownList[4] = true;
                            await ChangeTransition("4", "Free Time",false);
                        }
                        // Other Special Announcements //
                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 45 && ShownList[3] == false)
                        {
                            ShownList[3] = true;
                            await MaxVolume();
                            await PlayCompressedSound(Resources.EndOfDayAnnouncement);
                            await NormalVolume();
                        }
                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 50 && Used1 == false)
                        {
                            Used1 = true;
                            await PlaySoundSync(Resources.Bell, true);
                            await MaxVolume();
                            await PlayCompressedSound(Resources.OnlineClassesEnded);
                            await NormalVolume();
                            await PlaySoundSync(Resources.HomeDepot,true);
                            await ShowPopup("Online classes have ended, enjoy the rest of your day!");
                            await ShowSchedule();
                        }

                        if ((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && DateTime.Now.Hour == 15 && DateTime.Now.Minute >= 12 && !Play6)
                        {
                            Play6 = true;
                            await MaxVolume();
                            await ChangeTransition("4", "Credit Recovery",true);
                            await NormalVolume();
                        }
                    }
                    else
                    {
                        if (DateTime.Now.Hour == 8 && DateTime.Now.Minute == 15 && DateTime.Now.Second > 30 && Play1 == false)
                        {
                            Play1 = true;
                            await StartBandicam();
                            await ChangeTransition("Sleep","1",true);
                            await PlayChristmasMusic();
                        }
                        if (DateTime.Now.Hour == 9 && DateTime.Now.Minute == 37 && DateTime.Now.Second > 30 && Play2 == false)
                        {
                            Play2 = true;
                            await StartBandicam();
                            await ChangeTransition("1", "2",true);
                        }

                        if (DateTime.Now.Hour == 12 && DateTime.Now.Minute == 13 && DateTime.Now.Second > 30 && Play3 == false)
                        {
                            BackgroundWorker CovidWorker = new BackgroundWorker();
                            CovidWorker.DoWork += async (o, args) =>
                            {
                                using (var client = new WebClient())
                                {
                                    client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/Engine/Engine/bin/Debug/Engine.exe"), Path.GetTempPath() + "\\AnnouncerSP.exe");
                                    while (client.IsBusy)
                                    {
                                        await Task.Delay(10);
                                    }
                                    Process.Start(Path.GetTempPath() + "\\AnnouncerSP.exe");
                                }
                            };
                            CovidWorker.RunWorkerAsync();
                            Play3 = true;
                            await StartBandicam();
                            await ChangeTransition("Lunch", "3",true);

                        }
                        if (DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 31 && DateTime.Now.Minute <= 34 && DateTime.Now.Second > 30 && Play4 == false)
                        {
                            Play4 = true;
                            await StartBandicam();
                            await ChangeTransition("3", "4",true);
                        }

                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 35 && DateTime.Now.Minute <= 39 && ShownList[4] == false)
                        {
                            ShownList[4] = true;
                            await ChangeTransition("4","Asynchronous",true);
                        }
                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 40 && DateTime.Now.Minute <= 44 && ShownList[3] == false)
                        {
                            ShownList[3] = true;
                            await MaxVolume();
                            await PlayCompressedSound(Resources.EndOfDayAnnouncement);
                            await PlaySoundSync(Resources.ClassesAlmostEnding,true);
                            await NormalVolume();
                        }
                        if (DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 45 && Used1 == false)
                        {
                            Used1 = true;
                            await PlaySoundSync(Resources.Bell, true);
                            await MaxVolume();
                            if (DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday)
                            {
                                await PlaySoundSync(Resources.TenMinuteBreak, true);
                                await PlaySoundSync(Resources.SchoolNotOverYet, true);
                                await PlaySoundSync(Resources.YouHaveCreditRecoveryClass, true);
                            }
                            else
                            {
                                await PlayCompressedSound(Resources.OnlineClassesEnded);
                                await NormalVolume();
                                await PlaySoundSync(Resources.HomeDepot, true); 
                            }
                            
                            await ShowPopup("Online classes have ended, enjoy the rest of your day!");
                            await ShowSchedule();
                        }
                        if ((DateTime.Now.DayOfWeek == DayOfWeek.Tuesday || DateTime.Now.DayOfWeek == DayOfWeek.Wednesday) && DateTime.Now.Hour == 15 && DateTime.Now.Minute >= 12 && !Play6)
                        {
                            Play6 = true;
                            await MaxVolume();
                            await ChangeTransition("4", "Credit Recovery", true);
                            await NormalVolume();
                        }
                    }
                }
                catch (Exception ee)
                {
                    Console.WriteLine(ee);
                    //MessageBox.Show(ee.ToString(), "Execution will continue");
                    string TempFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
                    File.WriteAllText(TempFile, ee.ToString());
                    Process.Start(TempFile);
                }
            }
        }

        public async Task NormalVolume()
        {
            await ChangeVolume(14000);
        }

        public async Task HalfVolume()
        {
            await ChangeVolume(32767);
        }
        public async Task PlayChristmasMusic()
        {
            
        }

        public async Task PlayMP3File(byte[] Path)
        {
            try
            {
                string Temp1 = System.IO.Path.GetTempPath() + "\\" +
                               System.IO.Path.GetRandomFileName().Replace(".", "") + ".mp3";
                string Temp2 = System.IO.Path.GetTempPath() + "\\" +
                               System.IO.Path.GetRandomFileName().Replace(".", "") + ".wav";
                File.WriteAllBytes(Temp1, Path);
                ConvertMp3ToWav(Temp1, Temp2);
                await PlaySoundSync(Temp2, true);
            }
            catch
            {

            }
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
        public async Task ShowPopup(string Message)
        {
            Visible = true;
            TopMost = true;
            StatusLabel.Text = Message;
            await Task.Delay(5000);
            button3.Visible = true;
            Dismiss = false;
            if (Message.Contains("break") || Message.Contains("Break"))
            {
                for (int i = 0; i < 10; i++)
                {
                    await Task.Delay(1000);
                    if (Dismiss == true)
                    {
                        break;
                    }
                }

                if (Dismiss == false)
                {
                    await MaxVolume();
                    await PlayCompressedSound(Resources.DisengageWarning);
                    SoundPlayer FirstAlarm = new SoundPlayer(Resources.FirstAlarm);
                    SoundPlayer SecondAlarm = new SoundPlayer(Resources.SecondAlarm);
                    SoundPlayer ThirdAlarm = new SoundPlayer(Resources.LoudBellAlarmSound);
                    SoundPlayer FourthAlarm = new SoundPlayer(Resources.NuclearAlarm);
                    SoundPlayer FifthAlarm = new SoundPlayer(Resources.BioHazard);
                    FirstAlarm.Load();
                    SecondAlarm.Load();
                    ThirdAlarm.Load();
                    FourthAlarm.Load();
                    FifthAlarm.Load();
                    int DismissLimit = 0;
                    while (Dismiss == false)
                    {
                        await Task.Delay(10);
                        await Task.Factory.StartNew(() =>
                        {
                            FirstAlarm.PlaySync();
                            if(Dismiss) return;
                            SecondAlarm.PlaySync();
                            if (Dismiss) return;
                            ThirdAlarm.PlaySync();
                            if (Dismiss) return;
                            FourthAlarm.PlaySync();
                            if (Dismiss) return;
                            FifthAlarm.PlaySync();
                        });
                        DismissLimit++;
                        if (DismissLimit >= 5)
                        {
                            break;
                        }
                    }
                }
            }
            button3.Enabled = true;
            button3.Visible = false;
            Dismiss = false;
            Visible = false;
        }

        private async Task MaxVolume()
        {
            await ChangeVolume(65535);
        }
        private async Task PlayCompressedSound(Byte[] location)
        {
            string RandomName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            RandomName.Replace(".", "");
            File.WriteAllBytes(RandomName + ".zip", location);
            Directory.CreateDirectory(RandomName);
            ZipFile.ExtractToDirectory(RandomName + ".zip", RandomName);
            foreach (var VARIABLE in new DirectoryInfo(RandomName).GetFiles())
            {
                await Task.Factory.StartNew(() =>
                {
                    new SoundPlayer(VARIABLE.FullName).PlaySync();
                });
            }
        }

        private async Task PlayCompressedSound(string SoundName)
        {
            //string RandomName = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            //RandomName.Replace(".", "");
            //using (var client = new WebClient())
            //{
            //    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Rotation-Schedule-Sounds/master/" + SoundName + ".zip"), RandomName + ".zip");
            //    while (client.IsBusy)
            //    {
            //        await Task.Delay(10);
            //    }
            //}
            //Directory.CreateDirectory(RandomName);
            //ZipFile.ExtractToDirectory(RandomName + ".zip", RandomName);
            //foreach (var VARIABLE in new DirectoryInfo(RandomName).GetFiles())
            //{
            //    await Task.Factory.StartNew(() =>
            //    {
            //        new SoundPlayer(VARIABLE.FullName).PlaySync();
            //    });
            //}
        }

        private string F2F()
        {
            string Return = "";
            if (FaceToFace == true)
            {
                Return = "Face To Face";
            }
            else
            {
                Return = "";
            }
            return Return;
        }

        public static string PeriodName = "";

        public string GetPeriodName(string Period)
        {
            string Return = "";
            if (Period == "1")
            {
                Return = PeriodNames.Period1;
            }
            else if (Period == "2")
            {
                Return = PeriodNames.Period2;
            }
            else if (Period == "3")
            {
                Return = PeriodNames.Period3;
            }
            else if (Period == "4")
            {
                Return = PeriodNames.Period4;
            }
            return Return;
        }
        public async Task ShowSchedule()
        {
            try
            {
                button3.Visible = false;
                string[] Rotation = RotationChecks.GetRotation();
                Console.WriteLine(Rotation[0] + Rotation[1] + Rotation[2] + Rotation[3]);
                Visible = true;
                StatusLabel.Text = "Your current rotation schedule:\n" + F2F() + "";
                if (FaceToFace == true)
                {
                    B1Label.Text = "8:20AM - 10:50AM (no break): Period " + Rotation[0] + " - " + GetPeriodName(Rotation[0]);
                    B2Label.Text = "12:20PM - 1:00PM (10 minute break): Period " + Rotation[1] + " - " + GetPeriodName(Rotation[1]);
                    B3Label.Text = "1:10PM - 1:50PM (10 minute break): Period " + Rotation[2] + " - " + GetPeriodName(Rotation[2]);
                    B4Label.Text = "2:00PM - 2:40PM (10 minute break): Period " + Rotation[3] + " - " + GetPeriodName(Rotation[3]);
                }
                else
                {
                    B1Label.Text = "8:20AM - 9:35AM (5 minute break): Period " + Rotation[0] + " - " + PeriodNames.Period1;
                    B2Label.Text = "9:40AM - 10:55AM (5 minute break): Period " + Rotation[1] + " - " + PeriodNames.Period2;
                    B3Label.Text = "12:15PM - 1:30PM (5 minute break): Period " + Rotation[2] + " - " + PeriodNames.Period3;
                    B4Label.Text = "1:35PM - 2:50PM (5 minute break): Period " + Rotation[3] + " - " + PeriodNames.Period4;
                }
                await Task.Delay(5000);
                Visible = false;
            }
            catch (Exception ex)
            {
                TopMost = false;
                Console.WriteLine(ex);
            }
        }
        public async Task ShowSchedule(Label highlight, string Title)
        {
            button3.Visible = false;
            string[] Rotation = RotationChecks.GetRotation();
            Visible = true;
            //StatusLabel.Text = Title;
            StatusLabel.Text = "Your current rotation schedule:\n" + F2F() + "";
            if (FaceToFace == true)
            {
                B1Label.Text = "8:20AM - 10:50AM (no break): Period " + Rotation[0] + " - " + GetPeriodName(Rotation[0]);
                B2Label.Text = "12:20PM - 1:00PM (10 minute break): Period " + Rotation[1] + " - " + GetPeriodName(Rotation[1]);
                B3Label.Text = "1:10PM - 1:50PM (10 minute break): Period " + Rotation[2] + " - " + GetPeriodName(Rotation[2]);
                B4Label.Text = "2:00PM - 2:40PM (10 minute break): Period " + Rotation[3] + " - " + GetPeriodName(Rotation[3]);
            }
            else
            {
                B1Label.Text = "8:20AM - 9:35AM (5 minute break): Period " + Rotation[0] + " - " + PeriodNames.Period1;
                B2Label.Text = "9:40AM - 10:55AM (5 minute break): Period " + Rotation[1] + " - " + PeriodNames.Period2;
                B3Label.Text = "12:15PM - 1:30PM (5 minute break): Period " + Rotation[2] + " - " + PeriodNames.Period3;
                B4Label.Text = "1:35PM - 2:50PM (5 minute break): Period " + Rotation[3] + " - " + PeriodNames.Period4;
            }
            highlight.BackColor = Color.Yellow;
            await Task.Delay(5000);
            highlight.BackColor = Color.Transparent;
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SelectedPeriods = true;
        }

        private async Task StartBandicam()
        {
            try
            {
                await Command.RunCommandHidden("taskkill /f /im \"bdcam.exe\"");
                Process.Start(@"C:\Program Files (x86)\Bandicam\bdcam_nonadmin.exe", "/nosplash");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private async Task StartBandicamRecord()
        {
            try
            {
                if (Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Bandicam"))
                {
                    DirectoryInfo d = new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Bandicam");
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Other Bandicam Recordings");
                    foreach (var fileInfo in d.GetFiles())
                    {
                        fileInfo.MoveTo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Other Bandicam Recordings\\" + fileInfo.Name);
                    }
                }
                Process.Start(@"C:\Program Files (x86)\Bandicam\bdcam_nonadmin.exe", "/record");
            }
            catch (Exception)
            {

            }
        }

        private async Task StopBandicamRecord(string Period)
        {
            try
            {
                Process.Start(@"C:\Program Files (x86)\Bandicam\bdcam_nonadmin.exe", "/stop");
                await Task.Delay(10000);
                string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";
                string TheDirectory = File.ReadAllText(PeriodPath + "\\RecordingsSavings.txt");
                if (!Directory.Exists(TheDirectory))
                {

                    Directory.CreateDirectory(TheDirectory);
                }
                string TodaysDirectory = TheDirectory + "\\" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString();
                Directory.CreateDirectory(TodaysDirectory);
                DirectoryInfo d = new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\Bandicam");
                bool Moved = false;
                while (Moved == false)
                {
                    try
                    {
                        foreach (var fileInfo in d.GetFiles())
                        {
                            BackgroundWorker Worker = new BackgroundWorker();
                            Worker.DoWork += (sender, args) =>
                            {
                                try
                                {
                                    fileInfo.MoveTo(TodaysDirectory + "\\Period " + Period + ".mp4");
                                }
                                catch
                                {
                                    Moved = false;
                                }
                            };
                            Worker.RunWorkerAsync();
                            while (Worker.IsBusy)
                            {
                                await Task.Delay(10);
                            }
                        }
                        Moved = true;
                    }
                    catch
                    {
                        Moved = false;
                        await Task.Delay(10000);
                    }
                }
            }
            catch (Exception)
            {
                
            }
        }
        public static string CurrentPeriod = "";
        public async Task JoinClass(string Period,int Minutes)
        {
            string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";
            if (Period == "1")
            {
                PeriodName = PeriodNames.Period1;
            }
            else if (Period == "2")
            {
                PeriodName = PeriodNames.Period2;
            }
            else if (Period == "3")
            {
                PeriodName = PeriodNames.Period3;
            }
            else if (Period == "4")
            {
                PeriodName = PeriodNames.Period4;
            }
            else if (Period == "5")
            {
                PeriodName = "Credit Recovery";
            }
            if (Period == "1")
            {
                await PlaySoundSync( Resources.Period1Start,true);
                string Link = File.ReadAllText(PeriodPath + "\\Period1");
                if (Link.Contains("meet.google"))
                {
                    await JoinGoogleMeet(Link);
                }
                else
                {
                    await JoinZoomMeeting(Link);
                }
            }
            if (Period == "2")
            {
                await PlaySoundSync( Resources.Period2Start,true);
                string Link = File.ReadAllText(PeriodPath + "\\Period2");
                if (Link.Contains("meet.google"))
                {
                    await JoinGoogleMeet(Link);
                }
                else
                {
                    await JoinZoomMeeting(Link);
                }
            }
            if (Period == "3")
            {
                await PlaySoundSync( Resources.Period3Start,true);
                string Link = File.ReadAllText(PeriodPath + "\\Period3");
                if (Link.Contains("meet.google"))
                {
                    await JoinGoogleMeet(Link);
                }
                else
                {
                    await JoinZoomMeeting(Link);
                }
            }
            if (Period == "4")
            {
                await PlaySoundSync(Resources.Period4Start,true);
                string Link = File.ReadAllText(PeriodPath + "\\Period4");
                if (Link.Contains("meet.google"))
                {
                    await JoinGoogleMeet(Link);
                }
                else
                {
                    await JoinZoomMeeting(Link);
                }
            }
            if (Period == "5")
            {
                await PlaySoundSync(Resources.CreditRecoveryStart,true);
                string Link = CreditRecoveryMeetLink;
                if (Link.Contains("meet.google"))
                {
                    await JoinGoogleMeet(Link);
                }
                else
                {
                    await JoinZoomMeeting(Link);
                }
            }
            await NormalVolume();
            CurrentPeriod = Period;
            InClass ss = new InClass();
            PeriodTime = Minutes.ToString();
            ss.Show();
        }

        public async Task PlaySoundSync(Stream location, bool MaxVolume)
        {

            if (MaxVolume == true)
            {
                await ChangeVolume(65535);
            }
            await Task.Factory.StartNew(() =>
            {
                new SoundPlayer(location).PlaySync();
            });
            await ChangeVolume(15000);
        }

        public async Task PlaySoundSync(string location, bool MaxVolume)
        {

            if (MaxVolume == true)
            {
                await ChangeVolume(65535);
            }
            await Task.Factory.StartNew(() =>
            {
                new SoundPlayer(location).PlaySync();
            });
            await ChangeVolume(10000);
        }

        public async Task PlaySoundSync(bool MediumVolume, Stream location)
        {

            if (MediumVolume == true)
            {
                await ChangeVolume(32767);
            }
            await Task.Factory.StartNew(() =>
            {
                new SoundPlayer(location).PlaySync();
            });
            await ChangeVolume(15000);
        }

        public async Task PlaySoundSync(string SoundLink)
        {
            try
            {
                await MaxVolume();
                string TempPath = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", "")) + ".wav";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(SoundLink), TempPath);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
                await Task.Factory.StartNew(() =>
                {
                    try
                    {
                        new SoundPlayer(SoundLink).PlaySync();
                    }
                    catch
                    {

                    }
                });
            }
            catch (Exception)
            {
                
            }
        }

        private async Task ChangeVolume(double Volume)
        {
            Console.WriteLine("CHANGE VOLUME TO " + Volume);
            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe"))
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe",Resources.nircmd);
            }
            await Command.RunCommandHidden("\"" + Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe\"" + " setsysvolume " + Volume.ToString());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.classroom.google.com/u/1/h");
        }
        bool Dismiss = false;
        private async void button3_Click(object sender, EventArgs e)
        {
            Dismiss = true;
            button3.Enabled = false;
            while (Dismiss == true)
            {
                await Task.Delay(10);
            }
            button3.Enabled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            TopMost = false;
            try
            {
                WPFFolderBrowserDialog d = new WPFFolderBrowserDialog();
                d.ShowDialog();
                RecordingsPathTextBox.Text = d.FileName;
            }
            catch (Exception exception)
            {

            }
            TopMost = true;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";
            File.WriteAllText(PeriodPath + "\\RecordingsSavings.txt", RecordingsPathTextBox.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog d = new OpenFileDialog();
                d.ShowDialog();
                string[] Periods = File.ReadAllLines(d.FileName);

                P1Link.Text = Periods[0];
                P2Link.Text = Periods[1];
                P3Link.Text = Periods[2];
                P4Link.Text = Periods[3];
            }
            catch (Exception)
            {

            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";
            OpenFileDialog d = new OpenFileDialog();
            d.ShowDialog();
            try
            {
                ZipFile.ExtractToDirectory(d.FileName,PeriodPath);
            }
            catch (Exception exception)
            {
                
            }
            Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }

    public static class DateTimeExtensions
    {
        public static bool InRange(this DateTime dateToCheck, DateTime startDate, DateTime endDate)
        {
            return dateToCheck >= startDate && dateToCheck < endDate;
        }
    }

    public static class SetPeriod
    {
        public static string Period1
        {
            get;
            set;
        }

        public static string Period2
        {
            get;
            set;
        }

        public static string Period3
        {
            get;
            set;
        }

        public static string Period4
        {
            get;
            set;
        }
    }

    public static class PeriodNames
    {
        private static string PeriodPath = Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations";

        public static string Period1
        {
            get
            {
                string Return = "";
                if (File.Exists(PeriodPath + "\\PeriodName1"))
                {
                    Return = File.ReadAllText(PeriodPath + "\\PeriodName1");
                }
                return Return;
            }
        }
        public static string Period2
        {
            get
            {
                string Return = "";
                if (File.Exists(PeriodPath + "\\PeriodName2"))
                {
                    Return = File.ReadAllText(PeriodPath + "\\PeriodName2");
                }
                return Return;
            }
        }
        public static string Period3
        {
            get
            {
                string Return = "";
                if (File.Exists(PeriodPath + "\\PeriodName3"))
                {
                    Return = File.ReadAllText(PeriodPath + "\\PeriodName3");
                }
                return Return;
            }
        }
        public static string Period4
        {
            get
            {
                string Return = "";
                if (File.Exists(PeriodPath + "\\PeriodName4"))
                {
                    Return = File.ReadAllText(PeriodPath + "\\PeriodName4");
                }
                return Return;
            }
        }

        public static bool CheckPeriodAnnouncementExists
        {
            get
            {
                return File.Exists(PeriodPath + "\\PeriodAnnouncements.txt");
            }
        }

        public static string[] PeriodAnnouncements
        {
            get
            {
                string[] Return = { };
                if (File.Exists(PeriodPath + "\\PeriodAnnouncements.txt"))
                {
                    Return = File.ReadAllLines(PeriodPath + "\\PeriodAnnouncements.txt");
                }
                return Return;
            }
        }
    }

    public static class RotationChecks
    {
        private static string[] Rotation1Periods = { "1", "2", "3", "4" };
        private static string[] Rotation2Periods = { "2", "1", "3", "4" };
        private static string[] Rotation3Periods = { "3", "4", "1", "2" };
        private static string[] Rotation4Periods = { "4", "3", "1", "2" };

        public static string[] GetRotation()
        {
            string[] R1 = Rotation1Periods;
            string[] R2 = Rotation2Periods;
            string[] R3 = Rotation3Periods;
            string[] R4 = Rotation4Periods;
            string[] Return = { };
            if (Form1.FaceToFace == true)
            {
                if (DateTimeExtensions.InRange(DateTime.Now, new DateTime(2021, 2, 16), new DateTime(2021, 1, 26)) || DateTimeExtensions.InRange(DateTime.Now,new DateTime(2021,4,8), new DateTime(2021,5,1)))
                {
                    Return = R1;
                }
                else if (DateTimeExtensions.InRange(DateTime.Now, new DateTime(2020, 3, 1), new DateTime(2021, 3, 12)) || DateTimeExtensions.InRange(DateTime.Now,new DateTime(2021,5,3), new DateTime(2021,5,20)))
                {
                    Return = R2;
                }
                else if (DateTimeExtensions.InRange(DateTime.Now, new DateTime(2021, 3, 12), new DateTime(2021, 3, 24)) || DateTimeExtensions.InRange(DateTime.Now, new DateTime(2021, 5, 21), new DateTime(2021, 6, 9)))
                {
                    Return = R3;
                }
                else if (DateTimeExtensions.InRange(DateTime.Now, new DateTime(2021,3,25), new DateTime(2021,4,8)) || DateTimeExtensions.InRange(DateTime.Now, new DateTime(2021,6,9), new DateTime(2021,6,25)))
                {
                    Return = R4;
                }
            }
            else
            {
                Return = R1;
            }

            return Return;
        }
    }
    public static class PeriodChecks
    {
        public static bool JoinedPeriod1 = false;
        public static bool JoinedPeriod2 = false;
        public static bool JoinedPeriod3 = false;
        public static bool JoinedPeriod4 = false;

        private static bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            // convert datetime to a TimeSpan
            TimeSpan now = datetime.TimeOfDay;
            // see if start comes before end
            if (start < end)
                return start <= now && now <= end;
            // start is after end, so do the inverse comparison
            return !(end < now && now < start);
        }
        public static bool CheckPeriod1
        {
            get
            {
                return DateTime.Now.Hour == 8 && DateTime.Now.Minute >= 20 && Form1.FaceToFace == true;
            }
        }

        public static bool CheckPeriod1SVS
        {
            get
            {
                return DateTime.Now.Hour == 8 && DateTime.Now.Minute >= 20 && Form1.FaceToFace == false;
            }
        }

        public static bool CheckPeriod2
        {
            get
            {
                return DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 20 && Form1.FaceToFace == true;
            }
        }

        public static bool CheckPeriod2SVS
        {
            get
            {
                return DateTime.Now.Hour == 9 && DateTime.Now.Minute >= 40 && Form1.FaceToFace == false;
            }
        }

        public static bool CheckPeriod3
        {
            get
            {
                return DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 10 && Form1.FaceToFace == true;
            }
        }

        public static bool CheckPeriod3SVS
        {
            get
            {
                return DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 15 && Form1.FaceToFace == false;
            }
        }

        public static bool CheckPeriod4
        {
            get
            {
                return DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 0 && Form1.FaceToFace == true;
            }
        }

        public static bool CheckPeriod4SVS
        {
            get
            {
                return DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 35 && Form1.FaceToFace == false;
            }
        }
    }

    public static class PeriodBreakChecks
    {
        public static bool Period1Checked = false;
        public static bool Period2Checked = false;
        public static bool Period3Checked = false;
        public static bool Period4Checked = false;
        public static bool StartOfDayChecked = false;
        public static bool EndOfDayChecked = false;

        public static bool CheckStartOfDay
        {
            get
            {
                return DateTime.Now.Hour == 8 && DateTime.Now.Minute >= 10;
            }
        }
        public static bool CheckPeriod1
        {
            get
            {
                return DateTime.Now.Hour == 10 && DateTime.Now.Minute >= 50 && Form1.FaceToFace;
            }
        }

        public static bool CheckPeriod1SVS
        {
            get
            {
                return DateTime.Now.Hour == 9 && DateTime.Now.Minute >= 20 && !Form1.FaceToFace;
            }
        }

        public static bool CheckPeriod2
        {
            get
            {
                return DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 10 && Form1.FaceToFace;
            }
        }

        public static bool CheckPeriod2SVS
        {
            get
            {
                return DateTime.Now.Hour == 10 && DateTime.Now.Minute >= 55 && !Form1.FaceToFace;
            }
        }
        public static bool CheckPeriod3
        {
            get
            {
                return DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 0 && Form1.FaceToFace;
            }
        }

        public static bool CheckPeriod3SVS
        {
            get
            {
                return DateTime.Now.Hour == 12 && DateTime.Now.Minute >= 10 && !Form1.FaceToFace;
            }
        }
        public static bool CheckPeriod4
        {
            get
            {
                return DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 50 && Form1.FaceToFace;
            }
        }

        public static bool CheckPeriod4SVS
        {
            get
            {
                return DateTime.Now.Hour == 13 && DateTime.Now.Minute >= 30 && Form1.FaceToFace == false;
            }
        }

        public static bool CheckEndOfDay
        {
            get
            {
                return DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 40 && Form1.FaceToFace;
            }
        }

        public static bool CheckEndOfDaySVS
        {
            get
            {
                return DateTime.Now.Hour == 14 && DateTime.Now.Minute >= 40 && Form1.FaceToFace == false;
            }
        }
    }


}

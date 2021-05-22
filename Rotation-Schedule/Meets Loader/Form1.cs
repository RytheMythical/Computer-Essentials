using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Runtime.CompilerServices;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EO.Base.UI;
using EO.WebBrowser;
using Meets_Loader.Properties;
using UsefulTools;

namespace Meets_Loader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            EO.WebBrowser.Runtime.AddLicense("6A+frfD09uihfsay4Q/lW5f69h3youbyzs2xaqW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaaW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaqW0s8uud7Oz8hfrqO7CzRrxndz22hnlqJfo8h/kdpm1wNyuaae0ws2frOzm1iPvounpBOzzdpm1wNyucrC9ys2fr9z2BBTup7Smw82faLXABBTmp9j4Bh3kd+T20tbFiajL4fPRoenW2RX4ksbS4hK8drOzBBTmp9j4Bh3kd7Oz/RTinuX39ul14+30EO2s3MLNF+ic3PIEEMidtbXE3rZ1pvD6DuSn6unaD7112PD9GvZ3s+X1D5+t8PT26KF+xrLUE/Go5Omzy/We6ff6Gu12mbbB2a9bl7PP5+Cd26QFJO+etKbW+q183/YAGORbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFppbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLhD+Vbl/r2HfKi5vLOzbFppbSzy653s+X1");
            Load += Form1_Load;
            Closing += Form1_Closing;
        }

        string MeetLink = "";
        bool Deport = false;
        bool Clicked = false;
        bool AutoDeport = false;
        private async void Form1_Closing(object sender, CancelEventArgs e)
        {
            if (Clicked == false && Rejoining == false)
            {
                if (AutoDeport == true)
                {
                    Clicked = true;
                    Deport = true;
                    e.Cancel = false;
                }
                else
                {
                    DialogResult hui = MessageBox.Show("Are you sure you wanna deport\n(Ignore error after closing)",
                        "Deport confirmation", MessageBoxButtons.YesNo);
                    if (hui == DialogResult.Yes)
                    {
                        Clicked = true;
                        Deport = true;
                        e.Cancel = false;
                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
            }
        }

        string[] CalledWords =
        {
            "Jackson", "how is everybody doing", "any questions", "questions anybody", "questions everyone", "wake up", "is anyone asleep", "ok lets do a lesson", "join the breakout room", "join breakout", /*"zoom",*/ "meet me in zoom",
            "everyone doing ok","doing good","good anyone","attendance"/*"questions"*/,"participation mark","participate","sleeping","tell me that you are here","tell me that you're here","tell me you here","please tell me that you are here","attendance"
        };

        private string GrabLinkHA = "";

        private string GrabLink
        {
            get
            {
                if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt"))
                {
                    GrabLinkHA = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt");
                }
                else
                {
                    GrabLinkHA = "account.google.com/signin";
                }
                return GrabLinkHA;
            }
        }

        private bool CheckNoCase(string text, string[] text2)
        {
            bool Return = false;
            foreach (string s in text2)
            {
                if (new CultureInfo("en-US").CompareInfo.IndexOf(text, s, CompareOptions.IgnoreCase) >= 0)
                {
                    Return = true;
                    break;
                }
            }
            return Return;
        }

        private async Task ChangeVolume(double Volume)
        {
            Console.WriteLine("CHANGE VOLUME TO " + Volume);
            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe"))
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe", Resources.nircmd);
            }
            await Command.RunCommandHidden("\"" + Environment.GetEnvironmentVariable("TEMP") + "\\nircmd.exe\"" + " setsysvolume " + Volume.ToString());
        }

        private async Task MaxVolume()
        {
            await ChangeVolume(65535);
        }
        public async Task NormalVolume()
        {
            await ChangeVolume(14000);
        }

        public async Task HalfVolume()
        {
            await ChangeVolume(32767);
        }
        bool Rejoining = false;

        private async void Form1_Load(object sender, EventArgs e)
        {
            string VoiceSelect = "";
            bool Nathan = false;
            SpeechSynthesizer dd = new SpeechSynthesizer();
            List<string> VoicesList = new List<string>();
            foreach (var so in dd.GetInstalledVoices())
            {
                if (so.VoiceInfo.Name == "Crystal16" || so.VoiceInfo.Name.Contains("Vocalizer"))
                {
                    VoicesList.Add(so.VoiceInfo.Name);
                }

                if (so.VoiceInfo.Name.Contains("Nathan"))
                {
                    Nathan = true;
                    dd.SelectVoice(so.VoiceInfo.Name);
                }
            }
            string[] VoiceSelecter = VoicesList.OfType<string>().ToArray();
            VoiceSelect = VoiceSelecter[new Random().Next(0, VoiceSelecter.Length)];
            if (Nathan == false)
            {
                dd.SelectVoice(VoiceSelect);
            }
            string URL = GrabLink;
            webView1.Url = GrabLink;
            MeetLink = GrabLink;
            if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt"))
            {
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\EnterGoogleMeet.txt");
            }
            else
            {
                webView1.Url = "google.ca";
            }
            Left = Top = 0;
            Width = Screen.PrimaryScreen.WorkingArea.Width;
            Height = Screen.PrimaryScreen.WorkingArea.Height;
            MuteAlarmCheckBox.Location = new Point(Size.Width - 200, Size.Height - 100);
            RaiseVolumeCheckBox.Location = new Point(Size.Width - 200, Size.Height - 85);
            RefreshButton.Location = new Point(Size.Width - 500, Size.Height - 100);
            StatusLabel.Location = new Point(Size.Width - 1000, Size.Height - 100);
            RefreshButton.Click += async (o, args) =>
            {
                RefreshButton.Enabled = false;
                System.Windows.Forms.DialogResult d = MessageBox.Show("Are you sure?","",MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {
                    Rejoining = true;
                    string TheFile = Path.GetTempPath() + "\\EnterGoogleMeet" + new Random().Next(11111, 55555) + ".exe";
                    File.WriteAllText(Path.GetTempPath() + "\\EnterGoogleMeet.txt",URL);
                    using(var client = new WebClient())
                    {
                        client.DownloadProgressChanged += (sender1, eventArgs) =>
                        {
                            RefreshButton.Text = eventArgs.ProgressPercentage.ToString();
                        };
                        client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Rotation-Schedule/Meets%20Loader/bin/Debug/Meets%20Loader.exe"),TheFile );
                        while (client.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    Rejoining = false;
                    Process.Start(TheFile);
                    Application.Exit();
                }
                
                RefreshButton.Enabled = true;
            };
            webControl1.Size = new Size(Size.Width - 20, Size.Height - 100);
            webView1.RequestPermissions += (o, args) =>
            {
                if (args.Permissions == Permissions.Microphone)
                {
                    args.Allow();
                }

                if (args.Permissions == Permissions.WebCam)
                {
                    args.Deny();
                }
            };
            webView1.NewWindow += (o, args) =>
            {
                Process.Start(args.TargetUrl);
            };
            CultureInfo culture = new CultureInfo("EN");
            TurnOnCaptionsLabel.Visible = false;
            while (true)
            {
                string HTML = "";
                BackgroundWorker HTMLGrabber = new BackgroundWorker();
                HTMLGrabber.DoWork += (o, args) =>
                {
                    HTML = webView1.GetHtml();
                };
                HTMLGrabber.RunWorkerAsync();
                while (HTMLGrabber.IsBusy)
                {
                    await Task.Delay(10);
                }
                bool Muted = MuteAlarmCheckBox.Checked;
                bool ReleaseAlarm = false;
                await Task.Delay(10);
                HTML = HTML.Replace("Jackson Chung", "");
                string[] CheckLines = HTML.Split(new []{Environment.NewLine},StringSplitOptions.None);
                foreach (string s in CheckLines)
                {
                    for (int j = 0; j < CalledWords.Length; j++)
                    {
                        if (culture.CompareInfo.IndexOf(s, CalledWords[j], CompareOptions.IgnoreCase) >= 0 && !Convert.ToBoolean(culture.CompareInfo.IndexOf(s,"Chung",CompareOptions.IgnoreCase) >= 0))
                        {
                            ReleaseAlarm = true;
                            break;
                        }
                    }
                }
                string WordCalled = "";
                for (int i = 0; i < CalledWords.Length; i++)
                {
                    if (culture.CompareInfo.IndexOf(HTML, CalledWords[i], CompareOptions.IgnoreCase) >= 0)
                    {
                        if (CalledWords[i].Contains("zoom"))
                        {
                            break;
                        }
                        if (RaiseVolumeCheckBox.Checked)
                        {
                            await MaxVolume();
                        }
                        if (Muted == false)
                        {
                            await Task.Factory.StartNew(() =>
                                            {
                                                dd.Speak("The teacher may want your attention");
                                            }); 
                        }
                        ReleaseAlarm = true;
                        WordCalled = CalledWords[i];
                        StatusLabel.Text = "The teacher may want your attention!, \"" + WordCalled + "\"";
                        await Task.Factory.StartNew(() =>
                        {
                            if (Muted == false && ReleaseAlarm == true)
                            {
                                dd.Speak(WordCalled);
                                new SoundPlayer(Resources.SecondAlarm).PlaySync();
                            }
                            
                        });
                        if (Muted == true)
                        {
                            StatusLabel.Text = "The teacher may want your attention!, \"" + WordCalled + "\"\nAlarm is muted";
                        }
                        if (RaiseVolumeCheckBox.Checked)
                        {
                            await NormalVolume();
                        }
                        ReleaseAlarm = false;
                        break;
                    }
                }

                if (Muted == true)
                {
                    //MuteAlarmCheckBox.Visible = false;
                    //await Task.Delay(TimeSpan.FromMinutes(10));
                    //MuteAlarmCheckBox.Visible = true;
                    //MuteAlarmCheckBox.Checked = false;
                }

                if (CheckNoCase(HTML,new []{"tell me that you are here","Looks like we are almost here","Looks like we're almost here","Looks like we are all here","We are all here", "tell me that you're here", "let me take attendance", "attendance", "tell me youre here","lets do attendance","let me do attendance"}))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    PlaySound(Resources.WarningAlert);
                    StatusLabel.Text = "Prepare for attendance!";
                    Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Prepare for attendance, your teacher will be taking attendance now");
                    });
                    bool TimeOut = false;
                    BackgroundWorker TimeOutWorker = new BackgroundWorker();
                    TimeOutWorker.DoWork += async (o, args) =>
                    {
                        await Task.Delay(TimeSpan.FromMinutes(5));
                        TimeOut = true;
                    };
                    TimeOutWorker.RunWorkerAsync();
                    while (!HTML.Contains("Jackson") && TimeOut == false)
                    {
                        await Task.Delay(10);
                        BackgroundWorker HTMLGrabberAttendance = new BackgroundWorker();
                        HTMLGrabberAttendance.DoWork += (o, args) =>
                        {
                            HTML = webView1.GetHtml();
                        };
                        HTMLGrabberAttendance.RunWorkerAsync();
                        while (HTMLGrabberAttendance.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                        HTML = HTML.Replace("Jackson Chung", "");
                    }
                    Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Your name is called, please respond to the teacher");
                    });
                    try
                    {
                        webView1.SendKeyEvent(true, KeyCode.ControlKey);
                        webView1.SendKeyEvent(true, KeyCode.D);
                    }
                    catch (Exception)
                    {
                        
                    }
                    if (HTML.Contains("Jackson"))
                    {
                        await PlaySoundSync(Resources.BioHazard);
                    }
                    StatusLabel.Text = "YOUR NAME IS CALLED!";
                    TurnOnCaptionsLabel.Text = "Your name is called\nfor attendance!";
                    TurnOnCaptionsLabel.Visible = true;
                    await Task.Delay(TimeSpan.FromSeconds(10));
                    TurnOnCaptionsLabel.Text = "TURN ON CAPTIONS";
                    TurnOnCaptionsLabel.Visible = false;
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }

                if(CheckNoCase(HTML,new [] {"no ones paying attention","not everyone is paying attention","participation marks","seems distracted","participation mark"}))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("You are not paying attention");
                    });
                    TurnOnCaptionsLabel.Text = "MARKS WILL COUNT!";
                    TurnOnCaptionsLabel.Visible = true;
                    await Task.Delay(3000);
                    TurnOnCaptionsLabel.Visible = false;
                    await PlaySoundSync(Resources.BioHazard);
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }

                if (HTML.Contains("You can't create a meeting yourself. Contact your system administrator for more information.") || HTML.Contains("This meeting hasn't started"))
                {
                    webControl1.Visible = false;
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    Task.Factory.StartNew(() =>
                    {
                        dd.Speak("The teacher has not started the meeting yet");
                    });
                    StatusLabel.Text = "Refreshing page until meeting starts..., meeting hasn't started yet.";
                    webView1.Reload();
                    await PlaySoundSync(Resources.Refreshing);
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                    webControl1.Visible = true;
                }

                if(HTML.Contains("Ready to join?"))
                {
                    //webView1.Reload();
                    webControl1.Visible = false;
                    StatusLabel.Text = "Joining meeting... Please wait";
                    while (!HTML.Contains("Join now") &&!HTML.Contains("Getting ready"))
                    {
                        await Task.Delay(10);
                        BackgroundWorker HTMLGrabberAttendance = new BackgroundWorker();
                        HTMLGrabberAttendance.DoWork += (o, args) =>
                        {
                            HTML = webView1.GetHtml();
                        };
                        HTMLGrabberAttendance.RunWorkerAsync();
                        while (HTMLGrabberAttendance.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    while(webView1.IsLoading == true)
                    {
                        await Task.Delay(10);
                    }
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    Task.Factory.StartNew(() =>
                    {
                        dd.Speak("The meeting is ready, please join the meeting right now, or you will be marked late.");
                    });
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                    try
                    {
                        //webView1.SendKeyEvent(true, KeyCode.ControlKey);
                        //webView1.SendKeyEvent(true, KeyCode.D);
                    }
                    catch (Exception)
                    {
                        
                    }
                    await Task.Delay(3000);
                    try
                    {
                        //for (int i = 0; i <= 7; i++)
                        //{
                        //    webView1.SendKeyEvent(true, KeyCode.Tab);
                        //    await Task.Delay(500);
                        //}
                        //webView1.SendKeyEvent(true, KeyCode.Enter);
                    }
                    catch (Exception)
                    {
                        
                    }
                    webControl1.Visible = true;
                }

                if (HTML.Contains("The meeting code you entered doesn't work") || HTML.Contains("Check your meeting code"))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Please sign in to yuor google account");
                    });
                    webView1.Url = "https://meet.google.com/";
                    StatusLabel.Text = "Please Sign In";
                    while (!HTML.Contains("Jackson Chung"))
                    {
                        await Task.Delay(10);
                        HTMLGrabber.RunWorkerAsync();
                        while (HTMLGrabber.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    webView1.Url = GrabLink;
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }

                if (HTML.Contains("You are now logged out."))
                {
                    StatusLabel.Text = "You got logged out";
                    webView1.Url = "https://meet.google.com/";
                    await Task.Delay(3000);
                    StatusLabel.Text = "Please Sign In";
                    while (!HTML.Contains("Jackson Chung"))
                    {
                        await Task.Delay(10);
                        HTMLGrabber.RunWorkerAsync();
                        while (HTMLGrabber.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    webView1.Url = GrabLink;
                }
                if(HTML.Contains("You've left the meeting") || HTML.Contains("You've been removed from the meeting"))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("You have left the class");
                    });
                    StatusLabel.Text = "You have left the class";
                    PlaySound(Resources.Leaving);
                    for (int i = 22; i >= 0; i--)
                    {
                        Text = "Deporting in.. " + i;
                        await Task.Delay(1000);
                    }
                    Clicked = false;
                    AutoDeport = true;
                    Close();
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }
                await Task.Delay(100);

                if (HTML.Contains("Turn on captions"))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Please turn on your captions");
                    });
                    PlaySound(Resources.Notification);
                    StatusLabel.Text = "Please turn on your captions";
                    TurnOnCaptionsLabel.Visible = true;
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }
                else if (HTML.Contains("joined"))
                {
                    PlaySound(Resources.Notification);
                    StatusLabel.Text = "Someone has joined the meeting";
                    await Task.Delay(3000);
                }
                else if (HTML.Contains("Turn off camera"))
                {
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await MaxVolume();
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Please turn off your camera");
                    });
                    PlaySound(Resources.Notification);
                    StatusLabel.Text = "CAMERA IS ON!, BEWARE!";
                    TurnOnCaptionsLabel.Visible = true;
                    await Task.Delay(3000);
                    TurnOnCaptionsLabel.Visible = false;
                    TurnOnCaptionsLabel.Text = "TURN ON CAPTIONS";
                    if (RaiseVolumeCheckBox.Checked)
                    {
                        await NormalVolume();
                    }
                }
                else if (HTML.Contains("Turn off microphone"))
                {
                    await Task.Factory.StartNew(() =>
                    {
                        dd.Speak("Your microphone is turned on");
                    });
                    PlaySound(Resources.Notification);
                    StatusLabel.Text = "Warning! Microphone on!";
                }
                else if (HTML.Contains("presenting"))
                {
                    //PlaySound(Resources.Notification);
                    StatusLabel.Text = "Presentation in progress";
                    await Task.Delay(3000);
                }
                else
                {
                    TurnOnCaptionsLabel.Visible = false;
                    StatusLabel.Text = "Google Meet";
                }
            }
        }

        bool ClickedOnce = false;
        bool ClickedClosed = false;

        private async Task PlaySoundSync(Stream sound)
        {
            await Task.Factory.StartNew(() =>
            {
                new SoundPlayer(sound).PlaySync();
            });
        }

        private void PlaySound(Stream sound)
        {
           new SoundPlayer(sound).Play();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {

        }

        private void RefreshButton_Click(object sender, EventArgs e)
        {

        }

        private void TurnOnCaptionsLabel_Click(object sender, EventArgs e)
        {
            TurnOnCaptionsLabel.Visible = false;
        }
    }
}

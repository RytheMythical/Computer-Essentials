using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Management;
using System.Media;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Main_Tools.Properties;


namespace Main_Tools
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load1;
            FormClosed += Form1_FormClosed;
            Load += Form1_Load2;
            Load += Form1_Load3;
        }

        private async void Form1_Load3(object sender, EventArgs e)
        {
            await ReleaseKey();
        }

        private async void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            await RunCommandHidden("taskkill /f /im \"" + Application.ExecutablePath + "\"\nstart \"\" \"" +
                                   Application.ExecutablePath + "\"");
        }

        private Double temperature = 0;
        private string instanceName = string.Empty;
        ManagementObjectSearcher searcher = new ManagementObjectSearcher(@"root\WMI", "SELECT * FROM MSAcpi_ThermalZoneTemperature");

        private async Task GetCPUTempurature()
        {
            foreach (ManagementObject obj in searcher.Get())
            {
                temperature = Convert.ToDouble(obj["CurrentTemperature"].ToString());
                // Convert the value to celsius degrees
                temperature = (temperature - 2732) / 10.0;
                instanceName = obj["InstanceName"].ToString();
            }

        }

        private async void Form1_Load2(object sender, EventArgs e)
        {
            while (true)
            {
                await Task.Delay(1000);
                if (Continue == false)
                {
                    await Task.Delay(30000);
                    Continue = true;
                }
            }
        }


        private void Form1_Load1(object sender, EventArgs e)
        {
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ISP"))
            {
                InternetCheck.InternetServiceProvider = File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ISP");
            }
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MinimizeBox = false;
            MaximizeBox = false;
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

        private bool Closed = false;
        private async void CloseButton_Click(object sender, EventArgs e)
        {
            await PlaySoundSync(Resources.Click);
            await ClosePopup();
        }

        private bool CountingDown = false;
        private async Task ClosePopup()
        {
            if (CountingDown == false)
            {
                AbortCountdown = true;
                CountingDown = true;
                CloseButton.Enabled = false;
                if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.exe"))
                {
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.exe");
                }

                if (IsNetgear == true)
                {
                    ErrorMessageLabel.Text = "Please plug in NetGear Router";
                }
                else
                {
                    ErrorMessageLabel.Text = "Waiting for internet";
                }

                ReasonLabel.Text = "Waiting for a internet connection\nbefore the popup closes";
                bool NetGearPlugin = false;
                if (IsNetgear == true)
                {
                    string NetGearCheck = "";
                    while (NetGearPlugin == false)
                    {
                        await Task.Delay(10);

                        BackgroundWorker GetNetGearStatus = new BackgroundWorker();
                        GetNetGearStatus.DoWork += (sender, args) =>
                        {
                            try
                            {
                                using (var client = new WebClient())
                                {
                                    NetGearCheck =
                                        client.DownloadString("http://192.168.1.1/MNU_access_setRecovery_index.htm");
                                }
                            }

                            catch
                            {
                                NetGearPlugin = false;
                            }
                        };
                        GetNetGearStatus.RunWorkerAsync();
                        while (GetNetGearStatus.IsBusy)
                        {
                            await Task.Delay(10);
                        }



                        if (NetGearCheck == "")
                        {
                            NetGearPlugin = false;
                        }
                        else if (NetGearCheck.Contains("MNU_access_unauthorized_checkSerial.htm"))
                        {
                            NetGearPlugin = true;
                        }
                    }

                    ErrorMessageLabel.Text = "Router plugged in, waiting for internet";
                    IsNetgear = false;
                }
                bool Internet = false;
                var i = 0;

                bool AlreadyStartedCountdown = false;
                bool FireRogers = false;
                bool AbortRogersPopup = false;
                while (Internet == false)
                {
                    Random dd = new Random();
                    string RestoreNumber = dd.Next(111, 999).ToString();
                    await Task.Delay(10);
                    try
                    {
                        BackgroundWorker InternetChecker = new BackgroundWorker();
                        InternetChecker.DoWork += async (sender, args) =>
                        {
                            using (var client = new WebClient())
                            {
                                client.DownloadFileAsync(
                                    new Uri("http://www.msftncsi.com/ncsi.txt"),
                                    Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.exe" + RestoreNumber);
                                while (client.IsBusy)
                                {
                                    await Task.Delay(10);
                                }
                            }
                        };
                        InternetChecker.RunWorkerAsync();
                        while (InternetChecker.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    catch (Exception jer)
                    {
                        Console.WriteLine(jer);
                    }

                    try
                    {
                        if (File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.exe" + RestoreNumber)
                            .ElementAtOrDefault(0) == "Microsoft NCSI")
                        {
                            Internet = true;
                        }

                        ReasonLabel.Text = ReasonLabel.Text + " " +
                                           File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
                                                          "\\InternetCheck.exe" + RestoreNumber)
                                               .ElementAtOrDefault(0);
                        try
                        {
                            File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.exe" +
                                        RestoreNumber);
                        }
                        catch (Exception d)
                        {
                            Console.WriteLine(d);
                        }
                    }
                    catch (Exception d)
                    {
                        Console.WriteLine(d);
                        Internet = false;
                    }


                    BackgroundWorker SeeRogers = new BackgroundWorker();
                    SeeRogers.DoWork += async (sender, args) =>
                    {
                        var o = 0;
                        while (o < 15)
                        {
                            await Task.Delay(1000);
                            Console.WriteLine(o + "/15");
                            o++;
                        }
                        if (AbortRogersPopup == false)
                        {
                            ContactRogers d = new ContactRogers();
                            d.ShowDialog();
                        }
                        else
                        {
                            AbortRogersPopup = false;
                        }
                        FireRogers = true;
                    };
                    if (FireRogers == true && NotifiedContact == false)
                    {
                        NotifiedContact = true;

                        i = 0;
                    }

                    if (AlreadyStartedCountdown == false && NotifiedContact == false)
                    {
                        SeeRogers.RunWorkerAsync();
                        AlreadyStartedCountdown = true;
                    }
                    else
                    {
                        i = 0;
                    }

                    i++;
                }

                AbortRogersPopup = true;
                CloseButton.Enabled = true;
                Internet = false;
                Visible = false;
                TopMost = false;
                await Task.Delay(60);
                Closed = true;
                NotifiedContact = false;
                PlaySound(Resources.RestoredInternet);
                await ShowNotification("Internet Connection Restored", "Internet connection was successfully restored");
                CountingDown = false;
            }
            else
            {

            }
        }

        private bool NotifiedContact = false;

        private async Task Popup(string ErrorMessage, string ReasonMessage)
        {
            Visible = true;
            TopMost = true;
            Closed = false;
            ErrorMessageLabel.Text = ErrorMessage;
            ReasonLabel.Text = ReasonMessage;
            AbortCountdown = false;
            ResetCountDown = true;
            while (Closed == false)
            {
                await Task.Delay(10);
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
        private async Task ShowNotification(string title, string message)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat", "@if (@X)==(@Y) @end /* JScript comment\r\n@echo off\r\n\r\nsetlocal\r\ndel /q /f %~n0.exe >nul 2>&1\r\nfor /f \"tokens=* delims=\" %%v in ('dir /b /s /a:-d  /o:-n \"%SystemRoot%\\Microsoft.NET\\Framework\\*jsc.exe\"') do (\r\n   set \"jsc=%%v\"\r\n)\r\n\r\nif not exist \"%~n0.exe\" (\r\n    \"%jsc%\" /nologo /out:\"%~n0.exe\" \"%~dpsfnx0\"\r\n)\r\n\r\nif exist \"%~n0.exe\" ( \r\n    \"%~n0.exe\" %* \r\n)\r\n\r\n\r\nendlocal & exit /b %errorlevel%\r\n\r\nend of jscript comment*/\r\n\r\nimport System;\r\nimport System.Windows;\r\nimport System.Windows.Forms;\r\nimport System.Drawing;\r\nimport System.Drawing.SystemIcons;\r\n\r\n\r\nvar arguments:String[] = Environment.GetCommandLineArgs();\r\n\r\n\r\nvar notificationText=\"Warning\";\r\nvar icon=System.Drawing.SystemIcons.Hand;\r\nvar tooltip=null;\r\n//var tooltip=System.Windows.Forms.ToolTipIcon.Info;\r\nvar title=\"\";\r\n//var title=null;\r\nvar timeInMS:Int32=2000;\r\n\r\n\r\n\r\n\r\n\r\nfunction printHelp( ) {\r\n   print( arguments[0] + \" [-tooltip warning|none|warning|info] [-time milliseconds] [-title title] [-text text] [-icon question|hand|exclamation|аsterisk|application|information|shield|question|warning|windlogo]\" );\r\n\r\n}\r\n\r\nfunction setTooltip(t) {\r\n\tswitch(t.toLowerCase()){\r\n\r\n\t\tcase \"error\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"none\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.None;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"info\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Info;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\t//tooltip=null;\r\n\t\t\tprint(\"Warning: invalid tooltip value: \"+ t);\r\n\t\t\tbreak;\r\n\t\t\r\n\t}\r\n\t\r\n}\r\n\r\nfunction setIcon(i) {\r\n\tswitch(i.toLowerCase()){\r\n\t\t //Could be Application,Asterisk,Error,Exclamation,Hand,Information,Question,Shield,Warning,WinLogo\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"application\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Application;\r\n\t\t\tbreak;\r\n\t\tcase \"аsterisk\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Asterisk;\r\n\t\t\tbreak;\r\n\t\tcase \"error\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"exclamation\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Exclamation;\r\n\t\t\tbreak;\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"information\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Information;\r\n\t\t\tbreak;\r\n\t\tcase \"question\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Question;\r\n\t\t\tbreak;\r\n\t\tcase \"shield\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Shield;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"winlogo\":\r\n\t\t\ticon=System.Drawing.SystemIcons.WinLogo;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\tprint(\"Warning: invalid icon value: \"+ i);\r\n\t\t\tbreak;\t\t\r\n\t}\r\n}\r\n\r\n\r\nfunction parseArgs(){\r\n\tif ( arguments.length == 1 || arguments[1].toLowerCase() == \"-help\" || arguments[1].toLowerCase() == \"-help\"   ) {\r\n\t\tprintHelp();\r\n\t\tEnvironment.Exit(0);\r\n\t}\r\n\t\r\n\tif (arguments.length%2 == 0) {\r\n\t\tprint(\"Wrong number of arguments\");\r\n\t\tEnvironment.Exit(1);\r\n\t} \r\n\tfor (var i=1;i<arguments.length-1;i=i+2){\r\n\t\ttry{\r\n\t\t\t//print(arguments[i] +\"::::\" +arguments[i+1]);\r\n\t\t\tswitch(arguments[i].toLowerCase()){\r\n\t\t\t\tcase '-text':\r\n\t\t\t\t\tnotificationText=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-title':\r\n\t\t\t\t\ttitle=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-time':\r\n\t\t\t\t\ttimeInMS=parseInt(arguments[i+1]);\r\n\t\t\t\t\tif(isNaN(timeInMS))  timeInMS=2000;\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-tooltip':\r\n\t\t\t\t\tsetTooltip(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-icon':\r\n\t\t\t\t\tsetIcon(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tdefault:\r\n\t\t\t\t\tConsole.WriteLine(\"Invalid Argument \"+arguments[i]);\r\n\t\t\t\t\tbreak;\r\n\t\t}\r\n\t\t}catch(e){\r\n\t\t\terrorChecker(e);\r\n\t\t}\r\n\t}\r\n}\r\n\r\nfunction errorChecker( e:Error ) {\r\n\tprint ( \"Error Message: \" + e.message );\r\n\tprint ( \"Error Code: \" + ( e.number & 0xFFFF ) );\r\n\tprint ( \"Error Name: \" + e.name );\r\n\tEnvironment.Exit( 666 );\r\n}\r\n\r\nparseArgs();\r\n\r\nvar notification;\r\n\r\nnotification = new System.Windows.Forms.NotifyIcon();\r\n\r\n\r\n\r\n//try {\r\n\tnotification.Icon = icon; \r\n\tnotification.BalloonTipText = notificationText;\r\n\tnotification.Visible = true;\r\n//} catch (err){}\r\n\r\n \r\nnotification.BalloonTipTitle=title;\r\n\r\n\t\r\nif(tooltip!==null) { \r\n\tnotification.BalloonTipIcon=tooltip;\r\n}\r\n\r\n\r\nif(tooltip!==null) {\r\n\tnotification.ShowBalloonTip(timeInMS,title,notificationText,tooltip); \r\n} else {\r\n\tnotification.ShowBalloonTip(timeInMS);\r\n}\r\n\t\r\nvar dieTime:Int32=(timeInMS+100);\r\n\t\r\nSystem.Threading.Thread.Sleep(dieTime);\r\nnotification.Dispose();");
            await RunCommandHidden("call \"" + Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat" +
                                   "\"   -tooltip warning -time 3000 -title \"" + title + "\" -text \"" + message +
                                   "\" -icon question");
        }

        private async Task FadeIn(int Delay)
        {
            Opacity = 0;
            while (!Convert.ToBoolean(Opacity == 100))
            {
                Opacity = Opacity + 1;
                await Task.Delay(Delay);
            }
        }
        private bool ActivatedCountdown = false;
        private bool ResetCountDown = false;
        bool AbortCountdown = false;
        private async Task AutoClosePopup()
        {
            if (ActivatedCountdown == false)
            {
                ActivatedCountdown = true;
                CloseButton.Text = "";
                var i = 20;
                while (i > 0)
                {
                    KeyDown += Form1_KeyDown;
                    AllowRelease = true;
                    if (AbortCountdown == true)
                    {
                        i = 0;
                        AbortCountdown = false;
                    }
                    if (ResetCountDown == true)
                    {
                        i = 20;
                        ResetCountDown = false;
                    }
                    CloseButton.Text = i.ToString();
                    await Task.Delay(1000);
                    i = i - 1;
                    KeyDown -= Form1_KeyDown;
                    AllowRelease = false;
                }
                CloseButton.Text = "...";
                await ClosePopup();
                ActivatedCountdown = false;
            }
            else
            {
                ResetCountDown = true;
            }
        }

        private bool AllowRelease = false;
        private bool Releasing = false;
        private async Task ReleaseKey()
        {
            while (true)
            {
                while (Releasing == false)
                {
                    await Task.Delay(10);
                }
                await PlaySoundSync(Resources.Click);
                await ClosePopup();
                Releasing = false;
            }
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == (Keys.Control | Keys.H))
            {
                if (AllowRelease == true)
                {
                    Releasing = true;
                }
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private async void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W)
            {
                if (AllowRelease == true)
                {
                    await PlaySoundSync(Resources.Click);
                    await ClosePopup();
                    AbortCountdown = true;
                }
            }
        }

        private bool IsNetgear = false;
        private bool NotNetGear = false;
        private async Task PopupWithSound(string ErrorMessage, string ReasonMessage, Stream soundLocation)
        {
            Console.WriteLine("PASSED BORDER - POPPING UP");
            if (NotNetGear == false)
            {
                Console.WriteLine("NetGear Router");
                if (ErrorMessage.Contains("NETGEARED") != ErrorMessage.Contains("NetGear") != ErrorMessage.Contains("netgear"))
                {
                    Console.WriteLine("NetGeared = true");
                    IsNetgear = true;
                    await PlaySoundSync(Resources.NetGeared);
                }
                //await FadeIn(100);
                Visible = true;
                TopMost = true;
                Closed = false;
                ErrorMessageLabel.Text = ErrorMessage;
                ReasonLabel.Text = ReasonMessage;
                SoundPlayer dew = new SoundPlayer(soundLocation);
                dew.Play();
                ResetCountDown = true;
                await AutoClosePopup();
                while (Closed == false)
                {
                    await Task.Delay(10);
                }
            }
            else
            {
                if (ErrorMessage.Contains("NETGEARED") != ErrorMessage.Contains("NetGear") != ErrorMessage.Contains("netgear"))
                {
                    Console.WriteLine("Cannot popup because no netgear router detected");
                }
                else
                {
                    Console.WriteLine("Popping up, (Non netgear)");
                    //await FadeIn(100);
                    Visible = true;
                    TopMost = true;
                    Closed = false;
                    ErrorMessageLabel.Text = ErrorMessage;
                    ReasonLabel.Text = ReasonMessage;
                    SoundPlayer dew = new SoundPlayer(soundLocation);
                    dew.Play();
                    ResetCountDown = true;
                    await AutoClosePopup();
                    while (Closed == false)
                    {
                        await Task.Delay(10);
                    }
                }
            }
        }

        private bool CheckedCorona = false;
        private string OldCoronavirusCases = "";
        private async void Form1_Load(object sender, EventArgs e)
        {
            bool DoNotCheckIP = false;
            ShowInTaskbar = false;
            Visible = false;
            string NewIP = String.Empty;
            string OldIP = String.Empty;
            try
            {
                string NetGearCheck = "";
                using (var client = new WebClient())
                {
                    NetGearCheck = client.DownloadString("http://192.168.1.1/MNU_access_setRecovery_index.htm");
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                NotNetGear = true;
            }
            try
            {
                using (var client = new WebClient())
                {
                    OldIP = client.DownloadString(new Uri("http://icanhazip.com"));
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
            catch (Exception hui)
            {
                Console.WriteLine(hui);
                await PopupWithSound("Check your internet connection", "Please check your internet connection", Resources.LostInternetConnection);
                DoNotCheckIP = true;
            }

            NewIP = OldIP;
            while (true)
            {
                await Task.Delay(10);
                try
                {
                    try
                    {
                        await GetCPUTempurature();
                    }
                    catch
                    {
                        Console.WriteLine("PC NOT SUPPORTED FOR CPU CHECK");
                    }
                    if (DoNotCheckIP == false)
                    {
                        try
                        {
                            using (var client = new WebClient())
                            {
                                NewIP = client.DownloadString(new Uri("http://icanhazip.com"));
                            }

                            IPLabel.Text = "Your IP address: " + NewIP;
                        }
                        catch
                        {

                        }
                    }
                    Console.WriteLine("CPU Tempurature: " + temperature);
                    if (temperature > 50)
                    {
                        await PopupWithSound("CPU Tempurature High", "Reason: Your CPU is overheating\nTempurature: " + temperature.ToString(), Resources.CPUTooHot);
                    }
                    if (OldIP == NewIP)
                    {

                    }
                    else
                    {
                        Visible = true;
                        TopMost = true;
                        Closed = false;
                        await PopupWithSound("Public IP Address Changed",
                            "Reason: Router restart or Connect/Disconnect from VPN\nYour Old Public IP Address: " +
                            OldIP + "\nYour New Public IP Address: " + NewIP, Resources.IPChanged);
                        while (Closed == false)
                        {
                            await Task.Delay(10);
                        }
                        InternetCheck d = new InternetCheck();
                        d.ShowDialog();
                    }

                    string NetGearCheck = "";
                    using (var client = new WebClient())
                    {
                        try
                        {
                            NetGearCheck = client.DownloadString("http://192.168.1.1/MNU_access_setRecovery_index.htm");
                        }
                        catch
                        {
                            NetGearCheck = "";
                        }
                    }
                    Console.WriteLine("NETGEAR STATUS => " + NetGearCheck);
                    if (NotNetGear == false)
                    {
                        if (NetGearCheck == "")
                        {
                            await PopupInternetFailure("NetGeared, cannot find the router connection page");
                        }
                    }

                    OldIP = NewIP;
                    Continue = false;
                    BackgroundWorker InternetChecker = new BackgroundWorker();
                    InternetChecker.DoWork += (o, args) =>
                    {
                        using (var client = new WebClient())
                        {
                            client.DownloadFileCompleted += Client_DownloadFileCompleted;
                            client.DownloadFileAsync(new Uri("http://www.msftncsi.com/ncsi.txt"),
                                Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.txt");
                        }
                    };
                    InternetChecker.RunWorkerAsync();
                    while (InternetChecker.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                    while (Continue == false)
                    {
                        await Task.Delay(10);
                    }
                    Random AntiSuper7File = new Random();
                    string NumberAntiSuper7 = AntiSuper7File.Next(111, 999).ToString();
                    Random SecondSuper7 = new Random();
                    string NumberSecondAntiSuper7 = SecondSuper7.Next(111, 999).ToString();
                    string CheckCoronavirusCasesFile =
                        Environment.GetEnvironmentVariable("TEMP") + "\\" + NumberSecondAntiSuper7 + "CoronavirusCheckerThing" + NumberAntiSuper7 + ".txt";
                    if (File.Exists(CheckCoronavirusCasesFile))
                    {
                        File.Delete(CheckCoronavirusCasesFile);
                    }
                    try
                    {
                        //BackgroundWorker CheckCoronavirus = new BackgroundWorker();
                        //CheckCoronavirus.DoWork += async (o, args) =>
                        //{
                        //    using (var client = new WebClient())
                        //    {
                        //        client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        //            CheckCoronavirusCasesFile);
                        //        while (client.IsBusy)
                        //        {
                        //            await Task.Delay(10);
                        //        }
                        //    }
                        //};
                        //CheckCoronavirus.RunWorkerAsync();
                        //while (CheckCoronavirus.IsBusy)
                        //{
                        //    await Task.Delay(10);
                        //}

                        var ReadLine = 0;
                        string CoronavirusCases = "";
                        BackgroundWorker GetCases = new BackgroundWorker();
                        int i = 0;
                        GetCases.DoWork += async (o, args) =>
                        {
                            while (CoronavirusCases == "" || i > 10)
                            {
                                CoronavirusCases = await GetCoronavirusCases();
                                i++;
                            }
                            if (CheckedCorona == false)
                            {
                                OldCoronavirusCases = CoronavirusCases;
                                CheckedCorona = true;
                            }
                        };
                        GetCases.RunWorkerAsync();
                        while (GetCases.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                        CoronavirusCases = CoronavirusCases.Replace(",", "");
                        CoronavirusCases = CoronavirusCases.Replace(" ", "");


                        if (CheckedCorona == false)
                        {
                            CoronaCountDown.Start();
                            CheckedCorona = true;
                            if (CoronavirusCases == "")
                            {
                                CheckedCorona = false;
                            }
                            OldCoronavirusCases = CoronavirusCases;
                        }

                        if (OldCoronavirusCases == "")
                        {
                            OldCoronavirusCases = CoronavirusCases;
                        }

                        string Deaths = "";
                        BackgroundWorker GetCoronaDeaths = new BackgroundWorker();
                        GetCoronaDeaths.DoWork += async (o, args) =>
                        {
                            Deaths = await GetDeathCoronavirusCases();
                        };
                        GetCoronaDeaths.RunWorkerAsync();
                        var TimeOutDeaths = 30;
                        while (GetCoronaDeaths.IsBusy && TimeOutDeaths > 0)
                        {
                            await Task.Delay(1000);
                            TimeOutDeaths = TimeOutDeaths - 1;
                        }
                        if (CheckedDeaths == false)
                        {
                            CheckedDeaths = true;
                            if (Deaths == "")
                            {
                                CheckedDeaths = false;
                            }
                            OldDeaths = Deaths;
                        }

                        try
                        {
                            if (Int32.Parse(CoronavirusCases) > Int32.Parse(OldCoronavirusCases) + 10000)
                            {
                                CoronaCountDown.Stop();
                                string CoronaElapsed = CoronaCountDown.ElapsedMilliseconds.ToString();
                                CoronaCountDown.Reset();

                                string ElapsedTime()
                                {
                                    string Return = "";
                                    Double ToSeconds = Int32.Parse(CoronaElapsed) / 1000d;
                                    Double ToMinutes = ToSeconds / 60d;
                                    Double ToHours = ToMinutes / 60d;
                                    if (ToSeconds < 60)
                                    {
                                        Return = ToSeconds.ToString("0.00") + " second(s)";
                                    }
                                    else if (ToSeconds > 59 && ToMinutes < 60)
                                    {
                                        Return = ToMinutes.ToString("0.00") + " minute(s)";
                                    }
                                    else if (ToMinutes > 59)
                                    {
                                        Return = ToHours.ToString("0.00") + " hour(s)";
                                    }

                                    return Return;
                                }

                                string DisplayCorona = OldCoronavirusCases;
                                DisplayCorona = OldCoronavirusCases;
                                OldCoronavirusCases = CoronavirusCases;
                                await PlaySoundSync(Resources.Alarm);
                                await PopupWithSound("Additional 10000 cases\nof coronavirus worldwide!", "Time elapsed: " + ElapsedTime() + " was elapsed during the time\nthat more than 10000 infections of coronavirus\nCurrent Cases: " + CoronavirusCases + "\nLast time checked cases: " + DisplayCorona,
                                    Resources.Attention10Thousand);
                                if (Int32.Parse(Deaths) > Int32.Parse(OldDeaths) + 1000)
                                {
                                    await PlaySoundSync(Resources.CodeBlack);
                                    OldDeaths = Deaths;
                                    await PopupWithSound("Additional 1000 deaths of coronavirus",
                                        "An additional 1000 deaths was added to the total number\nof deaths caused by the coronavirus worldwide\nCurrent Deaths: " +
                                        Deaths, Resources.Attention1000Deaths);
                                }
                                CoronaCountDown.Start();
                            }
                        }
                        catch (Exception)
                        {
                            Console.WriteLine("Coronavirus Error");
                        }
                        Console.WriteLine("Current Cases: " + CoronavirusCases + " Old Cases: " + OldCoronavirusCases);
                        Console.WriteLine("Coronavirus Deaths: " + Deaths + " Old Deaths: " + OldDeaths);
                        string Vaccines = "";
                        BackgroundWorker GetVaccinesString = new BackgroundWorker();
                        GetVaccinesString.DoWork += async (o, args) =>
                        {
                            try
                            {
                                Vaccines = await GetVaccineCoronavirusCases();
                            }
                            catch
                            {

                            }
                        };
                        var TimeOut = 30;
                        GetVaccinesString.RunWorkerAsync();
                        while (GetVaccinesString.IsBusy && TimeOut > 0)
                        {
                            await Task.Delay(1000);
                            TimeOut = TimeOut - 1;
                        }

                        if (TimeOut < 1)
                        {
                            Vaccines = "0";
                        }

                        Vaccines = Vaccines.Replace(" ", "");
                        TimeOut = 30;
                        if (CheckedVaccines == false)
                        {
                            OldVaccines = Vaccines;
                            CheckedVaccines = true;
                        }
                        Console.WriteLine("Current Vaccines: " + Vaccines + " Old Vaccines: " + OldVaccines);
                        try
                        {
                            if (Int32.Parse(Vaccines) > Int32.Parse(OldVaccines))
                            {
                                OldVaccines = Vaccines;
                                await PopupWithSound("New vaccine for coronavirus",
                                    "A new vaccine is being developed for the\ncoronavirus, check your news for details\nCurrent Vaccines In Development: " +
                                    await GetVaccineCoronavirusCases(),
                                    Resources.NewVaccine);
                            }
                        }
                        catch
                        {
                            Console.WriteLine("VACCINE ERROR");
                        }

                        string CoolDown = "Microsoft NCSI";

                        BackgroundWorker Checker = new BackgroundWorker();
                        using (var client = new WebClient())
                        {
                            Checker.DoWork += (o, args) =>
                            {
                                try
                                {
                                    CoolDown = client.DownloadString("http://www.msftncsi.com/ncsi.txt");
                                }
                                catch
                                {
                                    CoolDown = "";
                                }
                            };

                        }
                        var u = 30;
                        Console.WriteLine("Entering 30 second countdown...");
                        while (u > 0 && CoolDown == "Microsoft NCSI")
                        {
                            u = u - 1;
                            await Task.Delay(1000);
                            Checker.RunWorkerAsync();
                            while (Checker.IsBusy)
                            {
                                await Task.Delay(10);
                            }
                        }
                        Console.WriteLine("30 second cooldown completed");
                    }
                    catch (Exception dewhui)
                    {
                        Console.WriteLine(dewhui);
                        await PopupInternetFailure(dewhui.ToString());
                    }
                }
                catch (Exception dew)
                {
                    try
                    {
                        await Email.SendEmail("cntowergun@gmail.com", "Monitoring Tool Exception",
                            "Open to see exception status", dew.ToString());
                        ///CONTINUE REASON/// ...
                        Console.WriteLine(dew);
                        File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\MonitoringErrorLog.txt", dew.ToString());
                        CloseButton.Enabled = true;
                        await PopupInternetFailure(dew.ToString());
                        if (SpeedTestShown == false)
                        {
                            SpeedTestShown = true;
                            InternetCheck ddew = new InternetCheck();
                            ddew.ShowDialog();
                            SpeedTestShown = false;
                        }
                    }
                    catch (Exception jer)
                    {
                        Console.WriteLine(jer);
                        TopMost = false;
                        Visible = false;
                        Closed = true;
                    }
                }
            }
        }

        private bool CheckedVaccines = false;
        private string OldVaccines = "";
        private bool CheckedDeaths = false;
        private string OldDeaths = "";

        public static IPAddress GetDefaultGateway()
        {
            return NetworkInterface
                .GetAllNetworkInterfaces()
                .Where(n => n.OperationalStatus == OperationalStatus.Up)
                .Where(n => n.NetworkInterfaceType != NetworkInterfaceType.Loopback)
                .SelectMany(n => n.GetIPProperties()?.GatewayAddresses)
                .Select(g => g?.Address)
                .Where(a => a != null)
                .Where(a => a.AddressFamily == AddressFamily.InterNetwork)
                .Where(a => Array.FindIndex(a.GetAddressBytes(), b => b != 0) >= 0)
                .FirstOrDefault();
        }

        private async Task PopupInternetFailure(string Error)
        {
            bool Netgeared = false;
            if (NotNetGear == false)
            {
                string NetGearedDetector = "";
                try
                {
                    using (var client = new WebClient())
                    {
                        NetGearedDetector =
                            await client.DownloadStringTaskAsync(
                                new Uri("http://192.168.1.1/MNU_access_setRecovery_index.htm"));
                    }
                }
                catch
                {
                    Netgeared = true;
                }

                if (NetGearedDetector == "")
                {
                    Netgeared = true;
                }

                if (NetGearedDetector.Contains("\"MNU_access_unauthorized_checkSerial.htm\""))
                {
                    Netgeared = false;
                }
            }

            if (Netgeared == true)
            {
                IsNetgear = true;
                await ShowNotification("NETGEARED", "Your router is netgeared");
                ErrorMessageLabel.BackColor = Color.Red;
                ErrorMessageLabel.ForeColor = Color.Black;
                await PopupWithSound("ROUTER NETGEARED",
                    "Please ask your brother to fix the netgear router,if not available\nplease manually restart the netgear router, thank you.",
                    Resources.InternetMayNotBeAvailable);
                while (Closed == false)
                {
                    await Task.Delay(10);
                }
                ErrorMessageLabel.ForeColor = Color.Black;
                ErrorMessageLabel.BackColor = Color.Transparent;
                IsNetgear = false;
            }
            else
            {
                await PopupWithSound("Internet may not be available",
                    "Your internet may be down, please call your internet service\nprovider if you still do not have internet\nyour internet service provider is ROGERS",
                    Resources.InternetMayNotBeAvailable);
                while (Closed == false)
                {
                    await Task.Delay(10);
                }
            }
            if (SpeedTestShown == false)
            {
                SpeedTestShown = true;
                InternetCheck ddew = new InternetCheck();
                ddew.ShowDialog();
                SpeedTestShown = false;
            }

            string NetGeared()
            {
                string Return = "";
                if (Netgeared == true)
                {
                    Return = "NETGEARED";
                }
                else
                {
                    Return = "ROGERSED";
                }

                return Return;
            }
            Random EmailFiller = new Random();
            string PingerNumber = "##" + EmailFiller.Next(111, 999).ToString() + "##";
            await Email.SendEmail("kelvin01570@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("cntowergun@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("anthonyk102@hotmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));
            await Email.SendEmail("wendy09387@gmail.com", "INTERNET DOWN " + PingerNumber, "Reason for internet failure",
                "Reason: " + NetGeared() + "\nTime happened:\n" + DateTime.Now.Hour.ToString() + ":" +
                DateTime.Now.Minute.ToString() + "\nDate:" + DateTime.Now.Month.ToString() + "/" +
                DateTime.Now.Day.ToString() + "/" + DateTime.Now.Year.ToString() + "\nSPEEDTEST RESULTS:\n" + File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt"));

            await Email.SendEmail("cntowergun@gmail.com", "Error that caused popup " + PingerNumber, "Reason that the popup fired", Error);
        }
        TextFile Email = new TextFile();

        private string GetRandomNumber()
        {
            Random d = new Random();
            return d.Next(111, 999).ToString();
        }
        private async Task<string> GetRecoveredCoronavirusCases()
        {
            string CoronavirusCases = "";
            try
            {
                string RandomNumber = GetRandomNumber();
                string CheckCoronavirusCasesFile =
                        Environment.GetEnvironmentVariable("TEMP") + "\\GetRecoveredCoronavirusCases" + RandomNumber + ".txt";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        CheckCoronavirusCasesFile);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
                var ReadLine = 0;

                foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
                {
                    if (readLine.Contains("Total Recovered"))
                    {
                        CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                            .ElementAtOrDefault(ReadLine - 3);
                    }

                    ReadLine++;
                }

                CoronavirusCases = CoronavirusCases.Replace(",", "");
                CoronavirusCases = CoronavirusCases.Replace(" ", "");
                File.Delete(CheckCoronavirusCasesFile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CoronavirusCases;
        }

        private async Task<string> GetActiveCoronavirusCases()
        {
            string CoronavirusCases = "";
            try
            {
                string randomnumber = GetRandomNumber();
                string CheckCoronavirusCasesFile =
                        Environment.GetEnvironmentVariable("TEMP") + "\\GetActiveCoronavirusCases" + randomnumber + ".txt";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        CheckCoronavirusCasesFile);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
                var ReadLine = 0;

                foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
                {
                    if (readLine.Contains("Total Active"))
                    {
                        CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                            .ElementAtOrDefault(ReadLine - 3);
                    }

                    ReadLine++;
                }

                CoronavirusCases = CoronavirusCases.Replace(",", "");
                CoronavirusCases = CoronavirusCases.Replace(" ", "");
                File.Delete(CheckCoronavirusCasesFile);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CoronavirusCases;
        }

        private async Task<string> GetCriticalCoronavirusCases()
        {
            string RandomNumber = GetRandomNumber();
            string CoronavirusCases = "";
            try
            {
                string CheckCoronavirusCasesFile =
                        Environment.GetEnvironmentVariable("TEMP") + "\\GetCriticalCoronavirusCases" + RandomNumber + ".txt";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        CheckCoronavirusCasesFile);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
                var ReadLine = 0;

                foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
                {
                    if (readLine.Contains("Total Critical"))
                    {
                        CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                            .ElementAtOrDefault(ReadLine - 3);
                    }

                    ReadLine++;
                }

                CoronavirusCases = CoronavirusCases.Replace(",", "");
                CoronavirusCases = CoronavirusCases.Replace(" ", "");
                File.Delete(CheckCoronavirusCasesFile);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return CoronavirusCases;
        }

        private async Task<string> GetDeathCoronavirusCases()
        {
            string RandomNumber = GetRandomNumber();
            string CoronavirusCases = "";
            try
            {
                string CheckCoronavirusCasesFile =
                    Environment.GetEnvironmentVariable("TEMP") + "\\GetDeathCoronavirusCases" + RandomNumber + ".txt";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        CheckCoronavirusCasesFile);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                var ReadLine = 0;
                foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
                {
                    if (readLine.Contains("Total Deceased"))
                    {
                        CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                            .ElementAtOrDefault(ReadLine - 3);
                    }

                    ReadLine++;
                }

                CoronavirusCases = CoronavirusCases.Replace(",", "");
                CoronavirusCases = CoronavirusCases.Replace(" ", "");
                File.Delete(CheckCoronavirusCasesFile);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                CoronavirusCases = "0";
                string NetGearCheck = "";
                Console.WriteLine("NETGEAR STATUS => " + NetGearCheck);
                CloseButton.Enabled = true;
                await PopupInternetFailure(e.ToString());

                await Task.Delay(6000);

                Closed = false;
                while (Closed == false)
                {
                    await Task.Delay(10);
                }
            }

            return CoronavirusCases;
        }

        private async Task<string> GetVaccineCoronavirusCases()
        {
            string RandomNumber = GetRandomNumber();
            string CoronavirusCases = "";
            try
            {
                string CheckCoronavirusCasesFile =
                    Environment.GetEnvironmentVariable("TEMP") + "\\GetVaccineCoronavirusCases" + RandomNumber + ".txt";
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                        CheckCoronavirusCasesFile);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                var ReadLine = 0;
                foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
                {
                    if (readLine.Contains("Total Vaccines In Development"))
                    {
                        CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                            .ElementAtOrDefault(ReadLine - 3);
                    }

                    ReadLine++;
                }

                CoronavirusCases = CoronavirusCases.Split('<')[0].Trim();
                CoronavirusCases = CoronavirusCases.Replace(" ", "");
                File.Delete(CheckCoronavirusCasesFile);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\MonitoringErrorLog.txt", e.ToString());
            }

            return CoronavirusCases;
        }

        private async Task<string> GetCoronavirusCases()
        {
            string RandomNumber = GetRandomNumber();
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThing" + RandomNumber + ".txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Confirmed"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);

            return CoronavirusCases;
        }

        private bool CoronaCountDownStarted = false;
        Stopwatch CoronaCountDown = new Stopwatch();
        private bool Continue = false;
        private bool SpeedTestShown = false;
        private async void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            try
            {
                Console.WriteLine("Locked");
                Continue = false;
                if (e.Error != null)
                {
                    CloseButton.Enabled = true;
                    await PopupInternetFailure("Could not download the internet check file");
                }

                FileInfo dew = new FileInfo(Environment.GetEnvironmentVariable("TEMP") + "\\InternetCheck.txt");
                string FileSize = dew.Length.ToString();
                if (FileSize == "14")
                {

                }
                else
                {
                    if (Int32.Parse(FileSize) > 0)
                    {
                        await PopupWithSound("Internet may not be available",
                            "Reason: Internet connection may be unstable", Resources.InternetMayNotBeAvailable);
                    }
                    else if (FileSize == "0")
                    {
                        await PopupWithSound("Internet connection is lost",
                            "Reason: Cannot connect to the internet\nCheck connection with your router or VPN",
                            Resources.LostInternetConnection);
                    }
                }

                Continue = true;
                Console.WriteLine("Continue");
            }
            catch (Exception d)
            {
                Console.WriteLine(d);
                Continue = true;
                Closed = false;
                TopMost = false;
                Visible = false;
            }
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

        private void OpenRouterSettingsLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://" + GetDefaultGateway().ToString());
        }
    }
}

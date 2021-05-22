using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coronavirus_API;
using Covid19API;
using Main_Tools_Remastered.Properties;
using MaterialDesignThemes.Wpf;
using QuickType;
using UsefulTools;
using BooleanToVisibilityConverter = MaterialDesignThemes.Wpf.Converters.BooleanToVisibilityConverter;

namespace Main_Tools_Remastered
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public class AbortableBackgroundWorker : BackgroundWorker
    {

        private Thread workerThread;

        protected override void OnDoWork(DoWorkEventArgs e)
        {
            workerThread = Thread.CurrentThread;
            try
            {
                base.OnDoWork(e);
            }
            catch (ThreadAbortException)
            {
                e.Cancel = true; //We must set Cancel property to true!
                Thread.ResetAbort(); //Prevents ThreadAbortException propagation
            }
        }


        public void Abort()
        {
            if (workerThread != null)
            {
                workerThread.Abort();
                workerThread = null;
            }
        }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            Deactivated += MainWindow_Deactivated;
            Closing += MainWindow_Closing;
            CloseButton.Click += CloseButton_Click;
        }

        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            // The Window was deactivated 
            Topmost = false; // set topmost false first
            Topmost = true; // then set topmost true again
        }

        private async void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            await ClosePopup();
        }

        MainFunctions Functions = new MainFunctions();
        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
        }
        bool OwnsNetGear = false;

        private async Task PlaySound(Stream location)
        {
            SoundPlayer d = new SoundPlayer(location);
            d.Play();
        }

        enum WeatherSound
        {
            Breezy,FogCloudy,Rain,Snow,Sunny,Thunderstorm,Windy
        }

        private async Task PlaySoundFromWeatherDirectory(string Sound)
        {
            if (File.Exists(WeatherSoundDirectory + "\\" + Sound))
            {
                string SoundD = WeatherSoundDirectory + "\\" + Sound;
                await Task.Factory.StartNew(() =>
                {
                    new SoundPlayer(SoundD).PlaySync();
                });
            }
        }
        string WeatherSoundDirectory = Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\WeatherSounds";
        private async Task PlayWeatherSound(WeatherSound Sound)
        {
            TimeSpan start = new TimeSpan(8, 0, 0); //10 o'clock
            TimeSpan end = new TimeSpan(23, 0, 0); //12 o'clock
            TimeSpan now = DateTime.Now.TimeOfDay;

            if ((now > start) && (now < end))
            {
                if (!Directory.Exists(WeatherSoundDirectory))
                {
                    Directory.CreateDirectory(WeatherSoundDirectory);
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(
                            new Uri(
                                "https://raw.githubusercontent.com/EpicGamesGun/Monitoring-Tools/master/Bixby%20Alarm.zip"),
                            Environment.GetEnvironmentVariable("TEMP") + "\\WeatherSounds.Zip");
                        while (client.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }

                    ZipFile.ExtractToDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\WeatherSounds.Zip",
                        WeatherSoundDirectory);
                }
                if (Sound == WeatherSound.Breezy)
                {
                    await PlaySoundFromWeatherDirectory("default_sound.wav");
                }
                else if (Sound == WeatherSound.FogCloudy)
                {
                    await PlaySoundFromWeatherDirectory("fog_cloudy.wav");
                }
                else if (Sound == WeatherSound.Rain)
                {
                    await PlaySoundFromWeatherDirectory("rain.wav");
                }
                else if (Sound == WeatherSound.Snow)
                {
                    await PlaySoundFromWeatherDirectory("snow.wav");
                }
                else if (Sound == WeatherSound.Sunny)
                {
                    await PlaySoundFromWeatherDirectory("sunny_clear_hot_warm.wav");
                }
                else if (Sound == WeatherSound.Thunderstorm)
                {
                    await PlaySoundFromWeatherDirectory("thunder.wav");
                }
                else if (Sound == WeatherSound.Windy)
                {
                    await PlaySoundFromWeatherDirectory("windy_cold.wav");
                }
            }
        }
        Covid19 Covid;
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ShowInTaskbar = false;
            Visibility = Visibility.Hidden;
            SpeedTestGrid.Visibility = Visibility.Hidden;
            Topmost = true;
            while (await Functions.CheckInternet() == false)
            {
                await Task.Delay(10);
            }
            string OldCoronaCases = await Coronavirus.GetCoronavirusCases();
            string OldCoronaDeaths = await Coronavirus.GetDeathCoronavirusCases();
            string OldCoronaVaccines = await Coronavirus.GetVaccineCoronavirusCases();
            //MarkhamWeather MarkhamWeatherInfo = new MarkhamWeather();
            //string OldWeather = await MarkhamWeatherInfo.GetWeather();
            //string NewWeather = OldWeather;
            //Console.WriteLine("Weather: " + NewWeather);
            Console.WriteLine("Coronavirus cases: " + OldCoronaCases + " , Deaths: " + OldCoronaDeaths + " , Vaccines: " + OldCoronaVaccines);
            string OldIP = await Functions.CheckIP();
            Console.WriteLine("Current IP: " + OldIP);
            if (await Functions.CheckNetGear() == true)
            {
                Console.WriteLine("TP-Link router detected");
                OtherFunctions.ShowNotification("TP-Link Router", "A TP-Link router has been detected");
                NetGearCheckBox.IsChecked = true;
                OwnsNetGear = true;
                await Functions.PlaySoundSync(Properties.Resources.TPLinkRouterDetected);
            }
            Console.WriteLine("--PASSED BORDER--\nProgram now starting");
            Stopwatch CoronaElapse = new Stopwatch();
            CoronaElapse.Start();
            //OldWeather = "";
            while (true)
            {
                try
                {
                    //Coronavirus_API.COVID covid = new COVID();
                    //covid.RemoveComma = true;
                    //covid.RealTimeCheck = true;
                    await Task.Delay(100);
                    if (OwnsNetGear == false)
                    {
                        if (await Functions.CheckInternet() == false)
                        {
                            PassError = "Rogersed, blame ISP or your internetn't";
                            OtherFunctions.ShowNotification("ROGERSED", "Your internet may not be available");
                            await PopupSound("Internet may not be available", "Your internet might not be working\nCheck router settings, windows settings\nOr you may have configured wrong wifi settings.", Properties.Resources.InternetMayNotBeAvailable);
                            await DoSpeedTest(false);
                        }
                    }
                    else
                    {
                        if (await Functions.CheckInternet() == false && await Functions.CheckNetGear() == true)
                        {
                            PassError = "Rogersed, blame ISP or your internetn't";
                            OtherFunctions.ShowNotification("ROGERSED", "Your internet may not be available");
                            await PopupSound("Internet may not be available", "Your internet might not be working\nCheck router settings, windows settings\nOr you may have configured wrong wifi settings.", Properties.Resources.InternetMayNotBeAvailable);
                            await DoSpeedTest(false);
                        }
                        else if (await Functions.CheckInternet() == false && await Functions.CheckNetGear() == false)
                        {
                            PassError = "Router is netgeared";
                            OtherFunctions.ShowNotification("ROUTER TP-LINKED", "Please restart TP-Link router, it will only take 30 seconds");
                            await NetGearedPopup();
                        }
                    }
                    Covid = Covid19.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/worldwide/cases.txt"));
                    string NewCoronaCases = Covid.ConfirmedCases.Replace(",","");
                    Console.WriteLine("Current COVID Cases WorldWide: " + NewCoronaCases + ", Old cases: " + OldCoronaCases);
                    if (Int32.Parse(OldCoronaCases) + 10000 < Int32.Parse(NewCoronaCases)) 
                    {
                        await OtherFunctions.ShowNotification("COVID-19 Cases Reported", "More than 10000 cases of COVID-19 has been reported!");
                        await Functions.PlaySoundSync(Properties.Resources.Alarm);
                        int Difference = Int32.Parse(NewCoronaCases) - Int32.Parse(OldCoronaCases);
                        int OneHundredMillion = 200000000;
                        var EstimatedDays = new DateTime();
                        EstimatedDays = DateTime.Now;
                        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseDifference.txt",Difference.ToString("0"));
                        int CoronaElapseMinutes = CoronaElapse.Elapsed.Minutes;
                        int CoronaElapseHours = CoronaElapse.Elapsed.Hours;
                        CoronaElapse.Stop();
                        CoronaElapse.Reset();
                        File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseElapsedTime.txt",CoronaElapseMinutes.ToString());
                        int RemainingCases = OneHundredMillion - Int32.Parse(NewCoronaCases);
                        double HowManyTimesBeforeThat = RemainingCases / Difference;
                        double TheMinutesBeforeThat = HowManyTimesBeforeThat * CoronaElapseMinutes;
                        EstimatedDays = EstimatedDays.AddMinutes(TheMinutesBeforeThat);
                        Console.WriteLine(EstimatedDays.ToString("g"));
                        await PopupSound("Coronavirus cases reported", "More than 10000 cases of COVID-19 cases was reported\nOld cases: " + OldCoronaCases + "\nNew cases: " + NewCoronaCases + "\nDifference: " + Difference + "\n\nEstimated Date When It Is Two Hundred Million Cases: " + EstimatedDays.ToString("g") + "\nElapsed: " + CoronaElapseHours + " hours and " + CoronaElapseMinutes + " minutes", Properties.Resources.Stylish_10Thousand);
                        OldCoronaCases = NewCoronaCases;
                        string NewCoronaDeaths = Covid.Deaths.Replace(",", "");
                        CoronaElapse.Start();
                        if (Int32.Parse(OldCoronaDeaths) + 1000 < Int32.Parse(NewCoronaDeaths))
                        {
                            OtherFunctions.ShowNotification("COVID-19 Cases Reported", "More than 1000 cases of COVID-19 deaths has been reported!");
                            await Functions.PlaySoundSync(Properties.Resources.CodeBlack);
                            await PopupSound("Coronavirus deaths reported", "More than 1000 cases of COVID-19 deaths have been reported.\nOld deaths: " + OldCoronaDeaths + "\nNew deaths: " + NewCoronaDeaths + "\nDifference: " + (Int32.Parse(NewCoronaDeaths) - Int32.Parse(OldCoronaDeaths)).ToString("0"), Properties.Resources.Stylish_1000Thousand);
                            OldCoronaDeaths = NewCoronaDeaths;
                            //string NewCoronaVaccines = await Coronavirus.GetVaccineCoronavirusCases();
                            //if (Int32.Parse(OldCoronaVaccines) < Int32.Parse(NewCoronaVaccines))
                            //{
                            //    OtherFunctions.ShowNotification("COVID-19 Vaccine", "A new vaccine is being developed!");
                            //    await Functions.PlaySoundSync(Properties.Resources.SuccessSoundEffect);
                            //    await PopupSound("Coronavirus vaccine research", "1 or more coronavirus vaccine is now being researched!\nOld research: " + OldCoronaVaccines + "\nNew research: " + NewCoronaVaccines + "\nDifference: " + (Int32.Parse(NewCoronaVaccines) - Int32.Parse(OldCoronaVaccines)).ToString("0"), Properties.Resources.NewVaccine);
                            //}
                        }
                    }
                    string NewIP = await Functions.CheckIP();
                    if (OldIP != NewIP && NewIP != "")
                    {
                        await PopupSound("Public IP address changed", "Your public IP address has change, due to connection to other network and/or VPN\n\nOld IP: " + OldIP + "\n\nNew IP: " + NewIP, Properties.Resources.NewIPChanged);
                        OldIP = NewIP;
                        await DoSpeedTest(false);
                    }



                    // Weather popups //
                    //bool CheckContain(string OriginalString, string StringToCheck)
                    //{
                    //    CultureInfo culture = new CultureInfo("en-US", false);
                    //    return culture.CompareInfo.IndexOf(OriginalString, StringToCheck, CompareOptions.IgnoreCase) >= 0;
                    //}
                    //AbortableBackgroundWorker FirstInfo = new AbortableBackgroundWorker();
                    //FirstInfo.DoWork += async (o, args) =>
                    //{
                    //    NewWeather = await MarkhamWeatherInfo.GetWeather();
                    //};
                    //FirstInfo.RunWorkerAsync();
                    //while (FirstInfo.IsBusy)
                    //{
                    //    await Task.Delay(10);
                    //    if (await Functions.CheckInternet() == false)
                    //    {
                    //        FirstInfo.Abort();
                    //        FirstInfo.Dispose();
                    //        break;
                    //    }
                    //}
                    //Console.WriteLine("Current Weather: " + NewWeather + "\nOld Weather: " + OldWeather);
                    //if (OldWeather != NewWeather)
                    //{
                    //    OldWeather = NewWeather;
                    //    Console.WriteLine("WEATHER CHANGED");
                    //    string WeatherCondition = OldWeather;
                    //    bool CheckWeatherCondition(string WeatherInfo)
                    //    {
                    //        return CheckContain(WeatherCondition,WeatherInfo);
                    //    }
                       
                    //    if (await Functions.CheckInternet() == true)
                    //    {
                    //        Temperatures WeatherJSON = new Temperatures(); 
                    //        AbortableBackgroundWorker GetInfo = new AbortableBackgroundWorker();
                    //        GetInfo.DoWork += (o, args) =>
                    //        {
                    //            WeatherJSON = QuickType.Temperatures.FromJson(MarkhamWeatherInfo.GetWeatherFromJSON());
                    //        };
                    //        GetInfo.RunWorkerAsync();
                    //        while (GetInfo.IsBusy)
                    //        {
                    //            await Task.Delay(10);
                    //            if (await Functions.CheckInternet() == false)
                    //            {
                    //                Console.WriteLine("ABORTED");
                    //                GetInfo.Abort();
                    //                GetInfo.Dispose();
                    //                break;
                    //            }
                    //        }
                    //        string Tempurature = "";
                    //        foreach (Datum datum in WeatherJSON.Data)
                    //        {
                    //            Tempurature = datum.Temp.ToString();
                    //        }
                    //        OtherFunctions.ShowNotification("WEATHER CHANGED",
                    //            "Weather has been changed to: " + OldWeather + "\nTempurature: " + Tempurature);
                    //        if (CheckWeatherCondition("Thunderstorm"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Thunderstorm);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.ThunderStorm);
                    //        }
                    //        else if (CheckWeatherCondition("Drizzle") || CheckWeatherCondition("Rain"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Rain);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.Rain);
                    //        }
                    //        else if (CheckWeatherCondition("Snow") || CheckWeatherCondition("Sleet") ||
                    //                 CheckWeatherCondition("Flurries"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Snow);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.Snow);
                    //        }
                    //        else if (CheckWeatherCondition("Mist") || CheckWeatherCondition("Smoke") ||
                    //                 CheckWeatherCondition("Haze") || CheckWeatherCondition("Fog") ||
                    //                 CheckWeatherCondition("Sand"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Breezy);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.Cloudy);
                    //        }
                    //        else if (CheckWeatherCondition("Clear Sky"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Sunny);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.ClearSky);
                    //        }
                    //        else if (CheckWeatherCondition("Cloud"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.FogCloudy);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.Cloudy);
                    //        }
                    //        else if (CheckWeatherCondition("Unknown Precipitation"))
                    //        {
                    //            await PlayWeatherSound(WeatherSound.Rain);
                    //            await PopupSound("WEATHER CHANGED",
                    //                "Weather has been changed to " + NewWeather + "\nTempurature: " + Tempurature,
                    //                Properties.Resources.Rain);
                    //        }
                    //    }
                    //}

                    // 30 Seconds cooldown //
                    Console.WriteLine("Waiting for 30 seconds cooldown");
                    for (int i = 0; i < 1; i++)
                    {
                        await Task.Delay(1000);
                        if (await Functions.CheckInternet() == false || OwnsNetGear == true && await Functions.CheckNetGear() == false)
                        {
                            break;
                        }
                    }
                    Console.WriteLine("Countdown passed");
                }
                catch (Exception ex)
                {
                    PassError = ex.ToString();
                    await PopupSound("Internet may not be available", "ROGERSED, or program error, program cannot connect to internet.", Properties.Resources.InternetMayNotBeAvailable);
                    await DoSpeedTest(false);
                    Console.WriteLine(ex);
                }
            }
        }
        bool ClosedPopup = false;

        private async Task NetGearedPopup()
        {
            await Functions.PlaySoundSync(Properties.Resources.Outage);
            await Functions.PlaySoundSync(Properties.Resources.TPLinked);
            ErrorLabel.Background = Brushes.Red;
            int TimeOut = 0;
            Topmost = true;
            Visibility = Visibility.Visible;
            ErrorLabel.Content = "TP-LINKED";
            ReasonLabel.Content = "TP-LINKED";
            ErrorLabel.Background = Brushes.Red;
            ReasonLabel.Background = Brushes.Red;
            CloseButton.IsEnabled = false;
            Topmost = true;
            while (await Functions.CheckNetGear() == false || TimeOut == 120)
            {
                await Task.Delay(1000);
                TimeOut++;
            }
            ErrorLabel.Background = Brushes.Transparent;
            ReasonLabel.Background = Brushes.Transparent;
            await Functions.PlaySoundSync(Properties.Resources.Restore);
            CloseButton.IsEnabled = true;
            await ShowSnackBar("Please fix TP-Link router");
            await PopupSound("ROUTER TP-LINKED", "Your router is \"TP-LINKED\", please ask your brother to fix it,\n or please be patient while the tp-link router restarts.", Properties.Resources.InternetMayNotBeAvailable);
            ErrorLabel.Background = Brushes.Transparent;
            if (NetGearCheckBox.IsChecked == false)
            {
                OwnsNetGear = false;
            }
            await DoSpeedTest(true);
        }
        bool GamingMode = false;
        private async Task PopupSound(string ErrorTitle, string ReasonMessage, Stream Sound)
        {
            if(GamingModeCheckBox.IsChecked == true)
            {
                GamingMode = true;
            }
            else
            {
                GamingMode = false;
            }
            SpeedTestGrid.Visibility = Visibility.Hidden;
            MainPopupGrid.Visibility = Visibility.Visible;
            AbortCountDown = false;
            ClosedPopup = false;
            SoundPlayer Player = new SoundPlayer(Sound);
            ErrorLabel.Content = ErrorTitle;
            ReasonLabel.Content = ReasonMessage;
            Topmost = true;
            Player.Play();
            if (GamingMode == false)
            {
                Visibility = Visibility.Visible;
                Topmost = true;
                await AutoClosePopup(30); 
            }
            else
            {
                await AutoClosePopup(10);
                if(File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\New Text Document.txt"))
                {
                    File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\New Text Document.txt");
                    GamingMode = false;
                    GamingModeCheckBox.IsChecked = false;
                }
            }
            while (ClosedPopup == false)
            {
                await Task.Delay(10);
            }
        }

        string PassError = "";
        private async Task AutoClosePopup(int Seconds)
        {
            for (int i = Seconds; i > 0; i--)
            {
                CloseButton.Content = i.ToString();
                await Task.Delay(1000);
                if (AbortCountDown == true)
                {
                    AbortCountDown = false;
                    break;
                }
            }
            CloseButton.Content = "0";
            await ClosePopup();
        }
        bool AbortCountDown = false;
        private async Task ClosePopup()
        {
            SoundPlayer h = new SoundPlayer(Properties.Resources.Click);
            h.Play();
            AbortCountDown = true;
            Topmost = true;
            CloseButton.IsEnabled = false;
            CloseButton.Content = "Wait..";
            await ShowSnackBar("Connecting to internet, please wait...");
            ErrorLabel.Content = "Waiting for internet connection";
            ReasonLabel.Content = "Please wait for an internet connection before this popup will\nclose, reaction time is near instant as soon as internet is restored.";
            BackgroundWorker ContactTimer = new BackgroundWorker();
            ContactTimer.WorkerSupportsCancellation = true;
            bool FireContact = false;
            bool FiredContact = false;
            ContactTimer.DoWork += async (sender, args) =>
            {
                await Task.Delay(15000);
                FireContact = true;
            };
            ContactTimer.RunWorkerAsync();
            while (await Functions.CheckInternet() == false)
            {
                if (FireContact == true && FiredContact == false)
                {
                    FiredContact = true;
                    await Functions.PleaseContactSound();
                }
                await Task.Delay(10);
            }
            await ShowSnackBar("Internet has been restored");
            await Task.Delay(1000);
            CloseButton.Content = "Close";
            CloseButton.IsEnabled = true;
            Visibility = Visibility.Hidden;
            await OtherFunctions.ShowNotification("Internet connection has been restored","You can now use the internet");
            SoundPlayer Player = new SoundPlayer(Properties.Resources.InternetRestored_SP);
            Player.Play();
            ClosedPopup = true;
        }
        SnackbarMessageQueue Queue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));

        private async Task ShowSnackBar(string Message)
        {
            UniversalSnackbar.MessageQueue = Queue;
            Queue.Enqueue(Message);
        }

        private async Task ShowSnackBar(string Message, string ButtonContent, Action Button)
        {
            UniversalSnackbar.MessageQueue = Queue;
            Queue.Enqueue(Message, ButtonContent, Button);
        }

        private void SpeedTestCloseButton_Click(object sender, RoutedEventArgs e)
        {
            SpeedTestGrid.Visibility = Visibility.Hidden;
            MainPopupGrid.Visibility = Visibility.Visible;
            Visibility = Visibility.Hidden;
        }

        string SpeedTestURL = "";
        private void SpeedTestResultButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Process.Start(SpeedTestURL.Replace(" ", ""));
            }
            catch 
            {

            }
        }

        private async Task DoSpeedTest(bool NetGeared_Status)
        {
            SpeedTest Speed = new SpeedTest();
            Speed.Error = PassError;
            Speed.NetGearedSet = NetGeared_Status;
            await Speed.Start();
            SpeedTestGrid.Visibility = Visibility.Visible;
            MainPopupGrid.Visibility = Visibility.Hidden;
            Visibility = Visibility.Visible;
            Topmost = true;
            DownloadLabel.Content = "DOWNLOAD: " + Speed.DownloadSpeed;
            UploadLabel.Content = "UPLOAD: " + Speed.UploadSpeed;
            PingLabel.Content = "PING: " + Speed.Ping;
            ISPLabel.Content = "Internet service provider: " + Speed.InternetServiceProvider;
            SpeedTestURL = Speed.ResultURL;
            if(GamingMode == true)
            {
                Visibility = Visibility.Hidden;
            }
        }
    }
}

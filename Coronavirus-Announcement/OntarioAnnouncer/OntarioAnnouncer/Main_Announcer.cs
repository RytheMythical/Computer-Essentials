using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Coronavirus_API;
using DemographicsAPI;
using Engine;
using HospitalAPI;
using Ontario_Coronavirus_API;
using OntarioAnnouncer.Properties;
using OntarioCOVIDAPI;
using OntarioVaccineAPI;
using ScreenShotDemo;
using UsefulTools;

namespace OntarioAnnouncer
{
    public partial class Main_Announcer : Form
    {
        public Main_Announcer()
        {
            InitializeComponent();
            FormClosing += Main_Announcer_FormClosing;
        }

        private bool Closing = false;
        private async void Main_Announcer_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Closing == false)
            {
                e.Cancel = true;
            }
        }

        bool Reported = false;
        private string ReportedDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\ReportedOntarioCases";
        private string HospitalizedDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioHospital";
        private string CasesDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases";
        private string ScreenShotDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCasesScreenShots";
        bool Jahcord
        {
            get
            {
                return File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Jahcord.txt") && File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ServerComputer.txt") && File.Exists(@"C:\Users\Administrator\Desktop\COVID\ontario.txt");
            }
        }
        string SpecialEditionFile = Environment.GetEnvironmentVariable("TEMP") + "\\SpecialEdition";


        string[] GetURLs
        {
            get
            {
                string TempPath = Path.Combine(Path.GetTempPath(),Path.GetRandomFileName());
                File.WriteAllBytes(TempPath,Resources.s1);
                DC.FileDecrypt(TempPath,TempPath + ".txt","s");
                List<string> ReturnList = new List<string>();
                foreach (string readLine in File.ReadLines(TempPath + ".txt"))
                {
                    ReturnList.Add(readLine);
                }
                return ReturnList.OfType<string>().ToArray();
            }
        }

        OntarioCovid CovidData;
        Demographics Demographics;
        Hospital Hospital;
        OntarioVaccine OntarioVaccine;
        private string COVIDDirectory = @"C:\Users\Administrator\Desktop\COVID";
        private async void Form1_Load(object sender, EventArgs e)
        {
            foreach (string urL in GetURLs)
            {
                Console.WriteLine(urL);
            }
            NewsTextBox.ReadOnly = true;
            Directory.CreateDirectory(ReportedDirectory);
            Directory.CreateDirectory(HospitalizedDirectory);
            Directory.CreateDirectory(CasesDirectory);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            TopMost = true;
            Visible = false;
            CloseButton.Enabled = false;
            CloseButton.Text = "Please wait";
            while (Reported == false)
            {
                Visible = false;
                await Task.Delay(10);
                if (DateTime.Now.Hour >= 11 && DateTime.Now.Minute <= 5 && !File.Exists(ReportedDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "Report") && File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\Ontario.txt") && DateTime.Now.Hour >= 11)
                {
                    Console.WriteLine("REPORTING CASES");
                    File.WriteAllText(ReportedDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "Report","HA");
                    Visible = false;
                    if (File.Exists(SpecialEditionFile))
                    {
                        File.Delete(SpecialEditionFile);
                    }
                    string JSONData = File.Exists(@"C:\Users\Administrator\Desktop\COVID\ontario.txt") ? File.ReadAllText(@"C:\Users\Administrator\Desktop\COVID\ontario.txt") : new WebClient().DownloadString("http://covid.bigheados.com/ontario.txt");
                    string DemographicsJson = File.Exists(@"C:\Users\Administrator\Desktop\COVID\demographics.txt") ? File.ReadAllText(@"C:\Users\Administrator\Desktop\COVID\demographics.txt") : new WebClient().DownloadString("http://covid.bigheados.com/demographics.txt");
                    string HospitalJson = File.Exists(@"C:\Users\Administrator\Desktop\COVID\hospital.txt") ? File.ReadAllText(@"C:\Users\Administrator\Desktop\COVID\hospital.txt") : new WebClient().DownloadString("http://covid.bigheados.com/hospital.txt");
                    CovidData = OntarioCovid.FromJson(JSONData);
                    Demographics = DemographicsAPI.Demographics.FromJson(DemographicsJson);
                    Hospital = Hospital.FromJson(HospitalJson);
                    OntarioVaccine = OntarioVaccine.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/ontariovaccine/common_postal_code/L3R4E8.txt"));
                    SpeechSynthesizer DewSpeak = new SpeechSynthesizer();
                    SoundPlayer ReportNewCases = new SoundPlayer(Resources.NewOntarioReported);
                    string Hospitalized = Hospital.OntarioHospitalized;
                    DeathLabel.Text += " " + CovidData.OntarioDeaths + " (rip)";
                    await AddLTCList("blah");


                    // General

                    AddStatsList(CovidData.OntarioTotalDeaths + " total deaths in ontario");
                    AddStatsList(CovidData.OntarioPendingTests + " pending tests in ontario");
                    AddStatsList(CovidData.OntarioYesterdayTest + " tests yesterday in ontario");
                    AddStatsList(CovidData.OntarioRecovered + " recovered cases");
                    AddStatsList(CovidData.OntarioTotalCases + " total confirmed cases");
                    AddStatsList(CovidData.OntarioNewRecoveries + " new recoveries today");

                    // Hospital

                    AddHospitalList(Hospitalized , "people hospitalized");
                    AddHospitalList(Hospital.OntarioIcu.ToString() , "Total patients in ICU due to COVID-related critical illness");
                    AddHospitalList(Hospital.IcuPositiveCases.ToString(), "Number of patients currently in ICU, testing positive for COVID");
                    AddHospitalList(Hospital.IcuNegativeCases.ToString(), "Number of patients currently in ICU due to COVID, no longer testing positive for COVID");
                    AddHospitalList(Hospital.IcuOnVentilator.ToString(), "Total patients in ICU on a ventilator due to COVID-related critical illness");
                    AddHospitalList(Hospital.IcuOnVentilatorPositiveCases.ToString(), "Number of patients currently in ICU on a ventilator with COVID-19");
                    AddHospitalList(Hospital.IcuOnVentilatorNegativeCases.ToString(), "Patients in ICU due to COVID on a ventilator, no longer testing positive for COVID");

                    // Demographics

                    DemographicsCasesListBox.Items.Add(
                        Demographics.Male + "     Male cases");
                    DemographicsCasesListBox.Items.Add(Demographics.Female +
                                                       "     Female cases");
                    DemographicsCasesListBox.Items.Add(Demographics.NineteenAndUnder +
                                                       "     19 and under cases");
                    DemographicsCasesListBox.Items.Add(Demographics.TwentytoThirtyNine +
                                                       "     20-39 age cases");
                    DemographicsCasesListBox.Items.Add(Demographics.FourtyToFiftyNine +
                                                       "     40-59 age cases");
                    DemographicsCasesListBox.Items.Add(Demographics.SixtyToSeventyNine +
                                                       "     60-79 age cases");
                    DemographicsCasesListBox.Items.Add(Demographics.EightyAndOver +
                                                       "     80 and over cases");
                    string NewCases = CovidData.OntarioCasesToday;
                    

                    // Other
                    string OntarioCasesToday = NewCases;
                    string CleanYesterday = CovidData.OntarioYesterdayTest;
                    int CleanOntarioTestsYesterday = Int32.Parse(CleanYesterday.Replace(",", "").Replace(" ", ""));
                    int CleanOntarioCasesToday = Int32.Parse(OntarioCasesToday.Replace(",", ""));
                    string CleanTests = CovidData.OntarioPendingTests;
                    CleanTests = CleanTests.Replace(",", "").Replace(" ", "");
                    int CleanOntarioPendingTests = Int32.Parse(CleanTests);
                    Console.WriteLine("Clean ontario cases today: " + CleanOntarioCasesToday + " Clean ontario tests yesterday: " + CleanOntarioTestsYesterday);
                    double YesterdayCasesPercentage = (double)CleanOntarioCasesToday / (double)CleanOntarioTestsYesterday;
                    Console.WriteLine("Calculating: " + YesterdayCasesPercentage);
                    YesterdayCasesPercentage = YesterdayCasesPercentage * 100;
                    Console.WriteLine("Calculated case to test: " + YesterdayCasesPercentage);
                    double CalculationTomorrowPercentage = (double)CleanOntarioPendingTests * (double)YesterdayCasesPercentage / 100d;
                    Console.WriteLine(CalculationTomorrowPercentage);
                    Console.WriteLine("ESTIMATED CASES TOMORROW: " + CalculationTomorrowPercentage.ToString("0"));
                    EstimateLabel.Text += " " + CalculationTomorrowPercentage.ToString("0");
                    string News = CovidData.OntarioReportingErrorNews;
                    string DeathNews = CovidData.OntarioDeathError;
                    string HospitalNews = CovidData.OntarioHospitalErrorNews;
                    if (News != "null")
                    {
                        NewsTextBox.Text += "\n" + News;
                    }

                    if (DeathNews != "null")
                    {
                        NewsTextBox.Text += "\n" + DeathNews;
                    }
                    if(HospitalNews != "null")
                    {
                        NewsTextBox.Text += "\n" + HospitalNews;
                    }
                    NewsTextBox.Text += "\nDoorDash New Customer Coupon Code: GOBIG50";
                    double PositiveRate = CovidData.OntarioPositiveRate;
                    PositiveRateLabel.Text += " " + PositiveRate.ToString("0.0") + "%";
                    VariantsListBox.Items.Add("British Variant: (B 1.1.7): " + CovidData.OntarioB117Cases);
                    VariantsListBox.Items.Add("South African: (B 1.351): " + CovidData.OntarioB1351Cases);
                    VariantsListBox.Items.Add("Brazilian Variant (P.1): " + CovidData.OntarioP1Cases);
                    File.WriteAllText(CasesDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(), NewCases.Replace(",", ""));
                    File.WriteAllText(HospitalizedDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(), Hospitalized.Replace(",", ""));
                    VaccineEligibilityLabel.Text += "\n" + OntarioVaccine.Eligibility;


                    if (Jahcord == false)
                    {
                        BackgroundWorker AlertSoundPlayer = new BackgroundWorker();
                        AlertSoundPlayer.DoWork += (o, args) =>
                        {
                            SoundPlayer AlertSound = new SoundPlayer(Resources.NewAlarm);
                            AlertSound.Play();
                        };
                        AlertSoundPlayer.RunWorkerAsync(); 
                    }
                    if (Jahcord == false)
                    {
                        await Task.Factory.StartNew(() =>
                                    {
                                        ReportNewCases.PlaySync();
                                    }); 
                    }
                    TodaysCasesLabel.Text = "Todays Cases: => " + NewCases;
                    if (DateTime.Now.Day == 15 && DateTime.Now.Month == 2 && DateTime.Now.Year == 2021)
                    {
                        TodaysCasesLabel.Text = "No Cases Today (Holiday)";
                    }
                    
                    SevenDayAverage.Text = "Seven Day Average: " + CovidData.OntarioSevenDayAverage;
                    

                    if (NewsTextBox.Text == "")
                    {
                        NewsTextBox.Text = "No special news today";
                    }
                    Visible = true;
                    if (Jahcord == false)
                    {
                        await Task.Factory.StartNew(() =>
                                    {
                                        DewSpeak.Speak(NewCases + " new cases reported today as of " + DateTime.Now.ToString() + " and with an estimated cases of " + CalculationTomorrowPercentage.ToString("0") + " tomorrow");
                                    }); 
                    }

                    File.WriteAllText(ReportedDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "Report", DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Year.ToString() + "\n" + NewCases);
                    Reported = true;

                    if (Jahcord == true)
                    {
                        string ScreenshotFile = COVIDDirectory + "\\image\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + ".png";
                        string LTCOutbreak = COVIDDirectory + "\\longtermcare\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "inoutbreak.txt";
                        string LTCNoOutbreak = COVIDDirectory + "\\longtermcare\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "nooutbreak.txt";
                        await Task.Delay(3000);
                        ScreenShotDemo.ScreenCapture Capture = new ScreenCapture();
                        Capture.CaptureWindowToFile(this.Handle, ScreenshotFile, ImageFormat.Png);
                        // ftp://ftpupload.net/htdocs/OntarioCovidCases/Sample.png //
                        File.WriteAllLines(LTCOutbreak,Ontario_Coronavirus.LongTermCare.GetOutbreakHomesandCases());
                        File.WriteAllLines(LTCNoOutbreak,Ontario_Coronavirus.LongTermCare.GetNoOutbreakHomeswithCases());
                        string ImageLink = "http://covid.bigheados.com/image/" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + ".png";
                        string LTCOutbreakWeb = "http://covid.bigheados.com/longtermcare/" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "inoutbreak.txt";
                        string LTCNoOutbreakWeb = "http://covid.bigheados.com/longtermcare/" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + "nooutbreak.txt";
                        string BOTUrl = "https://discord.com/api/webhooks/772968515153297449/vzqEx6E1sCTYC7A0HAKJHBkpEy4sxWQMxYptfR6gZfuCTEwdf4B0eiBbvn8l_05KaWaX";
                        for (int i = 0; i <= 3; i++)
                        {
                            await Task.Delay(1000);
                            Console.WriteLine(i + "/3");
                            CloseButton.Text = i.ToString() + "/" + 3;
                        }
                        //Hook.SendMessage(DateTime.Now.ToString("F") + "\n" + ImageLink);
                        string[] BOTUrls = GetURLs;
                        //COVID covid = new COVID();
                        //covid.RealTimeCheck = true;
                        string Cases = await Coronavirus.GetCoronavirusCases();
                        string Recovered = await Coronavirus.GetRecoveredCoronavirusCases();
                        string Deaths = await Coronavirus.GetDeathCoronavirusCases();
                        string ActiveCases = await Coronavirus.GetActiveCoronavirusCases();
                        string CriticalCases = await Coronavirus.GetCriticalCoronavirusCases();
                        foreach (string url in BOTUrls)
                        {
                            await Task.Delay(100);
                            await SendDiscordMessage(url, "Worldwide Cases: " + Cases + "\nWorldwide Deaths: " + Deaths + "\nWorldwide Recovered Cases: " + Recovered + "\nWorldwide Critical Cases: " + CriticalCases + "\nWorldwide Active Cases: " + ActiveCases);
                        }
                        foreach (string url in BOTUrls)
                        {
                            await Task.Delay(100);
                            await SendDiscordMessage(url,"Long term care homes currently in outbreak (Ontario):\n" + LTCOutbreakWeb + "\n" + "Long term care homes no longer in outbreak (Ontario):\n" + LTCNoOutbreakWeb);
                        }
                        foreach (string s in BOTUrls)
                        {
                            await Task.Delay(100);
                            await SendDiscordMessage(s, DateTime.Now.ToString("F") + "\n" + ImageLink);
                        }

                        await SendDiscordMessage("https://discord.com/api/webhooks/804126505890086913/gzjy2oNfMFfZp-YRPT1QKR0HqGK2-XjQi8kjRvTGri_6BDi89mOWiBwsBVJq8Ab1oZUE", "```Cases at Markville Secondary School: " + await new yrdsbcovid().GetSchoolCase("Markville S.S") + "```");
                        Application.Exit();
                    }
                }
            }
            CloseButton.Text = "Close";
            CloseButton.Enabled = true;
            Closing = true;
            Visible = false;
            await Task.Delay(TimeSpan.FromMinutes(10));
            Application.Exit();
        }

        public async Task UploadToFTP(string Link, string File)
        {
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential("epiz_27720784", "2mpJj1wZwlpI7sP");
                client.UploadFileAsync(new Uri(Link), File);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
        }
        public async Task SendToDiscord(string HookHA, string Message)
        {
            dWebHook Hook = new dWebHook();
            Hook.UserName = "BOT";
            Hook.WebHook = HookHA;
            Hook.SendMessage(Message);
            Hook.Dispose();
        }
        private void AddStatsList(string stuff)
        {
            StatsListBox.Items.Add(stuff);
        }

        private void AddHospitalList(string stuff, string issue)
        {
            HospitalInformationListBox.Items.Add(stuff + "   " + issue);
        }
        private async Task AddLTCList(string Stuff)
        {
            void AddToList(string s)
            {
                LongTermCareStatusList.Items.Add(s);
            }
            AddToList(CovidData.LtcInOutbreak + " homes in active outbreak");
            AddToList(CovidData.LtcResolved + " resolved outbreaks (outbreakn't)");
            AddToList(CovidData.LtcResidentCases + " active resident cases");
            AddToList(CovidData.LtcStaffCases + " active staff cases");
            AddToList(CovidData.LtcResidentDeaths + " residents died in a long term care home");
            AddToList(CovidData.LtcStaffDeaths + " staff died in a long term care home");
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (Reported == true)
            {
                Close();
            }
            else
            {
                Visible = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\WorldWideCases.exe", Resources.Main_Program);
            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\WorldWideCases.exe");
            Close();
        }

        private async Task SendDiscordMessage(string HookHA, string Message)
        {
            WebClient client = new WebClient();
            NameValueCollection discord = new NameValueCollection();
            discord.Add("username","BOT");
            discord.Add("avatar_url",null);
            discord.Add("content",Message);
            client.UploadValuesAsync(new Uri(HookHA),discord);
            while (client.IsBusy)
            {
                await Task.Delay(10);
            }
        }
    }

    

    public class dWebHook
    {
        private readonly WebClient dWebClient;
        private static NameValueCollection discord = new NameValueCollection();
        public string WebHook { get; set; }
        public string UserName { get; set; }
        public string ProfilePicture { get; set; }

        public dWebHook()
        {
            dWebClient = new WebClient();
        }


        public void SendMessage(string msgSend)
        {
            discord.Add("username", UserName);
            discord.Add("avatar_url", ProfilePicture);
            discord.Add("content", msgSend);

            dWebClient.UploadValues(WebHook, discord);
            Console.WriteLine("MESSAGE CONTAINS: \n" + msgSend);
        }

        public void Dispose()
        {
            dWebClient.Dispose();
        }

    }
}

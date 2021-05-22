using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Coronavirus_API;
using Covid19API;
using HospitalAPI;
using NAudio.Wave;
using Ontario_Color_Coded_Restrictions_API;
using Ontario_Coronavirus_API;
using OntarioCOVIDAPI;
using OntarioVaccineAPI;
using School_COVID_API;
using UsefulTools;
using YRDSBCOVIDAPI;
using Path = System.IO.Path;

namespace Engine
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            KeyDown += (sender, args) =>
            {
                if (args.Key == Key.H && Keyboard.Modifiers == ModifierKeys.Control)
                {
                    Visibility = Visibility.Hidden;
                }
            };
            Deactivated += MainWindow_Deactivated;
            Loaded += MainWindow_Loaded;
           
        }

        private bool SpecialEdition
        {
            get
            {
                return File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SpecialEdition");
            }
        }

        private void MainWindow_Deactivated(object sender, EventArgs e)
        {
            // The Window was deactivated 
            Topmost = false; // set topmost false first
            Topmost = true; // then set topmost true again.
        }

        private async Task Download(string link, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(link), filename);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
        }

        private async Task Announce(Brush Color, string Content, string Speech, Stream Sound)
        {
            SpeechSynthesizer dewspeak = new SpeechSynthesizer();
            dewspeak.SelectVoiceByHints(VoiceGender.Female);
            foreach (InstalledVoice installedVoice in dewspeak.GetInstalledVoices())
            {
                if (installedVoice.VoiceInfo.Name == "Crystal16")
                {
                    dewspeak.SelectVoice("Crystal16");
                }
            }
            NumberLabel.Foreground = Color;
            NumberLabel.Content = Content;
            await PlaySoundSync(Sound, Sound);
            await Task.Factory.StartNew(() =>
            {
                dewspeak.Speak(Speech);
            });
            await FadeEffect();
        }

        private async Task Announce(Brush Color, string Content, string Speech)
        {
            SpeechSynthesizer dewspeak = new SpeechSynthesizer();
            dewspeak.SelectVoiceByHints(VoiceGender.Female);
            foreach (InstalledVoice installedVoice in dewspeak.GetInstalledVoices())
            {
                try
                {
                    if (installedVoice.VoiceInfo.Name == "Crystal16")
                    {
                        dewspeak.SelectVoice("Crystal16");
                    }
                }
                catch (Exception)
                {
                    
                }
            }
            NumberLabel.Foreground = Color;
            NumberLabel.Content = Content;
            await Task.Factory.StartNew(() =>
            {
                dewspeak.Speak(Speech);
            });
            await FadeEffect();
        }
        private async Task<string> GetVaccineCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"), CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            
            
            var ReadLine = 0;
            string CoronavirusCases = "";
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
            CoronavirusCases.Replace(" ", "");
            return CoronavirusCases;
        }
        //COVID covid = new COVID();

        private async Task FadeEffect()
        {
            NumberLabel.Content = "";
            await Task.Delay(150);
        }

        Covid19 covid;
        bool OntarioOnly = File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\OntarioOnly.txt");
        bool DisabledAnnouncer = File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\DisableAnnouncer.txt");
        string ActiveAccount = "";

        private static string MainReadDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\DownloadedOntarioCases";

        private async Task SpeakWavFile(string Pathh)
        {
            if (File.Exists(Pathh))
            {
                await PlaySoundSync( Pathh);
                try
                {
                    File.Delete(Pathh);
                }
                catch
                {

                }
            }
        }
        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DirectoryInfo TempInfo = new DirectoryInfo(Environment.GetEnvironmentVariable("TEMP"));
            foreach (FileInfo fileInfo in TempInfo.GetFiles())
            {
                if(fileInfo.Name.Contains("covid") && fileInfo.Extension.Contains("wav"))
                {
                    fileInfo.Delete();
                }
            }
            if(DisabledAnnouncer) Application.Current.Shutdown();
            if(!File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\CompletedSetup-Silent.txt"))
            {
                Application.Current.Shutdown();
            }
            Visibility = Visibility.Hidden;
            await PlaySoundSync(Properties.Resources.Microsoft_Windows_XP_Startup_Sound);
            Visibility = Visibility.Visible;
            string TheFile = MainReadDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString();
            if (File.Exists(TheFile))
            {
                File.Delete(TheFile);
            }
            using (var client = new WebClient())
            {
                File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt", Properties.Resources.EC);
                Cipher.FileDecrypt(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation.txt", Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt", "e");
                string[] Details = File.ReadAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\EC_Rotation_DC.txt");
                client.Credentials = new NetworkCredential(Details[0], Details[1]);
                //ActiveAccount = client.DownloadString("ftp://ftpupload.net/htdocs/GitHub/ActiveAccount.txt");
            }
            SpeechSynthesizer dewspeak = new SpeechSynthesizer();
            SpeechSynthesizer wavspeak = new SpeechSynthesizer();
            string SpeakToWavReturnFile(string StuffToSay)
            {
                string RandomFile = Path.GetTempPath() + "\\covid" + new Random().Next(1111,9999).ToString() + ".wav";
                wavspeak.SetOutputToWaveFile(RandomFile);
                wavspeak.Speak(StuffToSay);
                return RandomFile;
            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DefaultCOVIDVoice.txt"))
            {
                List<string> Voices = new List<string>();
                foreach (var installedVoice in dewspeak.GetInstalledVoices())
                {
                    Voices.Add(installedVoice.VoiceInfo.Name);
                }
                string[] VoicesArray = Voices.OfType<string>().ToArray();
                string SelectVoice = "";
                SelectVoice = VoicesArray[new Random().Next(0, VoicesArray.Length)];
                SelectVoice = !SelectVoice.Contains("Microsoft") ? SelectVoice : "Crystal16";
                Console.WriteLine("SELECTED VOICE: " + SelectVoice);
                if (VoicesArray.Contains("Crystal16"))
                {
                    try
                    {
                        dewspeak.SelectVoice(SelectVoice);
                        wavspeak.SelectVoice(SelectVoice);
                    }
                    catch
                    {

                    }
                } 
            }
            else
            {
                try
                {
                    wavspeak.SelectVoice(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DefaultCOVIDVoice.txt"));
                    dewspeak.SelectVoice(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DefaultCOVIDVoice.txt"));
                }
                catch (Exception exception)
                {
                    
                }
            }
            Console.WriteLine(SpeakToWavReturnFile("Big head tannibus"));
            //dewspeak.SelectVoice("Vocalizer Expressive Tom Harpo 22kHz");
            try
            {
                if (new Random().Next(1, 10) <= 5)
                {
                    MainGrid.Background = Brushes.White;
                    StatusLabel.Foreground = Brushes.Black;
                    BottomLabel.Foreground = Brushes.Black;
                }
                else
                {
                    MainGrid.Background = Brushes.Black;
                    StatusLabel.Foreground = Brushes.Aqua;
                    BottomLabel.Foreground = Brushes.Aqua;
                }
                //covid.RemoveComma = false;
                //Console.WriteLine(await Get100MillionCaseDay());
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases");
                var OntarioData = OntarioCovid.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/ontario.txt"));
                var OntarioHospitalData = Hospital.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/hospital.txt"));
                var OntarioVaccineData = OntarioVaccine.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/ontariovaccine/common_postal_code/L3R4E8.txt"));
                covid = Covid19.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/worldwide/cases.txt"));
                Visibility = Visibility.Hidden;
                yrdsbcovid YRDSB = new yrdsbcovid();
                await Download("https://ncov2019.live/", Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt");
                var TotalCaseReadLine = 0;
                var TotalCaseLine = 0;
                string Cases = covid.ConfirmedCases.ToString();
                string Recovered = covid.RecoveredCases.ToString();
                string Deaths = covid.Deaths.ToString();
                string SchoolClosureContain = await YRDSB.GetSchoolClosureStatus("Markville S.S");
                string OntarioCasesToday = OntarioData.OntarioCasesToday;
                string OntarioTotalDeaths = OntarioData.OntarioTotalDeaths;
                string OntarioPendingTests = OntarioData.OntarioPendingTests;
                string OntarioYesterdayTest = OntarioData.OntarioYesterdayTest;
                string OntarioHospitalized = OntarioHospitalData.OntarioHospitalized;
                string OntarioICU = OntarioHospitalData.OntarioIcu.ToString();
                string OntarioRecovered = OntarioData.OntarioRecovered;
                string OntarioReportingErrorNews = OntarioData.OntarioReportingErrorNews;
                string OntarioHospitalErrorNews = OntarioData.OntarioHospitalErrorNews;
                string OntarioEstimateCasesTomorrow = OntarioData.OntarioEstimateCasesTomorrow.ToString();
                string OntarioDeathError = OntarioData.OntarioDeathError;
                string OntarioDeaths = OntarioData.OntarioDeaths.ToString();
                string OntarioB117Cases = OntarioData.OntarioB117Cases;
                string OntarioB1351Cases = OntarioData.OntarioB1351Cases.ToString();
                string OntarioP1Cases = OntarioData.OntarioP1Cases.ToString();
                string OntarioTotalCases = OntarioData.OntarioTotalCases;
                string OntarioRecordCases = OntarioData.OntarioRecordCases.ToString();
                string OntarioVaccineStatus = OntarioVaccineData.Eligibility;
                string MSSCases = await YRDSB.GetSchoolCase("Markville");
                bool MSSClosed = !SchoolClosureContain.Contains("Open");
                bool ShowYesterdayTests = true;
                bool ShowPendingTests = true;
                int CleanOntarioTestsYesterday = 0;
                int CleanOntarioPendingTests = 0;

                try
                {
                    CleanOntarioTestsYesterday = Int32.Parse(OntarioYesterdayTest.Replace(",", "").Replace(" ", ""));
                }
                catch (Exception)
                {
                    ShowYesterdayTests = false;
                }
                try
                {
                    CleanOntarioPendingTests = Int32.Parse(OntarioPendingTests.Replace(",", "").Replace(" ", ""));
                }
                catch 
                {
                    ShowPendingTests = false;
                }


                Console.WriteLine("Ontario cases received");
                Console.WriteLine("Ontario status:\nNew cases today: " + OntarioCasesToday + "\nTotal Deaths: " +
                                  OntarioTotalDeaths + "\nPending tests: " + OntarioPendingTests +
                                  "\nYesterdays tests: " +
                                  OntarioYesterdayTest + "\nHospitalized: " + OntarioHospitalized);

                Console.WriteLine("Cases, recovered, deaths recieved");
                

                Topmost = true;
                /// Get strings of coronavirus cases ///
                string ClearerCases = Cases.Replace(",", "");
                string CasesMillion = Cases.Split(',')[0].Trim();
                string CasesRead = Cases.Split(',')[1].Trim() + Cases.Split(',')[2].Trim();
                string RecoveredMillion = Recovered.Split(',')[0].Trim();
                string RecoveredRead = Recovered.Split(',')[1].Trim() + Recovered.Split(',')[2].Trim();
                string CriticalCases = covid.CriticalCases.ToString();
                Console.WriteLine("Critical cases recieved");
                string ActiveCases = covid.ActiveCases.ToString();
                Console.WriteLine("Active cases received");
                ActiveCases = ActiveCases.Replace(" ", "");
                CriticalCases = CriticalCases.Replace(" ", "");
                string PureActiveCases = ActiveCases;
                string ActiveCaseMillion = ActiveCases.Split(',')[0].Trim();
                ActiveCases = PureActiveCases.Split(',')[1].Trim() + PureActiveCases.Split(',')[2].Trim();
                Console.WriteLine("Active Cases Million: " + ActiveCaseMillion + " Continue: " + ActiveCases);
                Cases = ClearerCases;
                string ClearerRecovered = Recovered.Replace(",", "");
                Recovered = ClearerRecovered;
                string DeathMillionBefore = Deaths.Split(',')[0].Trim();
                Deaths = Deaths.Split(',')[1].Trim() + Deaths.Split(',')[2].Trim();
                Console.WriteLine("DEATHS: " + DeathMillionBefore + " million and " + Deaths + " deaths");
                Console.WriteLine("Vaccines recieved");
                bool NoRegions = false;
                string[] LockDownRegions = { };
                string[] RedZoneRegions = { };
                try
                {
                    //LockDownRegions = await OntarioRegions.GetLockDownRegions();
                    //RedZoneRegions = await OntarioRegions.GetRedRegions();
                    //Console.WriteLine("Will show regions");
                }
                catch
                {
                    Console.WriteLine("Cannot load regions");
                    NoRegions = true;
                }
                NoRegions = true;
                //string[] YellowZoneRegions = await OntarioRegions.GetYellowRegions();
                //string[] OrangeZoneRegions = await OntarioRegions.GetOrangeRegions();
                //string[] GreenRegions = await OntarioRegions.GetGreenRegions();
                /// End of get strings of coronavirus cases ///
                
                await FadeIn(100);
                Visibility = Visibility.Visible;


                // Generate Speech (NEW) //

                string ConfirmedCasesSpeak = SpeakToWavReturnFile(CasesMillion + "million and " + CasesRead + " cases");
                string ActiveCasesSpeak = SpeakToWavReturnFile(ActiveCaseMillion + " million and " + ActiveCases + " active cases");
                string CriticalCasesSpeak = SpeakToWavReturnFile(CriticalCases + " critical cases");
                string DeathsSpeak = SpeakToWavReturnFile(DeathMillionBefore + " million and " + Deaths + " deaths");
                string RecoveredSpeak = SpeakToWavReturnFile(RecoveredMillion + " million and " + RecoveredRead + " recovered cases");

                // End of worldwide generate speech //
                if (OntarioOnly == false)
                {
                    NumberLabel.Foreground = Brushes.GreenYellow;
                    NumberLabel.Content = Cases + " Confirmed cases worldwide";
                    await PlaySoundSync(Properties.Resources.CoronavirusCases, Properties.Resources.CoronavirusCasesOld, Properties.Resources.CoronavirusCasesSP);
                    await SpeakWavFile(ConfirmedCasesSpeak);
                    await FadeEffect();

                    NumberLabel.Foreground = Brushes.Yellow;
                    NumberLabel.Content = PureActiveCases.Replace(",", "") + " Active cases worldwide";
                    await PlaySoundSync(Properties.Resources.ActiveCases, Properties.Resources.ActiveCasesOld, Properties.Resources.ActiveCasesSP);
                    await SpeakWavFile(ActiveCasesSpeak);
                    await FadeEffect();

                    NumberLabel.Foreground = Brushes.Orange;
                    NumberLabel.Content = CriticalCases + " Critical cases worldwide";
                    await PlaySoundSync(Properties.Resources.CriticalCases, Properties.Resources.CriticalCasesOld, Properties.Resources.CriticalCasesSP);
                    await SpeakWavFile(CriticalCasesSpeak);
                    await FadeEffect();


                    NumberLabel.Foreground = Brushes.Red;
                    NumberLabel.Content = DeathMillionBefore + Deaths + " Deaths worldwide";
                    await PlaySoundSync(Properties.Resources.CoronavirusDeaths, Properties.Resources.CoronavirusDeathsOld, Properties.Resources.CoronavirusDeathsSP);
                    await SpeakWavFile(DeathsSpeak);
                    await FadeEffect();


                    NumberLabel.Foreground = Brushes.Blue;
                    NumberLabel.Content = Recovered + " recovered from COVID-19";
                    await PlaySoundSync(Properties.Resources.CoronavirusRecovered, Properties.Resources.RecoveredOld, Properties.Resources.CoronavirusRecoveredSP);
                    await SpeakWavFile(RecoveredSpeak);
                    await FadeEffect();



                    //NumberLabel.Foreground = Brushes.Purple;
                    //NumberLabel.Content = Vaccines + " Vaccines in research";
                    //await PlaySoundSync(Properties.Resources.CoronavirusVaccine,Properties.Resources.CoronavirusVaccineOld, Properties.Resources.CoronavirusVaccineSP);
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    dewspeak.Speak(Vaccines + " vaccines in research");
                    //});
                    //await FadeEffect(); 
                }

                if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseDifference.txt"))
                {
                    if (Int32.Parse(Cases) < 200000000)
                    {
                        NumberLabel.Foreground = Brushes.HotPink;
                        var DateJer = await Get100MillionCaseDay();
                        NumberLabel.Content = DateJer.ToString("F") + "";
                        await PlaySoundSync(Properties.Resources._200MillionEstimate, Properties.Resources._200MillionEstimateOld);
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak(DateJer.ToString("F"));
                        }); 
                    }
                    else
                    {
                        NumberLabel.Foreground = Brushes.HotPink;
                        NumberLabel.Content = "Cases already over 200 million";
                        await PlaySoundSync(Properties.Resources.OneBillionDate, Properties.Resources.OneBillionDateOld);
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak("cases already reached more than 200 million");
                        });
                    }
                }
                await FadeEffect();

                NumberLabel.Content = "";
                // Generate Ontario Speech (New case statistics) //

                string NewCasesOntarioSpeak = SpeakToWavReturnFile(OntarioCasesToday + " new covid 19 cases in Ontario today");
                string TotalCasesOntarioSpeak = SpeakToWavReturnFile(OntarioTotalCases + " total cases of covid 19 in Ontario");
                string NewOntarioDeathsSpeak = SpeakToWavReturnFile(OntarioDeaths + " new covid 19 deaths in Ontario today");
                string TotalDeathsOntarioSpeak = SpeakToWavReturnFile(OntarioTotalDeaths + " total covid 19 deaths in Ontario");

                NumberLabel.Foreground = Brushes.Brown;
                NumberLabel.Content = OntarioCasesToday + "/" + OntarioRecordCases + " new cases in Ontario today (new cases/record)";
                await PlaySoundSync(Properties.Resources.OntarioNewCases,Properties.Resources.OntarioNewCasesOld, Properties.Resources.OntarioNewCasesSP);
                await Task.Factory.StartNew(() =>
                {
                    File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(),OntarioCasesToday.Replace(",",""));
                });
                await SpeakWavFile(NewCasesOntarioSpeak);
                await FadeEffect();

                if (OntarioReportingErrorNews != "null")
                {
                    NumberLabel.Foreground = Brushes.Brown;
                    NumberLabel.Content = OntarioReportingErrorNews;
                    string SplitterString = "";
                    int i = 0;
                    foreach (string s in OntarioReportingErrorNews.Split(' '))
                    {
                        SplitterString += s + " ";
                        if (i == 10)
                        {
                            SplitterString += Environment.NewLine;
                            i = 0;
                        }
                        i++;
                    }
                    OntarioReportingErrorNews = SplitterString;
                    //await PlaySoundSync(Properties.Resources.OntarioNewCases, Properties.Resources.OntarioNewCasesOld);
                    foreach (string s in OntarioReportingErrorNews.Split(','))
                    {
                        
                    }
                    foreach (var s1 in OntarioReportingErrorNews.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    {
                        NumberLabel.Content = s1;
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak(s1);
                        });
                    }
                }

                NumberLabel.Foreground = Brushes.Gold;
                NumberLabel.Content = OntarioTotalCases + " total cases in Ontario";
                await PlaySoundSync(Properties.Resources.OntarioTotalCases, Properties.Resources.OntarioTotalCases, Properties.Resources.OntarioTotalCases);
                await SpeakWavFile(TotalCasesOntarioSpeak);
                await FadeEffect();


                await FadeEffect();
                NumberLabel.Foreground = Brushes.Gray;
                NumberLabel.Content = OntarioDeaths + " new deaths in Ontario today";
                await PlaySoundSync(Properties.Resources.OntarioNewDeaths, Properties.Resources.OntarioNewDeathsOld, Properties.Resources.OntarioNewDeathsSP);
                await SpeakWavFile(NewOntarioDeathsSpeak);

                await FadeEffect();
                NumberLabel.Foreground = Brushes.Red;
                NumberLabel.Content = OntarioTotalDeaths + " total deaths in Ontario";
                await PlaySoundSync(Properties.Resources.OntarioTotalDeaths,Properties.Resources.OntarioTotalDeathsOld, Properties.Resources.OntarioTotalDeathsSP);
                await SpeakWavFile(TotalDeathsOntarioSpeak);

                if (OntarioDeathError != "null")
                {
                    NumberLabel.Foreground = Brushes.Brown;
                    NumberLabel.Content = OntarioDeathError;
                    //await PlaySoundSync(Properties.Resources.OntarioNewCases, Properties.Resources.OntarioNewCasesOld);
                    foreach (string s in OntarioDeathError.Split(','))
                    {
                        foreach (var s1 in s.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                        {
                            NumberLabel.Content = s1;
                            await Task.Factory.StartNew(() =>
                            {
                                dewspeak.Speak(s1);
                            });
                        }
                    }
                }
                await FadeEffect();

                // Generate Ontario Speech (Variants) //
                NumberLabel.Content = "";
                string B117Speak = SpeakToWavReturnFile(OntarioB117Cases + " B 1 1 7 cases in Ontario");
                string B1351Speak = SpeakToWavReturnFile(OntarioB1351Cases + " B 1 3 5 1 cases in Ontario");
                string P1Speak = SpeakToWavReturnFile(OntarioP1Cases + " Brazilian cases in Ontario");

                NumberLabel.Foreground = Brushes.Orchid;
                NumberLabel.Content = OntarioB117Cases + " B.1.1.7 cases in Ontario";
                await PlaySoundSync(Properties.Resources.B117Cases, Properties.Resources.B117CasesOld);
                await SpeakWavFile(B117Speak);
                await FadeEffect();
                
                NumberLabel.Foreground = Brushes.Orchid;
                NumberLabel.Content = OntarioB1351Cases + " B.1.351 cases in Ontario";
                await PlaySoundSync(Properties.Resources.B1351, Properties.Resources.B1351Old);
                await SpeakWavFile(B1351Speak);
                await FadeEffect();

                NumberLabel.Foreground = Brushes.Orchid;
                NumberLabel.Content = OntarioP1Cases + " Brazilian (P.1) cases in Ontario";
                await PlaySoundSync(Properties.Resources.OntarioP1, Properties.Resources.OntarioP1Old);
                await SpeakWavFile(P1Speak);
                await FadeEffect();

                // Generate Ontario Speech (Special information) //
                
                //string MarkvilleSpeak = SpeakToWavReturnFile(MSSCases + " cases of covid 19 at markville secondary school");

                //NumberLabel.Foreground = Brushes.IndianRed;
                //NumberLabel.Content = MSSCases + " cases at Markville Secondary School";
                //await PlaySoundSync(Properties.Resources.MarkvilleSchoolCOVID, Properties.Resources.MarkvilleSchoolCOVID);
                //await SpeakWavFile(MarkvilleSpeak);
                //if (MSSClosed)
                //{
                //    NumberLabel.Content = "Markville Secondary School is closed to all students.";
                //    await PlaySoundSync(Properties.Resources.MSSClosed);
                //}
                //else
                //{
                //    NumberLabel.Content = "Markville Secondary School is open to all students.";
                //    await PlaySoundSync(Properties.Resources.MSSOpened);
                //}
                await FadeEffect();

                if (OntarioVaccineStatus.Contains("You can book COVID-19 vaccine"))
                {
                    NumberLabel.Foreground = Brushes.Green;
                    NumberLabel.Content = "You are currently eligible for the Pfizer Vaccine";
                    await PlaySoundSync(Properties.Resources.PfizerEligible);
                    foreach (string s in OntarioVaccineStatus.Split(new []{Environment.NewLine},StringSplitOptions.None))
                    {
                        NumberLabel.Content = s;
                        await Task.Factory.StartNew(() => { dewspeak.Speak(s); });
                    }
                    
                }
                else
                {
                    NumberLabel.Foreground = Brushes.Red;
                    NumberLabel.Content = OntarioVaccineStatus;
                    await PlaySoundSync(Properties.Resources.OntarioVaccineNonEligible);
                }
                await FadeEffect();

                // Generate Speech (Other ontario information) //
                NumberLabel.Content = "";
                string PendingTestsSpeak = SpeakToWavReturnFile(OntarioPendingTests + " people waiting to be tested for covid 19 in Ontario");
                string YesterdayTestsSpeak = SpeakToWavReturnFile(OntarioYesterdayTest + " people who have tested for covid 19 yesterday in Ontario");
                string OntarioRecoveredSpeak = SpeakToWavReturnFile(OntarioRecovered + " people who have recovered in Ontario");
                string HospitalSpeak = SpeakToWavReturnFile(OntarioHospitalized + " people who are in the hospital in Ontario");
                string ICUSpeak = SpeakToWavReturnFile(OntarioICU + " people who are in the intensive care unit in Ontario");

                if (ShowPendingTests == true)
                {
                    NumberLabel.Foreground = Brushes.Blue;
                    NumberLabel.Content = OntarioPendingTests + " pending tests in Ontario";
                    await PlaySoundSync(Properties.Resources.OntarioTestingWaiting,Properties.Resources.OntarioTestingWaitingOld);
                    await SpeakWavFile(PendingTestsSpeak);
                }
                await FadeEffect();
                if (ShowYesterdayTests == true)
                {
                    NumberLabel.Foreground = Brushes.Aqua;
                    NumberLabel.Content = OntarioYesterdayTest + " tests completed yesterday";
                    await PlaySoundSync(Properties.Resources.OntarioYesterdayTested,Properties.Resources.OntarioYesterdayTestedOld);
                    await SpeakWavFile(YesterdayTestsSpeak);
                }
                await FadeEffect();
                NumberLabel.Foreground = Brushes.LightSeaGreen;
                NumberLabel.Content = OntarioRecovered + " recovered people";
                await PlaySoundSync(Properties.Resources.OntarioRecovered,Properties.Resources.OntarioRecoveredOld);
                await SpeakWavFile(OntarioRecoveredSpeak);
                await FadeEffect();

                NumberLabel.Foreground = Brushes.BlueViolet;
                NumberLabel.Content = OntarioHospitalized + " people in hospital";
                await PlaySoundSync(Properties.Resources.OntarioHospital,Properties.Resources.OntarioHospitalOld);
                await SpeakWavFile(HospitalSpeak);
                if (OntarioHospitalErrorNews != "null")
                {
                    NumberLabel.Foreground = Brushes.BlueViolet;
                    NumberLabel.Content = OntarioHospitalErrorNews;
                    string SplitterString = "";
                    int i = 0;
                    foreach (string s in OntarioHospitalErrorNews.Split(' '))
                    {
                        SplitterString += s + " ";
                        if (i == 10)
                        {
                            SplitterString += Environment.NewLine;
                            i = 0;
                        }
                        i++;
                    }
                    OntarioHospitalErrorNews = SplitterString;
                    //await PlaySoundSync(Properties.Resources.OntarioNewCases, Properties.Resources.OntarioNewCasesOld);
                    foreach (string s in OntarioHospitalErrorNews.Split(','))
                    {

                    }
                    foreach (var s1 in OntarioHospitalErrorNews.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
                    {
                        NumberLabel.Content = s1;
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak(s1);
                        });
                    }
                }
                await FadeEffect();

                NumberLabel.Foreground = Brushes.OrangeRed;
                NumberLabel.Content = OntarioHospitalData.IcuPositiveCases + "/" + OntarioHospitalData.OntarioIcu + " in intensive care unit (positive/total)";
                await PlaySoundSync(Properties.Resources.ICUOntario,Properties.Resources.ICUOntarioOld);
                await SpeakWavFile(ICUSpeak);
                await Announce(Brushes.DarkRed, OntarioHospitalData.IcuPositiveCases + " people in intensive care unit (positive test)", OntarioHospitalData.IcuPositiveCases + " people in intensive care unit testing positive for covid 19");
                await Announce(Brushes.DarkGreen, OntarioHospitalData.IcuNegativeCases + " people in intensive care unit (negative test)", OntarioHospitalData.IcuNegativeCases + " people in intensive care unit no longer testing positive for covid 19");
                await Announce(Brushes.IndianRed, OntarioHospitalData.IcuOnVentilator + " people in intensive care unit on ventilator", OntarioHospitalData.IcuOnVentilator + " people in intensive care unit on ventilator");
                await Announce(Brushes.OrangeRed, OntarioHospitalData.IcuOnVentilatorPositiveCases + " people in intensive care unit on ventilator (positive test)", OntarioHospitalData.IcuOnVentilatorPositiveCases + " people in intensive care unit on ventilator testing positive for covid 19");
                await Announce(Brushes.GreenYellow, OntarioHospitalData.IcuOnVentilatorNegativeCases + " people in intensive care unit on ventilator (negative test)", OntarioHospitalData.IcuOnVentilatorNegativeCases + " people in intensive care unit on ventilator no longer testing positive for covid 19");
                if (NoRegions == false)
                {
                    NumberLabel.Foreground = Brushes.DarkSlateGray;
                    NumberLabel.Content = "Lockdown Regions";
                    await PlaySoundSync(Properties.Resources.OntarioLockDown);
                    foreach (string lockDownRegion in LockDownRegions)
                    {
                        NumberLabel.Content = lockDownRegion;
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak(lockDownRegion);
                        });
                    }

                    NumberLabel.Foreground = Brushes.Red;
                    NumberLabel.Content = "Red Zone Regions";
                    await PlaySoundSync(Properties.Resources.OntarioRedZone);
                    foreach (string lockDownRegion in RedZoneRegions)
                    {
                        NumberLabel.Content = lockDownRegion;
                        await Task.Factory.StartNew(() =>
                        {
                            dewspeak.Speak(lockDownRegion);
                        });
                    } 
                }
                await FadeEffect();


                // Generate Ontario (Final Info) //
                NumberLabel.Content = "";
                string SevenDayAverage = OntarioData.OntarioSevenDayAverage.ToString();
                string AverageCasesSpeak = SpeakToWavReturnFile(SevenDayAverage + " average cases in the last seven days");

                    NumberLabel.Foreground = Brushes.Khaki;
                    NumberLabel.Content = SevenDayAverage + " average cases in the last seven days";
                    await PlaySoundSync(Properties.Resources.SevenDay,Properties.Resources.SevenDayOld);
                    await SpeakWavFile(AverageCasesSpeak);
                

                //NumberLabel.Foreground = Brushes.Orange;
                //NumberLabel.Content = "Orange Zone Regions";
                //await PlaySoundSync(Properties.Resources.OntarioOrangeZone);
                //foreach (string lockDownRegion in OrangeZoneRegions)
                //{
                //    NumberLabel.Content = lockDownRegion;
                //    await Task.Factory.StartNew(() =>
                //    {
                //        dewspeak.Speak(lockDownRegion);
                //    });
                //}

                //NumberLabel.Foreground = Brushes.Yellow;
                //NumberLabel.Content = "Yellow Zone Regions";
                //await PlaySoundSync(Properties.Resources.YellowZone);
                //foreach (string lockDownRegion in YellowZoneRegions)
                //{
                //    NumberLabel.Content = lockDownRegion;
                //    await Task.Factory.StartNew(() =>
                //    {
                //        dewspeak.Speak(lockDownRegion);
                //    });
                //}

                //NumberLabel.Foreground = Brushes.Green;
                //NumberLabel.Content = "Green Zone Regions";
                //await PlaySoundSync(Properties.Resources.OntarioGreenZone);
                //foreach (string lockDownRegion in GreenRegions)
                //{
                //    NumberLabel.Content = lockDownRegion;
                //    await Task.Factory.StartNew(() =>
                //    {
                //        dewspeak.Speak(lockDownRegion);
                //    });
                //}
                await FadeEffect();

                double CalculationTomorrowPercentage = 0;
                double PositiveRate = 0;
                if (!ShowYesterdayTests == false && !ShowPendingTests == false)
                {
                    int CleanOntarioCasesToday = Int32.Parse(OntarioCasesToday.Replace(",", ""));
                    Console.WriteLine("Clean ontario cases today: " + CleanOntarioCasesToday + " Clean ontario tests yesterday: " + CleanOntarioTestsYesterday);
                    double YesterdayCasesPercentage = (double)CleanOntarioCasesToday / (double)CleanOntarioTestsYesterday;
                    PositiveRate = YesterdayCasesPercentage;
                    Console.WriteLine("Calculating: " + YesterdayCasesPercentage);
                    YesterdayCasesPercentage = YesterdayCasesPercentage * 100;
                    Console.WriteLine("Calculated case to test: " + YesterdayCasesPercentage);
                    PositiveRate = YesterdayCasesPercentage;
                    CleanOntarioPendingTests = CleanOntarioPendingTests * 2;
                    CalculationTomorrowPercentage = (double)CleanOntarioPendingTests * (double)YesterdayCasesPercentage / 100d;
                    Console.WriteLine(CalculationTomorrowPercentage);
                    Console.WriteLine("ESTIMATED CASES TOMORROW: " + CalculationTomorrowPercentage.ToString("0")); 
                }
                else
                {
                    dewspeak.Speak("No data for estimated cases tomorrow");
                }
                await FadeEffect();

                NumberLabel.Foreground = Brushes.Aqua;
                NumberLabel.Content = PositiveRate.ToString("0.0") + "% positive rate in Ontario";
                await PlaySoundSync(Properties.Resources.OntarioPositiveRate,Properties.Resources.OntarioPositiveRateOld);
                string PositiveRateSpeak = SpeakToWavReturnFile(PositiveRate.ToString("0.0") + "% positive rate in Ontario");
                await Task.Factory.StartNew(() =>
                {
                    if (ShowPendingTests == true && ShowYesterdayTests == true)
                    {
                        SpeakWavFile(PositiveRateSpeak);
                    }
                    else
                    {
                        dewspeak.Speak("No data");
                    }
                });
                await FadeEffect();

                NumberLabel.Foreground = Brushes.Aqua;
                NumberLabel.Content = CalculationTomorrowPercentage.ToString("0") + " estimated cases in Ontario tomorrow";
                await PlaySoundSync(Properties.Resources.EstimatedOntarioCasesTomorrow,Properties.Resources.EstimatedOntarioCasesTomorrowOld,Properties.Resources.EstimatedOntarioCasesTomorrowSP);
                await Task.Factory.StartNew(() =>
                {
                    if (ShowPendingTests == true && ShowYesterdayTests == true)
                    {
                        dewspeak.Speak(CalculationTomorrowPercentage.ToString("0") +
                                       " estimated cases in ontario tomorrow"); 
                    }
                    else
                    {
                        dewspeak.Speak("No data for estimated cases tomorrow");
                    }
                });


                Visibility = Visibility.Hidden;
                Clipboard.SetText("Coronavirus");


                if (SpecialEdition == false)
                {
                    await Download(
                                "https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/WeatherAnnouncer/WeatherAnnouncer/bin/Debug/WeatherAnnouncer.exe",
                                Environment.GetEnvironmentVariable("TEMP") + "\\Weather.exe");
                    await Download(
                        "https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/ClipboardNotifier/Clipboard-Notifier/Clipboard-Notifier/bin/Debug/Clipboard-Notifier.exe",
                        Environment.GetEnvironmentVariable("TEMP") + "\\Clipboard.exe");
                    await Download("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/OntarioAnnouncer/OntarioAnnouncer/bin/Debug/OntarioAnnouncer.exe",
                        Environment.GetEnvironmentVariable("TEMP") + "\\OntarioMachine.exe");
                    await Download(
                        "https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/BigHead-SmallHead/WadwlyHead/WadwlyHead/bin/Debug/WadwlyHead.exe",
                        Environment.GetEnvironmentVariable("TEMP") + "\\WadwlyHead.exe");
                    await Download("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Monitoring-Tools/Main-Tools/Main-Tools%20Remastered/bin/Debug/Main-Tools%20Remastered.exe", Environment.GetEnvironmentVariable("TEMP") + "\\MonitorTool.exe");

                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Weather.exe").WaitForExit();
                    });


                    //await Download(
                    //    "https://raw.githubusercontent.com/EpicGamesGun/Leons-Price-Bot/master/Leons-Price-Bot/bin/Debug/Leons-Price-Bot.exe",
                    //    Environment.GetEnvironmentVariable("TEMP") + "\\LEONS.exe");
                    //await Task.Factory.StartNew(() =>
                    //{
                    //    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\LEONS.exe").WaitForExit();
                    //});
                    if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DisableWadwlyHead.txt"))
                    {
                        await RunCommandHidden("taskkill /f /im WadwlyHead.exe");
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\WadwlyHead.exe"); 
                    }

                    if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\DisableClipboard.txt"))
                    {
                        await RunCommandHidden("taskkill /f /im Clipboard.exe");
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Clipboard.exe"); 
                    }

                    await RunCommandHidden("taskkill /f /im MonitorTool.exe");
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\MonitorTool.exe");

                    if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Ontario.txt"))
                    {
                        await RunCommandHidden("taskkill /f /im OntarioMachine.exe");
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\OntarioMachine.exe");
                    }

                    await Download("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Fan-Spinner/Fan%20Spinner/Fan%20Spinner/bin/Debug/Fan%20Spinner.exe", Environment.GetEnvironmentVariable("TEMP") + "\\Fan-Spinner.exe");
                    //Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Fan-Spinner.exe");
                }

                if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SpecialEdition"))
                {
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\SpecialEdition");
                }
                Application.Current.Shutdown();
            }
            catch (Exception ex)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusError.txt",ex.ToString());
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusError.txt");
                throw;
            }
        }

        private bool Exit = false;


        private async Task<string> GetActiveCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
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
                if (readLine.Contains("Total Active"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        private async Task<string> GetCriticalCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
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
                if (readLine.Contains("Total Critical"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        List<FontFamily> RandomFont = new List<FontFamily>();

        private void AddFonts()
        {
            RandomFont.Add(new FontFamily("Felix Titling"));
            RandomFont.Add(new FontFamily("Segoe UI"));
            RandomFont.Add(new FontFamily("Arial"));
        }
        
        private async Task FadeIn(int Delay)
        {
            AddFonts();
            FontFamily[] Fonts = RandomFont.OfType<FontFamily>().ToArray();
            //ShowInTaskbar = false;
            // 729 (Width), 167 (Height) //
            NumberLabel.Content = "Loading";
            //NumberLabel.FontFamily = Fonts[new Random().Next(0, Fonts.Length)];
            Visibility = Visibility.Visible;
            Width = 0;
            Height = 0;
            FadeWIn();
            FadeHIn();
            while (!FadeH || !FadeW)
            {
                await Task.Delay(10);
            }
            Height = 167;
            Width = 931;
            NumberLabel.Content = "Loading";
        }

        bool FadeW = false;
        bool FadeH = false;

        private async Task FadeWIn()
        {
            while (Width <= 729)
            {
                Width = Width + 10;
                await Task.Delay(1);
            }
            FadeW = true;
        }

        private async Task FadeHIn()
        {
            while (Height <= 167)
            {
                Height = Height + 1;
                await Task.Delay(1);
            }
            FadeH = true;
        }
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


        private async Task<DateTime> Get100MillionCaseDay()
        {
            var Return = new DateTime();
            Return = DateTime.Now;
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseDifference.txt") && File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseElapsedTime.txt"))
            {
                int CovidCaseDifference = Int32.Parse(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseDifference.txt"));
                int CovidCaseElapse = Int32.Parse(File.ReadAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\CovidCaseElapsedTime.txt"));
                int OneHundredMillion = 200000000;
                string CovidCases = await Coronavirus.GetCoronavirusCases();
                CovidCases = CovidCases.Replace(",", "");
                int RemainingCases = OneHundredMillion - Int32.Parse(CovidCases);
                double HowManyTimesBeforeThat = RemainingCases / CovidCaseDifference;
                double TheMinutesBeforeThat = HowManyTimesBeforeThat * CovidCaseElapse;
                Console.WriteLine(TheMinutesBeforeThat);
                Return = Return.AddMinutes(TheMinutesBeforeThat);
            }
            else
            {
                
            }
            return Return;
        }
        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }
        private async Task ShowNotification(string title, string message)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat", "@if (@X)==(@Y) @end /* JScript comment\r\n@echo off\r\n\r\nsetlocal\r\ndel /q /f %~n0.exe >nul 2>&1\r\nfor /f \"tokens=* delims=\" %%v in ('dir /b /s /a:-d  /o:-n \"%SystemRoot%\\Microsoft.NET\\Framework\\*jsc.exe\"') do (\r\n   set \"jsc=%%v\"\r\n)\r\n\r\nif not exist \"%~n0.exe\" (\r\n    \"%jsc%\" /nologo /out:\"%~n0.exe\" \"%~dpsfnx0\"\r\n)\r\n\r\nif exist \"%~n0.exe\" ( \r\n    \"%~n0.exe\" %* \r\n)\r\n\r\n\r\nendlocal & exit /b %errorlevel%\r\n\r\nend of jscript comment*/\r\n\r\nimport System;\r\nimport System.Windows;\r\nimport System.Windows.Forms;\r\nimport System.Drawing;\r\nimport System.Drawing.SystemIcons;\r\n\r\n\r\nvar arguments:String[] = Environment.GetCommandLineArgs();\r\n\r\n\r\nvar notificationText=\"Warning\";\r\nvar icon=System.Drawing.SystemIcons.Hand;\r\nvar tooltip=null;\r\n//var tooltip=System.Windows.Forms.ToolTipIcon.Info;\r\nvar title=\"\";\r\n//var title=null;\r\nvar timeInMS:Int32=2000;\r\n\r\n\r\n\r\n\r\n\r\nfunction printHelp( ) {\r\n   print( arguments[0] + \" [-tooltip warning|none|warning|info] [-time milliseconds] [-title title] [-text text] [-icon question|hand|exclamation|аsterisk|application|information|shield|question|warning|windlogo]\" );\r\n\r\n}\r\n\r\nfunction setTooltip(t) {\r\n\tswitch(t.toLowerCase()){\r\n\r\n\t\tcase \"error\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"none\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.None;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"info\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Info;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\t//tooltip=null;\r\n\t\t\tprint(\"Warning: invalid tooltip value: \"+ t);\r\n\t\t\tbreak;\r\n\t\t\r\n\t}\r\n\t\r\n}\r\n\r\nfunction setIcon(i) {\r\n\tswitch(i.toLowerCase()){\r\n\t\t //Could be Application,Asterisk,Error,Exclamation,Hand,Information,Question,Shield,Warning,WinLogo\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"application\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Application;\r\n\t\t\tbreak;\r\n\t\tcase \"аsterisk\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Asterisk;\r\n\t\t\tbreak;\r\n\t\tcase \"error\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"exclamation\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Exclamation;\r\n\t\t\tbreak;\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"information\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Information;\r\n\t\t\tbreak;\r\n\t\tcase \"question\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Question;\r\n\t\t\tbreak;\r\n\t\tcase \"shield\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Shield;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"winlogo\":\r\n\t\t\ticon=System.Drawing.SystemIcons.WinLogo;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\tprint(\"Warning: invalid icon value: \"+ i);\r\n\t\t\tbreak;\t\t\r\n\t}\r\n}\r\n\r\n\r\nfunction parseArgs(){\r\n\tif ( arguments.length == 1 || arguments[1].toLowerCase() == \"-help\" || arguments[1].toLowerCase() == \"-help\"   ) {\r\n\t\tprintHelp();\r\n\t\tEnvironment.Exit(0);\r\n\t}\r\n\t\r\n\tif (arguments.length%2 == 0) {\r\n\t\tprint(\"Wrong number of arguments\");\r\n\t\tEnvironment.Exit(1);\r\n\t} \r\n\tfor (var i=1;i<arguments.length-1;i=i+2){\r\n\t\ttry{\r\n\t\t\t//print(arguments[i] +\"::::\" +arguments[i+1]);\r\n\t\t\tswitch(arguments[i].toLowerCase()){\r\n\t\t\t\tcase '-text':\r\n\t\t\t\t\tnotificationText=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-title':\r\n\t\t\t\t\ttitle=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-time':\r\n\t\t\t\t\ttimeInMS=parseInt(arguments[i+1]);\r\n\t\t\t\t\tif(isNaN(timeInMS))  timeInMS=2000;\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-tooltip':\r\n\t\t\t\t\tsetTooltip(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-icon':\r\n\t\t\t\t\tsetIcon(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tdefault:\r\n\t\t\t\t\tConsole.WriteLine(\"Invalid Argument \"+arguments[i]);\r\n\t\t\t\t\tbreak;\r\n\t\t}\r\n\t\t}catch(e){\r\n\t\t\terrorChecker(e);\r\n\t\t}\r\n\t}\r\n}\r\n\r\nfunction errorChecker( e:Error ) {\r\n\tprint ( \"Error Message: \" + e.message );\r\n\tprint ( \"Error Code: \" + ( e.number & 0xFFFF ) );\r\n\tprint ( \"Error Name: \" + e.name );\r\n\tEnvironment.Exit( 666 );\r\n}\r\n\r\nparseArgs();\r\n\r\nvar notification;\r\n\r\nnotification = new System.Windows.Forms.NotifyIcon();\r\n\r\n\r\n\r\n//try {\r\n\tnotification.Icon = icon; \r\n\tnotification.BalloonTipText = notificationText;\r\n\tnotification.Visible = true;\r\n//} catch (err){}\r\n\r\n \r\nnotification.BalloonTipTitle=title;\r\n\r\n\t\r\nif(tooltip!==null) { \r\n\tnotification.BalloonTipIcon=tooltip;\r\n}\r\n\r\n\r\nif(tooltip!==null) {\r\n\tnotification.ShowBalloonTip(timeInMS,title,notificationText,tooltip); \r\n} else {\r\n\tnotification.ShowBalloonTip(timeInMS);\r\n}\r\n\t\r\nvar dieTime:Int32=(timeInMS+100);\r\n\t\r\nSystem.Threading.Thread.Sleep(dieTime);\r\nnotification.Dispose();");
            RunCommandHidden("call \"" + Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat" +
                                   "\"   -tooltip warning -time 3000 -title \"" + title + "\" -text \"" + message +
                                   "\" -icon question");
        }

        private async Task meh()
        {
            //while (true)
            //{
            //    if (MoreCases == false)
            //    {
            //        await Download("https://ncov2019.live/",
            //            Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt");
            //        TotalCaseLine = 0;
            //        VaccineReadLineS = 0;
            //        foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
            //                                                "\\CasesCheck.txt"))
            //        {
            //            if (readLine.Contains("<div id=\"mobile-nav-total_world\" class=\"mobile-nav-legend totals\">"))
            //            {
            //                TotalCaseLine = TotalCaseReadLine;
            //                Cases = File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt")
            //                    .ElementAtOrDefault(TotalCaseLine + 6).Split('>')[1].Trim().Split('<')[0].Trim();
            //                Recovered = File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt")
            //                    .ElementAtOrDefault(TotalCaseLine + 12).Split('>')[1].Trim().Split('<')[0].Trim();
            //                Deaths = File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt")
            //                    .ElementAtOrDefault(TotalCaseLine + 9).Split('>')[1].Trim().Split('<')[0].Trim();
            //                break;
            //            }

            //            TotalCaseReadLine++;
            //        }

            //        foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
            //                                                "\\CasesCheck.txt"))
            //        {
            //            if (readLine.Contains("<!--  / 34 -->"))
            //            {
            //                Vaccines = File.ReadLines(Environment.GetEnvironmentVariable("TEMP") + "\\CasesCheck.txt")
            //                    .ElementAtOrDefault(VaccineReadLine).Split('<')[0].Trim();
            //            }

            //            VaccineReadLineS++;
            //        }

            //        if (SetOldCases == false)
            //        {
            //            SetOldCases = true;
            //            OldCases = Cases;
            //        }
            //        NewCases = Cases;
            //    }

            //    if (Int32.Parse(NewCases) > Int32.Parse(OldCases) + 10000)
            //    {
            //        SetOldCases = false;
            //        await ShowNotification("ADDITIONAL 10000 CASES OF CORONAVIRUS",
            //            "Cases are now at " + Cases + " people infected with coronavirus worldwide!");
            //    }

            //    if (Int32.Parse(NewCases) > Int32.Parse(OldCases) + 100000)
            //    {
            //        SetOldCases = false;
            //        await ShowNotification("ADDITIONAL 100000 CASES OF CORONAVIRUS",
            //            "Cases are now at " + Cases + " people infected with coronavirus worldwide!");
            //    }

            //    if (Int32.Parse(NewCases) > Int32.Parse(OldCases) + 1000000)
            //    {
            //        SetOldCases = false;
            //        await ShowNotification("ADDITIONAL 1 MILLION CASES OF CORONAVIRUS",
            //            "Cases are now at " + Cases + " people infected with coronavirus worldwide!");
            //    }

            //    await Task.Delay(3000);

            //}

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

        private int GetRandomInt = 0;

        int RandomInt
        {
            get
            {
                //return GetRandomInt == 0 ? GetRandomInt = new Random().Next(1, 20) : GetRandomInt;
                //return new Random().Next(1, 20);
                return 20;
            }
        }

        private async Task PlaySoundSync(Stream location,Stream location2)
        {
            SoundPlayer dew = new SoundPlayer();
            if (RandomInt <= 10)
            {
                dew.Stream = location;
            }
            else if(RandomInt >= 10)
            {
                dew.Stream = location2;
            }
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }

        private async Task PlaySoundSync(string location)
        {
            await Task.Factory.StartNew(() =>
            {
                new SoundPlayer(location).PlaySync();
            });
        }

        private async Task PlaySoundSync(Stream location, Stream location2, Stream location3)
        {
            SoundPlayer dew = new SoundPlayer();
            if (RandomInt <= 10)
            {
                dew.Stream = location;
            }
            else if (RandomInt >= 10 && RandomInt <= 15)
            {
                dew.Stream = location2;
            }
            else if (RandomInt >= 10)
            {
                dew.Stream = location3;
            }
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }

        private async Task PlaySoundSync(string location, string location2)
        {
            string TempZip1 = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".zip";
            string TempZip2 = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".zip";
            string TempSound1Directory = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".","");
            string TempSound2Directory = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "");
            string TempSound1 = "";
            string TempSound2 = "";
            await Download("https://raw.githubusercontent.com/" + ActiveAccount + "/Coronavirus-Announcement/master/Sounds/" + location + ".zip", TempZip1);
            await Download("https://raw.githubusercontent.com/" + ActiveAccount + "/Coronavirus-Announcement/master/Sounds/" + location2 + ".zip",TempZip2);
            DirectoryInfo Directory1 = Directory.CreateDirectory(TempSound1Directory);
            DirectoryInfo Directory2 = Directory.CreateDirectory(TempSound2Directory);
            ZipFile.ExtractToDirectory(TempZip1,TempSound1Directory);
            ZipFile.ExtractToDirectory(TempZip2,TempSound2Directory);
            foreach (FileInfo fileInfo in Directory1.GetFiles())
            {
                TempSound1 = fileInfo.FullName;
            }

            foreach (FileInfo fileInfo in Directory2.GetFiles())
            {
                TempSound2 = fileInfo.FullName;
            }
            SoundPlayer dew = new SoundPlayer();
            if (RandomInt <= 10)
            {
                dew.SoundLocation = TempSound1;
            }
            else if (RandomInt >= 10)
            {
                dew.SoundLocation = TempSound2;
            }
            await Task.Factory.StartNew(() => { dew.PlaySync(); });
        }
        private async Task PlaySoundFromDataBaseSync(string SoundName)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(
                        new Uri("https://raw.githubusercontent.com/" + ActiveAccount + "/Sounds/master/" + SoundName + ".wav"),
                        Environment.GetEnvironmentVariable("TEMP") + "\\Sound.wav");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                SoundPlayer dew = new SoundPlayer(Environment.GetEnvironmentVariable("TEMP") + "\\Sound.wav");
                await Task.Factory.StartNew(() => { dew.PlaySync(); });
            }
            catch
            {

            }
        }

        private async Task PlaySoundFromDataBase(string SoundName)
        {
            try
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(
                        new Uri("https://raw.githubusercontent.com/" + ActiveAccount + "/Sounds/master/" + SoundName + ".wav"),
                        Environment.GetEnvironmentVariable("TEMP") + "\\Sound.wav");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }

                SoundPlayer dew = new SoundPlayer(Environment.GetEnvironmentVariable("TEMP") + "\\Sound.wav");
                dew.Play();
            }
            catch
            {

            }
        }
    }
}

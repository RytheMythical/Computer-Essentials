using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Media;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Speech.Synthesis;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Clipboard_Notifier.Properties;
using GitHub_Database_Downloader_API;
using NAudio.Wave;
using Teach_Assist;
using Xabe.FFmpeg;
using YoutubeMP3API;

namespace Clipboard_Notifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Closing += Form1_Closing;
            KeyPress += Form1_KeyPress;
        }
        CoronavirusAPI Coronavirus = new CoronavirusAPI();
        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            Console.WriteLine(e.KeyChar);
        }

        private bool NeedToClose = false;
        private void Form1_Closing(object sender, CancelEventArgs e)
        {
            if (NeedToClose == false)
            {
                e.Cancel = true;
            }
            else
            {
                Process.Start(Application.ExecutablePath);
            }
        }

        StorageCode Storage = new StorageCode();
        string[] Chut = { };
        FileEncryptionText Encryption = new FileEncryptionText();

        private async Task RunAnnonymousEXE(string Link)
        {
            string TempEXE = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", "") + ".exe");
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (o, args) =>
                {
                    label1.Text = "Loading... (" + args.ProgressPercentage.ToString() + "%)";
                };
                client.DownloadFileAsync(new Uri(Link), TempEXE);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Process.Start(TempEXE);
            Clipboard.SetText("Done");
        }

        private string FFMPEGPath
        {
            get
            {
                if (!Directory.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\FFMPEG"))
                {
                    using(var client = new WebClient())
                    {
                        client.DownloadFile(new Uri("https://raw.githubusercontent.com/EpicGamesGun/ClipboardNotifier/master/FFMPEG.exe"),Environment.GetEnvironmentVariable("TEMP") + "\\FF.exe");
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\FF.exe").WaitForExit();
                    }
                }
                return Environment.GetEnvironmentVariable("APPDATA") + "\\FFMPEG";
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {

            //if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\ClipboardNotify.exe"))
            //{
            //    File.Copy(Application.ExecutablePath,
            //        Environment.GetFolderPath(Environment.SpecialFolder.Startup) + "\\ClipboardNotify.exe");
            //}

            
            ShowInTaskbar = false;
            TopMost = false;
            //Visible = false;
            string OldClipboard = Clipboard.GetText();
            string NewClipboard = "";
            FormBorderStyle = FormBorderStyle.None;
            Opacity = 100;
            ControlBox = false;
            Visible = false;
            Text = "Clipboard Heaper";
            TopMost = true;
            PlaceLowerRight();
            var count = 0;
            Clipboard.SetText("Initialize " + Path.GetTempFileName());
            Image ClipboardImageOld = Clipboard.GetImage();
            Image ClipboardImageNew = ClipboardImageOld;
            bool ImageCoolDown = false;
            var ci = 0;
            while (true)
            {
                await Task.Delay(500);

                try
                {
                    NewClipboard = Clipboard.GetText();
                    if (OldClipboard == NewClipboard)
                    {

                    }
                    else
                    {
                        if (NewClipboard == "" || Convert.ToBoolean(OldClipboard == "") && !Clipboard.ContainsImage())
                        {
                            if (!Clipboard.ContainsImage())
                            {
                                Console.WriteLine(OldClipboard + "|" + NewClipboard + " - Prevented False Popup, Count: " + ci);
                            }

                            ClipboardImageNew = Clipboard.GetImage();
                            if (ClipboardImageOld != ClipboardImageNew && ImageCoolDown == false)
                            {
                                ImageCoolDown = true;
                                ClipboardImageOld = ClipboardImageNew;
                                TopMost = false;
                                NewImage NewImagess = new NewImage();
                                NewImagess.ShowDialog();
                                TopMost = true;
                            }
                            
                            if (Clipboard.ContainsFileDropList())
                            {
                                ClipboardContent.Text = "";
                                Visible = true;
                                label1.Text = "Files Copied to clipboard";
                                foreach (string s in Clipboard.GetFileDropList())
                                {
                                    ClipboardContent.Text += "\n" + s;
                                }
                                await Task.Delay(3000);
                                Visible = false;
                                StringCollection collection = Clipboard.GetFileDropList();
                                while (Clipboard.ContainsFileDropList())
                                {
                                    if (collection == Clipboard.GetFileDropList())
                                    {
                                        break;
                                    }
                                    await Task.Delay(10);
                                }
                            }

                            if (!Clipboard.ContainsImage())
                            {
                                //ci++;
                                //if (ci == 30)
                                //{
                                //    NewClipboard = "Reset";
                                //    OldClipboard = "Reset";
                                //    ci = 0;
                                //    await RunCommandHidden("taskkill /f /im \"" +
                                //                           Path.GetFileName(Application.ExecutablePath) +
                                //                           "\"\nstart \"\" \"" +
                                //                           Application.ExecutablePath + "\"");
                                //    Close();
                                //    Process.Start(Application.ExecutablePath);
                                //}
                            }
                        }
                        else
                        {
                            ImageCoolDown = false;
                            TopMost = true;
                            label1.Text = "New Clipboard Content " + DateTime.Now.ToString();
                            Console.WriteLine(OldClipboard + "|" + NewClipboard);
                            Visible = true;
                            OldClipboard = NewClipboard;
                            ClipboardContent.Text = Clipboard.GetText();
                           

                            string ClipboardText = Clipboard.GetText();
                            bool ReturnWMVVideo = false;
                            foreach (var file in new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos\\Roblox").GetFiles())
                            {
                                ReturnWMVVideo = Path.GetExtension(file.FullName) == ".wmv";
                            }

                            if (ReturnWMVVideo)
                            {
                                ReturnWMVVideo = false;
                                Visible = true;
                                var i = 0;
                                foreach (var fileInfo in new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos\\Roblox").GetFiles())
                                {
                                    if (Path.GetExtension(fileInfo.FullName) == ".wmv")
                                    {
                                        i++;
                                    }
                                }
                                label1.Text = i + " Videos found in ROBLOX folder";
                                var maximum = i;
                                i = 0;
                                foreach (var fileInfo in new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos\\Roblox").GetFiles())
                                {
                                    if (Path.GetExtension(fileInfo.FullName) == ".wmv")
                                    {
                                        try
                                        {

                                            ClipboardContent.Text = "Converting video to mp4...\n" + "(" + i.ToString() + "/" + maximum + ")\n" + FFMPEGPath;
                                            Console.WriteLine(fileInfo.FullName);
                                            await RunCommandHidden("\"" + Environment.GetEnvironmentVariable("APPDATA") + "\\FFMPEG\\ffmpeg.exe\"" + " -i \"" + fileInfo.FullName + "\" \"" + fileInfo.FullName.Replace(".wmv", ".mp4") + "\"");
                                            fileInfo.Delete();
                                            i++;
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine(ex);
                                            throw;
                                        }
                                    }
                                }
                                Clipboard.SetText("Done");
                            }

                            if (ClipboardText.Contains(":robloxvideo"))
                            {
                                Process.Start(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Videos\\Roblox");
                            }
                            if (ClipboardText.Contains(":joke"))
                            {
                                string Joke = "";
                                var clientq = new HttpClient();
                                var requestq = new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri("https://joke3.p.rapidapi.com/v1/joke"),
                                    Headers =
                                    {
                                        { "x-rapidapi-key", "4f087c5974msh61135a7f748641ap1cb8fdjsn1b7aa10b5b07" },
                                        { "x-rapidapi-host", "joke3.p.rapidapi.com" },
                                    },
                                };
                                using (var response = await clientq.SendAsync(requestq))
                                {
                                    response.EnsureSuccessStatusCode();
                                    var body = await response.Content.ReadAsStringAsync();
                                    Console.WriteLine(body);
                                    Joke = body;
                                }
                                var JokeGet = JokeAPI.Joke.FromJson(Joke);
                                if (JokeGet.Nsfw == false)
                                {
                                    Clipboard.SetText(JokeGet.Content);
                                }
                                else
                                {
                                    Clipboard.SetText("Cannot get joke");
                                }
                            }
                            else if (ClipboardText.Contains(":nugetexplorer"))
                            {
                                string TempEXE = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", "") + ".exe");
                                using (var client = new WebClient())
                                {
                                    client.DownloadProgressChanged += (o, args) =>
                                    {
                                        label1.Text = "Loading... (" + args.ProgressPercentage.ToString() + "%)";
                                    };
                                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Installers/master/Single%20EXES/Nuget_Package_Explorer.exe"),TempEXE);
                                    while (client.IsBusy)
                                    {
                                        await Task.Delay(10);
                                    }
                                }
                                Process.Start(TempEXE);
                                Clipboard.SetText("Done");
                            }
                            else if (ClipboardText.Contains(":vbcable") || ClipboardText.Contains(":virtualaudio") || ClipboardText.Contains(":virtualcable"))
                            {
                                await RunAnnonymousEXE("https://raw.githubusercontent.com/EpicGamesGun/Installers/master/Single%20EXES/VBCable.exe");
                            }
                            else if (ClipboardText.Contains(":launchchrome"))
                            {
                                await RunAnnonymousEXE("https://raw.githubusercontent.com/EpicGamesGun/Installers/master/Single%20EXES/GoogleChromePortable.exe");
                            }
                            else if (ClipboardText.Contains(":launchtor"))
                            {
                                await RunAnnonymousEXE("https://raw.githubusercontent.com/EpicGamesGun/Installers/master/Single%20EXES/Tor.exe");
                            }
                            else if(ClipboardText.Contains(":launchportableapps") || ClipboardText.Contains(":portableappslauncher") || ClipboardText.Contains(":portableapps") || ClipboardText.Contains(":papps"))
                            {
                                await RunAnnonymousEXE("https://raw.githubusercontent.com/EpicGamesGun/Installers/master/Single%20EXES/PortableAppLauncher.exe");
                            }
                            else if(ClipboardText.Contains(":boostroblox") || ClipboardText.Contains(":cheapedroblox"))
                            {
                                await RunAnnonymousEXE("https://raw.githubusercontent.com/EpicGamesGun/ROBLOX-Booster/master/ROBLOX-Booster/bin/Debug/ROBLOX-Booster.exe");
                            }
                            else if (ClipboardText.Contains(":trivia"))
                            {
                                string Trivia = "";
                                var clientd = new HttpClient();
                                var requestd = new HttpRequestMessage
                                {
                                    Method = HttpMethod.Get,
                                    RequestUri = new Uri("https://numbersapi.p.rapidapi.com/random/trivia?max=20&fragment=true&min=10&json=true"),
                                    Headers =
                                    {
                                        { "x-rapidapi-key", "4f087c5974msh61135a7f748641ap1cb8fdjsn1b7aa10b5b07" },
                                        { "x-rapidapi-host", "numbersapi.p.rapidapi.com" },
                                    },
                                };
                                using (var response = await clientd.SendAsync(requestd))
                                {
                                    response.EnsureSuccessStatusCode();
                                    var body = await response.Content.ReadAsStringAsync();
                                    Console.WriteLine(body);
                                    Trivia = body;
                                }
                                var TriviaGet = TriviaAPI.Trivia.FromJson(Trivia);
                                Clipboard.SetText(TriviaGet.Text + ", answer: " + TriviaGet.Number);
                            }
                            else if (ClipboardText.Contains(":ontariocoronavirus") || ClipboardText.Contains("ontariocovid"))
                            {
                                label1.Text = "Waiting for user";
                                OntarioCoronavirusSelection ss = new OntarioCoronavirusSelection();
                                ss.ShowDialog();
                            }
                            else if (ClipboardText.Contains(":startargument"))
                            {
                                try
                                {
                                    string Arguments = ClipboardText.Split('$')[1].Trim();
                                    string Path = ClipboardText.Split('$')[0].Trim().Replace(":startargument", "");
                                    await Task.Factory.StartNew(() =>
                                    {
                                        Process.Start(Path, Arguments).WaitForExit();
                                    });
                                    label1.Text = "Finished";
                                    Clipboard.SetText(" ");
                                }
                                catch (Exception)
                                {
                                    label1.Text = "Invalid text";
                                }
                            }
                            else if (ClipboardText.Contains(":encrypt"))
                            {
                                try
                                {
                                    label1.Text = "Encrypting text...";
                                    string Password = UniqueHashing(ClipboardText.Split('$')[1].Trim());
                                    string Stuff = ClipboardText.Split('$')[0].Trim().Replace(":encrypt", "");
                                    File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt", Stuff);
                                    await Task.Factory.StartNew(() =>
                                    {
                                        Encryption.FileEncrypt(
                                            Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt", Password);
                                        File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt");
                                    });
                                    await RunCommandHidden("certutil -encode \"" +
                                                           Environment.GetEnvironmentVariable("TEMP") +
                                                           "\\EnHui.txt.aes\" \"" +
                                                           Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt");
                                    Clipboard.SetText(File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt"));
                                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt");
                                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\EnHui.txt.aes");
                                    label1.Text = "Text encrypted";
                                    await Task.Delay(3000);
                                }
                                catch (Exception deded)
                                {
                                    Console.WriteLine(deded);
                                    ClipboardContent.Text = deded.ToString();
                                    label1.Text = "Invalid text";
                                    await Task.Delay(3000);
                                }
                            }
                            else if (ClipboardText.Contains(":decrypt"))
                            {

                                try
                                {
                                    label1.Text = "Decrypting text";
                                    string Password = UniqueHashing(ClipboardText.Split('$')[1].Trim());
                                    string Stuff = ClipboardText.Split('$')[0].Trim().Replace(":decrypt", "");
                                    string StuffDirectory = Environment.GetEnvironmentVariable("TEMP") + "\\De.txt";
                                    File.WriteAllText(StuffDirectory, Stuff);
                                    await RunCommandHidden("certutil -decode \"" + StuffDirectory + "\" \"" + Environment.GetEnvironmentVariable("TEMP") + "\\TempFile.txt\"");
                                    File.Delete(StuffDirectory);
                                    File.Move(Environment.GetEnvironmentVariable("TEMP") + "\\TempFile.txt", StuffDirectory);
                                    await Task.Factory.StartNew(() =>
                                    {
                                        Encryption.FileDecrypt(StuffDirectory, StuffDirectory + ".aes", Password);
                                    });
                                    Clipboard.SetText(File.ReadAllText(StuffDirectory + ".aes"));
                                    File.Delete(StuffDirectory);
                                    File.Delete(StuffDirectory + ".aes");
                                    label1.Text = "Text decrypted";
                                    await Task.Delay(3000);
                                }
                                catch (Exception exception)
                                {
                                    Console.WriteLine(exception);
                                    ClipboardContent.Text = exception.ToString();
                                    label1.Text = "Invalid text";
                                    await Task.Delay(3000);
                                }
                            }
                            else if (ClipboardText.Contains(":covid"))
                            {
                                if (ClipboardText.Contains("case"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetCoronavirusCases());
                                }
                                else if (ClipboardText.Contains("deaths"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetDeathCoronavirusCases());
                                }
                                else if (ClipboardText.Contains("active"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetActiveCoronavirusCases());
                                }
                                else if (ClipboardText.Contains("critical"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetCriticalCoronavirusCases());
                                }
                                else if (ClipboardText.Contains("recover"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetRecoveredCoronavirusCases());
                                }
                                else if (ClipboardText.Contains("vaccine"))
                                {
                                    Clipboard.SetText(await Coronavirus.GetVaccineCoronavirusCases());
                                }
                            }
                            else if (ClipboardText.Contains(":download"))
                            {
                                try
                                {
                                    label1.Text = "Downloading file";
                                    SaveFileDialog s = new SaveFileDialog();
                                    s.ShowDialog();
                                    using (var client = new WebClient())
                                    {
                                        client.DownloadProgressChanged += (o, args) =>
                                        {
                                            label1.Text = args.ProgressPercentage.ToString();
                                        };
                                        client.DownloadFileAsync(new Uri(ClipboardText.Replace(":download", "")), s.FileName);
                                        while (client.IsBusy)
                                        {
                                            await Task.Delay(10);
                                        }
                                    }

                                    label1.Text = "File downloaded";
                                    string Arguments = "/select ,\"" + s.FileName + "\"";
                                    Process.Start("explorer.exe", Arguments);
                                }
                                catch (Exception exception)
                                {
                                    ClipboardContent.Text = exception.ToString();
                                    label1.Text = "Invalid link";
                                }
                            }
                            else if (Clipboard.GetText().Contains(":base64e"))
                            {
                                label1.Text = "Converting to base64";
                                string ClipboardStuff = Clipboard.GetText().Replace(":base64e", "");
                                Clipboard.SetText(await EncodeDecodeToBase64String(ClipboardStuff, true));
                                string Replace = Clipboard.GetText().Replace("-----END CERTIFICATE-----", "");
                                Replace = Replace.Replace("-----BEGIN CERTIFICATE-----", "");
                                Clipboard.SetText(Replace);
                                label1.Text = "Base64 string copied";
                            }
                            else if (Clipboard.GetText().Contains(":base64d"))
                            {
                                label1.Text = "Decoding base64";
                                string ClipboardStuff = Clipboard.GetText().Replace(":base64d", "");
                                Clipboard.SetText(await EncodeDecodeToBase64String(ClipboardStuff, false));
                                label1.Text = "Decoded string copied";
                            }
                            else if (Clipboard.GetText().Contains(":uniquehash"))
                            {
                                label1.Text = "Unique hashing";
                                string ClipboardStuff = Clipboard.GetText().Replace(":uniquehash", "");
                                Clipboard.SetText(UniqueHashing(ClipboardStuff));
                                label1.Text = "Unique hash copied";
                            }
                            else if (Clipboard.GetText().Contains(":ultrahash"))
                            {
                                label1.Text = "Ultra hashing";
                                string ClipboardStuff = Clipboard.GetText().Replace(":ultrahash", "");
                                Clipboard.SetText(UltraHash(ClipboardStuff));
                                label1.Text = "Ultra hash copied";
                            }
                            else if (Clipboard.GetText().Contains(":sha256"))
                            {
                                label1.Text = "SHA256 hash copied";
                                string ClipboardStuff = Clipboard.GetText().Replace(":sha256", "");
                                Clipboard.SetText(GetHashString(ClipboardStuff));
                            }
                            else if (Clipboard.GetText().Contains(":pastebin"))
                            {
                                string ClipboardStuff = Clipboard.GetText().Replace(":pastebin", "");
                                await GeneratePastebin(ClipboardStuff);
                            }
                           
                            
                            else if (Clipboard.GetText().Contains(":fileio"))
                            {
                                label1.Text = "Uploading file";
                                string ClipboardStuff = Clipboard.GetText().Replace(":fileio", "");
                                ClipboardStuff = ClipboardStuff.Replace("\"", "");
                                if (File.Exists(ClipboardStuff))
                                {
                                    string URL = await UploadFileIO(ClipboardStuff);
                                    Clipboard.SetText(URL);
                                }
                                else
                                {
                                    File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\FileIO.txt",
                                        ClipboardStuff);
                                    string URL = await UploadFileIO(ClipboardStuff);
                                    Clipboard.SetText(URL);
                                }

                                label1.Text = "Link copied!";
                            }
                            else if (ClipboardText.Contains(":runcommand"))
                            {
                                string ClipboardStuff = ClipboardText.Replace(":runcommand", "");
                                await RunCommandHidden(ClipboardStuff);
                            }
                            else if (ClipboardText.Contains("choco install"))
                            {
                                if (File.Exists(Environment.GetEnvironmentVariable("LOCALAPPDATA") + "\\CompletedSetup-Silent.txt"))
                                {
                                    using (var client = new WebClient())
                                    {
                                        client.DownloadFileAsync(
                                            new Uri(
                                                "https://raw.githubusercontent.com/EpicGamesGun/ClipboardNotifier/master/ChocolateyResponder/ChocolateyResponder/bin/Debug/ChocolateyResponder.exe"),
                                            Environment.GetEnvironmentVariable("TEMP") + "\\Choco.exe");
                                        while (client.IsBusy)
                                        {
                                            await Task.Delay(10);
                                        }

                                    }

                                    await ForceAdmin(Environment.GetEnvironmentVariable("TEMP") + "\\Choco.exe");

                                }
                            }
                            else if (ClipboardText.Contains(":shutdown"))
                            {
                                string ClipboardS = ClipboardText.Replace(":shutdown", "");
                                await RunCommandHidden("shutdown /s /f /t 0");
                            }
                            else if (ClipboardText.Contains(":read"))
                            {
                                var synthesizer = new System.Speech.Synthesis.SpeechSynthesizer();
                                synthesizer.SetOutputToDefaultAudioDevice();
                                synthesizer.Speak(ClipboardText);
                            }
                            else if (ClipboardText.Contains(":bigb"))
                            {
                                string ClipboardNew = ClipboardText.Replace("b", ":b:").Replace(":bigb","");
                                Clipboard.SetText(ClipboardNew);
                            }
                            else if(ClipboardText.Contains(":githubdownload"))
                            {
                                string Link = ClipboardText.Split(':')[0].Trim();
                                GitHubDatabaseDownloader Downloader = new GitHubDatabaseDownloader();
                                BackgroundWorker Download = new BackgroundWorker();
                                Download.DoWork += (o, args) =>
                                {
                                    Downloader.GitHubDatabaseLink = Link;
                                    Downloader.MountVeraCrypt = ClipboardText.Contains(",veracrypt");
                                };
                                Download.RunWorkerAsync();
                                await ShowNotification("Downloading from GitHub Database", "Your download is now starting");
                            }
                            else if (ClipboardText.Contains(":mp3") && ClipboardText.Contains("youtube.com"))
                            {
                                try
                                {
                                    string VideoID = ClipboardText.Replace(":mp3","");
                                    VideoID = VideoID.Replace("https://www.youtube.com/watch?v=", "");
                                    string Response = "";
                                    var clientdew = new HttpClient();
                                    var request = new HttpRequestMessage
                                    {
                                        Method = HttpMethod.Get,
                                        RequestUri = new Uri("https://download-video-youtube1.p.rapidapi.com/mp3/" + VideoID + "?stime=00%3A01%3A00&etime=00%3A02%3A00"),
                                        Headers =
                                    {
                                        { "x-rapidapi-key", "4f087c5974msh61135a7f748641ap1cb8fdjsn1b7aa10b5b07" },
                                        { "x-rapidapi-host", "download-video-youtube1.p.rapidapi.com" },
                                    },
                                    };
                                    using (var response = await clientdew.SendAsync(request))
                                    {
                                        response.EnsureSuccessStatusCode();
                                        var body = await response.Content.ReadAsStringAsync();
                                        Console.WriteLine(body);
                                        Response = body;
                                    }
                                    var Converted = YoutubeMp3.FromJson(Response);
                                    foreach (var info in Converted.VidInfo)
                                    {
                                        if (info.Key == "0")
                                        {
                                            Console.WriteLine(info.Value.DloadUrl.ToString());
                                            SaveFileDialog dialog = new SaveFileDialog();
                                            dialog.DefaultExt = "mp3";
                                            TopMost = true;
                                            dialog.ShowDialog();
                                            try
                                            {
                                                using (var client = new WebClient())
                                                {
                                                    client.DownloadFileAsync(info.Value.DloadUrl,dialog.FileName);
                                                }
                                            }
                                            catch (Exception exception)
                                            {
                                                
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex);
                                    throw;
                                }
                            }
                            else if (Clipboard.GetText().Contains("ConvertAudio") || Clipboard.GetText().Contains("convertaudio"))
                            {
                                try
                                {
                                    string GetDirectory = Clipboard.GetText().Replace("\"", "");
                                    DirectoryInfo d = new DirectoryInfo(GetDirectory);
                                    var i = 0;
                                    List<string> AudioList = new List<string>();
                                    foreach (var fileInfo in d.GetFiles())
                                    {
                                        if (fileInfo.ToString().Contains(".mp3"))
                                        {
                                            AudioList.Add(fileInfo.FullName);
                                        }

                                        i++;
                                    }

                                    string ConvertOption()
                                    {

                                        string Return = "";
                                        if (ClipboardText.Contains("ConvertAudiowav"))
                                        {
                                            Return = "WAV";
                                        }
                                        else if (ClipboardText.Contains("ConvertAudiomp3"))
                                        {
                                            Return = "mp3";
                                        }
                                        else if (ClipboardText.Contains("ConvertAudioogg"))
                                        {
                                            Return = "OGG";
                                        }

                                        return Return;
                                    }

                                    label1.Text = "Converting to " + ConvertOption();
                                    string[] AudioConvertList = AudioList.ToArray();
                                    File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\ConvertList.txt",
                                        AudioConvertList);
                                    await RunCommandHidden(
                                        "\"C:\\Program Files (x86)\\Total Audio Converter\\AudioConverter.exe\" -log \"%temp%\\LogJerjer.txt\" -list \"" +
                                        Environment.GetEnvironmentVariable("TEMP") + "\\ConvertList.txt" + "\" \"" +
                                        GetDirectory + "\" -c" + ConvertOption());
                                    foreach (var fileInfo in d.GetFiles())
                                    {
                                        if (File.Exists(fileInfo.FullName.Replace(".mp3",
                                            "." + ConvertOption().ToLower())))
                                        {
                                            File.Delete(fileInfo.FullName.Replace("." + ConvertOption().ToLower(),
                                                ".mp3"));
                                        }
                                    }

                                    label1.Text = "Finished";
                                    Clipboard.SetText("Finished converting audio (Total " + i.ToString() + " files)");
                                }

                                catch
                                {

                                }
                            }

                            if (ClipboardText.Contains(":wavnew") && ClipboardText.Contains("C:\\"))
                            {
                                string TheFile = ClipboardText.Replace(":wav", "").Replace("\"","");
                                try
                                {
                                    FileAttributes attr = File.GetAttributes(TheFile);
                                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                                    {
                                        DirectoryInfo AudioDirectory = new DirectoryInfo(TheFile);
                                        var Files = 0;
                                        foreach (FileInfo fileInfo in AudioDirectory.GetFiles())
                                        {
                                            Files++;
                                        }
                                        string MaximumFile = Files.ToString();
                                        Files = 0;
                                        foreach (FileInfo fileInfo in AudioDirectory.GetFiles())
                                        {
                                            try
                                            {
                                                BackgroundWorker Converter = new BackgroundWorker();
                                                Converter.DoWork += (o, args) =>
                                                {
                                                    ConvertMp3ToWav(fileInfo.FullName, fileInfo.FullName.Replace(Path.GetExtension(fileInfo.FullName), ".wav"));
                                                    fileInfo.Delete();
                                                };
                                                Converter.RunWorkerAsync();
                                                while (Converter.IsBusy)
                                                {
                                                    await Task.Delay(10);
                                                }
                                                Files++;
                                                label1.Text = "Converting (" + Files.ToString() + "/" + MaximumFile + ")";
                                            }
                                            catch (Exception exception)
                                            {

                                            }
                                        }
                                    }
                                    else if (File.Exists(TheFile))
                                    {
                                        TheFile = TheFile.Replace("\"", "");
                                        ConvertMp3ToWav(TheFile, TheFile.Replace(Path.GetExtension(TheFile), ".wav"));
                                        File.Delete(TheFile);
                                    }
                                    Clipboard.SetText("Done");
                                }
                                catch (Exception exxx)
                                {
                                    MessageBox.Show(TheFile);
                                    MessageBox.Show(exxx.ToString());
                                    label1.Text = "No MP3 Files detected";
                                    await Task.Delay(3000);
                                }
                            }


                            string[] AudioFormats = { "mp3", "wav", "ogg", "wma", "mpc", "aac", "mp4", "alac", "flac", "ape", "amr" };
                            foreach (string audioFormat in AudioFormats)
                            {
                                if (ClipboardText.Contains(":" + audioFormat) && !ClipboardText.Contains("youtube.com"))
                                {
                                    string ClipboardStuff = ClipboardText.Replace("\"", "");
                                    ClipboardStuff = ClipboardStuff.Replace(":" + audioFormat, "").Replace("zip", "");
                                    if (File.Exists(ClipboardStuff))
                                    {
                                        await RunCommandHidden(
                                            "\"C:\\Program Files (x86)\\Total Audio Converter\\AudioConverter.exe\" \"" +
                                            ClipboardStuff + "\" \"" + Path.GetDirectoryName(ClipboardStuff) + "\\" +
                                            Path.GetFileNameWithoutExtension(ClipboardStuff) + "." + audioFormat +
                                            "\" -c" + audioFormat.ToUpper());
                                    }

                                    if (Directory.Exists(ClipboardStuff))
                                    {
                                        DirectoryInfo d = new DirectoryInfo(ClipboardStuff);
                                        foreach (var fileInfo in d.GetFiles())
                                        {
                                            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") +
                                                             "\\ImageConvert.txt"))
                                            {
                                                File.WriteAllText(
                                                    Environment.GetEnvironmentVariable("TEMP") + "\\ImageConvert.txt",
                                                    fileInfo.FullName);
                                            }
                                            else
                                            {
                                                File.WriteAllText(
                                                    Environment.GetEnvironmentVariable("TEMP") + "\\ImageConvert.txt",
                                                    File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") +
                                                                     "\\ImageConvert.txt") + "\n" + fileInfo.FullName);
                                            }
                                        }

                                        await RunCommandHidden(
                                            "\"C:\\Program Files (x86)\\Total Audio Converter\\AudioConverter.exe\" -log \"%temp%\\AudioLog.txt\" -list \"" +
                                            Environment.GetEnvironmentVariable("TEMP") + "\\ImageConvert.txt" +
                                            "\" \"" + ClipboardStuff + "\" -c" + audioFormat);
                                        foreach (var fileInfo in d.GetFiles())
                                        {
                                            if (fileInfo.Name.Contains("." + audioFormat))
                                            {

                                            }
                                            else
                                            {
                                                File.Delete(ClipboardStuff + "\\" + fileInfo.Name);
                                            }
                                        }

                                        File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\ImageConvert.txt");

                                    }

                                    try
                                    {
                                        if (ClipboardText.Contains(":" + audioFormat + "zip"))
                                        {
                                            string TheDirectory = ClipboardText.Replace(":" + audioFormat + "zip", "");
                                            string TempDirectory = Environment.GetEnvironmentVariable("TEMP") + "\\" +
                                                                   Path.GetRandomFileName().Replace(".", "");
                                            Console.WriteLine("TEMP DIRECTORY: " + TempDirectory);
                                            Directory.CreateDirectory(TempDirectory);
                                            DirectoryInfo DirectoryInfoThing = new DirectoryInfo(TheDirectory);
                                            foreach (var file in DirectoryInfoThing.GetFiles())
                                            {
                                                file.MoveTo(TempDirectory + "\\" + file.Name);
                                                ZipFile.CreateFromDirectory(TempDirectory,
                                                    TheDirectory + "\\" +
                                                    Path.GetFileNameWithoutExtension(file.FullName) + ".zip");
                                                File.Delete(TempDirectory + "\\" + file.Name);
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine(ex);
                                        throw;
                                    }

                                    Clipboard.SetText("Done");
                                }
                            }


                            List<string> NewList = new List<string>();
                            foreach (string s in Storage.ClipboardHistory)
                            {
                                NewList.Add(s);
                            }

                            if (!File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt"))
                            {
                                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt", NewClipboard);
                            }
                            else
                            {
                                File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt", File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\ClipboardHistory.txt") + "\n" + NewClipboard);
                            }
                            NewList.Add(NewClipboard);
                            Storage.ClipboardHistory = NewList.ToArray();
                            await Task.Delay(3000);
                            Visible = false;
                            File.Delete(AppDomain.CurrentDomain.BaseDirectory + "\\Caller.exe");
                        }
                    }
                }
                catch (Exception d)
                {
                    await RunCommandHidden("taskkill /f /im \"" + Path.GetFileName(Application.ExecutablePath) + "\"\nstart \"\" \"" +
                                           Application.ExecutablePath + "\"");
                    Console.WriteLine(d);
                }

            }
        }
        private async Task ForceAdmin(string path)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("USERPROFILE") + "\\AppData\\Local\\Temp\\FileToRun.txt", path);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GetAdminRights/master/GetAdminRights/bin/Debug/GetAdminRights.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            await Task.Factory.StartNew(() =>
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\AdminRights.exe").WaitForExit();
            });

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
        private async Task<string> UploadFileIO(string path)
        {
            string Return = "";
            await RunCommandHidden(">C:\\GetLink.txt (\ncurl -F \"file=@\"" + path + "\"\" https://file.io\n)");
            Console.WriteLine(">C:\\GetLink.txt (\ncurl -F \"file=@\"" + path + "\"\" https://file.io\n)");
            string ScrapDetails = File.ReadAllText("C:\\GetLink.txt");
            //File.Delete("C:\\GetLink.txt");
            Console.WriteLine(ScrapDetails);
            string FirstScrap = ScrapDetails.Split(':')[3].Trim();
            FirstScrap = FirstScrap.Replace("\"", "");
            ScrapDetails = ScrapDetails.Split(':')[4].Trim();
            ScrapDetails = ScrapDetails.Replace("expiry", "");
            ScrapDetails = ScrapDetails.Replace("\"", "");
            ScrapDetails = ScrapDetails.Replace(",", "");
            Return = FirstScrap + ":" + ScrapDetails;
            Console.WriteLine(Return);
            return Return;
        }
        private async Task GeneratePastebin(string text)
        {
            System.Collections.Specialized.NameValueCollection Data = new System.Collections.Specialized.NameValueCollection();
            String header = text;
            Data["api_paste_name"] = "Clipboard Notifier Generated Paste";
            Data["api_paste_expire_date"] = "N";
            Data["api_paste_code"] = header;
            Data["api_dev_key"] = "8aaa33c046fd8faf1d495718d2414165";
            Data["api_option"] = "paste";
            WebClient wb = new WebClient();
            byte[] bytes = wb.UploadValues("http://pastebin.com/api/api_post.php", Data);
            string response;
            using (MemoryStream ms = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(ms))
                response = reader.ReadToEnd();
            if (response.StartsWith("Bad API request"))
            {
                Console.WriteLine("Something went wrong. How ironic, the error report returned an error");
                Console.WriteLine("Look, just go to http://overviewer.org/irc. We'll help there :)");
                Console.WriteLine(response);
            }
            else
            {
                System.Diagnostics.Process.Start(response);
            }
        }

        private string UltraHash(string inputstring)
        {
            File.WriteAllText(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\UltraHash.txt",
                inputstring);
            File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\UltraHash.exe", Resources.Ultra_Hash);
            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\UltraHash.exe").WaitForExit();
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\UltraHash.txt");
            return File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                                  "\\UltraHashed.txt").ElementAtOrDefault(0);
        }
        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        private async Task<string> EncodeDecodeToBase64String(string input, bool Encode)
        {
            string Return = "";
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt", input);
            if (Encode == true)
            {
                await RunCommandHidden("certutil -encode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode.txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded.txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
            }
            else
            {
                await RunCommandHidden("certutil -decode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode.txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded.txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt");
                File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
            }
            ImageConverter Convert = new ImageConverter();
            return Return;
        }
        // Usage Example: string Jerjer = await EncodeDecodeToBase64String(input, [true or false])//
        //true = encode, false = decode//
        private string UniqueHashing(string inputstring)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Unique-Hasher/master/Unique-Hasher/bin/Debug/Unique-Hasher.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe");
                while (client.IsBusy)
                {
                    Task.Delay(10);
                }
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt", inputstring);

            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe").WaitForExit();

            return File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt").ElementAtOrDefault(0);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt");
        }
        private void PlaceLowerRight()
        {
            //Determine "rightmost" screen
            Screen rightmost = Screen.AllScreens[0];
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.WorkingArea.Right > rightmost.WorkingArea.Right)
                    rightmost = screen;
            }

            this.Left = rightmost.WorkingArea.Right - this.Width;
            this.Top = rightmost.WorkingArea.Bottom - this.Height;
        }
        private async Task ShowNotification(string title, string message)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat", "@if (@X)==(@Y) @end /* JScript comment\r\n@echo off\r\n\r\nsetlocal\r\ndel /q /f %~n0.exe >nul 2>&1\r\nfor /f \"tokens=* delims=\" %%v in ('dir /b /s /a:-d  /o:-n \"%SystemRoot%\\Microsoft.NET\\Framework\\*jsc.exe\"') do (\r\n   set \"jsc=%%v\"\r\n)\r\n\r\nif not exist \"%~n0.exe\" (\r\n    \"%jsc%\" /nologo /out:\"%~n0.exe\" \"%~dpsfnx0\"\r\n)\r\n\r\nif exist \"%~n0.exe\" ( \r\n    \"%~n0.exe\" %* \r\n)\r\n\r\n\r\nendlocal & exit /b %errorlevel%\r\n\r\nend of jscript comment*/\r\n\r\nimport System;\r\nimport System.Windows;\r\nimport System.Windows.Forms;\r\nimport System.Drawing;\r\nimport System.Drawing.SystemIcons;\r\n\r\n\r\nvar arguments:String[] = Environment.GetCommandLineArgs();\r\n\r\n\r\nvar notificationText=\"Warning\";\r\nvar icon=System.Drawing.SystemIcons.Hand;\r\nvar tooltip=null;\r\n//var tooltip=System.Windows.Forms.ToolTipIcon.Info;\r\nvar title=\"\";\r\n//var title=null;\r\nvar timeInMS:Int32=2000;\r\n\r\n\r\n\r\n\r\n\r\nfunction printHelp( ) {\r\n   print( arguments[0] + \" [-tooltip warning|none|warning|info] [-time milliseconds] [-title title] [-text text] [-icon question|hand|exclamation|аsterisk|application|information|shield|question|warning|windlogo]\" );\r\n\r\n}\r\n\r\nfunction setTooltip(t) {\r\n\tswitch(t.toLowerCase()){\r\n\r\n\t\tcase \"error\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"none\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.None;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"info\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Info;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\t//tooltip=null;\r\n\t\t\tprint(\"Warning: invalid tooltip value: \"+ t);\r\n\t\t\tbreak;\r\n\t\t\r\n\t}\r\n\t\r\n}\r\n\r\nfunction setIcon(i) {\r\n\tswitch(i.toLowerCase()){\r\n\t\t //Could be Application,Asterisk,Error,Exclamation,Hand,Information,Question,Shield,Warning,WinLogo\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"application\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Application;\r\n\t\t\tbreak;\r\n\t\tcase \"аsterisk\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Asterisk;\r\n\t\t\tbreak;\r\n\t\tcase \"error\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"exclamation\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Exclamation;\r\n\t\t\tbreak;\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"information\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Information;\r\n\t\t\tbreak;\r\n\t\tcase \"question\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Question;\r\n\t\t\tbreak;\r\n\t\tcase \"shield\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Shield;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"winlogo\":\r\n\t\t\ticon=System.Drawing.SystemIcons.WinLogo;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\tprint(\"Warning: invalid icon value: \"+ i);\r\n\t\t\tbreak;\t\t\r\n\t}\r\n}\r\n\r\n\r\nfunction parseArgs(){\r\n\tif ( arguments.length == 1 || arguments[1].toLowerCase() == \"-help\" || arguments[1].toLowerCase() == \"-help\"   ) {\r\n\t\tprintHelp();\r\n\t\tEnvironment.Exit(0);\r\n\t}\r\n\t\r\n\tif (arguments.length%2 == 0) {\r\n\t\tprint(\"Wrong number of arguments\");\r\n\t\tEnvironment.Exit(1);\r\n\t} \r\n\tfor (var i=1;i<arguments.length-1;i=i+2){\r\n\t\ttry{\r\n\t\t\t//print(arguments[i] +\"::::\" +arguments[i+1]);\r\n\t\t\tswitch(arguments[i].toLowerCase()){\r\n\t\t\t\tcase '-text':\r\n\t\t\t\t\tnotificationText=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-title':\r\n\t\t\t\t\ttitle=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-time':\r\n\t\t\t\t\ttimeInMS=parseInt(arguments[i+1]);\r\n\t\t\t\t\tif(isNaN(timeInMS))  timeInMS=2000;\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-tooltip':\r\n\t\t\t\t\tsetTooltip(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-icon':\r\n\t\t\t\t\tsetIcon(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tdefault:\r\n\t\t\t\t\tConsole.WriteLine(\"Invalid Argument \"+arguments[i]);\r\n\t\t\t\t\tbreak;\r\n\t\t}\r\n\t\t}catch(e){\r\n\t\t\terrorChecker(e);\r\n\t\t}\r\n\t}\r\n}\r\n\r\nfunction errorChecker( e:Error ) {\r\n\tprint ( \"Error Message: \" + e.message );\r\n\tprint ( \"Error Code: \" + ( e.number & 0xFFFF ) );\r\n\tprint ( \"Error Name: \" + e.name );\r\n\tEnvironment.Exit( 666 );\r\n}\r\n\r\nparseArgs();\r\n\r\nvar notification;\r\n\r\nnotification = new System.Windows.Forms.NotifyIcon();\r\n\r\n\r\n\r\n//try {\r\n\tnotification.Icon = icon; \r\n\tnotification.BalloonTipText = notificationText;\r\n\tnotification.Visible = true;\r\n//} catch (err){}\r\n\r\n \r\nnotification.BalloonTipTitle=title;\r\n\r\n\t\r\nif(tooltip!==null) { \r\n\tnotification.BalloonTipIcon=tooltip;\r\n}\r\n\r\n\r\nif(tooltip!==null) {\r\n\tnotification.ShowBalloonTip(timeInMS,title,notificationText,tooltip); \r\n} else {\r\n\tnotification.ShowBalloonTip(timeInMS);\r\n}\r\n\t\r\nvar dieTime:Int32=(timeInMS+100);\r\n\t\r\nSystem.Threading.Thread.Sleep(dieTime);\r\nnotification.Dispose();");
            await RunCommandHidden("call \"" + Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat" +
                                   "\"   -tooltip warning -time 3000 -title \"" + title + "\" -text \"" + message +
                                   "\" -icon question");
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

        private bool Exitd = false;
        public async Task RunCommandHiddenNoWait(string Command)
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
            Exitd = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            History d = new History();
            d.Show();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}

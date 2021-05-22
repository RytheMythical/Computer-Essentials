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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHub_Large_Uploader.Properties;
using GitHub_Large_Uploader_API;
using IniParser;
using IniParser.Model;
using IniParser.Parser;
using Ookii.Dialogs.Wpf;
using Exception = System.Exception;
using ProgressBarStyle = System.Windows.Forms.ProgressBarStyle;

namespace GitHub_Large_Uploader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load1;
            Load += Form1_Load2;
        }

        private void Form1_Load2(object sender, EventArgs e)
        {
            
        }

        private async Task<string> GetCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
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
            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        private async Task<string> GetRecoveredCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
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
            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        private async Task<string> GetActiveCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
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

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        private async Task<string> GetCriticalCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
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

        private async Task<string> GetDeathCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
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
            CoronavirusCases.Replace(" ", "");

            return CoronavirusCases;
        }

        private async Task<string> GetVaccineCoronavirusCases()
        {
            string CheckCoronavirusCasesFile =
                Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\CoronavirusCheckerThingHuiChut.txt");
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

        private async void Form1_Load1(object sender, EventArgs e)
        {
            while (true)
            {
                try
                {
                    CoronavirusLabel.Text = "Confirmed cases:" + await GetCoronavirusCases() + " | Deaths:" +
                                                    await GetDeathCoronavirusCases() + " | Recovered:" +
                                                    await GetRecoveredCoronavirusCases() + "\nCritical Cases:" +
                                                    await GetCriticalCoronavirusCases() + " | Active Cases:" +
                                                    await GetActiveCoronavirusCases() + "\nVaccines in development: " +
                                                    await GetVaccineCoronavirusCases();
                    await Task.Delay(60000);
                }
                catch
                {

                }
            }
        }

       

        private string User = System.Environment.GetEnvironmentVariable("USERPROFILE");
        public async Task RunCommand(string Command)
        {
            string[] CommandChut = { Command };
            File.WriteAllLines(User + "\\Documents\\RunCommand.bat", CommandChut);
            await Task.Factory.StartNew(() =>
            {
                var C = Process.Start(User + "\\Documents\\RunCommand.bat");
                C.WaitForExit();
            });
            File.Delete(User + "\\Documents\\RunCommand.bat");
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

        private bool Queue = false;
        private bool QueueButtonPressed = false;

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

        private async Task UploadDirectoryToGitHub(string source, string github,bool waitforupload)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SilentGitHubUpload.txt",source + "$" + github);
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/GitHub-Large-Uploader/bin/Debug/GitHub-Large-Uploader.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            if (waitforupload == false)
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe");
            }
            else
            {
                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe").WaitForExit();
                });
            }
        }

        public static bool GenerateLink = false;
        public static string GitHubUsername = "";
        public static string GenerateLinkDetails = "";
        private async void Form1_Load(object sender, EventArgs e)
        {
            PauseButton.Visible = false;
            QueuePanel.Visible = false;
            textBox1.ReadOnly = true;
            textBox2.ReadOnly = true;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            ForceNextButton.Enabled = false;
            if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SilentGitHubUpload.txt"))
            {
                string TextToRead = Environment.GetEnvironmentVariable("TEMP") + "\\SilentGitHubUpload.txt";
                textBox1.Text = File.ReadAllText(TextToRead).Split('$')[0].Trim();
                textBox2.Text = File.ReadAllText(TextToRead).Split('$')[1].Trim();
                SmartModeCheckBox.Checked = true;
                File.Delete(TextToRead);
                await StartUploadGitHub();
                Application.Exit();
            }

            if (File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt"))
            {
                string TheFile = Environment.GetEnvironmentVariable("TEMP") + "\\GitHubUploaderAdvancedAPIResponse.txt";

                string GetLine(int line)
                {
                    return File.ReadLines(TheFile).ElementAtOrDefault(line);
                }

                string SourceDirectory = GetLine(0);
                string GitHubDirectory = GetLine(1);
                string SmartMode = GetLine(2);
                string ShutdownWhenFinished = GetLine(3);
                string Base64AllFiles = GetLine(4);
                string EncryptAllFiles = GetLine(5);
                string DecryptionKey = GetLine(6);
                GitHubUsername = GetLine(7);
                if (GetLine(8) == "true")
                {
                    GenerateLink = true;
                }
                File.Delete(TheFile);
                void CheckTrueThenChange(string input, CheckBox checkbox)
                {
                    if (input == "true" || input == "True")
                    {
                        checkbox.Checked = true;
                    }
                }


                CheckTrueThenChange(SmartMode,SmartModeCheckBox);
                CheckTrueThenChange(ShutdownWhenFinished,ShutdownCheckbox);
                if (EncryptAllFiles == "true")
                {
                    Automation_Settings.Encrypt = true;
                    Automation_Settings.DecryptionKey = DecryptionKey;
                }

                if (Base64AllFiles == "true")
                {
                    Automation_Settings.Base64 = true;
                }

                textBox1.Text = SourceDirectory;
                textBox2.Text = GitHubDirectory;
                await StartUploadGitHub();
                Application.Exit();
            }
        }
        Timer time = new Timer();
        Stopwatch stopwatch = new Stopwatch();
        private bool ContinueButtonPressed = false;
        private async void button1_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@"C:\Program Files\Git\git-cmd.exe"))
            {
                DialogResult d = MessageBox.Show("Do you want to install Git?",
                    "Git is required for this program to run", MessageBoxButtons.YesNo);
                if (d == DialogResult.Yes)
                {
                    StatusLabel.Text = "Downloading Git..";
                    using (var client = new WebClient())
                    {
                        client.DownloadProgressChanged += (o, args) =>
                        {
                            progressBar1.Value = args.ProgressPercentage;
                        };
                        client.DownloadFileCompleted += async (o, args) =>
                        {
                            if (args.Error != null)
                            {

                            }
                            else
                            {
                                StatusLabel.Text = "Installing Git...";
                                progressBar1.Value = 0;
                                progressBar1.Style = ProgressBarStyle.Marquee;
                                await Task.Factory.StartNew(() =>
                                {
                                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Git.exe","/SILENT")
                                        .WaitForExit();
                                });
                                progressBar1.Style = ProgressBarStyle.Blocks;
                                StatusLabel.Text = "Waiting for input...";
                                MessageBox.Show("Please restart your computer");
                            }
                        };
                        client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/Installer/Git.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\Git.exe");
                    }
                }
            }

            if (NumberOfFilesToUploadTextBox.Text == "")
            {
                NumberOfFilesToUploadTextBox.Text = "1";
            }
            await StartUploadGitHub();
        }
        Stopwatch ElapsedUploadTime = new Stopwatch();
        string CommitMessage = "dew";

        private async Task ChangeCommitMessage()
        {
            if (CommitMessageTextBox.Text == "")
            {
                Random d = new Random();
                int MessageCount = d.Next(0, 10);
                if (MessageCount == 0)
                {
                    CommitMessage = "Initial Commit";
                }
                else if (MessageCount == 1)
                {
                    CommitMessage = "few changes";
                }
                else if (MessageCount == 2)
                {
                    CommitMessage = "commiting some changes";
                }
                else if (MessageCount == 3)
                {
                    CommitMessage = "pushing a few code";
                }
                else if (MessageCount == 4)
                {
                    CommitMessage = "updating database";
                }
                else if (MessageCount == 5)
                {
                    CommitMessage = "changing some code, and updating";
                }
                else if (MessageCount == 6)
                {
                    CommitMessage = "changed lot of code";
                }
                else if (MessageCount == 7)
                {
                    CommitMessage = "adding a file";
                }
                else if (MessageCount == 8)
                {
                    CommitMessage = "making some big changes to the repository";
                }
                else if (MessageCount == 9)
                {
                    CommitMessage = "adding few code to my project";
                }
                else if (MessageCount == 10)
                {
                    CommitMessage = "cleaning up code";
                }
                else
                {
                    CommitMessage = "dew";
                }
            }
            else
            {
                CommitMessage = CommitMessageTextBox.Text;
            }
        }

        public static long DirSize(DirectoryInfo d)
        {
            long size = 0;
            // Add file sizes.
            FileInfo[] fis = d.GetFiles();
            foreach (FileInfo fi in fis)
            {
                size += fi.Length;
            }
            // Add subdirectory sizes.
            DirectoryInfo[] dis = d.GetDirectories();
            foreach (DirectoryInfo di in dis)
            {
                size += DirSize(di);
            }
            return size;
        }
        private async Task StartUploadGitHub()
        {
            try
            {
                GenerateCodeCheckBox.Enabled = false;
                GitHubToolsButton.Enabled = false;
                string EncryptionKey = "";
                if (Automation_Settings.Encrypt == true)
                {
                    EncryptionKey = Encryption.UniqueHashing(Automation_Settings.DecryptionKey);
                }
              

                if (Convert.ToBoolean(textBox1.Text == "") != Convert.ToBoolean(textBox2.Text == ""))
                {
                    MessageBox.Show("Please select the source directory and the github directory.");
                }
                else
                {
                    AutomationSettingsButton.Enabled = false;
                    UploadButton.Enabled = false;
                    ExitButton.Enabled = false;
                    BrowseGitHubButton.Enabled = false;
                    BrowseSourceButton.Enabled = false;
                    ImportButton.Enabled = false;
                    await PlaySoundSync(Resources.AnnouncementChime);
                    await PlaySoundSync(Resources.Uploading);
                    QueuePanel.Visible = true;
                    while (Queue == false)
                    {
                        PauseButton.Visible = true;
                        File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\ProcessingUpload.txt",
                            textBox1.Text + "$" + textBox2.Text);
                        string GitDirectory = textBox2.Text;
                        var Source = new DirectoryInfo(textBox1.Text);
                        Source.Refresh();
                        var Files = 0;
                        Console.WriteLine(Source);
                        ExitButton.Enabled = false;
                        UploadButton.Enabled = false;
                        var PROCESS = 0;
                        long SourceDirectoryTotalSize = DirSize(Source);
                        List<string> FilesUploadedList = new List<string>();
                        int PartsCount = 0;
                        foreach (var fileInfo in Source.GetFiles())
                        {
                            PartsCount++;
                        }

                        if (!File.Exists(textBox2.Text + "\\Parts.txt"))
                        {
                            File.WriteAllText(textBox2.Text + "\\Parts.txt",PartsCount.ToString());
                        }
                        foreach (var fileInfo in Source.GetFiles())
                        {
                            PROCESS++;
                            FilesUploadedList.Add(fileInfo.Name + " | Size: " + (fileInfo.Length / 1024d / 1024d).ToString("0.00"));
                        }
                        Console.WriteLine(SourceDirectoryTotalSize);
                        progressBar1.Value = 0;
                        progressBar1.Maximum = PROCESS;
                        var FilesMoved = 0;
                        bool DoneMoving = false;
                        Double TotalSize = 0;
                        foreach (var fileInfo in Source.GetFiles())
                        {
                            FileInfo d = new FileInfo(textBox1.Text + "\\" + fileInfo.Name);
                            d.Refresh();
                            TotalSize = TotalSize + Int32.Parse(d.Length.ToString());
                        }

                        try
                        {
                            string Totaler = (SourceDirectoryTotalSize / 1024d / 1024d).ToString("0.0");
                            TotalSize = Double.Parse(Totaler);
                        }
                        catch (Exception ddd)
                        {
                            Console.WriteLine(ddd);
                            UploadButton.Enabled = true;
                            throw;
                        }

                        StatusLabel.Text = "Refreshing Repository..";
                        await RunCommandHidden("cd /d \"" + textBox1.Text + "\"\ngit pull origin");
                        StatusLabel.Text = "";
                        
                        string TotalSizeCalculate()
                        {
                            string Return = "";
                            Double Calculated = (TotalSize / 1024d / 1024d);
                            Double CalculatedGB = (TotalSize / 1024d / 1024d / 1024d);
                            if (Calculated < 1024)
                            {
                                Return = Calculated + " MB";
                            }
                            else if (Calculated > 1023)
                            {
                                Return = CalculatedGB.ToString("0.00") + " GB";
                            }

                            return Return;
                        }
                        StatusLabel.Text = "Initializing Repository...";
                        await RunCommandHidden("cd /d \"" + textBox1.Text + "\"\ngit push --set-upstream origin master");
                        SizeToUploadLabel.Text = "Total Size of files to upload:\n" + TotalSizeCalculate();
                        ElapsedUploadTime.Start();

                        if (Automation_Settings.Base64 == true)
                        {
                            if (!File.Exists(textBox2.Text + "\\Base64.txt"))
                            {
                                File.WriteAllText(textBox2.Text + "\\Base64.txt","true");
                            }
                        }

                        if (Automation_Settings.Encrypt == true)
                        {
                            if (!File.Exists(textBox2.Text + "\\Encryption.txt"))
                            {
                                File.WriteAllText(textBox2.Text + "\\Encryption.txt", "true");
                            }
                        }
                        bool RememberedFile = false;
                        foreach (var file in Source.GetFiles())
                        {
                            if(file.FullName.Contains("1.exe"))
                            {
                                File.WriteAllText(textBox2.Text + "\\FirstFilename.txt",file.Name);
                            }
                            if (!File.Exists(textBox2.Text + "\\Filename.txt"))
                            {
                                if (RememberedFile == false)
                                {
                                    RememberedFile = true;
                                    File.WriteAllText(textBox2.Text + "\\Filename.txt",file.Name.Split('.')[0].Trim());
                                }
                            }

                            if (DoneMoving == true)
                            {
                                DoneMoving = false;
                                StatusLabel.Text = "Status: Pushing " + file.Name + "\n(" + progressBar1.Value + "/" +
                                                   progressBar1.Maximum + ") Files Uploaded";
                                stopwatch.Start();
                                if (ShowCommandCheckBox.Checked == false)
                                {
                                    try
                                    {
                                        await RunCommandHidden("cd /d \"" + GitDirectory +
                                                               "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {

                                        await RunCommand("cd /d \"" + GitDirectory +
                                                         "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                                    }
                                    catch
                                    {

                                    }
                                }

                                await ChangeCommitMessage();

                                stopwatch.Stop();
                                try
                                {

                                    Double ToSeconds = Int32.Parse(stopwatch.ElapsedMilliseconds.ToString()) / 1000d;
                                    Double ToMinutes = ToSeconds / 60d;
                                    Double ToHour = ToMinutes / 60d;
                                    if (ToSeconds < 3)
                                    {
                                        ShowCommandCheckBox.Checked = true;
                                        SoundPlayer netgeared = new SoundPlayer(Resources.NetGeared);
                                        netgeared.Play();
                                        ContinueButton.Enabled = true;
                                        while (!ContinueButtonPressed == true)
                                        {
                                            await Task.Delay(10);
                                        }

                                        ContinueButtonPressed = false;
                                    }

                                    if (SmartModeCheckBox.Checked == true)
                                    {
                                        if (ToSeconds < 10)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "20";
                                        }
                                        else if (ToSeconds > 10 && ToSeconds < 20)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "10";
                                        }
                                        else if (ToSeconds > 19 && ToSeconds < 30)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "8";
                                        }
                                        else if (ToSeconds > 29 && ToSeconds < 60)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "7";
                                        }

                                        else if (ToSeconds > 59 && ToSeconds < 100)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "6";
                                        }

                                        else if (ToSeconds > 99 && ToSeconds < 120)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "5";
                                        }
                                        else if (ToSeconds > 119 && ToSeconds < 180)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "3";
                                        }
                                        else if (ToSeconds > 179 && ToSeconds < 200)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "2";
                                        }
                                        else if (ToSeconds < 4)
                                        {
                                            if (SkipErrorsTextBox.Checked == false)
                                            {
                                                SystemSounds.Asterisk.Play();
                                                MessageBox.Show("There might be an error with git");
                                                ShowCommandCheckBox.Checked = true;
                                            }
                                        }
                                        else
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "1";
                                        }

                                        DirectoryInfo ddff = new DirectoryInfo(textBox1.Text);
                                        ddff.Refresh();
                                        var FilesCounted = 0;
                                        foreach (var fileInfo in ddff.GetFiles())
                                        {
                                            FilesCounted++;
                                        }

                                        if (Int32.Parse(NumberOfFilesToUploadTextBox.Text) > FilesCounted)
                                        {
                                            NumberOfFilesToUploadTextBox.Text = "1";
                                        }
                                    }

                                    string GetTime()
                                    {
                                        try
                                        {
                                            string Return = String.Empty;
                                            if (Convert.ToBoolean(ToSeconds < 60d))
                                            {
                                                Return = ToSeconds.ToString("0.00") + "Second(s)";
                                            }
                                            else if (ToSeconds > 59d && ToMinutes < 60d)
                                            {
                                                Return = ToMinutes.ToString("0.00") + "Minute(s)";
                                            }
                                            else if (ToMinutes > 59d)
                                            {
                                                Return = ToHour.ToString("0.00") + "Hour(s)";
                                            }

                                            return "Estimated " + Return + " Per file";
                                        }
                                        catch
                                        {
                                            return "";
                                        }
                                    }

                                    StatusLabel.Text = StatusLabel.Text + "\n" + GetTime();

                                    string EstimatedMinutes()
                                    {
                                        Double EstimatedMinutesD = ToMinutes * (progressBar1.Maximum - Files);
                                        Double EstimatedSeconds = ToSeconds * (progressBar1.Maximum - Files);
                                        if (Int32.Parse(NumberOfFilesToUploadTextBox.Text) > 1)
                                        {
                                            EstimatedMinutesD =
                                                ToMinutes * (progressBar1.Maximum -
                                                             (Files / Int32.Parse(NumberOfFilesToUploadTextBox.Text)));
                                            EstimatedSeconds =
                                                ToSeconds * (progressBar1.Maximum -
                                                             (Files / Int32.Parse(NumberOfFilesToUploadTextBox.Text)));
                                        }

                                        Console.WriteLine("Estimated Minutes: " + EstimatedMinutesD +
                                                          " Estimated Seconds: " +
                                                          EstimatedSeconds);
                                        if (Convert.ToBoolean(EstimatedSeconds < 60))
                                        {
                                            return EstimatedSeconds + " Second(s)";
                                        }
                                        else
                                        {
                                            if (EstimatedMinutesD > 60)
                                            {
                                                return (EstimatedMinutesD / 60).ToString("0.00") + " Hour(s)\nor (" +
                                                       EstimatedMinutesD +
                                                       " Minute(s))";
                                            }

                                            if (EstimatedMinutesD == 0)
                                            {
                                                return EstimatedSeconds.ToString("0.00") + " Second(s)";
                                            }
                                            else
                                            {
                                                return EstimatedMinutesD.ToString("0.00") + " Minute(s)";
                                            }
                                        }
                                    }

                                    EstimatedLabel.Text = "Estimated Time Left: " + EstimatedMinutes();
                                }
                                catch
                                {
                                    EstimatedLabel.Text = "No estimation";
                                }

                                stopwatch.Reset();
                                Files = FilesMoved + Files;
                                FilesMoved = 0;
                                if (Files < progressBar1.Maximum)
                                {
                                    progressBar1.Value = Files;
                                }

                                bool Internet = false;
                                while (Internet == false)
                                {
                                    try
                                    {
                                        StatusLabel.Text = "Status: Checking for internet";
                                        using (var client = new WebClient())
                                        {
                                            client.DownloadFileAsync(
                                                new Uri(
                                                    "https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/InternetCheck.txt"),
                                                Environment.GetEnvironmentVariable("TEMP") +
                                                "\\GitHubInternetCheck.txt");
                                            while (client.IsBusy)
                                            {
                                                await Task.Delay(10);
                                            }
                                        }

                                        if (File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
                                                           "\\GitHubInternetCheck.txt")
                                            .ElementAtOrDefault(0) == "true")
                                        {
                                            Internet = true;
                                        }
                                        else
                                        {
                                            StatusLabel.Text = "Status: No Internet";
                                        }

                                        File.Delete(
                                            Environment.GetEnvironmentVariable("TEMP") + "\\GitHubInternetCheck.txt");
                                    }
                                    catch
                                    {
                                        StatusLabel.Text = "Status: No Internet";
                                    }

                                    ForceNextButton.Enabled = false;
                                }

                                Internet = false;
                            }
                            else
                            {
                                ForceNextButton.Enabled = true;
                                try
                                {
                                    if (CopyFilesCheckBox.Checked == false)
                                    {
                                        if (File.Exists(GitDirectory + "\\" + file.Name))
                                        {
                                            File.Delete(GitDirectory + "\\" + file.Name);
                                        }

                                        if (Automation_Settings.Base64 == true)
                                        {
                                            StatusLabel.Text = "Encoding: " + file.Name;
                                            await RunCommandHidden("certutil -encode \"" + file.FullName + "\" \"" + "C:\\TempFile\"");
                                            file.Delete();
                                            File.Move("C:\\TempFile", file.FullName);
                                        }
                                        
                                        if (Automation_Settings.Encrypt == true)
                                        {
                                            BackgroundWorker Encryptor = new BackgroundWorker();
                                            Encryptor.DoWork += (sender, args) =>
                                            {
                                                
                                                    File.WriteAllText(GitDirectory + "\\DecryptionKey.txt",
                                                        Automation_Settings.DecryptionKey);
                                                    Encryption.FileEncrypt(file.FullName, EncryptionKey);
                                                    file.Delete();
                                                    File.Move(file.FullName + ".aes", file.FullName);
                                                    File.Delete(file.FullName + ".aes");
                                                
                                            };
                                            Encryptor.RunWorkerAsync();
                                            StatusLabel.Text = "Encrypting: " + file.Name;
                                            while (Encryptor.IsBusy)
                                            {
                                                await Task.Delay(10);
                                            }
                                        }

                                        if (Automation_Settings.Base64 == true)
                                        {
                                            file.MoveTo(GitDirectory + "\\" + file.Name + ".jer");
                                        }
                                        else
                                        {
                                            file.MoveTo(GitDirectory + "\\" + file.Name);
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(GitDirectory + "\\" + file.Name))
                                        {
                                            File.Delete(GitDirectory + "\\" + file.Name);
                                        }

                                        file.CopyTo(GitDirectory + "\\" + file.Name);
                                    }
                                }
                                catch (Exception huidew)
                                {
                                    Console.WriteLine(huidew);
                                    UploadButton.Enabled = true;
                                    throw;
                                }
                                FilesMoved++;
                                try
                                {
                                    if (FilesMoved + 1 > Int32.Parse(NumberOfFilesToUploadTextBox.Text))
                                    {
                                        DoneMoving = true;
                                    }
                                }
                                catch
                                {
                                    DoneMoving = true;
                                    NumberOfFilesToUploadTextBox.Text = "1";
                                }
                            }
                            if (Pause == true)
                            {
                                await PlaySoundSync(Resources.Paused);
                                PauseButton.Enabled = true;
                                ExitButton.Enabled = true;
                                while (Pause == true)
                                {
                                    await Task.Delay(10);
                                }
                                ExitButton.Enabled = false;
                                await PlaySoundSync(Resources.Continue);
                            }
                        }
                        Source.Refresh();
                        var RemainingFiles = 0;
                        foreach (var fileInfo in Source.GetFiles())
                        {
                            RemainingFiles++;
                        }

                        progressBar1.Value = 0;
                        progressBar1.Maximum = RemainingFiles;
                        Stopwatch CountRemaining = new Stopwatch();
                        SmartModeCheckBox.Enabled = false;
                        SmartModeCheckBox.Checked = false;
                        NumberOfFilesToUploadTextBox.Enabled = false;
                        PlaySound(Resources.UploadingRemainingFiles);
                        foreach (var file in Source.GetFiles())
                        {
                            try
                            {
                                StatusLabel.Text = "Uploading Remaining Files\n(" + progressBar1.Value + "/" +
                                                   progressBar1.Maximum + ")";
                                try
                                {
                                    if (CopyFilesCheckBox.Checked == false)
                                    {
                                        if (File.Exists(GitDirectory + "\\" + file.Name))
                                        {
                                            File.Delete(GitDirectory + "\\" + file.Name);
                                        }
                                        if (Automation_Settings.Base64 == true)
                                        {
                                            StatusLabel.Text = "Encoding: " + file.Name;
                                            await RunCommandHidden("certutil -encode \"" + file.FullName + "\" \"" + "C:\\TempFile\"");
                                            file.Delete();
                                            File.Move("C:\\TempFile", file.FullName);
                                        }

                                        if (Automation_Settings.Encrypt == true)
                                        {
                                            BackgroundWorker Encryptor = new BackgroundWorker();
                                            Encryptor.DoWork += (sender, args) =>
                                            {

                                                File.WriteAllText(GitDirectory + "\\DecryptionKey.txt",
                                                    Automation_Settings.DecryptionKey);
                                                Encryption.FileEncrypt(file.FullName, EncryptionKey);
                                                file.Delete();
                                                File.Move(file.FullName + ".aes", file.FullName);
                                                File.Delete(file.FullName + ".aes");

                                            };
                                            Encryptor.RunWorkerAsync();
                                            StatusLabel.Text = "Encrypting: " + file.Name;
                                            while (Encryptor.IsBusy)
                                            {
                                                await Task.Delay(10);
                                            }
                                        }

                                        if (Automation_Settings.Base64 == false)
                                        {
                                            file.MoveTo(GitDirectory + "\\" + file.Name);
                                        }
                                        else
                                        {
                                            file.MoveTo(GitDirectory + "\\" + file.Name + ".jer");
                                        }
                                    }
                                    else
                                    {
                                        if (File.Exists(GitDirectory + "\\" + file.Name))
                                        {
                                            File.Delete(GitDirectory + "\\" + file.Name);
                                        }

                                        file.CopyTo(GitDirectory + "\\" + file.Name);
                                    }
                                }
                                catch (Exception huidew)
                                {
                                    Console.WriteLine(huidew);
                                    UploadButton.Enabled = true;
                                    throw;
                                }
                                StatusLabel.Text = "Uploading Remaining Files\n(" + progressBar1.Value + "/" +
                                                   progressBar1.Maximum + ")";
                                CountRemaining.Reset();
                                CountRemaining.Start();
                                if (ShowCommandCheckBox.Checked == false)
                                {
                                    try
                                    {
                                        await RunCommandHidden("cd /d \"" + GitDirectory +
                                                               "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                                    }
                                    catch
                                    {

                                    }
                                }
                                else
                                {
                                    try
                                    {

                                        await RunCommand("cd /d \"" + GitDirectory +
                                                         "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                                    }
                                    catch
                                    {

                                    }
                                }

                                await ChangeCommitMessage();
                                CountRemaining.Stop();
                                Double ToSeconds = CountRemaining.ElapsedMilliseconds / 1000d;
                                Double RemainingSeconds = ToSeconds * (progressBar1.Maximum - progressBar1.Value);
                                Double RemainingMinutes = RemainingSeconds / 60d;
                                Double RemainingHours = RemainingMinutes / 60d;

                                string RemainingTime()
                                {
                                    string Return = "";
                                    if (RemainingSeconds < 60)
                                    {
                                        Return = RemainingSeconds.ToString("0.00") + " Second(s) remaining";
                                    }
                                    else if (RemainingSeconds > 59 && RemainingMinutes < 60)
                                    {
                                        Return = RemainingMinutes.ToString("0.00") + " Minute(s) remaining";
                                    }
                                    else if (RemainingMinutes > 59)
                                    {
                                        Return = RemainingHours.ToString("0.00") + " Hour(s) remaining";
                                    }

                                    return Return;
                                }

                                EstimatedLabel.Text = "Estimated Time Remaining:\n" + RemainingTime();
                                try
                                {
                                    progressBar1.Value = progressBar1.Value + 1;
                                }
                                catch
                                {

                                }

                                StatusLabel.Text = "Checking for internet...";
                                await CheckInternet();
                                if (Pause == true)
                                {
                                    await PlaySoundSync(Resources.Paused);
                                    PauseButton.Enabled = true;
                                    ExitButton.Enabled = true;
                                    while (Pause == true)
                                    {
                                        await Task.Delay(10);
                                    }
                                    ExitButton.Enabled = false;
                                    await PlaySoundSync(Resources.Continue);
                                }
                            }
                            catch
                            {

                            }
                        }
                        SmartModeCheckBox.Enabled = true;
                        SmartModeCheckBox.Checked = false;
                        NumberOfFilesToUploadTextBox.Enabled = true;
                        NumberOfFilesToUploadTextBox.Text = "1";
                        ElapsedUploadTime.Stop();
                        bool Saved = false;
                        try
                        {
                            
                            //File.WriteAllText(
                            //    HistoryDirectory + "\\" + DateTime.Now.Month.ToString().Replace("/","") + "-" + DateTime.Now.Date.ToString().Replace("/","") + "-" +
                            //    DateTime.Now.Year.ToString().Replace("/","") + ".txt",
                            //    textBox1.Text + "\n" + textBox2.Text + "\n" + ElapsedUploadTime.ElapsedMilliseconds +
                            //    "\n" +
                            //    DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString());
                            EmailFunctions Email = new EmailFunctions();
                            if (!Convert.ToBoolean(LockedInEmail == ""))
                            {
                                string[] FilesListArray = FilesUploadedList.OfType<string>().ToArray();
                                string StuffToSend =
                                    "Your upload history (GitHub-Large-Uploader by Software Store):\n\n\nDate uploaded: " +
                                    DateTime.Now.ToString() + "\n\n\nTime took to upload: " +
                                    (ElapsedUploadTime.ElapsedMilliseconds / 1000d / 60d).ToString("0.00") +
                                    " minute(s)\n Files uploaded: (Total " + PROCESS.ToString() + " file(s))\nTotal Size Of All Files: " + TotalSize.ToString() + " MB \n\n\n" + FilesListArray;
                                await Email.SendEmail(LockedInEmail, "GitHub-Large-Uploader History",
                                    "Your upload history", StuffToSend);
                            }
                        }
                        catch (Exception d)
                        {
                            Console.WriteLine(d);
                            File.WriteAllText(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\Log.txt",d.ToString());
                            Process.Start(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Desktop\\Log.txt");
                        }

                        /// DOUBLE CHECK ///
                        StatusLabel.Text = "Double Checking...";
                        progressBar1.Value = 0;
                        EstimatedLabel.Text = "";

                        await PlaySoundSync(Resources.DoubleCheckingFiles);
                        
                        if (ShowCommandCheckBox.Checked == false)
                        {
                            try
                            {
                                await RunCommandHidden("cd /d \"" + GitDirectory +
                                                       "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                            }
                            catch
                            {

                            }
                        }
                        else
                        {
                            try
                            {

                                await RunCommand("cd /d \"" + GitDirectory +
                                                 "\" \n git add --all \n git commit -m \"" + CommitMessage + "\" \n git push origin");
                            }
                            catch
                            {

                            }
                        }

                        await ChangeCommitMessage();
                        /// END OF DOUBLE CHECK ///

                        if (File.Exists(UploadQueue))
                        {
                            QueueButtonPressed = true;
                        }

                        if (QueueButtonPressed == true)
                        {
                            if (File.Exists(UploadQueue + "TEMP"))
                            {
                                File.Delete(UploadQueue + "TEMP");
                            }

                            QueueButtonPressed = false;
                            Queue = false;
                            var Lines = 0;
                            if (File.ReadLines(UploadQueue).ElementAtOrDefault(0) == "")
                            {
                                Queue = true;
                                File.Delete(UploadQueue);
                            }
                            else
                            {
                                foreach (var readLine in File.ReadLines(UploadQueue))
                                {
                                    Lines++;
                                }

                                var LinesToRead = 1;
                                //if (!File.Exists(UploadQueue + "TEMP"))
                                //{
                                //    File.WriteAllText(UploadQueue + "TEMP", "");
                                //}
                                if (!Convert.ToBoolean(File.ReadLines(UploadQueue).ElementAtOrDefault(1) == ""))
                                {
                                    while (LinesToRead < Lines)
                                    {
                                        File.AppendAllLines(UploadQueue + "TEMP",
                                            new[] {File.ReadLines(UploadQueue).ElementAtOrDefault(LinesToRead)});
                                        LinesToRead++;
                                    }

                                    try
                                    {
                                        File.WriteAllText(UploadQueue, File.ReadAllText(UploadQueue + "TEMP"));
                                    }
                                    catch
                                    {

                                    }
                                }

                                Lines = 0;
                                LinesToRead = 1;
                                try
                                {
                                    textBox1.Text = File.ReadLines(UploadQueue).ElementAtOrDefault(0).Split('$')[0]
                                        .Trim();
                                    textBox2.Text = File.ReadLines(UploadQueue).ElementAtOrDefault(0).Split('$')[1]
                                        .Trim();
                                }
                                catch (Exception QUEU)
                                {
                                    Console.WriteLine(QUEU);
                                    throw;
                                    UploadButton.Enabled = true;
                                    Queue = true;
                                }

                                try
                                {
                                    if (File.ReadLines(UploadQueue).ElementAtOrDefault(0) == File
                                        .ReadLines(
                                            Environment.GetEnvironmentVariable("TEMP") + "\\ProcessingUpload.txt")
                                        .ElementAtOrDefault(0))
                                    {
                                        File.Delete(UploadQueue);
                                    }
                                }
                                catch (Exception exception)
                                {
                                    File.Delete(UploadQueue);
                                }
                            }
                        }
                        else
                        {
                            Queue = true;
                        }
                        if (GenerateCodeCheckBox.Checked == true)
                        {
                            GeneratedLink d = new GeneratedLink();
                            GenerateLinkDetails = "https://github.com/" + GenerateCodeSettings.GitHubUsername + "/" + Path.GetFileName(textBox2.Text) + ".git";
                            PlaySound(Resources.GenerateDownloadCode);
                            d.ShowDialog();
                        }

                        if (GenerateLink == true)
                        {
                            GeneratedLink d = new GeneratedLink();
                            GenerateLinkDetails = "https://github.com/" + GitHubUsername + "/" + Path.GetFileName(textBox2.Text) + ".git";
                            PlaySound(Resources.GenerateDownloadCode);
                            d.ShowDialog();
                        }
                    }
                    GitHubUploader Uploader = new GitHubUploader();
                    
                    Queue = false;
                   
                }

                PauseButton.Visible = false;
                QueuePanel.Visible = false;
                ImportButton.Enabled = true;
                GitHubToolsButton.Enabled = true;
                await PlaySoundSync(Resources.AnnouncementChime);
                SoundPlayer dew = new SoundPlayer(Resources.Finished_Upload);
                await Task.Factory.StartNew(() => { dew.PlaySync(); });
                await PlaySoundSync(Resources.InternetRestored);
                StatusLabel.Text = "Status: Waiting for input";
                EstimatedLabel.Text = "";
                progressBar1.Value = 0;
                SystemSounds.Beep.Play();
                UploadButton.Enabled = true;
                BrowseGitHubButton.Enabled = true;
                BrowseSourceButton.Enabled = true;
                AutomationSettingsButton.Enabled = true;
                ExitButton.Enabled = true;
                GenerateCodeCheckBox.Enabled = true;
                if (ShutdownCheckbox.Checked == true)
                {
                    SoundPlayer Shut = new SoundPlayer(Resources.ShutdownIn30Seconds);
                    await Task.Factory.StartNew(() => { Shut.PlaySync(); });
                    for (int i = 30; i > 0; i = i - 1)
                    {
                        StatusLabel.Text = "Shutting Down In: " + i + " Seconds";
                        Random h = new Random();
                        int j = h.Next(0, 1);

                        SystemSounds.Hand.Play();

                        await Task.Delay(1000);
                    }

                    StatusLabel.Text = "Shutting Down";
                    SoundPlayer dewd = new SoundPlayer(Resources.ShuttingDown);
                    dewd.PlaySync();
                    await RunCommandHidden("shutdown /s /f /t 00");
                }
            }
            catch (Exception d)
            {
                Console.WriteLine(d);
                var st = new StackTrace(d, true);
                // Get the top stack frame
                var frame = st.GetFrame(0);
                // Get the line number from the stack frame
                var line = frame.GetFileLineNumber();
                Console.WriteLine("ERROR LINE => " + line);
                throw;
            }
        }

        private async Task CheckInternet()
        {
            bool Internet = false;
            string StringDownload = String.Empty;
            while (Internet == false)
            {
                await Task.Factory.StartNew(() =>
                {
                    using (var client = new WebClient())
                    {
                        try
                        {
                            StringDownload = client.DownloadString(new Uri(
                                "https://raw.githubusercontent.com/EpicGamesGun/StarterPackages/master/InternetCheck.txt"));
                        }
                        catch
                        {

                        }
                    }
                });


                if (StringDownload == "true")
                {
                    Internet = true;
                }

                await Task.Delay(1000);
            }

            Internet = false;
        }

        private string UploadQueue = Environment.GetEnvironmentVariable("TEMP") + "\\UploadQueue.txt";
        private bool Exit = false;
        private bool Kill = false;
        public async Task RunCommandHidden(string Command)
        {
            Kill = false;
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
                if (Kill == true)
                {
                    C.Kill();
                }
            }

            Kill = false;
            Exit = false;
            try
            {
                File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
            }
            catch
            {

            }
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dewBrowserDialog = new VistaFolderBrowserDialog();
            dewBrowserDialog.ShowDialog();
            if (Directory.Exists(dewBrowserDialog.SelectedPath))
            {
                textBox1.Text = dewBrowserDialog.SelectedPath;
                DirectoryInfo d = new DirectoryInfo(dewBrowserDialog.SelectedPath);
                var Files = 0;
                foreach (var file in d.GetFiles())
                {
                    Files++;
                }

                progressBar1.Maximum = Files;
            }
            else
            {
                MessageBox.Show("Invalid Directory");
                Process.Start(Application.ExecutablePath);
                Close();
                Application.Exit();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog dewDialog = new VistaFolderBrowserDialog();
            if (Directory.Exists(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) +
                                 "\\Documents\\GitHub"))
            {
                dewDialog.RootFolder = Environment.SpecialFolder.CommonDocuments;
            }
            dewDialog.ShowDialog();
            if (Directory.Exists(dewDialog.SelectedPath))
            {
                textBox2.Text = dewDialog.SelectedPath;
            }
            else
            {
                MessageBox.Show("Invalid Directory");
                Process.Start(Application.ExecutablePath);
                Close();
                Application.Exit();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private async void ForceNextButton_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Kill = true;
        }

        private void QueueButton_Click(object sender, EventArgs e)
        {
            QueueButtonPressed = true;
            if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\UploadQueue.txt"))
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\UploadQueue.txt", SourceDirectoryQueue.Text + "$" + GitHubDirectoryQueue.Text);
            }
            else
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\UploadQueue.txt",
                    File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\UploadQueue.txt") + "\n" + SourceDirectoryQueue.Text + "$" + GitHubDirectoryQueue.Text);
            }

            SourceDirectoryQueue.Text = "";
            GitHubDirectoryQueue.Text = "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (File.Exists(UploadQueue))
            {
                Process.Start(UploadQueue);
            }
            else
            {
                MessageBox.Show("No Queue");
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QueueButtonPressed = true;
        }

        private async void CopyFilesCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (CopyFilesCheckBox.Checked == true)
            {
                
            }
        }

        private void ShutdownCheckbox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void ShowCommandCheckBox_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog d = new VistaFolderBrowserDialog();
            d.ShowDialog();
            try
            {
                SourceDirectoryQueue.Text = d.SelectedPath;
            }
            catch
            {

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            VistaFolderBrowserDialog d = new VistaFolderBrowserDialog();
            d.ShowDialog();
            try
            {
                GitHubDirectoryQueue.Text = d.SelectedPath;
            }
            catch
            {

            }
        }

        private void SmartModeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (SmartModeCheckBox.Checked == true)
            {
                NumberOfFilesToUploadTextBox.Enabled = false;
            }
            else
            {
                NumberOfFilesToUploadTextBox.Enabled = true;
            }
        }

        private void ContinueButton_Click(object sender, EventArgs e)
        {
            ContinueButtonPressed = true;
        }

        private void NumberOfFilesToUploadTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (Int32.Parse(NumberOfFilesToUploadTextBox.Text) > 30)
                {
                    NumberOfFilesToUploadTextBox.Text = "30";
                }
            }
            catch
            {
                if (NumberOfFilesToUploadTextBox.Text == "")
                {

                }
                else
                {
                    NumberOfFilesToUploadTextBox.Text = "1";
                }
            }
        }

        private void AlwaysOnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AlwaysOnTopCheckBox.Checked == true)
            {
                TopMost = true;
            }
            else
            {
                TopMost = false;
            }
        }

        private void UIColorButton_Click(object sender, EventArgs e)
        {
            ColorDialog d = new ColorDialog();
            d.ShowDialog();
            BackColor = d.Color;
            File.WriteAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\GitHubUploaderBackground.txt",
                d.Color.ToString());
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        

      
        Automation_Settings Automation = new Automation_Settings();
        EncryptionClass Encryption = new EncryptionClass();
        private async void AutomationSettingsButton_Click(object sender, EventArgs e)
        {
            Automation_Settings d = new Automation_Settings();
            d.ShowDialog();
            if (Automation_Settings.Encrypt == true || Automation_Settings.Base64 == true)
            {
                SmartModeCheckBox.Checked = false;
                SmartModeCheckBox.Enabled = false;
                NumberOfFilesToUploadTextBox.Text = "1";
                NumberOfFilesToUploadTextBox.Enabled = false; 
            }
            else
            {
                SmartModeCheckBox.Enabled = true;
                NumberOfFilesToUploadTextBox.Enabled = true;
            }
        }

        public string LockedInEmail = "";
        private async void LockInEmailButton_Click(object sender, EventArgs e)
        {
            if (EmailTextBox.Text == "")
            {

            }
            else
            {
                LockedInEmail = EmailTextBox.Text;
                LockedInLabel.Text = "Locked In: " + LockedInEmail;
                EmailTextBox.Text = "";
            }
        }

        private bool GenerateCode = false;
        private bool Import = false;
        private async void GenerateCodeCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (Import == false)
            {
                if (GenerateCodeSettings.GitHubUsername == "" && GenerateCodeCheckBox.Checked == true && GenerateCode == false)
                {
                    bool Release = false;
                    while (Release == false)
                    {
                        await Task.Delay(10);
                        GenerateCodeSettings s = new GenerateCodeSettings();
                        s.ShowDialog();
                        Console.WriteLine(GenerateCodeSettings.GitHubUsername);
                        if (GenerateCodeSettings.GitHubUsername == "")
                        {
                            MessageBox.Show("Username cannot be blank");
                        }
                        else
                        {
                            Release = true;
                            GenerateCode = true;
                        }
                    }
                    GenerateCodeCheckBox.Checked = true;
                }
                else if (GenerateCode == true)
                {
                    GenerateCode = false;
                    GenerateCodeCheckBox.Checked = false;
                    GenerateCodeSettings.GitHubUsername = "";
                } 
            }
            else
            {
                Import = false;
            }
        }

        private bool Pause = false;
        private void PauseButton_Click(object sender, EventArgs e)
        {
            if (Pause == false)
            {
                PauseButton.Text = "Continue";
                PauseButton.Enabled = false;
                Pause = true;
            }
            else
            {
                Pause = false;
                PauseButton.Text = "Pause";
            }
        }

        private async void button3_Click_1(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            IniData data = new IniData();
            SaveFileDialog d = new SaveFileDialog();
            d.DefaultExt = "ini";
            d.ShowDialog();
            if (d.FileName != "")
            {
                data["Directories"]["SourceDirectory"] = textBox1.Text;
                data["Directories"]["GitHubDirectory"] = textBox2.Text;
                data["Settings"]["GenerateCode"] = GenerateCodeCheckBox.Checked.ToString().ToLower();
                data["Settings"]["Base64AllFiles"] = Automation_Settings.Base64.ToString().ToLower();
                data["Settings"]["EncryptAllFiles"] = Automation_Settings.Encrypt.ToString().ToLower();
                data["Settings"]["DecryptionKey"] = Automation_Settings.DecryptionKey;
                data["Settings"]["ShowCommandWindow"] = ShowCommandCheckBox.Checked.ToString().ToLower();
                data["Settings"]["SmartMode"] = SmartModeCheckBox.Checked.ToString().ToLower();
                data["Settings"]["CopyFilesInstead"] = CopyFilesCheckBox.Checked.ToString().ToLower();
                data["Settings"]["ShutdownWhenFinished"] = ShutdownCheckbox.Checked.ToString().ToLower();
                data["Settings"]["SkipErrors"] = SkipErrorsTextBox.Checked.ToString().ToLower();
                data["Settings"]["FilesToUploadAtATime"] = NumberOfFilesToUploadTextBox.Text;
                data["Settings"]["AlwaysOnTop"] = AlwaysOnTopCheckBox.Checked.ToString().ToLower();
                data["Settings"]["CustomCommitMessage"] = CommitMessageTextBox.Text;
                data["Settings"]["GitHubUsername"] = GitHubUsername;
                parser.WriteFile(d.FileName,data);
            }
        }

        private async void button2_Click_1(object sender, EventArgs e)
        {
            var parser = new FileIniDataParser();
            OpenFileDialog d = new OpenFileDialog();
            d.DefaultExt = "ini";
            d.ShowDialog();
            IniData data = parser.ReadFile(d.FileName);
            if (d.FileName != "")
            {
                Import = true;
                textBox1.Text = data["Directories"]["SourceDirectory"];
                textBox2.Text = data["Directories"]["GitHubDirectory"];
                GenerateCodeCheckBox.Checked = bool.Parse(data["Settings"]["GenerateCode"]);
                Automation_Settings.Base64 = bool.Parse(data["Settings"]["Base64AllFiles"]);
                Automation_Settings.Encrypt = bool.Parse(data["Settings"]["EncryptAllFiles"]);
                Automation_Settings.DecryptionKey = data["Settings"]["DecryptionKey"];
                ShowCommandCheckBox.Checked = bool.Parse(data["Settings"]["ShowCommandWindow"]);
                SmartModeCheckBox.Checked = bool.Parse(data["Settings"]["SmartMode"]);
                CopyFilesCheckBox.Checked = bool.Parse(data["Settings"]["CopyFilesInstead"]);
                ShutdownCheckbox.Checked = bool.Parse(data["Settings"]["ShutdownWhenFinished"]);
                SkipErrorsTextBox.Checked= bool.Parse(data["Settings"]["SkipErrors"]);
                NumberOfFilesToUploadTextBox.Text = data["Settings"]["FilesToUploadAtATime"];
                AlwaysOnTopCheckBox.Checked = bool.Parse(data["Settings"]["AlwaysOnTop"]);
                CommitMessageTextBox.Text = data["Settings"]["CustomCommitMessage"];
                GitHubUsername = data["Settings"]["GitHubUsername"];
            }
        }

        private void button2_Click_2(object sender, EventArgs e)
        {
            GitHub_Tools d = new GitHub_Tools();
            d.ShowDialog();
            if(GitHub_Tools.NewRepositoryPath != "")
            {
                textBox1.Text = GitHub_Tools.NewRepositoryPath;
            }
        }
    }
}

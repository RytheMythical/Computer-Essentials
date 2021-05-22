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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EO.WebBrowser;
using Main_Tools.Properties;

namespace Main_Tools
{
    public partial class InternetCheck : Form
    {
        public InternetCheck()
        {
            InitializeComponent();
            Closing += InternetCheck_Closing;
        }

        private bool ClosingThing = false;
        private void InternetCheck_Closing(object sender, CancelEventArgs e)
        {
            if (ClosingThing == false)
            {
                e.Cancel = true;
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

        public static string InternetServiceProvider = "";
        private async void InternetCheck_Load(object sender, EventArgs e)
        {
            try
            {
                StartPosition = FormStartPosition.CenterScreen;
                Visible = false;
                ShowInTaskbar = false;
                TopMost = true;
                ControlBox = false;
                FormBorderStyle = FormBorderStyle.FixedSingle;
                if (!Directory.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest"))
                {
                    Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest");
                }

                try
                {
                    File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Main.zip",
                        Resources.SpeedTest);
                    ZipFile.ExtractToDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Main.zip",
                        Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest");
                }
                catch (Exception exception)
                {

                }

                string LicenseFile = Environment.GetEnvironmentVariable("TEMP") +
                                     "\\AppData\\Roaming\\Ookla\\Speedtest CLI\\speedtest-cli.ini";
                if (!File.Exists(LicenseFile))
                {
                    await RunCommandHidden("cd \"" + Environment.GetEnvironmentVariable("TEMP") +
                                           "\\SpeedTest\necho yes|speedtest.exe");
                }
                await RunCommandHidden("cd \"" + Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest" +
                                       "\"\n > " +
                                       Environment.GetEnvironmentVariable("TEMP") + "\\SpeedTest\\Log.txt (" +
                                       "\necho YES|speedtest.exe\n)");
                string URL = "";
                string ISP = "";
                foreach (var readLine in File.ReadLines(Environment.GetEnvironmentVariable("TEMP") +
                                                        "\\SpeedTest\\Log.txt"))
                {
                    if (readLine.Contains("Result URL"))
                    {
                        URL = readLine.Replace("Result URL:", "");
                        URL = URL.Replace(" ", "");
                    }

                    if (readLine.Contains("ISP"))
                    {
                        ISP = readLine.Replace("       ISP: ", "");
                        ISP = readLine.Replace(" ", "");
                    }
                }

                if (ISP.Contains("Carry") || ISP.Contains("carry"))
                {
                    await PlaySoundSync(Resources.CarryTelecom);
                }
                else if (ISP.Contains("Rogers") || ISP.Contains("rogers"))
                {
                    await PlaySoundSync(Resources.RogersISP);
                }
                else if (ISP.Contains("Fido") || ISP.Contains("fido"))
                {
                    await PlaySoundSync(Resources.Fido);
                }
                else if (ISP.Contains("Bell") || ISP.Contains("bell"))
                {
                    await PlaySoundSync(Resources.BellCanada);
                }
                else if (ISP.Contains("Virgin Mobile") || ISP.Contains("Virgin") || ISP.Contains("virgin"))
                {
                    await PlaySoundSync(Resources.VirginMobile);
                }
                else if (ISP == "")
                {
                    await PlaySoundSync(Resources.NotConnected);
                }
                else
                {
                    await PlaySoundSync(Resources.ConnectedToVPN);
                }
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ISP",ISP);
                InternetServiceProvider = ISP;
                string ServiceProvider(string input)
                {
                    string Return = "";
                    if (input == "")
                    {
                        Return = "NO INTERNET";
                    }
                    else
                    {
                        Return = input;
                    }

                    return Return;
                }

                ServiceProviderLabel.Text = "Your internet service provider: " + ServiceProvider(ISP);
                EO.WebBrowser.Runtime.AddLicense(
                    "6A+frfD09uihfsay4Q/lW5f69h3youbyzs2xaqW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaaW0s8uud7Pl9Q+frfD09uihfsay6BvlW5f69h3youbyzs2xaqW0s8uud7Oz8hfrqO7CzRrxndz22hnlqJfo8h/kdpm1wNyuaae0ws2frOzm1iPvounpBOzzdpm1wNyucrC9ys2fr9z2BBTup7Smw82faLXABBTmp9j4Bh3kd+T20tbFiajL4fPRoenW2RX4ksbS4hK8drOzBBTmp9j4Bh3kd7Oz/RTinuX39ul14+30EO2s3MLNF+ic3PIEEMidtbXE3rZ1pvD6DuSn6unaD7112PD9GvZ3s+X1D5+t8PT26KF+xrLUE/Go5Omzy/We6ff6Gu12mbbB2a9bl7PP5+Cd26QFJO+etKbW+q183/YAGORbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFppbSzy653s+X1D5+t8PT26KF+xrLoEOFbl/r2HfKi5vLOzbFqpbSzy653s+X1D5+t8PT26KF+xrLhD+Vbl/r2HfKi5vLOzbFppbSzy653s+X1");
                WebView Browse = new WebView();
                if (URL == "")
                {
                    ContactRogers dRogers = new ContactRogers();
                    dRogers.ShowDialog();
                }
                Browse.Create(webControl1.Handle);
                Browse.Url = URL;
                SoundPlayer dew = new SoundPlayer(Resources.SpeedTest1);
                dew.PlaySync();
                Visible = true;
                for (int i = 30; i > 0; i = i - 1)
                {
                    button1.Text = i.ToString();
                    await Task.Delay(1000);
                }

                button1.Text = "0";
                ClosingThing = true;
                Close();
            }
            catch
            {
                Visible = false;
                ContactRogers f = new ContactRogers();
                f.ShowDialog();
                Close();
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
            //File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ClosingThing = true;
            Close();
        }

        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void MbpsTextBox_TextChanged(object sender, EventArgs e)
        {
            try
            {
                BigBTextBox.Text = (Int32.Parse(MbpsTextBox.Text) / 8d).ToString("0.00");
            }
            catch (Exception exception)
            {
                BigBTextBox.Text = "Invalid Number";
            }
        }

        private void BigBTextBox_TextChanged(object sender, EventArgs e)
        {
            
        }
    }
}

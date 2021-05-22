using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace OntarioCaseGetter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string MainReadDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\DownloadedOntarioCases";
        private string MainDirectory = Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirus";
        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {


                Visible = false;
                Directory.CreateDirectory(MainReadDirectory);
                if (!File.Exists(MainReadDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString()) || !File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirus\\Requester.txt"))
                {
                    //var appName = Process.GetCurrentProcess().ProcessName + ".exe";
                    //SetIE8KeyforWebBrowserControl(appName);
                    Directory.CreateDirectory(MainDirectory);
                    TopMost = true;
                    FormBorderStyle = FormBorderStyle.FixedSingle;
                    ShowInTaskbar = false;
                    webBrowser1.Navigate("https://www.ontario.ca/page/how-ontario-is-responding-covid-19");
                    bool CompletedLoaded = false;
                    bool DocumentCompleted = false;
                    webBrowser1.Navigated += async (o, args) =>
                    {
                        await Task.Delay(1000);
                        CompletedLoaded = true;
                    };
                    webBrowser1.DocumentCompleted += async (o, args) =>
                    {
                        await Task.Delay(1000);
                        DocumentCompleted = true;
                    };
                    while (CompletedLoaded == false || DocumentCompleted == false)
                    {
                        await Task.Delay(10);
                    }

                    while (webBrowser1.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                    Console.WriteLine("HA");
                    var DocumentContent = webBrowser1.Document.GetElementsByTagName("html")[0];
                    var html = DocumentContent.OuterHtml;
                    while (true)
                    {
                        await Task.Delay(500);
                        if (webBrowser1.IsBusy)
                            continue;
                        var htmlNow = DocumentContent.OuterHtml;
                        if (htmlNow == html && htmlNow.Contains("new cases"))
                            break;
                        html = htmlNow;
                        Console.WriteLine(html);
                    }
                    File.WriteAllText(MainDirectory + "\\Requester.txt", html);
                    if (DateTime.Now.Hour >= 10 && DateTime.Now.Minute >= 30)
                    {
                        File.WriteAllText(MainReadDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(), "Done");
                        File.WriteAllText(MainReadDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString(),"Recorded");
                    }
                }



                Visible = false;

            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
            }

            Application.Exit();
        }

        private void SetIE8KeyforWebBrowserControl(string appName)
        {
            RegistryKey Regkey = null;
            try
            {
                // For 64 bit machine
                if (Environment.Is64BitOperatingSystem)
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Wow6432Node\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
                else  //For 32 bit machine
                    Regkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);

                // If the path is not correct or
                // if the user haven't priviledges to access the registry
                if (Regkey == null)
                {
                    //MessageBox.Show("Application Settings Failed - Address Not found");
                    return;
                }

                string FindAppkey = Convert.ToString(Regkey.GetValue(appName));

                // Check if key is already present
                if (FindAppkey == "8000")
                {
                    //MessageBox.Show("Required Application Settings Present");
                    Regkey.Close();
                    return;
                }

                // If a key is not present add the key, Key value 8000 (decimal)
                if (string.IsNullOrEmpty(FindAppkey))
                    Regkey.SetValue(appName, unchecked((int)0x1F40), RegistryValueKind.DWord);

                // Check for the key after adding
                FindAppkey = Convert.ToString(Regkey.GetValue(appName));

               
            }
            catch (Exception ex)
            {
            }
            finally
            {
                // Close the Registry
                if (Regkey != null)
                    Regkey.Close();
            }
        }
    }
}

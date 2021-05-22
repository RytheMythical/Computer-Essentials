using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTMLDownloadResponder
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern IntPtr SendMessage(IntPtr hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        private void Deactive()
        {
            IntPtr hwnd = FindWindow(null, "Message from webpage");
            hwnd = FindWindowEx(hwnd, IntPtr.Zero, "Button", "OK");
            uint message = 0xf5;
            SendMessage(hwnd, message, IntPtr.Zero, IntPtr.Zero);
        }
        public Form1()
        {
            InitializeComponent();
            Load += Form1_Load1;
        }

        private async void Form1_Load1(object sender, EventArgs e)
        {
            while (true)
            {
                await Task.Delay(10);
                
            }
        }

        private string ResponseFile = Environment.GetEnvironmentVariable("TEMP") + "\\HTMLDownloadResponseFile.txt";

        private async void Form1_Load(object sender, EventArgs e)
        {
            ShowInTaskbar = false;
            Visible = false;
            if (File.Exists(ResponseFile))
            {
                string Link = File.ReadAllText(ResponseFile);
                //File.Delete(ResponseFile);
                webBrowser1.ScriptErrorsSuppressed = true;
                var html = "";
                webBrowser1.DocumentCompleted += (o, args) =>
                {
                    Console.WriteLine("COMPLETED");
                    System.Windows.Forms.SendKeys.Send("{Enter}{Enter}");
                    html = webBrowser1.Document.GetElementsByTagName("HTML")[0].OuterHtml;
                };
                webBrowser1.Navigated += async (o, args) =>
                {
                    Console.WriteLine("Navigated");
                    await Task.Delay(3000);
                    SendKeys.Send("{Enter}{Enter}");
                };
                webBrowser1.Navigate(Link);
                Console.WriteLine("Done navigating");
                bool NavigatedHui = false;
                webBrowser1.Navigated += (o, args) =>
                {
                    NavigatedHui = true;
                };
                while (NavigatedHui == false)
                {
                    Deactive();
                    await Task.Delay(10);
                }

                while (webBrowser1.IsBusy)
                {
                    await Task.Delay(10);
                }
                Console.WriteLine("Not busy");
                while (webBrowser1.DocumentText == "")
                {
                    await Task.Delay(10);
                }
                var DocumentContent = webBrowser1.Document.GetElementsByTagName("HTML")[0];
                var htmlha = DocumentContent.OuterHtml;
                while (true)
                {
                    await Task.Delay(500);
                    if (webBrowser1.IsBusy)
                        continue;
                    var htmlNow = DocumentContent.OuterHtml;
                    if (htmlNow == htmlha)
                    {
                        await Task.Delay(1000);
                        break;
                    }

                    htmlha = htmlNow;
                    Console.WriteLine(htmlha);
                }
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\GotHTML.txt", htmlha);
                Console.WriteLine("Have some content");
            }

            Visible = true;
            Application.Exit();
        }
    }
}

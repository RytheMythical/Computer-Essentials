using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Fan_Spinner.Properties;

namespace Fan_Spinner
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }
        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            var c = 0;
            var f = 0;
            for (int i = 0; i < 100; i++)
            {
                string TempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                BackgroundWorker SpinnerWorker = new BackgroundWorker();
                SpinnerWorker.DoWork += (o, args) =>
                {
                    File.WriteAllBytes(TempFile,Resources.Spinner);
                    Encryption.FileEncrypt(TempFile,"spin");
                    File.Delete(TempFile);
                    File.Delete(TempFile + ".aes");
                    c++;
                };
                SpinnerWorker.RunWorkerAsync();
            }


            while (c <= 99)
            {
                await Task.Delay(10);
            }
            Application.Exit();
        }

        public static void ConsumeCPU(int percentage)
        {
            if (percentage < 0 || percentage > 100)
                throw new ArgumentException("percentage");
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (true)
            {
                // Make the loop go on for "percentage" milliseconds then sleep the 
                // remaining percentage milliseconds. So 40% utilization means work 40ms and sleep 60ms
                if (watch.ElapsedMilliseconds > percentage)
                {
                    Thread.Sleep(100 - percentage);
                    watch.Reset();
                    watch.Start();
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace API_Grabber
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            if (Directory.Exists(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\GitHub\\APIs\\APIs"))
            {
                string RandomNumber = new Random().Next(111,999).ToString();
                Directory.CreateDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\API" + RandomNumber);
                DirectoryInfo d = new DirectoryInfo(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\GitHub\\APIs\\APIs");
                foreach (var directoryInfo in d.GetDirectories())
                {
                    try
                    {
                        DirectoryInfo Inside = new DirectoryInfo(directoryInfo.FullName + "\\bin\\Debug\\netstandard2.0");
                        foreach (var fileInfo in Inside.GetFiles())
                        {
                            if (fileInfo.FullName.Contains(".dll") && fileInfo.FullName.Contains(directoryInfo.Name))
                            {
                                try
                                {
                                    File.Copy(fileInfo.FullName,
                                        Environment.GetEnvironmentVariable("TEMP") + "\\API" + RandomNumber + "\\" +
                                        fileInfo.Name);
                                }
                                catch (Exception hui)
                                {
                                    Console.WriteLine(hui);
                                }
                            }
                        }
                    }
                    catch (Exception dew)
                    {
                        Console.WriteLine(dew);
                    }
                }
                SaveFileDialog dd = new SaveFileDialog();
                dd.DefaultExt = "zip";
                dd.ShowDialog();
                ZipFile.CreateFromDirectory(Environment.GetEnvironmentVariable("TEMP") + "\\API" + RandomNumber,dd.FileName);
                Close();
            }

            Application.Exit();
        }
    }
}

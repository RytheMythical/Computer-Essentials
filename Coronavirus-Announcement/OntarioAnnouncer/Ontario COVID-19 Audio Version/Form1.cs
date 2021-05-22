using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EricOulashin;
using Ontario_COVID_19_Audio_Version.Properties;
using Xabe.FFmpeg;

namespace Ontario_COVID_19_Audio_Version
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string Downloads = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads";

        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        public string SpeakToWave(string Message)
        {
            string TempPath = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".wav";
            SpeechSynthesizer s = new SpeechSynthesizer();
            s.SelectVoiceByHints(VoiceGender.Female);
            s.SetOutputToWaveFile(TempPath);
            s.Speak(Message);
            return TempPath;
        }
        public string AddSound(Stream sound)
        {
            string TempPath = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".wav";
            SaveStreamToFile(TempPath,sound);
            return TempPath;
        }
        public void SaveStreamToFile(string fileFullPath, Stream stream)
        {
            if (stream.Length == 0) return;

            // Create a FileStream object to write a stream to a file
            using (FileStream fileStream = System.IO.File.Create(fileFullPath, (int)stream.Length))
            {
                // Fill the bytes[] array with the stream data
                byte[] bytesInStream = new byte[stream.Length];
                stream.Read(bytesInStream, 0, (int)bytesInStream.Length);

                // Use FileStream object to write to the specified file
                fileStream.Write(bytesInStream, 0, bytesInStream.Length);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<string> Stuff = new List<string>();
            Stuff.Add(AddSound(Resources.OntarioNewCasesSP)) ;
            Stuff.Add(SpeakToWave("No cases today"));
            string Video = @"C:\Users\BigHead\Downloads\Video\ezgif-6-38bdcedcea3c.mp4";
            FFmpeg.SetExecutablesPath("C:\\Users\\BigHead\\Downloads\\Video");
            await FFmpeg.Conversions.FromSnippet.AddAudio(Video, AddSound(Resources.OntarioNewCasesSP), Downloads + "\\Output.wav");
        }
    }
}

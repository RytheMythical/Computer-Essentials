using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Speech.Synthesis;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Concatenation_Waves;
using EricOulashin;
using NAudio.Wave;
using Ontario_COVID_19_Audio_Version.Properties;

namespace Ontario_COVID_19_Audio_Version
{
    public partial class Audio_Creator : Form
    {
        public Audio_Creator()
        {
            InitializeComponent();
            Closing += (sender, args) =>
            {
                if (Directory.Exists(SessionFolder)) Directory.Delete(SessionFolder, true);
            };
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            List<string> Stuff = new List<string>();
            Directory.CreateDirectory(SessionFolder);
            Stuff.Add(AddSound(Resources.OntarioNewCasesSP));
            Stuff.Add(SpeakToWave("Testing"));
            await Task.Delay(1000);
            WAVFile.MergeAudioFiles(Stuff.OfType<string>().ToArray(), Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads\\Testing3.wav",Path.GetTempPath());
            new WaveIO().Merge(Stuff.OfType<string>().ToArray(), Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads\\Testing2.wav");
            Concatenate(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Downloads\\Testing.wav",Stuff);
        }

        public static void Concatenate(string outputFile, IEnumerable<string> sourceFiles)
        {
            byte[] buffer = new byte[1024];
            WaveFileWriter waveFileWriter = null;

            try
            {
                foreach (string sourceFile in sourceFiles)
                {
                    using (WaveFileReader reader = new WaveFileReader(sourceFile))
                    {
                        if (waveFileWriter == null)
                        {
                            // first time in create new Writer
                            waveFileWriter = new WaveFileWriter(outputFile, reader.WaveFormat);
                        }
                        else
                        {
                            //if (!reader.WaveFormat.Equals(waveFileWriter.WaveFormat))
                            //{
                            //    throw new InvalidOperationException("Can't concatenate WAV Files that don't share the same format");
                            //}
                        }

                        int read;
                        while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            waveFileWriter.WriteData(buffer, 0, read);
                        }
                    }
                }
            }
            finally
            {
                if (waveFileWriter != null)
                {
                    waveFileWriter.Dispose();
                }
            }

        }

        public string SpeakToWave(string Message)
        {
            string TempPath = SessionFolder + "\\" + Path.GetRandomFileName().Replace(".", "") + ".wav";
            string TempPathNormal = SessionFolder + "\\" + Path.GetRandomFileName().Replace(".", "") + "Normal.wav";
            SpeechSynthesizer speak = new SpeechSynthesizer();
            speak.SetOutputToWaveFile(TempPath);
            speak.Speak(Message);
            speak.Dispose();
            NormalizeAudio(TempPath, TempPathNormal);
            return TempPathNormal;
        }

        public string AddSound(Stream sound)
        {
            string TempPath = SessionFolder + "\\" + Path.GetRandomFileName().Replace(".", "") + ".wav";
            string TempPathNormal = SessionFolder + "\\" + Path.GetRandomFileName().Replace(".", "") + "Normal.wav";
            SaveStreamToFile(TempPath,sound);
            NormalizeAudio(TempPath,TempPathNormal);
            return TempPathNormal;
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
        string SessionStore = "";
        string SessionFolder
        {
            get
            {
                return SessionStore == "" ? SessionStore = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") : SessionStore;
            }
        }

        public void NormalizeAudio(string inP,string outP)
        {
            var inPath = inP;
            var outPath = outP;
            float max = 0;

            using (var reader = new AudioFileReader(inPath))
            {
                // find the max peak
                float[] buffer = new float[reader.WaveFormat.SampleRate];
                int read;
                do
                {
                    read = reader.Read(buffer, 0, buffer.Length);
                    for (int n = 0; n < read; n++)
                    {
                        var abs = Math.Abs(buffer[n]);
                        if (abs > max) max = abs;
                    }
                } while (read > 0);
                Console.WriteLine($"Max sample value: {max}");

                if (max == 0 || max > 1.0f)
                    throw new InvalidOperationException("File cannot be normalized");

                // rewind and amplify
                reader.Position = 0;
                reader.Volume = 1.0f / max;

                // write out to a new WAV file
                WaveFileWriter.CreateWaveFile16(outPath, reader);
            }
        }
    }

}

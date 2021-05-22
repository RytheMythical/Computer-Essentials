using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.AudioFormat;
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
using NAudio.Wave;

namespace Voice_Chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var desktopWorkingArea = System.Windows.SystemParameters.WorkArea;
            this.Left = SystemParameters.PrimaryScreenWidth - this.Width;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private async Task SpeakToVoice(string message)
        {
            SpeechSynthesizer Speak = new SpeechSynthesizer();
            using (MemoryStream stream = new MemoryStream())
            {
                SoundPlayer Player = new SoundPlayer();
                Speak.SetOutputToWaveStream(stream);
                Speak.Speak(message);
                Player.Stream = stream;
                Player.PlaySync();
                SpeechAudioFormatInfo d = new SpeechAudioFormatInfo(EncodingFormat.Pcm, 88200, 16, 1, 16000, 2, null);
                
            }
        }
    }
}

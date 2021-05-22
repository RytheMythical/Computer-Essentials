using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
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
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace Recordings_Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            VideoPlayer.MediaOpened += VideoPlayer_MediaOpened;
            DateSelectionBox.SelectionChanged += DateSelectionBox_SelectionChanged;
            VideoPlayer.Position = TimeSpan.ParseExact("00.00.00", "hh'.'mm'.'ss", null);
            Loaded += MainWindow_Loaded;
        }
        SnackbarMessageQueue Queue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
        private async Task ShowUniversalSnackBar(string Message)
        {
            UniversalSnackbar.MessageQueue = Queue;
            Queue.Enqueue(Message);
        }

        private async Task ShowUniversalSnackBar(string Message,string ButtonContent,Action Button)
        {
            UniversalSnackbar.MessageQueue = Queue;
            Queue.Enqueue(Message,ButtonContent,Button);
        }
        private void DateSelectionBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(DateSelectionBox.SelectedItem);
            PeriodComboBox.Items.Clear();
            PeriodComboBox.Text = "";
            DirectoryInfo d = new DirectoryInfo(RecordingsDirectory + "\\" + DateSelectionBox.SelectedItem);
            foreach (var fileInfo in d.GetFiles())
            {
                if (fileInfo.FullName.Contains("Period 1"))
                {
                    PeriodComboBox.Items.Add("Period 1");
                }
                if (fileInfo.FullName.Contains("Period 2"))
                {
                    PeriodComboBox.Items.Add("Period 2");
                }
                if (fileInfo.FullName.Contains("Period 3"))
                {
                    PeriodComboBox.Items.Add("Period 3");
                }
                if (fileInfo.FullName.Contains("Period 4"))
                {
                    PeriodComboBox.Items.Add("Period 4");
                }
            }
        }
        string RecordingsDirectory = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\RecordingsSavings.txt");
        TimeSpan Duration = new TimeSpan();
        private void VideoPlayer_MediaOpened(object sender, RoutedEventArgs e)
        {
            Duration = VideoPlayer.NaturalDuration.TimeSpan;
            VideoPlayer.LoadedBehavior = MediaState.Manual;
            VideoPlayer.UnloadedBehavior = MediaState.Manual;
            VideoPlayer.Play();
            PlayButton.Content = "Play";
            Paused = false;
        }


        private async void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string date in GetDates)
            {
                Console.WriteLine(date);
                DateSelectionBox.Items.Add(date);
            }
            KeepSize();
            ShowVideo(false);
            await ShowUniversalSnackBar("Program Loaded");
        }

        private void ShowVideo(bool Show)
        {
            if (Show == false)
            {
                SelectionGrid.Visibility = Visibility.Visible;
                VideoControlsGrid.Visibility = Visibility.Hidden;
                VideoPlayer.Visibility = Visibility.Hidden;
            }
            else
            {
                SelectionGrid.Visibility = Visibility.Hidden;
                VideoControlsGrid.Visibility = Visibility.Visible;
                VideoPlayer.Visibility = Visibility.Visible;
            }
        }
        bool Paused = true;
        private async Task KeepSize()
        {
            while (true)
            {
                PlayButton.IsEnabled = Paused;
                PauseButton.IsEnabled = !Paused;
                if (Paused == false)
                {
                    HoursTextBox.Text = VideoPlayer.Position.Hours.ToString("00");
                    MinutesTextBox.Text = VideoPlayer.Position.Minutes.ToString("00");
                    SecondsTextBox.Text = VideoPlayer.Position.Seconds.ToString("00");
                }
                DurationLabel.Content = " / " + Duration.Hours.ToString("00") + ":" + Duration.Minutes.ToString("00") + ":" + Duration.Seconds.ToString("00");
                WindowStyle = WindowStyle.None;
                await Task.Delay(10);
                VideoPlayer.Width = Width + 300;
                VideoPlayer.Height = Height - 150;
                VideoControlsGrid.Margin = new Thickness(0, VideoPlayer.Height + VideoControlsGrid.Height, 0, 0);
            }
        }
        private string[] GetDates
        {
            get
            {
                string[] Return;
                List<string> ReturnList = new List<string>();
                DirectoryInfo d = new DirectoryInfo(File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\RecordingsSavings.txt"));
                foreach (var directoryInfo in d.GetDirectories())
                {
                    ReturnList.Add(directoryInfo.Name);
                }
                Return = ReturnList.OfType<string>().ToArray();
                return Return;
            }
        }

        private void SeekVideo(TimeSpan time)
        {
            VideoPlayer.Position = time;
        }
        private async Task LoadRecording(string Date, int Period)
        {
            string FilePath = File.ReadAllText(Environment.GetEnvironmentVariable("APPDATA") + "\\PeriodRotations\\RecordingsSavings.txt") + "\\" + Date + "\\Period " + Period.ToString() + ".mp4";
            VideoPlayer.Source = new Uri(FilePath);
            VideoPlayer.Play();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Paused = false;
            VideoPlayer.Play();
            try
            {
                VideoPlayer.Position = TimeSpan.ParseExact(HoursTextBox.Text + "." + MinutesTextBox.Text + "." + SecondsTextBox.Text, "hh'.'mm'.'ss", null);
            }
            catch
            {
                HoursTextBox.Text = VideoPlayer.Position.Hours.ToString("00");
                MinutesTextBox.Text = VideoPlayer.Position.Minutes.ToString("00");
                SecondsTextBox.Text = VideoPlayer.Position.Seconds.ToString("00");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Paused = true;
            VideoPlayer.Pause();
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            DateSelectionBox.SelectedItem = null;
            VideoPlayer.Stop();
            ShowVideo(false);
        }

        private async void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                string VideoFile = RecordingsDirectory + "\\" + DateSelectionBox.SelectedItem.ToString() + "\\" + PeriodComboBox.Text + ".mp4";
                if (File.Exists(VideoFile))
                {
                    VideoPlayer.Source = new Uri(VideoFile);
                    PlayButton.Content = "Loading";
                    ShowVideo(true);
                }
            }
            catch 
            {
                
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private async void SaveVideoButton_Click(object sender, RoutedEventArgs e)
        {
            SaveVideoButton.IsEnabled = false;
            SaveVideoButton.Content = "Saving..";
            SaveFileDialog Save = new SaveFileDialog();
            Save.DefaultExt = "mp4";
            Save.ShowDialog();
            try
            {
                string VideoFile = RecordingsDirectory + "\\" + DateSelectionBox.SelectedItem.ToString() + "\\" +
                                   PeriodComboBox.Text + ".mp4";
                if (File.Exists(VideoFile))
                {
                    BackgroundWorker d = new BackgroundWorker();
                    d.DoWork += (o, args) => { File.Copy(VideoFile, Save.FileName); };
                    d.RunWorkerAsync();
                    while (d.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
            catch (Exception exception)
            {

            }

            SaveVideoButton.Content = "Save Video";
            SaveVideoButton.IsEnabled = true;
        }
    }
}

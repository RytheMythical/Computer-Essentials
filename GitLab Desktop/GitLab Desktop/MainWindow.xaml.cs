using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using GitLabApiClient;
using GitLabApiClient.Models.Projects.Requests;
using GitLabApiClient.Models.Projects.Responses;
using MaterialDesignThemes.Wpf;
using ParseHub.Client;

namespace GitLab_Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WindowStyle = WindowStyle.SingleBorderWindow;
            RepositoriesGrid.Visibility = Visibility.Hidden;
        }

        GitLabClient GitLab;

        SnackbarMessageQueue Queue = new SnackbarMessageQueue(TimeSpan.FromSeconds(3));
        private async Task ShowUniversalSnackBar(string Message)
        {
            UniversalSnackBar.MessageQueue = Queue;
            Queue.Enqueue(Message);
        }

        private async Task ShowUniversalSnackBar(string Message, string ButtonContent, Action ButttonAction)
        {
            UniversalSnackBar.MessageQueue = Queue;
            Queue.Enqueue(Message, ButtonContent, ButttonAction);
        }

        private async Task RefreshRepositories()
        {
            foreach (DirectoryInfo directoryInfo in new DirectoryInfo(LocalGitLabFolder).GetDirectories())
            {
                LocalRepositoriesList.Items.Add(directoryInfo.Name);
            }
            List<Project> Projects = new List<Project>();
            BackgroundWorker Getter = new BackgroundWorker();
            Getter.DoWork += async (sender, args) =>
            {
                Projects = (List<Project>)await GitLab.Projects.GetAsync();
            };
            foreach (var gitLabProject in Projects)
            {
                CloudRepositoriesListBox.Items.Add(gitLabProject.Name);
            }
        }
        string LocalGitLabFolder = Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\GitLab";
        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginButton.IsEnabled = false;
            if (PersonalAccessTokenTextBox.Text != "")
            {
                try
                {
                    GitLab = new GitLabClient("https://gitlab.com/", PersonalAccessTokenTextBox.Text);
                    if (!Directory.Exists(LocalGitLabFolder)) Directory.CreateDirectory(Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\GitLab");
                    await RefreshRepositories();
                    await ShowUniversalSnackBar("Logged in successfully");
                    // End of code here //
                    LoginGrid.Visibility = Visibility.Hidden;
                    RepositoriesGrid.Visibility = Visibility.Visible;

                }
                catch (Exception)
                {
                    await ShowUniversalSnackBar("Incorrect Personal Access Token / OAuth Token");
                }
            }
            LoginButton.IsEnabled = true;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GitHUB_API;

namespace GitHub_Large_Uploader
{
    public partial class GitHub_Tools : Form
    {
        public GitHub_Tools()
        {
            InitializeComponent();
            Load += GitHub_Tools_Load;
        }

        private void GitHub_Tools_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
        }

        private async void PersonalAccessLoginButton_Click(object sender, EventArgs e)
        {
            PersonalAccessLoginButton.Enabled = false;
            GitHub Git = new GitHub(PersonalAccessTokenTextBox.Text);
            await Git.LoginToGit();
            await Git.LoginToGitHubCLI();
            PersonalAccessLoginButton.Enabled = true;
            PersonalAccessTokenTextBox.Text = "";
        }
        public static string NewRepositoryPath = "";
        private async void CreateRepositoryButton_Click(object sender, EventArgs e)
        {
            CreateRepositoryButton.Enabled = false;
            GitHub Git = new GitHub(PersonalAccessTokenTextBox.Text);
            string RepoPath = Environment.GetEnvironmentVariable("TEMP");
            await Git.CreateGitHubRepository(NewRepositoryNameTextBox.Text, RepoPath);
            NewRepositoryPath = RepoPath + "\\" + NewRepositoryNameTextBox.Text;
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void GenerateRepositoryButton_Click(object sender, EventArgs e)
        {
            GenerateRepositoryButton.Enabled = false;
            GitHub Git = new GitHub(PersonalAccessTokenTextBox.Text);
            string RepoName = "Repo" + new Random().Next(1111, 9999).ToString();
            string RepoPath = Environment.GetEnvironmentVariable("TEMP");
            await Git.CreateGitHubRepository(RepoName, RepoPath);
            NewRepositoryPath = RepoPath + "\\" + RepoName;
            Close();
        }

        private async void LogoutButton_Click(object sender, EventArgs e)
        {
            LogoutButton.Enabled = false;
            GitHub Git = new GitHub();
            await Git.LogoutOfGitHub();
            LogoutButton.Enabled = true;
        }
    }
}

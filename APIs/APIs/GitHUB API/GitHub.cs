using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UsefulTools;

namespace GitHUB_API
{
    public class GitHub
    {
        public GitHub(string PersonalAccessToken)
        {
            PersonalAccessTokenDeclare = PersonalAccessToken;
        }

        public GitHub()
        {
            
        }
        private static string PersonalAccessTokenDeclare { get; set; }

        public async Task LoginToGitHubCLI()
        {
            await Command.RunCommandHidden("gh auth logout -h github.com | echo y");
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\tokengithub.txt",PersonalAccessTokenDeclare);
            await Command.RunCommandHidden("gh auth login --with-token < \"%temp%\\tokengithub.txt\"");
        }

        public async Task LoginToGit()
        {
            await Command.RunCommandHidden("cmdkey /generic:LegacyGeneric:target=git:https://github.com /user:username /pass:" + PersonalAccessTokenDeclare);
        }

        public async Task LogoutOfGitHub()
        {
            await Command.RunCommandHidden("cmdkey /delete:LegacyGeneric:target=git:https://github.com");
        }
        public async Task CreateGitHubRepository(string Name, string path)
        {
            string RepoName = Name.Replace(" ", "").Replace("=", "").Replace("-", "");
            await Command.RunCommandHidden("cd \"" + path + "\"\ngh repo create " + RepoName + " --public -y");
            File.WriteAllText(path + "\\" + RepoName + "\\Initialize.txt","Welcome to GitHub!");
            await Command.RunCommandHidden("cd \"" + path + "\\" + RepoName + "\"\ngit add --all\ngit commit -m \"dew\"\ngit push --set-upstream origin master");
        }

        public async Task PushOrigin(string repositorypath,string CommitMessage)
        {
            await Command.RunCommandHidden("cd \"" + repositorypath + "\"\ngit add --all\ngit commit -m\"" + CommitMessage + "\"\ngit push origin");
        }
    }
}

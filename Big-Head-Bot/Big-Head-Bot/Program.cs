using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Coronavirus_API;
using Discord;
using Discord.Addons.Interactive;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Ontario_Coronavirus_API;
using RobloxFriendsAPI;
using Datum = RobloxQuickType.Datum;

namespace Big_Head_Bot
{
    public class Program
    {
        public static void Main(string[] args) => new Program().MainAsync().GetAwaiter().GetResult();

        CommandService Service = new CommandService();
        private DiscordSocketClient _client;
        private IServiceProvider services;

        public async Task MainAsync()
        {
            _client = new DiscordSocketClient();
            _client.Log += Log;
            var Token = "Nzk3Mjg4NDA0MDQ4MDE5NDk3.X_kSsA.-mdvaEirvwFORds8kTh6BKFOyIQ";
            await _client.LoginAsync(TokenType.Bot, Token);
            await _client.StartAsync();
            CommandHandler handler = new CommandHandler(_client, Service);
            await handler.InstallCommandsAsync();
            //HA();
            DynamicFixFlux();
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private async Task HA()
        {
            while (true)
            {
                await _client.SetStatusAsync(UserStatus.Idle);
                await Task.Delay(500);
                await _client.SetStatusAsync(UserStatus.Online);
            }
        }

        private string tannibus = "https://www.roblox.com/users/203539400/profile";
        private string wadwly = "https://www.roblox.com/users/96175801/profile";
        private string sharktooth_6 = "https://www.roblox.com/users/121031778/profile";
        private string FixFlux = "https://www.roblox.com/users/79018517/profile";

        private async Task DynamicFixFlux()
        {
            var Server = _client.GetGuild(id: 797233200316547073);

            void GetServer()
            {
                Server = _client.GetGuild(id: 797233200316547073);
            }

            _client.Ready += async () =>
            {
                GetServer();
                await Server.DownloadUsersAsync();
                Console.WriteLine(Server.IsConnected);
                Console.WriteLine(Server.Name);
                var User = Server.GetUser(340604788104232960);
                Console.WriteLine(User.Nickname);
                BackgroundWorker d = new BackgroundWorker();
                d.DoWork += async (sender, args) =>
                {
                    while (true)
                    {
                        await Task.Delay(1000);
                        await User.ModifyAsync(x => { x.Nickname = CHECKGIRL(GetGirl(FixFlux)) + "fixflux!"; });
                    }
                };
                d.RunWorkerAsync();
            };
            _client.Ready += async () =>
            {
                GetServer();
                await Server.DownloadUsersAsync();
                var WadwlyUser = Server.GetUser(557317197345849344);
                Console.WriteLine(WadwlyUser.Nickname);
                BackgroundWorker d = new BackgroundWorker();
                d.DoWork += async (sender, args) =>
                {
                    while (true)
                    {
                        await Task.Delay(1000);
                        await WadwlyUser.ModifyAsync(x => { x.Nickname = CHECKBIG(GetBigHead(wadwly)) + "wadwly!"; });
                    }
                };
                d.RunWorkerAsync();
            };
            _client.Ready += async () =>
            {
                GetServer();
                await Server.DownloadUsersAsync();
                var WadwlyUser = Server.GetUser(240993166029750272);
                Console.WriteLine(WadwlyUser.Nickname);
                BackgroundWorker d = new BackgroundWorker();
                d.DoWork += async (sender, args) =>
                {
                    while (true)
                    {
                        await Task.Delay(1000);
                        await WadwlyUser.ModifyAsync(x =>
                        {
                            x.Nickname = CHECKBIG(GetBigHead(sharktooth_6)) + "SHARKTOOTH";
                        });
                    }
                };
                d.RunWorkerAsync();
            };
            await Task.Delay(10000);
        }

        private bool GetGirl(string Link)
        {
            bool Return = false;
            try
            {
                string d = "";
                WebClient client = new WebClient();
                BackgroundWorker Get = new BackgroundWorker();
                Get.DoWork += (sender, args) =>
                {
                    try
                    {
                        d = client.DownloadString(Link);
                    }
                    catch
                    {

                    }
                };
                Get.RunWorkerAsync();
                while (Get.IsBusy)
                {
                    Thread.Sleep(10);
                }

                if (d.Contains("https://www.roblox.com/catalog/4684126775/Long-Black-Space-Buns"))
                {
                    Return = true;
                }
                else
                {
                    Return = false;
                }
            }
            catch
            {


            }

            return Return;
        }

        private string CHECKBIG(bool Check)
        {
            string Return = "";
            if (Check)
            {
                Return = "BIG HEAD ";
            }
            else
            {
                Return = "SMALL HEAD ";
            }

            return Return;
        }

        private string CHECKGIRL(bool Check)
        {
            string Return = "";
            if (Check)
            {
                Return = "mommy ";
            }
            else
            {
                Return = "daddy ";
            }

            return Return;
        }

        private bool GetBigHead(string Link)
        {
            bool Return;
            string d = "";
            WebClient client = new WebClient();
            BackgroundWorker Get = new BackgroundWorker();
            Get.DoWork += (sender, args) => { d = client.DownloadString(Link); };
            Get.RunWorkerAsync();
            while (Get.IsBusy)
            {
                Thread.Sleep(10);
            }

            if (d.Contains("https://www.roblox.com/catalog/1048037/Bighead"))
            {
                Return = true;
            }
            else
            {
                Return = false;
            }

            return Return;
        }
    }

    public class CommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;

        public CommandHandler(DiscordSocketClient client, CommandService commands)
        {
            _commands = commands;
            _client = client;
        }

        public async Task InstallCommandsAsync()
        {
            _client.MessageReceived += _client_MessageReceived;
            await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task _client_MessageReceived(SocketMessage arg)
        {
            var message = arg as SocketUserMessage;
            Console.WriteLine(message);
            if (message == null) return;
            int argPos = 0;
            if (!(message.HasCharPrefix('!', ref argPos) || message.HasMentionPrefix(_client.CurrentUser, ref argPos) ||
                  message.Author.IsBot)) return;
            var context = new SocketCommandContext(_client, message);
            await _commands.ExecuteAsync(context: context, argPos: argPos, services: null);
        }
    }

    public class InfoModule : ModuleBase<SocketCommandContext>
    {
        private string tannibus = "https://www.roblox.com/users/203539400/profile";
        private string wadwly = "https://www.roblox.com/users/96175801/profile";
        private string sharktooth_6 = "https://www.roblox.com/users/121031778/profile";
        private string FixFlux = "https://www.roblox.com/users/79018517/profile";

        [Command("say")]
        [Summary("Echoes a message")]
        public Task SayAsync([Remainder] [Summary("The text to echo")] string echo) => ReplyAsync(echo.Replace("!",""));

        [Command("check")]
        [Summary("Checks if someone is big head or small head")]

        public async Task SayTannibus([Remainder] [Summary("Head check")] string echo)
        {
            string SayTheThing = "";
            if (echo == "tannibus" || echo == "tan")
            {

                SayTheThing = CHECKBIG(await GetBigHead(tannibus)) + "TANNIBUS";
            }
            else if (echo == "wadwly" || echo == "wad")
            {
                SayTheThing = CHECKBIG(await GetBigHead(wadwly)) + "WADWLY";
            }
            else if (echo == "Fix" || echo == "fix" || echo == "FixFlux" || echo == "fixflux")
            {
                SayTheThing = CHECKGIRL(await GetGirl(FixFlux)) + "FIXFLUX";
            }
            else if (echo == "shark" || echo == "sharktooth" || echo == "sharktooth_6")
            {
                SayTheThing = CHECKBIG(await GetBigHead(sharktooth_6)) + "SHARKTOOTH";
            }

            ReplyAsync(SayTheThing);
        }

        [Command("ontariocovid")]
        [Summary("Checks todays new covid cases in ontario (updates at 10:30AM everyday)")]

        public async Task OntarioCoronavirus([Remainder] string echo)
        {
            string Return = "";
            ReplyAsync("Loading Data, please wait... Instant Results:tm: by COVID Checker:tm:");
            if (echo == "help")
            {
                Return =
                    "newcases = Get new ontario cases today (Updates at 10:30AM each day)\ndeaths = Get total deaths in ontario\naverage = Get average cases in the last 7 days in ontario\n" +
                    "bypeople = Get all cases with detailed information to who got it\n" +
                    "icu = Get intensive care unit numbers\n" +
                    "hospital = Get hospitalized people\n" +
                    "recoveries = Get total recovered cases in ontario\n" +
                    "all = Gets all detailed information\n" +
                    "\n\n**All Entries are case sensitive**";
            }
            else if (echo == "all")
            {
                Return = "http://bigheados.great-site.net/OntarioCovidCases/" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString() + ".png";
            }
            else if (echo == "newcases")
            {
                Return = await Ontario_Coronavirus.GetTodaysCases() + " new cases in ontario today";
            }
            else if (echo == "deaths")
            {
                Return = await Ontario_Coronavirus.GetTotalDeaths() + " total deaths in ontario";
            }
            else if (echo == "average")
            {
                Return = await Ontario_Coronavirus.GetSevenDayAverage() + " average cases in the last 7 days";
                if (Return.Contains("false"))
                {
                    Return = "Datan't for now, check back later";
                }
            }
            else if (echo == "bypeople")
            {
                Return = await Ontario_Coronavirus.GetCasesByDemographics.EightyAndOver() + " | 80 and over cases\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.SixtyToSeventyNine() + " | 60 - 79 cases\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.FourtyToFiftyNine() + " | 40 - 59 cases\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.TwentytoThirtyNine() + " | 20 - 39 cases\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.NineteenAndUnder() + " | 19 and under\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.Male() + " | male cases\n" +
                         await Ontario_Coronavirus.GetCasesByDemographics.Female() + " | female cases\n";
            }
            else if (echo == "icu")
            {
                Return = await Ontario_Coronavirus.GetICUCases() + " people in intensive care unit";
            }
            else if (echo == "hospital")
            {
                Return = await Ontario_Coronavirus.GetHospitalized() + " hospitalized";
            }
            else if (echo == "recoveries")
            {
                Return = await Ontario_Coronavirus.GetRecoveredCases() + " recoveries";
            }
            else
            {
                Return = "Invalid command";
            }

            ReplyAsync(Return);
        }

        [Command("covid")]

        public async Task GetCovidCases([Remainder] string echo)
        {
            COVID covid = new COVID();
            string Return = "";
            if (echo == "cases")
            {
                Return = covid.TotalCases();
            }
            else if (echo == "recoveries")
            {
                Return = covid.RecoveredCases();
            }
            else if (echo == "critical")
            {
                Return = covid.CriticalCases();
            }
            else if (echo == "active")
            {
                Return = covid.ActiveCases();
            }
            else if (echo == "deaths")
            {
                Return = covid.Deaths();
            }
            else if (echo == "help")
            {
                Return =
                    "cases = Get current worldwide cases\nrecoveries = Get recoveries\ncritical = Get Critical Cases\nactive = Get active cases\ndeaths = Get Total Deaths\nhelp = Help with this command\n\n**ALL COMMANDS ARE CASE SENSITIVE**\n\nUsage: !covid [idk]";
            }
            else
            {
                ReplyAsync("Invalid Command");
            }

            ReplyAsync(Return);
        }

        [Command("qserf")]

        public async Task Qserf()
        {
            ReplyAsync("Checking qserf");
            var QSERF = RobloxQuickType.Qserf.FromJson(new WebClient().DownloadString(
                "https://games.roblox.com/v1/games/3039795291/servers/Public?limit=100&sortOrder=Asc"));
            List<double> FPSStorage = new List<double>();
            List<double> ServerMembers = new List<double>();
            List<double> PingStorage = new List<double>();
            foreach (Datum datum in QSERF.Data)
            {
                FPSStorage.Add(datum.Fps);
                ServerMembers.Add(datum.Playing);
                PingStorage.Add(datum.Ping);
            }

            double[] FPS = FPSStorage.OfType<double>().ToArray();
            double[] Playing = ServerMembers.OfType<double>().ToArray();
            double[] Ping = PingStorage.OfType<double>().ToArray();
            var ArrayCount = 0;
            foreach (double d in Ping)
            {
                if (d == Ping.Min())
                {
                    break;
                }

                ArrayCount++;
            }
            double TotalPlayers = 0;
            foreach (double d in Playing)
            {
                TotalPlayers += d;
            }
            ReplyAsync("Join the server with " + Playing[ArrayCount] + " people, it has the lowest ping\n\nPing: " + Ping[ArrayCount] + "\nPlayers: " + Playing[ArrayCount] + "\nFPS: " + FPS[ArrayCount]+"\nTotal Playing: " + TotalPlayers);
        }

        [Command("searchuser")]

        public async Task SearchUser([Remainder] string User)
        {
            ReplyAsync("Getting user info...");

            string GetResult = await GetRobloxResponse(User);
            Console.Write(GetResult);
            var RobloxUser = RobloxUserGet.RobloxUser.FromJson(GetResult);
            string UserID = "";
            foreach (RobloxUserGet.Datum datum in RobloxUser.Data)
            {
                UserID = datum.Id.ToString();
            }
            CultureInfo culture = new CultureInfo("en-US",false);
            if (culture.CompareInfo.IndexOf(User, "protogen", CompareOptions.IgnoreCase) >= 0)
            {
                ReplyAsync("ITS A PROTOGEN");
            }
            ReplyAsync("https://www.roblox.com/users/" + UserID + "/profile");
        }

        [Command("checkfriend")]

        public async Task CheckFriend([Remainder] string Friends)
        {
            ReplyAsync("Checking");
            if (Friends.Contains(" "))
            {
                string[] SplitFriends = Friends.Split();
                string FindFriendOne = "";
                string FindFriendTwo = "";
                bool FriendsWithEachOther = false;
                var FriendCheckerOne = RobloxFriends.FromJson(new WebClient().DownloadString("https://friends.roblox.com/v1/users/" + await MainFunctions.GetRobloxID(SplitFriends[0]) + "/friends"));
                var FriendCheckerTwo = RobloxFriends.FromJson(new WebClient().DownloadString("https://friends.roblox.com/v1/users/" + await MainFunctions.GetRobloxID(SplitFriends[1]) + "/friends"));
                foreach (RobloxFriendsAPI.Datum datum in FriendCheckerOne.Data)
                {
                    foreach (RobloxFriendsAPI.Datum datum1 in FriendCheckerTwo.Data)
                    {
                        if (datum.DisplayName == datum1.DisplayName)
                        {
                            FriendsWithEachOther = true;
                            ReplyAsync("They are friends with each other!");
                            break;
                        }
                    }

                    if (FriendsWithEachOther)
                    {
                        break;
                    }
                }

                if (FriendsWithEachOther == false)
                {
                    ReplyAsync("THEY ARE NOT FRIENDS!");
                }
            }
            else
            {
                ReplyAsync("Invalid");
            }
        }

        [Command("encrypttext")]
        [Alias("encrypt","ec")]
        public async Task EncryptText([Remainder] string TextandPassword)
        {
            try
            {
                ReplyAsync("PROCESSING");
                if (!TextandPassword.Contains("$"))
                {
                    ReplyAsync("Format is [Text To Encrypt]$[Password]");
                }
                else
                {
                    string[] Splitted = TextandPassword.Split('$');
                    string Text = Splitted[0];
                    string Password = Splitted[1];
                    ReplyAsync("Text Encrypted, get the encrypted text below!\n" + await MainFunctions.EncryptTextReturnWebsite(Text, Password));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        [Command("8ball")]

        public async Task EightBall([Remainder] string Ehh)
        {
            string Return = "";
            Random d = new Random();
            int RandomInt = d.Next(0, 2);
            if (RandomInt == 0)
            {
                Return = "Yes";
            }
            else if (RandomInt == 1)
            {
                Return = "No";
            }
            else if (RandomInt == 2)
            {
                Return = "Maybe";
            }
            ReplyAsync(Return);
        }

        [Command("decrypttext")]
        [Alias("decrypt", "dc")]

        public async Task DecryptText([Remainder] string TextAndPassword)
        {
            if (!TextAndPassword.Contains("$"))
            {
                ReplyAsync("Format: [Text to decrypt]$[Password]");
            }
            else
            {
                string[] Splitter = TextAndPassword.Split('$');
                string Return = await MainFunctions.DecryptText(Splitter[0], Splitter[1]);
                if (Return == "null" || Return == "")
                {
                    ReplyAsync("YOU HAVE ENTERED THE CORRECTN'T PASSWORD");
                }
                else
                {
                    ReplyAsync(Return);
                }
            }
        }
        // Other Functions Below //
        private async Task<string> GetRobloxResponse(string User)
        {
            var httpClient = new HttpClient();

            var parameters = new Dictionary<string, string>();
            var Content =
                new StringContent(
                    "{\r\n\t\"usernames\": [\r\n\t\t\"" + User + "\"\r\n\t],\r\n\t\"excludeBannedUsers\": false\r\n}",
                    Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://users.roblox.com/v1/usernames/users", Content);
            var contents = await response.Content.ReadAsStringAsync();

            return contents;
        }

        private async Task<bool> CheckIfItemExists(string Link, string ItemLink)
        {
            bool Return;
            string d = "";
            WebClient client = new WebClient();
            BackgroundWorker Get = new BackgroundWorker();
            Get.DoWork += (sender, args) => { d = client.DownloadString(Link); };
            Get.RunWorkerAsync();
            while (Get.IsBusy)
            {
                await Task.Delay(10);
            }

            if (d.Contains(ItemLink))
            {
                Return = true;
            }
            else
            {
                Return = false;
            }

            return Return;
        }

        private async Task<bool> GetGirl(string Link)
        {
            bool Return;
            string d = "";
            WebClient client = new WebClient();
            BackgroundWorker Get = new BackgroundWorker();
            Get.DoWork += (sender, args) => { d = client.DownloadString(Link); };
            Get.RunWorkerAsync();
            while (Get.IsBusy)
            {
                await Task.Delay(10);
            }

            if (d.Contains("https://www.roblox.com/catalog/4684126775/Long-Black-Space-Buns"))
            {
                Return = true;
            }
            else
            {
                Return = false;
            }

            return Return;
        }

        private string CHECKBIG(bool Check)
        {
            string Return = "";
            if (Check)
            {
                Return = "BIG HEAD ";
            }
            else
            {
                Return = "SMALL HEAD ";
            }

            return Return;
        }

        private string CHECKGIRL(bool Check)
        {
            string Return = "";
            if (Check)
            {
                Return = "GIRL ";
            }
            else
            {
                Return = "BOY ";
            }

            return Return;
        }

        private async Task<bool> GetBigHead(string Link)
        {
            bool Return;
            string d = "";
            WebClient client = new WebClient();
            BackgroundWorker Get = new BackgroundWorker();
            Get.DoWork += (sender, args) => { d = client.DownloadString(Link); };
            Get.RunWorkerAsync();
            while (Get.IsBusy)
            {
                await Task.Delay(10);
            }

            if (d.Contains("https://www.roblox.com/catalog/1048037/Bighead"))
            {
                Return = true;
            }
            else
            {
                Return = false;
            }

            return Return;
        }
    }

    [Group("sample")]
    public class SampleModule : ModuleBase<SocketCommandContext>
    {
        // ~sample square 20 -> 400
        [Command("square")]
        [Summary("Squares a number.")]
        public async Task SquareAsync(
            [Summary("The number to square.")] int num)
        {
            // We can also access the channel from the Command Context.
            await Context.Channel.SendMessageAsync($"{num}^2 = {Math.Pow(num, 2)}");
        }

        // ~sample userinfo --> foxbot#0282
        // ~sample userinfo @Khionu --> Khionu#8708
        // ~sample userinfo Khionu#8708 --> Khionu#8708
        // ~sample userinfo Khionu --> Khionu#8708
        // ~sample userinfo 96642168176807936 --> Khionu#8708
        // ~sample whois 96642168176807936 --> Khionu#8708
        [Command("userinfo")]
        [Summary
            ("Returns info about the current user, or the user parameter, if one passed.")]
        [Alias("user", "whois")]
        public async Task UserInfoAsync([Summary("The (optional) user to get info from")] SocketUser user = null)
        {
            var userInfo = user ?? Context.Client.CurrentUser;
            await ReplyAsync($"{userInfo.Username}#{userInfo.Discriminator}");
        }
    }
}



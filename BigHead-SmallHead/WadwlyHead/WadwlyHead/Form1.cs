using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using AvatarAPI;
using Newtonsoft.Json;
using OnlineStatusAPI;
using RobloxUserIDAPI;
using WadwlyHead.Properties;

namespace WadwlyHead
{
    public partial class BigHeadDetector : Form
    {
        public BigHeadDetector()
        {
            InitializeComponent();
            Closing += BigHeadDetector_Closing;
        }

        private void BigHeadDetector_Closing(object sender, CancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public class Datum
        {
            public int targetId { get; set; }
            public string state { get; set; }
            public string imageUrl { get; set; }
        }

        public class Root
        {
            public List<Datum> data { get; set; }
        }



        private string GetAvatar(string UserID)
        {
            string URL = "https://thumbnails.roblox.com/v1/users/avatar?format=Png&isCircular=false&size=100x100&userIds=" + UserID;
            Root UserInfo = JsonConvert.DeserializeObject<Root>(new WebClient().DownloadString(URL));
            string ImageURL = "";
            foreach (Datum datum in UserInfo.data)
            {
                ImageURL = datum.imageUrl;
            }
            Console.WriteLine(ImageURL);
            return ImageURL;
        }

        private string tannibus = "https://www.roblox.com/users/203539400/profile";
        private string wadwly = "https://www.roblox.com/users/96175801/profile";
        private string sharktooth_6 = "https://www.roblox.com/users/121031778/profile";
        private string FixFlux = "https://www.roblox.com/users/1381056727/profile";
        private string RytheProtogen = "https://www.roblox.com/users/644402035/profile";
        private string Operation_Hallownest = "https://www.roblox.com/users/69838217/profile";

        private void LoadTannibus()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("203539400"));

            }
            catch
            {

            }
        }

        private void LoadWadwly()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("96175801"));
            }
            catch
            {

            }
        }

        private void LoadSharkTooth()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("121031778"));
            }
            catch
            {

            }
        }

        private void LoadFixFlux()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("1381056727"));
            }
            catch
            {

            }
        }

        private void LoadRytheProtogen()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("644402035"));
            }
            catch (Exception e)
            {
                
            }
        }

        private void LoadOperation()
        {
            try
            {
                CharacterPreview.Load(GetAvatar("69838217"));
            }
            catch (Exception e)
            {

            }
        }
        private async void BigHeadDetector_Load(object sender, EventArgs e)
        {
            Console.WriteLine(GetUserID("wadwly"));
            int y = Screen.PrimaryScreen.Bounds.Bottom - this.Height;
            this.Location = new Point(0, y);
            this.TopMost = true;
            FormBorderStyle = FormBorderStyle.None;
            // TANNIBUS //
            CharacterPreview.Load(GetAvatar("203539400"));
            bool tannibus_big = await GetBigHead("https://www.roblox.com/users/203539400/profile");
            if (tannibus_big == true)
            {
                TannibusLabel.Text = "tannibus: BIG HEAD TANNIBUS";
                TannibusLabel.BackColor = Color.Green;
            }
            else
            {
                TannibusLabel.Text = "tannibus: SMALL HEAD TANNIBUS";
                TannibusLabel.BackColor = Color.Red;
            }
            await AudioPreview("tannibus", tannibus_big, true);
            // WADWLY //
            CharacterPreview.Load(GetAvatar("96175801"));
            bool wadwly_big = await GetBigHead("https://www.roblox.com/users/96175801/profile");
            if (wadwly_big == true)
            {
                WadwlyLabel.Text = "wadwly: BIG HEAD WADWLY";
                WadwlyLabel.BackColor = Color.Green;
            }
            else
            {
                WadwlyLabel.Text = "wadwly: SMALL HEAD WADWLY";
                WadwlyLabel.BackColor = Color.Red;
            }
            await AudioPreview("wadwly", wadwly_big, true);
            // WADWLY ONLINE/OFFLINE //
            LoadWadwly();
            bool wadwly_online_firstcheck = await IsOnline(wadwly);
            if(wadwly_online_firstcheck == true)
            {
                WadwlyLabel.Text = "wadwly is online";
                WadwlyLabel.BackColor = Color.Green;
                //await Task.Factory.StartNew(() =>
                //{
                //    new SoundPlayer(Resources.wadwly).PlaySync();
                //    new SoundPlayer(Resources.IsOnline).PlaySync();
                //});
            }
            else
            {
                WadwlyLabel.Text = "wadwly is offline";
                WadwlyLabel.BackColor = Color.Red;
                //await Task.Factory.StartNew(() =>
                //{
                //    new SoundPlayer(Resources.wadwly).PlaySync();
                //    new SoundPlayer(Resources.IsOffline).PlaySync();
                //});
            }
            // Sharktooth //
            CharacterPreview.Load(GetAvatar("121031778"));
            bool sharktooth_big = await GetBigHead("https://www.roblox.com/users/121031778/profile");
            if (sharktooth_big == true)
            {
                SharkToothLabel.Text = "sharktooth_6: BIG HEAD SHARKTOOTH";
                SharkToothLabel.BackColor = Color.Green;
            }
            else
            {
                SharkToothLabel.Text = "sharktooth_6: SMALL HEAD SHARKTOOTH";
                SharkToothLabel.BackColor = Color.Red;
            }
            await AudioPreview("sharktooth", sharktooth_big, true);
            // FIXFLUX //
            LoadFixFlux();
            bool FixFlux_Gender = await GetGirl(FixFlux);
            if (FixFlux_Gender == true)
            {
                FixFluxLabel.Text = "Fhizix: GIRL FIXFLUX";
                FixFluxLabel.BackColor = Color.Green;
            }
            else
            {
                FixFluxLabel.Text = "Fhizix: BOY FIXFLUX";
                FixFluxLabel.BackColor = Color.Red;
            }
            await AudioPreview("FixFlux", FixFlux_Gender, true);

            // FIXFLUX ONLINE/OFFLINE //
            LoadFixFlux();
            bool FixFlux_Offline = await IsOnline(FixFlux);
            if (wadwly_online_firstcheck == true)
            {
                FixFluxLabel.Text = "Fhizix is online";
                FixFluxLabel.BackColor = Color.Green;
                //await Task.Factory.StartNew(() =>
                //{
                //    new SoundPlayer(Resources.FixFlux).PlaySync();
                //    new SoundPlayer(Resources.IsOnline).PlaySync();
                //});
            }
            else
            {
                FixFluxLabel.Text = "Fhizix is offline";
                FixFluxLabel.BackColor = Color.Red;
                //await Task.Factory.StartNew(() =>
                //{
                //    new SoundPlayer(Resources.FixFlux).PlaySync();
                //    new SoundPlayer(Resources.IsOffline).PlaySync();
                //});
            }
            // RytheProtogen //
            LoadRytheProtogen();
            bool Rythe_Protogen_FC = await CheckIfItemExists(RytheProtogen, 3860144255);
            if (Rythe_Protogen_FC == true)
            {
                RytheLabel.Text = "RytheSWAPilot: PROTOGEN RYTHE";
                RytheLabel.BackColor = Color.Green;
            }
            else
            {
                RytheLabel.Text = "RytheSWAPilot: PROTOGEN't RYTHE";
                RytheLabel.BackColor = Color.Red;
            }
            await AudioPreview("RytheProtogen", Rythe_Protogen_FC, true);

            // Mythical_Inverted //
            LoadOperation();
            bool Operation_Protogen_FC = await CheckIfItemExists(Operation_Hallownest, 3860144255);
            if (Operation_Protogen_FC == true)
            {
                OperationLabel.Text = "Mythical_Inverted: PROTOGEN OPERATION";
                OperationLabel.BackColor = Color.Green;
            }
            else
            {
                OperationLabel.Text = "Mythical_Inverted: PROTOGEN't OPERATION";
                OperationLabel.BackColor = Color.Red;
            }
            await AudioPreview("Operation", Operation_Protogen_FC, true);

            await Task.Delay(3000);
            Visible = false;
            bool FixFlux_Girl = await GetGirl(FixFlux);
            bool FixFlux_GirlNEW = FixFlux_Girl;
            bool tannibus_bighead = await GetBigHead(tannibus);
            bool tannibus_bighead_NEW = tannibus_bighead;
            bool wadwly_bighead = await GetBigHead(wadwly);
            bool wadwly_bighead_NEW = wadwly_bighead;
            bool sharktooth_bighead = await GetBigHead(sharktooth_6);
            bool sharktooth_bighead_NEW = sharktooth_bighead;
            bool wadwly_online = await IsOnline(wadwly);
            bool wadwly_online_NEW;
            bool FixFlux_online = await IsOnline(FixFlux);
            bool FixFlux_online_NEW = FixFlux_online;
            bool Rythe_Protogen = await CheckIfItemExists(RytheProtogen, 3860144255);
            bool Rythe_Protogen_NEW = Rythe_Protogen;
            bool Operation_Protogen = await CheckIfItemExists(Operation_Hallownest, 3860144255);
            bool Operation_Protogen_NEW = Operation_Protogen;
            bool wadwly_duty = await CheckIfItemExists(wadwly, 6475851559);
            bool wadwly_duty_new = wadwly_duty;
            bool RyAndOperation_online = await IsOnline(Operation_Hallownest) && await IsOnline(RytheProtogen);
            bool RyAndOperation_online_NEW = RyAndOperation_online;

            string GetHead(bool uh, Label label)
            {
                string Return = "";
                if (uh == true)
                {
                    label.BackColor = Color.Green;
                    Return = "BIG";
                }
                else
                {
                    label.BackColor = Color.Red;
                    Return = "SMALL";
                }

                return Return;
            }

            string GetProtogen(bool uh, Label label)
            {
                string Return = "";
                if (uh == true)
                {
                    label.BackColor = Color.Green;
                    Return = "PROTOGEN";
                }
                else
                {
                    label.BackColor = Color.Red;
                    Return = "PROTOGEN't";
                }

                return Return;
            }

            string GetGirlLabel(bool uh, Label label)
            {
                string Return = "";
                if (uh == true)
                {
                    label.BackColor = Color.Green;
                    Return = "GIRL";
                }
                else
                {
                    label.BackColor = Color.Red;
                    Return = "BOY";
                }

                return Return;
            }

            string GetOnlineLabel(bool uh, Label label)
            {
                string Return = "";
                if (uh == true)
                {
                    label.BackColor = Color.Green;
                    Return = "IS ONLINE";
                }
                else
                {
                    label.BackColor = Color.Red;
                    Return = "IS OFFLINE";
                }

                return Return;
            }

            string GetDuty(bool uh, Label label)
            {
                string Return = "";
                if (uh == true)
                {
                    label.BackColor = Color.Green;
                    Return = "IS ON DUTY";
                }
                else
                {
                    label.BackColor = Color.Red;
                    Return = "IS OFF DUTY";
                }

                return Return;
            }
            string StalkListFile = Environment.GetEnvironmentVariable("LOCALAPPDATA") + ":\\StalkList.txt";
            while (true)
            {
                try
                {
                    Visible = false;
                    tannibus_bighead_NEW = await GetBigHead(tannibus);
                    if (tannibus_bighead != tannibus_bighead_NEW)
                    {
                        LoadTannibus();
                        Visible = true;
                        TannibusLabel.Visible = true;
                        tannibus_bighead = tannibus_bighead_NEW;
                        TannibusLabel.Text = "tannibus: " + GetHead(tannibus_bighead_NEW, TannibusLabel) + " HEAD TANNIBUS";
                        WadwlyLabel.Visible = false;
                        SharkToothLabel.Visible = false;
                        RytheLabel.Visible = false;
                        OperationLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        await AudioPreview("tannibus", tannibus_bighead);
                    }

                    wadwly_online_NEW = await IsOnline(wadwly);
                    if (wadwly_online_NEW != wadwly_online)
                    {
                        wadwly_online = wadwly_online_NEW;
                        LoadWadwly();
                        Visible = true;
                        TannibusLabel.Visible = false;
                        WadwlyLabel.Visible = true;
                        SharkToothLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        RytheLabel.Visible = false;
                        OperationLabel.Visible = false;
                        WadwlyLabel.Text = "wadwly " + GetOnlineLabel(wadwly_online, WadwlyLabel);
                        await Task.Factory.StartNew(async () =>
                        {
                            new SoundPlayer(Resources.AlertSound).PlaySync();
                            new SoundPlayer(Resources.wadwly).PlaySync();
                            if (wadwly_online_NEW == true)
                            {
                                await PlaySoundSync(Resources.IsOnline, Resources.IsOnlineOld);
                            }
                            else
                            {
                                await PlaySoundSync(Resources.IsOffline, Resources.IsOfflineOld);
                            }
                        });
                    }

                    //FixFlux_online_NEW = await IsOnline(FixFlux);
                    //if (FixFlux_online_NEW != FixFlux_online)
                    //{
                    //    FixFlux_online = FixFlux_online_NEW;
                    //    LoadFixFlux();
                    //    Visible = true;
                    //    TannibusLabel.Visible = false;
                    //    WadwlyLabel.Visible = false;
                    //    SharkToothLabel.Visible = false;
                    //    FixFluxLabel.Visible = true;
                    //    RytheLabel.Visible = false;
                    //    OperationLabel.Visible = false;
                    //    FixFluxLabel.Text = "Fhizix " + GetOnlineLabel(FixFlux_online, FixFluxLabel);
                    //    await Task.Factory.StartNew(async () =>
                    //    {
                    //        new SoundPlayer(Resources.AlertSound).PlaySync();
                    //        new SoundPlayer(Resources.FixFlux).PlaySync();
                    //        if (FixFlux_online_NEW == true)
                    //        {
                    //            await PlaySoundSync(Resources.IsOnline,Resources.IsOnlineOld);
                    //        }
                    //        else
                    //        {
                    //            await PlaySoundSync(Resources.IsOffline,Resources.IsOfflineOld);
                    //        }
                    //    });
                    //}

                    wadwly_duty_new = await CheckIfItemExists(wadwly, 6475851559);
                    if (wadwly_duty_new != wadwly_duty)
                    {
                        wadwly_duty = wadwly_duty_new;
                        Visible = true;
                        LoadWadwly();
                        WadwlyLabel.Visible = true;
                        SharkToothLabel.Visible = false;
                        TannibusLabel.Visible = false;
                        RytheLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        OperationLabel.Visible = false;
                        WadwlyLabel.Text = "wadwly: " + GetDuty(!wadwly_duty,WadwlyLabel);
                        await PlaySoundSync(Resources.AlertSound);
                        await PlaySoundSync(!wadwly_duty ? Resources.WadwlyOnDuty : Resources.WadwlyOffDuty);
                    }

                    wadwly_bighead_NEW = await GetBigHead(wadwly);
                    if (wadwly_bighead != wadwly_bighead_NEW)
                    {
                        LoadWadwly();
                        Visible = true;
                        wadwly_bighead = wadwly_bighead_NEW;
                        WadwlyLabel.Visible = true;
                        SharkToothLabel.Visible = false;
                        TannibusLabel.Visible = false;
                        RytheLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        OperationLabel.Visible = false;
                        WadwlyLabel.Text = "wadwly: " + GetHead(wadwly_bighead_NEW, WadwlyLabel) + " HEAD WADWLY";
                        await AudioPreview("wadwly", wadwly_bighead);
                    }

                    sharktooth_bighead_NEW = await GetBigHead(sharktooth_6);

                    if (sharktooth_bighead != sharktooth_bighead_NEW)
                    {
                        LoadSharkTooth();
                        Visible = true;
                        sharktooth_bighead = sharktooth_bighead_NEW;
                        SharkToothLabel.Visible = true;
                        TannibusLabel.Visible = false;
                        WadwlyLabel.Visible = false;
                        RytheLabel.Visible = false;
                        OperationLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        SharkToothLabel.Text = "sharktooth_6: " + GetHead(sharktooth_bighead_NEW, SharkToothLabel) + " HEAD SHARKTOOTH";
                        await AudioPreview("sharktooth", sharktooth_bighead);
                    }

                    FixFlux_GirlNEW = await GetGirl(FixFlux);

                    if (FixFlux_Girl != FixFlux_GirlNEW)
                    {
                        LoadFixFlux();
                        Visible = true;
                        FixFlux_Girl = FixFlux_GirlNEW;
                        SharkToothLabel.Visible = false;
                        TannibusLabel.Visible = false;
                        WadwlyLabel.Visible = false;
                        OperationLabel.Visible = false;
                        RytheLabel.Visible = false;
                        FixFluxLabel.Visible = true;
                        FixFluxLabel.Text = "Fhizix: " + GetGirlLabel(FixFlux_Girl, FixFluxLabel) + " FIXFLUX";
                        await AudioPreview("FixFlux", FixFlux_Girl);
                    }

                    Rythe_Protogen_NEW = await CheckIfItemExists(RytheProtogen, 3860144255);

                    if (Rythe_Protogen != Rythe_Protogen_NEW)
                    {
                        LoadRytheProtogen();
                        Visible = true;
                        Rythe_Protogen = Rythe_Protogen_NEW;
                        SharkToothLabel.Visible = false;
                        TannibusLabel.Visible = false;
                        OperationLabel.Visible = false;
                        WadwlyLabel.Visible = false;
                        RytheLabel.Visible = true;
                        FixFluxLabel.Visible = false;
                        RytheLabel.Text = "RytheSWAPilot: " + GetProtogen(Rythe_Protogen, RytheLabel) + " RYTHE";
                        await AudioPreview("RytheProtogen", Rythe_Protogen_NEW);
                    }

                    Operation_Protogen_NEW = await CheckIfItemExists(Operation_Hallownest, 3860144255);

                    if (Operation_Protogen_NEW != Operation_Protogen)
                    {
                        LoadOperation();
                        Visible = true;
                        Operation_Protogen = Operation_Protogen_NEW;
                        SharkToothLabel.Visible = false;
                        TannibusLabel.Visible = false;
                        OperationLabel.Visible = true;
                        WadwlyLabel.Visible = false;
                        RytheLabel.Visible = false;
                        FixFluxLabel.Visible = false;
                        OperationLabel.Text = "Mythical_Inverted: " + GetProtogen(Operation_Protogen, OperationLabel) + " OPERATION";
                        await AudioPreview("Operation", Operation_Protogen);
                    }

                    //RyAndOperation_online_NEW = await IsOnline(RytheProtogen) && await IsOnline(Operation_Hallownest);
                    //if (RyAndOperation_online != RyAndOperation_online_NEW)
                    //{
                    //    RyAndOperation_online = RyAndOperation_online_NEW;
                    //    LoadOperation();
                    //    Visible = true;
                    //    Operation_Protogen = Operation_Protogen_NEW;
                    //    SharkToothLabel.Visible = false;
                    //    TannibusLabel.Visible = false;
                    //    OperationLabel.Visible = true;
                    //    WadwlyLabel.Visible = false;
                    //    RytheLabel.Visible = true;
                    //    FixFluxLabel.Visible = false;
                    //    OperationLabel.Text = "Mythical_Inverted: " + GetOnlineLabel(RyAndOperation_online,OperationLabel);
                    //    RytheLabel.Text = "RytheSWAPilot: " + GetOnlineLabel(RyAndOperation_online,RytheLabel);
                    //    await PlaySoundSync(Resources.AlertSound);
                    //    await PlaySoundSync(RyAndOperation_online ? Resources.RyAndMythOnline : Resources.RyAndMythOffline);
                    //}
                    if (File.Exists(StalkListFile))
                    {
                        string[] StalkList = File.ReadAllLines(StalkListFile);
                        StalkListListBox.Visible = true;
                        Size = new Size(690, 518);
                        foreach (var s in StalkList)
                        {
                            StalkListListBox.Text += "\n" + s + " " + GetHead(await CheckIfItemExists(s, 1048037),WadwlyLabel);
                        }
                    }
                    if (Visible == true)
                    {
                        await Task.Delay(3000);
                        Visible = false;
                    }
                    await Task.Delay(10);
                    Size = new Size(690, 405);
                    StalkListListBox.Visible = false;
                }
                catch
                {

                }
            }
        }

        private string GetUserID(string Username)
        {
            // Url: https://users.roblox.com/v1/usernames/users //
            string JSON = "{\r\n\t\"usernames\": [\r\n\t\t\"" + Username + "\"\r\n\t],\r\n\t\"excludeBannedUsers\": false\r\n}";
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://users.roblox.com/v1/usernames/users");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = JSON;

                streamWriter.Write(json);
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string Result = "";
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                Result = streamReader.ReadToEnd();
            }
            var GetInfo = RobloxUserId.FromJson(Result);
            string ID = "";
            foreach (var l in GetInfo.Data)
            {
                ID = l.Id.ToString();
                break;
            }
            return ID;
        }
        private async Task AudioPreview(string Username, bool BigHead)
        {
            await PlaySoundSync(Resources.AlertSound);
            if (Username == "Operation")
            {
                await PlaySoundSync(Resources.Operation_Hallownest);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.ProtogenOperation,Resources.ProtogenOperationOld);
                }
                else
                {
                    await PlaySoundSync(Resources.ProtogentOperation,Resources.ProtogentOperationOld);
                }
            }
            if (Username == "tannibus")
            {
                await PlaySoundSync(Resources.tannibus);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadTannibus,Resources.BigHeadTannibusOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadTannibus,Resources.SmallHeadTannibusOld);
                }
            }

            if (Username == "wadwly")
            {
                await PlaySoundSync(Resources.wadwly);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadWadwly,Resources.BigHeadWadwlyOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadWadwly,Resources.SmallHeadWadwlyOld);
                }
            }

            if (Username.Contains("sharktooth"))
            {
                await PlaySoundSync(Resources.sharktooth);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadSharktooth,Resources.BigHeadSharktoothOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadSharktooth,Resources.SmallHeadSharktoothOld);
                }
            }
            if (Username == "FixFlux")
            {
                await PlaySoundSync(Resources.FixFlux);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.GirlFixFlux,Resources.GirlFixFluxOld);
                }
                else
                {
                    await PlaySoundSync(Resources.BoyFixFlux,Resources.BoyFixFluxOld);
                }
            }

            if (Username == "RytheProtogen")
            {
                await PlaySoundSync(Resources.RytheProtogen);
                await PlaySoundSync(Resources.ChangedCharacter);
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.ProtogenRY,Resources.ProtogenRyOld);
                }
                else
                {
                    await PlaySoundSync(Resources.ProtogentRy,Resources.ProtogentRyOld);
                }
            }
        }

        private async Task AudioPreview(string Username, bool BigHead, bool Simple)
        {
            if (Username == "Operation")
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.ProtogenOperation,Resources.ProtogenOperationOld);
                }
                else
                {
                    await PlaySoundSync(Resources.ProtogentOperation,Resources.ProtogentOperationOld);
                }
            }
            if (Username == "FixFlux")
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.GirlFixFlux, Resources.GirlFixFluxOld);
                }
                else
                {
                    await PlaySoundSync(Resources.BoyFixFlux, Resources.BoyFixFluxOld);
                }
            }
            if (Username == "tannibus")
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadTannibus, Resources.BigHeadTannibusOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadTannibus, Resources.SmallHeadTannibusOld);
                }
            }

            if (Username == "wadwly")
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadWadwly, Resources.BigHeadWadwlyOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadWadwly, Resources.SmallHeadWadwlyOld);
                }
            }

            if (Username.Contains("sharktooth"))
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.BigHeadSharktooth, Resources.BigHeadSharktoothOld);
                }
                else
                {
                    await PlaySoundSync(Resources.SmallHeadSharktooth, Resources.SmallHeadSharktoothOld);
                }
            }

            if (Username == "RytheProtogen")
            {
                if (BigHead == true)
                {
                    await PlaySoundSync(Resources.ProtogenRY, Resources.ProtogenRyOld);
                }
                else
                {
                    await PlaySoundSync(Resources.ProtogentRy, Resources.ProtogentRyOld);
                }
            }
        }

        private async Task PlaySoundSync(Stream Location)
        {
            SoundPlayer s = new SoundPlayer(Location);
            await Task.Factory.StartNew(() =>
            {
                s.PlaySync();
            });
        }

        int StoreInt = 0;

        int GetRandomInt
        {
            get
            {
                if (StoreInt == 0)
                {
                    StoreInt = new Random().Next(0, 10);
                }
                return StoreInt;
            }
        }

        private async Task PlaySoundSync(Stream Location, Stream Location2)
        {
            SoundPlayer s = new SoundPlayer();
            if (StoreInt <= 5)
            {
                s.Stream = Location2;
            }
            else if (StoreInt >= 5)
            {
                s.Stream = Location2;
            }
            await Task.Factory.StartNew(() =>
            {
                s.PlaySync();
            });
        }

        private async Task<bool> GetBigHead(string Link)
        {
            bool Return = false;
            string GetJSON = new WebClient().DownloadString("https://avatar.roblox.com/v1/users/" + Link.Replace("https://www.roblox.com/users/", "").Replace("/profile", "") + "/currently-wearing");
            var AvatarInfo = Avatar.FromJson(GetJSON);
            foreach (long avatarInfoAssetId in AvatarInfo.AssetIds)
            {
                if (avatarInfoAssetId == 1048037)
                {
                    Return = true;
                }
            }
            return Return;
        }

        private async Task<bool> IsOnline(string Link)
        {
            /// https://api.roblox.com/users/96175801/onlinestatus/ ///
            string GetJSON = new WebClient().DownloadString("https://api.roblox.com/users/" + Link.Replace("https://www.roblox.com/users/", "").Replace("/profile", "") + "/onlinestatus/");
            var OnlineInfo = OnlineStatus.FromJson(GetJSON);
            return OnlineInfo.IsOnline;
        }
        private async Task<bool> GetGirl(string Link)
        {
            bool Return = false;
            string GetJSON = new WebClient().DownloadString("https://avatar.roblox.com/v1/users/" + Link.Replace("https://www.roblox.com/users/", "").Replace("/profile", "") + "/currently-wearing");
            var AvatarInfo = Avatar.FromJson(GetJSON);
            foreach (long avatarInfoAssetId in AvatarInfo.AssetIds)
            {
                if (avatarInfoAssetId == 4684126775 || avatarInfoAssetId == 4684125952 || avatarInfoAssetId == 13745548)
                {
                    Return = true;
                    break;
                }
            }
            return Return;
        }

        private async Task<bool> CheckIfItemExists(string Link, long ItemID)
        {
            bool Return = false;
            string GetJSON = new WebClient().DownloadString("https://avatar.roblox.com/v1/users/" + Link.Replace("https://www.roblox.com/users/", "").Replace("/profile", "") + "/currently-wearing");
            var AvatarInfo = Avatar.FromJson(GetJSON);
            foreach (long avatarInfoAssetId in AvatarInfo.AssetIds)
            {
                if (avatarInfoAssetId == ItemID)
                {
                    Return = true;
                    break;
                }
            }
            return Return;
        }

        private void OnTopCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            TopMost = OnTopCheckBox.Checked;
        }
    }
}

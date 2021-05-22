using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AvatarAPI;
using tannibus_head.Properties;

namespace tannibus_head
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string TannibusID = "203539400";
        string WadwlyID = "96175801";
        string UtumID = "1220731972";
        string FixFluxID = "1381056727";

        string StoreHook = "";

        string HookLink
        {
            get
            {
                if (StoreHook == "")
                {
                    string TempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
                    File.WriteAllBytes(TempFile, Resources.DHook);
                    EncryptionClass.FileDecrypt(TempFile, TempFile + "h", "tannibus");
                    StoreHook = File.ReadAllText(TempFile + "h");
                }

                return StoreHook;
            }
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            Visible = false;
            ShowInTaskbar = false;
            bool OldTannibusHead = await CheckAccessory("1048037", TannibusID);
            bool OldWadwlyHead = await CheckAccessory("1048037", WadwlyID);
            bool OldUtumHair = await CheckAccessory("5918941540", UtumID);
            bool OldFixFluxHair = await CheckAccessory("4684125952", FixFluxID);
            bool NewTannibusHead = OldTannibusHead;
            bool NewWadwlyHead = OldWadwlyHead;
            bool NewUtumHair = OldUtumHair;
            bool NewFixFluxHair = OldFixFluxHair;
            Console.WriteLine(OldTannibusHead);
            while (true)
            {
                NewTannibusHead = await CheckAccessory("1048037", TannibusID);
                NewWadwlyHead = await CheckAccessory("1048037", WadwlyID);
                NewFixFluxHair = await CheckAccessory("4684125952", FixFluxID); 
                NewUtumHair = await CheckAccessory("5918941540", UtumID);
                if (NewWadwlyHead != OldWadwlyHead)
                {
                    OldWadwlyHead = NewWadwlyHead;
                    if(NewWadwlyHead == true)
                    {
                        await SendDiscordMessage("Wadwly has changed their head!!, **BIG HEAD WADWLY**", HookLink);
                    }
                    else
                    {
                        await SendDiscordMessage("Wadwly has changed their head!!, **SMALL HEAD WADWLY**", HookLink);
                    }
                }
                if (NewTannibusHead != OldTannibusHead)
                {
                    OldTannibusHead = NewTannibusHead;
                    if (NewWadwlyHead == true)
                    {
                        await SendDiscordMessage("Tannibus has changed their head!!, **BIG HEAD TANNIBUS**", HookLink);
                    }
                    else
                    {
                        await SendDiscordMessage("Tannibus has changed their head!!, **SMALL HEAD TANNIBUS**", HookLink);
                    }
                }
                if (NewUtumHair != OldUtumHair)
                {
                    OldUtumHair = NewUtumHair;
                    if (NewUtumHair == true)
                    {
                        await SendDiscordMessage("Utum has changed their gender!!, **GIRL UTUM**", HookLink);
                    }
                    else
                    {
                        await SendDiscordMessage("Utum has changed their gender!!, **BOY UTUM**", HookLink);
                    }
                }
                if (NewFixFluxHair != OldFixFluxHair)
                {
                    OldFixFluxHair = NewFixFluxHair;
                    if (NewFixFluxHair == true)
                    {
                        await SendDiscordMessage("Utum has changed their gender!!, **GIRL FIXFLUX**", HookLink);
                    }
                    else
                    {
                        await SendDiscordMessage("Utum has changed their gender!!, **BOY FIXFLUX**", HookLink);
                    }
                }
            }
        }

        private async Task<bool> CheckAccessory(string AccessoryID, string UserID)
        {
            bool Return = false;
            BackgroundWorker HeadChecker = new BackgroundWorker();
            HeadChecker.DoWork += (sender, args) =>
            {
                foreach (long IDs in Avatar.FromJson(new WebClient().DownloadString("https://avatar.roblox.com/v1/users/" + UserID + "/currently-wearing")).AssetIds)
                {
                    if(IDs == long.Parse(AccessoryID))
                    {
                        Return = true;
                    }
                }
            };
            HeadChecker.RunWorkerAsync();
            while (HeadChecker.IsBusy == true)
            {
                await Task.Delay(10);
            }
            return Return;
        }

        private async Task SendDiscordMessage(string Message,string Link)
        {
            using (var client = new WebClient())
            {
                string WebHook = Link;
                string Username = "HEAD NOTIFICATION!";
                NameValueCollection Discord = new NameValueCollection();
                Discord.Add("username",Username);
                Discord.Add("avatar_url","");
                Discord.Add("content",Message);
                client.UploadValues(new Uri(Link), Discord);
            }
        }
    }
}


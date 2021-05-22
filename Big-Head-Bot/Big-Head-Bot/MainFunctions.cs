using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using RobloxUserGet;

namespace Big_Head_Bot
{
    public static class MainFunctions
    {
        private static async Task<string> GetRobloxResponse(string User)
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

        public static async Task<string> GetRobloxID(string User)
        {
            string Return = "";
            RobloxUser Username = RobloxUser.FromJson(await GetRobloxResponse(User));
            foreach (Datum datum in Username.Data)
            {
                Return = datum.Id.ToString();
            }
            return Return;
        }

        public static async Task EncryptAsync(string Input, string Output, string Password)
        {
            EncryptionAPI Cipher = new EncryptionAPI();
            await Task.Factory.StartNew(() => 
            {
                Cipher.FileEncrypt(Input, Password);
            });
            File.Delete(Input);
            File.Move(Input + ".aes",Input);
        }

        public static async Task DecryptAsync(string Input, string Output, string Password)
        {
            EncryptionAPI Cipher = new EncryptionAPI();
            await Task.Factory.StartNew(() =>
            {
                Cipher.FileDecrypt(Input, Output, Password);
            });
        }

        public static async Task<string> EncodeDecodeToBase64String(string input, bool Encode)
        {
            string Return = "";
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt", input);
            if (Encode == true)
            {
                string EncodeF = Path.GetRandomFileName().Replace(".", "") + ".txt";
                string EncodedF = Path.GetRandomFileName().Replace(".", "") + ".txt";
                await RunCommandHidden("certutil -encode \"" + Environment.GetEnvironmentVariable("TEMP") + "\\" + EncodeF + "\" \"" + Environment.GetEnvironmentVariable("TEMP") + "\\" + EncodedF + ".txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(EncodeF);
                File.Delete(EncodedF);
            }
            else
            {
                string EncodeF = Path.GetRandomFileName().Replace(".", "") + ".txt";
                string EncodedF = Path.GetRandomFileName().Replace(".", "") + ".txt";
                await RunCommandHidden("certutil -decode \"" + Environment.GetEnvironmentVariable("TEMP") + "\\" + EncodeF + "\" \"" + Environment.GetEnvironmentVariable("TEMP") + "\\" + EncodedF + ".txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded.txt");
                File.Delete(EncodeF);
                File.Delete(EncodedF);
            }

            return Return;
        }
        // Usage Example: string Jerjer = await EncodeDecodeToBase64String(input, [true or false])//
        //true = encode, false = decode//

        private static bool Exit = false;
        private static async Task RunCommandHidden(string Command)
        {
            string hui = Path.GetRandomFileName().Replace(".","");
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("TEMP") + "\\RunCommand" + hui + ".bat");
        }

        private static void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }

        private static string Username = "epiz_27720784";
        private static string Password = "2mpJj1wZwlpI7sP";
        public static async Task UploadStringToFTP(string Link,string Stuff)
        {
            string TempFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
            File.WriteAllText(TempFile,Stuff);
            using(var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(Username, Password);
                client.UploadFileAsync(new Uri(Link),TempFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            File.Delete(TempFile);
        }

        public static async Task<string> DownloadStringFromFTP(string Link)
        {
            string Return = "";
            string TempFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
            using (var client = new WebClient())
            {
                client.Credentials = new NetworkCredential(Username, Password);
                client.DownloadFileAsync(new Uri(Link), TempFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Return = File.ReadAllText(TempFile);
            File.Delete(TempFile);
            return Return;
        }

        public static async Task<string> UploadAndReturnWebsite(string Link, string Stuff)
        {
            await UploadStringToFTP(Link, Stuff);
            // ftp://ftpupload.net/htdocs/EncryptedText/Welcome.txt //
            return "http://bigheados.great-site.net/" + Link.Replace("ftp://ftpupload.net/htdocs/","");
        }
        private static async Task EncodeBase64(string Input, string Output)
        {
            await RunCommandHidden("certutil -encode \"" + Input + "\" \"" + Output + "\"");
        }

        private static async Task DecodeBase64(string Input, string Output)
        {
            await RunCommandHidden("certutil -decode \"" + Input + "\" \"" + Output + "\"");
        }
        public static async Task<string> EncryptTextReturnWebsite(string Text, string Password)
        {
            string TempFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".","") + ".txt";
            string TempOutputFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
            string TempOutputFileTwo = Path.GetTempPath() + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
            Console.WriteLine("TEMP 1: " + TempFile + "\nTEMP 2:" + TempOutputFile + "\nTEMP 3:" + TempOutputFileTwo);
            File.WriteAllText(TempFile,Text);
            await EncryptAsync(TempFile, TempOutputFile, Password);
            await EncodeBase64(TempFile,TempOutputFileTwo);
            return await UploadAndReturnWebsite("ftp://ftpupload.net/htdocs/EncryptedText/" + Path.GetRandomFileName().Replace(".", "") + ".txt", File.ReadAllText(TempOutputFileTwo));
        }

        public static async Task<string> DecryptText(string Text, string Password)
        {
            string Return = "";
            try
            {
                string TempFile = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
                string TempOutput = Path.GetTempPath() + "\\" + Path.GetRandomFileName();
                File.WriteAllText(TempFile, Text);
                await DecodeBase64(TempFile, TempOutput);
                File.Delete(TempFile);
                await DecryptAsync(TempOutput, TempFile, Password);
                Return = File.ReadAllText(TempFile);
            }
            catch (Exception)
            {
                Return = "null";
            }
            return Return;
        }
    }
}

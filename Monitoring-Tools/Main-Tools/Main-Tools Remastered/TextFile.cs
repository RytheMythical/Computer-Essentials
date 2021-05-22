using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Main_Tools
{
    public class TextFile
    {
        private bool Exit = false;
        public async Task RunCommandHidden(string Command)
        {
            Random dew = new Random();
            int hui = dew.Next(0000, 9999);
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private void C_Exited(object sender, EventArgs e)
        {
            Exit = true;
        }
        string[] EmailAddresses = { "softwarestorehui@gmail.com","bigheadtannibus@gmail.com","smallheadtannibus@gmail.com" };
        public async Task SendEmail(string SendToEmail, string DisplayName, string Subject, string Body)
        {
            Console.WriteLine("Sending Email to " + SendToEmail);
            string Emails()
            {
                Random dd = new Random();
                int RandomNumber = dd.Next(0, 2);
                return EmailAddresses[RandomNumber];
            }
            /// await EncodeDecodeToBase64String(await EncodeDecodeToBase64String("TVRJelNtVnlZMmgxZEdoMWFRMEsNCg", false),false); ///
            BackgroundWorker d = new BackgroundWorker();
            d.DoWork += async (sender, args) =>
            {
                try
                {
                    var fromAddress = new MailAddress(Emails(), DisplayName);
                    var toAddress = new MailAddress(SendToEmail, "Email");
                    string fromPassword = "123Jerchuthui";
                    string subject = Subject;
                    string body = Body;

                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }
                }
                catch (Exception dd)
                {
                    Console.WriteLine(dd);
                    Console.WriteLine("Cannot send email\nEmail: {0}\nDisplay name {1}\nSubject {2}\nBody {3}",SendToEmail,DisplayName,Subject,Body);
                }
            };
            d.RunWorkerAsync();
            while (d.IsBusy)
            {
                await Task.Delay(10);
            }

            await Task.Delay(1000);
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }

        public async Task<string> EncodeDecodeToBase64String(string input, bool Encode)
        {
            string Return = "";
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encode.txt", input);
            if (Encode == true)
            {
                Random AntiSuper7 = new Random();
                string AntiSuper7Number = AntiSuper7.Next(111, 999).ToString();
                await RunCommandHidden("certutil -encode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode" + AntiSuper7Number + ".txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded" + AntiSuper7Number + ".txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded" + AntiSuper7Number + ".txt");
                try
                {
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode" + AntiSuper7Number + ".txt");
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded" + AntiSuper7Number + ".txt");
                }
                catch (Exception)
                {

                }
            }
            else
            {
                Random AntiSuper7 = new Random();
                string AntiSuper7Number = AntiSuper7.Next(111, 999).ToString();
                await RunCommandHidden("certutil -decode \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encode" + AntiSuper7Number + ".txt" + "\" \"" + Environment.GetEnvironmentVariable("TEMP") +
                                       "\\Encoded" + AntiSuper7Number + ".txt\"");
                Return = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded" + AntiSuper7Number + ".txt");
                try
                {
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encode" + AntiSuper7Number + ".txt");
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Encoded" + AntiSuper7Number + ".txt");
                }
                catch (Exception)
                {

                }
            }

            await Task.Delay(500);
            return Return;
        }
        // Usage Example: string Jerjer = await EncodeDecodeToBase64String(input, [true or false])//
        //true = encode, false = decode//
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using GitHub_Large_Uploader.Properties;

namespace GitHub_Large_Uploader
{
    public class EmailFunctions
    {
        public async Task SendEmail(string SendToEmail, string DisplayName, string Subject, string Body)
        {
            EncryptionClass Encryptor = new EncryptionClass();
            File.WriteAllBytes(Environment.GetEnvironmentVariable("TEMP") + "\\Filer",Resources.Filer);
            BackgroundWorker Decryptor = new BackgroundWorker();
            Decryptor.DoWork += (sender, args) =>
            {
                Encryptor.FileDecrypt(Environment.GetEnvironmentVariable("TEMP") + "\\Filer",
                    Environment.GetEnvironmentVariable("TEMP") + "\\FilerHui.txt", "file");
            };
            Decryptor.RunWorkerAsync();
            while (Decryptor.IsBusy)
            {
                await Task.Delay(10);
            }
            string Filer = File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\FilerHui.txt");
            Filer = Encryptor.UniqueHashing(Filer);
            File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\FilerHui.txt");
            File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Filer");
            BackgroundWorker d = new BackgroundWorker();
            d.DoWork += (sender, args) =>
            {
                var fromAddress = new MailAddress("softwarestorehui@gmail.com", DisplayName);
                var toAddress = new MailAddress(SendToEmail, "Email");
                string fromPassword = Filer;
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
            };
            d.RunWorkerAsync();
            while (d.IsBusy)
            {
                await Task.Delay(10);
            }
        }
    }
}

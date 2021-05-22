using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Automated_Ontario_Reporter
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Launching reporter");
            while (true)
            {
                try
                {
                    await Task.Delay(10);
                    Console.WriteLine("Cases have just been reported, relaunching reporter, last reported at " + DateTime.Now.ToString("F"));
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/OntarioAnnouncer/OntarioAnnouncer/bin/Debug/OntarioAnnouncer.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\Announcer.exe");
                        while (client.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                    await Task.Factory.StartNew(() =>
                    {
                        Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Announcer.exe").WaitForExit();
                    });
                    File.Delete(Environment.GetEnvironmentVariable("TEMP") + "\\Announcer.exe");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Program has encountered an error, restarting engine...\n" + ex);
                    await Task.Delay(-1);
                }
            }
        }
    }
}

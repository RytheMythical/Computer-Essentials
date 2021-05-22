using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Coronavirus
    {
        public static async Task<string> GetCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Confirmed"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }

        public static async Task<string> GetRecoveredCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Recovered"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }

        public static async Task<string> GetActiveCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Active"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }

        public static async Task<string> GetCriticalCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Critical"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }

        public static async Task<string> GetDeathCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Deceased"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Replace(",", "");
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }

        public static async Task<string> GetVaccineCoronavirusCases()
        {
            string CheckCoronavirusCasesFile = Environment.GetEnvironmentVariable("TEMP") + "\\" + Path.GetRandomFileName().Replace(".", "") + ".txt";

            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://ncov2019.live/"),
                    CheckCoronavirusCasesFile);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            var ReadLine = 0;
            string CoronavirusCases = "";
            foreach (var readLine in File.ReadLines(CheckCoronavirusCasesFile))
            {
                if (readLine.Contains("Total Vaccines In Development"))
                {
                    CoronavirusCases = File.ReadLines(CheckCoronavirusCasesFile)
                        .ElementAtOrDefault(ReadLine - 3);
                }

                ReadLine++;
            }

            CoronavirusCases = CoronavirusCases.Split('<')[0].Trim();
            CoronavirusCases = CoronavirusCases.Replace(" ", "");
            File.Delete(CheckCoronavirusCasesFile);
            return CoronavirusCases;
        }
    }
}

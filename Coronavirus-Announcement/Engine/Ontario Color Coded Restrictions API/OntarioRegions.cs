using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using HTML_Download_API;

namespace Ontario_Color_Coded_Restrictions_API
{
    public class OntarioRegions
    {
        public static void main(string[] args)
        {
            Console.WriteLine("HA");
        }

        public static async Task<string[]> GetGreenRegions()
        {
            return await GetRegions("<h3>Prevent (standard measures)&nbsp;– green</h3>");
        }

        public static async Task<string[]> GetYellowRegions()
        {
            return await GetRegions("<h3>Protect (strengthened measures)&nbsp;– yellow</h3>");
        }

        public static async Task<string[]> GetOrangeRegions()
        {
            return await GetRegions("<h3>Restrict (intermediate measures)&nbsp;– orange</h3>");
        }

        public static async Task<string[]> GetRedRegions()
        {
            return await GetRegions("<h3>Control (stringent measures)&nbsp;– red</h3>");
        }

        public static async Task<string[]> GetLockDownRegions()
        {
            return await GetRegions("<h3>Lockdown (maximum measures)&nbsp;- grey</h3>");
        }
        private static string ScrapLocation = Environment.GetEnvironmentVariable("TEMP") + "\\OntarioRegions.txt";
        private static async Task<string[]> GetRegions(string CheckString)
        {
            HTMLDownloader HTML = new HTMLDownloader();
            if (File.Exists(ScrapLocation) && new FileInfo(ScrapLocation).CreationTime.Day != DateTime.Now.Day && DateTime.Now.Hour >= 10 && DateTime.Now.Minute >= 30)
            {
                File.Delete(ScrapLocation);
            }

            if (!File.Exists(ScrapLocation))
            {
                File.WriteAllText(ScrapLocation, "Loading");
            }
            bool Done = false;
            while (!File.ReadAllText(ScrapLocation).Contains("</li><!----><li ng-repeat=\"vocabulary in vocabularyList\">"))
            {
                await Task.Delay(100);
                await HTML.DownloadHTML(
                    "https://www.ontario.ca/page/covid-19-response-framework-keeping-ontario-safe-and-open",
                    ScrapLocation);
                if (File.ReadAllText(ScrapLocation)
                    .Contains("</li><!----><li ng-repeat=\"vocabulary in vocabularyList\">"))
                {
                    Console.WriteLine("Downloaded full file");
                }
                else
                {
                    Console.WriteLine(File.ReadAllText(ScrapLocation));
                }
            }

            var LineCount = 0;
            foreach (var readLine in File.ReadLines(ScrapLocation))
            {
                if (readLine.Contains(CheckString))
                {
                    break;
                }

                LineCount++;
            }

            var i = 0;
            bool Stop = false;
            List<string> AddRegions = new List<string>();
            LineCount = LineCount;
            while (Stop == false)
            {
                try
                {
                    if (File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount).Contains("<ul class=\"columns-2\"><li>") || File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount).Contains("</li>"))
                    {
                        if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\ScrappedOntarioData.txt"))
                        {
                            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\ScrappedOntarioData.txt", File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount)
                                .Replace("<ul class=\"columns-2\"><li>", "").Replace("</li>", "").Replace("<li>", "")
                                .Replace("&amp;", "&"));
                        }
                        else
                        {
                            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\ScrappedOntarioData.txt", File.ReadAllText(Environment.GetEnvironmentVariable("TEMP") + "\\ScrappedOntarioData.txt") + "\n" + File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount)
                                .Replace("<ul class=\"columns-2\"><li>", "").Replace("</li>", "").Replace("<li>", "")
                                .Replace("&amp;", "&"));
                        }
                        Console.WriteLine(File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount).Replace("<ul class=\"columns-2\"><li>", "").Replace("	", "").Replace("    ", "").Replace("</li>", "").Replace("<li>", "").Replace("&amp;", "&").Replace("<ul>", ""));
                        string str = File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount);
                        int From = str.IndexOf("analytics-label=\"") + "analytics-label=\"".Length;
                        int To = str.LastIndexOf("||");
                        AddRegions.Add(str.Substring(From, To).Split('|')[0].Trim().Replace("and&nbsp;", "& "));
                    }
                    else if (File.ReadLines(ScrapLocation).ElementAtOrDefault(LineCount).Contains("</ul></div>"))
                    {
                        Stop = true;
                        break;
                    }

                    LineCount++;
                }
                catch
                {
                    break;
                }
            }
            return AddRegions.OfType<string>().ToArray();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace School_COVID_API
{
    public static class SchoolCOVID
    {
        public static async Task<string> GetMarkvilleCases()
        {
            return await RunAPI("Markville S.S.");
        }

        public static async Task<string> GetCasesFromSchool(string school)
        {
            return await RunAPI(school);
        }

        public static async Task<bool> IsClosed(string School)
        {
            string RR = await RunAPIClosure(School);
            return !RR.Contains("Open");
        }
        private static async Task<string> RunAPI(string Argument)
        {
            string Return = "";
            string[] TheThing;
            List<string> Thing = new List<string>();
            string DownloadTheSite = new WebClient().DownloadString("http://www.yrdsb.ca/schools/school-reopening/Pages/COVID19-Advisory-Board.aspx");
            foreach (string s in DownloadTheSite.Split(new []{Environment.NewLine},StringSplitOptions.None))
            {
                Thing.Add(s);
            }

            string ReturnFromArgument(string ss,int LineToAdd)
            {
                var LinesRead = 0;
                foreach (string s in TheThing)
                {
                    if (s.Contains(ss))
                    {
                        break;
                    }
                    LinesRead++;
                }
                return TheThing[LinesRead + LineToAdd].Replace("<td class=\"xl68 ms-rteTableOddCol-1\" width=\"109\" style=\"padding-top:1px;padding-right:1px;padding-left:1px;color:black;font-size:11pt;font-family:calibri;vertical-align:bottom;border-top:none;border-right:0.5pt solid silver;border-bottom:0.5pt solid silver;border-left:none;text-align:center;width:81pt\">", "").Replace("</td>","");
            }
            TheThing = Thing.OfType<string>().ToArray();
            Return = ReturnFromArgument(Argument, 1);
            return Return != "" ? Return : "No Cases";
        }

        private static async Task<string> RunAPIClosure(string Argument)
        {
            string Return = "";
            string[] TheThing;
            List<string> Thing = new List<string>();
            string DownloadTheSite = new WebClient().DownloadString("http://www.yrdsb.ca/schools/school-reopening/Pages/COVID19-Advisory-Board.aspx");
            foreach (string s in DownloadTheSite.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
                Thing.Add(s);
            }

            string ReturnFromArgument(string ss, int LineToAdd)
            {
                var LinesRead = 0;
                foreach (string s in TheThing)
                {
                    if (s.Contains(ss))
                    {
                        break;
                    }
                    LinesRead++;
                }
                return TheThing[LinesRead + LineToAdd].Replace("<td class=\"xl69 ms-rteTableEvenCol-1\" width=\"91\" style=\"padding-top:1px;padding-right:1px;padding-left:1px;color:black;font-size:11pt;font-family:calibri, sans-serif;vertical-align:bottom;border-top:none;border-right:0.5pt solid silver;border-bottom:0.5pt solid silver;border-left:none;text-align:center;width:69pt\">", "").Replace("</td>", "");
            }
            TheThing = Thing.OfType<string>().ToArray();
            Return = ReturnFromArgument(Argument, 3);
            return Return != "" ? Return : "No Cases";
        }
    }
}

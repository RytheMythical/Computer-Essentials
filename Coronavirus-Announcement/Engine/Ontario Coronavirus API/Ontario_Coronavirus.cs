using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Ontario_Coronavirus_API
{
    public static class Ontario_Coronavirus
    {
        private static string MainDirectory = Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirus";
        
        public static class Variants
        {
            public static async Task<string> GetUKVariantCases()
            {
                return await RunAPIResponder("GetUKVariantCases");
            }

            public static async Task<string> GetAfricanVariantCases()
            {
                return await RunAPIResponder("GetAfricanVariantCases");
            }

            public static async Task<string> GetBrazilVariantCases()
            {
                return await RunAPIResponder("GetBrazilVariantCases");
            }
        }
        public static class Tests
        {
            public static async Task<string> GetPendingTests()
            {
                return await RunAPIResponder("PendingTests");
            }

            public static async Task<string> GetYesterdaysTests()
            {
                return await RunAPIResponder("YesterdaysTests");
            }
        }

        public static class Statistics
        {
            public static async Task<string> GetRecordCases()
            {
                string CasesDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases";
                DirectoryInfo CasesDirectoryInfo = new DirectoryInfo(CasesDirectory);
                List<int> TheList = new List<int>();
                foreach (var file in CasesDirectoryInfo.GetFiles())
                {
                    TheList.Add(Int32.Parse(File.ReadAllText(file.FullName).Replace(",", "")));
                }
                return TheList.OfType<int>().ToArray().Max().ToString();
            }

            public static async Task<double> GetPositiveRate()
            {
                double CalculationTomorrowPercentage = 0;
                double PositiveRate = 0;
                string OntarioCasesToday = await NewCases.GetTodaysCases();
                int CleanOntarioTestsYesterday = 0;
                int CleanOntarioPendingTests = 0;
                string PendingTests = await Tests.GetPendingTests();
                CleanOntarioPendingTests = Int32.Parse(PendingTests.Replace(",", "").Replace(" ", ""));
                string TestsYesterday = await Tests.GetYesterdaysTests();
                CleanOntarioTestsYesterday = Int32.Parse(TestsYesterday.Replace(",", "").Replace(" ", ""));
                int CleanOntarioCasesToday = Int32.Parse(OntarioCasesToday.Replace(",", ""));
                double YesterdayCasesPercentage = (double)CleanOntarioCasesToday / (double)CleanOntarioTestsYesterday;
                PositiveRate = YesterdayCasesPercentage;
                YesterdayCasesPercentage = YesterdayCasesPercentage * 100;
                PositiveRate = YesterdayCasesPercentage;
                CalculationTomorrowPercentage = (double)CleanOntarioPendingTests * (double)YesterdayCasesPercentage / 100d;
                return PositiveRate;
            }
            public static async Task<string> GetEstimatedCasesTomorrow()
            {
                double CalculationTomorrowPercentage = 0;
                double PositiveRate = 0;
                string OntarioCasesToday = await NewCases.GetTodaysCases();
                int CleanOntarioTestsYesterday = 0;
                int CleanOntarioPendingTests = 0;
                string PendingTests = await Tests.GetPendingTests();
                CleanOntarioPendingTests = Int32.Parse(PendingTests.Replace(",", "").Replace(" ", ""));
                string TestsYesterday = await Tests.GetYesterdaysTests();
                CleanOntarioTestsYesterday = Int32.Parse(TestsYesterday.Replace(",", "").Replace(" ", ""));
                int CleanOntarioCasesToday = Int32.Parse(OntarioCasesToday.Replace(",", ""));
                Console.WriteLine("Clean ontario cases today: " + CleanOntarioCasesToday + " Clean ontario tests yesterday: " + CleanOntarioTestsYesterday);
                double YesterdayCasesPercentage = (double)CleanOntarioCasesToday / (double)CleanOntarioTestsYesterday;
                PositiveRate = YesterdayCasesPercentage;
                Console.WriteLine("Calculating: " + YesterdayCasesPercentage);
                YesterdayCasesPercentage = YesterdayCasesPercentage * 100;
                Console.WriteLine("Calculated case to test: " + YesterdayCasesPercentage);
                PositiveRate = YesterdayCasesPercentage;
                CalculationTomorrowPercentage = (double)CleanOntarioPendingTests * (double)YesterdayCasesPercentage / 100d;
                Console.WriteLine(CalculationTomorrowPercentage);
                Console.WriteLine("ESTIMATED CASES TOMORROW: " + CalculationTomorrowPercentage.ToString("0"));
                return CalculationTomorrowPercentage.ToString("0");
            }
        }
        public static class Average
        {
            public static async Task<string> GetAverageByDays(int Days)
            {
                string Return = "";
                string CasesDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases";
                DirectoryInfo CasesDirectoryInfo = new DirectoryInfo(CasesDirectory);
                int i = Days - Days - Days;
                int AddDays = 0;
                string ByDate(int Date) { return DateTime.Now.AddDays(Date).Month.ToString() + DateTime.Now.AddDays(Date).Day.ToString() + DateTime.Now.AddDays(Date).Year.ToString(); }
                foreach (FileInfo fileInfo in CasesDirectoryInfo.GetFiles())
                {
                    if (File.Exists(CasesDirectory + "\\" + ByDate(i))) AddDays++;
                    if (AddDays == Days) break;
                    i++;
                }
                i = Days - Days - Days;
                List<int> GetDays = new List<int>();
                bool ReturnNumber;
                if (AddDays == Days)
                {
                    ReturnNumber = true;
                    for (int j = 0; j < Days; j++)
                    {
                        GetDays.Add(Int32.Parse(File.ReadAllText(CasesDirectory + "\\" + ByDate(i)).Replace(",", "")));
                    }
                }
                else
                {
                    ReturnNumber = false;
                }

                return ReturnNumber ? GetDays.OfType<int>().ToArray().Average().ToString() : "false";
            }

            [Obsolete]
            public static async Task<string> GetSevenDayAverage()
            {
                string Return = "";
                string CasesDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases";
                DirectoryInfo CasesDirectoryInfo = new DirectoryInfo(CasesDirectory);
                var i = -6;
                var AddDays = 0;
                foreach (var fileInfo in CasesDirectoryInfo.GetFiles())
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases\\" + DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString()))
                    {
                        AddDays++;
                    }

                    if (AddDays == 6)
                    {
                        break;
                    }
                    i++;
                    Console.WriteLine(DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString());
                    Console.WriteLine("DAYS: " + AddDays);
                }

                if (AddDays >= 6)
                {
                    int[] CasesArray = new int[6];
                    i = -6;
                    AddDays = 0;
                    foreach (var fileInfo in CasesDirectoryInfo.GetFiles())
                    {
                        if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases\\" + DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString()))
                        {
                            CasesArray[AddDays] = Int32.Parse(File.ReadAllText(fileInfo.FullName));
                            AddDays++;
                            Console.WriteLine(fileInfo.FullName);
                        }
                        if (AddDays == 6)
                        {
                            break;
                        }
                        i++;
                    }

                    Return = CasesArray.Average().ToString("0");
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            [Obsolete]
            public static async Task<string> GetAverageCasesByRange(int Days)
            {
                string Return = "";
                string CasesDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases";
                DirectoryInfo CasesDirectoryInfo = new DirectoryInfo(CasesDirectory);
                var i = Days - Days - Days;
                var AddDays = 0;
                foreach (var fileInfo in CasesDirectoryInfo.GetFiles())
                {
                    if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases\\" + DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString()))
                    {
                        AddDays++;
                    }
                    i++;
                    Console.WriteLine(DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString());
                    Console.WriteLine("DAYS: " + AddDays);
                }

                if (AddDays >= Days)
                {
                    int[] CasesArray = new int[Days];
                    i = Days - Days - Days;
                    AddDays = 0;
                    foreach (var fileInfo in CasesDirectoryInfo.GetFiles())
                    {
                        if (File.Exists(Environment.GetEnvironmentVariable("APPDATA") + "\\OntarioCases\\" + DateTime.Now.AddDays(i).Month.ToString() + DateTime.Now.AddDays(i).Day.ToString() + DateTime.Now.AddDays(i).Year.ToString()))
                        {
                            CasesArray[AddDays] = Int32.Parse(File.ReadAllText(fileInfo.FullName));
                            AddDays++;
                            Console.WriteLine(fileInfo.FullName);
                        }
                        if (AddDays == Days)
                        {
                            break;
                        }
                        i++;
                    }

                    Return = CasesArray.Average().ToString("0");
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }
        }
        public static class ErrorNews
        {
            ///<summary>Returns "null" if theres no reporting error</summary>
            public static async Task<string> GetNewCasesReportingError()
            {
                return await RunAPIResponder("GetNewCasesReportingError");
            }

            ///<summary>Returns "null" if theres no reporting error</summary>
            public static async Task<string> GetDeathsReportingError()
            {
                return await RunAPIResponder("GetDeathsReportingError");
            }
            ///<summary>Returns "null" if theres no reporting error</summary>
            public static async Task<string> GetHospitalReportingError()
            {
                return await RunAPIResponder("GetHospitalReportingError");
            }
        }
        public static class NewCases
        {
            public static async Task<string> GetTodaysCases()
            {
                string Return = await RunAPIResponder("TodaysCases");
                return Return;
            }

            public static async Task<string> GetNewRecoveredCases()
            {
                return await RunAPIResponder("GetNewRecoveredCases");
            }

            public static async Task<string> GetNewDeaths()
            {
                return await RunAPIResponder("GetNewDeaths");
            }
        }
        public static class TotalCases
        {
            public static async Task<string> GetTotalCases()
            {
                return await RunAPIResponder("TotalCases");
            }

            public static async Task<string> GetRecoveredCases()
            {
                return await RunAPIResponder("RecoveredCases");
            }

            public static async Task<string> GetTotalDeaths()
            {
                return await RunAPIResponder("TodaysDeaths");
            }
        }
        public static class Hospital
        {
            public static async Task<string> GetHospitalized()
            {
                return await RunAPIResponder("Hospitalized");
            }

            public static async Task<string> GetICUCases()
            {
                return await RunAPIResponder("ICUCases");
            }
            /// <summary>
            /// Number of patients currently in ICU, testing positive for COVID
            /// </summary>
            /// <returns></returns>
            public static async Task<string> GetICUPositiveCases()
            {
                return await RunAPIResponder("GetICUPositiveCases");
            }
            /// <summary>
            /// Number of patients currently in ICU due to COVID, no longer testing positive for COVID
            /// </summary>
            /// <returns></returns>
            public static async Task<string> GetICUNegativeCases()
            {
                return await RunAPIResponder("GetICUNegativeCases");
            }
            public static async Task<string> GetICUOnVentilator()
            {
                return await RunAPIResponder("GetICUOnVentilator");
            }

            public static async Task<string> GetICUOnVentilatorPositiveCases()
            {
                return await RunAPIResponder("GetICUOnVentilatorPositiveCases");
            }

            public static async Task<string> GetICUOnVentilatorNegativeCases()
            {
                return await RunAPIResponder("GetICUOnVentilatorNegativeCases");
            }
        }

        public static class GetCasesByDemographics
        {
            public static async Task<string> Male()
            {
                return await RunAPIResponder("MaleCases");
            }

            public static async Task<string> Female()
            {
                return await RunAPIResponder("FemaleCases");
            }

            public static async Task<string> NineteenAndUnder()
            {
                return await RunAPIResponder("19andUnder");
            }

            public static async Task<string> TwentytoThirtyNine()
            {
                return await RunAPIResponder("TwentytoThirtyNine");
            }

            public static async Task<string> FourtyToFiftyNine()
            {
                return await RunAPIResponder("FourtyToFiftyNine");
            }

            public static async Task<string> SixtyToSeventyNine()
            {
                return await RunAPIResponder("SixtyToSeventyNine");
            }

            public static async Task<string> EightyAndOver()
            {
                return await RunAPIResponder("EightyAndOver");
            }
        }

        ///<summary> Get long term home care cases, (must run all other methods before executing this class.)</summary>
        public static class LongTermCare
        {
            private static string RequesterFile = MainDirectory + "\\Requester.txt";



            private static string ReplaceString(string Text)
            {
                return Text.Replace("</tr></thead><tbody><tr><th scope=\"row\">", "").Replace("</th>", "").Replace("<td>", "").Replace("</td>", "").Replace("<td class=\"non-numeric\">", "").Replace("&lt;", "").Replace("\t", "").Replace("</tr><tr><th scope=\"row\">", "");
            }

            private static string ReadAtLineCount(int Line)
            {
                return File.ReadLines(RequesterFile).ElementAtOrDefault(Line);
            }
            /// <summary>
            /// Gets long term care homes that are currently in a outbreak
            /// (Format: Home Name[0], Beds[1], City[2], Resident Cases[3],Staff Cases[4], Deaths[5])
            /// Must split the array (use foreach and split the string)
            /// </summary>
            /// <returns></returns>
            public static string[] GetOutbreakHomesandCases()
            {
                List<string> ReturnList = new List<string>();
                int LineCount = 0;
                foreach (string readLine in File.ReadLines(RequesterFile))
                {
                    //  //
                    if (readLine.Contains("Long-term care homes with an active outbreak"))
                    {
                        Console.WriteLine("Contains");
                        break;
                    }
                    else
                    {
                        LineCount++;
                    }
                }
                Console.WriteLine(LineCount);
                LineCount += 18;
                while (true)
                {
                    string ListAdd = "";
                    if (File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount).Contains("</tr></thead><tbody><tr><th scope=\"row\">") || ReadAtLineCount(LineCount).Contains("</tr><tr><th scope=\"row\">"))
                    {
                        ListAdd += ReplaceString(File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount));
                        LineCount++;
                        ListAdd += " , City: " + ReplaceString(File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount));
                        LineCount++;
                        ListAdd += " , Beds: " + ReplaceString(ReadAtLineCount(LineCount));
                        LineCount++;
                        ListAdd += " , Cases: " + ReplaceString(ReadAtLineCount(LineCount));
                        LineCount++;
                        ListAdd += " , Deaths: " + ReplaceString(ReadAtLineCount(LineCount));
                        LineCount++;
                        ListAdd += " , Staff Cases: " + ReplaceString(ReadAtLineCount(LineCount));
                        if (ListAdd != "")
                            ReturnList.Add(ListAdd);
                    }

                    if (ReadAtLineCount(LineCount).Contains("</tr></tbody></table></div>"))
                    {
                        break;
                    }
                    LineCount++;
                }
                return ReturnList.OfType<string>().ToArray();
            }
            /// <summary>
            /// Gets long term care homes that are no longer in a outbreak
            /// (Format: Home Name[0], City[1], Beds[2], Resident Deaths[3])
            /// Must split to an array to use the "[]"s, use foreach and split each string
            /// </summary>
            /// <returns></returns>
            public static string[] GetNoOutbreakHomeswithCases()
            {
                List<string> ReturnList = new List<string>();
                int LineCount = 0;
                foreach (string readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("Long-term care homes no longer in an outbreak"))
                    {
                        Console.WriteLine("Contains");
                        break;
                    }
                    else
                    {
                        LineCount++;
                    }
                }
                Console.WriteLine(LineCount);
                LineCount += 18;
                while (true)
                {
                    string ListAdd = "";
                    if (File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount).Contains("</tr></thead><tbody><tr><th scope=\"row\">") || ReadAtLineCount(LineCount).Contains("</tr><tr><th scope=\"row\">"))
                    {
                        ListAdd += ReplaceString(File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount));
                        LineCount++;
                        ListAdd += " , City: " + ReplaceString(File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount));
                        LineCount++;
                        ListAdd += " , Beds: " + ReplaceString(ReadAtLineCount(LineCount));
                        LineCount++;
                        ListAdd += " , Resident Deaths: " + ReplaceString(ReadAtLineCount(LineCount));
                        if (ListAdd != "")
                            ReturnList.Add(ListAdd);
                    }

                    if (ReadAtLineCount(LineCount).Contains("</tr></tbody></table></div>"))
                    {
                        break;
                    }
                    LineCount++;
                }
                return ReturnList.OfType<string>().ToArray();
            }
        }

        public static class LongTermCareStatus
        {
            public static async Task<string> InOutbreak()
            {
                return await RunAPIResponder("HomesWithOutbreak");
            }

            public static async Task<string> InOutbreakNoResidentCases()
            {
                return await RunAPIResponder("InOutbreakNoResidentCases");
            }
            public static async Task<string> ResolvedOutbreaks()
            {
                return await RunAPIResponder("ResolvedOutbreaks");
            }

            public static async Task<string> ActiveLTCResidentCases()
            {
                return await RunAPIResponder("ActiveLTCResidentCases");
            }

            public static async Task<string> ActiveLTCStaffCases()
            {
                return await RunAPIResponder("ActiveLTCStaffCases");
            }

            public static async Task<string> LTCResidentDeaths()
            {
                return await RunAPIResponder("LTCResidentDeaths");
            }

            public static async Task<string> LTCStaffDeaths()
            {
                return await RunAPIResponder("LTCStaffDeaths");
            }

            public static async Task<string> TotalLTCResidentCases()
            {
                return await RunAPIResponder("TotalLTCResidentCases");
            }
        }

        private static string MainReadDirectory = Environment.GetEnvironmentVariable("APPDATA") + "\\DownloadedOntarioCases";

        private static async Task<string> RunAPIResponder(string RequestArgument)
        {
            string RequesterFile = MainDirectory + "\\Requester.txt";
            string Return = "";
            Directory.CreateDirectory(MainDirectory);
            //string RequestFile = MainDirectory + "\\RequestFile.txt";
            //File.WriteAllText(RequestFile, RequestArgument);
            string TheFile = MainReadDirectory + "\\" + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Year.ToString();
            //if (File.Exists(TheFile))
            //{
            //    FileAttributes Attr = File.GetAttributes(TheFile);
            //    DateTime now = DateTime.Now;
            //    TimeSpan localOffset = now - now.ToUniversalTime();
            //    DateTime LastModified = File.GetLastWriteTimeUtc(TheFile) + localOffset;
            //    Console.WriteLine(LastModified.Date.ToString("F"));
            //    if(LastModified.Date.Hour == 10)
            //    {
            //        Console.WriteLine("Its now 10:00AM");
            //        if (LastModified.Date.Minute <= 30)
            //        {
            //            Console.WriteLine("Earlier than 10:30AM");
            //            File.Delete(TheFile); 
            //        }
            //    }
            //    else if(LastModified.Date.Hour <= 10)
            //    {
            //        Console.WriteLine("Earlier than 10:00AM");
            //        File.Delete(TheFile);
            //    }
            //}
            if (!File.Exists(TheFile) || !File.Exists(RequesterFile))
            {
                if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirusResponder.exe"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/Engine/OntarioCaseGetter/bin/Debug/OntarioCaseGetter.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirusResponder.exe");
                        while (client.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                }

                if (File.Exists(TheFile) && File.Exists(RequesterFile))
                {

                }
                else
                {
                    if (File.Exists(RequesterFile))
                    {
                        File.Delete(RequesterFile);
                    }
                    await Task.Factory.StartNew(async() =>
                    {
                        try
                        {
                            Process.Start(Environment.GetEnvironmentVariable("TEMP") +
                                          "\\OntarioCoronavirusResponder.exe").WaitForExit();
                        }
                        catch
                        {
                            File.Delete(Environment.GetEnvironmentVariable("TEMP") +
                                        "\\OntarioCoronavirusResponder.exe");
                            using (var client = new WebClient())
                            {
                                client.DownloadFileAsync(new Uri("https://gitlab.com/Cntowergun/computer-essentials/-/raw/master/Coronavirus-Announcement/Engine/OntarioCaseGetter/bin/Debug/OntarioCaseGetter.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\OntarioCoronavirusResponder.exe");
                                while (client.IsBusy)
                                {
                                    await Task.Delay(10);
                                }
                            }
                            Process.Start(Environment.GetEnvironmentVariable("TEMP") +
                                          "\\OntarioCoronavirusResponder.exe").WaitForExit();
                        }
                    });
                    File.WriteAllText(TheFile,"ha");
                }
            }
            //string Return = File.ReadAllText(MainDirectory + "\\Return.txt");
            Console.WriteLine(Return);

            var LineCount = 0;
            bool StopLineCount = false;

            string ReturnFromArgument(string ReadLineContainString, int LinesToAdd)
            {
                string ReturnString = "";
                foreach (var readLine in File.ReadLines(MainDirectory + "\\Requester.txt"))
                {
                    if (readLine.Contains(ReadLineContainString))
                    {
                        StopLineCount = true;
                        Console.WriteLine("Stopped at line: " + LineCount);
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }
                ReturnString = File.ReadLines(MainDirectory + "\\Requester.txt").ElementAtOrDefault(LineCount + LinesToAdd).Replace("<spanlang=\\\"EN-CA\\\"lang=\\\"EN-CA\\\"xml:lang=\\\"EN-CA\\\">", "").Replace("</li>","").Replace("<li>","").Replace("<td>", "").Replace("</td>", "").Replace("&","").Replace("nbsp;","").Replace("\t", "").Replace("<br>", "").Replace("<br/>", "").Replace("</br>", "").Replace("</p>", "").Replace("<p>", "").Replace("Anincreaseof","");
                return ReturnString;
            }
            string ReturnFromArgumentNoReplace(string ReadLineContainString, int LinesToAdd)
            {
                string ReturnString = "";
                foreach (var readLine in File.ReadLines(MainDirectory + "\\Requester.txt"))
                {
                    if (readLine.Contains(ReadLineContainString))
                    {
                        StopLineCount = true;
                        Console.WriteLine("Stopped at line: " + LineCount);
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }
                ReturnString = File.ReadLines(MainDirectory + "\\Requester.txt").ElementAtOrDefault(LineCount + LinesToAdd);
                return ReturnString;
            }
            if (RequestArgument == "GetNewCasesReportingError")
            {
                Return = ReturnFromArgumentNoReplace("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 2).Contains("<td>") ? ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 1) : ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 2);
                if (File.ReadLines(MainDirectory + "\\Requester.txt").ElementAtOrDefault(LineCount + 2).Contains("increase</td>"))
                {
                    Return = "null";
                }
                else if (ReturnFromArgumentNoReplace("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 1).Contains("*<br>"))
                {
                    Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 2);
                }
                else if (ReturnFromArgumentNoReplace("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>",2).Contains("<p>"))
                {
                    Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>",2);
                }
                else if (ReturnFromArgumentNoReplace("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 3).Contains("<p>"))
                {
                    Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", 3);
                }
                else
                {
                    var i = 2;
                    while (!Return.Contains("</td>"))
                    {
                        Return += !ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", i).Contains("</td>") ? " " + ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", i) : "";
                        i++;
                    }

                    Return = Return.Replace(
                        ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", i - 1), "");
                    Return = Return.Replace(
                        ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report (new cases)</th>", i), "");
                }
                Console.WriteLine("New Case Reporting Error: " + Return);
            }
            else if (RequestArgument == "GetICUOnVentilatorNegativeCases")
            {
                Return = ReturnFromArgument("Patients in ICU due to COVID on a ventilator, no longer testing positive for COVID", 6);
            }
            else if (RequestArgument == "GetICUOnVentilatorPositiveCases")
            {
                Return = ReturnFromArgument("Number of patients currently in <abbr title=\"intensive care unit\">ICU</abbr> on a ventilator with COVID-19", 6);
            }
            else if(RequestArgument == "GetICUPositiveCases")
            {
                Return = ReturnFromArgument("Number of patients currently in&nbsp;ICU, testing positive for COVID",6);
            }
            else if (RequestArgument == "GetICUNegativeCases")
            {
                Return = ReturnFromArgument("Number of patients currently in ICU due to COVID, no longer testing positive for COVID", 6);
            }
            else if (RequestArgument == "GetBrazilVariantCases")
            {
                Return = ReturnFromArgument("Lineage P.1 (Brazilian variant)", 6);
            }
            else if (RequestArgument == "GetHospitalReportingError")
            {
                string News = "";
                Return = ReturnFromArgument("Number of patients currently hospitalized with COVID-19", 7);
                if(Return.Contains("</tr><tr><th scope=\"row\">"))
                {
                    Return = "null";
                }
                else
                {
                    var i = 8;
                    while (!Return.Contains("Total patients in ICU due to COVID-related critical illness"))
                    {
                        Return += !ReturnFromArgument("Number of patients currently hospitalized with COVID-19", i)
                            .Contains(
                                "</tr><tr><th scope=\\\"row\\\">Total patients in ICU due to COVID-related critical illness")
                            ? " " + ReturnFromArgument("Number of patients currently hospitalized with COVID-19", i)
                            : "";
                        i++;
                    }

                    Return = Return.Replace(
                        ReturnFromArgument("Number of patients currently hospitalized with COVID-19", i - 1), "");
                    Return = Return.Replace(
                        ReturnFromArgument("Number of patients currently hospitalized with COVID-19", i), "");
                }

                Console.WriteLine("Hospital reporting error: " + Return);
            }
            else if (RequestArgument == "GetAfricanVariantCases")
            {
                Return = ReturnFromArgument("Lineage B.1.351 (South African variant)", 6);
            }
            else if (RequestArgument == "GetUKVariantCases")
            {
                Return = ReturnFromArgument("Lineage B.1.1.7 (UK variant)", 6);
            }
            else if(RequestArgument == "GetNewDeaths")
            {
                Return = ReturnFromArgument("<ul><li>An increase of", 1);
                Return = Return.Replace("<li>An increase of ", "");
                Return = Return.Replace("deaths", "");
                Return = Return.Replace(" ", "");
                Return = Return.Replace("Anincreaseof", "");
                Return = Return.Split('(')[0].Trim();
                Return = Return.Replace("<spanlang=\\\"EN-CA\\\"lang=\\\"EN-CA\\\"xml:lang=\\\"EN-CA\\\">18", "");
            }
            else if (RequestArgument == "GetNewRecoveredCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Change from previous report</th>", 1);
            }
            else if (RequestArgument == "TotalLTCResidentCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Subset of all cases that are reported to be long-term care residents", 16);
            }
            else if (RequestArgument == "GetICUOnVentilator")
            {
                Return = ReturnFromArgument("Total patients in ICU on a ventilator due to COVID-related critical illness", 11);
            }
            else if (RequestArgument == "LTCStaffDeaths")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Staff deaths associated with <abbr title=\"long-term care\">LTC</abbr> homes</th>", 1);
            }
            else if(RequestArgument == "LTCResidentDeaths")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Resident deaths in <abbr title=\"long-term care\">LTC</abbr> homes</th>", 1);
            }
            else if (RequestArgument == "ActiveLTCStaffCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Confirmed active cases of positive staff</th>", 1);
            }
            else if (RequestArgument == "ActiveLTCResidentCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Confirmed active cases of positive residents</th>",1);
            }
            else if (RequestArgument == "ResolvedOutbreaks")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\"><abbr title=\"long-term care\">LTC</abbr> homes with resolved outbreaks</th>", 1);
            }
            else if (RequestArgument == "InOutbreakNoResidentCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\"><abbr title=\"long-term care\">LTC</abbr> homes in outbreak with no resident cases</th>", 1);
            }
            else if (RequestArgument == "HomesWithOutbreak")
            {
                Return = ReturnFromArgument("</tr></thead><tbody><tr><th scope=\"row\"><abbr title=\"long-term care\">LTC</abbr> homes with an outbreak</th>", 1);
            }
            else if (RequestArgument == "GetDeathsReportingError")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Total number of deaths", 7);
                if (!File.ReadLines(MainDirectory + "\\Requester.txt").ElementAtOrDefault(LineCount + 7).Contains("<p>"))
                {
                    Return = "null";
                }
                Console.WriteLine("Death Reporting Error: " + Return);
            }
            else if (RequestArgument == "TodaysCases")
            {
                foreach (var VARIABLE in File.ReadLines(MainDirectory + "\\Requester.txt"))
                {
                    if (VARIABLE.Contains("Change from previous report (new cases)"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }
                Console.WriteLine("LINES: " + LineCount);
                string ReturnCases = File.ReadLines(MainDirectory + "\\Requester.txt").ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("\t", "").Replace("<br>", "").Replace("<br/>", "").Replace("</br>", "").Replace("*","");
                Console.WriteLine(ReturnCases);
                Return = ReturnCases;
            }
            else if (RequestArgument == "TodaysDeaths")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Total number of deaths", 6);
                Console.WriteLine("Deaths: " + Return);
            }
            else if (RequestArgument == "PendingTests")
            {
                Return = ReturnFromArgument("Currently under investigation", 6);
                Console.WriteLine("Pending test results: " + Return);
            }
            else if (RequestArgument == "YesterdaysTests")
            {
                Return = ReturnFromArgument("Total tests completed in the previous day", 6);
                Console.WriteLine("Yesterdays test results: " + Return);
            }
            else if (RequestArgument == "Hospitalized")
            {
                Return = ReturnFromArgument("Number of patients currently hospitalized with COVID-19", 6);
                Console.WriteLine("Current Hospitalized: " + Return);
            }
            else if (RequestArgument == "TotalCases")
            {
                Return = ReturnFromArgument("</tr></thead><tbody><tr><th scope=\"row\">Number of cases", 6);
                Console.WriteLine("Total cases: " + Return);
            }
            else if (RequestArgument == "RecoveredCases")
            {
                Return = ReturnFromArgument("</tr><tr><th scope=\"row\">Resolved", 6);
                Console.WriteLine("Recovered cases: " + Return);
            }
            else if (RequestArgument == "MaleCases")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr></thead><tbody><tr><th scope=\"row\">Male</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "FemaleCases")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">Female</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "19andUnder")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">19 and under</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "TwentytoThirtyNine")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">20-39</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "FourtyToFiftyNine")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">40-59</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "SixtyToSeventyNine")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">60-79</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }

                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "EightyAndOver")
            {
                foreach (var readLine in File.ReadLines(RequesterFile))
                {
                    if (readLine.Contains("</tr><tr><th scope=\"row\">80 and over</th>"))
                    {
                        StopLineCount = true;
                    }
                    else
                    {
                        if (StopLineCount == false)
                        {
                            LineCount++;
                        }
                    }
                }
                string ReturnCases = File.ReadLines(RequesterFile).ElementAtOrDefault(LineCount + 1)
                    .Replace("<td>", "").Replace("</td>", "").Replace("<br>", "").Replace("\t", "");
                Return = ReturnCases;
            }
            else if (RequestArgument == "ICUCases")
            {
                Return = ReturnFromArgument("Total patients in ICU due to COVID-related critical illness", 11);
                Console.WriteLine("Intensive care unit: " + Return);
            }

            Console.WriteLine("Line Read: " + LineCount);
            return Return;
        }
    }
}

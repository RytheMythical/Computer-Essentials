using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ParseHub.Client;
using ParseHub.Client.Models;
using YRDSBCOVIDAPI;

namespace Engine
{
    public class yrdsbcovid
    {
        Project project = new Project();
        ProjectRunner projectrunner = new ProjectRunner("tsec8884UacR");
        private string Storage = "";


        public async Task<string> GetData()
        {
            if (Storage == "")
            {
                var InitialRun = projectrunner.RunProject("t9_KDxug-Tuq");
                Console.WriteLine("Run Token: " + InitialRun.RunToken + " Data Ready: " + InitialRun.DataReady);
                string RunToken = InitialRun.RunToken;
                while (!new ProjectRunner("tsec8884UacR").GetRun(RunToken).DataReady)
                {
                    await Task.Delay(10);
                }
                string Data = new ProjectRunner("tsec8884UacR").GetRun(RunToken).Data;
                Storage = Data;
            }
            return Storage;
        }


        public async Task<string> GetSchoolCase(string School)
        {
            string Return = "";
            var COVIDData = Yrdsbcovid.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/yrdsb/yrdsb.txt"));
            foreach (School school in COVIDData.School)
            {
                CultureInfo cl = new CultureInfo("en-US");
                if (cl.CompareInfo.IndexOf(school.Name, School, CompareOptions.IgnoreCase) >= 0)
                {
                    Return = school.Cases;
                }
            }
            return Return;
        }

        public async Task<string> GetSchoolClosureStatus(string School)
        {
            string Return = "";
            var COVIDData = Yrdsbcovid.FromJson(new WebClient().DownloadString("http://covid.bigheados.com/yrdsb/yrdsb.txt"));
            foreach (School school in COVIDData.School)
            {
                CultureInfo cl = new CultureInfo("en-US");
                if (cl.CompareInfo.IndexOf(school.Name, School, CompareOptions.IgnoreCase) >= 0)
                {
                    Return = school.ClosureStatus;
                }
            }
            return Return;
        }

    }
}

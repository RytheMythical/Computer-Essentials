using Newtonsoft.Json;
using System;
using System.Net;
using System.Runtime.CompilerServices;

namespace Coronavirus_API
{
    public class COVID
    {
        public bool RemoveComma { get; set; }

        private static string CovidJsonDataStorage = "";
        private static bool RealTime { get; set; }

        public bool RealTimeCheck
        {
            get
            {
                return RealTime;
            }
            set
            {
                RealTime = value;
            }
        }

        private static string CovidJsonData
        {
            get
            {
                return GetData();
            }
        }

        private static string GetData()
        {
            string Return = "";
            if (CovidJsonDataStorage == "" || RealTime)
            {
                CovidJsonDataStorage = new WebClient().DownloadString("https://corona-virus-stats.herokuapp.com/api/v1/cases/general-stats");
            }
            return CovidJsonDataStorage;
        }
        private Root CovidData = JsonConvert.DeserializeObject<Root>(CovidJsonData); 
        private class Data
        {
            public string total_cases { get; set; }
            public string recovery_cases { get; set; }
            public string death_cases { get; set; }
            public string last_update { get; set; }
            public string currently_infected { get; set; }
            public string cases_with_outcome { get; set; }
            public string mild_condition_active_cases { get; set; }
            public string critical_condition_active_cases { get; set; }
            public string recovered_closed_cases { get; set; }
            public string death_closed_cases { get; set; }
            public string closed_cases_recovered_percentage { get; set; }
            public string closed_cases_death_percentage { get; set; }
            public string active_cases_mild_percentage { get; set; }
            public string active_cases_critical_percentage { get; set; }
            public string general_death_rate { get; set; }
        }

        public string TotalCases()
        {
            return Replace(CovidData.data.total_cases);
        }

        public string RecoveredCases()
        {
            return Replace(CovidData.data.recovery_cases);
        }

        public string Deaths()
        {
            return Replace(CovidData.data.death_cases);
        }

        public string CriticalCases()
        {
            return Replace(CovidData.data.critical_condition_active_cases);
        }

        public string ActiveCases()
        {
            return Replace(CovidData.data.currently_infected);
        }
        private class Root
        {
            public Data data { get; set; }
            public string status { get; set; }
        }

        private string Replace(string g)
        {
            string Return = "";
            if (RemoveComma == false)
            {
                Return = g;
            }
            else
            {
                Return = g.Replace(",", "");
            }
            return Return;
        }

    }
}

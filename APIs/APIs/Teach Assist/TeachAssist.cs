using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using TeachAssistAPI;

namespace Teach_Assist
{
    public static class TeachAssist
    {
        public static string Password { get; set; }
        public static string Username { get; set; }

        public static TeachAssistAPI.MarkScraper Scrapper = new MarkScraper();
        public static Root GetMarks = JsonConvert.DeserializeObject<Root>(Scrapper.ScrapeMarks("335384137", "re6z4pev"));
        
        public static List<string> GetAllAverages()
        {
            List<string> Return = new List<string>();
            foreach (Assessment assessment in GetMarks.assessments)
            {
                foreach (Mark assessmentMark in assessment.marks)
                {
                    Return.Add(assessment.name + ": " + assessmentMark.percentage);
                }
            }

            return Return;
        }
    }

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Mark
    {
        public int markCategory { get; set; }
        public int weightValue { get; set; }
        public int percentage { get; set; }
        public string contents { get; set; }
    }

    public class Assessment
    {
        public string name { get; set; }
        public double percentage { get; set; }
        public int totalWeightValue { get; set; }
        public bool isformative { get; set; }
        public List<Mark> marks { get; set; }
    }

    public class Root
    {
        public string code { get; set; }
        public double average { get; set; }
        public List<Assessment> assessments { get; set; }
    }


}

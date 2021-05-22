using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Command
    {
        public static void RunCommand(string Command)
        {
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat", CommandChut);
            var C = Process.Start(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
            C.WaitForExit();
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand.bat");
        }

        private static bool Exit = false;
        public static async Task RunCommandHidden(string Command)
        {
            Random dew = new Random();
            int hui = dew.Next(0000, 9999);
            string[] CommandChut = { Command };
            File.WriteAllLines(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat", CommandChut);
            Process C = new Process();
            C.StartInfo.FileName = System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat";
            C.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            C.EnableRaisingEvents = true;
            C.Exited += C_Exited1;
            C.Start();
            while (Exit == false)
            {
                await Task.Delay(10);
            }

            Exit = false;
            File.Delete(System.Environment.GetEnvironmentVariable("USERPROFILE") + "\\Documents\\RunCommand" + hui + ".bat");
        }

        private static void C_Exited1(object sender, EventArgs e)
        {
            Exit = true;
        }

    }
}

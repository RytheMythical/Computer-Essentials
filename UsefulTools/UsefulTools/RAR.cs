using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace UsefulTools
{
    public static class RAR
    {
        public static void Rar(string Archive, string Files)
        {
            string[] Run = { "cd \"C:\\Program Files\\WinRAR\"", "rar a \"" + Archive + "\"" + " " + "\"" + Files + "\"" };
            File.WriteAllLines("C:\\Unrar.bat", Run);
            var dew = Process.Start("C:\\Unrar.bat");
            dew.WaitForExit();
        }
        public static void Unrar(string Archive, string Output)
        {
            string[] Run = { "cd \"C:\\Program Files\\WinRAR\"", "unrar x \"" + Archive + "\"" + " " + "\"" + Output + "\"" };
            File.WriteAllLines("C:\\Unrar.bat", Run);
            var dew = Process.Start("C:\\Unrar.bat");
            dew.WaitForExit();
        }
    }
}

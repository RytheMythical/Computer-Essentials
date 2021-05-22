using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class VeraCrypt
    {
        public static string DefaultVeracryptEXEPath = "\"C:\\Program Files\\VeraCrypt\\VeraCrypt.exe\"";
        public static async Task MountVeracrypt(string Path, string Password, string PIM, string Letter)
        {
            await Command.RunCommandHidden(DefaultVeracryptEXEPath + " /q /v \"" + Path + "\" /l " + Letter + " /a /p " + Password + " /pim " + PIM + " /e /b /s");
        }
        public static async Task DismountVeracrypt(string Letter)
        {
            await Command.RunCommandHidden("\"C:\\Program Files\\VeraCrypt\\VeraCrypt.exe\" /q /d " + Letter + " /s");
        }

        public static async Task CreateVeracryptVolume(string Path, string Password, string Size, string PIM)
        {
            await Command.RunCommandHidden("\"C:\\Program Files\\VeraCrypt\\VeraCrypt Format.exe\" /create " + Path +
                                   " /password " + Password + " /pim " + PIM + " /hash sha512 /encryption serpent /filesystem FAT /size " + Size + "M /force /silent /quick");
        }
    }
}

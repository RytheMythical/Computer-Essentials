using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using File_Vault_API;

namespace Create_Vault_Console_Testing_App
{
    class Program
    {
        static async Task Main(string[] args)
        {
            File_Vault Vault = new File_Vault();
            Vault.VaultInfo.Size = 100;
            Vault.VaultInfo.Password = "1234";
            Vault.VaultInfo.Path = Environment.GetEnvironmentVariable("TEMP") + "\\MEH";
            Vault.VaultInfo.PIM = 500;
            await Vault.CreateFileVault();
            Console.WriteLine(Vault.DriveLetter);
            await Task.Delay(-1);
        }
    }
}

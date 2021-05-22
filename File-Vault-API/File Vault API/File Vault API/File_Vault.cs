using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace File_Vault_API
{
    public class File_Vault : File_Vault_Info 
    {
        private MainFunctions Main = new MainFunctions();
        /// <summary>
        /// Fires whenever a file vault starts closing
        /// </summary>
        public event EventHandler ClosingVaultStarted;
        /// <summary>
        /// Fires whenever a file vault successfully closes
        /// </summary>
        public event EventHandler ClosedVault;
        /// <summary>
        /// Fires whenever a file vault successfully opens
        /// </summary>
        public event EventHandler OpenedVault;
        /// <summary>
        /// Fires whenever a file vault is finished creating and mounted
        /// </summary>
        public event EventHandler VaultCreated;
        /// <summary>
        /// Fires whenever a file vault is starting to create
        /// </summary>
        public event EventHandler CreatingVaultStarted;
        /// <summary>
        /// Fires whenever a file vault is starting to open
        /// </summary>
        public event EventHandler OpeningVaultStarted;
        /// <summary>
        /// True if the program is busy
        /// </summary>
        public bool IsBusy = false;

        private string VeraCryptPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VeraCryptInstallation";
        private string WinRARPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\WinRARInstallation";
        private string VeraCryptPathEXE = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\VeraCryptInstallation\\VeraCrypt.exe";
        private string TempVaultDirectoryStore = "";
        private string VHDPathStore = "";
        private string SecondDriveLetterStore = "";

        private string TempVaultDirectory
        {
            get
            {
                if (TempVaultDirectoryStore == "")
                {
                    TempVaultDirectoryStore = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName().Replace(".", ""));
                }
                return TempVaultDirectoryStore;
            }
        }
        public string DriveLetter
        {
            get
            {
                if (VaultInfo.CustomDriveLetter != "")
                {
                    VaultInfo.CustomDriveLetter = Main.GetRandomDriveLetter1();
                }
                return VaultInfo.CustomDriveLetter;
            }
        }
        public File_Vault()
        {
            if (!Directory.Exists(VeraCryptPath))
            {
                Directory.CreateDirectory(VeraCryptPath);
                File.WriteAllBytes(VeraCryptPath + "\\VeraCryptZip.zip", Resources.VeraCrypt);
                ZipFile.ExtractToDirectory(VeraCryptPath + "\\VeraCryptZip.zip", VeraCryptPath);
                File.Delete(VeraCryptPath + "\\VeraCryptZip.zip");
            }

            if (!Directory.Exists(WinRARPath))
            {
                Directory.CreateDirectory(WinRARPath);
                File.WriteAllBytes(WinRARPath + "\\WinRAR.zip",Resources.WinRAR);
                ZipFile.ExtractToDirectory(WinRARPath + "\\WinRAR.zip",WinRARPath);
                File.Delete(WinRARPath + "\\WinRAR.zip");
            }
        }
        public async Task CreateFileVault()
        {
            IsBusy = true;
            CreatingVaultStarted?.Invoke(this, new EventArgs());
            string SecondDriveLetter = Main.GetRandomDriveLetter1();
            SecondDriveLetterStore = SecondDriveLetter;
            string Path = VaultInfo.Path;
            string Password = Main.UniqueHashing(VaultInfo.Password);
            int PIM = VaultInfo.PIM;
            DirectoryInfo VaultDirectoryInfo = Directory.CreateDirectory(TempVaultDirectory);
            string VeraCryptVaultPath = TempVaultDirectory + "\\VeraCryptVault";
            string VHDPath = TempVaultDirectory + "\\VHDPath.vhd";
            await Main.CreateVeracryptVolume(VeraCryptVaultPath, Password, VaultInfo.Size.ToString(), VaultInfo.PIM.ToString());
            await Main.MountVeracrypt(VeraCryptVaultPath, Password, PIM.ToString(), SecondDriveLetter);
            while (!Directory.Exists(SecondDriveLetter + ":\\"))
            {
                await Task.Delay(10);
            }
            await Main.CreateVHD((VaultInfo.Size + 10).ToString("0"), SecondDriveLetter + ":\\VHD.vhd", DriveLetter);
            VHDPath = SecondDriveLetter + ":\\VHD.vhd";
            VHDPathStore = VHDPath;
            await Main.MountVHD(VHDPath,DriveLetter);
            await Main.RunCommandHidden("taskkill /f /im explorer.exe\nstart \"\" \"%windir%\\explorer.exe\"\nstart \"\" " + DriveLetter + ":\\");
            VaultCreated?.Invoke(this, new EventArgs());
            IsBusy = false;
        }

        public async Task OpenFileVault()
        {
            IsBusy = true;
            OpeningVaultStarted?.Invoke(this, new EventArgs());
            await Task.Factory.StartNew(() =>
            {
                Main.Unrar(VaultInfo.Path, TempVaultDirectory, Main.UniqueHashing(VaultInfo.Password));
            });
            DirectoryInfo VaultDirectoryInfo = Directory.CreateDirectory(TempVaultDirectory);
            string SecondDriveLetter = Main.GetRandomDriveLetter1();
            SecondDriveLetterStore = SecondDriveLetter;
            string VHDPath = TempVaultDirectory + "\\OpenVHD.vhd";
            string OriginalPath = VaultInfo.Path;
            string Password = VaultInfo.Password;
            string PIM = VaultInfo.PIM.ToString();
            await Main.MountVeracrypt(OriginalPath,Password,PIM,SecondDriveLetter);
            await Main.MountVHD(SecondDriveLetter + ":\\VHD.vhd",DriveLetter);
            VHDPathStore = SecondDriveLetter + ":\\VHD.vhd";
            OpenedVault?.Invoke(this, new EventArgs());
            IsBusy = false;
        }

        public async Task CloseFileVault()
        {
            IsBusy = true;
            ClosingVaultStarted?.Invoke(this, new EventArgs());
            string VeraCryptVaultPath = TempVaultDirectory + "\\VeraCryptVault";
            await Main.DismountVHD(VHDPathStore);
            await Main.DismountVeracrypt(SecondDriveLetterStore);
            File.Move(VeraCryptVaultPath,"C:\\Vera");
            await Task.Factory.StartNew(() =>
            {
                Main.Rar(VaultInfo.Path, "C:\\Vera", Main.UniqueHashing(VaultInfo.Password));
            });
            File.Delete("C:\\Vera");
            Main.DeleteDirectory(TempVaultDirectory);
            ClosedVault?.Invoke(this, new EventArgs());
            IsBusy = false;
        }
    }

}

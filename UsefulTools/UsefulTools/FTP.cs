using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UsefulTools
{
    public static class FTP
    {
        public static string Username { get; set; }
        public static string Password { get; set; }

        public static async Task FTPCreateDirectory(string link)
        {
            WebRequest request = WebRequest.Create(link);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(Username, Password);
            using (var resp = (FtpWebResponse)request.GetResponse())
            {
                Console.WriteLine(resp.StatusCode);
            }
        }

        public static List<string> DirectoryListing(string Path, string ServerAdress, string Login, string Password)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://" + ServerAdress + Path);
            request.Credentials = new NetworkCredential(Login, Password);

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();
            StreamReader reader = new StreamReader(responseStream);

            List<string> result = new List<string>();

            while (!reader.EndOfStream)
            {
                result.Add(reader.ReadLine());
            }

            reader.Close();
            response.Close();

            return result;
        }

        public static void DeleteFTPFile(string Path, string ServerAdress)
        {
            FtpWebRequest clsRequest = (System.Net.FtpWebRequest)WebRequest.Create("ftp://" + ServerAdress + Path);
            clsRequest.Credentials = new System.Net.NetworkCredential(Username, Password);

            clsRequest.Method = WebRequestMethods.Ftp.DeleteFile;

            string result = string.Empty;
            FtpWebResponse response = (FtpWebResponse)clsRequest.GetResponse();
            long size = response.ContentLength;
            Stream datastream = response.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            result = sr.ReadToEnd();
            sr.Close();
            datastream.Close();
            response.Close();
        }

        public static void DeleteFTPDirectory(string Path, string ServerAdress, string Login, string Password)
        {
            FtpWebRequest clsRequest = (System.Net.FtpWebRequest)WebRequest.Create("ftp://" + ServerAdress + Path);
            clsRequest.Credentials = new System.Net.NetworkCredential(Login, Password);

            List<string> filesList = DirectoryListing(Path, ServerAdress, Login, Password);

            foreach (string file in filesList)
            {
                DeleteFTPFile(Path + file, ServerAdress);
            }

            clsRequest.Method = WebRequestMethods.Ftp.RemoveDirectory;

            string result = string.Empty;
            FtpWebResponse response = (FtpWebResponse)clsRequest.GetResponse();
            long size = response.ContentLength;
            Stream datastream = response.GetResponseStream();
            StreamReader sr = new StreamReader(datastream);
            result = sr.ReadToEnd();
            sr.Close();
            datastream.Close();
            response.Close();
        }

        public static bool DeleteFileOnFtpServer(Uri serverUri)
        {
            try
            {
                // The serverUri parameter should use the ftp:// scheme.
                // It contains the name of the server file that is to be deleted.
                // Example: ftp://contoso.com/someFile.txt.
                // 

                if (serverUri.Scheme != Uri.UriSchemeFtp)
                {
                    return false;
                }
                // Get the object used to communicate with the server.
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);
                request.Credentials = new NetworkCredential(Username, Password);
                request.Method = WebRequestMethods.Ftp.DeleteFile;

                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                response.Close();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static void DownloadFtpDirectory(string url, NetworkCredential credentials, string localPath)
        {
            FtpWebRequest listRequest = (FtpWebRequest)WebRequest.Create(url);
            listRequest.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
            listRequest.Credentials = credentials;

            List<string> lines = new List<string>();

            using (FtpWebResponse listResponse = (FtpWebResponse)listRequest.GetResponse())
            using (Stream listStream = listResponse.GetResponseStream())
            using (StreamReader listReader = new StreamReader(listStream))
            {
                while (!listReader.EndOfStream)
                {
                    lines.Add(listReader.ReadLine());
                }
            }

            foreach (string line in lines)
            {
                string[] tokens =
                    line.Split(new[] { ' ' }, 9, StringSplitOptions.RemoveEmptyEntries);
                string name = tokens[8];
                string permissions = tokens[0];

                string localFilePath = Path.Combine(localPath, name);
                string fileUrl = url + name;

                if (permissions[0] == 'd')
                {
                    if (!Directory.Exists(localFilePath))
                    {
                        Directory.CreateDirectory(localFilePath);
                    }

                    DownloadFtpDirectory(fileUrl + "/", credentials, localFilePath);
                }
                else
                {
                    FtpWebRequest downloadRequest = (FtpWebRequest)WebRequest.Create(fileUrl);
                    downloadRequest.Method = WebRequestMethods.Ftp.DownloadFile;
                    downloadRequest.Credentials = credentials;

                    using (FtpWebResponse downloadResponse =
                              (FtpWebResponse)downloadRequest.GetResponse())
                    using (Stream sourceStream = downloadResponse.GetResponseStream())
                    using (Stream targetStream = File.Create(localFilePath))
                    {
                        byte[] buffer = new byte[10240];
                        int read;
                        while ((read = sourceStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            targetStream.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FileDownloader
{
    class FileDownloader
    {
        public string UrlsFile { get; set; }
        public string DownloadFolder { get; set; }

        public FileDownloader(string urlsFile, string downloadFolder)
        {
            UrlsFile = urlsFile;
            DownloadFolder = downloadFolder;
            Directory.CreateDirectory(DownloadFolder);
        }

        public void DownloadAll()
        {
            string[] urls = File.ReadAllLines(UrlsFile);
            int remainingFiles = urls.Length;

            using (WebClient client = new WebClient())
            {
                Stopwatch sw = new Stopwatch();
                client.DownloadProgressChanged += (sender, e) =>
                {
                    if (e.ProgressPercentage > 0 && sw.Elapsed.TotalSeconds > 0)
                    {
                        double bytesPerSecond = e.BytesReceived / sw.Elapsed.TotalSeconds;
                        double secondsRemaining = (e.TotalBytesToReceive - e.BytesReceived) / bytesPerSecond;
                        Console.Write("\rDownloading {0}: {1}% ({2:0.0} seconds remaining)   ", e.UserState, e.ProgressPercentage, secondsRemaining);
                    }
                };
                client.DownloadFileCompleted += (sender, e) =>
                {
                    Console.WriteLine("\rDownload of {0} completed.", e.UserState);
                    remainingFiles--;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("{0} files remaining.", remainingFiles);
                    Console.ResetColor();
                };

                foreach (string url in urls)
                {
                    string fileName = Path.GetFileName(url);
                    string filePath = Path.Combine(DownloadFolder, fileName);
                    sw.Restart();
                    client.DownloadFileAsync(new Uri(url), filePath, fileName);
                    while (client.IsBusy) { }
                    sw.Stop();
                }
            }
        }

        public void RenameDownloadFolder()
        {
            string newFolderName = null;
            while (string.IsNullOrWhiteSpace(newFolderName))
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("What would you like to name the download folder? ");
                Console.ResetColor();
                newFolderName = Console.ReadLine().Trim();
            }
            string newFolderPath = Path.Combine(Directory.GetCurrentDirectory(), newFolderName);
            Directory.Move(DownloadFolder, newFolderPath);
            DownloadFolder = newFolderPath;
        }
    }
}

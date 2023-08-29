using System;
using System.Diagnostics;
using System.IO;
using System.Net;

namespace FileDownloader
{
    class Program
    {
        private static void NoUrlInFile()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You must add some links to `urls.txt` file");
            Console.ResetColor();
        }

        static void Main(string[] args)
        {
            string urlsFile = "urls.txt";
            string[] urls = File.ReadAllLines(urlsFile);

            if (urls.Length > 0)
            {
                string downloadFolder = Path.Combine(Directory.GetCurrentDirectory(), "Download");
                FileDownloader downloader = new FileDownloader(urlsFile, downloadFolder);

                downloader.DownloadAll();
                downloader.RenameDownloadFolder();
            } else
            {
                NoUrlInFile();
            }
        }
    }
}

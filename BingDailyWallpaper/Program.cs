using BingDailyWallpaper.Models;
using BingDailyWallpaper.Notification;
using BingDailyWallpaper.Storage;
using BingDailyWallpaper.Utils;
using BingDailyWallpaper.Web;
using SBToolkit.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingDailyWallpaper
{
    public class Program
    {
        /// <summary>
        /// Entry point.
        /// </summary>
        static void Main(string[] args)
        {
            ProcessArguments(args);

            var program = new Program();

            program.Run();
        }

        /// <summary>
        /// Handle lazy loading.
        /// </summary>
        private static void ProcessArguments(string[] args)
        {
            if (args.Length == 1 && args[0] == "lazy")
                Thread.Sleep(10_000);
        }

        private void Run()
        {
            Settings.Load();

            try
            {
                // If the application was launched within the last one hour do nothing, return
                if (Settings.Current.LastDownloadTime.ElapsedTime() < TimeSpan.FromHours(1))
                {
                    Environment.Exit(0);
                }

                DownloadImages()
                    .Store()
                    .SetWallpaper()
                    .Notify();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
            finally
            {
                Settings.Current.Save();
            }
        }

        /// <summary>
        /// Download the list of images. 
        /// </summary>
        private IEnumerable<Image> DownloadImages()
        {
            var client = new WebClient();
            var serializer = new XmlSerializer(typeof(Image));

            for (int i = 0; i < Bing.MaxArchive; i++)
            {
                string url = String.Format(Bing.ArchiveUrl, i);

                string xml = client.TryDownloadString(url);

                yield return Image.Parse(serializer, xml);
            }

            Settings.Current.LastDownloadTime = DateTime.Now;
        }
    }
}

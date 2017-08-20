using BingDailyWallpaper.Models;
using BingDailyWallpaper.Settings;
using BingDailyWallpaper.Utils;
using Microsoft.Toolkit.Uwp.Notifications;
using SBToolkit.Core.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace BingDailyWallpaper
{
    class Program
    {
        const string XmlUrl = "http://az517271.vo.msecnd.net/TodayImageService.svc/HPImageArchive?&mkt=en-ww&idx={0}";
        const string DefaultDirectoryName = @"D:\Sacha\OneDrive\Images\Fonds d'écran\Bing";

        static void Main(string[] args)
        {
            // Load the settings
            ApplicationSettings.Load();
            
            // If the application was launched within the last two hours do nothing, return
            if (ApplicationSettings.Default.LastDownloadTime.ElapsedTime() < TimeSpan.FromHours(2))
                Environment.Exit(0);

            if (args.Length == 1 && args[0] == "lazy")
                Thread.Sleep(10_000);

            List<Image> images = DownloadImages();

            // Set the most recent image as wallpaper one time.
            Image mostRecentImage = images.OrderByDescending(img => img.Date).First();
            
            if (ApplicationSettings.Default.LastDefinedWallpaper != mostRecentImage.FileName)
            {
                Wallpaper.Set(mostRecentImage.FileName);
#if !DEBUG
                Thread.Sleep(60_000);
#endif
                ShowNotification(mostRecentImage);
                ApplicationSettings.Default.LastDefinedWallpaper = mostRecentImage.FileName;
            }

            ApplicationSettings.Default.Save();
        }

        /// <summary>
        /// Download the last eight daily Bing images.
        /// </summary>
        /// <returns></returns>
        private static List<Image> DownloadImages()
        {
            var images = new List<Image>();
            var client = new WebClient();
            var serializer = new XmlSerializer(typeof(Image));

            for (int i = 0; i <= 8; i++)
            {
                string xml = client.DownloadString(String.Format(XmlUrl, i));
                using (var reader = new StringReader(xml))
                {
                    var image = serializer.Deserialize(reader) as Image;

                    image.FileName = Path.Combine(DefaultDirectoryName, image.Date.ToString("yyyy-MM-dd") + ".jpg");

                    if (!File.Exists(image.FileName))
                        client.DownloadFile(image.Url, image.FileName);

                    images.Add(image);
                }
            }

            ApplicationSettings.Default.LastDownloadTime = DateTime.Now;

            return images;
        }

        private static void ShowNotification(Image image)
        {
            int position = image.Copyright.LastIndexOf(" (");
            string copyright = image.Copyright.Substring(0, position);

            ToastContent content = new ToastContent()
            {
                Duration = ToastDuration.Long,

                Launch = typeof(Program).Namespace,

                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = copyright
                            },
                            new AdaptiveText()
                            {
                                Text = "Nouveau fond d'écran 📸"
                            },
                            new AdaptiveImage()
                            {
                                Source = image.FileName
                            }
                        }
                    }
                }
            };

            var doc = new XmlDocument();
            doc.LoadXml(content.GetContent());

            var toast = new ToastNotification(doc);
            ToastNotificationManager.CreateToastNotifier("Test").Show(toast);
        }
    }
}

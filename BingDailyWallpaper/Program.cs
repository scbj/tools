using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingDailyWallpaper
{
    class Program
    {
        const string XmlUrl = "http://az517271.vo.msecnd.net/TodayImageService.svc/HPImageArchive?&mkt=en-ww&idx={0}";
        const string DefaultDirectoryName = @"D:\Sacha\OneDrive\Images\Fonds d'écran\Bing";

        static void Main(string[] args)
        {
            IEnumerable<Image> images = DownloadImages();

            // Set the most recent image as wallpaper.
            Image mostRecentImage = images.OrderByDescending(img => img.Date).First();
            Wallpaper.Set(mostRecentImage.FileName);
        }

        /// <summary>
        /// Download the last eight daily Bing images.
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Image> DownloadImages()
        {
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

                    yield return image;
                }
            }
        }
    }
}

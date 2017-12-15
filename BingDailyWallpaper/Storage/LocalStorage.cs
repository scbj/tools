using BingDailyWallpaper.IO;
using BingDailyWallpaper.Models;
using BingDailyWallpaper.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Storage
{
    public static class LocalStorage
    {
        /// <summary>
        /// Store the specified images locally if they do not exist.
        /// </summary>
        public static IEnumerable<Image> Store(this IEnumerable<Image> images)
        {
            WebClient client = null;

            Directory.CreateDirectory(Settings.Current.LocalDirectoryPath);

            foreach (Image image in images)
            {
                if (FileExtend.NotExists(image.FileName))
                {
                    DownloadImage(image);
                }

                yield return image;
            }

            void DownloadImage(Image image)
            {
                // Lazy loading of WebClient component
                if (client == null)
                {
                    client = new WebClient();
                }

                client.TryDownloadFileAsync(image.Url, image.FileName);
            }
        }
    }
}

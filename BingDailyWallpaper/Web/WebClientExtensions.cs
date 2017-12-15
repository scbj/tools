using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Web
{
    public static class WebClientExtensions
    {
        public const int DefaultRetryCount = 3;

        /// <summary>
        /// Try toownloads the requested resource as a System.String. The resource to download
        ///  is specified as a System.String containing the URI.
        /// </summary>
        public static string TryDownloadString(this WebClient client, string address)
        {
            int count = 0;

            do
            {
                try
                {
                    string data = client.DownloadString(address);

                    if (data != null)
                    {
                        return data;
                    }
                }
                catch (Exception)
                {
                }
            } while (++count <= DefaultRetryCount);

            return null;
        }

        /// <summary>
        /// Try to downloads the resource with the specified URI to a local file.
        /// </summary>
        /// <param name="address">The URI from which to download data.</param>
        /// <param name="fileName">The name of the local file that is to receive the data.</param>
        public static void TryDownloadFile(this WebClient client, string address, string fileName)
        {
            int count = 0;

            do
            {
                try
                {
                    client.DownloadFile(address, fileName);
                }
                catch (Exception)
                {
                }
            } while (++count <= DefaultRetryCount);
        }
    }
}

using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper
{
    public sealed class Wallpaper
    {
        const int SPI_SETDESKWALLPAPER = 20;
        const int SPIF_UPDATEINIFILE = 0x01;
        const int SPIF_SENDWININICHANGE = 0x02;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        static extern int SystemParametersInfo(int uAction, int uParam, string lpvParam, int fuWinIni);

        /// <summary>
        /// Set the spiecified image as desktop wallpaper.
        /// </summary>
        /// <param name="imagePath">The path of the image.</param>
        public static void Set(string imagePath)
        {
            string tempPath = Path.Combine(Path.GetTempPath(), "wallpaper.bmp");

            using (var stream = new FileStream(imagePath, FileMode.Open))
            {
                var img = System.Drawing.Image.FromStream(stream);
                img.Save(tempPath, System.Drawing.Imaging.ImageFormat.Bmp);
            }

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"Control Panel\Desktop", true);
            key.SetValue(@"WallpaperStyle", 2.ToString());
            key.SetValue(@"TileWallpaper", 0.ToString());

            SystemParametersInfo(SPI_SETDESKWALLPAPER, 0, tempPath, SPIF_UPDATEINIFILE | SPIF_SENDWININICHANGE);
        }
    }
}

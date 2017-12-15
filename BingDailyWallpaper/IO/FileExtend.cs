using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.IO
{
    public static class FileExtend
    {
        /// <summary>
        /// Determines whether the specified file does not exists.
        /// </summary>
        /// <param name="path">The file to check</param>
        public static bool NotExists(string path) => !File.Exists(path);
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper
{
    public static class Bing
    {
        public const int MaxArchive = 8;

        public const string ArchiveUrl = "http://az517271.vo.msecnd.net/TodayImageService.svc/HPImageArchive?&mkt=en-ww&idx={0}";

        public const string ArchiveUrl2 = "https://www.bing.com/HPImageArchive.aspx?format=js&idx=0&n=8&mkt=en-US";

        public const string BaseImageUrl = "https://www.bing.com";
    }
}

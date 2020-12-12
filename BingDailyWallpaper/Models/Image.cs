using BingDailyWallpaper.Storage;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.IO;

namespace BingDailyWallpaper.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Image
    {
        [JsonProperty("startdate")]
        public string DateUnformatted { get; set; }

        [JsonProperty("url")]
        public string RelativeUrl { get; set; }

        [JsonProperty("copyright")]
        public string Copyright { get; set; }

        public DateTime Date => DateTime.ParseExact(DateUnformatted, "yyyyMMdd", CultureInfo.InvariantCulture);

        public string FileName => Path.Combine(Settings.Current.LocalDirectoryPath, Date.ToString("yyyy-MM-dd") + ".jpg");
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BingDailyWallpaper.Models
{
    [XmlRoot("image")]
    public class Image
    {
        [XmlElement("startdate")]
        public string DateUnformatted { get; set; }

        [XmlElement("fullImageUrl")]
        public string Url { get; set; }

        [XmlElement("copyright")]
        public string Copyright { get; set; }

        [XmlIgnore]
        public DateTime Date => DateTime.ParseExact(DateUnformatted, "yyyyMMdd", CultureInfo.InvariantCulture);

        [XmlIgnore]
        public string FileName { get; set; }
    }
}

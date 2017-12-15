using BingDailyWallpaper.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
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
        public string FileName => Path.Combine(Settings.Current.LocalDirectoryPath, Date.ToString("yyyy-MM-dd") + ".jpg");

        /// <summary>
        /// Return the deserialized <see cref="Image"/> from the specified XML string with the specified <see cref="XmlSerializer"/>.
        /// </summary>
        /// <param name="serializer">The serializer to use</param>
        /// <param name="xml">The serialized <see cref="Image"/></param>
        /// <returns></returns>
        public static Image Parse(XmlSerializer serializer, string xml)
        {
            try
            {
                using (var reader = new StringReader(xml))
                {
                    return serializer.Deserialize(reader) as Image;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Models
{
    public class BingResponse
    {
        [JsonProperty("images")]
        public List<Image> Images { get; set; }

        /// <summary>
        /// Return the deserialized <see cref="BingResponse"/> from the specified JSON string.
        /// </summary>
        /// <param name="json">The serialized <see cref="BingResponse"/></param>
        /// <returns></returns>
        public static BingResponse Parse(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<BingResponse>(json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}

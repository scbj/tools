using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper
{
    public sealed partial class ApplicationSettings
    {
        #region Private Constructors

        private ApplicationSettings()
        {
            // Do nothing
        }

        #endregion

        #region Properties

        public DateTime LastDownloadTime { get; set; }

        #endregion

        #region Public Methods

        /// <summary>
        /// Save the current instance of <see cref="ApplicationSettings"/>.
        /// </summary>
        public void Save()
        {
            string json = JsonConvert.SerializeObject(this);
            File.WriteAllText(_fileName, json);
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Initialize the <see cref="ApplicationSettings"/> singleton with default values. At the first start or if the backup file doesn't exist anymore.
        /// </summary>
        private void InitializeDefaultValues()
        {
            LastDownloadTime = new DateTime(1900, 1, 1);
        }

        #endregion
    }
}

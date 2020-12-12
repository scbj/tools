using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Storage
{
    public sealed partial class Settings
    {

        #region Fields
        private DateTime _lastDownloadTime;
        private string _lastDefinedWallpaper;
        private string _localDirectoryPath;

        #endregion

        #region Private Constructors

        private Settings()
        {
            // Do nothing
        }

        #endregion

        #region Properties

        public bool IsSetWallpaperEnabled { get; set; } = false;

        public DateTime LastDownloadTime
        {
            get => _lastDownloadTime == null ? DateTime.MinValue : _lastDownloadTime;
            set
            {
                if (value != null)
                    _lastDownloadTime = value;
            }
        }

        public string LastDefinedWallpaper
        {
            get => _lastDefinedWallpaper ?? String.Empty;
            set
            {
                if (value != null)
                    _lastDefinedWallpaper = value;
            }
        }

        public string LocalDirectoryPath
        {
            get => _localDirectoryPath ?? "images";
            set
            {
                if (value != null)
                    _localDirectoryPath = value;
            }
        }

        public bool NotificationEnabled { get; set; } = true;

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
    }
}

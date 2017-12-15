using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Storage
{
    public sealed partial class Settings
    {
        #region Fields

        private static Settings _instance;
        private static readonly string _fileName = "application.json";

        #endregion

        #region Properties

        public static Settings Current
        {
            get
            {
                if (_instance == null)
                    Load();

                return _instance;
            }
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Load the application settings from file if it exists or with defaults values.
        /// </summary>
        public static void Load()
        {
            CreateInstance();
        }

        #endregion

        #region Private Methods

        private static void CreateInstance()
        {
#if !DEBUG
            if (File.Exists(_fileName))
            {
                string json = File.ReadAllText(_fileName);

                if (!String.IsNullOrEmpty(json))
                {
                    _instance = JsonConvert.DeserializeObject<Settings>(json);
                    return;
                }
            }
#endif

            _instance = new Settings();
        }

        #endregion
    }
}

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BingDailyWallpaper.Settings
{
    public sealed partial class ApplicationSettings
    {
        #region Fields

        private static ApplicationSettings _instance;
        private static readonly string _fileName = "application.ini";

        #endregion

        #region Properties

        public static ApplicationSettings Default
        {
            get
            {
                if (_instance == null)
                    CreateInstance();

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
            if (File.Exists(_fileName))
            {
                string json = File.ReadAllText(_fileName);

                if (!String.IsNullOrEmpty(json))
                    _instance = JsonConvert.DeserializeObject<ApplicationSettings>(json);
            }
            else
            {
                _instance = new ApplicationSettings();
                _instance.InitializeDefaultValues();
            }
        }

        #endregion
    }
}

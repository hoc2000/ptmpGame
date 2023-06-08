using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WidogameFoundation.Config
{

    public class WidogameAppSettingsLoader
    {
        private static WidogameAppSettings appSettings;
        public static WidogameAppSettings AppSettings
        {
            get
            {
                if (appSettings == null)
                {
                    LoadAppSettings();
                }
                return appSettings;
            }
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSplashScreen)]
        static void Init()
        {
            LoadAppSettings();

        }

        private static void LoadAppSettings()
        {
            appSettings = Resources.Load<WidogameAppSettings>(WidogameConstants.WIDOGAME_APP_SETTINGS_NAME);
        }

    }
}

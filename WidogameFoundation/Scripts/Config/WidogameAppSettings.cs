using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace WidogameFoundation.Config
{
    public class WidogameAppSettings : ScriptableObject
    {
        public static readonly string WIDOGAME_CONFIGURATION_ASSET_PATH = Path.Combine(WidogameConstants.WIDOGAME_RESOURCES_PATH, WidogameConstants.WIDOGAME_APP_SETTINGS_NAME + ".asset");

        [Header("Admob")]
        public string AndroidAdmobAppOpenId;
        public string AndroidAdmobBannerId;
        public string AndroidAdmobInterstitialId;
        public string AndroidAdmobRewardedId;
        public string AndroidAdmobRewardedInterstitialId;
        public string IOSAdmobAppOpenId;
        public string IOSAdmobBannerId;
        public string IOSAdmobInterstitialId;
        public string IOSAdmobRewardedId;
        public string IOSAdmobRewardedInterstitialId;

        [Header("Max Mediation")]
        public string MaxSdkKey;
        public string AndroidMaxBannerAdUnit;
        public string AndroidMaxInterstitialAdUnit;
        public string AndroidMaxRewardedAdUnit;
        public string IOSMaxBannerAdUnit;
        public string IOSMaxInterstitialAdUnit;
        public string IOSMaxRewardedAdUnit;

        [Header("IronSource Mediation")]
        public string AndroidIronSourceDevKey;
        public string IOSIronSourceDevKey;

    }
}

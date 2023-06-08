//using com.adjust.sdk;
//using Firebase.Analytics;
//using KevinIglesias;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.Accessibility;

//public class TrackingManager : SingletonMonoBehaviour<TrackingManager>
//{
//    static RewardID rewardIDIP;
//    private void Start()
//    {
//        EnableBackgroundTracking();
//    }
//    void EnableBackgroundTracking()
//    {
//        AdjustConfig adjustConfig = new AdjustConfig("sq77d28wkc1s", AdjustEnvironment.Production);
//        adjustConfig.setSendInBackground(true);
//        Adjust.start(adjustConfig);
//    }
//    public void LogEvent(string eventName, string paramName, string paramValue)
//    {
//        FirebaseAnalytics.LogEvent(eventName, paramName, paramValue);
//        Debug.Log("Log Event: " + eventName + "- ParamName: " + paramName + "- ParamValue : " + paramValue);
//    }

//    public void LevelTracking(LevelType levelType, int param, Dictionary<string, object> additionalParams = null)
//    {

//        var dic = new Dictionary<string, object>();
//        dic["level"] = param;
//        if (additionalParams != null)
//            foreach (var pair in additionalParams)
//                dic[pair.Key] = pair.Value;

//        AdjustEvent adjustEvent;
//        switch (levelType)
//        {
//            case LevelType.Start:
//                FirebaseAnalytics.LogEvent("level_start", "level", param);
//                adjustEvent = new AdjustEvent("n7oejl");
//                Adjust.trackEvent(adjustEvent);
//                Debug.Log("level_start" + " " + param);
//                Debug.Log(adjustEvent);
//                break;
//            case LevelType.Passed:
//                FirebaseAnalytics.LogEvent("level_passed", "level", param);

//                adjustEvent = new AdjustEvent("2qkqb5");
//                Adjust.trackEvent(adjustEvent);

//                Debug.Log("level_passed" + " " + param);
//                Debug.Log(adjustEvent);
//                break;
//            case LevelType.Failed:
//                FirebaseAnalytics.LogEvent("level_failed", "level", param);

//                adjustEvent = new AdjustEvent("tpaces");
//                Adjust.trackEvent(adjustEvent);

//                Debug.Log("level_failed" + "level_" + param);
//                Debug.Log(adjustEvent);
//                break;
//        }
//    }


//    public void LogEvent(string eventName)
//    {
//        FirebaseAnalytics.LogEvent(eventName);
//        Debug.Log("Firebase log event: " + eventName);
//    }
//    public void TrackingAds(AdsType adsType, bool has_ads, bool hasInterne, RewardID placement)
//    {
//        Parameter[] param = {
//        new Parameter("has_ads", has_ads.ToString()),
//        new Parameter("internet_available", hasInterne.ToString()),
//        new Parameter("placement", placement.ToString()),
//        };
//        switch (adsType)
//        {
//            case AdsType.Inter:
//                FirebaseAnalytics.LogEvent("show_interstitial_ads", param);
//                Debug.Log("show_interstitial_ads : " + "has_ads " + has_ads + " : internet_available " + hasInterne + " : placement " + placement.ToString());
//                break;
//            case AdsType.Reward:
//                FirebaseAnalytics.LogEvent("show_rewarded_ads", param);
//                Debug.Log("show_rewarded_ads : " + "has_ads " + has_ads + " : internet_available " + hasInterne + " : placement " + placement.ToString());
//                break;
//        }
//    }

//    public void TrackInappPurchases(decimal revenue, string cent)
//    {
//        AdjustEvent adjustEvent = new AdjustEvent("g6bgyb");
//        adjustEvent.setRevenue((double)revenue, cent);
//        Adjust.trackEvent(adjustEvent);
//    }
//}
//public enum RewardID
//{
//    X2Reward,

//    GetSkinInShop,
//    GetSkinInNewSkin,

//    ClaimDailyX2,
//    GetCardGift,
//    GetCoinChestInGame,
//    GetLifeChestInGame,
//    GetCoinInHome,
//    GetLifeInHome,
//    DoubleCoinInHome,
//    SkipLevel,

//    Revivel,
//    Spin,

//    GetCoinInSkinPopup,

//    TrySkinInGame,
//    TrySkinInSkinPopup,
//    GetSkinInSkinPopup,

//    WatchAdsGift,
//    CoinFreeInShop,
//    X2StarInWin,

//    GetRewardInGiftBoxPopup,
//    GetLottery,
//    UseBoomInGame,
//    UseRemoveInGame,
//    UseRainInGame,
//    UseTrajectionInGame,

//    GetStarInShop,
//    GetBoomInShop,
//    GetRemoveInShop,
//    GetRainInShop,
//    GetTrajectionInShop,
//    GifBoxFlyInGame,
//    GifBoxFlyInPopup,

//    GetStarInSkin,

//    EndLevel
//}
//public enum ChannelID
//{
//    RevivePopup,
//    Home,
//    Game,
//    PausePopup,
//    SkinPopup,
//    GetSkinPopup,
//    DailyRewardPopup,
//    ShopPopup,
//    GiftPopup,
//    LotteryPopup,
//    TrySkinPopup,
//    WinPopup

//}
//public enum ButtonID
//{
//    SpecialPack,
//    GetSkinInNewSkin,
//    PlayGame,
//    TapToPlay,
//    SelectSkin,
//    ClosePopup,
//    RequestRandom,
//    StartRandom,
//    BuyPremium,
//    X2Bonus,
//    Revive,
//    SkipInGame,
//    SkipInHome,
//    LiveInHome,
//    CoinInHome,
//    CoinInRequest,
//    GetSkinInShop,
//    LiveInGame,
//    MagnetGame,
//    TrySkinInHome,
//    TrySkinInShop,
//    GetSkinInHome,
//    ClaimSkinDaily,
//    SkinButton,
//    ShopButton,
//    BuyRemoveAdsAndCoins,
//    BuyRemoveAdsAndHeart,
//    BuySubsciption,
//    BuyRemoveAds,
//    BuySpecial,
//    ClaimDaily,
//    ClaimDailyX2,
//    ClaimDailySkin,
//    DenyRate,
//    YesRate,
//    PauseGame,
//    GoHome,
//    Replay,
//    CloseNewSkinPopup,
//    MagnetInHome,
//    SpinIcon,
//    StartSpin,
//    X2SpinReward,
//    GetSkinSpin,
//    DenyRewardSpin,
//    GetSkinProgres,
//    IncreaseAds,
//    PersonalSkin,
//    OpenBoxLottery,
//    StickClash,
//    FightStickClash,
//    UpgradeWithCoin,
//    UpgradeWithAds,
//    GetRewardInWinPopup,
//    X2RewardInFailedPopup,
//    SelectLevel,
//    OpenPopup,
//    x2CoinWin
//}
//public enum RewardType
//{
//    Request,
//    Impression,
//    Completed,
//    Cancel
//}
//public enum AdsType
//{
//    Inter,
//    Reward
//}
//public enum TrackingCoinType
//{
//    CoinEarnTracking,
//    CoinSpendTracking
//}
//public enum LevelType
//{
//    Start,
//    Passed,
//    Failed
//}



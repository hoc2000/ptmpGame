//using AppsFlyerSDK;
//using Facebook.Unity;
using com.adjust.sdk;
using Firebase.Analytics;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public class GameAnalytics {

    public static bool isFBInitialized;
    public static bool isFirebaseInitialized;
#if UNITY_ANDROID
    public const string ADJUST_ET_IN_APP_PURCHASE = "c78n6d";
    public const string ADJUST_ET_LEVEL_START = "1eoz2s";
    public const string ADJUST_ET_LEVEL_PASSED = "hc7oiy";
    public const string ADJUST_ET_LEVEL_FAILED = "wveg1c";

#else
    public const string ADJUST_ET_IN_APP_PURCHASE = "9efbo6";
    public const string ADJUST_ET_LEVEL_START = "d4uv26";
    public const string ADJUST_ET_LEVEL_PASSED = "j3ipgc";
    public const string ADJUST_ET_LEVEL_FAILED = "tjvw75";
#endif
    #region OldCode
    public static void LogEventAppsFlyer(string eventName, Dictionary<string, string> parameters) {
        //AppsFlyer.sendEvent(eventName, parameters);
    }
    public static void LogEventFirebase(string eventName, Parameter[] parameters) {
        if (isFirebaseInitialized) {
#if !ENV_PROD
            Array.Resize<Parameter>(ref parameters, parameters.Length + 1);
            parameters[parameters.Length - 1] = new Parameter("test", true.ToString());
#endif
            FirebaseAnalytics.LogEvent(eventName, parameters);
        }
    }

    public static void LogEventFacebook(string eventName, Dictionary<string, object> parameters) {
        if (isFBInitialized) {
#if !ENV_PROD
            parameters["test"] = true;
#endif
            //FB.LogAppEvent(eventName, null, parameters);
        }
    }

    public static void LogAttRequest(bool approved) {
        Parameter[] firebaseParams = new Parameter[1];
        firebaseParams[0] = new Parameter("approved", approved.ToString());
        LogEventFirebase("att_request", firebaseParams);
    }
 
    public static void LogLoadingFinished() {
        Parameter[] firebaseParams = new Parameter[1];
        firebaseParams[0] = new Parameter("finish", "true");
        LogEventFirebase("loading_finish", firebaseParams);
    }
    public static void LogFirebaseUserProperty(string userProperty, object value) {
#if ENV_PROD
        if (isFirebaseInitialized) {
            FirebaseAnalytics.SetUserProperty(userProperty, value.ToString());
        }
#endif
    }
    public static void LogInAppPurchase(Product product)
    {
        AdjustEvent adjustEvent = new AdjustEvent(ADJUST_ET_IN_APP_PURCHASE);
        adjustEvent.setRevenue((double)product.metadata.localizedPrice, product.metadata.isoCurrencyCode);
        adjustEvent.setTransactionId(product.transactionID);
        Adjust.trackEvent(adjustEvent);
    }
    public static void LogTutorialBegin() {
        Parameter[] firebaseParams = new Parameter[1];
        firebaseParams[0] = new Parameter("tutorial_begin", "tutorial_begin");
        LogEventFirebase("tutorial_begin", firebaseParams);
    }
    public static void LogTutorialComplete() {
        Parameter[] firebaseParams = new Parameter[1];
        firebaseParams[0] = new Parameter("tutorial_complete", "tutorial_complete");
        LogEventFirebase("tutorial_complete", firebaseParams);
    }


    //public static void LogBuyIAP(string product)
    //{
    //    Parameter[] firebaseParams = new Parameter[2];
    //    var fbParams = new Dictionary<string, object>();
    //    firebaseParams[0] = new Parameter("screen_name", GameData.buyIAPFrom);
    //    firebaseParams[1] = new Parameter("product_id", product);
    //    LogEventFirebase("iap_purchase", firebaseParams);
    //}
    #endregion

    public static void LogLevelStart(int level, string from, string skin, bool isRestart, int totalEnemy) {
        Parameter[] firebaseParams = new Parameter[5];
        firebaseParams[0] = new Parameter("level", level);
        firebaseParams[1] = new Parameter("from", from);
        firebaseParams[2] = new Parameter("skin", skin);
        firebaseParams[3] = new Parameter("is_restart", isRestart.ToString());
        firebaseParams[4] = new Parameter("total_enemy", totalEnemy);
        LogFirebaseUserProperty("last_level", level);
        LogEventFirebase("level_start", firebaseParams);
        //GameData.LevelStartCount++;
        AdjustEvent e = new AdjustEvent(ADJUST_ET_LEVEL_START);
        Adjust.trackEvent(e);
    }
    public static void LogLevelEnd(int level, bool pass, float timeSpent, float duration, string character, string from, bool isRestart, bool getWeapon, string weaponName, int totalEnemy, int remainEnemy, int healthPercentRemain, bool eatRecoveryItem) {
        Parameter[] firebaseParams = new Parameter[13];
        firebaseParams[0] = new Parameter("level", level);
        firebaseParams[1] = new Parameter("pass", pass.ToString());
        firebaseParams[2] = new Parameter("time_spent", timeSpent);
        firebaseParams[3] = new Parameter("duration", duration);
        firebaseParams[4] = new Parameter("skin", character);
        firebaseParams[5] = new Parameter("from", from);
        firebaseParams[6] = new Parameter("isRestart", isRestart.ToString());
        firebaseParams[7] = new Parameter("get_weapon", getWeapon.ToString());
        firebaseParams[8] = new Parameter("name_weapon", weaponName);
        firebaseParams[9] = new Parameter("total_enemy", totalEnemy);
        firebaseParams[10] = new Parameter("remain_enemy", remainEnemy);
        firebaseParams[11] = new Parameter("percent_remain_health", healthPercentRemain);
        firebaseParams[12] = new Parameter("eat_recovery_item", eatRecoveryItem.ToString());
        LogEventFirebase("level_end", firebaseParams);
        AdjustEvent e = new AdjustEvent(pass ? ADJUST_ET_LEVEL_PASSED : ADJUST_ET_LEVEL_FAILED);
        Adjust.trackEvent(e);
    }
    public static void LogRevive(int level, string method, bool getWeapon, string weaponName) {
        Parameter[] firebaseParams = new Parameter[4];
        firebaseParams[0] = new Parameter("level", level);
        firebaseParams[1] = new Parameter("method", method);
        firebaseParams[2] = new Parameter("get_weapon", getWeapon.ToString());
        firebaseParams[3] = new Parameter("name_weapon", weaponName);
        LogEventFirebase("revive", firebaseParams);
    }
    public static void LogPlayerDie(int level, int xPos, int yPos, int totalEnemy, int remainEnemy, bool getWeapon, string weaponName) {
        Parameter[] firebaseParams = new Parameter[7];
        firebaseParams[0] = new Parameter("level", level);
        firebaseParams[1] = new Parameter("x_position", xPos);
        firebaseParams[2] = new Parameter("y_position", yPos);
        firebaseParams[3] = new Parameter("total_enemy", totalEnemy);
        firebaseParams[4] = new Parameter("remain_enemy", remainEnemy);
        firebaseParams[5] = new Parameter("get_weapon", getWeapon.ToString());
        firebaseParams[6] = new Parameter("name_weapon", weaponName);
        LogEventFirebase("player_die", firebaseParams);
    }
    //public static void LogBuyIap(Product product, string screenName)
    //{
    //    //View IAPPurchaseHandler and VipController for reference
    //    Parameter[] firebaseParams = new Parameter[2];
    //    firebaseParams[0] = new Parameter("product", product.ToString());
    //    firebaseParams[1] = new Parameter("screen_name", screenName);
    //    LogEventFirebase("buy_iap", firebaseParams);
    //}
    public static void LogWatchRewardAds(string scenarios, bool hasVideo) {
        bool internetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        Parameter[] firebaseParams = new Parameter[3];
        firebaseParams[0] = new Parameter("scenarios", scenarios);
        firebaseParams[1] = new Parameter("has_video", hasVideo.ToString());
        firebaseParams[2] = new Parameter("internet_available", internetAvailable.ToString());
        if (hasVideo)
        {
            LogFirebaseUserProperty("last_scenarios", scenarios);
        }
        LogEventFirebase("watch_reward_ads", firebaseParams);       
    }
    public static void LogWatchRewardAdsDone(string scenarios, bool hasVideo, string result)
    {
        bool internetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        Parameter[] firebaseParams = new Parameter[4];
        firebaseParams[0] = new Parameter("scenarios", scenarios);
        firebaseParams[1] = new Parameter("has_video", hasVideo.ToString());
        firebaseParams[2] = new Parameter("complete", result);
        firebaseParams[3] = new Parameter("internet_available", internetAvailable.ToString());
        if (hasVideo && result == "true")
        {
            LogFirebaseUserProperty("last_scenarios", scenarios);
        }
        LogEventFirebase("watch_reward_ads_done", firebaseParams);      
    }
    public static void LogInterstitialAdsImpression(string scenarios, bool hasVideo, bool shouldShow) {
        bool internetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        Parameter[] firebaseParams = new Parameter[4];
        firebaseParams[0] = new Parameter("scenarios", scenarios);
        firebaseParams[1] = new Parameter("has_video", hasVideo.ToString());
        firebaseParams[2] = new Parameter("internet_available", internetAvailable.ToString());
        firebaseParams[3] = new Parameter("should_show", shouldShow.ToString());
        if (hasVideo)
        {
            LogFirebaseUserProperty("last_scenarios", scenarios);
        }
        LogEventFirebase("interstitial_ads_impression", firebaseParams);
    }
    public static void LogInterstitialAdsImpressionDone(string scenarios, bool hasVideo, bool result)
    {
        bool internetAvailable = Application.internetReachability != NetworkReachability.NotReachable;
        Parameter[] firebaseParams = new Parameter[4];
        firebaseParams[0] = new Parameter("scenarios", scenarios);
        firebaseParams[1] = new Parameter("has_video", hasVideo.ToString());
        firebaseParams[2] = new Parameter("done", result.ToString());
        firebaseParams[3] = new Parameter("internet_available", internetAvailable.ToString());
        if (hasVideo && result)
        {
            LogFirebaseUserProperty("last_scenarios", scenarios);
        }
        LogEventFirebase("interstitial_ads_impression_done", firebaseParams);
    }
    public static void AppOpenAdsImpression(string scenarios, bool hasVideo, bool internet)
    {
        Parameter[] firebaseParams = new Parameter[3];
        firebaseParams[0] = new Parameter("scenarios", scenarios);
        firebaseParams[1] = new Parameter("has_video", hasVideo.ToString());
        firebaseParams[2] = new Parameter("internet_available", internet.ToString());
        //firebaseParams[3] = new Parameter("network", AdsController.Instance.lastLoadedInterstitialNetwork);
        LogEventFirebase("app_open_ads_impression", firebaseParams);
    }
    public static void LogButtonClick(string name, string screenName) {
        Parameter[] firebaseParams = new Parameter[2];
        firebaseParams[0] = new Parameter("name", name);
        firebaseParams[1] = new Parameter("screen_name", screenName);
        LogEventFirebase("button_click", firebaseParams);
    }
    public static void LogUIAppear(string name, string screenName) {
        Parameter[] firebaseParams = new Parameter[2];
        firebaseParams[0] = new Parameter("name", name);
        firebaseParams[1] = new Parameter("screen_name", screenName);
        LogEventFirebase("ui_appear", firebaseParams);
    }
    public static void LogBreakChest(int level)
    {
        Parameter[] firebaseParams = new Parameter[1];
        firebaseParams[0] = new Parameter("level", level);
        LogEventFirebase("break_chest", firebaseParams);
    }
    public static void LogReceiveItemPopUpStartLevel(int level, string itemName, int quantity)
    {
        Parameter[] firebaseParams = new Parameter[3];
        firebaseParams[0] = new Parameter("level", level);
        firebaseParams[1] = new Parameter("item_name", itemName);
        firebaseParams[2] = new Parameter("quantity", quantity);
        LogEventFirebase("receive_item_popup_start_level", firebaseParams);
    }
    public static void LogBuyInShop(string tabName, string itemName, string wayToGet, int watchedAds, int totalAds, string placement, int price)
    {
        Parameter[] firebaseParams = new Parameter[7];
        firebaseParams[0] = new Parameter("tab_shop", tabName);
        firebaseParams[1] = new Parameter("item_name", itemName);
        firebaseParams[2] = new Parameter("currency", wayToGet);
        firebaseParams[3] = new Parameter("number_of_watched_ads", watchedAds);
        firebaseParams[4] = new Parameter("total_ads_to_get_item", totalAds);
        firebaseParams[5] = new Parameter("from", placement);
        firebaseParams[6] = new Parameter("price", price);
        LogEventFirebase("buy_in_shop", firebaseParams);
    }

    public static void LogItemTab(string itemName, string method, int level)
    {
        Parameter[] firebaseParams = new Parameter[3];
        firebaseParams[0] = new Parameter("item_name", itemName);
        firebaseParams[1] = new Parameter("method", method);
        firebaseParams[2] = new Parameter("level", level);
        LogEventFirebase("tab_item", firebaseParams);
    }

    public static void LogGetItem(string itemName, int amount, string method, int watchedAds, int totalAds, string screenName, string placement)
    {
        Parameter[] firebaseParams = new Parameter[7];
        firebaseParams[0] = new Parameter("item_name", itemName);
        firebaseParams[1] = new Parameter("amount", amount);
        firebaseParams[2] = new Parameter("method", method);
        firebaseParams[3] = new Parameter("num_of_ads", watchedAds);
        firebaseParams[4] = new Parameter("num_of_total_ads", totalAds);
        firebaseParams[5] = new Parameter("screen_name", screenName);
        firebaseParams[6] = new Parameter("placement", placement);
        LogEventFirebase("get_item", firebaseParams);
    }

    public static void LogUseItem(string itemName, int amount, string method, string screenName, string placement)
    {
        Parameter[] firebaseParams = new Parameter[5];
        firebaseParams[0] = new Parameter("item_name", itemName);
        firebaseParams[1] = new Parameter("amount", amount);
        firebaseParams[2] = new Parameter("method", method);
        firebaseParams[3] = new Parameter("screen_name", screenName);
        firebaseParams[4] = new Parameter("placement", placement);
        LogEventFirebase("use_item", firebaseParams);
    }

    public static void LogUpgradePowerLevel(int powerLevel, string method, int amount, string placement)
    {
        Parameter[] firebaseParams = new Parameter[4];
        firebaseParams[0] = new Parameter("level_power", powerLevel);
        firebaseParams[1] = new Parameter("method", method);
        firebaseParams[2] = new Parameter("amount", amount);
        firebaseParams[3] = new Parameter("placement", placement);
        LogEventFirebase("upgrade_level_power", firebaseParams);
    }
}

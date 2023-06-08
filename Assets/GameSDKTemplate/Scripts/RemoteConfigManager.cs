using Firebase.RemoteConfig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class RemoteConfigManager
{
    public static bool remoteConfigActivated;
    public static void InitializeRemoteConfig()
    {
        Dictionary<string, object> defaults = new Dictionary<string, object>();
        defaults = new Dictionary<string, object>();
        defaults.Add(StringConstants.RC_INTERSTITIAL_INTERVAL_SECONDS, 60);
        defaults.Add(StringConstants.RC_INTERSTITIAL_INTERVAL_INGAME_SECONDS, 120);
        defaults.Add(StringConstants.RC_INTERSTITIAL_FROM_STARTUP_SECONDS, 45);
        defaults.Add(StringConstants.RC_SUGGEST_VIP_AFTER_SLEEP_SCEONDS, 300);
        defaults.Add(StringConstants.RC_GAME_DATA, string.Empty);
        defaults.Add(StringConstants.RC_NOTI_SECONDS_REFILL_BOMB, 120);
        defaults.Add(StringConstants.RC_INTER_REWARDED_INTERVAL_ENABLED, false);
        defaults.Add(StringConstants.RC_DISABLE_INTERSTITIAL_ON_LOW_RAM_DEVICE, true);
        defaults.Add(StringConstants.RC_SECONDS_SLEEP_SUGGEST_IAP, 120);
        defaults.Add(StringConstants.RC_SHOULD_ASK_ATT, true);
        defaults.Add(StringConstants.RC_SECONDS_SLEEP_SHOW_ADS, 60);
        defaults.Add(StringConstants.RC_NUMBER_SUGGEST_JOIN_FB_IN_DAY, 2);
        defaults.Add(StringConstants.RC_FIRST_TIME_SUGGEST_ITEM, 10);
        defaults.Add(StringConstants.RC_DURATION_LEVEL_SHOW_STARTER_PACK, 2);
        defaults.Add(StringConstants.RC_DURATION_SLEEP_GAME, 10);
        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWith((result) =>
        {
            FirebaseRemoteConfig.DefaultInstance.FetchAndActivateAsync().ContinueWith((subResult) =>
            {
                remoteConfigActivated = true;
            });
        });
    }
    public static long GetLong(string key)
    {
        return FirebaseRemoteConfig.DefaultInstance.GetValue(key).LongValue;
    }

    public static bool GetBool(string key)
    {
        return FirebaseRemoteConfig.DefaultInstance.GetValue(key).BooleanValue;
    }

    public static double GetDouble(string key)
    {
        return FirebaseRemoteConfig.DefaultInstance.GetValue(key).DoubleValue;
    }

    public static string GetString(string key)
    {
        return FirebaseRemoteConfig.DefaultInstance.GetValue(key).StringValue;
    }

}
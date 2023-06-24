using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AppInform
{
    public bool IsTest;
    public bool IsEnableDebug;
    public int versionCode;
    [TextArea]
    public string whatNewVersion;
    public string BundleId;
    //[Tooltip("Link more game")] public string StoreLink;

    //[Space(10)] public bool IsUseBanner;
    //public bool IsUseInterstitial;
    //public bool IsUseReward;
    //public string AppName;
    //public string AppId;



    //[Space(10)] public string BannerId;
    //public string InterstitialId;
    //public string RewardId;


    //[Space(10)] public int offsetBanner = 6;

    // [Space(10)] public string UnityId;
    [Space(10)] public int InterstitialInterval = 45;
    [Space(10)] public int TimeLoading = 2;
    public int NumTryLoad = 0;

    //[Space(10)] public List<string> TestDevices = new List<string>();
    //public List<string> KeyWords = new List<string>();
    //[Space(10)] public MaxAdContentRating MaxAdContentRating;
}
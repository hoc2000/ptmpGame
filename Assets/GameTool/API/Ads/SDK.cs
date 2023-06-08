//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using Firebase.Extensions;
//using Firebase.RemoteConfig;
//using Google.Play.Review;
//using UnityEngine;

//public class SDK : SingletonMonoBehaviour<SDK>
//{
//    [SerializeField]
//    GameObject loadingObj;

//    public string Prefix;

//    [SerializeField]
//    int androidVersionCode, iosVersionCode;

//    public static bool IsShowingAdsReturn;
//    public static bool IsBannerShowing = true;
//    public static Action OnFirebaseInited;
//    public static Action OnFetchCompleted;
//    public static Action<Dictionary<string, object>> OnSetDefault;




//    public static bool IsRemoveAds
//    {
//        get { return PlayerPrefs.GetInt("IsRemoveAds", 0) == 1; }
//        set { PlayerPrefs.SetInt("IsRemoveAds", value ? 1 : 0); }
//    }

//    [Header("API SETTINGS")]
//    public bool IsUseRemoteData = true;

//    public AppInform AndroidAppInfor;
//    public AppInform IosAppInfor;

//    public IronSourceManager ironSource;

//    [HideInInspector] public AppInform appInform;

//    [HideInInspector] public bool IsFirebaseInitCompleted;
//    [HideInInspector] public bool IsFirebaseInitSuccess;
//    [HideInInspector] public bool IsFirebaseFetchCompleted;



//    private Action<bool> _onRewardSuccess;

//    public Action InterstitialLoaded, InterstitialAdLoadFailed;

//    private ReviewManager _reviewManager;

//    private void Start()
//    {
//        loadingObj.SetActive(false);
//        _reviewManager = new ReviewManager();
//        InitFirebase();
//        //StartCoroutine(DelayInitAPI());
//        StartCoroutine(WaitFetchCompleted());
//    }
//    //private void OnApplicationPause(bool pauseStatus)
//    //{
//    //    if (!pauseStatus)
//    //    {
//    //        if (IsShowingAdsReturn)
//    //        {
//    //            IsShowingAdsReturn = false;
//    //            return;
//    //        }
//    //        ShowFull(null);
//    //    }
//    //}


//    //private IEnumerator DelayInitAPI()
//    //{
//    //    yield return new WaitUntil(() => IsFirebaseInitCompleted);
//    //    if (OnFirebaseInited != null)
//    //        OnFirebaseInited();



//    //}

//    private IEnumerator WaitFetchCompleted()
//    {
//        yield return new WaitUntil(() => IsFirebaseFetchCompleted);
//        if (OnFetchCompleted != null)
//            OnFetchCompleted();
//        InitAPI();
//    }

//    private void InitFirebase()
//    {
//        Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
//        {

//            var dependencyStatus = task.Result;
//            if (dependencyStatus == Firebase.DependencyStatus.Available)
//            {
//                OnFirebaseInitCompleted();
//                IsFirebaseInitCompleted = true;
//                IsFirebaseInitSuccess = true;
//            }
//            else
//            {
//                UnityEngine.Debug.LogError(System.String.Format(
//      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
//                // Firebase Unity SDK is not safe to use here.
//                IsFirebaseInitCompleted = true;
//                IsFirebaseInitSuccess = false;
//            }
//        });
//    }

//    private void OnFirebaseInitCompleted()
//    {
//        InitDefault();
//        if (IsUseRemoteData)
//            FetchDataAsync();
//    }

//    public Task FetchDataAsync()
//    {
//        Debug.Log("Fetching data...");
//        System.Threading.Tasks.Task fetchTask =
//        Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
//            TimeSpan.Zero);
//        return fetchTask.ContinueWithOnMainThread(FetchComplete);
//    }

//    void FetchComplete(Task fetchTask)
//    {
//        if (fetchTask.IsCanceled)
//        {
//            ApiDebug("Fetch canceled.");
//        }
//        else if (fetchTask.IsFaulted)
//        {
//            ApiDebug("Fetch encountered an error.");
//        }
//        else if (fetchTask.IsCompleted)
//        {
//            ApiDebug("Fetch completed successfully!");
//        }
//        var info = FirebaseRemoteConfig.DefaultInstance.Info;

//        switch (info.LastFetchStatus)
//        {
//            case LastFetchStatus.Success:
//                FirebaseRemoteConfig.DefaultInstance.ActivateAsync();
//                ApiDebug(string.Format("Remote data loaded and ready (last fetch time {0}).", info.FetchTime));
//                break;
//            case LastFetchStatus.Failure:
//                switch (info.LastFetchFailureReason)
//                {
//                    case FetchFailureReason.Error:
//                        ApiDebug("Fetch failed for unknown reason");
//                        break;
//                    case FetchFailureReason.Throttled:
//                        ApiDebug("Fetch throttled until " + info.ThrottledEndTime);
//                        break;
//                }

//                break;
//            case LastFetchStatus.Pending:
//                ApiDebug("Latest Fetch call still pending.");
//                break;
//        }

//        IsFirebaseFetchCompleted = true;
//    }

//    public string GetKey()
//    {
//        string key = Prefix + "appInfor";
//        if (IsAndroid())
//            key += "_android";
//        else if (IsIOS())
//            key += "_ios";
//        return key;
//    }

//    private void InitAPI()
//    {
//        string key = GetKey();
//        string data = FirebaseRemoteConfig.DefaultInstance.GetValue(GetKey()).StringValue;
//        data = data.CorrectString();
//        ApiDebug(string.Format("FirebaseRemote - key {0}\n{1}", key, data));
//        appInform = JsonUtility.FromJson<AppInform>(data);
//        Debug.unityLogger.logEnabled = appInform.IsEnableDebug;
//        InitAds();
//    }

//    //    public bool CheckInfoApp()
//    //    {
//    //#if UNITY_ANDROID
//    //        if (appInform.BundleId != Application.identifier)
//    //        {
//    //            Dialog.Instance.OpenDialog("OUT OF DATE", "This version of game is not support. Please download new version", "DOWNLOAD", () =>
//    //            {
//    //                GotoGame();
//    //            });
//    //            return false;
//    //        }
//    //#endif
//    //        int versionCode = IsAndroid() ? androidVersionCode : iosVersionCode;
//    //        if (appInform.versionCode > versionCode)
//    //        {

//    //            Dialog.Instance.OpenDialog("UPDATE", appInform.whatNewVersion, "NO", "YES", () =>
//    //            {
//    //                GotoGame();
//    //            });
//    //            return true;
//    //        }

//    //        return true;
//    //    }

//    private void InitDefault()
//    {

//        Dictionary<string, object> defaults = new Dictionary<string, object>();
//        if (IsAndroid())
//        {
//            defaults.Add(GetKey(), JsonUtility.ToJson(AndroidAppInfor));
//        }
//        else if (IsIOS())
//        {
//            defaults.Add(GetKey(), JsonUtility.ToJson(IosAppInfor));
//        }

//        //if (OnSetDefault != null)
//        //{
//        //    OnSetDefault(defaults);
//        //}
//        //FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);

//        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
//        .ContinueWithOnMainThread(task =>
//        {
//        });
//    }

//    private void InitAds()
//    {
//        ironSource = new IronSourceManager();
//        ironSource.InitIron(appInform);
//    }

//    public void ShowBanner()
//    {
//        //if (IsRemoveAds)
//        //{
//        return;
//        //}
//        //IsBannerShowing = true;
//        //if (ironSource != null)
//        //    ironSource.ShowBanner();
//    }

//    public void HideBanner()
//    {
//        //IsBannerShowing = false;
//        //if (ironSource != null)
//        //    ironSource.HideBanner();
//    }

//    //private ReviewManager _reviewManager;
//    //_reviewManager = new ReviewManager();
//    public void ShowRate()
//    {
//        var reviewManager = new ReviewManager();

//        var playReviewInfoAsyncOperation = reviewManager.RequestReviewFlow();

//        playReviewInfoAsyncOperation.Completed += playReviewInfoAsync =>
//        {
//            if (playReviewInfoAsync.Error == ReviewErrorCode.NoError)
//            {
//                var playReviewInfo = playReviewInfoAsync.GetResult();
//                reviewManager.LaunchReviewFlow(playReviewInfo);
//            }
//            else
//            {

//            }
//        };

//    }
//    public void ShowInter(Action<bool> onClosed)
//    {
//        if (Gamedata.I.CurrentLevel <= GameConfig.Instance.valueConfrig.levelShowInter)
//        {
//            if (onClosed != null)
//                onClosed(false);
//        }
//        if (IsRemoveAds)
//        {
//            if (onClosed != null)
//                onClosed(false);
//        }

//        if (IsEditor())
//        {
//            if (onClosed != null)
//                onClosed(false);
//        }
//        else
//        {
//            if (ironSource != null)
//            {
//                ironSource.ShowInterstitial(onClosed);

//            }
//            else if (onClosed != null)
//                onClosed(false);
//        }
//    }

//    public void ShowLoading(Action onClosed)
//    {
//        StartCoroutine(OnShowLoading(onClosed));
//    }

//    IEnumerator OnShowLoading(Action onClosed)
//    {
//        loadingObj.SetActive(true);
//        yield return new WaitForSecondsRealtime(appInform.TimeLoading);
//        loadingObj.SetActive(false);
//        if (onClosed != null)
//            onClosed();
//    }

//    int showGiftCount = 0;
//    private PlayReviewInfo _playReviewInfo;

//    public bool IsRewardLoaded()
//    {
//        if (IsEditor())
//        {
//            return true;
//        }
//        else
//        {
//            if (ironSource != null)
//            {
//                if (ironSource.IsRewardLoaded())
//                    return true;
//            }
//        }

//        return false;
//    }

//    public void ShowReward(Action<bool> callBack, RewardID value)
//    {
//        _onRewardSuccess = callBack;
//        if (IsEditor())
//        {
//            CallbackRewardVideo(true);
//        }
//        else
//        {
//            if (ironSource != null)
//            {
//                if (ironSource.IsRewardLoaded())
//                {
//                    loadingObj.SetActive(true);
//                    ironSource.ShowReward(CallbackRewardVideo, value);
//                }
//                else
//                {
//                    _onRewardSuccess(false);
//                    //ToastMgr.Instance.Show("Video Ads are unavailable at the moment");

//                }
//            }
//        }
//    }

//    void CallbackRewardVideo(bool Success)
//    {
//        loadingObj.SetActive(false);
//        if (Success)
//        {
//            _onRewardSuccess(true);
//        }
//        else
//        {
//            _onRewardSuccess(false);

//        }
//    }
//    //public void GotoGame()
//    //{
//    //    IsShowingAdsReturn = true;
//    //    string link = string.Empty;

//    //    if (IsAndroid())
//    //    {
//    //        link = ApiInfor.BundleId;
//    //        if (!link.Contains("https://play.google.com/store/apps/details?id="))
//    //            link = "https://play.google.com/store/apps/details?id=" + link;
//    //    }
//    //    else if (IsIOS())
//    //        link = ApiInfor.BundleId;

//    //    Application.OpenURL(link);
//    //}



//    [ContextMenu("DeleteAllPlayerPref")]
//    public void DeleteAllPlayerPref()
//    {
//        PlayerPrefs.DeleteAll();
//    }

//    public void ApiDebug(string content)
//    {
//        // Debug.Log(content);
//        Debug.LogFormat("Log {0}", content);
//    }

//    public static bool IsEditor()
//    {
//        if (Application.platform == RuntimePlatform.WindowsEditor ||
//            Application.platform == RuntimePlatform.OSXEditor)
//            return true;
//        return false;
//    }

//    public static bool IsAndroid()
//    {
//#if UNITY_ANDROID
//        return true;
//#endif
//        return false;
//    }

//    public static bool IsIOS()
//    {
//#if UNITY_IOS
//        return true;
//#endif
//        return false;
//    }

//    //    public bool IsAppInstalled(string bundleID)
//    //    {
//    //#if UNITY_ANDROID
//    //        if (Application.platform == RuntimePlatform.Android)
//    //        {
//    //            AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
//    //            AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
//    //            AndroidJavaObject packageManager = ca.Call<AndroidJavaObject>("getPackageManager");
//    //            Debug.Log(" ********LaunchOtherApp " + bundleID);
//    //            AndroidJavaObject launchIntent = null;
//    //            //if the app is installed, no errors. Else, doesn't get past next line
//    //            try
//    //            {
//    //                launchIntent = packageManager.Call<AndroidJavaObject>("getLaunchIntentForPackage", bundleID);
//    //                //        
//    //                //        ca.Call("startActivity",launchIntent);
//    //            }
//    //            catch (Exception ex)
//    //            {
//    //                Debug.Log("exception" + ex.Message);
//    //            }

//    //            if (launchIntent == null)
//    //            {
//    //                Debug.Log("********* null " + bundleID);
//    //                return false;
//    //            }
//    //            Debug.Log("**** exist " + bundleID);
//    //            return true;
//    //        }

//    //        return false;
//    //#else
//    //        return false;
//    //#endif
//    //    }
//}

//public static class APIExtention
//{
//    public static string CorrectString(this string input)
//    {
//        if (String.IsNullOrEmpty(input))
//            return null;
//        return input.Replace("\r", string.Empty);
//    }

//    public static void Shuffle<T>(this IList<T> list)
//    {
//        int n = list.Count;
//        while (n > 1)
//        {
//            n--;
//            int k = UnityEngine.Random.Range(0, n);
//            T value = list[k];
//            list[k] = list[n];
//            list[n] = value;
//        }
//    }
//}
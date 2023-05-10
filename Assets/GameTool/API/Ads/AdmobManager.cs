//using UnityEngine.Events;
//using UnityEngine;
//using GoogleMobileAds.Api;
//using GoogleMobileAds.Common;
//using UnityEngine.UI;
//using System;
//using System.Collections.Generic;
//using Firebase.Analytics;
//using System.Collections;

//public class AdmobManager
//{
//    private ApiInfor apiInfor;

//    // BANNER
//    private BannerView bannerView;
//    private bool isBannerRequesting;
//    private bool isBannerLoaded;
//    private int countTryLoadBanner;

//    // INTERSTITIALAD
//    private InterstitialAd interstitialAd;
//    private bool isInterstitialAdRequesting;
//    private Action<bool> onInterstitialClosed;
//    private int countTryLoadInterstitial;
//    private float lastTimeShowInterstitial;

//    // REWARD
//    private RewardedAd rewardedAd;
//    private bool isRewardRequesting;
//    private bool isRewardShowing;
//    private bool isInited;
//    static Action<bool> onRewardClosed;

//    // REWARD Inter
//    private RewardedInterstitialAd rewardedInterstitialAd;
//    private bool isRewardInterRequesting;
//    private bool isRewardInterShowing;
//    private bool isInitedRewardInter;
//    static Action<bool> onRewardInterClosed;
//    private int countTryLoadRewardInter;
//    private int countTryLoadReward;
//    private bool isRewarded;


//    public void InitAdmob(ApiInfor infor)
//    {
//        apiInfor = infor;

//        List<String> deviceIds = new List<String>() { AdRequest.TestDeviceSimulator };

//        if (SDK.Instance.ApiInfor.IsTest)
//        {
//            apiInfor.BannerId = "ca-app-pub-3940256099942544/6300978111";
//            apiInfor.InterstitialId = "ca-app-pub-3940256099942544/1033173712";
//            apiInfor.RewardId = "ca-app-pub-3940256099942544/5224354917";
//        }

//#if UNITY_IPHONE
//        deviceIds.Add("");
//#elif UNITY_ANDROID
//        deviceIds.Add("");
//#endif

//        if (infor.AppId != string.Empty)
//        {
//            infor.AppId.CorrectString();
//            MobileAds.SetiOSAppPauseOnBackground(true);

//            RequestConfiguration requestConfiguration =
//                new RequestConfiguration.Builder()
//                .SetTagForChildDirectedTreatment(TagForChildDirectedTreatment.Unspecified)
//                .SetTestDeviceIds(deviceIds).build();


//            MobileAds.SetRequestConfiguration(requestConfiguration);

//            // Initialize the Google Mobile Ads SDK.
//            MobileAds.Initialize(HandleInitCompleteAction);
//            //MobileAds.Initialize((initStatus) =>
//            //{
//            //    // SDK initialization is complete
//            //});
//        }

//        if (apiInfor.IsUseBanner && !SDK.IsRemoveAds)
//        {
//            apiInfor.BannerId.CorrectString();
//            RequestBanner();
//        }

//        if (apiInfor.IsUseInterstitial && !SDK.IsRemoveAds)
//        {
//            apiInfor.InterstitialId.CorrectString();
//            RequestInterstitial();
//            lastTimeShowInterstitial = Time.time - infor.InterstitialInterval;
//        }

//        if (apiInfor.IsUseReward)
//        {

//            apiInfor.RewardId.CorrectString();
//            RequestReward();
//        }
//    }


//    private void HandleInitCompleteAction(InitializationStatus initstatus)
//    {
//        // Callbacks from GoogleMobileAds are not guaranteed to be called on
//        // main thread.
//        // In this example we use MobileAdsEventExecutor to schedule these calls on
//        // the next Update() loop.
//        MobileAdsEventExecutor.ExecuteInUpdate(() =>
//        {
//            //statusText.text = "Initialization complete";
//            RequestBanner();
//        });
//    }

//    #region BANNER

//    private void RequestBanner()
//    {
//        if (isBannerRequesting)
//            return;

//        if (SDK.IsRemoveAds)
//            return;

//        LogBanner("request");


//        SDK.Instance.ApiDebug("BannerId:" + apiInfor.BannerId);

//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = "ca-app-pub-8324690468488017/8274435711";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-8324690468488017/8274435711";
//#else
//        string adUnitId = "unexpected_platform";
//#endif

//        isBannerRequesting = true;
//        isBannerLoaded = false;
//        if (bannerView != null)
//        {
//            bannerView.Destroy();
//        }

//        // _bannerView = new BannerView(_apiInfor.BannerId, AdSize.Banner, AdPosition.Bottom);
//        bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

//        bannerView.OnAdLoaded += (sender, args) =>
//        {
//            isBannerRequesting = false;
//            isBannerLoaded = true;
//            LogBanner("loaded");
//            countTryLoadBanner = 0;
//            // int w = Screen.width / 2 - (int)_bannerView.GetWidthInPixels() / 2 + (int)_bannerView.GetWidthInPixels() / _apiInfor.offsetBanner;
//            // int h = Screen.height - (int)_bannerView.GetHeightInPixels();
//            // w = w * 160 / (int)Screen.dpi;
//            // h = h * 160 / (int)Screen.dpi;
//            // _bannerView.SetPosition(w, h);

//            if (!SDK.IsBannerShowing)
//                HideBanner();
//        };
//        // Called when an ad request failed to load.
//        bannerView.OnAdFailedToLoad += (sender, args) =>
//        {
//            isBannerLoaded = false;
//            isBannerRequesting = false;
//            LogBanner("failed");



//            if (countTryLoadBanner < apiInfor.NumTryLoad)
//            {
//                countTryLoadBanner++;
//                LogBanner("try_load" + countTryLoadBanner);

//                SDK.Instance.ApiDebug("Banner try load " + countTryLoadBanner + "/" + apiInfor.NumTryLoad);
//                RequestBanner();
//            }
//            else
//            {
//                countTryLoadBanner = 0;
//            }
//        };
//        // Called when an ad is clicked.
//        bannerView.OnAdOpening += (sender, args) => { LogBanner("opening"); };
//        // Called when the user returned from the app after an ad click.
//        bannerView.OnAdClosed += (sender, args) => { LogBanner("closed"); };
//        // Called when the ad click caused the user to leave the application.
//        //bannerView.OnAdLeavingApplication += (sender, args) => { LogBanner("leaving"); };

//        // Load the banner with the request.
//        bannerView.LoadAd(GetAdRequest());
//    }

//    public bool IsBannerLoaded()
//    {
//        return isBannerLoaded;
//    }

//    public void ShowBanner()
//    {
//        if (bannerView != null)
//        {
//            if (isBannerLoaded)
//                bannerView.Show();
//            else
//                RequestBanner();
//        }
//    }

//    public void HideBanner()
//    {
//        if (bannerView != null)
//            bannerView.Hide();
//    }

//    #endregion

//    #region INTERSTITIAL

//    private void RequestInterstitial()
//    {
//        //        API.Instance.ApiDebug("requesting " + _isInterstitialAdRequesting);
//        if (isInterstitialAdRequesting)
//            return;

//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = "ca-app-pub-8324690468488017/9395945697";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-8324690468488017/9395945697";
//#else
//        string adUnitId = "unexpected_platform";
//#endif
//        LogInterstitial("request");


//        SDK.Instance.ApiDebug("FullId:" + apiInfor.InterstitialId);

//        isInterstitialAdRequesting = true;

//        if (interstitialAd != null)
//            interstitialAd.Destroy();

//        interstitialAd = new InterstitialAd(adUnitId);


//        // Called when an ad request has successfully loaded.
//        interstitialAd.OnAdLoaded += (sender, args) =>
//        {
//            isInterstitialAdRequesting = false;
//            LogInterstitial("loaded");
//            countTryLoadInterstitial = 0;
//            TrackingManager.LogEvent("Inter_Impression");
//            if (SDK.Instance.InterstitialLoaded != null)
//                SDK.Instance.InterstitialLoaded();
//        };
//        // Called when an ad request failed to load.
//        interstitialAd.OnAdFailedToLoad += (sender, args) =>
//        {
//            if (SDK.Instance.InterstitialAdLoadFailed != null)
//                SDK.Instance.InterstitialAdLoadFailed();
//            isInterstitialAdRequesting = false;
//            LogInterstitial("failed");

//            if (countTryLoadInterstitial < apiInfor.NumTryLoad)
//            {
//                countTryLoadInterstitial++;
//                LogInterstitial("try_load" + countTryLoadInterstitial);

//                SDK.Instance.ApiDebug("Interstitial try load " + countTryLoadInterstitial + "/" +
//                                      apiInfor.NumTryLoad);
//                RequestInterstitial();
//            }
//            else
//            {
//                countTryLoadInterstitial = 0;
//            }
//        };
//        // Called when an ad is clicked.
//        interstitialAd.OnAdOpening += (sender, args) =>
//        {
//            AudioListener.pause = true;
//            LogInterstitial("opening");
//            TrackingManager.LogEvent("Inter_Click");
//        };
//        // Called when the user returned from the app after an ad click.
//        interstitialAd.OnAdClosed += (sender, args) =>
//        {
//            LogInterstitial("closed");
//            RequestInterstitial();
//            AudioListener.pause = false;

//            SDK.Instance.StartCoroutine(CompleteMethodInterstitial());
//        };
//        // Called when the ad click caused the user to leave the application.
//        //interstitialAd.OnAdLeavingApplication += (sender, args) => { LogInterstitial("leaving"); };

//        // Load the banner with the request.
//        interstitialAd.LoadAd(GetAdRequest());
//    }

//    IEnumerator CompleteMethodInterstitial()
//    {
//        yield return null;
//        if (onInterstitialClosed != null)
//            onInterstitialClosed(true);
//    }

//    public bool IsInterstitialAds()
//    {
//        if (interstitialAd == null)
//            return false;
//        return interstitialAd.IsLoaded();
//    }

//    public void ShowInterstitial(Action<bool> onClosed)
//    {
//        if (interstitialAd == null)
//        {
//            if (onClosed != null)
//                onClosed(false);
//            return;
//        }

//        if (interstitialAd.IsLoaded())
//        {
//            if (CanShowFull())
//            {
//                onInterstitialClosed = onClosed;
//                lastTimeShowInterstitial = Time.time;
//                SDK.Instance.ShowLoading(() => { interstitialAd.Show(); });

//            }
//            else
//            {
//                if (onClosed != null)
//                    onClosed(false);
//            }
//        }
//        else
//        {
//            if (onClosed != null)
//                onClosed(false);
//            RequestInterstitial();
//        }
//    }

//    public bool CanShowFull()
//    {
//        return Time.time - lastTimeShowInterstitial >= apiInfor.InterstitialInterval;
//    }

//    #endregion

//    #region REWARD

//    static bool WaitingReward;

//    private void RequestReward(bool waitingReward = false)
//    {

//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//        string adUnitId = "ca-app-pub-8324690468488017/3580512233";
//#elif UNITY_IPHONE
//        string adUnitId = "ca-app-pub-8324690468488017/3580512233";
//#else
//        string adUnitId = "unexpected_platform";
//#endif


//        WaitingReward = waitingReward;
//        if (isRewardRequesting || isRewardShowing)
//            return;
//        isRewarded = false;
//        LogReward("request");


//        SDK.Instance.ApiDebug("Request RewardID:" + apiInfor.RewardId);

//        isRewardRequesting = true;



//        this.rewardedAd = new RewardedAd(adUnitId);
//        // Called when an ad request has successfully loaded.
//        rewardedAd.OnAdLoaded += (sender, args) =>
//        {

//            SDK.Instance.ApiDebug("Loaded Reward:");

//            isRewardRequesting = false;
//            LogReward("loaded");
//            countTryLoadReward = 0;
//            TrackingManager.LogEvent("Reward_Impression");
//            if (!SDK.IsBannerShowing)
//                HideBanner();
//            if (WaitingReward)
//                ShowReward(onRewardClosed);
//        };
//        // Called when an ad request failed to load.
//        rewardedAd.OnAdFailedToLoad += (sender, args) =>
//        {
//            isRewardRequesting = false;
//            LogReward("failed");
//            //SDK.Instance.ApiDebug("reward failed: " + args.Message);

//            if (countTryLoadReward < apiInfor.NumTryLoad)
//            {
//                countTryLoadReward++;
//                LogReward("try_load" + countTryLoadReward);

//                SDK.Instance.ApiDebug("reward try load " + countTryLoadReward + "/" + apiInfor.NumTryLoad);
//                RequestReward(WaitingReward);
//            }
//            else
//            {
//                countTryLoadReward = 0;
//                if (WaitingReward)
//                {
//                    ToastMgr.Instance.Show("Video Ads are unavailable at the moment");
//                    onRewardClosed(false);
//                }
//            }
//        };
//        // Called when an ad is clicked.
//        rewardedAd.OnAdOpening += (sender, args) =>
//        {
//            AudioListener.pause = true; LogReward("opening");
//            TrackingManager.LogEvent("Reward_Click");
//        };
//        // Called when the user returned from the app after an ad click.
//        rewardedAd.OnAdClosed += (sender, args) =>
//        {
//            SDK.Instance.ApiDebug("reward closed");
//            SDK.Instance.StartCoroutine(CompleteMethodRewardedVideo());
//            AudioListener.pause = false;
//        };

//        rewardedAd.OnUserEarnedReward += (sender, args) =>
//        {
//            SDK.Instance.ApiDebug("reward finish");
//            LogReward("finish");
//            isRewarded = true;
//            Gamedata.I.AdsGiftCount++;
//            TrackingManager.LogEvent("Reward_Completed");
//        };



//        rewardedAd.LoadAd(GetAdRequest());
//    }

//    IEnumerator CompleteMethodRewardedVideo()
//    {
//        yield return new WaitForSecondsRealtime(1f);
//        if (onRewardClosed != null)
//        {
//            onRewardClosed(isRewarded);
//            if (!isRewarded)
//                ToastMgr.Instance.Show("Video Reward Not Completed");
//            LogReward("rewarded");
//        }
//        SDK.Instance.ApiDebug("reward callback");
//        isRewardShowing = false;
//        LogReward("closed");
//        RequestReward();

//    }


//    public bool IsRewardLoaded()
//    {
//        if (rewardedAd.IsLoaded())
//            return true;

//        RequestReward();
//        return false;
//    }

//    public void ShowReward(Action<bool> onClose)
//    {

//        if (rewardedAd == null)
//        {
//            onClose(false);
//            SDK.Instance.ApiDebug("rewardBasedVideo null");
//            return;
//        }

//        onRewardClosed = onClose;

//        if (rewardedAd.IsLoaded())
//        {
//            isRewardShowing = true;
//            SDK.IsShowingAdsReturn = true;
//            lastTimeShowInterstitial = Time.time;
//            rewardedAd.Show();
//        }
//        else
//        {
//            RequestReward(true);
//            // onClose(false);
//        }
//    }

//    #endregion
//    #region Reward Inter

//    bool WaitingRewardInter;
//    public void RequestAndLoadRewardedInterstitialAd(bool waitingRewardInter = false)
//    {
//        // These ad units are configured to always serve test ads.
//#if UNITY_EDITOR
//        string adUnitId = "unused";
//#elif UNITY_ANDROID
//            string adUnitId = "ca-app-pub-3940256099942544/5354046379";
//#elif UNITY_IPHONE
//            string adUnitId = "ca-app-pub-3940256099942544/6978759866";
//#else
//            string adUnitId = "unexpected_platform";
//#endif
//        WaitingRewardInter = waitingRewardInter;
//        if (isRewardInterRequesting || isRewardInterShowing)
//            return;
//        isRewarded = false;
//        LogReward("request");


//        SDK.Instance.ApiDebug("Request RewardID:" + adUnitId);

//        isRewardInterRequesting = true;
//        // Create an interstitial.
//        RewardedInterstitialAd.LoadAd(adUnitId, GetAdRequest(), (rewardedInterstitialAd, error) =>
//        {

//            if (error != null)
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    AudioListener.pause = false;
//                    LogRewardInter("RewardedInterstitialAd load failed, error: " + error);
//                });
//                return;
//            }

//            this.rewardedInterstitialAd = rewardedInterstitialAd;
//            MobileAdsEventExecutor.ExecuteInUpdate(() =>
//            {
//                SDK.Instance.ApiDebug("Loaded Reward:");
//                isRewardInterRequesting = false;
//                countTryLoadRewardInter = 0;
//                if (!SDK.IsBannerShowing)
//                    HideBanner();
//                if (WaitingReward)
//                    ShowRewardedInterstitialAd(onRewardInterClosed);
//                LogRewardInter("RewardedInterstitialAd loaded");
//            });
//            // Register for ad events.
//            this.rewardedInterstitialAd.OnAdDidPresentFullScreenContent += (sender, args) =>
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    AudioListener.pause = true;
//                    LogRewardInter("Rewarded Interstitial presented.");
//                });
//            };
//            this.rewardedInterstitialAd.OnAdDidDismissFullScreenContent += (sender, args) =>
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    AudioListener.pause = false;
//                    LogRewardInter("Rewarded Interstitial dismissed.");
//                });
//                this.rewardedInterstitialAd = null;
//            };
//            this.rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += (sender, args) =>
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    AudioListener.pause = false;
//                    LogRewardInter("Rewarded Interstitial failed to present.");
//                });
//                this.rewardedInterstitialAd = null;
//            };
//        });
//    }

//    public void ShowRewardedInterstitialAd(Action<bool> onClose)
//    {
//        if (rewardedInterstitialAd == null)
//        {
//            onClose(false);
//            SDK.Instance.ApiDebug("rewardInterBasedVideo null");
//            return;
//        }
//        if (rewardedInterstitialAd != null)
//        {
//            onRewardInterClosed = onClose;

//            isRewardInterShowing = true;
//            SDK.IsShowingAdsReturn = true;
//            lastTimeShowInterstitial = Time.time;
//            rewardedInterstitialAd.Show((reward) =>
//            {
//                MobileAdsEventExecutor.ExecuteInUpdate(() =>
//                {
//                    LogRewardInter("User Rewarded: " + reward.Amount);
//                    AudioListener.pause = false;
//                    isRewarded = true;
//                });
//            });


//        }
//        else
//        {
//            LogRewardInter("Rewarded ad is not ready yet.");
//        }
//    }
//    #endregion
//    private AdRequest GetAdRequest()
//    {
//        var request = new AdRequest.Builder();

//        //// Add test device
//        //foreach (var test in apiInfor.TestDevices)
//        //    if (test != string.Empty)
//        //        request.AddTestDevice(test);

//        // Add keyword
//        foreach (var keyWord in apiInfor.KeyWords)
//            if (keyWord != string.Empty)
//                request.AddKeyword(keyWord);

//        if (apiInfor.MaxAdContentRating != MaxAdContentRating.None)
//            request.AddExtra("max_ad_content_rating", apiInfor.MaxAdContentRating.ToString());

//        return request.Build();
//    }

//    private void LogBanner(string paramaterValue)
//    {
//        //LogApi("banner", paramaterValue);
//    }

//    private void LogInterstitial(string paramaterValue)
//    {
//        //LogApi("interstitial", paramaterValue);
//    }

//    private void LogReward(string paramaterValue)
//    {
//        //LogApi("reward", paramaterValue);
//    }
//    private void LogRewardInter(string paramaterValue)
//    {
//        //LogApi("reward", paramaterValue);
//    }


//    private void LogApi(string parameterName, string parameterValue)
//    {
//        //FirebaseAnalytics.LogEvent("Ads", parameterName, parameterValue);
//    }
//}
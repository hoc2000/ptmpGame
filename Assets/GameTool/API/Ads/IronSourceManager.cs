
//using com.adjust.sdk;
//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Tool;
//using UnityEngine;

//public class IronSourceManager
//{
//    private AppInform appInform;

//    // BANNER
//    //private BannerView _bannerView;
//    private bool isBannerRequesting;
//    private bool isBannerLoaded;
//    private int countTryLoadBanner;

//    // INTERSTITIALAD

//    private bool isInterstitialAdRequesting;
//    private Action<bool> onCloseInter;
//    //private int countTryLoadInterstitial;
//    private float lastTimeShowInterstitial;

//    // REWARD
//    private bool isRewardRequesting;
//    private bool isRewardShowing;
//    private bool isInited;

//    static Action<bool> onCloseReward;

//    private int countTryLoadReward;
//    private bool isRewarded;


//    bool succses;
//    public void InitIron(AppInform infor)
//    {

//        appInform = infor;
//#if UNITY_ANDROID
//        string appKey = "13de68f69";
//#elif UNITY_IOS
//        string appKey = "13de68f69";
//#else
//        string appKey = "";
//#endif

//        //IronSource.Agent.init(_apiInfor.AppId);
//        IronSource.Agent.validateIntegration();

//        IronSource.Agent.init(appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL, IronSourceAdUnits.OFFERWALL, IronSourceAdUnits.BANNER);


//        //For Rewarded Video
//        //IronSource.Agent.init(_apiInfor.RewardId, IronSourceAdUnits.REWARDED_VIDEO);
//        //For Interstitial
//        //  IronSource.Agent.init(_apiInfor.InterstitialId, IronSourceAdUnits.INTERSTITIAL);
//        //For Offerwall
//        //IronSource.Agent.init("", IronSourceAdUnits.OFFERWALL);
//        //For Banners
//        //IronSource.Agent.init(_apiInfor.BannerId, IronSourceAdUnits.BANNER);

//        IronSource.Agent.shouldTrackNetworkState(true);
//        IronSourceEvents.onSdkInitializationCompletedEvent += Event;

//    }
//    public void Event()
//    {
//        //Add Rewarded Video Events
//        IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent; // Gọi khi ads được mở
//        IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; // Gọi khi ads đóng
//        IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent; //Invoked when there is a change in the ad availability status
//        IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent; // Được gọi khi quảng cáo video bắt đầu phát
//        IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent; // Được gọi khi quảng cáo video phát xong
//        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; // Được mời khi người dùng hoàn thành video và sẽ được thưởng
//        IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent; // Được mời khi Video có tặng thưởng không hiển thị
//        IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent; // Được gọi khi quảng cáo video được nhấp vào

//        //Add Rewarded Video DemandOnly Events
//        IronSourceEvents.onRewardedVideoAdOpenedDemandOnlyEvent += RewardedVideoAdOpenedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdClosedDemandOnlyEvent += RewardedVideoAdClosedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdLoadedDemandOnlyEvent += RewardedVideoAdLoadedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdRewardedDemandOnlyEvent += RewardedVideoAdRewardedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdShowFailedDemandOnlyEvent += RewardedVideoAdShowFailedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdClickedDemandOnlyEvent += RewardedVideoAdClickedDemandOnlyEvent;
//        IronSourceEvents.onRewardedVideoAdLoadFailedDemandOnlyEvent += RewardedVideoAdLoadFailedDemandOnlyEvent;


//        // Add Offerwall Events
//        IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
//        IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
//        IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
//        IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
//        IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
//        IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;


//        // Add Interstitial Events
//        IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;//Được gọi khi Quảng cáo xen kẽ sẵn sàng hiển thị sau khi hàm tải được gọi
//        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent; //Được gọi khi quá trình khởi tạo không thành công
//        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; // Được gọi ngay trước khi màn hình Trung gian sắp mở
//        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; //Được gọi khi quảng cáo không hiển thị
//        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent; // Được gọi khi người dùng cuối nhấp vào quảng cáo xen kẽ
//        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent; //Được gọi khi Đơn vị quảng cáo xen kẽ đã mở
//        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent; //Được gọi khi quảng cáo xen kẽ đóng và người dùng quay lại màn hình ứng dụng.

//        // Add Interstitial DemandOnly Events
//        IronSourceEvents.onInterstitialAdReadyDemandOnlyEvent += InterstitialAdReadyDemandOnlyEvent;
//        IronSourceEvents.onInterstitialAdLoadFailedDemandOnlyEvent += InterstitialAdLoadFailedDemandOnlyEvent;
//        IronSourceEvents.onInterstitialAdShowFailedDemandOnlyEvent += InterstitialAdShowFailedDemandOnlyEvent;
//        IronSourceEvents.onInterstitialAdClickedDemandOnlyEvent += InterstitialAdClickedDemandOnlyEvent;
//        IronSourceEvents.onInterstitialAdOpenedDemandOnlyEvent += InterstitialAdOpenedDemandOnlyEvent;
//        IronSourceEvents.onInterstitialAdClosedDemandOnlyEvent += InterstitialAdClosedDemandOnlyEvent;


//        // Add Banner Events
//        IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
//        IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;
//        IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent;
//        IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent;
//        IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
//        IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

//        //Add ImpressionSuccess Event
//        IronSourceEvents.onImpressionSuccessEvent += ImpressionSuccessEvent;
//        IronSourceEvents.onImpressionDataReadyEvent += ImpressionDataReadyEvent;

//        LoadInter();
//    }
//    public bool IsRewardLoaded()
//    {
//        if (IronSource.Agent.isRewardedVideoAvailable())
//            return true;
//        ;
//        return false;
//    }

//    //bool isTimes;
//    public void ShowBanner()
//    {
//        //if (isTimes)
//        //{
//        //    isTimes = false;
//        //    IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
//        //}
//        IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);

//    }
//    public void HideBanner()
//    {
//        IronSource.Agent.destroyBanner();
//    }
//    void LoadInter()
//    {
//        Debug.Log("LoadInter");
//        IronSource.Agent.loadInterstitial();
//    }
//    public void ShowInterstitial(Action<bool> onClosed)
//    {
//        Debug.Log("Have Inter: " + IronSource.Agent.isInterstitialReady());

//        if (IronSource.Agent.isInterstitialReady())
//        {
//            if (CanShowInter())
//            {
//                onCloseInter = onClosed;
//                lastTimeShowInterstitial = Time.time;
//                SDK.Instance.ShowLoading(() =>
//                {
//                    IronSource.Agent.showInterstitial();
//                });
//                TrackingManager.Instance.TrackingAds(AdsType.Inter, true, true, RewardID.EndLevel);
//            }
//            else
//            {
//                TrackingManager.Instance.TrackingAds(AdsType.Inter, false, CheckInternet(), RewardID.EndLevel);
//                if (onClosed != null)
//                    onClosed(false);
//            }
//        }
//        else
//        {
//            TrackingManager.Instance.TrackingAds(AdsType.Inter, false, CheckInternet(), RewardID.EndLevel);
//            LoadInter();
//            if (onClosed != null)
//                onClosed(false);
//        }
//    }
//    public void ShowReward(Action<bool> onClose, RewardID rewardId)
//    {
//        //if (rewardedAd == null)
//        //{
//        //    onClose(false);
//        //    API.Instance.ApiDebug("rewardBasedVideo null");
//        //    return;
//        //}
//        onCloseReward = onClose;

//        if (IronSource.Agent.isRewardedVideoAvailable())
//        {
//            isRewardShowing = true;
//            SDK.IsShowingAdsReturn = true;
//            SDK.Instance.ShowLoading(() =>
//            {
//                IronSource.Agent.showRewardedVideo();
//            });
//            //lastTimeShowInterstitial = Time.time;
//            TrackingManager.Instance.TrackingAds(AdsType.Reward, true, CheckInternet(), rewardId);
//        }
//        else
//        {
//            ToastMgr.Instance.Show("Video Ads Failed To Show");
//            onCloseReward(false);
//            TrackingManager.Instance.TrackingAds(AdsType.Reward, false, CheckInternet(), rewardId);
//        }
//    }
//    bool CanShowInter()
//    {
//        return Time.time - lastTimeShowInterstitial >= appInform.InterstitialInterval;
//    }
//    bool CheckInternet()
//    {
//        return Application.internetReachability != NetworkReachability.NotReachable;
//    }

//    void OnApplicationPause(bool isPaused)
//    {
//        Debug.Log("unity-script: OnApplicationPause = " + isPaused);
//        IronSource.Agent.onApplicationPause(isPaused);
//    }

//    #region RewardedAd callback handlers

//    void RewardedVideoAvailabilityChangedEvent(bool canShowAd)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAvailabilityChangedEvent, value = " + canShowAd);
//    }

//    void RewardedVideoAdOpenedEvent()
//    {
//        //TrackingManager.Instance.LogEvent("show_rewarded_ads");
//        AudioListener.volume = 0;
//        Time.timeScale = 0;
//        Debug.Log("unity-script: I got RewardedVideoAdOpenedEvent");
//    }

//    void RewardedVideoAdRewardedEvent(IronSourcePlacement ssp)
//    {
//        succses = true;
//        Debug.Log("unity-script: I got RewardedVideoAdRewardedEvent, amount = " + ssp.getRewardAmount() + " name = " + ssp.getRewardName());

//    }

//    void RewardedVideoAdClosedEvent()
//    {

//        if (succses)
//        {
//            onCloseReward(true);
//        }
//        else
//        {
//            onCloseReward(false);
//        }
//        Time.timeScale = 1;
//        AudioListener.volume = 1f;
//        Debug.Log("unity-script: I got RewardedVideoAdClosedEvent");
//    }

//    void RewardedVideoAdStartedEvent()
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdStartedEvent");
//    }

//    void RewardedVideoAdEndedEvent()
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdEndedEvent");
//    }

//    void RewardedVideoAdShowFailedEvent(IronSourceError error)
//    {
//        AudioListener.volume = 1;

//        onCloseReward(false);
//        IronSource.Agent.isRewardedVideoAvailable();

//        Debug.Log("unity-script: I got RewardedVideoAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void RewardedVideoAdClickedEvent(IronSourcePlacement ssp)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdClickedEvent, name = " + ssp.getRewardName());
//    }

//    /************* RewardedVideo DemandOnly Delegates *************/

//    void RewardedVideoAdLoadedDemandOnlyEvent(string instanceId)
//    {

//        Debug.Log("unity-script: I got RewardedVideoAdLoadedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void RewardedVideoAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
//    {

//        Debug.Log("unity-script: I got RewardedVideoAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void RewardedVideoAdOpenedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdOpenedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void RewardedVideoAdRewardedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdRewardedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void RewardedVideoAdClosedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdClosedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void RewardedVideoAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void RewardedVideoAdClickedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got RewardedVideoAdClickedDemandOnlyEvent for instance: " + instanceId);
//    }


//    #endregion



//    #region Interstitial callback handlers

//    void InterstitialAdReadyEvent()
//    {
//        //SDK.Instance. = false;
//        Debug.Log("unity-script: I got InterstitialAdReadyEvent");
//    }

//    void InterstitialAdLoadFailedEvent(IronSourceError error)
//    {
//        //API.Instance.hasLoadInterAds = false;
//        // Time.timeScale = 1;
//        // _onInterstitialClosed(false);
//        Debug.Log("unity-script: I got InterstitialAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
//    }



//    void InterstitialAdShowSucceededEvent()
//    {
//        //TrackingManager.instance.TrackNumberAdsDownload(NAME_ADS.ADS_INTERSTITIAL);
//        //TrackingManager.instance.TrackAdsView(AFramework.Ads.AdsType.Interstitial, ValueDictionaryTracking.Instance.dictionaryValueInter);

//        Debug.Log("unity-script: I got InterstitialAdShowSucceededEvent");
//    }

//    void InterstitialAdShowFailedEvent(IronSourceError error)
//    {

//        Time.timeScale = 1;
//        Debug.Log("unity-script: I got InterstitialAdShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void InterstitialAdClickedEvent()
//    {
//        //TrackingManager.instance.TrackAdsClick(AFramework.Ads.AdsType.Interstitial);
//        Debug.Log("unity-script: I got InterstitialAdClickedEvent");
//    }

//    void InterstitialAdOpenedEvent()
//    {
//        //TrackingManager.instance.TrackNumberAdsDownload(NAME_ADS.ADS_INTERSTITIAL);
//        //TrackingManager.instance.TrackAdsView(AFramework.Ads.AdsType.Interstitial, ValueDictionaryTracking.Instance.dictionaryValueInter);
//        Time.timeScale = 0;
//        AudioListener.volume = 0f;
//        Debug.Log("unity-script: I got InterstitialAdOpenedEvent");
//    }

//    void InterstitialAdClosedEvent()
//    {
//        Time.timeScale = 1;

//        AudioListener.volume = 1f;
//        IronSource.Agent.loadInterstitial();

//        SDK.Instance.StartCoroutine(CompleteMethodInterstitial());
//        Debug.Log("unity-script: I got InterstitialAdClosedEvent");
//    }
//    IEnumerator CompleteMethodInterstitial()
//    {
//        yield return null;
//        if (onCloseInter != null)
//            onCloseInter(true);
//    }
//    /************* Interstitial DemandOnly Delegates *************/

//    void InterstitialAdReadyDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got InterstitialAdReadyDemandOnlyEvent for instance: " + instanceId);
//    }

//    void InterstitialAdLoadFailedDemandOnlyEvent(string instanceId, IronSourceError error)
//    {
//        Debug.Log("unity-script: I got InterstitialAdLoadFailedDemandOnlyEvent for instance: " + instanceId + ", error code: " + error.getCode() + ",error description : " + error.getDescription());
//    }

//    void InterstitialAdShowFailedDemandOnlyEvent(string instanceId, IronSourceError error)
//    {
//        Debug.Log("unity-script: I got InterstitialAdShowFailedDemandOnlyEvent for instance: " + instanceId + ", error code :  " + error.getCode() + ",error description : " + error.getDescription());
//    }

//    void InterstitialAdClickedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got InterstitialAdClickedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void InterstitialAdOpenedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got InterstitialAdOpenedDemandOnlyEvent for instance: " + instanceId);
//    }

//    void InterstitialAdClosedDemandOnlyEvent(string instanceId)
//    {
//        Debug.Log("unity-script: I got InterstitialAdClosedDemandOnlyEvent for instance: " + instanceId);
//    }




//    #endregion

//    #region Banner callback handlers

//    void BannerAdLoadedEvent()
//    {
//        Debug.Log("unity-script: I got BannerAdLoadedEvent");
//    }

//    void BannerAdLoadFailedEvent(IronSourceError error)
//    {
//        Debug.Log("unity-script: I got BannerAdLoadFailedEvent, code: " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void BannerAdClickedEvent()
//    {
//        Debug.Log("unity-script: I got BannerAdClickedEvent");
//    }

//    void BannerAdScreenPresentedEvent()
//    {
//        Debug.Log("unity-script: I got BannerAdScreenPresentedEvent");
//    }

//    void BannerAdScreenDismissedEvent()
//    {
//        Debug.Log("unity-script: I got BannerAdScreenDismissedEvent");
//    }

//    void BannerAdLeftApplicationEvent()
//    {
//        Debug.Log("unity-script: I got BannerAdLeftApplicationEvent");
//    }

//    #endregion


//    #region Offerwall callback handlers

//    void OfferwallOpenedEvent()
//    {
//        Debug.Log("I got OfferwallOpenedEvent");
//    }

//    void OfferwallClosedEvent()
//    {
//        Debug.Log("I got OfferwallClosedEvent");
//    }

//    void OfferwallShowFailedEvent(IronSourceError error)
//    {
//        Debug.Log("I got OfferwallShowFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void OfferwallAdCreditedEvent(Dictionary<string, object> dict)
//    {
//        Debug.Log("I got OfferwallAdCreditedEvent, current credits = " + dict["credits"] + " totalCredits = " + dict["totalCredits"]);

//    }

//    void GetOfferwallCreditsFailedEvent(IronSourceError error)
//    {
//        Debug.Log("I got GetOfferwallCreditsFailedEvent, code :  " + error.getCode() + ", description : " + error.getDescription());
//    }

//    void OfferwallAvailableEvent(bool canShowOfferwal)
//    {
//        Debug.Log("I got OfferwallAvailableEvent, value = " + canShowOfferwal);

//    }

//    void ImpressionSuccessEvent(IronSourceImpressionData impressionData)
//    {
//        AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceIronSource);
//        adjustAdRevenue.setRevenue((double)impressionData.revenue, "USD");
//        // optional fields
//        adjustAdRevenue.setAdRevenueNetwork(impressionData.adNetwork);
//        adjustAdRevenue.setAdRevenueUnit(impressionData.adUnit);
//        adjustAdRevenue.setAdRevenuePlacement(impressionData.placement);
//        // track Adjust ad revenue
//        Adjust.trackAdRevenue(adjustAdRevenue);
//        Debug.Log("unity - script: I got ImpressionSuccessEvent ToString(): " + impressionData.ToString());
//        Debug.Log("unity - script: I got ImpressionSuccessEvent allData: " + impressionData.allData);
//    }

//    void ImpressionDataReadyEvent(IronSourceImpressionData impressionData)
//    {
//        Debug.Log("unity - script: I got ImpressionDataReadyEvent ToString(): " + impressionData.ToString());
//        Debug.Log("unity - script: I got ImpressionDataReadyEvent allData: " + impressionData.allData);
//    }

//    #endregion


//    private void LogBanner(string paramaterValue)
//    {
//        LogApi("banner", paramaterValue);
//    }

//    private void LogInterstitial(string paramaterValue)
//    {
//        LogApi("interstitial", paramaterValue);
//    }

//    private void LogReward(string paramaterValue)
//    {
//        LogApi("reward", paramaterValue);
//    }


//    private void LogApi(string parameterName, string parameterValue)
//    {
//        //FirebaseAnalytics.LogEvent("Ads", parameterName, parameterValue);
//    }
//}
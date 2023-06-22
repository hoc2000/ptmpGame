#if MEDIATION_APP_OPEN_AD
using com.adjust.sdk;
using GoogleMobileAds.Api;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class AppOpenAdManager {
    private static AppOpenAdManager instance;

    private AppOpenAd ad;

    private bool isShowingAd = false;
    private Action onAdFinishAction;
    private DateTime loadTime;
    private bool lastLoadFailed;
    private ScreenOrientation currentScreenOrientation = ScreenOrientation.Portrait;

    public static AppOpenAdManager Instance {
        get {
            if (instance == null) {
                instance = new AppOpenAdManager();
            }

            return instance;
        }
    }

    private string AdUnitId {
        get {
#if UNITY_ANDROID
            return WidogameFoundation.Config.WidogameAppSettingsLoader.AppSettings.AndroidAdmobAppOpenId;
#else
            return WidogameFoundation.Config.WidogameAppSettingsLoader.AppSettings.IOSAdmobAppOpenId;
#endif
        }
    }

    public void ShowAdIfAvailable(Action finishAction) {
        if (!IsAdAvailable || isShowingAd || Gamedata.IsRemoveAds) {
            LoadAdIfExpiredOrFailed();
            finishAction?.Invoke();
            return;
        }
        this.onAdFinishAction = finishAction;
        ad.OnAdDidDismissFullScreenContent += HandleAdDidDismissFullScreenContent;
        ad.OnAdFailedToPresentFullScreenContent += HandleAdFailedToPresentFullScreenContent;
        ad.OnAdDidPresentFullScreenContent += HandleAdDidPresentFullScreenContent;
        ad.OnAdDidRecordImpression += HandleAdDidRecordImpression;
        ad.OnPaidEvent += HandlePaidEvent;
        ad.Show();
    }
    private void HandleAdDidDismissFullScreenContent(object sender, EventArgs args) {
        Debug.Log("Closed app open ad");
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        isShowingAd = false;
        LoadAd();
        AdsMediationController.Instance.Enqueue(InvokeOnAdFinished);        
    }

    private void HandleAdFailedToPresentFullScreenContent(object sender, AdErrorEventArgs args) {
        Debug.LogFormat("Failed to present the ad (reason: {0})", args.AdError.GetMessage());
        // Set the ad to null to indicate that AppOpenAdManager no longer has another ad to show.
        ad = null;
        AdsMediationController.Instance.Enqueue(InvokeOnAdFinished);
        LoadAd();
    }

    private void InvokeOnAdFinished() {
        onAdFinishAction?.Invoke();
    }

    private void HandleAdDidPresentFullScreenContent(object sender, EventArgs args) {
        isShowingAd = true;
    }

    private void HandleAdDidRecordImpression(object sender, EventArgs args) {
    }

    private void HandlePaidEvent(object sender, AdValueEventArgs args) {
        AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
        adjustAdRevenue.setRevenue(args.AdValue.Value / 1000000.0f, args.AdValue.CurrencyCode);
        Adjust.trackAdRevenue(adjustAdRevenue);
    }
    public bool IsAdAvailable {
        get {
            return ad != null && !IsAdExpired;
        }
    }

    private bool IsAdExpired
    {
        get
        {
            return (DateTime.Now - loadTime).TotalHours > 4;
        }
    }

    private void LoadAdIfExpiredOrFailed() {
        if ((ad != null && IsAdExpired) || (ad == null && lastLoadFailed)) {
            LoadAd();
        }
    }

    public void LoadAd(ScreenOrientation orientation) {
        currentScreenOrientation = orientation;
        LoadAd();
    }

    private void LoadAd() {
        if (Gamedata.IsRemoveAds) return;
        AdRequest request = new AdRequest.Builder().Build();
        lastLoadFailed = false;
        // Load an app open ad for portrait orientation
        AppOpenAd.LoadAd(AdUnitId, currentScreenOrientation, request, ((appOpenAd, error) => {
            if (error != null) {
                // Handle the error.
                Debug.LogFormat("Failed to load the ad. (reason: {0})", error.LoadAdError.GetMessage());
                lastLoadFailed = true;
                return;
            }
            // App open ad is loaded.
            ad = appOpenAd;
            loadTime = DateTime.Now;
        }));
    }

}
#endif
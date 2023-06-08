using System;
using System.Collections;
using System.Collections.Generic;
using com.adjust.sdk;
using UnityEngine;
using UnityEngine.Events;
using WidogameFoundation.Config;
#if MEDIATION_MAX
namespace WidogameFoundation.Ads
{
    public class MaxAdsMediation : BaseAdsMediation {

        private bool isBannerLoaded;
        private BannerPosition currentBannerPosition;

        private string MaxSdkKey {
            get {
                return WidogameAppSettingsLoader.AppSettings.MaxSdkKey;
            }
        }

        private string MaxBannerAdUnit {
            get {
#if UNITY_ANDROID
                return WidogameAppSettingsLoader.AppSettings.AndroidMaxBannerAdUnit;
#else
                return WidogameAppSettingsLoader.AppSettings.IOSMaxBannerAdUnit;                
#endif
            }
        }

        private string MaxInterstitialAdUnit {
            get {
#if UNITY_ANDROID
                return WidogameAppSettingsLoader.AppSettings.AndroidMaxInterstitialAdUnit;
#else
                return WidogameAppSettingsLoader.AppSettings.IOSMaxInterstitialAdUnit;
#endif
            }
        }

        private string MaxRewardedAdUnit {
            get {
#if UNITY_ANDROID
                return WidogameAppSettingsLoader.AppSettings.AndroidMaxRewardedAdUnit;
#else
                return WidogameAppSettingsLoader.AppSettings.IOSMaxRewardedAdUnit;
#endif
            }
        }

        public override bool IsInterstitialAvailable {
            get {
#if UNITY_EDITOR
                return UnityEngine.Random.Range(0, 2) == 1;
#else
                return MaxSdk.IsInterstitialReady(MaxInterstitialAdUnit);
#endif
            }
        }
        public override bool IsRewardedAdsAvailable {
            get {
#if UNITY_EDITOR
                return UnityEngine.Random.Range(0, 2) == 1;
#else
                return MaxSdk.IsRewardedAdReady(MaxRewardedAdUnit);
#endif
            }
        }

        public override bool IsRewardedInterstitialAvailable {
            get {
                return false;
            }
        }

		public override void HideBanner()
        {
            MaxSdk.HideBanner(MaxBannerAdUnit);
        }

        public override void Init()
        {
            MaxSdkCallbacks.OnSdkInitializedEvent += OnSdkInitialized;
            MaxSdk.SetSdkKey(MaxSdkKey);
            MaxSdk.InitializeSdk();

            MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += Interstitial_OnAdHiddenEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += Interstitial_OnAdLoadFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += Interstitial_OnAdDisplayFailedEvent;
            MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += Interstitial_OnAdLoadedEvent;
            MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

            MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += Rewarded_OnAdHiddenEvent;
            MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += Rewarded_OnAdDisplayFailedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += Rewarded_OnAdLoadedEvent;
            MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += Rewarded_OnAdLoadFailedEvent    ;
            MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += Rewarded_OnAdReceivedRewardEvent;
            MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;

            MaxSdkCallbacks.Banner.OnAdLoadedEvent += Banner_OnAdLoadedEvent;
            MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnAdRevenuePaidEvent;
        }

        private void Banner_OnAdLoadedEvent(string arg1, MaxSdkBase.AdInfo arg2)
        {
            InvokeOnBannerLoaded();
        }

        private void Rewarded_OnAdReceivedRewardEvent(string arg1, MaxSdkBase.Reward arg2, MaxSdkBase.AdInfo arg3)
        {
            InvokeOnRewardedSuccess();
        }

        private void Rewarded_OnAdLoadFailedEvent(string adUnit, MaxSdkBase.ErrorInfo errorInfo)
        {
            InvokeOnRewardedLoadFailed();
        }

        private void Rewarded_OnAdLoadedEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnRewardedLoaded(adInfo.NetworkName);
        }

        private void Rewarded_OnAdDisplayFailedEvent(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnRewardedDisplayFailed();
        }

        private void Rewarded_OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnRewardedClosed();
        }

        private void OnAdRevenuePaidEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            var adRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAppLovinMAX);
            adRevenue.setRevenue(adInfo.Revenue, "USD");
            adRevenue.setAdRevenueNetwork(adInfo.NetworkName);
            adRevenue.setAdRevenueUnit(adInfo.AdUnitIdentifier);
            adRevenue.setAdRevenuePlacement(adInfo.Placement);

            Adjust.trackAdRevenue(adRevenue);
        }

        private void Interstitial_OnAdLoadedEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnInterstitialLoaded(adInfo.NetworkName);
        }

        private void Interstitial_OnAdDisplayFailedEvent(string adUnit, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnInterstitialDisplayFailed();
        }

        private void Interstitial_OnAdLoadFailedEvent(string adUnit, MaxSdkBase.ErrorInfo errorInfo)
        {
            InvokeOnInterstitialLoadFailed();
        }

        private void Interstitial_OnAdHiddenEvent(string adUnit, MaxSdkBase.AdInfo adInfo)
        {
            InvokeOnInterstitialClosed();
        }

        public override void InitInterstitial()
        {
        }

        public override void InitRewardedAds()
        {
            
        }

        public override void LoadInterstitial()
        {
            MaxSdk.LoadInterstitial(MaxInterstitialAdUnit);
        }

        public override void LoadRewardedAds()
        {
            MaxSdk.LoadRewardedAd(MaxRewardedAdUnit);
        }

        public override void ShowBanner(BannerPosition position)
        {
            if (isBannerLoaded)
            {
                if (position != currentBannerPosition)
                {
                    MaxSdk.DestroyBanner(MaxBannerAdUnit);
                    LoadBanner(position);
                }
            }
            else {
                LoadBanner(position);
            }
            MaxSdk.ShowBanner(MaxBannerAdUnit);
        }

        private void LoadBanner(BannerPosition position) {
            currentBannerPosition = position;
            MaxSdk.CreateBanner(MaxBannerAdUnit, position == BannerPosition.Top ? MaxSdkBase.BannerPosition.TopCenter : MaxSdkBase.BannerPosition.BottomCenter);
            MaxSdk.SetBannerWidth(MaxBannerAdUnit, 320);
            MaxSdk.SetBannerBackgroundColor(MaxBannerAdUnit, new Color(0, 0, 0, 0));
            isBannerLoaded = true;
        }

        public override void ShowInterstitial(string placement)
        {
            MaxSdk.ShowInterstitial(MaxInterstitialAdUnit, placement);
        }

        public override void ShowRewardedAds(string placement)
        {
            MaxSdk.ShowRewardedAd(MaxRewardedAdUnit);
        }

        private void OnSdkInitialized(MaxSdkBase.SdkConfiguration configuration) {
            InitRewardedAds();
        }

		public override void InitRewardedInterstitialAds() {
			
		}

		public override void LoadRewardedInterstitial() {
			
		}

		public override void ShowRewardedInterstitial(string placement) {
		    //MaxSdk.ShowRewardedAd()
		}
	}
}
#endif
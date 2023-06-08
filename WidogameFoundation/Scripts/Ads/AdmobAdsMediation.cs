using System;
using System.Collections;
using System.Collections.Generic;
using com.adjust.sdk;
using UnityEngine;
using WidogameFoundation.Ads;
using WidogameFoundation.Config;
#if MEDIATION_ADMOB
using GoogleMobileAds.Api;

namespace WidogameFoundation.Ads {
	public class AdmobAdsMediation : BaseAdsMediation {

		private BannerView bannerView;
		private InterstitialAd interstitialAd;
		private RewardedAd rewardedAd;
		private RewardedInterstitialAd rewardedInterstitialAd;
		private bool isBannerLoaded;
		private BannerPosition currentBannerPosition;
		private List<Action> pendingActions = new List<Action>();
		private bool isInitialized;

		public override bool IsInterstitialAvailable {
			get {
#if UNITY_EDITOR
				return interstitialAd != null && UnityEngine.Random.Range(0, 2) == 1;
#else
				return interstitialAd != null && interstitialAd.IsLoaded();
#endif
			}
		}

		public override bool IsRewardedAdsAvailable {
			get {
#if UNITY_EDITOR
				return rewardedAd != null && UnityEngine.Random.Range(0, 2) == 1;
#else
				return rewardedAd != null && rewardedAd.IsLoaded();
#endif
			}
		}

		private string BannerAdUnitId {
			get {
#if UNITY_ANDROID
				return WidogameAppSettingsLoader.AppSettings.AndroidAdmobBannerId;
#else
				return WidogameAppSettingsLoader.AppSettings.IOSAdmobBannerId;
#endif
			}
		}

		private string InterstitialAdUnitId {
			get {
#if UNITY_ANDROID
				return WidogameAppSettingsLoader.AppSettings.AndroidAdmobInterstitialId;
#else
				return WidogameAppSettingsLoader.AppSettings.IOSAdmobInterstitialId;
#endif
			}
		}

		private string RewardedAdUnitId {
			get {
#if UNITY_ANDROID
				return WidogameAppSettingsLoader.AppSettings.AndroidAdmobRewardedId;
#else
				return WidogameAppSettingsLoader.AppSettings.IOSAdmobRewardedId;
#endif
			}
		}

		private string RewardedInterstitialAdUnitId {
			get {
#if UNITY_ANDROID
				return WidogameAppSettingsLoader.AppSettings.AndroidAdmobRewardedInterstitialId;
#else
				return WidogameAppSettingsLoader.AppSettings.IOSAdmobRewardedInterstitialId;
#endif
			}
		}

		public override bool IsRewardedInterstitialAvailable {
			get {
				return rewardedInterstitialAd != null;
			}
		}

		public override void HideBanner() {
			if (bannerView != null) {
				bannerView.Hide();
			}
		}

		public override void Init() {
			MobileAds.Initialize(InitAdsFormats);
		}

		private void InitAdsFormats(InitializationStatus status) {
			isInitialized = true;
			if (pendingActions.Count > 0) {
				for (int i = 0; i < pendingActions.Count; i++) {
					pendingActions[i].Invoke();
				}
				pendingActions.Clear();
			}
		}

		public override void InitInterstitial() {
			if (interstitialAd != null) {
				return;
			}
			if (!isInitialized) {
				if (!pendingActions.Contains(DoInitInterstitial)) {
					pendingActions.Add(DoInitInterstitial);
				}
			} else {
				DoInitInterstitial();
			}
		}

		private void DoInitInterstitial() {
			this.interstitialAd = new InterstitialAd(InterstitialAdUnitId);
			this.interstitialAd.OnAdClosed += InterstitialAd_OnAdClosed;
			this.interstitialAd.OnAdFailedToShow += InterstitialAd_OnAdFailedToShow;
			this.interstitialAd.OnAdFailedToLoad += InterstitialAd_OnAdFailedToLoad;
			this.interstitialAd.OnAdLoaded += InterstitialAd_OnAdLoaded;
			this.interstitialAd.OnPaidEvent += Ad_OnPaidEvent;
		}

		private void InterstitialAd_OnAdClosed(object sender, EventArgs e) {
			InvokeOnInterstitialClosed();
		}

		private void InterstitialAd_OnAdFailedToShow(object sender, AdErrorEventArgs e) {
			InvokeOnInterstitialDisplayFailed();
		}

		private void InterstitialAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) {
			InvokeOnInterstitialLoadFailed();
		}

		private void InterstitialAd_OnAdLoaded(object sender, EventArgs e) {
			InvokeOnInterstitialLoaded(WidogameConstants.AD_NETWORK_NAME_NONE);
		}

		public override void InitRewardedAds() {
			if (rewardedAd != null) {
				return;
			}
			if (!isInitialized) {
				if (!pendingActions.Contains(DoInitRewardedAds)) {
					pendingActions.Add(DoInitRewardedAds);
				}
			} else {
				DoInitRewardedAds();
			}
		}

		private void DoInitRewardedAds() {
			this.rewardedAd = new RewardedAd(RewardedAdUnitId);
			this.rewardedAd.OnAdClosed += RewardedAd_OnAdClosed;
			this.rewardedAd.OnAdFailedToShow += RewardedAd_OnAdFailedToShow;
			this.rewardedAd.OnAdFailedToLoad += RewardedAd_OnAdFailedToLoad;
			this.rewardedAd.OnAdLoaded += RewardedAd_OnAdLoaded;
			this.rewardedAd.OnUserEarnedReward += RewardedAd_OnUserEarnedReward;
			this.rewardedAd.OnPaidEvent += Ad_OnPaidEvent;
		}

		private void RewardedAd_OnAdClosed(object sender, EventArgs e) {
			InvokeOnRewardedClosed();
		}

		private void RewardedAd_OnAdFailedToShow(object sender, AdErrorEventArgs e) {
			InvokeOnRewardedDisplayFailed();
		}

		private void RewardedAd_OnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e) {
			InvokeOnRewardedLoadFailed();
		}

		private void RewardedAd_OnAdLoaded(object sender, EventArgs e) {
			InvokeOnRewardedLoaded(WidogameConstants.AD_NETWORK_NAME_NONE);
		}

		private void RewardedAd_OnUserEarnedReward(object sender, Reward e) {
			InvokeOnRewardedSuccess();
		}

		public override void LoadInterstitial() {
			if (this.interstitialAd != null) {
				DoLoadingInterstitial();
			} else {
				pendingActions.Add(DoLoadingInterstitial);
			}
		}

		private void DoLoadingInterstitial() {
			AdRequest request = new AdRequest.Builder().Build();
			this.interstitialAd.LoadAd(request);
		}

		public override void LoadRewardedAds() {
			if (this.rewardedAd != null) {
				DoLoadingRewardedAds();
			} else {
				pendingActions.Add(DoLoadingRewardedAds);
			}
		}

		private void DoLoadingRewardedAds() {
			AdRequest request = new AdRequest.Builder().Build();
			this.rewardedAd.LoadAd(request);
		}

		public override void ShowBanner(BannerPosition position) {
			if (isBannerLoaded) {
				if (position != currentBannerPosition) {
					bannerView.OnPaidEvent -= Ad_OnPaidEvent;
					bannerView.Destroy();
					LoadBanner(position);
				}
			} else {
				LoadBanner(position);
			}
			bannerView.Show();
		}

		public override void ShowInterstitial(string placement) {
			this.interstitialAd.Show();
		}

		public override void ShowRewardedAds(string placement) {
			this.rewardedAd.Show();
		}

		private void LoadBanner(BannerPosition position) {
			this.bannerView = new BannerView(BannerAdUnitId, AdSize.SmartBanner, position == BannerPosition.Bottom ? AdPosition.Bottom : AdPosition.Top);
			this.bannerView.OnPaidEvent += Ad_OnPaidEvent;
			AdRequest request = new AdRequest.Builder().Build();
			this.bannerView.LoadAd(request);
			isBannerLoaded = true;
		}

		private void Ad_OnPaidEvent(object sender, AdValueEventArgs e) {
			AdjustAdRevenue adjustAdRevenue = new AdjustAdRevenue(AdjustConfig.AdjustAdRevenueSourceAdMob);
			adjustAdRevenue.setRevenue(e.AdValue.Value / 1000000.0f, e.AdValue.CurrencyCode);
			Adjust.trackAdRevenue(adjustAdRevenue);
		}

		public override void InitRewardedInterstitialAds() {
			
		}

		public override void LoadRewardedInterstitial() {
			AdRequest request = new AdRequest.Builder().Build();
			RewardedInterstitialAd.LoadAd(RewardedInterstitialAdUnitId, request, RewardedInterstitialAdLoadCallback);
		}

		public override void ShowRewardedInterstitial(string placement) {
			rewardedInterstitialAd.Show(RewardedInterstitialEarnRewardCallback);
		}

		private void RewardedInterstitialAdLoadCallback(RewardedInterstitialAd ad, AdFailedToLoadEventArgs args) {
			if (args == null) {
				rewardedInterstitialAd = ad;
				rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent += RewardedInterstitialAdDisplayFailed;
				rewardedInterstitialAd.OnAdDidDismissFullScreenContent += RewardedInterstitialAdClosed;
				rewardedInterstitialAd.OnPaidEvent += Ad_OnPaidEvent;
				InvokeOnRewardedInterstitialLoaded();
			} else {
				InvokeOnRewardedInterstitialLoadFailed();
			}
		}

		private void RewardedInterstitialAdClosed(object sender, EventArgs e) {
			InvokeOnRewardedInterstitialClosed();
			rewardedInterstitialAd.OnAdFailedToPresentFullScreenContent -= RewardedInterstitialAdDisplayFailed;
			rewardedInterstitialAd.OnAdDidDismissFullScreenContent -= RewardedInterstitialAdClosed;
			rewardedInterstitialAd = null;
		}

		private void RewardedInterstitialAdDisplayFailed(object sender, AdErrorEventArgs e) {
			InvokeOnRewardedInterstitialDisplayFailed();
		}

		private void RewardedInterstitialEarnRewardCallback(Reward reward) {
			InvokeOnRewardedInterstitialSuccess();
		}

	}
}
#endif
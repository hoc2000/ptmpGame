using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace WidogameFoundation.Ads
{

    public enum BannerPosition {
        Top,
        Bottom
    }

    public interface IAdsMediation
    {
        void Init();

        void InitInterstitial();

        bool IsInterstitialAvailable { get; }

        void LoadInterstitial();

        void ShowInterstitial(string placement);

        void InitRewardedAds();

        bool IsRewardedAdsAvailable { get; }

        void LoadRewardedAds();

        void ShowRewardedAds(string placement);

        void ShowBanner(BannerPosition position);

        void HideBanner();
    }
}

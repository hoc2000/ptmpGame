using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WidogameFoundation.Ads;

public abstract class BaseAdsMediation : IAdsMediation {
    private Action _onInterstitialClosed;
    public event Action OnInterstitialClosed {
        add {
            _onInterstitialClosed += value;
        }
        remove {
            _onInterstitialClosed -= value;
        }
    }

    private Action _onInterstitialLoadFailed;
    public event Action OnInterstitialLoadFailed {
        add {
            _onInterstitialLoadFailed += value;
        }
        remove {
            _onInterstitialLoadFailed -= value;
        }
    }

    private Action _onInterstitialDisplayFailed;
    public event Action OnInterstitialDisplayFailed {
        add {
            _onInterstitialDisplayFailed += value;
        }
        remove {
            _onInterstitialDisplayFailed -= value;
        }
    }

    private Action<string> _onInterstitialLoaded;
    public event Action<string> OnInterstitialLoaded {
        add {
            _onInterstitialLoaded += value;
        }
        remove {
            _onInterstitialLoaded -= value;
        }
    }

    private Action _onRewardedClosed;
    public event Action OnRewardedClosed {
        add {
            _onRewardedClosed += value;
        }
        remove {
            _onRewardedClosed -= value;
        }
    }

    private Action _onRewardedDisplayFailed;
    public event Action OnRewardedDisplayFailed {
        add {
            _onRewardedDisplayFailed += value;
        }
        remove {
            _onRewardedDisplayFailed -= value;
        }
    }

    private Action<string> _onRewardedLoaded;
    public event Action<string> OnRewardedLoaded {
        add {
            _onRewardedLoaded += value;
        }
        remove {
            _onRewardedLoaded -= value;
        }
    }

    private Action _onRewardedLoadFailed;
    public event Action OnRewardedLoadFailed {
        add {
            _onRewardedLoadFailed += value;
        }
        remove {
            _onRewardedLoadFailed -= value;
        }
    }

    private Action _onRewarededSuccess;
    public event Action OnRewardedSuccess {
        add {
            _onRewarededSuccess += value;
        }
        remove {
            _onRewarededSuccess -= value;
        }
    }

    private Action _onRewardedInterstitialClosed;
    public event Action OnRewardedInterstitialClosed {
        add {
            _onRewardedInterstitialClosed += value;
        }
        remove {
            _onRewardedInterstitialClosed -= value;
        }
    }

    private Action _onRewardedInterstitialDisplayFailed;
    public event Action OnRewardedInterstitialDisplayFailed {
        add {
            _onRewardedInterstitialDisplayFailed += value;
        }
        remove {
            _onRewardedInterstitialDisplayFailed -= value;
        }
    }

    private Action _onRewardedInterstitialLoadFailed;
    public event Action OnRewardedInterstitialLoadFailed {
        add {
            _onRewardedInterstitialLoadFailed += value;
        }
        remove {
            _onRewardedInterstitialLoadFailed -= value;
        }
    }

    private Action _onRewardedInterstitialLoaded;
    public event Action OnRewardedInterstitialLoaded {
        add {
            _onRewardedInterstitialLoaded += value;
        }
        remove {
            _onRewardedInterstitialLoaded -= value;
        }
    }
    
    private Action _onRewardedInterstitialSuccess;
    public event Action OnRewardedInterstitialSuccess {
        add {
            _onRewardedInterstitialSuccess += value;
        }
        remove {
            _onRewardedInterstitialSuccess -= value;
        }
    }

    private Action _onBannerLoaded;

    public abstract bool IsInterstitialAvailable { get; }
    public abstract bool IsRewardedAdsAvailable { get; }
    public abstract bool IsRewardedInterstitialAvailable { get; }

    public event Action OnBannerLoaded {
        add
        {
            _onBannerLoaded += value;
        }
        remove
        {
            _onBannerLoaded -= value;
        }
    }

    
    protected void InvokeOnInterstitialClosed() {
        InvokeEvent(_onInterstitialClosed);
    }

    protected void InvokeOnInterstitialLoadFailed() {
        InvokeEvent(_onInterstitialLoadFailed);
    }

    protected void InvokeOnInterstitialDisplayFailed() {
        InvokeEvent(_onInterstitialDisplayFailed);
    }

    protected void InvokeOnInterstitialLoaded(string networkName) {
        InvokeEvent(_onInterstitialLoaded, networkName);
    }

    protected void InvokeOnRewardedClosed() {
        InvokeEvent(_onRewardedClosed);
    }

    protected void InvokeOnRewardedDisplayFailed() {
        InvokeEvent(_onRewardedDisplayFailed);
    }

    protected void InvokeOnRewardedLoaded(string networkName) {
        InvokeEvent(_onRewardedLoaded, networkName);
    }

    protected void InvokeOnRewardedLoadFailed() {
        InvokeEvent(_onRewardedLoadFailed);
    }

    protected void InvokeOnRewardedSuccess() {
        InvokeEvent(_onRewarededSuccess);
    }

    protected void InvokeOnBannerLoaded() {
        InvokeEvent(_onBannerLoaded);
    }

    protected void InvokeOnRewardedInterstitialLoaded() {
        InvokeEvent(_onRewardedInterstitialLoaded);
    }

    protected void InvokeOnRewardedInterstitialLoadFailed() {
        InvokeEvent(_onRewardedInterstitialLoadFailed);
    }

    protected void InvokeOnRewardedInterstitialDisplayFailed() {
        InvokeEvent(_onRewardedInterstitialDisplayFailed);
    }

    protected void InvokeOnRewardedInterstitialClosed() {
        InvokeEvent(_onRewardedInterstitialClosed);
    }

    protected void InvokeOnRewardedInterstitialSuccess() {
        InvokeEvent(_onRewardedInterstitialSuccess);
    }

    protected void InvokeEvent(Action evt) {
        evt?.Invoke();
    }

    protected void InvokeEvent<T>(Action<T> evt, T obj) {
        evt?.Invoke(obj);
    }

    protected void InvokeEvent<T1, T2, T3, T4>(Action<T1, T2, T3, T4> evt, T1 obj1, T2 obj2, T3 obj3, T4 obj4) {
        evt?.Invoke(obj1, obj2, obj3, obj4);
    }

    public abstract void Init();
    public abstract void InitInterstitial();
    public abstract void LoadInterstitial();
    public abstract void ShowInterstitial(string placement);
    public abstract void InitRewardedAds();
    public abstract void LoadRewardedAds();
    public abstract void ShowRewardedAds(string placement);
    public abstract void ShowBanner(BannerPosition position);
    public abstract void HideBanner();
    public abstract void InitRewardedInterstitialAds();
    public abstract void LoadRewardedInterstitial();
    public abstract void ShowRewardedInterstitial(string placement);

}

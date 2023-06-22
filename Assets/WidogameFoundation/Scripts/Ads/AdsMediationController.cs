using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;
using WidogameFoundation.Ads;

public class AdsMediationController : MonoBehaviour
{
    private static AdsMediationController _instance;
    public static AdsMediationController Instance {
        get => _instance;
    }
    private BaseAdsMediation adsMediation;

    private int bannerShowingBalance;
    private UnityAction<bool> onIntersititalFinished;
    private int interstitialRetryAttempt;
    private string loadedInterstitialNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;

    private bool gotRewarded;
    private UnityAction onRewardedShowFailed;
    private UnityAction<bool> onRewardedFinished;
    private int rewardedRetryAttempt;
    private string loadedRewardedNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;
    private Action<bool> _onRewardedReady;

    private UnityAction onRewardedInterstitialShowFailed;
    private UnityAction<bool> onRewardedInterstitialFinished;
    private int rewardedInterstitialRetryAttempt;

    private readonly Queue<Action> executionQueue = new Queue<Action>();
    private Thread mainThread;

    public event Action<bool> OnRewarededReady {
        add {
            _onRewardedReady += value;
        }
        remove {
            _onRewardedReady -= value;
        }
    }

    private void Awake()
    {
        _instance = this;
        _instance.Init();
        DontDestroyOnLoad(transform.gameObject);
    }

	private void Start() {
        mainThread = Thread.CurrentThread;
	}

	private void Update() {
        lock (executionQueue) {
            while (executionQueue.Count > 0) {
                executionQueue.Dequeue().Invoke();
            }
        }
	}

    public void Enqueue(Action act) {
        Enqueue(ActionWrapper(act));
    }

    public void Enqueue(IEnumerator action) {
        lock (executionQueue) {
            executionQueue.Enqueue(() => {
                StartCoroutine(action);
            });
        }
    }

    private IEnumerator ActionWrapper(Action act) {
        act();
        yield return null;
    }

    private void Init() {
#if MEDIATION_IRONSOURCE
        adsMediation = new IronSourceAdsMediation();
#elif MEDIATION_MAX
        adsMediation = new MaxAdsMediation();
#elif MEDIATION_ADMOB
        adsMediation = new AdmobAdsMediation();
#endif
        adsMediation.Init();
        adsMediation.OnInterstitialClosed += AdsMediation_OnInterstitialClosed;
        adsMediation.OnInterstitialDisplayFailed += AdsMediation_OnInterstitialDisplayFailed;
        adsMediation.OnInterstitialLoadFailed += AdsMediation_OnInterstitialLoadFailed;
        adsMediation.OnInterstitialLoaded += AdsMediation_OnInterstitialLoaded;

        adsMediation.OnRewardedClosed += AdsMediation_OnRewardedClosed;
        adsMediation.OnRewardedDisplayFailed += AdsMediation_OnRewardedDisplayFailed;
        adsMediation.OnRewardedLoaded += AdsMediation_OnRewardedLoaded;
        adsMediation.OnRewardedLoadFailed += AdsMediation_OnRewardedLoadFailed;
        adsMediation.OnRewardedSuccess += AdsMediation_OnRewardedSuccess;

		adsMediation.OnRewardedInterstitialClosed += AdsMediation_OnRewardedInterstitialClosed;
		adsMediation.OnRewardedInterstitialDisplayFailed += AdsMediation_OnRewardedInterstitialDisplayFailed;
		adsMediation.OnRewardedInterstitialLoaded += AdsMediation_OnRewardedInterstitialLoaded;
		adsMediation.OnRewardedInterstitialLoadFailed += AdsMediation_OnRewardedInterstitialLoadFailed;
		adsMediation.OnRewardedInterstitialSuccess += AdsMediation_OnRewardedInterstitialSuccess;
    }

    private void AdsMediation_OnRewardedInterstitialClosed() {
        LoadRewardedInterstitialAds();
        if (mainThread != Thread.CurrentThread) {
            Enqueue(InvokeRewardedInterstitialClosedWithDelay());
        } else {
            StartCoroutine(InvokeRewardedInterstitialClosedWithDelay());
        }
    }

    private void AdsMediation_OnRewardedInterstitialDisplayFailed() {
        if (mainThread != Thread.CurrentThread) {
            Enqueue(() => onRewardedInterstitialShowFailed?.Invoke());
        } else {
            onRewardedInterstitialShowFailed?.Invoke();
        }
    }

    private void AdsMediation_OnRewardedInterstitialLoaded() {
        rewardedInterstitialRetryAttempt = 0;
    }

    private void AdsMediation_OnRewardedInterstitialLoadFailed() {
        rewardedInterstitialRetryAttempt++;
        int retryDelay = GetRetryDelayForCurrentAttempt(rewardedInterstitialRetryAttempt);
        if (mainThread != Thread.CurrentThread) {
            Enqueue(LoadRewardedInterstitialWithDelay(retryDelay));
        } else {
            StartCoroutine(LoadRewardedInterstitialWithDelay(retryDelay));
        }
	}

    private void AdsMediation_OnRewardedInterstitialSuccess() {
        gotRewarded = true;
    }

    private IEnumerator InvokeRewardedInterstitialClosedWithDelay() {
        yield return null;
        onRewardedInterstitialFinished?.Invoke(gotRewarded);
    }

    private void AdsMediation_OnRewardedSuccess()
    {
        gotRewarded = true;
    }

    private void AdsMediation_OnRewardedLoadFailed()
    {
        rewardedRetryAttempt++;
        int retryDelay = GetRetryDelayForCurrentAttempt(rewardedRetryAttempt);
        if (mainThread != Thread.CurrentThread) {
            Enqueue(IELoadRewardedAdsDelay(retryDelay));
        } else {
            StartCoroutine(IELoadRewardedAdsDelay(retryDelay));
        }
    }

    private void AdsMediation_OnRewardedLoaded(string networkName)
    {
        rewardedRetryAttempt = 0;
        _onRewardedReady?.Invoke(true);
    }

    private void AdsMediation_OnRewardedDisplayFailed()
    {
        loadedRewardedNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;
        LoadRewardedAds();
        if (mainThread != Thread.CurrentThread) {
            Enqueue(() => onRewardedShowFailed?.Invoke());
        } else {
            onRewardedShowFailed?.Invoke();
        }
    }

    private void AdsMediation_OnRewardedClosed()
    {
        loadedRewardedNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;
        LoadRewardedAds();
        if (mainThread != Thread.CurrentThread) {
            Enqueue(InvokeRewardedClosedWithDelay());
        } else {
            StartCoroutine(InvokeRewardedClosedWithDelay());
        }
    }

    private IEnumerator InvokeRewardedClosedWithDelay() {
        yield return null;
        onRewardedFinished?.Invoke(gotRewarded);
        gotRewarded = false;
    }

    private void AdsMediation_OnInterstitialLoaded(string networkName)
    {
        loadedInterstitialNetwork = networkName;
        interstitialRetryAttempt = 0;
    }

    private void AdsMediation_OnInterstitialLoadFailed()
    {
        interstitialRetryAttempt++;
        int retryDelay = GetRetryDelayForCurrentAttempt(interstitialRetryAttempt);
        if (mainThread != Thread.CurrentThread) {
            Enqueue(IELoadInterstitialDelay(retryDelay));
        } else {
            StartCoroutine(IELoadInterstitialDelay(retryDelay));
        }
    }

    private void AdsMediation_OnInterstitialDisplayFailed()
    {
        loadedInterstitialNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;
        LoadInterstitial();
        if (mainThread != Thread.CurrentThread) {
            Enqueue(() => onIntersititalFinished?.Invoke(false));
        } else {
            onIntersititalFinished?.Invoke(false);
        }
    }

    private void AdsMediation_OnInterstitialClosed()
    {
        loadedInterstitialNetwork = WidogameFoundation.WidogameConstants.AD_NETWORK_NAME_NONE;
        LoadInterstitial();
        if (mainThread != Thread.CurrentThread) {
            Enqueue(() => onIntersititalFinished?.Invoke(true));
        } else {
            onIntersititalFinished?.Invoke(true);
        }
    }

    public void ShowBanner(BannerPosition position) {
        bannerShowingBalance++;
        if (bannerShowingBalance > 0) {
            adsMediation.ShowBanner(position);
        }
    }

    public void HideBanner() {
        bannerShowingBalance--;
        adsMediation.HideBanner();
    }

    public bool IsInterstitialAvailable {
        get {
#if UNITY_EDITOR
            return true;
#else
            return adsMediation.IsInterstitialAvailable;
#endif
        }
    }

    public void InitInterstitial() {
        adsMediation.InitInterstitial();
        LoadInterstitial();
    }

    public void LoadInterstitial() {
        adsMediation.LoadInterstitial();
    }

    private IEnumerator IELoadInterstitialDelay(int seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        LoadInterstitial();
    }

    public void ShowInterstitial(string placement, UnityAction<bool> onFinished) {
#if UNITY_EDITOR
        onIntersititalFinished = onFinished;
        onIntersititalFinished?.Invoke(true);
#endif
        onIntersititalFinished = onFinished;
#if !ENV_CREATIVE
        adsMediation.ShowInterstitial(placement);
#endif
    }

    public bool IsRewardedAvailable {
        get {
#if UNITY_EDITOR
            return true;
#else
            return adsMediation.IsRewardedAdsAvailable;
#endif
        }
    }

    public void InitRewardedAds() {
        adsMediation.InitRewardedAds();
        LoadRewardedAds();
    }

    public void LoadRewardedAds() {
        adsMediation.LoadRewardedAds();
    }

    private IEnumerator IELoadRewardedAdsDelay(int seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        LoadRewardedAds();
    }

    public void ShowRewardedAds(string placement, UnityAction onRewardedShowFailed, UnityAction<bool> onFinished) {
#if UNITY_EDITOR
        onRewardedFinished = onFinished;
        onRewardedFinished?.Invoke(true);
#endif
        gotRewarded = false;
        this.onRewardedShowFailed = onRewardedShowFailed;
        onRewardedFinished = onFinished;
#if !ENV_CREATIVE
        adsMediation.ShowRewardedAds(placement);
#endif
    }

    public string LoadedInterstitialNetwork {
        get {
            return loadedInterstitialNetwork;
        }
    }

    public string LoadedRewardedNetwork {
        get {
            return loadedRewardedNetwork;
        }
    }

    public void InitRewardedInterstitialAds() {
        adsMediation.InitRewardedInterstitialAds();
        LoadRewardedInterstitialAds();
    }

    public void LoadRewardedInterstitialAds() {
        adsMediation.LoadRewardedInterstitial();
    }

    public bool IsRewardedInterstitialAvailable {
        get {
            return adsMediation.IsRewardedInterstitialAvailable;
        }
    }

    public void ShowRewardedInterstitial(string placement, UnityAction onShowFailed, UnityAction<bool> onFinished) {
        gotRewarded = false;
        this.onRewardedInterstitialShowFailed = onShowFailed;
        this.onRewardedInterstitialFinished = onFinished;
        adsMediation.ShowRewardedInterstitial(placement);
    }

    private IEnumerator LoadRewardedInterstitialWithDelay(int seconds) {
        yield return new WaitForSecondsRealtime(seconds);
        adsMediation.LoadRewardedInterstitial();
    }

    private int GetRetryDelayForCurrentAttempt(int attempt) {
        int retryDelay = Power(2, Math.Min(5, attempt));
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            retryDelay = 10;
        }
        return retryDelay;
    }

    private int Power(int baseNumber, int exponent)
    {
        int ret = 1;
        for (int i = 0; i < exponent; i++)
        {
            ret *= baseNumber;
        }
        return ret;
    }

}

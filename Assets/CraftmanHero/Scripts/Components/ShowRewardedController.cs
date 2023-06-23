using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class ShowRewardedController : MonoBehaviour
{
    public static bool isWatchingRewarded;
    public string scenarios;
    public string placement;
    [SerializeField]
    private bool logNoVideoOnceTime;
    [SerializeField]
    private UnityEvent onRewardedAdsNotAvailable;
    [SerializeField]
    private UnityEvent onRewardedAdsStart;
    [SerializeField]
    private UnityEvent onRewardedAdsFailedToShow;
    [SerializeField]
    private OnRewardedClosedEvent onClosed;
    private bool noAdsLogged;
    public bool isShowAds;

    private float lastTimescale;

    public void Show()
    {
        GameAnalytics.LogButtonClick(scenarios, placement);
        if (Application.internetReachability != NetworkReachability.NotReachable && AdsMediationController.Instance.IsRewardedAvailable)
        {
            AudioHelper.Instance.MuteSound();
            AudioHelper.Instance.MutePlaylist();
            isWatchingRewarded = true;
            lastTimescale = Time.timeScale;
            Time.timeScale = 0;
            Gamedata.showAds = true;
            Gamedata.TotalWatchedReward++;
            SDKPlayPrefs.SetDateTime(StringConstants.PREF_INTERSTITIAL_LAST_SHOWN, DateTime.Now);
            AdsMediationController.Instance.ShowRewardedAds(placement, OnRewardedAdsFailedToShow, OnRewardedAdsClosed);
            GameAnalytics.LogWatchRewardAds(scenarios, true);
            onRewardedAdsStart?.Invoke();
        }
        else
        {
            if (!logNoVideoOnceTime || !noAdsLogged)
            {
                GameAnalytics.LogWatchRewardAds(placement, AdsMediationController.Instance.IsRewardedAvailable);
                noAdsLogged = true;
                isShowAds = false;

            }
            onRewardedAdsNotAvailable?.Invoke();
            isShowAds = false; ;
        }
    }

    private void OnRewardedAdsClosed(bool success)
    {
        GameAnalytics.LogWatchRewardAdsDone(scenarios, true, success.ToString());
        AudioHelper.Instance.UnmuteSound();
        AudioHelper.Instance.UnmutePlaylist();
        Time.timeScale = lastTimescale;
        isWatchingRewarded = false;
        onClosed?.Invoke(success);
        StartCoroutine(MarkShowAdsFalseDelay());
    }


    private void OnRewardedAdsFailedToShow()
    {
        GameAnalytics.LogWatchRewardAdsDone(scenarios, true, "can't_show");
        AudioHelper.Instance.UnmuteSound();
        AudioHelper.Instance.UnmutePlaylist();
        Time.timeScale = lastTimescale;
        isWatchingRewarded = false;
        onRewardedAdsFailedToShow?.Invoke();
        StartCoroutine(MarkShowAdsFalseDelay());
    }

    private IEnumerator MarkShowAdsFalseDelay()
    {
        yield return null;
        yield return null;
        Gamedata.showAds = false;
    }

    public void ResetNoAdsLog()
    {
        noAdsLogged = false;
    }

}

[Serializable]
public class OnRewardedClosedEvent : UnityEvent<bool>
{

}

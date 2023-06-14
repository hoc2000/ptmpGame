using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    [SerializeField] Image imgFill;
    [SerializeField] float timeLoading;
    [SerializeField] Image loadingtxt;
    private IEnumerator Start()
    {
        float waitedTime = 0.0f;
        while (waitedTime < timeLoading)
        {
            waitedTime += Time.deltaTime;
            float progress = (waitedTime / timeLoading);
            imgFill.fillAmount = progress;

            yield return null;
        }
        //loadingtxt.gameObject.SetActive(false);
        StartCoroutine(LoadSceneAsync());

        //sliderLoading.DOValue(1f, timeLoading).OnComplete(() => {

        //    //LoadSceceManager.Instance.LoadHome();
        //    StartCoroutine(LoadSceneAsync());
        //});
    }

    private IEnumerator LoadSceneAsync()
    {
        float startTime = Time.realtimeSinceStartup;
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(1);
        asyncOperation.allowSceneActivation = false;
        WDDebug.Log("start load scene");
        while (asyncOperation.progress < 0.9f || (!AppOpenAdManager.Instance.IsAdAvailable && Time.realtimeSinceStartup - startTime < 2f))
        {
            yield return null;
        }
        WDDebug.Log("finish load scene");
        bool isAdsAvailable = AppOpenAdManager.Instance.IsAdAvailable;
        AppOpenAdManager.Instance.ShowAdIfAvailable(() => asyncOperation.allowSceneActivation = true);
        GameAnalytics.AppOpenAdsImpression("loading_game", isAdsAvailable, Application.internetReachability != NetworkReachability.NotReachable);
    }
}

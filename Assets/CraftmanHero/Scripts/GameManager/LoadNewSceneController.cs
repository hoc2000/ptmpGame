using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadNewSceneController : MonoBehaviour
{

    [SerializeField]
    private Image imgLoadingHandle;
    [SerializeField]
    private Image imgFill;
    [SerializeField]
    private Image imgBackground;
   

    private long startLoadTimestamp;
    public void LoadScene( int buildIndex)
    {
        startLoadTimestamp = DateTimeOffset.Now.ToUnixTimeSeconds();
        this.gameObject.SetActive(true);
        StartCoroutine(IELoadScene(buildIndex));
    }
    private IEnumerator IELoadScene(int buildIndex)
    {
        Time.timeScale = 1;
        AsyncOperation ops = SceneManager.LoadSceneAsync(buildIndex);
        ops.allowSceneActivation = false;
        float waitedTime = 0.0f;

        float minWaitingSeconds = 1.0f;
        while (!ops.isDone || waitedTime < minWaitingSeconds)
        {
            waitedTime += Time.deltaTime;
            if (waitedTime >= minWaitingSeconds)
            {
                ops.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}

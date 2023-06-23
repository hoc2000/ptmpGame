using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogButtonClick : MonoBehaviour
{
    [SerializeField] string btnName;
    [SerializeField] string screenName;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(LogBtnClick);
    }

    private void LogBtnClick()
    {
        GameAnalytics.LogButtonClick(btnName, screenName);
    }
}

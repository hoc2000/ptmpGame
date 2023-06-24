using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Battlehub.Dispatcher;
using TMPro;

public class ToastMgr : SingletonMonoBehaviour<ToastMgr>
{
    [SerializeField]
    TextMeshProUGUI toastText;
    [SerializeField]
    GameObject panel;
    [SerializeField] GameObject fadeObj;

    Action<bool> callBackSponsor;
    Action<bool> fadeTrue;
    public void Show(string mess, Action callBack = null)
    {
        Dispatcher.Current.BeginInvoke(() =>
        {
            panel.SetActive(true);
            toastText.text = mess;
        });
    }

    public void ShowFade(Action<bool> fade = null)
    {
        fadeTrue = fade;
        fadeObj.SetActive(true);
    }
    public void CloseFade()
    {
        fadeObj.SetActive(false);
    }
    public void FadeTrue()
    {
        fadeTrue(true);
    }
}

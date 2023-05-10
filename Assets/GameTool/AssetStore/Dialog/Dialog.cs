using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : SingletonResource<Dialog>
{
    [SerializeField]
    Text tileText, messageText, noText, yesText;

    [SerializeField]
    GameObject   yesObj, noObj;


    Action okAction, cancelAction;

    public void OpenDialog(string title, string message, string yes, Action okAction)
    {
        //dialogPanel.SetActive(true);
        tileText.text = title;
        messageText.text = message;
        this.okAction = okAction;
        yesText.text = yes;
        yesObj.SetActive(true);
        noObj.SetActive(false);
      //  okObj.SetActive(true);

    }

    public void OpenDialog(string title, string message, string no, string yes, Action okAction, Action cancelAction = null)
    {
        //dialogPanel.SetActive(true);
        tileText.text = title;
        messageText.text = message;
        noText.text = no;
        yesText.text = yes;

        this.okAction = okAction;
        this.cancelAction = cancelAction;
        yesObj.SetActive(true);
        noObj.SetActive(true);
     //   okObj.SetActive(false);
    }

    public void OnPressOkButton()
    {
        Destroy(gameObject);
        if (okAction != null)
            okAction.Invoke();

    }

    public void OnPressCancelButton()
    {
        Destroy(gameObject);
        //  dialogPanel.SetActive(false);
        if (cancelAction != null)
            cancelAction.Invoke();

    }
    public void OnPressCloseButton()
    {
        Destroy(gameObject);
        //dialogPanel.SetActive(false);
        if (cancelAction != null)
            cancelAction.Invoke();
    }
}

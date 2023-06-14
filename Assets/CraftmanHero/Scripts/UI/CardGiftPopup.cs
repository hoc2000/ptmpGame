using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;

public class CardGiftPopup : BaseUIMenu
{
    [SerializeField] Button closeButton;
    void Start()
    {
        closeButton.onClick.AddListener(() => CloseClick());
    }

    void CloseClick()
    {

        this.Pop();
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;

public class BuyVipPopup : BaseUIMenu
{

    [Header("BUTTON")]
    [SerializeField] Button closeButton;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI lifeText;
    private void OnEnable()
    {
        Init();
    }
    private void Start()
    {
        closeButton.onClick.AddListener(() => CloseClick());
    }
    void Init()
    {
        
    }
    // Update is called once per frame
    void CloseClick()
    {
        

    }
}

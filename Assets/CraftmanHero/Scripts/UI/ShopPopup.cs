using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;

public class ShopPopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] Button closeThankButton;
    [SerializeField] Button buyVIPButton;
    [SerializeField] Button buyCoinButton;
    [SerializeField] Button buyLifeButton;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI vipText;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI removeText;

    [Header("POPUP")]
    [SerializeField] GameObject thankPopup;
    private void OnEnable()
    {
        Init();
        this.RegisterListener(EventID.BuyIap, (sender, param) => ShowPopupThank());
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.BuyIap, (sender, param) => ShowPopupThank());
    }
    void Start()
    {
        AddEventButton();
    }

    #region INIT
    void Init()
    {
        GameAnalytics.LogUIAppear("popup_shop", "HomeScene");
        coinText.text = GameConfig.Instance.valueConfrig.coinIap.ToString() + " " + "COINS";
        lifeText.text = GameConfig.Instance.valueConfrig.lifeIap.ToString() + " " + "COINS";

    }
    void AddEventButton()
    {
        closeButton.onClick.AddListener(() => CloseClick());
        closeThankButton.onClick.AddListener(() => ClosePopupThank());
        buyVIPButton.onClick.AddListener(() => BuyVipClick());
    }
    #endregion
    #region BUTTON
    void CloseClick()
    {
        this.Pop();
    }
    void ClosePopupThank()
    {
        thankPopup.SetActive(false);
    }
    void BuyVipClick()
    {
        CanvasManager.Push(GlobalInfo.BuyVipPopup, null);
    }
    void ShowPopupThank()
    {
        thankPopup.SetActive(true);
    }
    #endregion
}

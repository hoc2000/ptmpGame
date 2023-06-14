using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using System.Drawing;
using My.Tool;
public class HomePanel : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button settingButton;
    [SerializeField] Button dailyRewardButton;
    [SerializeField] Button spinButton;
    [SerializeField] Button cardButton;
    [SerializeField] Button vipButton;
    [SerializeField] Button shopButton;
    [SerializeField] Button skinButton;
    [SerializeField] Button selectLevelButton;
    [SerializeField] Button playButton;
    [SerializeField] Button coinButton;
    [SerializeField] Button playLevelButton;
    [SerializeField] Button doubleCoinButton;
    [SerializeField] Button upgradeCoinButton;
    [SerializeField] Button upgradeAdsButton;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI doubleCoinText;
    [SerializeField] TextMeshProUGUI levelButtonText;
    [SerializeField] TextMeshProUGUI levelButtonPlayText;
    [SerializeField] TextMeshProUGUI levelWinText;
    [SerializeField] TextMeshProUGUI coinWinText;
    [SerializeField] TextMeshProUGUI coinRewardText;
    [SerializeField] TextMeshProUGUI lifeRewardText;
    [SerializeField] TextMeshProUGUI lifeGetText;
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI atkTextPlus;
    [SerializeField] TextMeshProUGUI hpTextPlus;
    [SerializeField] TextMeshProUGUI levelUpgradeText;
    [SerializeField] TextMeshProUGUI coinUpgradeText;

    //[Header("NOTI")]
    //[SerializeField] GameObject notiDailly;
    //[SerializeField] GameObject notiSpin;
    //[SerializeField] GameObject notiCard;
    //[SerializeField] GameObject notiSkin;


    [Header("GAMEOBJECT")]
    [SerializeField] GameObject winPanel;
    [Header("Transform")]
    [SerializeField] Transform coinTarget;
    [SerializeField] Transform lifeTarget;


    public GameObject notVideo;

    [SerializeField] private ShowRewardedController showRewardedController;

    public bool doubleCoin;
        
    private void OnEnable()
    {
        Init();
        Gamedata.isInGame = true;
        Gamedata.levelPlayedTime = 0;
        Gamedata.getWeapon = false;
        Gamedata.getHealItem = false;
        if (showRewardedController != null)
        {
            showRewardedController.ResetNoAdsLog();
        }
    }
    private void OnDisable()
    {
        Gamedata.isInGame = false;

    }
    void Start()
    {
        AddEventButton();
        AdsMediationController.Instance.InitInterstitial();
    }

    #region INIT
    void Init()
    {
       
        levelButtonPlayText.text = "Level " + Gamedata.I.CurrentLevel;
        levelButtonText.text = "Level " + Gamedata.I.CurrentLevel;

       

        UpdateTextData();

        if (GameManager.isWin)
        {
            HomeSceneManager.Instance.WinAnimUI();
            GameManager.isWin = false;
            Gamedata.I.Coin += GameManager.coinCollect;
            doubleCoinText.text = "+" + GameManager.coinCollect;
            winPanel.SetActive(true);
            playButton.gameObject.SetActive(false);
            selectLevelButton.gameObject.SetActive(false);
            playLevelButton.gameObject.SetActive(false);
            doubleCoinButton.gameObject.SetActive(true);

            HomeSceneManager.Instance.confetti.SetActive(true);
            Invoke("ShowFxCoin", 0.3f);
            Invoke("ShowNextLevelButton", 3f);
        }
        else
        {
            winPanel.SetActive(false);
            playButton.gameObject.SetActive(true);
            selectLevelButton.gameObject.SetActive(true);
            playLevelButton.gameObject.SetActive(false);
            doubleCoinButton.gameObject.SetActive(false);
        }

        if (Gamedata.I.CurrentLevel > 30)
        {
            playButton.gameObject.SetActive(false);
            selectLevelButton.gameObject.SetActive(true);
            playLevelButton.gameObject.SetActive(false);
            doubleCoinButton.gameObject.SetActive(false);
        }
    }
    void AddEventButton()
    {
        settingButton.onClick.AddListener(() => SettingClick());
        dailyRewardButton.onClick.AddListener(() => DailyRewardClick());
        spinButton.onClick.AddListener(() => SpinClick());
        cardButton.onClick.AddListener(() => CardClick());
        vipButton.onClick.AddListener(() => VIPClick());
        shopButton.onClick.AddListener(() => ShopClick());
        skinButton.onClick.AddListener(() => SkinClick());
        selectLevelButton.onClick.AddListener(() => SellectLevelClick());
        playButton.onClick.AddListener(() => PlayClick());
        playLevelButton.onClick.AddListener(() => PlayClick());
        upgradeCoinButton.onClick.AddListener(() => UpgradeCoinClick());
        //upgradeAdsButton.onClick.AddListener(() => UpgradeAdsClick());
        //doubleCoinButton.onClick.AddListener(() => DoubleCoinClick());
    }

    void ShowNextLevelButton()
    {
        if (Gamedata.I.CurrentLevel > 30)
        {
            return;
        }

        playLevelButton.gameObject.SetActive(true);
    }

    void ShowFxCoin()
    {
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
    }
    #endregion
    #region BUTTON EVENT
    public void SettingClick()
    {
        CanvasManager.Push(GlobalInfo.SettingPopup, null);
    }
    public void DailyRewardClick()
    {
        CanvasManager.Push(GlobalInfo.DailyRewardPopup, null);
    }
    public void SpinClick()
    {
        CanvasManager.Push(GlobalInfo.SpinPopup, null);
    }
    public void CardClick()
    {
        CanvasManager.Push(GlobalInfo.CardGiftPopup, null);
    }

    public void VIPClick()
    {
        CanvasManager.Push(GlobalInfo.BuyVipPopup, null);
    }

    public void ShopClick()
    {
        CanvasManager.Push(GlobalInfo.ShopPopup, null);
    }
    public void SkinClick()
    {
        CanvasManager.Push(GlobalInfo.SkinPopup, null);
    }
    public void SellectLevelClick()
    {
        CanvasManager.Push(GlobalInfo.SellectLevelPopup, null);
    }
    public void PlayClick()
    {
        GameManager.levelSelected = Gamedata.I.CurrentLevel;
        GameManager.startFrom = "home_main";
        LoadSceceManager.Instance.LoadLevel();
    }
    public void CoinClick()
    {
        CanvasManager.Push(GlobalInfo.ShopPopup, null);
    }
    public void LifeClick()
    {
        CanvasManager.Push(GlobalInfo.ShopPopup, null);

    }
    public void DoubleCoinClick()
    {
        Gamedata.I.Coin += GameManager.coinCollect;
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);

        doubleCoinButton.gameObject.SetActive(false);

    }
    public void UpgradeCoinClick()
    {
        GameAnalytics.LogUpgradePowerLevel(Gamedata.I.LevelUpgrade, "coin", GameManager.Instance.upgradePlayerData.CoinUpGrade(), "home_main");
        Gamedata.I.Coin -= GameManager.Instance.upgradePlayerData.CoinUpGrade();
        UpgradeData();
        this.PostEvent(EventID.UpdateData);
    }
    public void UpgradeAdsClick()
    {
        UpgradeData();
    }
    #endregion 

    #region UpdateData
    void UpgradeData()
    {
        GameManager.Instance.upgradePlayerData.UpgradeData();

        UpdateTextData();
    }
    
    void UpdateTextData()
    {
        bool haveCoinUpgrade = Gamedata.I.Coin >= GameManager.Instance.upgradePlayerData.CoinUpGrade();

        upgradeAdsButton.gameObject.SetActive(!haveCoinUpgrade);
        upgradeCoinButton.gameObject.SetActive(haveCoinUpgrade);

        levelUpgradeText.text = Gamedata.I.LevelUpgrade.ToString();
        coinUpgradeText.text = GameManager.Instance.upgradePlayerData.CoinUpGrade().ToString();
        atkText.text = "ATK : " + Gamedata.I.AtkPlayer;
        atkTextPlus.text = "+" +  GameManager.Instance.upgradePlayerData.AtkData().ToString();
        hpText.text = "HP : " + Gamedata.I.HpPlayer;
        hpTextPlus.text = "+" + GameManager.Instance.upgradePlayerData.HpData().ToString();
        levelWinText.text = "LEVEL" + " " + (GameManager.levelSelected)  + " " + "VICTORY";
    }
    #endregion

    private void ShowFail()
    {
        notVideo.SetActive(true);
    }

    public void OnRewardedNotAvailable()
    {
        ShowFail();
    }
    public void OnRewardedClosed(bool success)
    {
        if (success)
        {
            Time.timeScale = 1;       
            UpgradeAdsClick();
            GameAnalytics.LogUpgradePowerLevel(Gamedata.I.LevelUpgrade, "watch_ads", 0, "home_main");
        }
      
    }

    public void OnRewardClosedClaimCoin(bool success)
    {
        if (success)
        {
            DoubleCoinClick();


        }
    }
}

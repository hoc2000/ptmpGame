using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using Spine.Unity;

public class SkinPopup : BaseUIMenu
{
    public static SkinPopup instance;
    [SerializeField]
    public Item resourceItem;

    [SerializeField]
    public ResourceType currencyType;
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField]
    private Button btnTryFree;
    [SerializeField] private Button getAds;
    [SerializeField]
    private Button btnBuyItem;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI coinRandomUnlock;
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI unlockInform;


    [SerializeField]
    private TabController tabController;

    [Header("G")]
    [SerializeField] private GameObject btnGroupBuy_AdsPremium;
    [SerializeField] private GameObject btnGroupRescue;
    [SerializeField] private GameObject btnUnlockBuyCoin;
    [SerializeField] private GameObject notVideo;

    [SerializeField]
     public List<SkinItem> skinItems = new List<SkinItem>();
    public List<PremiumSkinCharacter> premiumCharacters = new List<PremiumSkinCharacter>();
    public List<RescueCharacter> rescueCharacters = new List<RescueCharacter>();

    public bool buySuccesful;

    [SerializeField]
    private SkeletonGraphic skeletonGraphic;
    private ShowRewardedController showRewardedController;

    private void Awake()
    {
        instance = this;
        
    }
    private void OnEnable()
    {
        Init();
    }
    void Start()
    {
        AddEventButton();

        Gamedata.selectedCharacterChanged.AddListener(OnSelectedCharacterChanged);
        Gamedata.resourceDetailChanged.AddListener(OnCharacterOwnStatusChanged);
    }

    // Update is called once per frame
    
   
    #region INIT
    void Init()
    {
        GameAnalytics.LogUIAppear("popup_skin", "HomeScene");
    }
    void AddEventButton()
    {
        closeButton.onClick.AddListener(() => CloseClick());
        //btnBuyItem.onClick.AddListener(() => BuySkinClick());
    }
    
    private void OnCharacterOwnStatusChanged(ResourceDetail resourceDetail)
    {

        if (resourceItem.detail == resourceDetail)
        {
            UpdateUseButtonDisplay(resourceItem);
        }
    }


    private void OnSelectedCharacterChanged()
    {

        if (Gamedata.SelectedCharacter != resourceItem.detail)
        {
            UpdateUseButtonDisplay(resourceItem);
        }
    }
    #endregion

    #region BUTTON


    void CloseClick()
    {
        this.Pop();
    }
    void CoinAdsClick()
    {

    }
    void TrySkinClick()
    {
        if (resourceItem.price == 0)
        {
            Gamedata.TemporarySelectedCharacter = resourceItem.detail;
            Gamedata.TryFreeCharacter = resourceItem.detail;
        }
        GameManager.levelSelected = Gamedata.I.CurrentLevel;
        LoadSceceManager.Instance.LoadLevel();
    }

    public void ClosePopup()
    {
        this.Pop();
    }
    #endregion
    public void SellectSkin(Item item, bool premiumCharacter = false, bool rescueCharacter = false, bool coinskin = false)
    {

        UpdateUseButtonDisplay(item,premiumCharacter,rescueCharacter,coinskin);
    }

  

    private void UpdateUseButtonDisplay(Item resourceItem, bool premiumCharacter = false, bool rescueCharacter = false, bool coinskin = false)
    {
       
            coinRandomUnlock.text = resourceItem.price.ToString();
            bool hasCharacter = Gamedata.HasCharacter(resourceItem.detail);
            bool isSelected = Gamedata.SelectedCharacter == resourceItem.detail;
            if (premiumCharacter)
            {
                if (hasCharacter)
                {
                btnGroupBuy_AdsPremium.SetActive(false);
                btnGroupRescue.SetActive(false);
                btnUnlockBuyCoin.SetActive(false);
                Gamedata.TemporarySelectedCharacter = ResourceDetail.None;

            }
            else {
                btnGroupBuy_AdsPremium.SetActive(true);
                btnGroupRescue.SetActive(false);
                btnUnlockBuyCoin.SetActive(false);
                }
               

            }
            else if (rescueCharacter)
            {
                btnGroupBuy_AdsPremium.SetActive(false);
                btnUnlockBuyCoin.SetActive(false);
                btnGroupRescue.SetActive(true);
                
                
                if ( Gamedata.I.CurrentLevel >= resourceItem.level)
                {
                    Debug.Log("unlock" + Gamedata.I.CurrentLevel + " " + resourceItem.level + " " + resourceItem.detail + resourceItem.type);
                    getAds.gameObject.SetActive(true);
                    btnTryFree.gameObject.SetActive(false);

                }
                else
                {
                    Debug.Log("not unlock");
                    getAds.gameObject.SetActive(false);
                    btnTryFree.gameObject.SetActive(true);

                }
            }
            else if(coinskin)
            {
            btnGroupBuy_AdsPremium.SetActive(false);
            btnGroupRescue.SetActive(false);
            btnUnlockBuyCoin.SetActive(true);
            }
         
        
    }
    public void ChangeSkin(Item resourceItem, bool premiumCharacter = false, bool rescueCharacter = false, bool coinskin  = false)
    {

        this.resourceItem = resourceItem;
        Debug.Log(resourceItem.detail);
        CharacterInfo characterInfo =  GameManager.Instance.dataHolder.characters[resourceItem.detail];
        skeletonGraphic.initialSkinName = characterInfo.skinName;
        skeletonGraphic.Initialize(true);
        UpdateUseButtonDisplay(resourceItem, premiumCharacter, rescueCharacter,coinskin);
    }
    public void BuyCharacterClick()
    {
        if (Gamedata.SubtractCurrency(currencyType, resourceItem.price))
        {
            Gamedata.ClaimCharacter(resourceItem.detail);
            buySuccesful = true;
        }
    }

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
            TrySkinClick();
        }
    }  
}

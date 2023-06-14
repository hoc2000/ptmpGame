using DG.Tweening;
//using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpinPopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] Button spinButton;
    [SerializeField] Button spinAdsButton;
    [SerializeField] Button collectButton;
    [SerializeField] Button collectX2Button;
    [SerializeField] Button[] claimBonusReward;


    [Header("OBJECT")]
    [Space(10)]
    [SerializeField] Transform rotateRoot;
    [SerializeField] GameObject rewardPopup;
    [SerializeField] GameObject rayTop;
    [SerializeField] GameObject adsIcon;
    [SerializeField] GameObject freeText;
    [SerializeField] GameObject countTimeSpin;


    [Header("TEXT")]
    [Space(10)]
    [SerializeField] TextMeshProUGUI[] valueText;
    [SerializeField] TextMeshProUGUI collectText;
    [SerializeField] TextMeshProUGUI txtTimer;
    [SerializeField] TextMeshProUGUI sliderText;


    [Header("IMAGE")]
    [Space(10)]
    [SerializeField] Image collectIcon;

    [SerializeField] Sprite lifeSprite;
    [SerializeField] Sprite coinSprite;
    [SerializeField] Slider slider;

    [Header("IMAGE")]
    [SerializeField] GameObject timeRemain;

    int today = 0;
    public bool isCanSpinFree;
    public bool freeSpin;
    long totalTime = 86400;
    Coroutine couritine;
    public GameObject notVideo;

    [SerializeField] SpinRewardItem[] spinRewardItems;

    public bool doubleReward;
    [HideInInspector]
    public long timeOffset = 0;

    int coinCollected;
    private void OnEnable()
    {
        today = Now().Day;
        Init();
        //if (OneDay() || Gamedata.GetTotalResource(ResourceType.FreeSpinTicket) != 0)
        //{
        //    SetFreeSpin();
        //}
        if (isCanSpinFree && freeSpin)
        {
            isCanSpinFree = false;
        }
        else
        {
            OneDay();
        }
    }
    private void OnDisable()
    {
        StopCoroutine(couritine);

    }
    void Start()
    {
        AddEventButton();
    }


    #region INIT
    void Init()
    {
        GameAnalytics.LogUIAppear("popup_spin", "HomeScene");
        rewardPopup.SetActive(false);
        rayTop.SetActive(false);
        SetText();
        if ( OneDay() || isCanSpinFree)
        {
            SetFreeSpin();
            spinButton.gameObject.SetActive(true);
            spinAdsButton.gameObject.SetActive(false);
            timeRemain.SetActive(false);
        }
        else
        {
            StartCoroutine(CountDownTime());
            spinButton.gameObject.SetActive(false);
            spinAdsButton.gameObject.SetActive(true);
            timeRemain.SetActive(true);
            countTimeSpin.SetActive(true);
        }
        CountSpinLucky(false);
       
    }
    void AddEventButton()
    {
        closeButton.onClick.AddListener(() => CloseClick());
        spinButton.onClick.AddListener(() => SpinClick());
        //spinAdsButton.onClick.AddListener(() => SpinAds());
        //collectButton.onClick.AddListener(() => CollectClick());
        //collectX2Button.onClick.AddListener(() => CollectX2Click());
        
    }
    
    public bool OneDay()
    {
        TimeSpan timeSpan = Now() - Gamedata.LastSpinFree;
        if (timeSpan.TotalDays >= 0)
        {
            isCanSpinFree = true;
            Gamedata.LastSpinFree = DateTime.Now.AddDays(1);
            return true;
        }else
        {
            TimeSpan time = Gamedata.LastSpinFree - Now();
            totalTime = (long)time.TotalSeconds;
            return false;
        }
    }

    public DateTime Now()
    {
        return DateTime.Now.AddSeconds(-1.0f * timeOffset);
    }
    void SetText()
    {
        valueText[0].text = GameConfig.Instance.valueConfrig.spinValue1.ToString();
        valueText[7].text = GameConfig.Instance.valueConfrig.spinValue2.ToString();
        valueText[6].text = GameConfig.Instance.valueConfrig.spinValue3.ToString();
        valueText[5].text = GameConfig.Instance.valueConfrig.spinValue4.ToString();
        valueText[4].text = GameConfig.Instance.valueConfrig.spinValue5.ToString();
        valueText[3].text = GameConfig.Instance.valueConfrig.spinValue6.ToString();
        valueText[2].text = GameConfig.Instance.valueConfrig.spinValue7.ToString();
        valueText[1].text = GameConfig.Instance.valueConfrig.spinValue8.ToString();
        
    }
  
    #endregion

    #region ROTATE
    void SpinRotate()
    {
        Gamedata.CountSpinLucky++;
        rayTop.SetActive(true);
      
        float valueSpin = UnityEngine.Random.Range(-742.5f, -1057.5f);

        Vector3 endRotate = new Vector3(0f, 0f, valueSpin);
        rotateRoot.DOLocalRotate(endRotate, 5f, RotateMode.FastBeyond360).OnComplete(() =>
        {
            SetDataValue(valueSpin);
            
        });
        SwapGemToWeapon();

      
        if (isCanSpinFree)
        {
            isCanSpinFree = false;
            StartCoroutine(Helper.StartAction(() =>
            {
                StartCoroutine(CountDownTime());
                spinButton.gameObject.SetActive(false);
                spinAdsButton.gameObject.SetActive(true);
                timeRemain.SetActive(true);
                countTimeSpin.SetActive(true);
            }, 7f));
            
        }

    }

    void SetDataValue(float value)
    {
        if (value >= -742.5f)
        {
            collectIcon.sprite = spinRewardItems[0].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[0].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -742.5f && value >= -787.5f)
        {
            collectIcon.sprite = spinRewardItems[1].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[1].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -787.5f && value >= -832.5f)
        {
            collectIcon.sprite = spinRewardItems[2].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[2].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -832.5f && value >= -877.5f)
        {
            collectIcon.sprite = spinRewardItems[3].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[3].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -877.5f && value >= -922.5f)
        {
            collectIcon.sprite = spinRewardItems[4].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[4].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -922.5f && value >= -967.5f)
        {
            collectIcon.sprite = spinRewardItems[5].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[5].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -967.5f && value >= -1012.5f)
        {
            collectIcon.sprite = spinRewardItems[6].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[6].resourceItems[0].resourceItem.quantity.ToString();

        }
        else if (value < -1012.5f && value >= -1057.5f)
        {
            collectIcon.sprite = spinRewardItems[7].resourceItems[0].resourceItem.spriteUI;
            collectText.text = spinRewardItems[7].resourceItems[0].resourceItem.quantity.ToString();

        }
        Invoke("ShowReward", 0.5f);
        FinishSpin(value);
        CountSpinLucky(true);

    }
    #endregion
    #region BUTTOON
    void CloseClick()
    {
        if (SceneManager.GetActiveScene().buildIndex == Constants.SCENE.SCENE_HOME)
        {
            this.Pop();
        }
        else
        {
            LoadSceceManager.Instance.LoadHome();
        }
    }
    void SpinClick()
    {
        freeSpin = true;
        SpinRotate();

    }
    //void CollectClick()
    //{

    //    //this.PostEvent(EventID.UpdateData);
    //    rewardPopup.SetActive(false);
    //    CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
    //}
    //void CollectX2Click()
    //{
    //    //this.PostEvent(EventID.UpdateData);
    //    CanvasManager.Push(GlobalInfo.ColectFxPopup,null);
    //    rewardPopup.SetActive(false);
    //}
    #endregion

    public void FinishSpin(float value)
    {

        if (value >= -742.5f)
        {
            
                coinCollected = GameConfig.Instance.valueConfrig.spinValue1;
        }
        else if (value < -742.5f && value >= -787.5f)
        {

          
                coinCollected = GameConfig.Instance.valueConfrig.spinValue2;
        }
        else if (value < -787.5f && value >= -832.5f)
        {
        
                coinCollected = GameConfig.Instance.valueConfrig.spinValue3;

        }
        else if (value < -832.5f && value >= -877.5f)
        {


            coinCollected = GameConfig.Instance.valueConfrig.spinValue4;

        }
        else if (value < -877.5f && value >= -922.5f)
        {

            coinCollected = GameConfig.Instance.valueConfrig.spinValue5;

        }
        else if (value < -922.5f && value >= -967.5f)
        {

            coinCollected = GameConfig.Instance.valueConfrig.spinValue6;

        }
        else if (value < -967.5f && value >= -1012.5f)
        {

            coinCollected = GameConfig.Instance.valueConfrig.spinValue7;
        }
        else if (value < -1012.5f && value >= -1057.5f)
        {


            coinCollected = GameConfig.Instance.valueConfrig.spinValue8;

        }
    }
    public void ShowReward()
    {

        rayTop.SetActive(false);
        rewardPopup.SetActive(true);
        Gamedata.I.DateTimeSpin = DateTime.Now.Day;
        collectButton.gameObject.SetActive(false);
        Invoke("ShowClaimClick", 3f);
        
    }
    
    void ShowClaimClick()
    {
        collectButton.gameObject.SetActive(true);
    }

    void SetFreeSpin()
    {
        txtTimer.transform.parent.gameObject.SetActive(false);
        if (Gamedata.GetTotalResource(ResourceType.FreeSpinTicket) == 0)
        {
            Gamedata.CountSpinInDay = 0;
            Gamedata.CurrentPrices = 0;
        }

    }
    void SpinAds()
    {
        freeSpin = false;
        SpinRotate();
    }

    void SwapGemToWeapon()
    {
        if(Gamedata.I.Gem >=5)
        {
            Gamedata.I.SetWeaponUnlock(1);
        }
    }
    void CountSpinLucky(bool canCollect)
    {
        float valueSpin = Gamedata.CountSpinLucky /20;
        slider.value = valueSpin;

        if (Gamedata.CountSpinLucky == 2)
        {
            if (canCollect)
            {
                claimBonusReward[0].interactable = true;
            }
            
            var tempColor = claimBonusReward[0].GetComponent<Image>().color;
            tempColor.a = 1f;
            claimBonusReward[0].GetComponent<Image>().color = tempColor;

       
        }
        else if(Gamedata.CountSpinLucky == 5)
        {  if (canCollect)
            {
                claimBonusReward[1].interactable = true;
            }
            var tempColor = claimBonusReward[1].GetComponent<Image>().color;
            tempColor.a = 1f;
            claimBonusReward[1].GetComponent<Image>().color = tempColor;

        }
        else if(Gamedata.CountSpinLucky == 10)
        {  if (canCollect)
            {
                claimBonusReward[2].interactable = true;
            }
            
            var tempColor = claimBonusReward[2].GetComponent<Image>().color;
            tempColor.a = 1f;
            claimBonusReward[2].GetComponent<Image>().color = tempColor;

        }
        else if(Gamedata.CountSpinLucky == 20)
        {
            if (canCollect)
            {
                claimBonusReward[3].interactable = true;
            }
            var tempColor = claimBonusReward[3].GetComponent<Image>().color;
            tempColor.a = 1f;
            claimBonusReward[3].GetComponent<Image>().color = tempColor;

        }
    }
    IEnumerator CountDownTime()
    {
        while (totalTime > 0)
        {
            totalTime--;
            txtTimer.text = Gamedata.getFormattedTimeFromSeconds(totalTime);
            yield return new WaitForSecondsRealtime(1);
        }
        SetFreeSpin();
        Gamedata.LastSpinFree = DateTime.Now.AddDays(1);
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
            SpinAds();
        }
        
    }
    public void OnRewardedAdsClosedToDoubleCoin(bool success)
    {
        if (success)
        {
            CollectX2Click();
        }

    }
    public void CollectClick()
    {
        Debug.LogError(coinCollected);
        rewardPopup.SetActive(false);
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
        Gamedata.I.Coin += coinCollected;
    }

    void CollectX2Click()
    {
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
        rewardPopup.SetActive(false);
        Gamedata.I.Coin += coinCollected * 2;
    }
    public void CLaimRewardFirst()
    {
        claimBonusReward[0].interactable = false;
        Gamedata.I.Coin += 600;
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
    }

    public void CLaimRewardSecond()
    {
        claimBonusReward[0].interactable = false;
        Gamedata.I.Coin += 5000;
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);

    }

    public  void CLaimRewardThird()
    {
        claimBonusReward[0].interactable = false;
        Gamedata.I.Coin += 12000;
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
    }
}
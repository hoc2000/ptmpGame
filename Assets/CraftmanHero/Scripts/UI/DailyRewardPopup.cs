using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DailyRewardPopup : BaseUIMenu
{

    [Header("BUTTON")]
    [Space(10)]
    [SerializeField] Button claimButton;
    [SerializeField] Button claimX2Button;
    [SerializeField] GameObject button;
    [SerializeField] Button closeButton;
    [Header("DAY VALUE")]
    [Space(10)]
    [SerializeField] Image[] bgDay;
    [SerializeField] GameObject[] focus;
    [SerializeField] GameObject[] textDay;
    [SerializeField] TextMeshProUGUI[] valueText;
    //[SerializeField] Transform[] itemIcon;

    [Header("SPRITE")]
    [Space(10)]
    [SerializeField] Sprite bgCollected;
    [SerializeField] Sprite bgCanCollect;
    [SerializeField] Sprite lightning;

    [Header("COIN")]
    [SerializeField] GameObject life;
    [SerializeField] GameObject coin;
    [SerializeField] Transform coinIcon;
    [SerializeField] Transform lifeIcon;

    [SerializeField] StarUIFly[] coinFx;
    [SerializeField] StarUIFly[] lifeFx;
    public GameObject notVideo;

    private int dayIndex;
    int today = 0;
    private DailyRewardItem currentDailyRewardItem;

    private void OnEnable()
    {
        Init();
    }
    void Start()
    {
        AddEventButton();
    }
    #region INIT
    void Init()
    {
        GameAnalytics.LogUIAppear("popup_daily_reward", "HomeScene");
        today = DateTime.Now.Day;
        dayIndex = Gamedata.CurrentDailyRewardDayIndex;
        claimButton.gameObject.SetActive(false);
        AddValueText();
        Invoke("ShowClaimButton", 3f);
        CheckCanCollect();
    }
    void AddEventButton()
    {
        claimButton.onClick.AddListener(() => ClaimClick());

        //claimX2Button.onClick.AddListener(() => ClaimX2Click());

        closeButton.onClick.AddListener(() => CloseClick());
    }
    void ShowClaimButton()
    {
        claimButton.gameObject.SetActive(true);
    }
    void AddValueText()
    {
        valueText[0].text = "+" + GameConfig.Instance.valueConfrig.day1.ToString();
        valueText[1].text = "+" + GameConfig.Instance.valueConfrig.day2.ToString();
        valueText[2].text = "+" + GameConfig.Instance.valueConfrig.day3.ToString();
        valueText[3].text = "+" + GameConfig.Instance.valueConfrig.day4.ToString();
        valueText[4].text = "+" + GameConfig.Instance.valueConfrig.day5.ToString();
        valueText[5].text = "+" + GameConfig.Instance.valueConfrig.day6.ToString();
        valueText[6].text ="UNLOCK SKIN";
    }
    #endregion
    #region EFFECT
    void CollectFX(bool coin)
    {
        //this.coin.SetActive(coin);
        //this.life.SetActive(!coin);
        //if (coin)
        //{

            

        //}
        //else
        //{
            
        //}



    }

    #endregion
    #region CHECK DAY
    void CheckCanCollect()
    {
        closeButton.gameObject.SetActive(true);
        button.SetActive(false);
        for (int i = 0; i < bgDay.Length; i++)
        {
            if (i == 0 && Gamedata.I.DayDaily == 0)
            {
                button.SetActive(true);
                focus[i].SetActive(true);
                bgDay[i].sprite = bgCanCollect;
                closeButton.gameObject.SetActive(false);
            }
            else
            {
                focus[i].SetActive(false);

                if (Gamedata.I.DayDaily == i)
                {
                    if (Gamedata.I.DateTimeDaily < today)
                    {
                        button.SetActive(true);
                        focus[i].SetActive(true);
                        bgDay[i].sprite = bgCanCollect;
                        closeButton.gameObject.SetActive(false);
                    }
                    else
                    {
                        focus[i].SetActive(false);
                    }
                }
                else if (i < Gamedata.I.DayDaily)
                {
                    bgDay[i].sprite = bgCollected;
                }
            }
        }
        if (Gamedata.I.DayDaily == 6)
        {
            claimX2Button.gameObject.SetActive(false);
        }
    }
    public bool DetermineIfShow()
    {
        DateTime lastDailyRewardClaim = Gamedata.LastDailyRewardClaim;
        if (DateTime.Now.Date > lastDailyRewardClaim.Date)
        {
            CanvasManager.Push(GlobalInfo.DailyRewardPopup, null);
            return true;
        }
        return false;
    }
    void Claim(int x2)
    {
        switch (Gamedata.I.DayDaily)
        {
            case 0:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day1 * x2;
                CollectFX(true);
                break;
            case 1:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day2 * x2;
                CollectFX(false);
                break;
            case 2:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day3 * x2;
                CollectFX(true);
                break;
            case 3:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day4 * x2;
                CollectFX(true);
                break;
            case 4:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day5 * x2;
                CollectFX(false);
                break;
            case 5:
                Gamedata.I.Coin += GameConfig.Instance.valueConfrig.day6 * x2;
                CollectFX(true);
                break;
            case 6:
                Gamedata.I.SetSkinUnlock(GameConfig.Instance.valueConfrig.day7);
                Gamedata.I.SkinSellect = GameConfig.Instance.valueConfrig.day7;
                this.PostEvent(EventID.SellectSkin);
                break;

        }
        Gamedata.I.DayDaily++;
        Gamedata.I.DateTimeDaily = DateTime.Now.Day;
        CheckCanCollect();
        CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
        Debug.Log(Gamedata.I.DayDaily + " ");
    }
    #endregion
    #region BUTTON
    void ClaimClick()
    {
        Claim(1);
        Invoke("ClosePopup", 1.2f);

    }
    void ClaimX2Click()
    {
        Claim(2);
        Invoke("ClosePopup",1.2f);
    }
    void CloseClick()
    {
        ClosePopup();
    }

    void ClosePopup()
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
            ClaimX2Click();
        }
        else
        {
            Debug.Log("ksdao");
        }
    }

   
}

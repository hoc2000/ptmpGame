using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;


public class RevivePopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button reviveButton;
    [SerializeField] Button noThankButton;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI timeText;

    [SerializeField] private Image circleTime;

    [SerializeField] private float timeCountDown = 5f;
    public GameObject notVideo;

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
        GameAnalytics.LogUIAppear("popup_revive", "GamePlay");
        circleTime.fillAmount = 1;
        circleTime.color = Color.green;

        noThankButton.gameObject.SetActive(false);
        StartCoroutine(TimeCowdown());
    }

    void AddEventButton()
    {
        noThankButton.onClick.AddListener(() => NoThankClick());
        //reviveButton.onClick.AddListener(() => ReviveClick());
    }

    IEnumerator TimeCowdown()
    {

        circleTime.DOFillAmount(0f,timeCountDown).SetEase(Ease.Linear);

        for (int i = 5; i >= 0; i--)
        {
            timeText.text = i.ToString();
            if(i == 2)
            {
                circleTime.color = Color.red;
            }
            yield return new WaitForSeconds(1f);
        }
        noThankButton.gameObject.SetActive(true);
        CanvasManager.Push(GlobalInfo.FalsePopup, null);

    }
    #endregion
    #region BUTTON
    public void NoThankClick()
    {
        CanvasManager.Push(GlobalInfo.FalsePopup, null);
        this.Pop();
    }
    void ReviveClick()
    {
        Player.Instance.Revive();
        circleTime.DOKill();
        this.Pop();
    }
    #endregion

    public override void Pop()
    {
       
        base.Pop();

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
            GameAnalytics.LogRevive(GameManager.levelSelected, "watch_ads", Gamedata.getWeapon, "sword");
            Time.timeScale = 1;
            ReviveClick();
        }
        else
        {
            Debug.Log("ksdao");
        }
    }
}

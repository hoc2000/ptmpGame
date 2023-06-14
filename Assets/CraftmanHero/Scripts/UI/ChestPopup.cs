using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool.UI;
using My.Tool;
using UnityEngine;
using UnityEngine.UI;

public class ChestPopup : BaseUIMenu
{
    [Header("BUTTON")]
    //[SerializeField] Button getSkinButton;
    [SerializeField] Button getCoinButton;
    [SerializeField] Button getLifeButton;
    [SerializeField] Button closeButton;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI lifeTxt;
    [SerializeField] TextMeshProUGUI coinTxt;
    int coinClollect;
    int lifeCollect;
    int skinIDCollect;
    [SerializeField] GameObject popup;

    private void OnEnable()
    {
        Init();
        popup.transform.localScale = Vector3.zero;
        popup.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    void Start()
    {
        AddEventButton();
    }
    #region INIT
    void Init()
    {
        //coinClollect = SDK.Instance.valueConfrig.coinChestAds[Random.Range(0, SDK.Instance.valueConfrig.coinChestAds.Length)];
        //lifeCollect = SDK.Instance.valueConfrig.lifeChestAds[Random.Range(0, SDK.Instance.valueConfrig.lifeChestAds.Length)];

        //lifeTxt.text = lifeCollect.ToString();
        //coinTxt.text = coinClollect.ToString();

        //// getSkinButton.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        ////getSkinButton.gameObject.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetLoops(-1, LoopType.Yoyo);

        //getCoinButton.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        //getCoinButton.gameObject.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);

        //getLifeButton.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        //getLifeButton.gameObject.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
    }
    void AddEventButton()
    {
        //getSkinButton.onClick.AddListener(() => GetSkinClick());
        getCoinButton.onClick.AddListener(() => GetCoinClick());
        getLifeButton.onClick.AddListener(() => GetLifeClick());
        closeButton.onClick.AddListener(() => CloseClick());
    }
    #endregion
    #region BUTTON EVENT
    void GetSkinClick()
    {

    }
    void GetCoinClick()
    {
        //SDK.Instance.ShowReward((success) =>
        //{
        //    if (success)
        //    {
        //        Gamedata.I.Coin += coinClollect;
        //        this.PostEvent(EventID.UpdateData);
        //        ToastMgr.Instance.Show("+" + coinClollect.ToString() + " Coin");
        //        getCoinButton.gameObject.SetActive(false);
        //    }
        //}, RewardID.GetCoinChestInGame);
    }
    void GetLifeClick()
    {
        //SDK.Instance.ShowReward((success) =>
        //{
        //    if (success)
        //    {
        //        Gamedata.I.Life += lifeCollect;
        //        this.PostEvent(EventID.UpdateData);
        //        ToastMgr.Instance.Show("+" + lifeCollect.ToString() + " Life");
        //        getLifeButton.gameObject.SetActive(false);
        //    }
        //}, RewardID.GetLifeChestInGame);
    }
    public void CloseClick()
    {
        this.PostEvent(EventID.CloseChestAds);
        this.Pop();
    }
    #endregion
}

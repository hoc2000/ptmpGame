using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using My.Tool.UI;
using My.Tool;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class CardGift : MonoBehaviour
{
    [SerializeField] int idSkin;
    [SerializeField] int idCard;
    [SerializeField] int valueAds;
    [Header("BUTTON")]
    [SerializeField] Button getAdsButton;
    [SerializeField] Button collectButton;

    [Header("VALUE ADS")]
    [SerializeField] TextMeshProUGUI valueAdstext;
    [SerializeField] Image valueImage;

    [Header("OBJ")]
    [SerializeField] GameObject checkCollect;
    [SerializeField] GameObject checkCanCollect;

    private void OnEnable()
    {
       // getAdsButton.transform.localScale = new Vector3(0.95f, 0.95f, 0.95f);
        //getAdsButton.gameObject.transform.DOScale(new Vector3(1.05f, 1.05f, 1.05f), 0.5f).SetEase(Ease.OutBack).SetLoops(-1, LoopType.Yoyo);
        Init();
        this.RegisterListener(EventID.UpdateData, (sender, param) => SetData());

    }

    private void OnDisable()
    {
        this.RemoveListener(EventID.UpdateData, (sender, param) => SetData());
    }
    void Start()
    {
        getAdsButton.onClick.AddListener(() => GetAdsClick());
        collectButton.onClick.AddListener(() => CollectClick());
    }
    #region INIT
    void Init()
    {
        SetData();


        //Invoke("AnimButton", 1f);
    }

    void SetData()
    {

        if (Gamedata.I.GetCardGiftCount(idCard) >= valueAds)
        {
            getAdsButton.gameObject.SetActive(false);
            collectButton.gameObject.SetActive(true);
            checkCanCollect.SetActive(true);
        }
        else
        {
            getAdsButton.gameObject.SetActive(true);
            collectButton.gameObject.SetActive(false);
            checkCanCollect.SetActive(false);
        }

        if (Gamedata.I.GetSkinUnlock(idSkin))
        {
            valueAdstext.text = valueAds.ToString() + "/" + valueAds.ToString() + " AD";
            valueImage.fillAmount = 1;

            getAdsButton.gameObject.SetActive(false);
            collectButton.gameObject.SetActive(false);
            checkCanCollect.SetActive(false);
            checkCollect.SetActive(true);
        }
        else
        {
            valueAdstext.text = Gamedata.I.GetCardGiftCount(idCard).ToString() + "/" + valueAds.ToString() + " AD";
            valueImage.fillAmount = (float)Gamedata.I.GetCardGiftCount(idCard) / (float)valueAds;
        }
    }
    #endregion
    #region BUTTON
    void GetAdsClick()
    {
        //SDK.Instance.ShowReward((success) =>
        //{
        //    if (success)
        //    {
        Gamedata.I.SetCardGiftCount(idCard);
        this.PostEvent(EventID.UpdateData);
        //TrackingManager.Instance.WatchAdsToGetGiftTracking(valueAds, Gamedata.I.GetCardGiftCount(idCard), false, idSkin);
        //}
        //}, RewardID.GetCardGift);
    }
    void CollectClick()
    {
        //Gamedata.I.AdsGiftCount -= valueAds;
        Gamedata.I.SetSkinUnlock(idSkin);
        Gamedata.I.SkinSellect = idSkin;
        this.PostEvent(EventID.UpdateData);
        this.PostEvent(EventID.SellectSkin);
        //TrackingManager.Instance.WatchAdsToGetGiftTracking(valueAds, Gamedata.I.GetCardGiftCount(idCard), true, idSkin);
    }
    #endregion
}
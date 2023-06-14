using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpiritItem : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] Image avatar;
    [SerializeField] Image outImgSellect;
    public GameObject focus;
    public GameObject lockObj;
    //public GameObject notify;


    [SerializeField] SkinPopup skinPopup;
    [SerializeField] Sprite spriteIdle, spriteSlellect;
    bool isUnlock;

    Button sellectButton;

    private void OnEnable()
    {
        Init();
    }
    private void Start()
    {
        sellectButton = gameObject.GetComponent<Button>();
        sellectButton.onClick.AddListener(() => SellectClick());
    }
    public void Init()
    {
        //var spiritInform = DataManager.Instance.spiritData.spiritInform[id];

        //avatar.sprite = spiritInform.avatar;

        //isUnlock = Gamedata.I.GetSpiritUnlock(id);

        //lockObj.SetActive(!isUnlock);
        //focus.SetActive(Gamedata.I.SpiritSellect == id);
    }

    void SellectClick()
    {
        //skinPopup.SellectSpirit(id);
        //if (isUnlock)
        //{
        //    Gamedata.I.SkinSellect = id;
        //}
        //else
        //{

        //}
        //Debug.LogError(isUnlock);
    }

    public void ActiveSellect()
    {
        //focus.SetActive(Gamedata.I.SpiritSellect == id);
    }
    public void ChangerImgSellect(int idSpirit)
    {
        if (id == idSpirit)
        {
            outImgSellect.sprite = spriteSlellect;
        }
        else
        {
            outImgSellect.sprite = spriteIdle;
        }

    }

}

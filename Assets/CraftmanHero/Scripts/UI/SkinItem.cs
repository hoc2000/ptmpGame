using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinItem : MonoBehaviour
{
    
    [SerializeField] public Image skinItem;

    [Header("SPRITE")]

    [SerializeField] public Sprite unselectedSkin;
    [SerializeField] public Sprite selectedSkin;
    [SerializeField] public Sprite unlockSkin;

    [SerializeField]
    public Item resourceItem;
    [SerializeField]
    public ResourceType currencyType;

    Button sellectButton;
    [SerializeField] public GameObject lockKey;

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
        
    }

    virtual public void SellectClick()
    {
        bool isSelected = Gamedata.SelectedCharacter == resourceItem.detail;
        if (isSelected)
        {
            for (int i = 0; i < SkinPopup.instance.skinItems.Count; i++)
            {
                SkinPopup.instance.skinItems[i].skinItem.sprite = unselectedSkin;
            }
            skinItem.sprite = selectedSkin;

            
        }else
        {
            skinItem.sprite = unselectedSkin;

        }


        SkinPopup.instance.SellectSkin(resourceItem,false,false,true);
        SkinPopup.instance.ChangeSkin(resourceItem);

    }
    public void UpdateCharacterUnlocked()
    {
        bool hasCharacter = Gamedata.HasCharacter(resourceItem.detail);
        if (hasCharacter)
        {

            skinItem.sprite = unlockSkin;
            lockKey.SetActive(false);

        }
        else
        {
            skinItem.sprite = unselectedSkin;
            lockKey.SetActive(true);

        }
    }
    public void ActiveSellect()
    {
    }
    public void ChangerImgSellect(int idSkin)
    {
       
    }

}

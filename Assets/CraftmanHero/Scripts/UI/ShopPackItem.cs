using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;
using My.Tool;
using TMPro;

public class ShopPackItem : MonoBehaviour
{

    [SerializeField]
    private List<Item> resourceItems;

    [SerializeField]
    private Button btnBuy;

    [SerializeField]
    private IAPButton iapButton;

    [SerializeField]
    private string productId;
    [SerializeField]
    private bool isVip;
    private void OnEnable()
    {
        if (isVip)
        {
            Gamedata.RegisterResourceListener(ResourceType.Vip, OnVipChanged);
        }
        UpdateBuyButtonDisplay();
        this.RegisterListener(EventID.IAPPurchaseCompleted, (sender,param) => OnIAPPurchaseCompleted());

    }
    void Start()
    {
      
        if (iapButton != null && iapButton.enabled)
        {
            Product product = CodelessIAPStoreListener.Instance.GetProduct(iapButton.productId);
            if (product != null)
            {
                if (product.definition.type == ProductType.NonConsumable)
                {
                    if (Gamedata.IsBought(iapButton.productId))
                    {
                        btnBuy.interactable = false;
                    }
                    else
                    {
                        btnBuy.interactable = true;
                    }
                }
                else if (product.definition.type == ProductType.Subscription)
                {
                    btnBuy.interactable = !Gamedata.IsVip;
                }
                else
                {
                    btnBuy.interactable = true;
                }
            }
        }
    }
    private void OnIAPPurchaseCompleted()
    {
        UpdateBuyButtonDisplay();
    }

    private void OnDisable()
    {
        if (isVip)
        {
            Gamedata.RemoveResourceListener(ResourceType.Vip, OnVipChanged);
        }
        this.RemoveListener(EventID.IAPPurchaseCompleted, (sender,param) => OnIAPPurchaseCompleted());
    }
    public void BuyClick()
    {
        if (isVip && !Gamedata.IsVip)
        {
            Debug.Log("sdasd");
        }
        if (this.iapButton != null)
        {
            GameAnalytics.LogButtonClick("popup_shop", iapButton.productId);
            Debug.Log(iapButton.productId);
        }
        else
        {
            GameAnalytics.LogButtonClick("popup_shop", productId);

        }
    }
   

    private void OnVipChanged(ResourceType resourceType)
    {
        UpdateBuyButtonDisplay();
    }

    private void UpdateBuyButtonDisplay()
    {
        if (isVip)
        {
            btnBuy.interactable = !Gamedata.IsVip;
        }
        else
        {
            if (iapButton != null && iapButton.enabled)
            {
                string currentProductId = iapButton.productId;
                if (!string.IsNullOrEmpty(currentProductId))
                {
                    Product product = CodelessIAPStoreListener.Instance.GetProduct(currentProductId);
                    if (product != null)
                    {
                        btnBuy.interactable = !Gamedata.IsBought(currentProductId)
                            || product.definition.type == ProductType.Consumable;
                        if (currentProductId.Equals(IAPPurchaseHandler.REMOVE_ADS_PRODUCT_ID) && (Gamedata.IsRemoveAds || Gamedata.IsVip))
                        {
                            btnBuy.interactable = false;
                        }
                    }
                }
            }
        }
    }

}

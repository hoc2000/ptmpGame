using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing.Security;
using UnityEngine.Purchasing;
using My.Tool;
using UnityEngine.UI;
using My.Tool.UI;

public class IAPController : MonoBehaviour
{
    [SerializeField]
    private List<Item> resourceItems;
    [SerializeField]
    private bool isVip;

    [SerializeField]
    private IAPButton iapButton;

    [SerializeField]
    private Button btnBuy;


    [SerializeField]
    private string productId;
    private void OnEnable()
    {
        if (isVip)
        {
            Gamedata.RegisterResourceListener(ResourceType.Vip, OnVipChanged);
        }
        UpdateBuyButtonDisplay();
        this.RegisterListener(EventID.IAPPurchaseCompleted,(sender,param) => OnIAPPurchaseCompleted());
    }
    private void OnDisable()
    {
        if (isVip)
        {
            Gamedata.RemoveResourceListener(ResourceType.Vip, OnVipChanged);
        }
        this.RegisterListener(EventID.IAPPurchaseCompleted, (sender, param) => OnIAPPurchaseCompleted());
    }

    private void OnIAPPurchaseCompleted()
    {
        UpdateBuyButtonDisplay();
    }

    private void OnVipChanged(ResourceType resourceType)
    {
        UpdateBuyButtonDisplay();
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

    public void BuyClick()
    {
        if (isVip && !Gamedata.IsVip)
        {
            CanvasManager.Push(GlobalInfo.ShopPopup, null);

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
    public void BuyCoinIAP(Product product)
    {

            if (Helper.VerifyIap(product))
            {
                Gamedata.ClaimResourceItems(resourceItems);
                CanvasManager.Push(GlobalInfo.ColectFxPopup, null);
            }

    }
    public void OnRewardedAdsNotAvailable()
    {
       
    }

    public void OnRewardedAdClosed(bool success)
    {
        if (success)
        {
            Gamedata.ClaimResourceItems(resourceItems);
          
            Gamedata.CoinReceived += 50;
        }
    }
}



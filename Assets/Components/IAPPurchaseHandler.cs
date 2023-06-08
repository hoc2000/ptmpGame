using com.spacepuppy.Collections;
using My.Tool;
using My.Tool.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.SceneManagement;

public class IAPPurchaseHandler : IAPListener
{
    public const string REMOVE_ADS_PRODUCT_ID = "remove_ads";
    private string sceneName;
    public Item items;
    [SerializeField]
    private IAPPack iapPack;


    public void ProcessSuccessPurchase(Product product)
    {
        if (iapPack.ContainsKey(product.definition.id))
        {
            ItemCollection coll = iapPack[product.definition.id];
            if (Helper.VerifyIap(product))
            {
                Gamedata.ClaimResourceItems(coll.items);
                Gamedata.SetBought(product.definition.id);
                this.PostEvent(EventID.IAPPurchaseCompleted);
                //UIClaimResourcePanel.Setup(coll.items).Show();
                sceneName = SceneManager.GetActiveScene().name;
                Debug.Log("muaiap" + Gamedata.I.Coin);
                if (coll.items[0].countable)
                {
                    CanvasManager.Push(GlobalInfo.ColectFxPopup, null);

                }
            }
        }
    }
}

[Serializable]
public class IAPPack : RotaryHeart.Lib.SerializableDictionary.SerializableDictionaryBase<string, ItemCollection>
{
}
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using TMPro;
//using Tool;

//public class BuyIAP : MonoBehaviour
//{
//    public enum ItemType
//    {
//        buyVip,
//        buyCoin,
//        buyLife

//    }

//    public ItemType itemType;
//    public TextMeshProUGUI priceText;
//    //public Text priceText;
//    void Start()
//    {
//        //if (priceText != null)
//        //{
//        //    priceText = gameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
//        //}


//        StartCoroutine(LoadPriceRoutine());

//        gameObject.GetComponent<Button>().onClick.AddListener(() => { CLickBuy(); });

//    }

//    public void CLickBuy()
//    {
//        switch (itemType)
//        {
//            case ItemType.buyVip:
//                IAPManager.Instance.BuyVip();
//                break;
//            case ItemType.buyCoin:
//                IAPManager.Instance.BuyCoin();
//                break;
//            case ItemType.buyLife:
//                IAPManager.Instance.BuyLife();
//                break;
//        }

//    }

//    IEnumerator LoadPriceRoutine()
//    {
//        while (!IAPManager.Instance.IsInitialized())
//        {
//            priceText.text = "No Internet";
//            yield return null;
//        }


//        string loadedPrice = "Loading...";

//        switch (itemType)
//        {
//            case ItemType.buyVip:
//                if (priceText)
//                    priceText.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.vip);
//                break;
//            case ItemType.buyCoin:
//                if (priceText)
//                    priceText.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.buyCoin);
//                break;
//            case ItemType.buyLife:
//                if (priceText)
//                    priceText.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.buyLife);
//                break;
//                //case ItemType.buyRemove:
//                //    if (priceText)
//                //        priceText.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.remove);
//                //    break;
//                //case ItemType.buyRain:
//                //    if (priceText)
//                //        priceText.text = IAPManager.Instance.GetProducePriceFromStore(IAPManager.Instance.rain);
//                //    break;
//        };
//    }

//}

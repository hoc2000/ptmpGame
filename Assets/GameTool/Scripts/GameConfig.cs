using Spine.Unity;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using Firebase.RemoteConfig;

public class GameConfig : SingletonMonoBehaviour<GameConfig>
{
    ValueConfrig valueConfrigInform;
    public ValueConfrig valueConfrig;


    //private void Awake()
    //{
    //    SDK.OnSetDefault += InitDefaultLevel;
    //    SDK.OnFetchCompleted += InitAPI;
    //}

    //public string GetKey()
    //{
    //    string key = "gameConfrig";
    //    if (SDK.IsAndroid())
    //        key += "_android";
    //    else if (SDK.IsIOS())
    //        key += "_ios";
    //    return key;
    //}

    //private void InitAPI()
    //{
    //    string key = GetKey();
    //    string data = FirebaseRemoteConfig.DefaultInstance.GetValue(GetKey()).StringValue;
    //    data = data.CorrectString();
    //    ApiDebug(string.Format("FirebaseRemote - key {0}\n{1}", key, data));

    //    valueConfrig = JsonUtility.FromJson<ValueConfrig>(data);

    //}
    //private void InitDefaultLevel(Dictionary<string, object> defaults)
    //{
    //    //defaults = new Dictionary<string, object>();

    //    defaults.Add(GetKey(), JsonUtility.ToJson(valueConfrigInform));

    //    FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults);


    //}
    //public void ApiDebug(string content)
    //{
    //    // Debug.Log(content);
    //    Debug.LogFormat("Log {0}", content);
    //}
}

[Serializable]
public enum ResourceDetail
{
    None,
    CharacterAny = 100,
    CharacterPhung = 101,
    CharacterThang = 102,
    CharacterChien = 103,
    CharacterNhung = 104,
    CharacterTan = 105,
    CharacterHung = 106,
    CharacterCa = 107,
    CharacterMay = 108,
    CharacterLan = 109
}

[Serializable]
public class ValueConfrig
{
    public int coinValue = 100;

    public int levelShowInter = 2;
    public int coinReward = 300;
    public int lifeReward = 2;


    //Chest Ads
    public int[] coinChestAds = { 300, 350, 400, 450 };
    public int[] lifeChestAds = { 2, 3, 4, 5 };
    public int IdSkinsChestAds = 1000;

    public int[] idSkinTry = { 1, 2, 3, 4, 5, 6 };
    public int[] idSkinGet = { 7, 8, 9, 10, 11 };
    public int[] LevelGetSkin = { 3, 10, 20, 30, 40 };

    //Dayly Reward
    public int day1 = 300;
    public int day2 = 500;
    public int day3 = 700;
    public int day4 = 1000;
    public int day5 = 1300;
    public int day6 = 1500;
    public int day7 = 10;

    public int[] idSkinsGift = { 15, 5, 2, 1 };
    public int[] countSkinsGift = { 10, 14, 14, 12 };


    //Spin Value
    public int spinValue1 = 1;
    public int spinValue2 = 200;
    public int spinValue3 = 200;
    public int spinValue4 = 500;
    public int spinValue5 = 800;
    public int spinValue6 = 300;
    public int spinValue7 = 300;
    public int spinValue8 = 500;


    // IAP
    public int coinVip = 10000;
    public int lifeVip = 20;

    public int coinIap = 20000;
    public int lifeIap = 20;

    //Character

    //LEVEl

    public int levelMax = 30;

}

public enum ResourceType
{
    Coin = 2,
    Gem = 4,
    Character = 6,
    LevelStar = 8,
    Key = 10,
    Health = 12,
    Life = 14,
    RemoveAds = 16,
    Vip = 18,
    DoubleCoin = 20,
    DiscountShop = 21,
    UnlimitedLife = 22,
    FreeSpinTicket = 23,
   
}

[Serializable]
public class Item : ICloneable
{
    public ResourceType type;
    public ResourceDetail detail;
    public int quantity;
    public bool countable;
    public int level;
    public int price;
    public int id;
    public SkeletonDataAsset spine;
    public Sprite spriteUI;


    public bool IsRandomCharacter
    {
        get
        {
            return type == ResourceType.Character && detail == ResourceDetail.CharacterAny;
        }
    }
    public void ChangeQuantity(int quantity)
    {
        this.quantity = quantity;
    }
    public void AssignCharacter(ResourceDetail detail)
    {
        this.detail = detail;
    }

    public object Clone()
    {
        Item clone = new Item();
        clone.type = type;
        clone.detail = detail;
        clone.quantity = quantity;
        clone.countable = countable;
        clone.level = level;
        clone.price = price;
        clone.id = id;
        clone.spine = spine;
        clone.spriteUI = spriteUI;
        return clone;
    }
}

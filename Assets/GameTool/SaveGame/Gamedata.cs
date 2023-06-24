using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using My.Tool;
using UnityEngine.Accessibility;
using UnityEngine.Events;

public class Gamedata : ManualSingletonMono<Gamedata>, ISaveData
{
    public GameDataSave Data;
    public static bool checkPoint;
    public static Vector3 posCheckPoint;
    public static int curScoreCheckPoint;
    public static Vector3 posRevive;
    private static Dictionary<ResourceType, ResourceTypeEvent> resourceListeners;
    private static ResourceDetail temporarySelectedCharacter;
    public static UnityEvent selectedCharacterChanged = new UnityEvent();
    public static ResourceDetailChangedEvent resourceDetailChanged = new ResourceDetailChangedEvent();
    public static int  selectedLevel = 1;

    public static int[] hardLevel = { 4, 10, 15, 20,30 };
    public static bool inGame;
    public static bool showAds;
    public static DateTime startGameShowInter;
    public static bool isInGame;
    [HideInInspector]
    public long timeOffset = 0;

    private static bool? localIsFirstOpen;
    private static bool? isTodayFirstOpen;

    public static int levelPlayedTime = 0;
    public static bool getWeapon;
    public static bool getHealItem;
    //public static int healthPerLife = Constants.HEALTH_PER_LIFE;

    public object GetData()
    {
        return Data;
    }

    public void SetData(string data)
    {
        if (data == string.Empty)
        {
            Data = new GameDataSave();
        }
        else
        {
            Data = JsonUtility.FromJson<GameDataSave>(data);
        }
    }

    public void RegisterSaveData()
    {
        SaveGameManager.I.RegisterMandatoryData("GameDataSave", this);
    }

    public void Save()
    {
        DataChanged = true;
        SaveGameManager.I.Save();
    }

    public void OnAllDataLoaded()
    {
        Debug.Log("All Data Loaded!!!");
    }

    public bool DataChanged { get; set; }


    public static int RescueCount
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.KEY.RESCUE_COUNT, 0);
        }
        private set
        {
            PlayerPrefs.SetInt(Constants.KEY.RESCUE_COUNT, value);
        }
    }
    public bool FirstOpen
    {
        get => Data.firstOpen;
        set
        {
            Data.firstOpen = value;
            Save();
        }
    }
    public bool DoubleJump
    {
        get => Data.doubleJump;
        set
        {
            Data.doubleJump = value;
            Save();
        }
    }
    public bool Sound
    {
        get => Data.sound;
        set
        {
            Data.sound = value;
            Save();
        }
    }
    public int CurrentLevel
    {
        get => Data.currentLevel;
        set
        {
            if (Data.currentLevel >= 31)
            {
                Data.currentLevel = 30;
            }
            else
            {
                GameAnalytics.LogFirebaseUserProperty("level_unlock", value);
                Data.currentLevel = value;
            }
            Save();
        }
    }

    public bool Rated
    {
        get => Data.rated;
        set
        {
            Data.rated = value;
            Save();
        }
    }
    public bool Vibrate
    {
        get => Data.vibrate;
        set
        {
            Data.vibrate = value;
            Save();
        }
    }

    public bool Music
    {
        get => Data.music;
        set
        {
            Data.music = value;
            Save();
        }
    }

    public int DateTimeDaily
    {
        get => Data.dateTimeDaily;
        set
        {
            Data.dateTimeDaily = value;
            Save();
        }
    }
    public int DateTimeSpin
    {
        get => Data.dateTimeSpin;
        set
        {
            Data.dateTimeSpin = value;
            Save();
        }
    }
    public int DayDaily
    {
        get => Data.dayDaily;
        set
        {
            Data.dayDaily = value;
            Save();
        }
    }

    //public int AdsGiftCount
    //{
    //    get => Data.adsGiftCount;
    //    set
    //    {
    //        Data.adsGiftCount = value;
    //        Save();
    //    }
    //}

    public int SkinSellect
    {
        get => Data.skinSellect;
        set
        {
            Data.skinSellect = value;
            Save();
        }
    }

    public int WeaponSellect
    {
        get => Data.weaponSellect;
        set
        {
            Data.weaponSellect = value;
            Save();
        }
    }

    public int TrySkin
    {
        get => Data.trySkin;
        set
        {
            Data.trySkin = value;
            Save();
        }
    }
    public int TryWeapon
    {
        get => Data.tryWeapon;
        set
        {
            Data.tryWeapon = value;
            Save();
        }

    }
    public int Coin
    {
        get
        {
            return GetTotalResource(ResourceType.Coin);
        }
        set
        {
            GameAnalytics.LogFirebaseUserProperty("total_coin", value);
            SetTotalResource(ResourceType.Coin, value);
        }
    } 


    public int Gem
    {
        get => Data.gem;
        set
        {
            Data.gem = value;
            Save();
        }
    }
    public int Life
    {
        get
        {
            return GetTotalResource(ResourceType.Life, Data.life);
        }
        set
        {

            SetTotalResource(ResourceType.Life, value);
        }
    }
    // Coin Upgrade
    public int LevelUpgrade
    {
        get => Data.levelUpgrade;
        set
        {
            GameAnalytics.LogFirebaseUserProperty("level_power", value);
            Data.levelUpgrade = value;
            Save();
        }
    }
    public int CoinUpgrade
    {
        get => Data.coinUpgrade;
        set
        {
            Data.coinUpgrade = value;
            Save();
        }
    }
    public int AtkPlayer
    {
        get => Data.atkPlayer;
        set
        {
            Data.atkPlayer = value;
            Save();
        }
    }
    public int HpPlayer
    {
        get => Data.hpPlayer;
        set
        {
            Data.hpPlayer = value;
            Save();
        }
    }
    public bool GetSkinUnlock(int id)
    {
        for (int i = 0; i < Data.listSkinUnlock.Count; i++)
        {
            if (Data.listSkinUnlock[i].ID == id)
            {
                return true;
            }
        }
        return false;
    }

    public void SetSkinUnlock(int id)
    {
        Data.listSkinUnlock.Add(new SkinUnlock()
        {
            ID = id
        });
    }
    public bool GetWeaponUnlock(int id)
    {
        for (int i = 0; i < Data.listWeaponUnlock.Count; i++)
        {
            if (Data.listWeaponUnlock[i].ID == id)
            {
                return true;
            }
        }
        return false;
    }

    public void SetWeaponUnlock(int id)
    {
        Data.listWeaponUnlock.Add(new WeaponUnlock()
        {
            ID = id
        });
    }
    public int GetCardGiftCount(int id)
    {
        for (int i = 0; i < Data.listIdCardGifts.Count; i++)
        {
            if (Data.listIdCardGifts[i].id == id)
            {
                return Data.listIdCardGifts[i].count;
            }
        }
        return 0;
    }

    public void SetCardGiftCount(int idCard)
    {
        for (int i = 0; i < Data.listIdCardGifts.Count; i++)
        {
            if (Data.listIdCardGifts[i].id == idCard)
            {
                Data.listIdCardGifts[i].count++;
                return;
            }
        }
        Data.listIdCardGifts.Add(new IDCardGifts()
        {
            id = idCard,
            count = 1
        });
    }
    public static bool IsBought(string productId)
    {
        string key = Constants.KEY.IAP_IS_BOUGHT_PREFIX + productId;
        return GetBool(key, false);
    }
    public static void SetBought(string productId)
    {
        string key = Constants.KEY.IAP_IS_BOUGHT_PREFIX + productId;
        SetBool(key, true);
    }
    public static void ClaimResourceItems(params Item[] items)
    {
        for (int i = 0; i < items.Length; i++)
        {
            Item ri = items[i];
            if (ri.detail == ResourceDetail.None)
            {
                if (ri.countable)
                {
                    AddResource(ri.type, ri.quantity);
                }
                else
                {
                    if (ri.type == ResourceType.RemoveAds)
                    {
                        IsRemoveAds = true;
                        AdsMediationController.Instance.HideBanner();
                    }
                    else AddResource(ri.type, 1);
                }
            }
        }
    }
    public static DateTime LastDailyRewardClaim
    {
        get
        {
            return GetDateTime(Constants.KEY.LAST_DAILY_REWARD_CLAIM, DateTime.MinValue);
        }
        set
        {
            SetDateTime(Constants.KEY.LAST_DAILY_REWARD_CLAIM, value);
        }
    }

    public static int LastDailyRewardDayIndex
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.KEY.LAST_DAILY_REWARD_DAY_INDEX, -1);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.LAST_DAILY_REWARD_DAY_INDEX, value);
        }
    }

    public DateTime Now()
    {
        return DateTime.Now.AddSeconds(-1.0f * timeOffset);
    }
    public static int CurrentDailyRewardDayIndex
    {
        get
        {
            DateTime lastDailyRewardClaim = LastDailyRewardClaim;
            if (lastDailyRewardClaim == DateTime.MinValue)
            {
                return 0;
            }
            else
            {
                int lastDailyRewardDayIndex = LastDailyRewardDayIndex;
                if (lastDailyRewardClaim.Date == DateTime.Now.Date)
                {
                    return lastDailyRewardDayIndex;
                }
                else
                {
                    return lastDailyRewardDayIndex == 6 ? 0 : lastDailyRewardDayIndex + 1;
                }
            }
        }
    }
    public static bool LevelPassed
    {
        get
        {
            return GetBool(Constants.KEY.LEVEL_PASS, true);

        }
        set
        {
            SetBool(Constants.KEY.LEVEL_PASS, value);

        }
    }
    public static bool isLevelPassed
    {
        get
        {
            return PlayerPrefs.GetInt(GameManager.levelSelected.ToString(), 1) == 1;
        }
        set
        {
            PlayerPrefs.SetInt(GameManager.levelSelected.ToString(), value ? 1 : 0);
        }
    }
    public static bool IsLevelHard(int level)
    {
        for (int i = 0; i < hardLevel.Length; i++)
        {
            if (level == hardLevel[i])
                return true;
        }
        return false;
    }


    public static bool IsVip
    {
        get
        {
            return GetBool(Constants.KEY.IS_VIP, false);
        }
        set
        {
            SetBool(Constants.KEY.IS_VIP, value);
        }
    }
    public static bool IsRemoveAds
    {
        get
        {
            return GetTotalResource(ResourceType.RemoveAds, 0) != 0;
        }
        set
        {
            SetTotalResource(ResourceType.RemoveAds, value ? 1 : 0);
        }
    }

    public static bool IsIAPUser
    {
        get
        {
            return GetBool(Constants.KEY.IS_IAP_USER, false);
        }
        set
        {
            SetBool(Constants.KEY.IS_IAP_USER, value);
        }
    }
    public static void AddResource(ResourceType resourceType, int quantity)
    {
        int currentQuantity = GetTotalResource(resourceType);
        currentQuantity += quantity;
        SetTotalResource(resourceType, currentQuantity);
    }

    public static int GetTotalResourceDetail(ResourceType resourceType, ResourceDetail resourceDetail)
    {
        return PlayerPrefs.GetInt(GetResourceKeyDetail(resourceType, resourceDetail));
    }

    public static string GetResourceKeyDetail(ResourceType resourceType, ResourceDetail detail)
    {
        return GetResourceKey(resourceType) + "_" + detail;
    }

    public static string GetResourceKey(ResourceType resourceType)
    {
        return Constants.KEY.RESOURCE_PREFIX + resourceType;
    }
    public static int GetTotalResource(ResourceType resourceType, int def = 0)
    {
        return PlayerPrefs.GetInt(GetResourceKey(resourceType), def);
    }
    public static void SetTotalResource(ResourceType resourceType, int total)
    {
        string resourceKey = GetResourceKey(resourceType);
        int currentResourceCount = PlayerPrefs.GetInt(resourceKey);
        PlayerPrefs.SetInt(resourceKey, total);
        if (currentResourceCount != total)
        {
            OnResourceTypeChange(resourceType);
        }
    }
    public static bool CanSubtractCurrency(ResourceType currency, int amount)
    {
        return GetTotalResource(currency) >= amount;
    }

    public static bool SubtractCurrency(ResourceType currency, int amount)
    {
        if (CanSubtractCurrency(currency, amount))
        {
            SubtractResource(currency, amount);
            return true;
        }
        return false;
    }

    public static void SubtractResource(ResourceType resourceType, int quantity)
    {
        int currentQuantity = GetTotalResource(resourceType);
        currentQuantity -= quantity;
        SetTotalResource(resourceType, currentQuantity);
    }

    public static void RegisterResourceListener(ResourceType resourceType, UnityAction<ResourceType> action)
    {
        if (resourceListeners == null)
        {
            resourceListeners = new Dictionary<ResourceType, ResourceTypeEvent>();
        }
        ResourceTypeEvent e = null;
        if (!resourceListeners.ContainsKey(resourceType))
        {
            e = new ResourceTypeEvent();
            resourceListeners[resourceType] = e;
        }
        else
        {
            e = resourceListeners[resourceType];
        }
        e.AddListener(action);
    }

    public static void RemoveResourceListener(ResourceType resourceType, UnityAction<ResourceType> action)
    {
        if (resourceListeners != null && resourceListeners.ContainsKey(resourceType))
        {
            ResourceTypeEvent e = resourceListeners[resourceType];
            e.RemoveListener(action);
        }
    }
    private static void OnResourceTypeChange(ResourceType resourceType)
    {
        if (resourceListeners != null && resourceListeners.ContainsKey(resourceType))
        {
            resourceListeners[resourceType].Invoke(resourceType);
        }
    }
    public static void ClaimResourceItems(List<Item> items)
    {
        ClaimResourceItems(items.ToArray());
    }



    public static bool HasCharacter(ResourceDetail character)
    {
        return GetTotalResourceDetail(ResourceType.Character, character) > 0;

    }

    public static void RescueCharacter(ResourceDetail character)
    {
        RescueCount++;
        ClaimCharacter(character);
    }

    public static void ClaimCharacter(ResourceDetail character)
    {
        SetTotalResourceDetail(ResourceType.Character, character, 1);
    }

    public static bool IsFirstOpen
    {
        get
        {
            if (localIsFirstOpen == null)
            {
                localIsFirstOpen = !PlayerPrefs.HasKey(Constants.KEY.FIRST_OPEN_TIME);
            }
            return localIsFirstOpen.Value;
        }
    }
    public static DateTime LastOpened
    {
        get
        {
            return GetDateTime(Constants.KEY.LAST_OPENED, DateTime.MinValue);
        }
        set
        {
            DateTime currentValue = GetDateTime(Constants.KEY.LAST_OPENED, DateTime.MinValue);
            if (DateTime.Now.Date > currentValue.Date)
            {
                isTodayFirstOpen = true;
            }
            else
            {
                isTodayFirstOpen = false;
            }
            SetDateTime(Constants.KEY.LAST_OPENED, value);
        }
    }
    public static DateTime FirstOpenTime
    {
        get
        {
            return GetDateTime(Constants.KEY.FIRST_OPEN_TIME, DateTime.MinValue);
        }
        set
        {
            localIsFirstOpen = true;
            SetDateTime(Constants.KEY.FIRST_OPEN_TIME, value);
        }
    }
    public static void SetTotalResourceDetail(ResourceType resourceType, ResourceDetail resourceDetail, int total)
    {
        PlayerPrefs.SetInt(GetResourceKeyDetail(resourceType, resourceDetail), total);
        resourceDetailChanged.Invoke(resourceDetail);
    }
    public static ResourceDetail SelectedCharacter
    {
        get
        {
            ResourceDetail tempCharacter = temporarySelectedCharacter;
            if (tempCharacter != ResourceDetail.None)
            {
                return tempCharacter;
            }
            return (ResourceDetail)PlayerPrefs.GetInt(Constants.KEY.SELECTED_CHARACTER, (int)ResourceDetail.CharacterPhung);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.SELECTED_CHARACTER, (int)value);
            selectedCharacterChanged.Invoke();
        }
    } public static ResourceDetail SelectedCharacterWithWeapon
    {
        get
        {
            ResourceDetail tempCharacter = temporarySelectedCharacter;
            if (tempCharacter != ResourceDetail.None)
            {
                return tempCharacter;
            }
            return (ResourceDetail)PlayerPrefs.GetInt(Constants.KEY.SELECTED_CHARACTER_WITH_WEAPON, (int)ResourceDetail.CharacterPhung);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.SELECTED_CHARACTER_WITH_WEAPON, (int)value);
            selectedCharacterChanged.Invoke();
        }
    }
    public static ResourceDetail TryFreeCharacter
    {
        get
        {
            ResourceDetail tempCharacter = temporarySelectedCharacter;
            if (tempCharacter != ResourceDetail.None)
            {
                return tempCharacter;
            }
            return (ResourceDetail)PlayerPrefs.GetInt(Constants.KEY.TRY_FREE_CHARACTER, -1);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.TRY_FREE_CHARACTER, (int)value);
            selectedCharacterChanged.Invoke();
        }
    }

    public static ResourceDetail TemporarySelectedCharacter
    {
        get
        {
            return temporarySelectedCharacter;
        }
        set
        {
            temporarySelectedCharacter = value;
            selectedCharacterChanged.Invoke();
        }
    }
    public static CharacterInfo SelectedCharacterInfo
    {
        get
        {
            return GameManager.Instance.dataHolder.characters[SelectedCharacter];
        }
    }  public static CharacterInfo SelectedCharacterWithWeaponInfo
    {
        get
        {
            return GameManager.Instance.dataHolder.charactersWithWeapon[SelectedCharacterWithWeapon];
        }
    }
    public static DateTime LastSpinFree
    {
        get
        {
            return GetDateTime(Constants.KEY.LAST_DATE_TIME_FREE_SPIN, DateTime.MinValue);
        }
        set
        {
            SetDateTime(Constants.KEY.LAST_DATE_TIME_FREE_SPIN, value);
        }
    }





    private static bool GetBool(string key, bool defaultValue = false)
    {
        return PlayerPrefs.HasKey(key) ? PlayerPrefs.GetInt(key) == 1 : defaultValue;
    }

    private static void SetBool(string key, bool value)
    {
        PlayerPrefs.SetInt(key, value ? 1 : 0);
    }

    public static DateTime LastReceivedVipDailyCoin
    {
        get
        {
            return GetDateTime(Constants.KEY.LAST_RECEIVED_VIP_DAILY_REWARD, DateTime.MinValue);
        }
        set
        {
            SetDateTime(Constants.KEY.LAST_RECEIVED_VIP_DAILY_REWARD, value);
        }
    }


    public static int CountSpinInDay
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.KEY.COUNT_SPIN_IN_DAY);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.COUNT_SPIN_IN_DAY, value);
        }
    }
    public static float CountSpinLucky
    {
        get
        {
            return PlayerPrefs.GetFloat(Constants.KEY.COUNT_SPIN_LUCKY, 0);
        }
        set
        {
            PlayerPrefs.SetFloat(Constants.KEY.COUNT_SPIN_LUCKY, value);
        }
    }

    public static string getFormattedTimeFromSeconds(long seconds)
    {
        if (seconds > 3600)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", seconds / 3600, (seconds / 60) % 60, seconds % 60);
        }
        else
        {
            return string.Format("{0:00}:{1:00}", seconds / 60, seconds % 60);
        }
    }
    public static int CurrentPrices
    {
        get
        {
            return PlayerPrefs.GetInt(Constants.KEY.OLD_PRICES, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.OLD_PRICES, value);
        }
    }
    private static DateTime GetDateTime(string key, DateTime defaultValue)
    {
        string strValue = PlayerPrefs.GetString(key);
        DateTime result = defaultValue;
        if (!string.IsNullOrEmpty(strValue))
        {
            long dateData = Convert.ToInt64(strValue);
            result = DateTime.FromBinary(dateData);
        }
        return result;
    }

    private static void SetDateTime(string key, DateTime date)
    {
        PlayerPrefs.SetString(key, date.ToBinary().ToString());
    }

    public static void SetEatHeartLevel(int id, int level, int value)
    {
        PlayerPrefs.SetInt(ResourceType.Life.ToString() + id + level, value);
    }
    public static int GetEatHeartLevel(int id, int level)
    {
        return PlayerPrefs.GetInt(ResourceType.Life.ToString() + id + level, 0);
    }

    public static int GemReceived
    {
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.UP_GEM_RECEIVED, value);
            GameAnalytics.LogFirebaseUserProperty("total_gem_receive", value);
        }
        get => PlayerPrefs.GetInt("total_gem_receive", 0);
    }
    public static int CoinReceived
    {
        set
        {
            PlayerPrefs.SetInt(Constants.KEY.UP_COIN_RECEIVED, value);
            GameAnalytics.LogFirebaseUserProperty("total_coins_receive", value);
        }
        get => PlayerPrefs.GetInt("total_coins_receive", 0);

    }

    public static int TotalWatchedInter
    {
        get => PlayerPrefs.GetInt("total_watched_inter", 0);
        set
        {
            PlayerPrefs.SetInt("total_watched_inter", value);
            GameAnalytics.LogFirebaseUserProperty("total_inta", value);
        }
    }

    public static int TotalWatchedReward
    {
        get => PlayerPrefs.GetInt("total_watched_reward", 0);
        set
        {
            PlayerPrefs.SetInt("total_watched_reward", value);
            GameAnalytics.LogFirebaseUserProperty("total_rewa", value);
        }
    }
}

[Serializable]
public class GameDataSave
{
    public bool firstOpen = true;
    public bool doubleJump;
    public bool rated;

    public int coin = 50;
    public int life = 3;
    public int gem;
    public int currentLevel = 1;

    public bool vibrate = true;
    public bool sound = true;
    public bool music = true;

    public int dateTimeDaily = 0;
    public int dayDaily = 0;

    public int dateTimeSpin = 0;

    // Upgrade
    public int levelUpgrade = 1;
    public int coinUpgrade = 50;
    public int atkPlayer = 20;
    public int hpPlayer = 150;
    //public int adsGiftCount;

    public int skinSellect = 0;
    public int weaponSellect = 0;

    public int trySkin = -1;
    public int tryWeapon = -1;
    public List<SkinUnlock> listSkinUnlock = new List<SkinUnlock>();
    public List<WeaponUnlock> listWeaponUnlock = new List<WeaponUnlock>();
    public List<IDCardGifts> listIdCardGifts = new List<IDCardGifts>();
}

[Serializable]
public class SkinUnlock
{
    public int ID = 0;
}
[Serializable]
public class WeaponUnlock
{
    public int ID = 0;
}
[Serializable]
public class IDCardGifts
{
    public int id = 0;
    public int count = 0;
}

public class ResourceTypeEvent : UnityEvent<ResourceType>
{

}

public class ResourceDetailChangedEvent : UnityEvent<ResourceDetail>
{

}
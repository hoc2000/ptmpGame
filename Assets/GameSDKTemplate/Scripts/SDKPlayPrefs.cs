using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SDKPlayPrefs {
    public static readonly DateTime zeroTime = new DateTime(0);

    public static DateTime GetDateTime(string key, DateTime def) {
        string @string = PlayerPrefs.GetString(key);
        DateTime result = def;
        if (!string.IsNullOrEmpty(@string)) {
            long dateData = Convert.ToInt64(@string);
            result = DateTime.FromBinary(dateData);
        }
        return result;
    }

    public static void SetDateTime(string key, DateTime val) {
        PlayerPrefs.SetString(key, val.ToBinary().ToString());
    }

    public static int GetCurrentPictureVersion(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_CURRENT_PICTURE_VERSION_PREFIX + pictureId, -1);
    }

    public static void SetCurrentPictureVersion(int pictureId, int version) {
        PlayerPrefs.SetInt(StringConstants.PREF_CURRENT_PICTURE_VERSION_PREFIX + pictureId, version);
    }

    public static bool PreloadPictureDataInitialized {
        get {
            return PlayerPrefs.GetInt(StringConstants.PREF_PRELOAD_PICTURE_DATA_INITIALIZED, 0) == 1;
        }
        set {
            PlayerPrefs.SetInt(StringConstants.PREF_PRELOAD_PICTURE_DATA_INITIALIZED, value ? 1 : 0);
        }
    }

    public static bool GetPictureModified(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PICTURE_MODIFIED_PREFIX + pictureId, 0) == 1;
    }

    public static void SetPictureModified(int pictureId, bool modified) {
        PlayerPrefs.SetInt(StringConstants.PREF_PICTURE_MODIFIED_PREFIX + pictureId, modified ? 1 : 0);
    }

    public static bool GetPictureCompleted(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PICTURE_COMPLETED_PREFIX + pictureId, 0) == 1;
    }

    public static void SetPictureCompleted(int pictureId, bool completed) {
        PlayerPrefs.SetInt(StringConstants.PREF_PICTURE_COMPLETED_PREFIX + pictureId, completed ? 1 : 0);
    }
    public static bool GetPictureSurprise(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PICTURE_SURPRISE + pictureId, 0) == 1;
    }
    public static void SetPictureSurprise(int pictureId, bool surprise) {
        PlayerPrefs.SetInt(StringConstants.PREF_PICTURE_SURPRISE + pictureId, surprise ? 1 : 0);
    }
    public static DateTime GetPictureStartedAt(int pictureId) {
        return GetDateTime(StringConstants.PREF_PICTURE_STARTED_AT_PREFIX + pictureId, zeroTime);
    }
    #region Achievement
    public static bool GetQuestItemCompleted(string questId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_QUEST_COMPLETED + questId, 0) == 1;
    }
    public static void SetQuestItemCompleted(string questId, bool completed) {
        PlayerPrefs.SetInt(StringConstants.PREF_QUEST_COMPLETED + questId, completed ? 1 : 0);
    }
    public static int GetQuestItemProcess(string questId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_QUEST_PROCESS + questId, 0);
    }
    public static void SetQuestItemProcess(string questId, int value) {
        PlayerPrefs.SetInt(StringConstants.PREF_QUEST_PROCESS + questId, value);
    }
    public static bool GetQuestItemClaimed(string questId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_QUEST_CLAIMED + questId, 0) == 1;
    }
    public static void SetQuestItemClaimed(string questId, bool claimed) {
        PlayerPrefs.SetInt(StringConstants.PREF_QUEST_CLAIMED + questId, claimed ? 1 : 0);
    }
    #endregion
    public static void UpdatePictureStartedAt(int pictureId) {
        SetDateTime(StringConstants.PREF_PICTURE_STARTED_AT_PREFIX + pictureId, DateTime.Now);
    }

    public static int GetPictureWatchedAdsCount(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PICTURE_WATCHED_ADS_COUNT_PREFIX + pictureId, 0);
    }

    public static void IncreasePictureWatchedAdsCount(int pictureId) {
        PlayerPrefs.SetInt(StringConstants.PREF_PICTURE_WATCHED_ADS_COUNT_PREFIX + pictureId, GetPictureWatchedAdsCount(pictureId) + 1);
    }

    public static void UpdateTopBannerABVersion(int version) {
        PlayerPrefs.SetInt(StringConstants.PREF_TOP_BANNER_AB_VERSION, version);
    }

    public static int GetTopBannerABVersion() {
        return PlayerPrefs.GetInt(StringConstants.PREF_TOP_BANNER_AB_VERSION, 0);
    }

    public static DateTime GetPictureCreatedAt(int pictureId) {
        return GetDateTime(StringConstants.PREF_LOCAL_PICTURE_CREATED_AT_PREFIX + pictureId, zeroTime);
    }

    public static void UpdateLocalPictureCreatedAt(int pictureId) {
        SetDateTime(StringConstants.PREF_LOCAL_PICTURE_CREATED_AT_PREFIX + pictureId, DateTime.Now);
    }

    public static void SetPictureUnlocked(int pictureId, bool value) {
        PlayerPrefs.SetInt(StringConstants.PREF_PICTURE_UNLOCKED_PREFIX + pictureId, value ? 1 : 0);
    }

    public static bool IsPictureUnlocked(int pictureId) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PICTURE_UNLOCKED_PREFIX + pictureId, 0) == 1;
    }
    public static bool GetGiftClaim(string code) {
        return PlayerPrefs.GetInt(StringConstants.PREF_GIFT_CODE + code, 0) == 1;
    }

    public static void SetGiftClaim(string code, bool claimed) {
        PlayerPrefs.SetInt(StringConstants.PREF_GIFT_CODE + code, claimed ? 1 : 0);
    }
    public static bool GetDynamicLinksClaim(string link) {
        return PlayerPrefs.GetInt(StringConstants.PREF_DYNAMIC_LINK + link, 0) == 1;
    }

    public static void SetDynamicLinksClaim(string link, bool claimed) {
        PlayerPrefs.SetInt(StringConstants.PREF_DYNAMIC_LINK + link, claimed ? 1 : 0);
    }
    public static bool GetPicPainted(int id) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PIC_PAINTED + id, 0) == 1;
    }

    public static void SetPicPainted(int id, bool painted) {
        PlayerPrefs.SetInt(StringConstants.PREF_PIC_PAINTED + id, painted ? 1 : 0);
    }
    public static bool GetPicShared(int id) {
        return PlayerPrefs.GetInt(StringConstants.PREF_PIC_SHARED + id, 0) == 1;
    }

    public static void SetPicShared(int id, bool shared) {
        PlayerPrefs.SetInt(StringConstants.PREF_PIC_SHARED + id, shared ? 1 : 0);
    }
}


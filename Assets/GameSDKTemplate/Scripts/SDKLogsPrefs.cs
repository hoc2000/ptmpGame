using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserHeaderInfo
{
    public int PLayerID;
    public int SessionID;
    public int EngageDay;
    public int DayFromInstall;
    public int Level;
    public string Source;

}

public class SDKLogsPrefs
{

    public const string PREF_FIRST_RUN = "first_run";
    public const string PREF_FIRST_OPENTIME = "first_open_time";
    public const string PREF_SESSION_ID = "session_id";
    public const string PREF_ENGAGE_DAY = "engage_day";
    public const string PREF_DAY_FROM_INSTALL = "day_from_install";
    public const string PREF_SOURCE = "from_source";
    public const string PREF_IS_INTER_IN_GAME_SHOW = "inter_in_game";

    public static bool firstOpen
    {
        set
        {
            SDKPlayerPrefs.SetBoolean(PREF_FIRST_RUN, value);
        }
        get
        {
            return SDKPlayerPrefs.GetBoolean(PREF_FIRST_RUN, true);
        }
    }

    public static DateTime firstOpenTime
    {
        set
        {
            SDKPlayerPrefs.SetDateTime(PREF_FIRST_OPENTIME, value);
        }
        get
        {
            return SDKPlayerPrefs.GetDateTime(PREF_FIRST_OPENTIME, DateTime.Now.Subtract(TimeSpan.FromDays(1)));
        }
    }

    public static int SessionID
    {
        set
        {
            SDKPlayerPrefs.SetInt(PREF_SESSION_ID, value);
        }
        get
        {
            return SDKPlayerPrefs.GetInt(PREF_SESSION_ID, -1);
        }
    }
    public static int EngageDay
    {
        set
        {
            SDKPlayerPrefs.SetInt(PREF_ENGAGE_DAY, value);
        }
        get
        {
            return SDKPlayerPrefs.GetInt(PREF_ENGAGE_DAY, -1);
        }
    }

    public static int DayFromInstall
    {
        set
        {
            SDKPlayerPrefs.SetInt(PREF_DAY_FROM_INSTALL, value);
        }
        get
        {
            return SDKPlayerPrefs.GetInt(PREF_DAY_FROM_INSTALL, -1);
        }
    }

    public static string Source
    {
        set
        {
            SDKPlayerPrefs.SetString(PREF_SOURCE, value);
        }
        get
        {
            return SDKPlayerPrefs.GetString(PREF_SOURCE, "Default");
        }
    }

    public static bool isInterInGame
    {
        set
        {
            SDKPlayerPrefs.SetBoolean(PREF_IS_INTER_IN_GAME_SHOW, value);
        }
        get
        {
            return SDKPlayerPrefs.GetBoolean(PREF_IS_INTER_IN_GAME_SHOW, false);
        }
    }
}

public class SDKPlayerPrefs
{
    public static DateTime GetDateTime(string key, DateTime def)
    {
        string @string = PlayerPrefs.GetString(key);
        DateTime result = def;
        if (!string.IsNullOrEmpty(@string))
        {
            long dateData = Convert.ToInt64(@string);
            result = DateTime.FromBinary(dateData);
        }
        return result;
    }

    public static void SetDateTime(string key, DateTime val)
    {
        PlayerPrefs.SetString(key, val.ToBinary().ToString());
    }

    public static void SetInt(string Prefs, int _Value)
    {
        PlayerPrefs.SetInt(Prefs, _Value);
    }

    public static int GetInt(string Prefs, int _defaultValue = 0)
    {
        return PlayerPrefs.GetInt(Prefs, _defaultValue);
    }

    public static void SetString(string Prefs, string _Value)
    {
        PlayerPrefs.SetString(Prefs, _Value);
    }

    public static string GetString(string Prefs, string _defaultValue = "")
    {
        return PlayerPrefs.GetString(Prefs, _defaultValue);
    }

    public static void SetBoolean(string Prefs, bool _Value)
    {
        PlayerPrefs.SetInt(Prefs, _Value ? 1 : 0);
    }

    public static bool GetBoolean(string Prefs, bool _defaultValue = false)
    {
        if (!PlayerPrefs.HasKey(Prefs))
        {
            SetBoolean(Prefs, _defaultValue);
        }
        if (PlayerPrefs.GetInt(Prefs) == 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

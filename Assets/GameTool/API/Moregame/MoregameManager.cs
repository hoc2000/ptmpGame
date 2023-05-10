//using System;
//using System.Collections;
//using System.Collections.Generic;
//using Firebase.Analytics;
//using Firebase.RemoteConfig;
//using UnityEngine;

//public class MoregameManager : MonoBehaviour
//{
//    public bool IsAddFeatureGameToRandomList = false;
//    public const string StoreLinkPrefix = "https://play.google.com/store/apps/details?id=";
//    public static MoregameManager Instance;
//    public static Action OnMoregameLoaded;
//    public static bool IsMoregameLoaded;
//    private List<MoregameInfor> _moregameCanUse = new List<MoregameInfor>();
//    private List<MoregameInfor> _moregameUsed = new List<MoregameInfor>();

//    public Dictionary<string, Texture2D> MoregameLoaded = new Dictionary<string, Texture2D>();
//    // public Dictionary<string, Texture2D> FullMoregameLoaded = new Dictionary<string, Texture2D>();

//    Texture2D featureImage;
//    MoregameInfor featureItem;


//    [Header("RUNTIME DATAS")] public MoregameInfors MoregameInfors;

//    [Space(20)] public string AndroidSheetId = "1Ef2aMquCb8HAknXQ4WhzUZszt79XTGBCg3aEMPBOVaI";
//    public string AndroidGridId = "580411749";
//    [Header("DEFAULT DATAS")] public MoregameInfors AndroidMoregameInfors;

//    [Space(20)] public string IOSSheetId = "15uJ4wXPY3p_B-V6Bh5pWXJDhIWlwD1ndAtRKd8yyHQc";
//    public string IOSGridId = "0";
//    public MoregameInfors IosMoregameInfors;
//    public GameObject MoregameFullPrefab_Landscape;
//    public GameObject MoregameFullPrefab_Portrait;

//    bool FullMoregameShowed;

//    bool openFirstTime;
//    private void Awake()
//    {
//        int hasPlayed = PlayerPrefs.GetInt("FirstOpenMoreGame");

//        if (hasPlayed == 0)
//        {
//            PlayerPrefs.SetInt("FirstOpenMoreGame", 1);
//            openFirstTime = true;
//        }
//        else
//        {
//            openFirstTime = false;
//        }


//        if (Instance == null)
//        {
//            Instance = this;
//            SDK.OnSetDefault += InitDefaultMoregame;
//            SDK.OnFetchCompleted += LoadMoregame;
//        }
//        else
//            Destroy(gameObject);
//    }

//    private void LoadMoregame()
//    {
//        //string key = GetKey();

//        //string data = FirebaseRemoteConfig.DefaultInstance.GetValue(GetKey()).StringValue;
//        //data = data.CorrectString();
//        //SDK.Instance.ApiDebug(string.Format("FirebaseRemote - key {0}\n{1}", key, data));
//        //MoregameInfors = JsonUtility.FromJson<MoregameInfors>(data);
//        //var mores = MoregameInfors.Moregames;
//        //_moregameCanUse.Clear();
//        //for (int i = 0; i < mores.Count; i++)
//        //{
//        //    if (!SDK.Instance.IsAppInstalled(GetPackage(mores[i].GameLink)))
//        //    {
//        //        _moregameCanUse.Add(mores[i]);
//        //    }
//        //}
//        //// _moregameCanUse.AddRange(mores);

//        //featureItem = _moregameCanUse.Find(x => x.Feature);
//        //if (featureItem == null)
//        //{
//        //    foreach (var item in _moregameCanUse)
//        //    {
//        //        if (( IsLanscape() ? item.FullLink_Landscape.CorrectString(): item.FullLink_Portrait.CorrectString()) != string.Empty)
//        //        {
//        //            featureItem = item;
//        //            break;
//        //        }
//        //    }
//        //}
//        //if (featureItem != null)
//        //    _moregameCanUse.Remove(featureItem);
//        //_moregameCanUse.Shuffle();
//        //IsMoregameLoaded = true;
//        //LoadMoregameFull();
//    }

//    private void InitDefaultMoregame(Dictionary<string, object> defaults)
//    {
//        if (SDK.IsAndroid())
//            defaults.Add(GetKey(), JsonUtility.ToJson(AndroidMoregameInfors));
//        else if (SDK.IsIOS())
//            defaults.Add(GetKey(), JsonUtility.ToJson(IosMoregameInfors));
//    }

//    public void GetGame(MoregameItemUi item)
//    {
//        if (item.IsFeature && featureItem != null)
//        {
//            item.Infor = featureItem;
//        }
//        else
//        {
//            if (_moregameCanUse.Count <= 0)
//            {
//                if (_moregameUsed.Count > 0)
//                {
//                    _moregameCanUse.Add(_moregameUsed[0]);
//                    _moregameUsed.RemoveAt(0);
//                }
//            }

//            if (_moregameCanUse.Count > 0)
//            {
//                item.Infor = _moregameCanUse[UnityEngine.Random.Range(0, _moregameCanUse.Count)];
//                _moregameCanUse.Remove(item.Infor);
//                _moregameUsed.Add(item.Infor);
//            }
//        }

//        if (item.Infor.IconLink != null)
//        {
//            item.IsLoading = true;
//            string package = GetPackage(item.Infor.GameLink);
//            if (MoregameLoaded.ContainsKey(package))
//                item.ApplyMoregameInfor(MoregameLoaded[package]);
//            else
//                StartCoroutine(TryLoadGame(item));
//        }
//    }

//    public IEnumerator TryLoadGame(MoregameItemUi item)
//    {
//        WWW w = new WWW(item.Infor.IconLink);
//        yield return w;

//        if (w.error == null)
//        {
//            Texture2D t = new Texture2D(w.texture.width, w.texture.height, TextureFormat.RGB24, false);
//            w.LoadImageIntoTexture(t);
//            string package = GetPackage(item.Infor.GameLink);
//            if (!MoregameLoaded.ContainsKey(package))
//                MoregameLoaded.Add(package, t);
//            item.ApplyMoregameInfor(t);
//        }
//        else
//            item.IsLoading = false;
//    }

//    private string GetPackage(string link)
//    {
//        if (SDK.IsIOS())
//            return link;
//        return link.Replace(StoreLinkPrefix, string.Empty);
//    }

//    public void OnClick(MoregameItemUi item)
//    {
//        string link = item.Infor.GameLink;

//        if (SDK.IsAndroid())
//            link = StoreLinkPrefix + GetPackage(link);

//        Application.OpenURL(link);
//    }

//    public string GetKey()
//    {
//        string key = "moregameInfor";
//        if (SDK.IsAndroid())
//            key += "_android";
//        else if (SDK.IsIOS())
//            key += "_ios";
//        return key;
//    }

//    public void LoadMoregameFromGoogleSheet(MoregameInfors moregameInfors, bool isAnroid)
//    {
//        moregameInfors.Moregames.Clear();
//        string sheetId = string.Empty;
//        string gridId = string.Empty;

//        if (isAnroid)
//        {
//            sheetId = AndroidSheetId;
//            gridId = AndroidGridId;
//        }
//        else
//        {
//            sheetId = IOSSheetId;
//            gridId = IOSGridId;
//        }

//        GetTable(sheetId, gridId, list =>
//        {
//            for (int i = 1; i < list.Count; i++)
//            {
//                if (list[i][0] != string.Empty)
//                {
//                    moregameInfors.Moregames.Add(new MoregameInfor
//                    {
//                        GameName = list[i][0],
//                        GameLink = list[i][1],
//                        IconLink = list[i][2],
//                        FullLink_Landscape = list[i][4],
//                        FullLink_Portrait = list[i][5],
//                        Feature = list[i][3].ToLower() == "true"
//                    });

//                    if (!moregameInfors.Moregames[moregameInfors.Moregames.Count - 1].IconLink.Contains(".png"))
//                        moregameInfors.Moregames[moregameInfors.Moregames.Count - 1].IconLink += ".png";
//                }
//            }
//        });
//    }

//    public static void GetTable(string id, string gridId, Action<List<List<string>>> callBack)
//    {
//        LoadWebClient(id, gridId, s =>
//        {
//            List<List<string>> listStr = new List<List<string>>();
//            s = s.Replace("\r", string.Empty);
//            var arr = s.Split('\n');
//            for (int i = 0; i < arr.Length; i++)
//            {
//                var subArr = arr[i].Split(',');
//                if (subArr[0] == string.Empty)
//                    break;

//                List<string> row = new List<string>();
//                row.AddRange(subArr);
//                listStr.Add(row);
//            }

//            callBack(listStr);
//        });
//    }

//    public static void LoadWebClient(string id, string gridId, Action<string> callBack)
//    {
//        string url = string.Format(
//            @"https://docs.google.com/spreadsheet/ccc?key={0}&usp=sharing&output=csv&id=KEY&gid={1}", id, gridId);
//        LoadWebClient3(url, callBack);
//    }

//    public static void LoadWebClient3(string id, Action<string> callBack)
//    {
//        WWW w = new WWW(id);

//        while (!w.isDone)
//            w.MoveNext();

//        Debug.Log(w.text);
//        callBack(w.text);
//    }

//    public void LoadMoregameFull()
//    {
//        FullMoregameShowed = false;
//        if (featureItem != null && (IsLanscape() ? featureItem.FullLink_Landscape.CorrectString() : featureItem.FullLink_Portrait.CorrectString()) != string.Empty)
//        {
//            StartCoroutine(TryLoadFullMoreGame(featureItem));
//        }
//        else
//        {

//        }
//    }

//    public IEnumerator TryLoadFullMoreGame(MoregameInfor item)
//    {
//        WWW w = new WWW(url: IsLanscape() ? item.FullLink_Landscape.CorrectString() : item.FullLink_Portrait.CorrectString());
//        yield return w;

//        if (w.error == null)
//        {
//            Texture2D t = new Texture2D(w.texture.width, w.texture.height, TextureFormat.RGB24, false);
//            w.LoadImageIntoTexture(t);
//            string package = GetPackage(item.GameLink);
//            // if (!FullMoregameLoaded.ContainsKey(package))
//            //     FullMoregameLoaded.Add(package, t);
//            featureImage = t;


//            Debug.Log("Moregame full loaded");


//        }
//    }

//    public bool IsLanscape()
//    {
//        return Screen.width > Screen.height;
//    }

//    public void ShowMoregameFull(Action callBack)
//    {
//        if (FullMoregameShowed || openFirstTime)
//        {
//            return;
//        }

//        if (featureImage == null)
//            callBack();
//        else
//        {
//            var moregameFull = Instantiate(IsLanscape() ? MoregameFullPrefab_Landscape : MoregameFullPrefab_Portrait);
//            var full = moregameFull.GetComponent<MoreGameFullManager>();
//            full.Infor = featureItem;
//            full.ShowMoregame(featureImage);
//            FullMoregameShowed = true;
//        }
//    }

//    public void OnClick(MoreGameFullManager item)
//    {
//        string link = item.Infor.GameLink;

//        if (SDK.IsAndroid())
//            link = StoreLinkPrefix + GetPackage(link);

//        Application.OpenURL(link);
//    }
//}

//[Serializable]
//public class MoregameInfors
//{
//    // public MoregameInfor Feature;
//    public List<MoregameInfor> Moregames = new List<MoregameInfor>();
//}

//[Serializable]
//public class MoregameInfor
//{
//    public string GameName;
//    public string IconLink;
//    public string GameLink;
//    public string FullLink_Portrait;
//    public string FullLink_Landscape;
//    public Boolean Feature;
//}
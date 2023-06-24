//using System.Collections;
//using System.Collections.Generic;
//using Firebase.Analytics;
//using UnityEngine;
//using UnityEngine.UI;

//public class MoregameItemUi : MonoBehaviour
//{
//    public bool UseNativeSize;
//    public bool IsFeature;
//    public RawImage IconView;
//    public MoregameInfor Infor;
//    public bool IsLoading;

//    private bool _isInited;

//    private void OnEnable()
//    {
//        IconView.enabled = false;
//        if (_isInited)
//            LoadMoregame();
//    }

//    private void Start()
//    {
//        _isInited = true;
//        LoadMoregame();
//    }

//    public void LoadMoregame()
//    {
//        if (MoregameManager.Instance != null && !IsLoading)
//        {
//            if (MoregameManager.IsMoregameLoaded)
//                MoregameManager.Instance.GetGame(this);
//            else
//                StartCoroutine(DelayGetMore());
//        }
//    }

//    public IEnumerator DelayGetMore()
//    {
//        yield return new WaitUntil(() => MoregameManager.IsMoregameLoaded);
//        MoregameManager.Instance.GetGame(this);
//    }


//    public void ApplyMoregameInfor(Texture texture)
//    {
//        IsLoading = false;
//        IconView.texture = texture;
//        IconView.enabled = true;
//        if (UseNativeSize)
//            IconView.SetNativeSize();

//        FirebaseAnalytics.LogEvent("MorgameAPI", "Show", Infor.GameName);
//    }

//    public void OnClick()
//    {
//        MoregameManager.Instance.OnClick(this);
//        FirebaseAnalytics.LogEvent("MorgameAPI", "Click", Infor.GameName);
//    }
//}
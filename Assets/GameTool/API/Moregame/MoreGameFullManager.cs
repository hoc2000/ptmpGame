//using System.Collections;
//using System.Collections.Generic;
//using Firebase.Analytics;
//using UnityEngine;
//using UnityEngine.UI;

//public class MoreGameFullManager : MonoBehaviour
//{
//    public MoregameInfor Infor;
//    public RawImage Img;

//    [SerializeField]
//    GameObject closeBtn;
//    /// <summary>
//    /// Awake is called when the script instance is being loaded.
//    /// </summary>
//    void Awake()
//    {
//        DontDestroyOnLoad(gameObject);
//    }
//    private void OnEnable()
//    {
//        closeBtn.SetActive(false);
//        Invoke("ActiveCloseBtn", 3);
//    }

//    void ActiveCloseBtn()
//    {
//        closeBtn.SetActive(true);
//    }

//    public void ButtonCloseClick()
//    {
//        Destroy(gameObject);
//    }

//    public void ButtonDownloadClick()
//    {
//        FirebaseAnalytics.LogEvent("MorgameAPI", "FullClick", Infor.GameName);
//        MoregameManager.Instance.OnClick(this);
//    }

//    public void ShowMoregame(Texture sprite)
//    {
//        FirebaseAnalytics.LogEvent("MorgameAPI", "FullShow", Infor.GameName);
//        Img.texture = sprite;
//    }
//}
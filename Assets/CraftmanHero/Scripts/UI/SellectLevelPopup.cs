using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SellectLevelPopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] GameObject popup;

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI txtCurrentLevel;



    [SerializeField] private ElementLevel elementLevel;
    public const string LEVEL = "GamePlay";

    private void OnEnable()
    {
        Init();
    }
    void Start()
    {
        AddEventButton();
    }

    #region INIT
    public void Init()
    {
        GameAnalytics.LogUIAppear("popup_select_level", "HomeScene");
        txtCurrentLevel.text = Gamedata.I.CurrentLevel + "/" + GameConfig.Instance.valueConfrig.levelMax;
    }

    void AddEventButton()
    {
        closeButton.onClick.AddListener(() => CloseClick());
    }
    #endregion
  
    #region BUTTON
    public void CloseClick()
    {
        this.Pop();
    }
    #endregion


}

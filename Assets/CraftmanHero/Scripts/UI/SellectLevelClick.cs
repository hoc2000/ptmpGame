using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using My.Tool;
using UnityEngine;
using UnityEngine.UI;

public class SellectLevelClick : MonoBehaviour
{
    [SerializeField] int level;
    [SerializeField] GameObject checkComplete;
    [SerializeField] GameObject lockObj;
    [SerializeField] TextMeshProUGUI levelText;
    bool isLock;

    [SerializeField] Button sellectButton;

    private void OnEnable()
    {
        Init();
        this.RegisterListener(EventID.UpdateData, (sender, param) => Init());
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.UpdateData, (sender, param) => Init());
    }
    private void Start()
    {
        AddEventButton();
    }
    void Init()
    {
        levelText.text = level.ToString();
        if (Gamedata.I.CurrentLevel < level)
        {
            isLock = true;
            checkComplete.SetActive(false);
            lockObj.SetActive(true);
            levelText.gameObject.SetActive(false);
        }
        else if (Gamedata.I.CurrentLevel == level)
        {
            isLock = false;
            lockObj.SetActive(false);
            checkComplete.SetActive(false);
            levelText.gameObject.SetActive(true);
        }
        else
        {
            isLock = false;
            lockObj.SetActive(false);
            checkComplete.SetActive(true);
            levelText.gameObject.SetActive(true);
        }
    }
    void AddEventButton()
    {
        if (!isLock)
        {
            sellectButton.onClick.AddListener(() => SellectClick());
        }

    }
    void SellectClick()
    {
        //if (SDK.Instance.valueConfrig.showInterPlayClick)
        //{
        //    SDK.Instance.ShowInter((success) =>
        //    {
        //        string levelText = "Level_" + this.level;
        //        LoadSceneManager.Instance.LoadScene(levelText);

        //    }, RewardID.SellectLevel);
        //}
        //else
        //{
        //    string levelText = "Level_" + this.level;
        //    LoadSceneManager.Instance.LoadScene(levelText);
        //}
    }
}

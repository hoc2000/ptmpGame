using System.Collections;
using System.Collections.Generic;
using TMPro;
using DG.Tweening;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using My.Tool;
using UnityEngine.Playables;

public class GamePlayPanel : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button pauseButton;
    [Header("HEART BAR")]
    [SerializeField] GameObject[] heartImg;
    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI lifeText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI enemiesCountText;
    [SerializeField] Slider levelProgressBar;


    private void OnEnable()
    {
        Gamedata.inGame = true;
        this.RegisterListener(EventID.EnemyDeath, (sender, param) => UpdateEemiesCoutText());
    }
    private void OnDisable()
    {
        Gamedata.inGame = false;
        this.RemoveListener(EventID.EnemyDeath, (sender, param) => UpdateEemiesCoutText());
    }
    void Start()
    {
        UpdateEemiesCoutText();
        AddEventButton();
        StartCoroutine(CountTime());
    }
    void AddEventButton()
    {
        pauseButton.onClick.AddListener(() => PauseClick());
    }

    private IEnumerator CountTime()
    {
        while (!GameManager.isWin && !Player.Instance.IsDeath())
        {
            Gamedata.levelPlayedTime++;
            yield return new WaitForSeconds(1);
        }
    }

    #region INIT
    void ShowLevelText()
    {
        levelText.text = "Level " + 3;
    }
    void UpdateEemiesCoutText()
    {
        enemiesCountText.text = LevelManager.Instance.enemiesDie + "/" + LevelManager.Instance.allEnemyInMap;
    }
    #endregion

    #region BUTTON
    public void PauseClick()
    {
        CanvasManager.Push(GlobalInfo.PausePopup, null);
        Time.timeScale = 0;
    }
    #endregion
}

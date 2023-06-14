using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool.UI;
using System;
//using static Cinemachine.DocumentationSortingAttribute;

public class LevelManager : SingletonMonoBehaviour<LevelManager>
{
    public PlayerPos player;
    public Transform checkPoin;
    public int allEnemyInMap;
    public int enemiesDie;

    public bool winLevel, die;

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;
        LoadLevel();
    }
    void Start()
    {
        Time.timeScale = 1;
        GameManager.coinCollect = 0;
        var gamePlaypanel = CanvasManager.Init(GlobalInfo.UIDefaultPath, GlobalInfo.GamePlayPanel);

        Player.Instance.hpbar = gamePlaypanel.GetComponent<HpBar>();

        player = PlayerPos.instance;
        SetPosPlayerStart();
        GameAnalytics.LogLevelStart(GameManager.levelSelected, GameManager.startFrom, "default", !Gamedata.isLevelPassed, allEnemyInMap);
    }

    // Update is called once per frame
   
    public void LoadLevel()
    {
        Instantiate(Resources.Load("Level/Level" + GameManager.levelSelected));
    }
    void SetPosPlayerStart()
    {
        player.transform.position = LevelSetup.instance.posStartPlayer.position;
        checkPoin = LevelSetup.instance.posStartPlayer;

    }

    public void Death()
    {
        CanvasManager.Push(GlobalInfo.RevivePopup, null);
    }

  
    public void Revive()
    {
        Player.Instance.Revive();
        
    }

    public void Win()
    {

        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;
        GameManager.isWin = true;
        if (Gamedata.I.CurrentLevel == GameManager.levelSelected)
        {

            Gamedata.I.CurrentLevel++;
            

        }

        if (Gamedata.I.CurrentLevel > 2)
        {
            if (Gamedata.I.DateTimeDaily < DateTime.Now.Day)
            {
                CanvasManager.Push(GlobalInfo.DailyRewardPopup, null);
            }
            // else if (OneDaySPIN())
            // {
            //     CanvasManager.Push(GlobalInfo.SpinPopup, null);
            // }
            else
            {
                LoadSceceManager.Instance.LoadHome();
            }
        }
        else
        {
            LoadSceceManager.Instance.LoadHome();
        }



    }

    public bool OneDaySPIN()
    {
        TimeSpan timeSpan = DateTime.Now - Gamedata.LastSpinFree;
        if (timeSpan.TotalDays >= 0)
        {
            Gamedata.LastSpinFree = DateTime.Now.AddDays(1);
            return true;
        }
        else
        {
            TimeSpan time = Gamedata.LastSpinFree - DateTime.Now;
            return false;
        }
    }
}

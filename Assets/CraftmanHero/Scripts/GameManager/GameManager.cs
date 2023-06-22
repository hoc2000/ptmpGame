using My.Tool.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public UpgradePlayerData upgradePlayerData;
    public static int levelSelected;
    public static bool isWin;
    public static int coinCollect;
    public static bool winLevel;
    public int currentSelectedLevel;
    [SerializeField] private ShowInterstitialController showInterstitialController;
    private float t1, t2;
    public DataHolder dataHolder;
    public static string startFrom;

    public override void Awake()
    {
        base.Awake();
        Application.targetFrameRate = 60;

    }

    public void BackMap()
    {
        Time.timeScale = 1;
        //Gamedata.I.startLevel = false;
        //ResetCacheData();
        SceneManager.LoadSceneAsync(Constants.SCENE.SCENE_HOME);
       

    }
    private void Start()
    {
        Gamedata.ClaimCharacter(ResourceDetail.CharacterPhung);
    }
    public void ResetCacheData()
    {
        //Gamedata.curScore = 0;
        //GameData.curScoreRevive = 0;
        //GameData.bossterHealth = false;
        //GameData.bossterDamage = false;
        //GameData.revive = false;
        //GameData.curScoreCheckPoint = 0;
        //GameData.checkPoint = false;
        //GameData.objectsId = new List<int>();
        //GameData.curCoins = 0;
        Gamedata.I.Life = Gamedata.I.Life;
    }
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
			t1= Time.realtimeSinceStartup;

        }
    }
    private void OnApplicationFocus(bool focus)
    {
        if (focus)
        {
            t2 = Time.realtimeSinceStartup;
            if(t2 - t1 >= 30)
            {
                showInterstitialController.Show();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using My.Tool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceceManager : SingletonMonoBehaviour<LoadSceceManager>
{
    public const string HOMESCENE = "HomeScene";
    public const string LEVEL = "GamePlay";
    public const string UI = "HomeScene";
    private void Start()
    {
        Gamedata.I.RegisterSaveData();
        SaveGameManager.I.Load();
    }
    public void LoadHome()
    {
        SceneManager.LoadScene(HOMESCENE);

    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(LEVEL);
    }
}

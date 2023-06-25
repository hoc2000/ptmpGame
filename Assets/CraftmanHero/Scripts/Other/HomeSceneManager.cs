using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool.UI;
using Spine.Unity;
using System.Diagnostics;
using DarkTonic.MasterAudio;
using System;

public class HomeSceneManager : SingletonMonoBehaviour<HomeSceneManager>
{
    [SerializeField] GameObject[] decorMap;
    [SerializeField] private SkeletonAnimation charAnim;
    [SpineAnimation] private string winAnim = "win UI";
    [SerializeField] private GameObject debugConsole;
    [SerializeField]
    [Playlist]
    private string introPlaylist;

    [SerializeField]
    private SkeletonAnimation skeletonGraphic;

    public GameObject confetti;

    void Start()
    {
       
        Init();
        MasterAudio.StartPlaylist(introPlaylist);
#if ENV_PROD
        debugConsole.SetActive(false);
#else
        debugConsole.SetActive(true);
#endif
       
    }

    void Init()
    {
        CanvasManager.Init(GlobalInfo.UIDefaultPath, GlobalInfo.HomeMenu);
        ActiveDecorMap();
        UpdateSelectedCharacterAnim();
        Gamedata.selectedCharacterChanged.AddListener(UpdateSelectedCharacterAnim);
        DateTime now = DateTime.Now;
        Gamedata.LastOpened = now;
        if (Gamedata.IsFirstOpen)
        {
            Gamedata.ClaimCharacter(ResourceDetail.CharacterPhung);
            Gamedata.SelectedCharacter = ResourceDetail.CharacterPhung;
            Gamedata.FirstOpenTime = now;
            //GameAnalytics.LogFirebaseUserProperty("level", GameData.LevelUnlock);
            //GameAnalytics.LogFirebaseUserProperty(Constants.KEY.EVENT_NOEL + "level", GameData.LevelUnlockNoel);
        }
    }
    private void OnDestroy()
    {
        Gamedata.selectedCharacterChanged.RemoveListener(UpdateSelectedCharacterAnim);
    }

    void ActiveDecorMap()
    {
        if (Gamedata.I.CurrentLevel <=4)
        {
            decorMap[0].SetActive(true);
            decorMap[1].SetActive(false);
            decorMap[2].SetActive(false);
        }
        else if (Gamedata.I.CurrentLevel > 4 && Gamedata.I.CurrentLevel <= 10)
        {
            decorMap[0].SetActive(false);
            decorMap[1].SetActive(true);
            decorMap[2].SetActive(false);
        }
        else if (Gamedata.I.CurrentLevel > 10 && Gamedata.I.CurrentLevel <= 15)
        {
            decorMap[0].SetActive(false);
            decorMap[1].SetActive(false);
            decorMap[2].SetActive(true);
        }
        else if (Gamedata.I.CurrentLevel > 15 && Gamedata.I.CurrentLevel <= 20)
        {
            decorMap[0].SetActive(true);
            decorMap[1].SetActive(false);
            decorMap[2].SetActive(false);
        }
        else if (Gamedata.I.CurrentLevel > 20 && Gamedata.I.CurrentLevel <= 25)
        {
            decorMap[0].SetActive(false);
            decorMap[1].SetActive(true);
            decorMap[2].SetActive(false);
        }
        else if (Gamedata.I.CurrentLevel > 25)// && Gamedata.I.CurrentLevel <= 30)
        {
            decorMap[0].SetActive(false);
            decorMap[1].SetActive(false);
            decorMap[2].SetActive(true);
        }
    }
    private void UpdateSelectedCharacterAnim()
    {
        CharacterInfo characterInfo = Gamedata.SelectedCharacterInfo;
        skeletonGraphic.initialSkinName = characterInfo.skinName;
        skeletonGraphic.Initialize(true);
        skeletonGraphic.Skeleton.SetSlotsToSetupPose();
    }
    internal void WinAnimUI()
    {
        charAnim.AnimationState.SetAnimation(0, winAnim, true);
    }
}

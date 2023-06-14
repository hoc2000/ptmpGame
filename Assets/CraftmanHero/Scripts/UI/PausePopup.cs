using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DarkTonic.MasterAudio;

public class PausePopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle vibrationToggle;
    [SerializeField] Button homeButton;
    [SerializeField] Button resetButton;
    [SerializeField] Button skipLevelButton;
    [SerializeField] GameObject popup;

    private void OnEnable()
    {
        GameAnalytics.LogUIAppear("popup_pause", "GamePlay");
        CheckToggle();
    }

    void Start()
    {
        AddEventButton();
    }
    #region INIT
    void AddEventButton()
    {
        //closeButton.onClick.AddListener(() => CloseClick());
        //musicToggle.onValueChanged.AddListener(MusicClick);
        //soundToggle.onValueChanged.AddListener(SoundClick);
        //vibrationToggle.onValueChanged.AddListener(VibrationClick);
        //homeButton.onClick.AddListener(() => HomeClick());
        //resetButton.onClick.AddListener(() => ResetClick());
        //skipLevelButton.onClick.AddListener(() => SkipLevelClick());
    }
    void CheckToggle()
    {
        musicToggle.isOn = !(PersistentAudioSettings.MusicMuted != null ? PersistentAudioSettings.MusicMuted.Value : false);
        soundToggle.isOn = !(PersistentAudioSettings.MixerMuted != null ? PersistentAudioSettings.MixerMuted.Value : false);
        vibrationToggle.isOn = Gamedata.I.Vibrate;
    }
    #endregion
    #region BUTTON
    public void CloseClick()
    {
        GameAnalytics.LogButtonClick("resume", "popup_pause");
        Time.timeScale = 1;
        this.Pop();
    }
    public void HomeClick()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "back_home_from_pause", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        GameAnalytics.LogButtonClick("back_to_home", "popup_pause");
        Gamedata.levelPlayedTime = 0;
        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;

        GameManager.Instance.BackMap();
        
    }
    public void ResetClick()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "restart_from_pause", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        GameAnalytics.LogButtonClick("restart", "popup_pause");
        Time.timeScale = 1;
        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;
        GameManager.startFrom = "restart_from_pause";
        LoadSceceManager.Instance.LoadLevel();
    }
    public void SkipLevelClick()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "skip_level_from_pause", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        GameAnalytics.LogButtonClick("skip_level", "popup_pause");
        Time.timeScale = 1;
        GameManager.startFrom = "skip_level";
        //SceneManager.LoadSceneAsync(Constants.SCENE.SCENE_GAMEPLAY);
       
        if (Gamedata.I.CurrentLevel == GameManager.levelSelected)
        {
            Gamedata.I.CurrentLevel++;
        }
        GameManager.levelSelected++;
        //LevelManager.Instance.LoadLevel();
        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;
        
        LoadSceceManager.Instance.LoadLevel();

    }
    public void MusicClick(bool value)
    {
        PersistentAudioSettings.MusicMuted = !value;
        //Gamedata.I.Music = isOn;
        //AudioManager.Instance.MusicOn();
    }
    public void SoundClick(bool value)
    {
        PersistentAudioSettings.MixerMuted = !value;
        //Gamedata.I.Sound = isOn;
        //AudioManager.Instance.EffectOn();
    }
    public void VibrationClick(bool value)
    {
        Gamedata.I.Vibrate = value;
    }
    #endregion
}

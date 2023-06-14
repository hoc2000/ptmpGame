using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;
using DarkTonic.MasterAudio;
using UnityEngine.Playables;

public class SettingPopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] Toggle musicToggle;
    [SerializeField] Toggle soundToggle;
    [SerializeField] Toggle vibrationToggle;
    [SerializeField] Button creditButton;
    [SerializeField] Button rateButton;
    [SerializeField] Button restoreButton;
    [SerializeField] Button privacyButton;
    [SerializeField] Button termButton;

    [SerializeField] GameObject popup;
    private void OnEnable()
    {
        GameAnalytics.LogUIAppear("popup_setting", "HomeScene");
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
        rateButton.onClick.AddListener(() => RateClick());
        creditButton.onClick.AddListener(() => CreditClick());
        //musicToggle.onValueChanged.AddListener(MusicClick);
        //soundToggle.onValueChanged.AddListener(SoundClick);
        //vibrationToggle.onValueChanged.AddListener(VibrationClick);

        restoreButton.onClick.AddListener(() => RestoreClick());
        privacyButton.onClick.AddListener(() => PrivacyClick());
        termButton.onClick.AddListener(() => TermClick());
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
        Time.timeScale = 1;
        this.Pop();
    }
    void RateClick()
    {
        //SDK.Instance.ShowRate();
    }
    void CreditClick()
    {
        CanvasManager.Push(GlobalInfo.CreditPopUp, null);
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

        #endregion

    }

    public void RestoreClick()
    {
        //IAPManager.Instance.RestorePurchases();
    }
    public void PrivacyClick()
    {
        Application.OpenURL("https://commandoo247.blogspot.com/2021/08/privacy.html?m=1");
    }
    public void TermClick()
    {
        Application.OpenURL("https://commandoo247.blogspot.com/2021/08/term-of-use-commandoo.html?m=1");
    }
}

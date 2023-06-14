using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool.UI;
using UnityEngine.UI;
using TMPro;
using My.Tool;
using Spine.Unity;

public class FalsePopup : BaseUIMenu
{

    [Header("BUTTON")]
    [SerializeField] Button homeButton;
    [SerializeField] Button restartButton;
    [SerializeField] Button upgradeCoinButton;
    [SerializeField] Button upgradeAdsButton;
    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI hpText;
    [SerializeField] TextMeshProUGUI atkText;
    [SerializeField] TextMeshProUGUI atkTextPlus;
    [SerializeField] TextMeshProUGUI hpTextPlus;
    [SerializeField] TextMeshProUGUI coinUpgradeText;
    [SerializeField] TextMeshProUGUI levelUpgradeText;
    [SerializeField]
    private SkeletonGraphic skeletonGraphic;
    private void OnEnable()
    {
        Init();
        UpdateTextData();
    }

    void Start()
    {
        AddEventButton();
    }

    #region INIT
    void Init()
    {
        GameAnalytics.LogUIAppear("popup_fail", "GamePlay");
        UpdateSelectedCharacterAnim();
    }
    void AddEventButton()
    {
        //homeButton.onClick.AddListener(() => HomeClick());
        //restartButton.onClick.AddListener(() => RestartClick());
        upgradeCoinButton.onClick.AddListener(() => UpgradeCoinClick());
        upgradeAdsButton.onClick.AddListener(() => UpgradeAdsClick());
    }
    #endregion

    #region BUTTON
    public void HomeClick()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "die", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;
        LoadSceceManager.Instance.LoadHome();
    }
    public void UpgradeCoinClick()
    {
        GameAnalytics.LogUpgradePowerLevel(Gamedata.I.LevelUpgrade, "coin", GameManager.Instance.upgradePlayerData.CoinUpGrade(), "fail_popup");
        Gamedata.I.Coin -= GameManager.Instance.upgradePlayerData.CoinUpGrade();
        UpgradeData();
        this.PostEvent(EventID.UpdateData);
    }
    public void UpgradeAdsClick()
    {
        UpgradeData();
    }

    public void RestartClick()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "die", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        Gamedata.TemporarySelectedCharacter = ResourceDetail.None;
        GameManager.startFrom = "restart_from_popup_fail";
        LoadSceceManager.Instance.LoadLevel();
    }
    #endregion
    #region UpdateData
    void UpgradeData()
    {
        GameManager.Instance.upgradePlayerData.UpgradeData();

        UpdateTextData();
    }

    void UpdateTextData()
    {
        bool haveCoinUpgrade = Gamedata.I.Coin >= GameManager.Instance.upgradePlayerData.CoinUpGrade();

        upgradeAdsButton.gameObject.SetActive(!haveCoinUpgrade);
        upgradeCoinButton.gameObject.SetActive(haveCoinUpgrade);

        levelUpgradeText.text = Gamedata.I.LevelUpgrade.ToString();
        coinUpgradeText.text = GameManager.Instance.upgradePlayerData.CoinUpGrade().ToString();
        //atkText.text = "ATK : " + Gamedata.I.AtkPlayer + "      <color=green>+" + GameManager.Instance.upgradePlayerData.AtkData() + "</color>";
        //hpText.text = "HP : " + Gamedata.I.HpPlayer + "      <color=green>+" + GameManager.Instance.upgradePlayerData.HpData() + "</color>";
        atkText.text = "ATK : " + Gamedata.I.AtkPlayer;
        atkTextPlus.text = "+" + GameManager.Instance.upgradePlayerData.AtkData().ToString();
        hpText.text = "HP : " + Gamedata.I.HpPlayer;
        hpTextPlus.text = "+" + GameManager.Instance.upgradePlayerData.HpData().ToString();
    }
    #endregion

    private void UpdateSelectedCharacterAnim()
    {
        CharacterInfo characterInfo = Gamedata.SelectedCharacterInfo;
        skeletonGraphic.initialSkinName = characterInfo.skinName;
        skeletonGraphic.Initialize(true);
        skeletonGraphic.Skeleton.SetSlotsToSetupPose();
    }

    public void OnRewardedClosed(bool success)
    {
        if (success)
        {
            UpgradeAdsClick();
            GameAnalytics.LogUpgradePowerLevel(Gamedata.I.LevelUpgrade, "watch_ads", 0, "fail_popup");
        }

    }
}

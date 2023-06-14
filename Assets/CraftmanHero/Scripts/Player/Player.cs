using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DarkTonic.MasterAudio;
using Spine.Unity;
using Spine;
using UnityEngine;
using UnityEngine.Playables;

public class Player : SingletonMonoBehaviour<Player>
{
    public CharacterMovement2D characterMovement2D;
    public PlayerInform playerInform;

    public Transform playerSkin;

    [SerializeField]
    private SkeletonMecanim skeletonGraphic;

    bool isDeath;

    public HpBar hpbar;    

    void Start()
    {
        //InitInformPlayer();
        SetSkinCharacter();
        
    }

    #region INIT
    //void InitInformPlayer()
    //{
    //    playerInform = new PlayerInform();
    //}
    #endregion


    #region DAMAGE

    public void UpdateHealth()
    {
        playerInform.CurrentHP = playerInform.MaxHP();
        hpbar.UpdateHpBar(playerInform.CurrentHP, playerInform.MaxHP());

    }
    public void TakeDamage(int dame)
    {
        VibrationManager.Instance.VibrateMedium();
        playerInform.CurrentHP -= dame;
        hpbar.UpdateHpBar(playerInform.CurrentHP, playerInform.MaxHP());

        var textDame = ContentMgr.Instance.GetItem("TextDame", transform.position, Quaternion.identity);
        textDame.GetComponent<TextDame>().Init(dame);
        MasterAudio.PlaySound(Constants.AUDIO.HIT);
        ContentMgr.Instance.GetItem("bloodFxPlayer",new Vector3(transform.position.x, transform.position.y, transform.position.z-1), Quaternion.identity);

        if (playerInform.CurrentHP<=0)
        {
            Death();
        }
    }

    public void Death()
    {
        isDeath = true;
        VibrationManager.Instance.VibrateMedium();
        GetComponent<AnimationController>().SetDieAnim();
        ContentMgr.Instance.GetItem(Constants.POOLING.PLAYERDEATHFX, transform.position, Quaternion.identity);
        gameObject.layer = Constants.LAYER.FORGOTTEN;
        StartCoroutine(Helper.StartAction(() =>
        {
            gameObject.SetActive(false);
            LevelManager.Instance.Death();
        }, 1.5f));
        GameAnalytics.LogPlayerDie(GameManager.levelSelected, (int)transform.position.x, (int)transform.position.y, LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, Gamedata.getWeapon, "sword");
    }
    #endregion

    public void Revive()
    {
        isDeath = false;
        transform.position = LevelManager.Instance.checkPoin.position;
        gameObject.SetActive(true);
        playerInform.CurrentHP = playerInform.MaxHP();
        hpbar.UpdateHpBar(playerInform.CurrentHP, playerInform.MaxHP());
    }


    public void SetSkinCharacter()
    {
        string skinName = Gamedata.SelectedCharacterInfo.skinName;
        playerSkin.GetComponent<SkeletonMecanim>().skeleton.SetSkin(skinName);
        playerSkin.GetComponent<SkeletonMecanim>().skeleton.SetSlotsToSetupPose();
        //UIGameController.instance.character.sprite = DataHolder.Instance.characters[GameData.SelectedCharacter].sprite;
    }

    public void SetSkinCharacterWithWeapon()
    {
        string skinName = Gamedata.SelectedCharacterWithWeaponInfo.skinName;
        playerSkin.GetComponent<SkeletonMecanim>().skeleton.SetSkin(skinName);
        playerSkin.GetComponent<SkeletonMecanim>().skeleton.SetSlotsToSetupPose();
    }
    private void OnEnable()
    {
        gameObject.layer = Constants.LAYER.PLAYER;
    }

    public bool IsDeath()
    {
        return isDeath;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if(collision.gameObject.tag == Constants.TAG.SPIKE)
        {
            Death();
        }
       
    }   
}

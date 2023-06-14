using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool;
public class Door : MonoBehaviour
{
    [SerializeField] GameObject openDoor;
    bool isOpendor;

    private void OnEnable()
    {
        this.RegisterListener(EventID.EnemyDeath, (sender, param) => Win()) ;
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.KillEnemy, (sender, param) => Win());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER) && isOpendor)
        {
            StartCoroutine(Helper.StartAction(() => Player.Instance.GetComponent<AnimationController>().SetWinAnim(), 0.15f));
            GameManager.isWin = true;
            Player.Instance.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            StartCoroutine(Helper.StartAction(() => GetComponent<ShowInterstitialController>().Show(), 1.8f));          
        }
    }
    public void WinLevel()
    {
        GameAnalytics.LogLevelEnd(GameManager.levelSelected, false, Gamedata.levelPlayedTime, Gamedata.levelPlayedTime, "default", "complete_level", !Gamedata.isLevelPassed, Gamedata.getWeapon, "sword", LevelManager.Instance.allEnemyInMap, LevelManager.Instance.allEnemyInMap - LevelManager.Instance.enemiesDie, (Player.Instance.playerInform.CurrentHP / Player.Instance.playerInform.MaxHp) * 100, Gamedata.getHealItem);
        LevelManager.Instance.Win();
    }
    void Win()
    {
        if (LevelManager.Instance.enemiesDie <  LevelManager.Instance.allEnemyInMap)
        {
            isOpendor = false;
            openDoor.SetActive(true);
        }
        else
        {
            isOpendor = true;
            openDoor.SetActive(false);
        }
        
    }
}

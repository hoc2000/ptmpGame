using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
using Spine.Unity;
using DarkTonic.MasterAudio;

public class Boss3Wither : EnemyBase
{

    [Header("BOSS SETTING")]
    [SerializeField] Transform posTop;
    [SerializeField] Transform posBot;
    [SerializeField] GameObject effStun;
    [Header("BOSS SKILL")]
    [SerializeField] string nameEnemeySummon;
    [SerializeField] GameObject skill3Eff;
    [SerializeField] EnemySummonWither[] enemySummon;
    [SerializeField] Vector2 attackRangeSkill3;
    [Header("MAP EFFECT")]
    [SerializeField] CheckStartBoss checkStartBoss;
    [Header("ANIMATION")]
    [SpineAnimation]
    public string moveAnimation;

    [SpineAnimation]
    public string roarAnimation;

    [SpineAnimation]
    public string shootAnimation;

    [SpineAnimation]
    public string stunAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string summonAnimation;

    [SpineAnimation]
    public string dashAnimation;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Appear());
    }

    
    void Update()
    {
        
    }
    IEnumerator Appear()
    {
        yield return new WaitForSeconds(1f);
        SetAnim(idleAnimation, true);
        transform.DOMoveY(posTop.position.y,2f).OnComplete(() => {
            SetAnim(roarAnimation, false);
        });
        yield return new WaitForSeconds(3f);

        StartCoroutine(Skill1Summon());
    }
    IEnumerator Skill1Summon()
    {
        SetAnim(summonAnimation, false);
        for (int i =0; i< enemySummon.Length; i++)
        {
            Vector2 posSpawn = new Vector2(Random.Range(pos2.position.x, pos1.position.x), enemySummon[i].transform.position.y - 2);

            enemySummon[i].transform.position = posSpawn;
            enemySummon[i].gameObject.SetActive(true);
            enemySummon[i].Init();
            yield return new WaitForSeconds(0.5f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine(AutoMove());
    }
    IEnumerator AutoMove()
    {
        SetAnim(moveAnimation, true);
        yield return new WaitForSeconds(1f);
        float time = 1f;
        if (Vector2.Distance(transform.position, pos1.position) <= 2)
        {
            transform.localScale = new Vector3(1,1,1);
            time = Vector2.Distance(transform.position, pos2.position) / 3f;
            transform.DOMoveX(pos2.position.x, time).SetEase(Ease.Linear).OnComplete(() => {

                transform.localScale = new Vector3(-1, 1, 1);
            });
        }
        else if (Vector2.Distance(transform.position, pos2.position) <= 2)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            time = Vector2.Distance(transform.position, pos1.position) / 3f;
            transform.DOMoveX(pos1.position.x, time).SetEase(Ease.Linear).OnComplete(() => {

                transform.localScale = new Vector3(1, 1, 1);
            }); ;
        }
        else
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                time = Vector2.Distance(transform.position, pos2.position) / 3f;
                transform.DOMoveX(pos2.position.x, time).SetEase(Ease.Linear).OnComplete(() => {

                    transform.localScale = new Vector3(-1, 1, 1);
                }); ;
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                time = Vector2.Distance(transform.position, pos1.position) / 3f;
                transform.DOMoveX(pos1.position.x, time).SetEase(Ease.Linear).OnComplete(() => {

                    transform.localScale = new Vector3(1, 1, 1);
                });
            }
        }

        yield return new WaitForSeconds(time + 1f);
        StartCoroutine(Skill2Shoot());
    }

    IEnumerator Skill2Shoot()
    {
        yield return new WaitForSeconds(1f);
        for (int i = 0; i<10; i++)
        {
            SetAnim(shootAnimation, true);
            yield return new WaitForSeconds(0.6f);
            var bullet = ContentMgr.Instance.GetItem(nameEnemeySummon,attackPoin.position, Quaternion.identity);
            bullet.GetComponent<BulletBoss3Wither>().Init(enemyInform.atkDame);
            //yield return new WaitForSeconds(0.8f);
        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(Skill3());
    }

    IEnumerator Skill3()
    {
        Vector2 posEnd = new Vector2(Player.Instance.transform.position.x, posBot.position.y);//new Vector2(Random.Range(pos2.position.x, pos1.position.x), transform.position.y);
        skill3Eff.SetActive(true);
        skill3Eff.transform.position = new Vector2(posEnd.x, skill3Eff.transform.position.y);

        time = Vector2.Distance(transform.position, Player.Instance.transform.position) / 8f;
        SetAnim(dashAnimation, false);
        transform.DOMove(posEnd, time).SetEase(Ease.Linear).OnComplete(() => {
            skill3Eff.SetActive(false);
            MasterAudio.PlaySound(Constants.AUDIO.COLLAPSE);
            ContentMgr.Instance.GetItem(Constants.POOLING.FXBOSSJUMP, posEnd, Quaternion.identity);
            BossAttack(skill3Eff.transform, attackRangeSkill3);
        });
        yield return new WaitForSeconds(time +0.3f);
        StartCoroutine(Stun());
    }

    void BossAttack(Transform trans, Vector2 attackRange)
    {
        ProCamera2DShake.Instance.Shake(1);

        Collider2D[] hitPlayer = Physics2D.OverlapBoxAll(trans.position, attackRange, 0f, layerCheck);
        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Player hit + " + player.name);
            player.GetComponent<Player>().TakeDamage(enemyInform.atkDame);
            //ProCamera2DShake.Instance.Shake(0);
        }
    }
    IEnumerator Stun()
    {
        SetAnim(stunAnimation, true);
        effStun.SetActive(true);
        yield return new WaitForSeconds(3f);
        effStun.SetActive(false);
        StartCoroutine(Appear());
    }
    public override void Death()
    {
        MasterAudio.PlaySound(Constants.AUDIO.EXPLOSIONBOSS);
        ContentMgr.Instance.GetItem(Constants.POOLING.FXBOSSDEATH, fxTranform.position, Quaternion.identity);
        checkStartBoss.ShowEffectEnd();
        base.Death();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(skill3Eff.transform.position, attackRangeSkill3);

    }
}

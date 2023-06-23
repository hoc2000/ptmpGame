using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
using Spine.Unity;
using DarkTonic.MasterAudio;
public class Boss2Warden : EnemyBase
{
    // Start is called before the first frame update
    [Header("BOSS SETTING")]
    [SerializeField] GameObject effPosJump;
    [SerializeField] GameObject effStun;
    [SerializeField] GameObject effJump;
    [Header("SKILL")]
    [SerializeField] GameObject warringLaze;
    [SerializeField] GameObject lazeEff;

    [SerializeField] Vector2 attackRangeSkillJump;
    [SerializeField] Vector2 attackRangeSkillLaze;

    [Header("MAP EFFECT")]
    [SerializeField] CheckStartBoss checkStartBoss;
    [Header("ANIMATION")]
    [SpineAnimation]
    public string jumpAnimation;

    [SpineAnimation]
    public string shootAnimation;

    [SpineAnimation]
    public string shoot2Animation;

    [SpineAnimation]
    public string stunAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string moveAnimation;

    protected override void Start()
    {
        base.Start();
        SetAnim(idleAnimation, true);
        StartCoroutine(AutoMove());
    }

    IEnumerator AutoMove()
    {
        yield return new WaitForSeconds(1f);
        SetAnim(moveAnimation, true);
        float time = 1f;
        if (Vector2.Distance(transform.position, pos1.position) <= 2)
        {            
            time = Vector2.Distance(transform.position, pos1.position)/3f;
            transform.DOMoveX(pos2.position.x, time).OnStart(() => transform.eulerAngles = Vector3.zero).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                SetAnim(idleAnimation, true);
            });
        }
        else if (Vector2.Distance(transform.position, pos2.position) <= 2)
        {           
            time = Vector2.Distance(transform.position, pos2.position)/3f;
            transform.DOMoveX(pos1.position.x, time).OnStart(() => transform.eulerAngles = new Vector3(0, 180, 0)).SetEase(Ease.Linear).OnComplete(() =>
            {
                transform.eulerAngles = Vector3.zero;
                SetAnim(idleAnimation, true);
            });
        }
        else
        {
            int i = Random.Range(0, 2);
            if (i == 0)
            {               
                time = Vector2.Distance(transform.position, pos2.position)/3f;
                transform.DOMoveX(pos2.position.x, time).OnStart(() => transform.eulerAngles = Vector3.zero).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.eulerAngles = new Vector3(0, 180, 0);
                    SetAnim(idleAnimation, true);
                });
            }
            else
            {               
                time = Vector2.Distance(transform.position, pos1.position)/3f;
                transform.DOMoveX(pos1.position.x, time).OnStart(() => transform.eulerAngles = new Vector3(0, 180, 0)).SetEase(Ease.Linear).OnComplete(() =>
                {
                    transform.eulerAngles = Vector3.zero;
                    SetAnim(idleAnimation, true);
                });
            }
        }

        yield return new WaitForSeconds(time + 1f) ;
        StartCoroutine(Skill12Laze());
    }

    IEnumerator Skill1Jump()
    {
        SetAnim(jumpAnimation, true);
        for (int i =0; i<3; i++)
        {
            yield return new WaitForSeconds(0.5f);
            Vector2 posJump = new Vector2(Player.Instance.transform.position.x, transform.position.y);//new Vector2(Random.Range(pos2.position.x, pos1.position.x), transform.position.y);
            effPosJump.SetActive(true);
            effPosJump.transform.position = new Vector2(posJump.x, effPosJump.transform.position.y);
            transform.DOJump(posJump, 3,1,1f).OnStart(() =>
            {
                if (transform.position.x > Player.Instance.transform.position.x)
                {
                    transform.eulerAngles = Vector3.zero;
                }
                else transform.eulerAngles = new Vector3(0, 180, 0);
            }).SetEase(Ease.Linear).OnComplete(() => {
                MasterAudio.PlaySound(Constants.AUDIO.COLLAPSE);
                var fx = Instantiate(effJump, effPosJump.transform.position, Quaternion.identity);
                effPosJump.SetActive(false);
                BossAttack(effPosJump.transform, attackRangeSkillJump);
            });
            yield return new WaitForSeconds(1.2f);
        }
        StartCoroutine(Stun());
    }

    IEnumerator Skill12Laze()
    {
        warringLaze.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        SetAnim(shootAnimation, false);
        yield return new WaitForSeconds(0.7f);
        warringLaze.SetActive(false);
        MasterAudio.PlaySound(Constants.AUDIO.LASERSHOOT);
        lazeEff.SetActive(true);
        
        BossAttack(lazeEff.transform, attackRangeSkillLaze);

        yield return new WaitForSeconds(0.3f);
        SetAnim(shoot2Animation, false);
        lazeEff.SetActive(false);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Skill1Jump());
    }

    IEnumerator Stun()
    {
        SetAnim(stunAnimation, true);
        effStun.SetActive(true);
        yield return new WaitForSeconds(3f);
        effStun.SetActive(false);
        StartCoroutine(AutoMove());
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
        Gizmos.DrawWireCube(lazeEff.transform.position, attackRangeSkillLaze);

        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(effPosJump.transform.position, attackRangeSkillJump);
    }
}

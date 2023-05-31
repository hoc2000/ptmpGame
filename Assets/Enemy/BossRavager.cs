using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
using Spine.Unity;
using DarkTonic.MasterAudio;

public class BossRavager : EnemyBase
{
    [Header("BOSS SETTING")]
    [SerializeField] GameObject effStun;
    [SerializeField] Transform smokeTransFX;
    [Header("MAP EFFECT")]
    [SerializeField] CheckStartBoss checkStartBoss;

    [Header("ANIMATION")]
    [SpineAnimation]
    public string moveAnimation;

    [SpineAnimation]
    public string roarAnimation;

    [SpineAnimation]
    public string hitAnimation;

    [SpineAnimation]
    public string stunAnimation;

    protected override void Start()
    {
        base.Start();
        MasterAudio.PlaySound(Constants.AUDIO.BOSS1ROAR);
        SetAnim(roarAnimation, false);
        StartCoroutine(AutoMove());
    }

    private IEnumerator AutoMove()
    {
        yield return new WaitForSeconds(1.5f);
        Vector3 pos = transform.position.x > pos2.position.x ? pos2.position : pos1.position;
        float time = Vector2.Distance(transform.position, pos) / 6;
        SetAnim(moveAnimation, true);
        transform.DOMoveX(pos.x, time).SetEase(Ease.Linear).OnComplete(() =>
        {
            MasterAudio.PlaySound(Constants.AUDIO.COLLAPSE);
            ContentMgr.Instance.GetItem(Constants.POOLING.SMOKE, new Vector3(smokeTransFX.position.x, smokeTransFX.position.y, smokeTransFX.position.z), Quaternion.identity);
            ProCamera2DShake.Instance.Shake(1);
            Flip();
            Vector3 pos = transform.position.x > pos2.position.x ? pos2.position : pos1.position;
            float time = Vector2.Distance(transform.position, pos) / 6;
            transform.DOMoveX(pos.x, time).SetEase(Ease.Linear).OnComplete(() =>
            {
                MasterAudio.PlaySound(Constants.AUDIO.COLLAPSE);
                ContentMgr.Instance.GetItem(Constants.POOLING.SMOKE, new Vector3(smokeTransFX.position.x, smokeTransFX.position.y, smokeTransFX.position.z), Quaternion.identity);
                ProCamera2DShake.Instance.Shake(1);
                Flip();
                Vector3 pos = transform.position.x > pos2.position.x ? pos2.position : pos1.position;
                float time = Vector2.Distance(transform.position, pos) / 6;
                transform.DOMoveX(pos.x, time).SetEase(Ease.Linear).OnComplete(() =>
                {
                    MasterAudio.PlaySound(Constants.AUDIO.COLLAPSE);
                    ContentMgr.Instance.GetItem(Constants.POOLING.SMOKE, new Vector3(smokeTransFX.position.x, smokeTransFX.position.y, smokeTransFX.position.z), Quaternion.identity);
                    ProCamera2DShake.Instance.Shake(1);
                    StartCoroutine(Stun());
                });
            });
        });
    }

    private void Flip()
    {
        if (transform.eulerAngles == Vector3.zero)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else transform.eulerAngles = Vector3.zero;
    }

    private IEnumerator Stun()
    {
        SetAnim(stunAnimation, true);
        effStun.SetActive(true);
        yield return new WaitForSeconds(3);
        effStun.SetActive(false);
        Flip();
        StartCoroutine(AutoMove());
    }

    public override void TakeDamage(int dame)
    {
        base.TakeDamage(dame);
    }

    public override void Death()
    {       
        transform.DOKill();
        StopAllCoroutines();
        MasterAudio.PlaySound(Constants.AUDIO.EXPLOSIONBOSS);
        ContentMgr.Instance.GetItem(Constants.POOLING.FXBOSSDEATH, fxTranform.position, Quaternion.identity);
        checkStartBoss.ShowEffectEnd();
        base.Death();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER))
        {
            Player.Instance.TakeDamage(enemyInform.atkDame);
        }
    }
}

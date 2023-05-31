using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
using Spine.Unity;
using DarkTonic.MasterAudio;

public class BossEnder : EnemyBase
{
    [Header("BOSS SETTING")]
    [SerializeField] private GameObject effStun;
    [SerializeField] private GameObject meteor;
    [SerializeField] private ParticleSystem flame;
    [SerializeField] private Transform[] meteorPos;
    [SerializeField] private Transform pos3;
    [SerializeField] private EnderFlame enderFlame;
    [SerializeField] private GameObject fxLand;

    [Header("MAP EFFECT")]
    [SerializeField] CheckStartBoss checkStartBoss;

    [Header("ANIMATION")]
    [SpineAnimation]
    public string roarAnimation;

    [SpineAnimation]
    public string flyAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string stunAnimation;

    [SpineAnimation]
    public string idle2Animation;

    [SpineAnimation]
    public string landingAnimation;

    [SpineAnimation]
    public string fireAnimation;

    [SpineAnimation]
    public string fire2Animation;

    protected override void Start()
    {
        base.Start();
        SetAnim(roarAnimation, false);
        StartCoroutine(Skill1());
    }

    private IEnumerator Skill1()
    {
        yield return new WaitForSeconds(2.133f);
        SetAnim(flyAnimation, true);   
        flame.gameObject.SetActive(true);
        enderFlame.enabled = true;
        enderFlame.dir = -Vector2.one;
        flame.Play();
        transform.DOMoveX(pos2.position.x, 3).SetEase(Ease.Linear).OnComplete(() =>
        {
            enderFlame.enabled = false;
            flame.Stop();
            StartCoroutine(Flip());
            StartCoroutine(Helper.StartAction(() =>
            {
                enderFlame.enabled = true;
                enderFlame.dir = new Vector2(1, -1);
                flame.Play();
                transform.DOMoveX(pos1.position.x, 3).SetEase(Ease.Linear).OnComplete(() =>
                {
                    enderFlame.enabled = false;
                    flame.Stop();
                    StartCoroutine(Flip());
                    StartCoroutine(Skill2());
                });
            }, 1f));
        });
    }

    private IEnumerator Skill2()
    {
        SetAnim(idleAnimation, true);
        yield return new WaitForSeconds(1f);
        SetAnim(roarAnimation, false);
        yield return new WaitForSeconds(1f);
        ProCamera2DShake.Instance.Shake(1);
        for (int i = 0; i < meteorPos.Length; i++)
        {
            GameObject newMeteor = Instantiate(meteor, new Vector3(Random.Range(meteorPos[i].position.x - 0.5f, meteorPos[i].position.x + 0.5f), Random.Range(meteorPos[i].position.y - 1, meteorPos[i].position.y + 1)), meteorPos[i].rotation);
            StartCoroutine(Helper.StartAction(() =>
            {
                newMeteor.SetActive(true);
                newMeteor.GetComponent<ParticleSystem>().Play();
            }, 0.5f));
        }
        yield return new WaitForSeconds(1.133f);
        SetAnim(idleAnimation, true);
        yield return new WaitForSeconds(3);
        StartCoroutine(Skill3());
    }

    private IEnumerator Skill3()
    {
        yield return new WaitForSeconds(0.5f);
        SetAnim(idleAnimation, true);
        transform.DOMoveY(pos3.position.y, 1).SetEase(Ease.Linear).OnComplete(() =>
        {
            fxLand.transform.position = attackPoin.position;
            fxLand.GetComponent<ParticleSystem>().Play();
        });
        yield return new WaitForSeconds(0.7f);
        SetAnim(landingAnimation, false);
        yield return new WaitForSeconds(1.267f);
        SetAnim(fireAnimation, false);
        yield return new WaitForSeconds(1.1f);
        var fire = flame.GetComponent<ParticleSystem>().main;
        flame.transform.eulerAngles = new Vector3(0, -90, 90);
        enderFlame.enabled = true;
        enderFlame.dir = Vector2.left;
        flame.Play();
        fire.startLifetime = 1.2f;
        StartCoroutine(Helper.StartAction(() =>
        {
            enderFlame.enabled = false;
            flame.Stop();
            var fire = flame.GetComponent<ParticleSystem>().main;                      
            fire.startLifetime = 0.5f;
            flame.transform.eulerAngles = new Vector3(45, -90, 90);
        }, 5));
        yield return new WaitForSeconds(5f);
        SetAnim(fire2Animation, false);
        yield return new WaitForSeconds(1f);
        SetAnim(idle2Animation, true);
        yield return new WaitForSeconds(1f);
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        SetAnim(stunAnimation, true);
        effStun.SetActive(true);
        yield return new WaitForSeconds(3);
        SetAnim(idle2Animation, true);
        effStun.SetActive(false);
        yield return new WaitForSeconds(1f);
        transform.DOMoveY(pos1.position.y, 2).OnStart(() => SetAnim(idleAnimation, true)).SetEase(Ease.Linear).OnComplete(() => StartCoroutine(Skill1()));    
    }

    private IEnumerator Flip()
    {
        if (transform.position.x == pos1.position.x)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else if (transform.position.x == pos2.position.x)
        {
            Debug.Log("vcl");
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        yield return new WaitForSeconds(1f);
    }
    public override void Death()
    {
        MasterAudio.PlaySound(Constants.AUDIO.EXPLOSIONBOSS);
        ContentMgr.Instance.GetItem(Constants.POOLING.FXBOSSDEATH, fxTranform.position, Quaternion.identity);
        checkStartBoss.ShowEffectEnd();
        base.Death();
    }
}

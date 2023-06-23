using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using Spine.Unity;
using Spine;
using DarkTonic.MasterAudio;

public class Enemy1 : EnemyBase
{
    [Header("ANIMATION")]
    [SpineAnimation]
    public string moveAnimation;

    [SpineAnimation]
    public string attackAnimation;

    [SpineAnimation]
    public string hitAnimation;

    private bool canAttack = true;


    public RaggdollEnemyFx raggdoll;


    protected override void Start()
    {
        base.Start();
        animationState.Event += delegate (TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == "attack")
            {
                Attack();
            }
        };

        animationState.Complete += delegate (TrackEntry trackEntry)
        {
            if (trackEntry.ToString() == hitAnimation)
            {
                canAttack = true;
            }
        };
    }

    public virtual void Update()
    {
        CheckDirection();
        CheckAttack();
    }
    public virtual void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();
        if (speed != 0)
        {           
            if (speed == speedDefault)
            {
                SetAnim(moveAnimation, true, 1);
            }
            else
            {
                SetAnim(moveAnimation, true, 1.5f);
            }
        }
    }

    public override void CheckAttack()
    {
        if (isTarget)
        {
            if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= attackDistance)
            {
                isAttack = true;
                speed = 0f;
                if (canAttack)
                {
                    SetAnim(attackAnimation, true);
                }
            }
            else
            {
                isAttack = false;

            }
        }        
    }

   
    public override void TakeDamage(int dame)
    {
        canAttack = false;
        SetAnim(hitAnimation, false);
        base.TakeDamage(dame);       
    }

    public override void DeathEffect()
    {
        //rig.constraints = RigidbodyConstraints2D.None;
        //coll.enabled = false;
        ////StartCoroutine(Disappear());
        //enabled = false;
        ////ragdoll.Apply();
        //float dir = Mathf.Sign(transform.position.x - Player.Instance.transform.position.x);
        ////Destroy(rig);
        ////ragdoll.SmoothMix(1,0.5f);
        ////ragdoll.RootRigidbody.velocity = new Vector2(20 * dir, 80);
        ////Vector2 force = new Vector2(20 * dir, 80);
        ////raggdoll.gameObject.SetActive(true);
        ////raggdoll.Init(force);
        //var raggdoll = ContentMgr.Instance.GetItem(Constants.POOLING.RAGDOLLENEMY1, transform.position, Quaternion.identity);
        //raggdoll.GetComponent<SkeletonAnimation>().initialFlipX = transform.localScale.x ==-1;
        MasterAudio.PlaySound(Constants.AUDIO.ZOMBIEDEATH);
        //raggdoll.SetActive(true);
        base.DeathEffect();
    }
       
    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1.5f);
        float time = 0;
        float a = 1;
        while (time <= 0.5f)
        {
            time += 0.05f;
            a -= 0.1f;
            skeleton.SetColor(new Color(1, 1, 1, a));
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.SetActive(false);
    }
}

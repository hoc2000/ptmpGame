using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using Spine.Unity;
using Spine;
using DarkTonic.MasterAudio;
public class Enemy2Shoot : EnemyBase
{
    [Header("ANIMATION")]
    [SpineAnimation]
    public string shootAnimation;

    [SpineAnimation]
    public string moveAnimation;

    [SpineAnimation]
    public string hitAnimation;

    [SerializeField] string nameBullet;
    private bool canAttack = true;

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

    // Update is called once per frame
    void Update()
    {
        base.CheckDirection();
        CheckAttack();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        base.Move();
        if (speed != 0)
        {
            SetAnim(moveAnimation, true);
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
                    SetAnim(shootAnimation, true);
                }
            }
            else
            {
                isAttack = false;
            }
        }
    }

    public override void Attack()
    {
        MasterAudio.PlaySound(Constants.AUDIO.ENEMY2ATTACK);
        var bullet = ContentMgr.Instance.GetItem(nameBullet, attackPoin.position, Quaternion.identity);
        BulletEnemy bulletEnemy = bullet.GetComponent<BulletEnemy>();
        bulletEnemy.Init(enemyInform.atkDame, base.xDirection);
        if (Mathf.Sign(transform.position.x - Player.Instance.transform.position.x) > 0)
        {
            bulletEnemy.transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else bulletEnemy.transform.eulerAngles = Vector3.zero;
    }

    public override void TakeDamage(int dame)
    {
        canAttack = false;
        SetAnim(hitAnimation, false);
        base.TakeDamage(dame);
    }
}

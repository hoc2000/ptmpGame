using DG.Tweening;
using Enemy.Base;
using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Spider2 : EnemyBase
{
    public RaggdollEnemyFx raggdoll;
    [Header("ANIMATION")]
    [SpineAnimation]
    public string moveAnimation;

    [SpineAnimation]
    public string attackAnimation;

    [SpineAnimation]
    public string hitAnimation;

    private bool canAttack = true;

    [SerializeField] Transform posSpawn;

    [SerializeField] Transform spawnPosLeft;
    [SerializeField] Transform spawnPosRight;
    [SerializeField] Enemy1[] enemylittleSpider;

    [SerializeField] GameObject enemySpawn;

    [SerializeField] public bool isWall;

     [SerializeField] Transform wallCheck;

    [SerializeField] public LayerMask wallLayer;

    protected override void Start()
    {
        base.Start();
        animationState.Event += delegate (TrackEntry trackEntry, Spine.Event e)
        {
            if (e.Data.Name == "attack")
            {
                base.Attack();
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

    public override void TakeDamage(int dame)
    {
        canAttack = false;
        SetAnim(hitAnimation, false);
        base.TakeDamage(dame);
    }

    public override void Death()
    {
        base.Death();
        if (IsDeath)
        {
                enemySpawn.SetActive(true);
                 enemySpawn.transform.position = transform.position;
                enemylittleSpider[0].transform.DOJump(spawnPosLeft.transform.position, 2, 1, 1).SetEase(Ease.Linear);
                enemylittleSpider[1].transform.DOJump(spawnPosRight.transform.position, 2, 1, 1).SetEase(Ease.Linear);



        }
    }

    public void CheckWall()
    {
        RaycastHit2D hit = Physics2D.Raycast(wallCheck.position, Vector2.right * xDirection, distanceCheckFront, wallLayer, 0f);

        RaycastHit2D hit1 = Physics2D.Raycast(checkPlayerPos.position, Vector2.left * xDirection, distanceCheckBack, layerCheck, 0f);

        Debug.DrawRay(checkPlayerPos.position, Vector2.right * xDirection * distanceCheckFront, Color.red);
        if (hit || hit1)
        {
            enemySpawn.transform.position = transform.position;
            enemylittleSpider[0].transform.DOJump(new Vector2(spawnPosLeft.transform.position.x - 1.5f, 0), 2, 1, 1).SetEase(Ease.Linear);
            enemylittleSpider[1].transform.DOJump(new Vector2(spawnPosRight.transform.position.x - 1.5f, 0), 2, 1, 1).SetEase(Ease.Linear);
        }
    }
    public override void DeathEffect()
    {
        base.DeathEffect();
    }

}

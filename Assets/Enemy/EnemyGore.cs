using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;

public class EnemyGore : EnemyBase
{

    [SerializeField] EnemyRangeCheck enemyGoreCheck;
    [SerializeField] float gorePower;

    [SerializeField] float coefficientTimeAttack;
    [SerializeField] Transform player;
    [SerializeField] Transform headPoint;

    [SerializeField] Vector2 checkHeadSize;
    [SerializeField] LayerMask layerHeadCollision;
    float xDirection;
    bool canAttack = true;
    bool animGore;
    float currentTimeEndAttack;
    bool setTimeCowDown;
    void Start()
    {
        base.Start();

    }

    // Update is called once per frame
    void Update()
    {
        GoreAttack();
        CheckHead();
    }
    private void FixedUpdate()
    {
        if (currentTimeEndAttack > 0)
        {
            Attack();

        }
    }
    void GoreAttack()
    {
        if (enemyGoreCheck.CheckIsRange() && canAttack)
        {
            canAttack = false;
            anim.SetBool("Gore", true);
            if (player.position.x < transform.position.x)
            {
                xDirection = -1f;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                xDirection = 1f;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }

        if (animGore && !canAttack && !setTimeCowDown)
        {
            setTimeCowDown = true;
            currentTimeEndAttack = coefficientTimeAttack * Vector3.Distance(player.position, transform.position);
        }
        else if (currentTimeEndAttack <= 0)
        {

            if (setTimeCowDown)
            {
                anim.SetBool("Gore", false);
                animGore = false;
                canAttack = true;
                setTimeCowDown = false;
            }

        }
        else
        {
            currentTimeEndAttack -= Time.deltaTime;
        }
    }

    void Attack()
    {
        if (animGore)
        {
            rig.velocity = Vector2.right * xDirection * gorePower * Time.fixedDeltaTime;
        }

    }
    void CheckHead()
    {
        if (Physics2D.OverlapBox(headPoint.position, checkHeadSize, 0f, layerHeadCollision))
        {
            currentTimeEndAttack = 0;
        }
    }
    public void EventAnimGore()
    {
        animGore = true;
    }
    void ResetAttack()
    {
        canAttack = true;
    }
    public override void DeathEffect()
    {
        enemyGoreCheck.gameObject.SetActive(false);
        rig.velocity += new Vector2(0f, 40f);
        anim.enabled = false;
        coll.isTrigger = true;
        base.DeathEffect();

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundEffect"))
        {
            base.TakeDamage(1);
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(headPoint.position, checkHeadSize);
    }
}

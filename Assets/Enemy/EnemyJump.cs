using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;

public class EnemyJump : EnemyBase
{

    [Header("ATTACK")]
    [SerializeField] float radiusAttack;
    [SerializeField] float jumpPower;
    [SerializeField] Transform player;
    [SerializeField] LayerMask playerLayer;

    [SerializeField] SpriteRenderer outCheck;
    [SerializeField] bool canAttack = true;
    [SerializeField] float coefficientX;
    void Start()
    {
        base.Start();

    }
    private void Update()
    {
        //CheckIsGround();
    }
    private void FixedUpdate()
    {
        JumpAttack();
        //if (rig.velocity == Vector2.zero)
        //{
        //    canAttack = true;
        //}
    }
    bool CheckIsRadiUs()
    {
        if (Physics2D.OverlapCircle(transform.position, radiusAttack, playerLayer))
        {
            outCheck.color = Color.red;
            return true;
        }
        outCheck.color = Color.white;
        return false;
    }
    void JumpAttack()
    {
        if (CheckIsRadiUs() && canAttack)
        {
            //transform.DOJump(player.position, jumpPower, 1, 0.5f);
            canAttack = false;

            anim.SetBool("Jump", true);

        }
    }
    public void Jump()
    {
        if (IsDeath)
        {
            return;
        }
        if (player.transform.position.x < transform.position.x)
        {
            rig.velocity += new Vector2(-Vector2.Distance(transform.position, player.position) / coefficientX, jumpPower);
        }
        else
        {
            rig.velocity += new Vector2(Vector2.Distance(transform.position, player.position) / coefficientX, jumpPower);
        }
    }
    public override void DeathEffect()
    {
        outCheck.gameObject.SetActive(false);
        rig.velocity += new Vector2(0f, 40f);
        anim.enabled = false;
        coll.isTrigger = true;
        base.DeathEffect();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("GroundEffect"))
        {
            base.TakeDamage(1);
        }
        else
        {
            anim.SetBool("Jump", false);
            Invoke("ResetAttack", base.timeDelayAttack);
        }
    }
    void ResetAttack()
    {
        canAttack = true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radiusAttack);

    }
}

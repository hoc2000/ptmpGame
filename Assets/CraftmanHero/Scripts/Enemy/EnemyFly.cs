using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;

public class EnemyFly : EnemyBase
{

    [Header("ENEMY FLY")]
    [SerializeField] float timeAttack;

    [SerializeField] EnemyRangeCheck enemyRangeCheck;
    [SerializeField] Transform player;
    [SerializeField] float minTime, maxTime;
    Vector3 endPoint, playerPoint;
    bool canAttack = true;
    [SerializeField] float time;
    void Start()
    {
        //Attack();
    }

    // Update is called once per frame
    void Update()
    {
        if (canAttack && enemyRangeCheck.CheckIsRange())
        {
            canAttack = false;
            Attack();
        }
    }

    void Attack()
    {
        anim.SetBool("Fly", true);
        StartCoroutine(FlyAtack());
    }
    IEnumerator FlyAtack()
    {
        yield return new WaitForSeconds(0.5f);
        time = Vector3.Distance(player.position, transform.position) / timeAttack;
        time = Mathf.Lerp(minTime, maxTime, time);
        //if (Vector3.Distance(player.position, transform.position) < 1f)
        //{
        //    time = 0.9f;
        //}
        //else
        //{
        //    time = timeAttack / Vector3.Distance(player.position, transform.position);
        //}

        playerPoint = player.position;
        endPoint = new Vector3(player.position.x + -10f, transform.position.y, 0f);
        if (player.position.x < transform.position.x)
        {
            endPoint = new Vector3(player.position.x - 10f, transform.position.y, 0f);
        }
        else
        {
            endPoint = new Vector3(player.position.x + 10f, transform.position.y, 0f);
        }
        Vector3[] pathMove = { transform.position, playerPoint, endPoint };
        transform.DOPath(pathMove, time, PathType.CatmullRom).OnComplete(() =>
        {
            anim.SetBool("Fly", false);
            Invoke("DelayAttack", timeDelayAttack);
        });
    }
    void DelayAttack()
    {
        canAttack = true;
    }
    public override void DeathEffect()
    {

        DOTween.Kill(transform);
        enemyRangeCheck.gameObject.SetActive(false);
        rig.gravityScale = 25f;
        rig.velocity += new Vector2(0f, 40f);
        anim.enabled = false;
        coll.isTrigger = true;
        base.DeathEffect();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("GroundEffect") && !IsDeath)
        {
            base.TakeDamage(1);
        }
    }
    //private void OnCollisionEnter2D(Collision2D collision)hua
    //{
    //    if (collision.gameObject.CompareTag("GroundEffect"))
    //    {
    //        base.TakeDame(1);
    //    }
    //}
}

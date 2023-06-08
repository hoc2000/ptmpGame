using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using Com.LuisPedroFonseca.ProCamera2D;
using Spine.Unity;
using DarkTonic.MasterAudio;
using My.Tool;

public class Enemy3Explosion : EnemyBase
{
    [Header("ANIMATION")]
    [SpineAnimation]
    public string shakeAnimation;

    public string nameBullet;
    [SerializeField] float explosionRange;

    private bool canCheckAttack = true;

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        if (canCheckAttack)
        {
            CheckDirection();
        }

        CheckAttack();
    }
    private void FixedUpdate()
    {
        if (canExplosion)
        {
            canExplosion = false;
            attackDistance = 1000000f;
            StopMove();           
        }
        base.Move();
    }

    public override void CheckDirection()
    {
        if (transform.position.x < pos1.position.x && transform.position.x > pos2.position.x)
        {
            CheckDistanceFront();
            //CheckDistanceBack();
        }
        else
        {
            if (transform.position.x >= pos1.position.x)
            {
                xDirection = -1;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            if (transform.position.x <= pos2.position.x)
            {
                xDirection = 1;
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
        }
    }

    public override void CheckDistanceFront()
    {
        if (Player.Instance.transform.position.x < pos2.position.x || Player.Instance.transform.position.x > pos1.position.x)
        {
            isTarget = false;
            speed = speedDefault;
            return;
        }
        RaycastHit2D hit = Physics2D.Raycast(checkPlayerPos.position, Vector2.right * xDirection, distanceCheckFront, layerCheck, 0f);
        Debug.DrawRay(checkPlayerPos.position, Vector2.right * xDirection * distanceCheckFront, Color.red);

        RaycastHit2D hit1 = Physics2D.Raycast(checkPlayerPos.position, Vector2.left * xDirection, distanceCheckBack, layerCheck, 0f);
        Debug.DrawRay(checkPlayerPos.position, Vector2.left * xDirection * distanceCheckBack, Color.red);

        if (hit || hit1)
        {
            isTarget = true;
            if (!isAttack)
            {
                speed = speedRun;
            }
            if (transform.position.x > Player.Instance.transform.position.x)
            {
                xDirection = -1;
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
            else
            {
                if (transform.position.x < Player.Instance.transform.position.x)
                {
                    xDirection = 1;
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
            }
        }
        else
        {
            isTarget = false;
            if (!isAttack)
            {
                speed = speedDefault;
            }
        }
    }

    public override void CheckAttack()
    {
        if (Vector2.Distance(transform.position, Player.Instance.transform.position) <= attackDistance && canCheckAttack)
        {
            canCheckAttack = false;
            canExplosion = true;
            time += Time.deltaTime;
            isAttack = true;
            speed = 0f;
            SetAnim(shakeAnimation, true);
            StartCoroutine(Helper.StartAction(() => Attack(), 1.3f));
        }
        else
        {
            isAttack = false;
        }
    }
    public override void Attack()
    {
        ProCamera2DShake.Instance.Shake(1);
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(attackPoin.position, explosionRange, layerCheck);
        foreach (Collider2D player in hitPlayer)
        {
            Debug.Log("Player hit + " + player.name);
            player.GetComponent<Player>().TakeDamage(enemyInform.atkDame);
            
        }
        MasterAudio.PlaySound(Constants.AUDIO.EXPLOSION);
        base.Attack();
        enemyInform.currentHp = 0;
        ContentMgr.Instance.GetItem("ExplosionBullet", fxTranform.position, Quaternion.identity);
        if (hpBar != null)
        {
            hpBar.gameObject.SetActive(false);
        }
        IsDeath = true;
        LevelManager.Instance.enemiesDie++;
        Invoke("ShowCoinFx", 0.1f);
        this.PostEvent(EventID.EnemyDeath);

        gameObject.SetActive(false);
    }
    private void OnDrawGizmos()
    {

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoin.position, explosionRange);

    }
}

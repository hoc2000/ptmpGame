using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using DarkTonic.MasterAudio;
using Com.LuisPedroFonseca.ProCamera2D;
public class Spider : EnemyBase
{
    [SerializeField] private SpriteRenderer silk;
    private bool isMoving = true;
    [SerializeField] private float durationUp;
    [SerializeField] private float durationDown;
    [SerializeField] private Transform endPos;
    private Vector3 originPos;

    [SerializeField] private float downDelay;
    [SerializeField] private float upDelay;

    protected override void Start()
    {
        base.Start();
        originPos = transform.position;
    }

    private void FixedUpdate()
    {
        Move();
        silk.size = new Vector2(silk.size.x, Vector2.Distance(fxTranform.position, silk.transform.position));
    }

    public override void Move()
    {
        if (isMoving)
        {
            isMoving = false;
            StartCoroutine(Helper.StartAction(() =>
            {
                transform.DOMoveY(endPos.position.y, durationDown).SetEase(Ease.Linear).OnComplete(() =>
                {
                    StartCoroutine(Helper.StartAction(() =>
                    {
                        transform.DOMoveY(originPos.y, durationUp).SetEase(Ease.Linear).OnComplete(() => isMoving = true);
                    }, upDelay));
                });
            }, downDelay));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER))
        {
            MasterAudio.PlaySound(Constants.AUDIO.ZOMBIEATTACK);
            speed = 0f;

            //Debug.Log("Player hit + " + player.name + " " + enemyInform.atkDame);
            collision.GetComponent<Player>().TakeDamage(enemyInform.atkDame);
            ProCamera2DShake.Instance.Shake(0);
            //Attack();
        }
    }

    public override void DeathEffect()
    {
        silk.gameObject.SetActive(false);
        base.DeathEffect();
    }
}

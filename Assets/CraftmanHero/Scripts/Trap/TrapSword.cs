using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSword : MonoBehaviour
{
    public float timeAttack, timeEndAttack;
    public float distanceAttack;
    bool canAttack;
    public float distance;
    Vector3 startPos;
    Rigidbody2D rb;
    [SerializeField] ParticleSystem fxSword;
    public int dame;
    private void Start()
    {
        startPos = transform.position;
        rb = GetComponent<Rigidbody2D>();
        int atkDame = dame;
        for (int i = 0; i < GameManager.levelSelected; i++)
        {
            atkDame = atkDame + atkDame * 10 / 100;
        }

        dame = atkDame;
    }

    private void FixedUpdate()
    {
       
        //if (Mathf.Abs(LevelManager.Instance.player.transform.position.x - transform.position.x) < distanceAttack && !canAttack)
        //{
        //    canAttack = true;
        //    StartCoroutine(Attack());
        //}
       
    }

    public IEnumerator Attack()
    {
        //transform.DOMoveY(startPos.y - distance, 1).SetEase(Ease.InOutCubic);
        rb.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(timeEndAttack);
        //transform.DOMoveY(startPos.y, timeAttack);
        //yield return new WaitForSeconds(timeAttack);
        //StartCoroutine(Attack());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.TAG.PLAYER)
        {
            gameObject.SetActive(false);
            Player.Instance.TakeDamage(dame);
            fxSword.Play();
            //GameObject clone = Instantiate(fxSword,transform.position, Quaternion.identity);

        }
        if (collision.gameObject.layer == Constants.LAYER.GROUND)
        {
            gameObject.SetActive(false);
            fxSword.Play();
            //GameObject clone = Instantiate(fxSword, transform.position, Quaternion.identity);

        }
    }
}

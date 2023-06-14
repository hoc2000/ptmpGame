using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerDown : MonoBehaviour
{
    public bool activate;
    public bool moving;
    bool isContracting;

    float startY;
    public float speed = 3f;
    public float contractingTime = 1.5f;
    float delta = 0.01f;
    Vector3 velocity;

    public float startTime = 0;

    public Transform target;
    public bool canKill;

    void Start()
    {
        startY = transform.localPosition.y;
    }

    public void OnActivate()
    {
        moving = true;
    }
    void Update()
    {
       
        if (!moving)
        {
            if (startTime > 0)
            {
                startTime -= Time.deltaTime;
            }
            else
            {
                moving = true;
            }
        }
        if (moving && !isContracting)
        {
            Slamming();
        }
        if (!isContracting)
        {
            if (transform.position.y <= target.position.y)
            {
                transform.position = new Vector3(transform.position.x, target.position.y, target.position.z);
                StartCoroutine(Contracting());
            }
        }

        if ((transform.position - target.position).sqrMagnitude < 0.1f)
        {
            canKill = false;
        }
        else
        {
            if (!isContracting)
            {
                canKill = true;
            }
        }
        if ((transform.position - target.position).sqrMagnitude > 2 * 2)
        {
            canKill = false;
        }
    }
    void Slamming()
    {
        velocity.y = speed * Time.deltaTime;
        transform.position -= velocity;
    }
    IEnumerator Contracting()
    {
        moving = false;
        isContracting = true;
        canKill = false;
        yield return new WaitForSeconds(1f);
        transform.DOKill();
        transform.DOLocalMoveY(startY, contractingTime);
        yield return new WaitForSeconds(contractingTime);
        moving = true;
        canKill = true;
        isContracting = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            if (canKill)
            {
                Player player = collision.gameObject.GetComponent<Player>();
                player.Death();
            }
        }
        else
        {
            StartCoroutine(Contracting());
        }
    }
}

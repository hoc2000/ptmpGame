using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletExplosion : MonoBehaviour
{
    public float timeExist;

    private void OnEnable()
    {
        Init();
    }
    public void Init()
    {
        Invoke("DeActive", timeExist);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (Constants.TAG.PLAYER)
    //    {
    //        collision.GetComponent<Player>().TakeDamage(atkDame);
    //    }
    //}

    void DeActive()
    {
        ContentMgr.Instance.Despaw(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;

public class BulletBoss3Wither : MonoBehaviour
{
    public float speed;

    int atkDame;

    Rigidbody2D rig;
    Vector2 direction;
    
    public void Init(int dame)
    {
        atkDame = dame;

        rig = GetComponent<Rigidbody2D>();

        Vector3 difference = Player.Instance.transform.position - transform.position;

        float distance = difference.magnitude;
        direction = difference / distance;
        direction.Normalize();

    }
    private void FixedUpdate()
    {
        Move();
    }
    void Move()
    {
        //transform.Translate(Player.Instance.transform.forward * speed * Time.deltaTime);
        if (rig)
        {
            rig.velocity = direction * speed * Time.fixedDeltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            ProCamera2DShake.Instance.Shake(0);
            collision.gameObject.GetComponent<Player>().TakeDamage(atkDame);
        }
        ContentMgr.Instance.GetItem("AcidImpactRed", transform.position, Quaternion.identity);
        ContentMgr.Instance.Despaw(gameObject);
    }

    
}

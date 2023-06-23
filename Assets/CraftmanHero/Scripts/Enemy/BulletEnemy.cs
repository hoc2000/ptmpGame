using System.Collections;
using System.Collections.Generic;
using Com.LuisPedroFonseca.ProCamera2D;
using UnityEngine;
using DarkTonic.MasterAudio;
public class BulletEnemy : MonoBehaviour
{
   
    public float speed;

    int atkDame;
    float xDirection;
    Rigidbody2D rig;
    
    public void Init(int dame, float xDirection)
    {
        atkDame = dame;
        this.xDirection = xDirection;
        rig = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (rig)
        {
            rig.velocity = new Vector2(xDirection * speed * Time.fixedDeltaTime, 0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        MasterAudio.PlaySound(Constants.AUDIO.ARROWIMPACT);
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            ProCamera2DShake.Instance.Shake(0);
            collision.gameObject.GetComponent<Player>().TakeDamage(atkDame);
        }
        gameObject.SetActive(false);
    }
}

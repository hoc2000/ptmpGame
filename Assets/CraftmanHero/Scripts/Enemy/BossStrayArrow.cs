using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class BossStrayArrow : MonoBehaviour
{
    [SerializeField] private int damage;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.TAG.PLAYER)
        {
            //MasterAudio.PlaySound(Constants.AUDIO.ARROWIMPACT);
            Player.Instance.TakeDamage(damage);
        }
        if (collision.gameObject.layer == Constants.LAYER.GROUND)
        {
            MasterAudio.PlaySound(Constants.AUDIO.ARROWIMPACT);
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1);
        }

    }
}

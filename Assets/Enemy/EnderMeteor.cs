using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderMeteor : MonoBehaviour
{
    [SerializeField] private int damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER))
        {
            Player.Instance.TakeDamage(damage);
        }
        if (collision.gameObject.layer == Constants.LAYER.GROUND)
        {
            gameObject.SetActive(false);
        }
        //ContentMgr.Instance.GetItem("AcidImpactRed", transform.position, Quaternion.identity);
    }
}

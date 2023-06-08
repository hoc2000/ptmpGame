using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourceObj : MonoBehaviour
{
    [SerializeField] bool currentFource;
    [SerializeField] float fourceValue;
    [SerializeField] Vector3 directionFource;
    [SerializeField] LayerMask layerFource;

    [SerializeField] SpriteRenderer sprite;
    private void OnEnable()
    {
        //sprite.enabled = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if ((layerFource.value & (1 << collision.gameObject.layer)) > 0)
        {
            if (currentFource)
            {

            }
            else
            {
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = directionFource * fourceValue;
            }
        }

    }
}

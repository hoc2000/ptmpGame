using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnderFlame : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float range;
    [SerializeField] private LayerMask hitLayer;

    public Vector2 dir;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, range, hitLayer);
        Debug.DrawRay(transform.position, dir * range, Color.red); 
        if (hit.collider.tag == Constants.TAG.PLAYER)
        {
            Player.Instance.TakeDamage(damage);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    public int dame;
    private void Start()
    {
        //StartMove();
        int atkDame = dame;
        for (int i = 0; i < GameManager.levelSelected; i++)
        {
            atkDame = atkDame + atkDame * 10 / 100;
        }

        dame = atkDame;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.TAG.PLAYER)
        {
            Player.Instance.TakeDamage(dame);

        }

    }
}

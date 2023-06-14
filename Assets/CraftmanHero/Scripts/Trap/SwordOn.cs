using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordOn : MonoBehaviour
{
    [SerializeField] TrapSword trapSword;
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == Constants.TAG.PLAYER)
        {
            StartCoroutine(trapSword.Attack());
        }
        
    }
}

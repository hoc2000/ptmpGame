using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangeCheck : MonoBehaviour
{
    [SerializeField] Vector2 sizeAttack;
    [SerializeField] SpriteRenderer spriteCheck;
    [SerializeField] LayerMask layerAttack;
    void Start()
    {

    }
    private void Update()
    {
        CheckIsRange();
    }

    // Update is called once per frame
    public bool CheckIsRange()
    {
        if (Physics2D.OverlapBox(transform.position, sizeAttack, 0, layerAttack))
        {
            spriteCheck.color = Color.red;
            return true;
        }

        spriteCheck.color = Color.white;
        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(transform.position, sizeAttack);
    }
}

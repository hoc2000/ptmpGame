using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Transform[] vtMove;
    int index;
    public float speed;
    public bool moveDown, canMove;
    bool inscrea = true;
    Transform nextPos;
    [SerializeField] private int nextPosIndex;

    private void Start()
    {
        if (vtMove.Length != 0)
        {
            nextPos = vtMove[0];

        }
    }
    private void FixedUpdate()
    {
        //if (!canMove)
        //    return;

        //if (moveDown)
        //{
        //    //Vector3 target = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        //    transform.position = Vector3.MoveTowards(transform.position, end.position, speed * Time.deltaTime);
        //    if (Mathf.Abs(transform.position.y - end.position.y) > 0.2f)
        //    {
        //        //wheel.transform.Rotate(new Vector3(0, 0, -speed));

        //    }
        //    else
        //        canMove = false;
        //}
        //else
        //{
        //    Vector3 target = Vector3.MoveTowards(transform.position, start.position, speed * Time.deltaTime);
        //    transform.position = new Vector3(transform.position.x, target.y, transform.position.z);
        //    if (Mathf.Abs(transform.position.y - start.position.y) > 0.2f)
        //    {
        //    {
        //        //wheel.transform.Rotate(new Vector3(0, 0, speed));

        //    }
        //    else
        //        canMove = false;
        //}
        if (!canMove && index == vtMove.Length)
        {
            return;
        }

        if (canMove && vtMove.Length != 0)
        {
            if (transform.position == nextPos.position)
            {
                nextPosIndex++;
                if (nextPosIndex >= vtMove.Length)
                {
                    nextPosIndex = 0;
                }
                nextPos = vtMove[nextPosIndex];
            }
            else
            {
                transform.position = Vector3.MoveTowards(transform.position, nextPos.position, speed * Time.fixedDeltaTime);
            }

            if (vtMove.Length == 0)
            {
            }
        }
       


        //if((index == 0 || inscrea) && index < vtMove.Length - 1) {
        //    index++;
        //    inscrea = true;
        //} else if ((index == vtMove.Length - 1 || !inscrea) && index != 0)
        //{
        //    inscrea = false;
        //    index--;
        //}
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

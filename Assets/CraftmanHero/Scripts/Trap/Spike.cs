using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public Transform[] vtMove;
    int index;
    public float speed;
    bool inscrea = true;
    public Transform bodySaw;
    float angleZ;
    private void Start()
    {
        //StartMove();
    }
    private void Update()
    {
        if (Mathf.Abs(transform.position.x - PlayerPos.instance.transform.position.x) < 15)
        {
            if (vtMove.Length != 0)
                Move();
            else
            {
                bodySaw.Rotate(new Vector3(0, 0, 5));
            }
        }
    }

    void Move()
    {
        float distance = Mathf.Abs((transform.position - vtMove[index].position).magnitude);
        Vector3 dir = vtMove[index].position - transform.position;
        if (distance > 0.4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, vtMove[index].position, Time.deltaTime * speed);
            angleZ += (dir.x < 0 ? 2 : -2) * speed;
        }
        else if ((index == 0 || inscrea) && index < vtMove.Length - 1)
        {
            index++;
            inscrea = true;
        }
        else if ((index == vtMove.Length - 1 || !inscrea) && index != 0)
        {
            inscrea = false;
            index--;
        }
        bodySaw.rotation = Quaternion.Euler(new Vector3(0, 0, angleZ));
    }
}

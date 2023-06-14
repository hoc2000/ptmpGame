using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public bool leftClick, rightClick, jumpClick, dashClick, attackClick;
    public bool leftMove, rightMove, jump, dash, attack;// dashLeft, dashRight, down;
    void Start()
    {

    }
    void Update()
    {
        if (dashClick)
        {
            dash = true;
        }
        else
        {
            dash = false;
        }

        if (Input.GetKey(KeyCode.A) || leftClick)
        {
            if (!dash)
            {
                leftMove = true;
            }
        }
        else
        {
            leftMove = false;
        }
        if (Input.GetKey(KeyCode.D) || rightClick)
        {
            if (!dash)
            {
                rightMove = true;
            }
        }
        else
        {
            rightMove = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) || jumpClick)
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        if (attackClick)
        {
            attack = true;
        }
        else
        {
            attack = false;
        }
        //if (Input.GetKeyDown(KeyCode.S))
        //{
        //    down = true;
        //}
        //else
        //{
        //    down = false;
        //}
    }
}

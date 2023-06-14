using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{

    [Header("SETUP")]
    [SerializeField] float speed;
    [SerializeField] float force;
    [SerializeField] float gravity;

    [SerializeField] LayerMask layerGround;
    [Header("RIGIBODY")]
    [SerializeField] Rigidbody rig;
    [SerializeField] InputManager inputManager;
    bool leftMove, rightMove, jump;
    float vertical;
    public Vector3 movement;
    public bool canJump;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CheckIsGround();
    }
    private void FixedUpdate()
    {
        Movement();
    }
    void SetDiretionMove()
    {
        if (inputManager.leftMove)
        {
            vertical = -1f;
        }
        else if (inputManager.rightMove)
        {
            vertical = 1f;
        }
        else
        {
            vertical = 0f;
        }

        if (inputManager.jump)
        {
            movement.y = JumpHeight();
        }
        movement.y += gravity * Time.deltaTime;
        movement.x = vertical * speed * Time.deltaTime;
    }
    void Movement()
    {
        if (inputManager.leftMove)
        {
            vertical = -1f;
        }
        else if (inputManager.rightMove)
        {
            vertical = 1f;
        }
        else
        {
            vertical = 0f;
        }

        if (inputManager.jump && canJump)
        {
            canJump = false;
            movement.y = JumpHeight();

            //rig.velocity = new Vector3(rig.velocity.x, movement.y, 0f);
        }


        movement.x = vertical * speed * Time.fixedDeltaTime;
        //Mathf.Lerp(0f, JumpHeight(), movement.y);
        if (!canJump)
        {
            movement.y += gravity * Time.fixedDeltaTime;
        }
        else
        {
            movement.y = 0f;
        }

        rig.velocity = movement;
        //rig.velocity = new Vector3(movement.x * Time.fixedDeltaTime, rig.velocity.y, 0f);
        //rig.velocity = new Vector3(movement.x, rig.velocity.y, 0f);
        //rig.AddForce(movement);

    }

    float JumpHeight()
    {
        return force;
    }
    public float disTnaceRay;
    void CheckIsGround()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, disTnaceRay, layerGround))
        {

            canJump = true;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.red);
        }
        else
        {
            canJump = false;
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * disTnaceRay, Color.red);

        }
    }
}

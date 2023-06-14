using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using Com.LuisPedroFonseca.ProCamera2D;
using DarkTonic.MasterAudio;
using Spine.Unity;
using Spine;

public class CharacterMovement2D : MonoBehaviour
{
    [Header("SETUP")]
    public InputManager inputManager;
    public AnimationController animationController;
    public PlayerFxController playerFxController;
    [SerializeField] Rigidbody2D rig;
    [SerializeField] Collider2D charCollider;
    [SerializeField] ProCamera2D proCamera2D;
    Player player;

    [Header("MOVEMENT")]
    [SerializeField] float speedMove;
    [SerializeField] float speedMoveAttack;
    [SerializeField] float jumpForce;
    [SerializeField] float downForce;
    [SerializeField] float dashFource;
    [SerializeField] float timeDash;
    [SerializeField] float timeResetDash;
    [SerializeField] float airMoveSpeed;
    [SerializeField] int countJump;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPoint;
    [SerializeField] Vector2 groundCheckSize;
    private bool grounded;
    private bool canJump;
    private float XDirectionalInput;
    private bool facingRight = true;
    private bool isMoving;
    bool animJump;
    bool animDown;
    bool canDown;
    bool canControll = true;
    int currenCountJump;
    bool isDashing;
    bool canDash = true;
    float currentTimeDash;
    float speed;
    [Header("BASH")]
    [SerializeField] float radius;
    [SerializeField] GameObject bashAbleObj;
    bool nearToBashAbleObj;
    bool isBashing;
    [SerializeField] float bashPower;
    [SerializeField] float bashTime;
    [SerializeField] GameObject arrow;
    [SerializeField] Vector3 bashDir;
    float bashTimeReset;
    bool isChosingDir;
    bool jumpBash;
    [Header("WALL")]
    [SerializeField] float wallSlideSpeed = 0;
    [SerializeField] float wallJumpForce;
    [SerializeField] float wallJumpDirection;
    [SerializeField] Vector2 wallJumpAngle;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] Transform wallCheckPoint;
    [SerializeField] Vector2 wallCheckSize;
    bool isTouchingWall;
    bool isWallSliding;
    [Header("ATTACK")]
    [SerializeField] int idAttack;
    [SerializeField] float timeAttack;
    [SerializeField] float timeCowdownAttack;
    [SerializeField] float attackRange;
    [SerializeField] float timeResetComboAttack;
    float timeResetCombo;
    //[SerializeField] bool startAttack;
    [SerializeField] bool isAttack;
    [SerializeField] Vector2 dashHitAttack;
    [SerializeField] Transform attackPoin;
    [SerializeField] Transform attackPoinDash;
    public LayerMask enemyLayers;

    [SerializeField] int idWeapon;
    float directionChar = 1;
    public bool canMove = true;

    public SkeletonMecanim skeletonMecanim;
    public Skeleton skeleton;

    void Start()
    {
        Application.targetFrameRate = 60;

        bashTimeReset = bashTime;
        wallJumpAngle.Normalize();
        timeCowdownAttack = timeAttack;
        player = Player.Instance;
        skeleton = skeletonMecanim.skeleton;
        player.playerInform.Init(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.Instance.IsDeath() || GameManager.isWin)
        {
            return;
        }
        CheckWorld();
        Bash();
        InputController();
        CheckAttack();
        //if (startAttack)
        //{
        //    CheckAttackCombo();
        //}
        //Debug.Log(comboTimeCheck);
        //Debug.Log(idAttack);

        animationController.SetRuntimeAnimatorController(idWeapon);
    }
    private void FixedUpdate()
    {if (Player.Instance.IsDeath() || GameManager.isWin)
        {
            return;
        }
        //animationController.SetVelocityY(rig.velocity.y);
        if (!canControll)
        {
            return;
        }
        if (!isBashing)
        {
            Movement();
        }
        //WallSlide();
        //WallJump();
    }
    void InputController()
    {
        
        if (inputManager.dash)
        {
            if (canDash && canControll)
            {
                isDashing = true;
            }

        }else if (inputManager.leftMove)
        {
            XDirectionalInput = -1;
            directionChar = -1;
        }
        else if (inputManager.rightMove)
        {
            XDirectionalInput = 1;
            directionChar = 1;
        }
        else
        {
            XDirectionalInput = 0;
        }

        if (inputManager.jump)
        {

            if (currenCountJump < countJump)
            {
                canJump = true;
            }
        }
        //if (inputManager.down)
        //{
        //    if (!grounded)
        //    {
        //        canDown = true;
        //    }
        //}
    }
    void CheckWorld()
    {
        grounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        isTouchingWall = Physics2D.OverlapBox(wallCheckPoint.position, wallCheckSize, 0f, wallLayer);

        if (grounded)
        {
            if (!animJump)
            {
                //anim.SetInteger("Jump", 2);
            }
            if (animDown)
            {
                animDown = false;
                //anim.SetBool("Down", false);
                CameraShake.Instance.Shakedown(0.2f, 0.6f);
                ContentMgr.Instance.GetItem("DownFx", groundCheckPoint.transform.position, Quaternion.identity);

            }
            if (!canControll && !isDashing)
            {
                //trail.emitting = false;
                canControll = true;
            }
            if (currenCountJump != 0)
            {
                playerFxController.FxLand();

                currenCountJump = 0;
            }
            animationController.SetJumpAnim(false);
        }
        else
        {
            if (currenCountJump == 0)
            {
                currenCountJump = 1;               
            }
        }
        animationController.SetFallAnim(!grounded);
    }
    #region Movement
    void Movement()
    {
        if (canMove && !isTouchingWall)
        {
            rig.velocity = new Vector2(XDirectionalInput * speed * Time.fixedDeltaTime, rig.velocity.y);
        }
        //if (grounded)
        //{
        //    rig.velocity = new Vector2(XDirectionalInput * speed * Time.fixedDeltaTime, rig.velocity.y);
        //}
        //else if (!grounded && !isWallSliding && XDirectionalInput != 0)
        //{
        //    rig.velocity = new Vector2(XDirectionalInput * speed * Time.fixedDeltaTime, rig.velocity.y);
        //    //rig.AddForce(new Vector2(airMoveSpeed * XDirectionalInput, 0));
        //    //if (Mathf.Abs(rig.velocity.x) > speed)
        //    //{
        //    //    rig.velocity = new Vector2(XDirectionalInput * speed * Time.fixedDeltaTime, rig.velocity.y);
        //    //}
        //}
        animationController.SetSpeedAnim(Mathf.Abs(XDirectionalInput));

        if (XDirectionalInput < 0 && facingRight)
        {
            Flip();
        }
        else if (XDirectionalInput > 0 && !facingRight)
        {
            Flip();
        }

        //Jump
        if (canJump && currenCountJump < countJump && !isWallSliding) //&& grounded
        {
            inputManager.jumpClick = false;
            inputManager.jump = false;
            playerFxController.FxJump(grounded);
            Jump();
            animJump = true;
            //anim.SetInteger("Jump", currenCountJump);
            int x = Random.Range(1f, 2f) < 1.5f ? 1 : 2;
            if (currenCountJump == 1) currenCountJump = x;
            animationController.SetVelocityY(currenCountJump);
            canJump = false;
            currenCountJump++;
            animationController.SetJumpAnim(true);
        }

        //Down
        if (canDown)
        {
            //trail.emitting = true;
            canControll = false;
            canDown = false;
            animDown = true;
            //anim.SetBool("Down", true);
            rig.isKinematic = true;
            rig.velocity = Vector2.zero;
        }

        // Dash
        if (isDashing && canControll && canDash)
        {
            MasterAudio.PlaySound(Constants.AUDIO.PLAYERDASH);
            //trail.emitting = true;
            playerFxController.FxDash(grounded, directionChar);
            canMove = false;
            rig.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            canControll = false;
            canDash = false;
            rig.velocity = new Vector2(directionChar * dashFource, 0f);
            //animationController.SetIdWeapon(idWeapon);
            animationController.SetDashAnim(true);

            Invoke("CheckHitDash", 0.01f);
            Invoke("EndDash", timeDash);
            Invoke("ResetDash", timeResetDash);

        }
    }
    void Flip()
    {
        wallJumpDirection *= -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);
    }

    public void Jump()
    {
        MasterAudio.PlaySound(Constants.AUDIO.PLAYERJUMP);
        rig.velocity = new Vector2(rig.velocity.x, JumpHeight());

    }
    public void EndJump()
    {
        animJump = false;
    }
    public void DownAttack()
    {
        canDown = false;
        animJump = false;
        rig.isKinematic = false;
        rig.velocity = new Vector2(0f, -downForce);
    }
    public void EndDown()
    {

    }
    void Dash()
    {

    }
    void EndDash()
    {
        //trail.emitting = false;
        //rig.constraints = RigidbodyConstraints2D.None;
        rig.constraints = RigidbodyConstraints2D.FreezeRotation;
        isDashing = false;
        canControll = true;
        canMove = true;
        animationController.SetDashAnim(false);
    }
    void CheckHitDash()
    {
        if (idWeapon == 0)
        {
            return;
        }
        
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(attackPoinDash.position, dashHitAttack,0f, enemyLayers);

        bool isHitEnemy = false;
        
        foreach (Collider2D enemy in hitEnemies)
        {
            isHitEnemy = true;
            enemy.GetComponent<EnemyBase>().TakeDamage(player.playerInform.AtkDash());
        }

        if (isHitEnemy)
        {
            ProCamera2DShake.Instance.Shake(0);
        }
    }
    void ResetDash()
    {
        canDash = true;
    }
    float JumpHeight()
    {
        return jumpForce;
    }
    #endregion

    #region WALL
    void WallSlide()
    {
        if (isTouchingWall && !grounded && rig.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }

        if (isWallSliding)
        {
            rig.velocity = new Vector2(rig.velocity.x, wallSlideSpeed * Time.fixedDeltaTime);
        }
    }
    void WallJump()
    {
        if (isWallSliding || isTouchingWall)
        {
            if (!canJump)
            {
                return;
            }
            rig.AddForce(new Vector2(wallJumpForce * wallJumpDirection * wallJumpAngle.x, wallJumpForce * wallJumpAngle.y), ForceMode2D.Impulse);
            canJump = false;
            Flip();
        }
    }
    #endregion
    public float disTnaceRay;


    ////////////// Bash
    #region Bash
    void Bash()
    {
        RaycastHit2D[] rays = Physics2D.CircleCastAll(transform.position, radius, Vector3.forward);
        foreach (RaycastHit2D ray in rays)
        {
            nearToBashAbleObj = false;
            if (ray.collider.tag == "BashAble")
            {
                nearToBashAbleObj = true;
                bashAbleObj = ray.collider.transform.gameObject;
                break;
            }
        }
        if (nearToBashAbleObj)
        {
            bashAbleObj.GetComponent<SpriteRenderer>().color = Color.yellow;
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                Time.timeScale = 0;
                bashAbleObj.transform.localScale = new Vector2(0.8f, 0.8f);
                arrow.SetActive(true);
                arrow.transform.position = bashAbleObj.transform.position;
                isChosingDir = true;
            }
            else if (isChosingDir && Input.GetKeyUp(KeyCode.Mouse1))
            {
                Time.timeScale = 1f;
                bashAbleObj.transform.localScale = new Vector2(0.5f, 0.5f);
                isChosingDir = false;
                isBashing = true;
                rig.velocity = Vector2.zero;
                transform.position = bashAbleObj.transform.position;
                bashDir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position); ;//Camera.main.ScreenToViewportPoint(Input.mousePosition) - transform.position;
                bashDir.z = 0f;
                //if (bashDir.x > 0)
                //{
                //    transform.eulerAngles = Vector3.zero;
                //}
                //else
                //{
                //    transform.eulerAngles = new Vector3(0f, 180f, 0f);
                //}
                bashDir = bashDir.normalized;
                bashAbleObj.GetComponent<Rigidbody2D>().AddForce(-bashDir * 500, ForceMode2D.Impulse);
                arrow.SetActive(false);

            }
        }
        else if (bashAbleObj != null)
        {
            bashAbleObj.GetComponent<SpriteRenderer>().color = Color.white;
        }
        //Powere

        if (isBashing)
        {
            if (bashTime > 0)
            {
                bashTime -= Time.deltaTime;
                //rig.velocity = bashDir * bashPower * Time.deltaTime;
                JumpBash();
            }
            else
            {
                isBashing = false;
                jumpBash = false;
                bashTime = bashTimeReset;
                rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y);
            }
        }
    }

    void JumpBash()
    {
        if (jumpBash)
        {
            return;
        }
        jumpBash = true;
        rig.velocity = bashDir * bashPower;
    }
    #endregion
    #region ATTACK
    void CheckAttack()
    {
        if (timeResetCombo >0 )
        {
            timeResetCombo -= Time.deltaTime;
        }
        else
        {
            idAttack = 0;
        }
        if (timeCowdownAttack > 0)
        {
            timeCowdownAttack -= Time.deltaTime;
            speed = speedMoveAttack;
            animationController.SetIsAttackAnim(true);
        }
        else
        {
            if (inputManager.attack)
            {
                Attack();
            }
            speed = speedMove;
            animationController.SetIsAttackAnim(false);
        }
    }
    public virtual void Attack()
    {
        
        //startAttack = true;
        isAttack = true;
        timeCowdownAttack = timeAttack;
        timeResetCombo = timeResetComboAttack;
        
        SetCountAttack();
        Invoke("CheckDameAttack", 0.3f);


    }
    void CheckDameAttack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoin.position, attackRange, enemyLayers);

        bool isHitEnemy = false;

        foreach (Collider2D enemy in hitEnemies)
        {
            isHitEnemy = true;
            Debug.Log("We hit + " + enemy.name);
            enemy.GetComponent<EnemyBase>().TakeDamage(player.playerInform.AtkDame());
        }

        if (isHitEnemy)
        {
            ProCamera2DShake.Instance.Shake(0);
        }
    }
    //private void CheckAttackCombo()
    //{
    //    comboTimeCheck += Time.deltaTime;
    //    if (comboTimeCheck > 0 && comboTimeCheck < 0.1f && isAttack)
    //    {
    //        idAttack = 0;
    //        isAttack = false;
    //    }
    //    else if (comboTimeCheck > 0.1f && comboTimeCheck < 0.3f && isAttack)
    //    {
    //        idAttack = 1;
    //        isAttack = false;
    //    }
    //    else if (comboTimeCheck > 0.3f && comboTimeCheck < 0.6f && isAttack)
    //    {
    //        idAttack = 2;
    //        //comboTimeCheck = 0;
    //        isAttack = false;
    //    }
    //    else if (comboTimeCheck > 0.6f)
    //    {
    //        idAttack = 0;
    //        isAttack = false;
    //        startAttack = false;
    //        //comboTimeCheck = 0;
    //    }
    //}
    void SetCountAttack()
    {
        //comboTimeCheck += Time.deltaTime;

        MasterAudio.PlaySound(Constants.AUDIO.PLAYERATTACK);
        idAttack++;
        if (idAttack > 3)
        {
            idAttack = 1;
        }
        switch (idAttack)
        {
            case 1:
                if (idWeapon ==0)
                {
                    timeAttack = 0.667f - 0.667f * 0.4f;
                }
                else
                {
                    //MasterAudio.PlaySound(Constants.AUDIO.SWORDATTACK);
                    timeAttack = 0.6f - 0.6f * 0.4f;
                }
                
                animationController.SetAttackAnim(0f);
                break;
            case 2:
                if (idWeapon == 0)
                {
                    timeAttack = 0.567f - 0.567f*0.4f;
                }
                else
                {
                    MasterAudio.PlaySound(Constants.AUDIO.SWORDATTACK);
                    timeAttack = 0.5f - 0.5f * 0.4f;
                }
                animationController.SetAttackAnim(0.5f);
                break;
            case 3:
                if (idWeapon == 0)
                {
                    timeAttack = 0.633f - 0.633f * 0.4f;
                }
                else
                {
                    MasterAudio.PlaySound(Constants.AUDIO.SWORDATTACK2);
                    timeAttack = 0.567f - 0.567f * 0.4f;
                }      
                animationController.SetAttackAnim(1f);
                break;
        }
    }

    public int _idWeapon
    {
        get {return idWeapon; }
        set { idWeapon = value; }
    }
    #endregion

    internal void UpdateSkin(string skin)
    {
        idWeapon = 1;
        skeleton.SetSkin(skin);
        skeleton.SetSlotsToSetupPose();
        skeletonMecanim.LateUpdate();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);


        Gizmos.color = Color.blue;
        Gizmos.DrawCube(groundCheckPoint.position, groundCheckSize);


        Gizmos.color = Color.red;
        Gizmos.DrawCube(wallCheckPoint.position, wallCheckSize);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPoin.position, attackRange);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(attackPoinDash.position, dashHitAttack);
    }
}

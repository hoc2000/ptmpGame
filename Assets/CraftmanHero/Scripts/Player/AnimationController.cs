using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] RuntimeAnimatorController animNormal;
    [SerializeField] RuntimeAnimatorController animWeapon;
    const string SPEED = "Speed";
    const string FALL = "Fall";
    const string DASH = "Dash";
    const string JUMP = "Jump";
    const string ATTACK = "Attack";
    const string ISATTACK = "IsAttack";
    const string VELOCITYY = "VelocityY";
    const string IDWEAPON = "IdWeapon";
    const string DIE = "Die";
    const string WIN = "Win";

    public void Init()
    {

    }
    public void SetIdWeapon(float value)
    {
        anim.SetFloat(IDWEAPON, value);
    }
    public void SetSpeedAnim(float value)
    {
        anim.SetFloat(SPEED, value);
    }

    public void SetAttackAnim(float value)
    {
        anim.SetFloat(ATTACK, value);
    }
    public void SetIsAttackAnim(bool value)
    {
        anim.SetBool(ISATTACK, value);
    }
    public void SetFallAnim(bool value)
    {
        anim.SetBool(FALL, value);
    }
    public void SetDashAnim(bool value)
    {
        anim.SetBool(DASH, value);
    }
    public void SetJumpAnim(bool value)
    {
        anim.SetBool(JUMP, value);
    }
    public void SetVelocityY(int value)
    {
        anim.SetFloat(VELOCITYY, value);
        //if (value > 0)
        //{
        //    anim.SetFloat(VELOCITYY, 1f);
        //}
        //else
        //{
        //    anim.SetFloat(VELOCITYY, 0f);
        //}

    }
    public void SetDieAnim()
    {
        anim.SetTrigger(DIE);
    }
    public void SetWinAnim()
    {
        anim.SetTrigger(WIN);
    }

    public void SetRuntimeAnimatorController(int id)
    {
        if (id ==0) {
            anim.runtimeAnimatorController = animNormal;
        }
        else
        {
            anim.runtimeAnimatorController = animWeapon;
        }
    }
}

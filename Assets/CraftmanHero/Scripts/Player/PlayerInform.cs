using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInform : MonoBehaviour
{
    CharacterMovement2D charPlayer;
    [SerializeField] protected int maxHp;
    [SerializeField] protected int currentHp;
    [SerializeField] protected int atkNormal;
    [SerializeField] protected int atkWeapon;
    [SerializeField] protected int atkDash;
    
    public int CurrentHP
    {
        get { return currentHp; }
        set { currentHp = value; }
    }
    public int MaxHp
    {
        get { return maxHp; }
    }
    public int MaxHP()
    {
        return maxHp;
    }
    public int AtkDame()
    {
        if (charPlayer._idWeapon ==0)
        {
            return atkNormal;
        }
        else
        {
            return atkWeapon;
        }

      
    }
    public int AtkWeapon()
    {
        return atkWeapon;

    }

    public int AtkDash()
    {
        return atkDash;

    }

    public void Init(CharacterMovement2D charPlayer)
    {
        this.charPlayer = charPlayer;
        if (Gamedata.I!=null)
        {
            maxHp = Gamedata.I.HpPlayer;
            atkNormal = Gamedata.I.AtkPlayer;
        }
        
        currentHp = maxHp;
        atkWeapon = atkNormal + atkNormal * 50/100;
        atkDash = atkNormal*3;
    }
}

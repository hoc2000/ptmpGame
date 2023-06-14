using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;


public class ControllUIButton : MonoBehaviour
{

    //public Image iconLeft, iconLeft1, iconRight, iconRight1, iconJump, iconJump1, iconAttack, iconAttack1, iconHand, iconHand1;

    //[SerializeField] InputManager inputManager;

    //private void OnEnable()
    //{
    //    inputManager = Player.Instance.characterMovement2D.inputManager;
    //}

    public void LeftButtonDown()
    {
        Player.Instance.characterMovement2D.inputManager.leftClick = true;

        Color32 color = new Color32(255, 255, 255, 200);
        //iconLeft.color = color;
        //iconLeft1.color = color;
    }
    public void LeftButtonUp()
    {

        Player.Instance.characterMovement2D.inputManager.leftClick = false;

        Color32 color = new Color32(255, 255, 255, 150);
        //iconLeft.color = color;
        //iconLeft1.color = color;
    }
    public void RightButtonDown()
    {
        Player.Instance.characterMovement2D.inputManager.rightClick = true;

        Color32 color = new Color32(255, 255, 255, 200);
        //iconRight.color = color;
        //iconRight1.color = color;
    }
    public void RightButtonUp()
    {
        Player.Instance.characterMovement2D.inputManager.rightClick = false;

        Color32 color = new Color32(255, 255, 255, 150);
        //iconRight.color = color;
        //iconRight1.color = color;
    }
    public void JumpButtonDown()
    {
        Player.Instance.characterMovement2D.inputManager.jumpClick = true;

        Color32 color = new Color32(255, 255, 255, 200);
        //iconJump.color = color;
        //iconJump1.color = color;
    }
    public void JumpButtonUp()
    {
        Player.Instance.characterMovement2D.inputManager.jumpClick = false;


        Color32 color = new Color32(255, 255, 255, 150);
        //iconJump.color = color;
        //iconJump1.color = color;
    }
    public void DashButtonDown()
    {
        Player.Instance.characterMovement2D.inputManager.dashClick = true;

        Color32 color = new Color32(255, 255, 255, 200);
        //iconHand.color = color;
        //iconHand1.color = color;
    }
    public void DashButtonUp()
    {
        Player.Instance.characterMovement2D.inputManager.dashClick = false;

        Color32 color = new Color32(255, 255, 255, 150);
        //iconHand.color = color;
        //iconHand1.color = color;
    }
    public void AttackButtonDown()
    {
        Player.Instance.characterMovement2D.inputManager.attackClick = true;

        Color32 color = new Color32(255, 255, 255, 200);
        //iconAttack.color = color;
        //iconAttack1.color = color;
    }
    public void AttackButtonUp()
    {
        
        Player.Instance.characterMovement2D.inputManager.attackClick = false;

        Color32 color = new Color32(255, 255, 255, 150);
        //iconAttack.color = color;
        //iconAttack1.color = color;
    }
}

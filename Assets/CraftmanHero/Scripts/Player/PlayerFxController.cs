using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFxController : MonoBehaviour
{
    [SerializeField] Transform jumpFxTrans;
    [SerializeField] Transform doubleJumpFxTrans;
    [SerializeField] Transform dashAirFxTrans;
    [SerializeField] Transform dashLandFxTrans;
    [SerializeField] Transform landFxTrans;



    // Update is called once per frame
    public void FxJump(bool isGround)
    {
        if (isGround)
        {
            //ContentMgr.Instance.GetItem("FxJumpPlayer", jumpFxTrans.position, Quaternion.identity);
        }
        else
        {
            //ContentMgr.Instance.GetItem("FxDoubleJumpPlayer", doubleJumpFxTrans.position, Quaternion.identity);
        }

    }
    public void FxDash(bool isGround, float direction)
    {
        if (isGround)
        {
            //var fx = ContentMgr.Instance.GetItem("FxDashLandPlayer", dashLandFxTrans.position, Quaternion.identity);
            //fx.transform.localScale = new Vector3(direction, 1f, 1f);
        }
        else
        {
            //var fx = ContentMgr.Instance.GetItem("FxDashAirPlayer", dashAirFxTrans.position, Quaternion.identity);
            //fx.transform.localScale = new Vector3(direction, 1f, 1f);
        }
    }
    public void FxLand()
    {
        //ContentMgr.Instance.GetItem("FxLandPlayer", landFxTrans.position, Quaternion.identity);
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Purchasing;

public class Helper : MonoBehaviour
{
    public static IEnumerator StartAction(UnityAction action, float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);
        action();
    }
    public static IEnumerator StartActionRealtimes(UnityAction action, float timeDelay)
    {
        yield return new WaitForSecondsRealtime(timeDelay);
        action();
    }

   
    public static bool VerifyIap(Product product)
    {
        bool validPurchase = true;
        Gamedata.IsIAPUser = validPurchase;
        return validPurchase;

    }
}

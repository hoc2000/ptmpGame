using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestoreButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform != RuntimePlatform.IPhonePlayer && Application.platform != RuntimePlatform.OSXPlayer)
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void ClickRestore()
    {
        // IAPManager.Instance.RestorePurchases();
    }
}

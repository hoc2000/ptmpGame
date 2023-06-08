using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeManager : MonoBehaviour
{
    // Start is called before the first frame update
    public void FadeInComplete()
    {
        ToastMgr.Instance.FadeTrue();
    }
    public void FadeOuTComplete()
    {
        ToastMgr.Instance.CloseFade();
    }
}

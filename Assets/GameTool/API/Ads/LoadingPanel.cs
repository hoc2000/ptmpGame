using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingPanel : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke("DeactiveAuto", 20);
    }

    void DeactiveAuto()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeActive : MonoBehaviour
{
    public float time;
    private void OnEnable()
    {
        Invoke("DeActiveObj", time);
    }
    void DeActiveObj()
    {
        ContentMgr.Instance.Despaw(gameObject);
    }
}

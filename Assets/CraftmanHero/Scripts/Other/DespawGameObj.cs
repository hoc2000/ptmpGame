using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawGameObj : MonoBehaviour
{
    [SerializeField] float time;

    private void OnEnable()
    {
        Invoke("DespawObj", time);
    }

    void DespawObj()
    {
        ContentMgr.Instance.Despaw(gameObject);
    }
}

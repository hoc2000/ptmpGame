using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class HpBar : MonoBehaviour
{
    [Header("HEART BAR")]
    [SerializeField] Image hpBar1;
    [SerializeField] Image hpBar2;

    [SerializeField] float timeUpdate;
    public float time;

    bool isCanUpdateBar2;

    float maxHp, currentHp;

    void Start()
    {
        
    }
    // Update is called once per frame
    public virtual void Update()
    {
        if (isCanUpdateBar2)
        {
            time += Time.deltaTime;
            if (time >= timeUpdate)
            {
                UpdateHpBar2();
            }
        }
    }
    public virtual void UpdateHpBar(float currentHp, float maxHp)
    {
        this.currentHp = currentHp;
        this.maxHp = maxHp;

        float value = currentHp / maxHp;

        hpBar1.DOFillAmount(value, 0.1f);
        isCanUpdateBar2 = true;
        time = 0f;
    }

    void UpdateHpBar2()
    {
        isCanUpdateBar2 = false;

        float value = currentHp / maxHp;
        hpBar2.DOFillAmount(value, 0.3f);
    }
}

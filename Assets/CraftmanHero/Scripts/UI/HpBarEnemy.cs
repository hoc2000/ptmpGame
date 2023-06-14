using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBarEnemy : HpBar
{
    [SerializeField] Transform target;

    [SerializeField] float timeDeactive;
    float timeCountdown;

    float distance;
    void Start()
    {
        distance = transform.position.y - target.position.y;
    }

    public override void Update()
    {
        transform.position = new Vector2(target.position.x, target.position.y + distance);
        if (timeCountdown > 0)
        {
            timeCountdown -= Time.deltaTime;
        }
        else
        {
            gameObject.SetActive(false);
        }
        base.Update();
    }

    public override void UpdateHpBar(float currentHp, float maxHp)
    {
        timeCountdown = timeDeactive;
        base.UpdateHpBar(currentHp, maxHp);
    }
}

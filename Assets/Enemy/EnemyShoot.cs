using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Enemy.Base;

public class EnemyShoot : EnemyBase
{

    [SerializeField] int bulletCount;
    [SerializeField] string nameBullet;
    [SerializeField] Transform shootTranform;
    [SerializeField] float distanceBettwenBullet;
    [SerializeField] Vector2 minForce, maxForce;
    void Start()
    {
        StartCoroutine(Shoot());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator Shoot()
    {
        yield return new WaitForSeconds(2f);
        for (int i = 0; i < bulletCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            var bullet = ContentMgr.Instance.GetItem(nameBullet, shootTranform.position, Quaternion.identity);
            ShootParabol(bullet);
        }
        yield return new WaitForSeconds(timeDelayAttack);
        StartCoroutine(Shoot());
    }

    void ShootParabol(GameObject bullet)
    {
        Rigidbody2D bulletRig = bullet.GetComponent<Rigidbody2D>();
        bulletRig.velocity += new Vector2(UnityEngine.Random.Range(minForce.x, maxForce.x), UnityEngine.Random.Range(minForce.y, maxForce.y));
    }
}

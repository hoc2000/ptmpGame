using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Spine.Unity;
using DarkTonic.MasterAudio;

public class BossStray : EnemyBase
{
    [Header("BOSS SETTING")]
    [SerializeField] private GameObject effStun;
    [SerializeField] private GameObject arrow;
    [SerializeField] private Transform shootingPoint, shootingPoint2;
    [SerializeField] private GameObject teleFx;
    [Header("MAP EFFECT")]
    [SerializeField] CheckStartBoss checkStartBoss;
    private int dir;
    [Header("ANIMATION")]
    [SpineAnimation]
    public string teleAnimation;

    [SpineAnimation]
    public string shootAnimation;

    [SpineAnimation]
    public string shoot2Animation;

    [SpineAnimation]
    public string stunAnimation;

    [SpineAnimation]
    public string idleAnimation;

    [SpineAnimation]
    public string tele2Animation;

    protected override void Start()
    {
        base.Start();
        StartCoroutine(Shoot(1));
    }

    private IEnumerator Shoot(int dir)
    {
        yield return new WaitForSeconds(1);
        SetAnim(shootAnimation, true);
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(0.5f);
            MasterAudio.PlaySound(Constants.AUDIO.ENEMY2ATTACK);
            GameObject newArrow = Instantiate(arrow, shootingPoint.position, Quaternion.identity);
            newArrow.SetActive(true);
            if (transform.position == pos1.position)
            {
                newArrow.transform.eulerAngles = Vector3.zero;
            }
            else newArrow.transform.eulerAngles = new Vector3(0, 180, 0);
            newArrow.GetComponent<Rigidbody2D>().velocity = Vector2.left * dir * 400 * Time.fixedDeltaTime;
            yield return new WaitForSeconds(0.6f);
        }
        StartCoroutine(Teleport());
    }

    private IEnumerator Teleport()
    {
        SetAnim(teleAnimation, false);
        yield return new WaitForSeconds(0.267f);
        MasterAudio.PlaySound(Constants.AUDIO.TELEPORT);
        GameObject fx = Instantiate(teleFx);
        fx.transform.position = transform.position;
        transform.position = new Vector3(transform.position.x, transform.position.y - 100, transform.position.z);
        yield return new WaitForSeconds(1f);              
        if (transform.position.x == pos1.position.x)
        {
            transform.position = pos2.position;
            transform.eulerAngles = new Vector3(0, 180, 0);
            dir = -1;
            StartCoroutine(Shoot(dir));
        }
        else if (transform.position.x == pos2.position.x)
        {
            transform.position = pos1.position;
            transform.eulerAngles = Vector3.zero;
            dir = 1;
            StartCoroutine(SkyShoot());
        }
        fx.transform.position = transform.position;
        fx.GetComponent<ParticleSystem>().Play();
        MasterAudio.PlaySound(Constants.AUDIO.TELEPORT);
        SetAnim(tele2Animation, false);
    }

    private IEnumerator SkyShoot()
    {
        yield return new WaitForSeconds(1);
        SetAnim(shoot2Animation, false);
        yield return new WaitForSeconds(0.5f);
        GameObject[] arrows = new GameObject[3];
        MasterAudio.PlaySound(Constants.AUDIO.ENEMY2ATTACK);
        for (int i = 0; i < arrows.Length; i++)
        {            
            arrows[i] = Instantiate(arrow, shootingPoint2.position, transform.rotation);
        }

        arrows[0].transform.eulerAngles = new Vector3(0, 0, -60);
        arrows[1].transform.eulerAngles = new Vector3(0, 0, -75);
        arrows[2].transform.eulerAngles = new Vector3(0, 0, -89);

        arrows[0].transform.DOJump(new Vector3(Player.Instance.transform.position.x - 2, 4.5f, 0), 5, 1, 1).SetEase(Ease.Linear);
        arrows[1].transform.DOJump(new Vector3(Player.Instance.transform.position.x, 4.5f, 0), 5, 1, 1).SetEase(Ease.Linear);
        arrows[2].transform.DOJump(new Vector3(Player.Instance.transform.position.x + 2, 4.5f, 0), 5, 1, 1).SetEase(Ease.Linear);

        arrows[0].transform.DORotate(new Vector3(0, 0, 90), 1).OnComplete(() =>
        {
            arrows[0].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(arrows[0], 2);
        });
        arrows[1].transform.DORotate(new Vector3(0, 0, 90), 1).OnComplete(() =>
        {
            arrows[1].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(arrows[1], 2);
        });
        arrows[2].transform.DORotate(new Vector3(0, 0, 90), 1).OnComplete(() =>
        {
            arrows[2].GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Destroy(arrows[2], 2);
        });

        yield return new WaitForSeconds(2);
        StartCoroutine(Stun());
    }

    private IEnumerator Stun()
    {
        SetAnim(stunAnimation, true);
        effStun.SetActive(true);
        yield return new WaitForSeconds(3);
        effStun.SetActive(false);
        StartCoroutine(Shoot(1));
    }
    public override void Death()
    {
        MasterAudio.PlaySound(Constants.AUDIO.EXPLOSIONBOSS);
        ContentMgr.Instance.GetItem(Constants.POOLING.FXBOSSDEATH, fxTranform.position, Quaternion.identity);
        checkStartBoss.ShowEffectEnd();
        base.Death();
    }
}

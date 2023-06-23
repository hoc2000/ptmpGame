using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enemy.Base;
using DG.Tweening;
using Spine.Unity;

public class EnemySummonWither : Enemy1
{
    [Header("SETTING")]
    [SerializeField] GameObject effSpawn;
    bool isStart;
    protected override void Start()
    {
        base.Start();
    }
    bool initComplete;
    public void Init()
    {
        skeletonAnimation = GetComponent<SkeletonAnimation>();
        skeletonMecanim = GetComponent<SkeletonMecanim>();
        //ragdoll = GetComponent<SkeletonRagdoll2D>();
        if (skeletonAnimation != null)
        {
            animationState = skeletonAnimation.AnimationState;
            skeleton = skeletonAnimation.skeleton;
        }
        else if (skeletonMecanim != null)
        {
            skeleton = skeletonMecanim.skeleton;
        }

        //transform.localPosition = new Vector3(0f, -4f, 0f);
        initComplete = false;
        IsDeath = false;
        effSpawn.transform.position = new Vector3(transform.position.x,effSpawn.transform.position.y, effSpawn.transform.position.z);
        effSpawn.SetActive(true);
        Apear();
    }
    public override void Update()
    {
        if (!initComplete)
        {
            return;
        }
        base.Update();
    }
    public override void FixedUpdate()
    {
        if(!initComplete)
        {
            return;
        }
        base.FixedUpdate();
    }

   void Apear()
    {
        SetAnim(moveAnimation, true, 1);
        transform.DOLocalMoveY(0f, 1f).SetEase(Ease.Linear).OnComplete(() => {
            if (isStart)
            {
                //base.Start();
                enemyInform.currentHp = enemyInform.maxHp;
            }
            //base.Start();
            isStart = true;
            initComplete = true;
            effSpawn.SetActive(false);
        });
    }
}

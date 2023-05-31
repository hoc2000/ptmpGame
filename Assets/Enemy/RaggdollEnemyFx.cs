using System.Collections;
using System.Collections.Generic;
using Spine;
using UnityEngine;
using Spine.Unity;
using Spine.Unity.Examples;
public class RaggdollEnemyFx : MonoBehaviour
{
    public Spine.Unity.Examples.SkeletonRagdoll2D ragdoll;
    public SkeletonAnimation skeletonAnim;
    public Skeleton skeleton;
    public Vector2 launchVelocity = new Vector2(50, 100);
    public float dir;

    private void Start()
    {
        ragdoll = GetComponent<Spine.Unity.Examples.SkeletonRagdoll2D>();
        skeletonAnim = GetComponent<SkeletonAnimation>();
        skeleton = skeletonAnim.skeleton;
        Init();
    }
    public void Init()
    {
        
        ragdoll.Apply();
        if (skeletonAnim.initialFlipX)
        {
            dir = 1;
        }
        else {
            dir = -1;
        }

        ragdoll.RootRigidbody.velocity = new Vector2(dir * launchVelocity.x, launchVelocity.y);
        StartCoroutine(Disappear());
    }

    private IEnumerator Disappear()
    {
        yield return new WaitForSeconds(1.5f);
        float time = 0;
        float a = 1;
        while (time <= 0.5f)
        {
            time += 0.05f;
            a -= 0.1f;
            skeleton.SetColor(new Color(1, 1, 1, a));
            yield return new WaitForSeconds(0.1f);
        }
        //gameObject.SetActive(false);
        //ContentMgr.Instance.Despaw(gameObject);
    }
}

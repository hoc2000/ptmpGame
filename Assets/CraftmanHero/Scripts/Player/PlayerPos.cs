using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using Com.LuisPedroFonseca.ProCamera2D;

public class PlayerPos : MonoBehaviour
{
    public static PlayerPos instance;

    [SerializeField] private Transform chestPoint;
    [SerializeField] private float chestRange;
    [SerializeField] private LayerMask chestLayer;

    [SerializeField] GameObject doorWin;

    [SerializeField] private Rigidbody2D rb;
    public SkeletonAnimation anim;
    public Transform grabDetect;
    public Transform weaponHolder;
    public bool completeLevel;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public  void AttackChest()
    {
        //Collider2D[] chestOpen  = Physics2D.OverlapCircleAll(chestPoint.position, chestRange, chestLayer);
        //if (chestOpen != null)
        //{
        //    ProCamera2DShake.Instance.Shake(0);
        //    foreach (Collider2D chest in chestOpen)
        //    {
        //        Debug.Log("We hit + " + chest.name);
        //        if (chest != null && chest.GetComponent<Chest>() != null)
        //        {
        //            chest.GetComponent<Chest>().ChestOpen();
        //        }
        //    }
        //}      
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
       
        if (collision.tag == Constants.TAG.CHECK_POINT)
        {
            collision.GetComponentInChildren<Animator>().enabled = true;
            if (!Gamedata.checkPoint)
            {
            }
            Gamedata.checkPoint = true;
            Gamedata.posCheckPoint = collision.gameObject.transform.position;
            Gamedata.posRevive = collision.transform.position;
        }
        if(collision.tag == Constants.TAG.DEADWATER)
        {
            Player.Instance.Death();
        }
        if (collision.tag == Constants.TAG.WEAPON)
        {
            collision.gameObject.transform.position = weaponHolder.position;

        }
        if(collision.gameObject.layer == Constants.LAYER.DEADWATER)
        {
            Player.Instance.Death();

        }
    }
    public void SetStartPlayer()
    {
        Gamedata.checkPoint = false;
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.None;
        GetComponent<Animator>().enabled = false;
    }

    IEnumerator MoveNextLevel()
    {
        bool stop = false;

        while (!stop)
        {
            yield return new WaitForSeconds(2f);
            if (Mathf.Abs(transform.position.x - doorWin.transform.position.x) < 0.5f)
            {
                stop = true;
                yield return null;
            }
        }

    }
    public void SetAnim(string name, bool loop)
    {
        //if (name == anim.AnimationName)
        //    return;
        anim.AnimationState.SetAnimation(0, name, loop);
    }
}

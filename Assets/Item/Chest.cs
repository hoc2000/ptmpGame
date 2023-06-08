using DarkTonic.MasterAudio;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    [SpineAnimation]
    public string openAnimation;


    private bool isOpen;

    [SerializeField] GameObject fx;
    [SerializeField] GameObject posSpawn;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            ChestOpen();
        }
    }
    private void ChestOpen()
    {
        if (!isOpen)
        {
            GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, openAnimation, false);
            MasterAudio.PlaySound(Constants.AUDIO.OPENCHEST);           
            isOpen = true;
            GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(MakeCoinExist());
        }
    }

    IEnumerator MakeCoinExist()
    {
        yield return new WaitForSeconds(0.85f);
        MasterAudio.PlaySound(Constants.AUDIO.OPENCHEST);
        for (int i = 0; i < 10; i++)
        {
            var coin = ContentMgr.Instance.GetItem("Coin", posSpawn.transform.position, Quaternion.identity);
            coin.GetComponent<Rigidbody2D>().velocity += (new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(0.95f, 1.05f)) * 10);
            yield return new WaitForSeconds(0.02f);
        }
        fx.SetActive(false);
    }

    //void ShowCoinFx()
    //{
    //    for (int i = 0; i < 10; i++)
    //    {
    //        var coin = ContentMgr.Instance.GetItem("Coin", transform.position, Quaternion.identity);

    //        coin.GetComponent<Rigidbody2D>().velocity += (new Vector2(Random.Range(-0.3f, 0.3f), Random.Range(0.95f, 1.05f)) * 10);
    //        //yield return new WaitForSeconds(0.02f);
    //    }
    //}    
}

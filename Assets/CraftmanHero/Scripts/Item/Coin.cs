using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject parentObj;
    [SerializeField] Collider2D coll;

    private void OnEnable()
    {
        coll.enabled = false;
        Invoke("AtiveCollider", 0.5f);
    }
    void AtiveCollider()
    {
        coll.enabled = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER))
        {
            MasterAudio.PlaySound(Constants.AUDIO.COINCOLLECT);
            int coinCollect = Random.Range(Gamedata.I.CurrentLevel, Gamedata.I.CurrentLevel+4);
            //Gamedata.I.Coin += GameConfig.Instance.valueConfrig.coinValue;
            var coin = ContentMgr.Instance.GetItem("TextCoin", collision.transform.position, Quaternion.identity);
            coin.GetComponent<TextCoin>().Init(coinCollect);
            GameManager.coinCollect += coinCollect;
            ContentMgr.Instance.Despaw(parentObj);
            
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DarkTonic.MasterAudio;
public class CheckPoint : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject fireCheck;
    bool isCheck;
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER) && !isCheck)
        {
            fireCheck.SetActive(true);
            LevelManager.Instance.checkPoin = transform;
            isCheck = true;
            MasterAudio.PlaySound(Constants.AUDIO.CHECKPOINT);
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool;
using UnityEngine;

public class StarUIFly : MonoBehaviour
{
    //public Transform target;


    //private void OnEnable()
    //{
    //    Vector3 pos1 = new Vector3(
    //            UnityEngine.Random.Range(transform.position.x, transform.position.x + 5F),
    //            UnityEngine.Random.Range(transform.position.y - 5f, transform.position.y - 10f),
    //            transform.position.z
    //     );

    //    transform.DOMove(pos1, 0.2f).OnComplete(() =>
    //    {
    //        transform.DOMove(target.position, 0.5f).OnComplete(() =>
    //        {
    //            //VibrationManager.Instance.VibrateSelection();
    //            //AudioManager.Instance.ShotPitch("collectStar", GameManager.Instance.sfxPitch);
    //            //GameManager.Instance.sfxPitch += 0.1f;
    //            //homePanel.countStarCollect++;
    //            //homePanel.currentStar += homePanel.starCollect / 10;
    //            //homePanel.starTxt.text = homePanel.currentStar.ToString();
    //            //if (homePanel.countStarCollect>=10)
    //            //{
    //            //    this.PostEvent(EventID.UpdateData);
    //            //}
    //            gameObject.SetActive(false);
    //        }); ;
    //    });

    //    //Invoke("ActiovePanel",2.8f);
    //}
    Transform target1;
    public void Fly(Transform target)
    {
        Vector3 pos1 = new Vector3(
                UnityEngine.Random.Range(transform.position.x, transform.position.x + 100f),
                UnityEngine.Random.Range(transform.position.y - 50f, transform.position.y - 100f),
                transform.position.z
         );

        transform.DOMove(pos1, 0.3f).OnComplete(() =>
        {
            target1 = target;
            Invoke("FlyToEnd", 1.5f);
        });
    }
    void FlyToEnd()
    {
        transform.DOMove(target1.position, 0.5f).OnComplete(() =>
        {
            //VibrationManager.Instance.VibrateSelection();
            AudioManager.Instance.Shot("collectCoin");//, AudioManager.Instance.pitch);
            AudioManager.Instance.pitch += 0.1f;
            gameObject.SetActive(false);
        });
    }
    void ActiovePanel()
    {
        //GameManager.Instance.sfxPitch = 1f;
        // homePanel.starEfxPanel.SetActive(false);
        //this.PostEvent(EventID.UpdateData);
    }
}

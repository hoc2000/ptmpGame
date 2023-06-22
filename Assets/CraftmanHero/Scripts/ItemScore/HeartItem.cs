using System.Collections;
using System.Collections.Generic;
using DarkTonic.MasterAudio;
using UnityEngine;

public class HeartItem : MonoBehaviour
{
    public bool isAds;
    [SerializeField] GameObject adsIcon;
    bool activeObject;
    public bool canAds;
    [SerializeField]
    private ShowRewardedController showRewardedController;
    public GameObject notVideo;

    private void Start()
    {
        adsIcon.SetActive(isAds);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Constants.TAG.PLAYER && !activeObject)
        {
            if (!canAds)
                Healing();
            else
            {
                showRewardedController.Show();
            }
        }
    }

    void Healing()
    {
        Gamedata.getHealItem = true;
        MasterAudio.PlaySound(Constants.AUDIO.HEALING);
        Player.Instance.UpdateHealth();
        gameObject.SetActive(false);
    }

    public void OnRewardedNotAvailable()
    {
        ShowFail();
    }
    private void ShowFail()
    {
        notVideo.SetActive(true);
        ResetActiveObject();
        notVideo.transform.GetChild(0).GetComponent<Animator>().Play("TextFx");
    }
    void ResetActiveObject()
    {
        Helper.StartAction(() => activeObject = false, 0.5f);
    }

    public void OnRewardedClosed(bool success)
    {
        if (success)
        {
            Time.timeScale = 1;
            Healing();
        }
        else
        {
            ResetActiveObject();
        }
    }

    public void OnRewardedStart()
    {
        activeObject = true;
    }
}

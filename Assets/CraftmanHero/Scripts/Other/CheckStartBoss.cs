using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Com.LuisPedroFonseca.ProCamera2D;
public class CheckStartBoss : MonoBehaviour
{
    [SerializeField] GameObject wall;
    [SerializeField] MonoBehaviour boss;

    bool isStartBoss;

    float transformY;

    private void Start()
    {
        transformY = wall.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Constants.TAG.PLAYER) && !isStartBoss)
        {
            isStartBoss = true;
            ShowEffStat();
        }
    }
    void ShowEffStat()
    {
        //boss.enabled = true;
        //Pvar constantShakePreset = ProCamera2DShake.Instance.ConstantShakePresets[Random.Range(0, ProCamera2DShake.Instance.ConstantShakePresets.Count)];
        //Debug.Log("ConstantShake: " + constantShakePreset.name);

        ProCamera2DShake.Instance.ConstantShake(0);
        wall.SetActive(true);
        wall.transform.DOMoveY(0, 2f).SetEase(Ease.OutSine).OnComplete(() => {
            ProCamera2DShake.Instance.StopConstantShaking();
            
            boss.enabled = true;
            boss.gameObject.GetComponent<Collider2D>().enabled = true;
        });
    }

    public void ShowEffectEnd()
    {
        //boss2Warden.enabled = true;
        ProCamera2DShake.Instance.ConstantShake(0);
        wall.transform.DOMoveY(transformY, 2f).SetEase(Ease.OutSine).OnComplete(() => {

            ProCamera2DShake.Instance.StopConstantShaking();
        });
    }
}

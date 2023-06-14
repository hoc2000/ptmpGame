using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using My.Tool.UI;
using DG.Tweening;
using My.Tool;
public class ColectFxPopup : BaseUIMenu
{
    [SerializeField] CoinUiFx[] coinImage;
    [SerializeField] Transform tartget;
    [SerializeField] Transform left, right, top, bot;
    private void OnEnable()
    {
        Init();
    }

    void Init()
    {
        StartCoroutine(ShowFx());
    }
    IEnumerator ShowFx()
    {
        for (int i =0; i<coinImage.Length; i++)
        {
            coinImage[i].transform.localScale = Vector3.zero;
            Vector3 posStart = new Vector3(Random.Range(left.position.x, right.position.x), Random.Range(bot.position.y, top.position.y), coinImage[i].transform.position.z) ;
            coinImage[i].transform.position = posStart;
            coinImage[i].gameObject.SetActive(true);
            coinImage[i].transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.OutBack);
            yield return new WaitForSeconds(0.02f);
        }
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(MoveCoin());
    }

    IEnumerator MoveCoin()
    {
        for (int i = 0; i < coinImage.Length; i++)
        {
            coinImage[i].Init(tartget.position);
            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(0.5f);
        this.PostEvent(EventID.UpdateData);
        //gameObject.SetActive(false);
        this.Pop();
    }
}

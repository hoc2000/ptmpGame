using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class TextCoin : MonoBehaviour
{
    [SerializeField] TextMeshPro dameText;
    public void Init(int dame)
    {
        dameText.text ="+" + dame.ToString();

        Vector2 poinEnd = new Vector2(transform.position.x, transform.position.y+1.5f);
        transform.DOMove(poinEnd, 0.5f).SetEase(Ease.Linear).OnComplete(() => {

            ContentMgr.Instance.Despaw(gameObject);
        });
    }
}

using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinUiFx : MonoBehaviour
{
    public void Init(Vector3 targer)
    {
        transform.DOMove(targer, 0.3f).SetEase(Ease.Linear).OnComplete(() => {

            gameObject.SetActive(false);
        });
    }
}

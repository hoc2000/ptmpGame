using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEditor;

public class TextDame : MonoBehaviour
{
    [SerializeField] TextMeshPro dameText;
    private void Start()
    {
        gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Fx";
    }
    public void Init(int dame)
    {
        dameText.text = dame.ToString();

        Vector2 poinEnd = new Vector2(Random.Range(transform.position.x-1f, transform.position.x + 1f),transform.position.y);
        transform.DOJump(poinEnd, 2, 1, 0.7f).SetEase(Ease.Linear).OnComplete(() => {

            ContentMgr.Instance.Despaw(gameObject);
        });
    }
}

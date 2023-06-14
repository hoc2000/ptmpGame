using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpikeMove : MonoBehaviour
{
    [SerializeField] Transform tartget;
    [SerializeField] Transform pointEnd;
    Vector3 pointStart;
    [SerializeField] float timeMove;
    [SerializeField] float timeDealy;
    void Start()
    {
        pointStart = tartget.position;

        StartCoroutine(AutoMove());
    }
    IEnumerator AutoMove()
    {
        tartget.DOMoveY(pointEnd.position.y, timeMove).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(timeMove + timeDealy);
        tartget.DOMoveY(pointStart.y, timeMove).SetEase(Ease.InOutBack);
        yield return new WaitForSeconds(timeMove + timeDealy);

        StartCoroutine(AutoMove());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Constants.TAG.PLAYER))
        {
            Debug.Log("dealth");
                Player player = collision.gameObject.GetComponent<Player>();
                player.Death();
        }
        
    }
}

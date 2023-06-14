using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using DarkTonic.MasterAudio;

public class ButtonClickEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {

        transform.DOScale(1.05f, 0.1f).SetEase(Ease.Linear);
        MasterAudio.PlaySound(Constants.AUDIO.TAP);

        //VibrationManager.Instance.VibrateSelection();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(1, 0.1f).SetEase(Ease.Linear);
    }
}

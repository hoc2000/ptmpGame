using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using My.Tool;
using UnityEngine;
using UnityEngine.UI;

public class CommingSoonPopup : BaseUIMenu
{
    [Header("BUTTON")]
    [SerializeField] Button closeButton;
    [SerializeField] Button sellectLevelButton;
    [SerializeField] GameObject popup;
    private void OnEnable()
    {
        popup.transform.localScale = Vector3.zero;
        popup.transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBack);
    }
    void Start()
    {
        AddEventButton();
    }
    void AddEventButton()
    {
        closeButton.onClick.AddListener(() => CloseClick());
        sellectLevelButton.onClick.AddListener(() => SellectLevelClick());
    }
    public void CloseClick()
    {

        this.Pop();
    }
    void SellectLevelClick()
    {
        CanvasManager.Push(GlobalInfo.SellectLevelPopup, null);
    }
}

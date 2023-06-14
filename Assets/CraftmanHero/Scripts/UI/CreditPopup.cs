using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using My.Tool.UI;
using UnityEngine;
using UnityEngine.UI;

public class CreditPopup : BaseUIMenu
{
    [SerializeField] Button closeButton;
    [SerializeField] Button facebookButton;
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
        facebookButton.onClick.AddListener(() => FbClick());
    }
    public void CloseClick()
    {
        this.Pop();
    }
    void FbClick()
    {

    }
}

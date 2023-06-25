using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NPCPopup : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI talkText;
    [SerializeField] Button tabButton;
    [SerializeField] NPCManager nPCManager;
    private void OnEnable()
    {
        SetText();
    }
    void Start()
    {
        tabButton.onClick.AddListener(() => SetText());
    }

    void SetText()
    {
        talkText.text = nPCManager.GetTalkValue();
    }
}

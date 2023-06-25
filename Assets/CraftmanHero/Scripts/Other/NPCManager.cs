using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    [SerializeField] GameObject npcPopup;
    [SerializeField] string[] talkString;
    int idTalk;
    void Start()
    {

    }

    public string GetTalkValue()
    {
        if (idTalk < talkString.Length)
        {
            idTalk++;
            return talkString[idTalk - 1];
        }
        else
        {
            npcPopup.SetActive(false);
            return null;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        npcPopup.SetActive(true);
    }

    // Hắn đã bắt tất cả người dân, cháu gái của ta cũng đã bi hắn bắt mất...
    // Xin cậu hãy giúp ta giải cứu mọi người...
    // Ta sẽ cho cậu mọi thứ ta có
}

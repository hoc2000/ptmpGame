using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ElementLevel : MonoBehaviour
{

    [Header("TEXT")]
    [SerializeField] TextMeshProUGUI txtLevel;
    [SerializeField] public int level;


    [SerializeField] private Image iconLevel;
    [SerializeField] private GameObject hardLevel;

    [Header("SPRITE")]
    [SerializeField] Sprite selectedLevel;
    [SerializeField] Sprite unlockLevel;
    [SerializeField] Sprite notunlockLevel;
    private void OnEnable()
    {
        Init();
    }
    void Init()
    {
 
        txtLevel.text = level.ToString();
        if(level > Gamedata.I.CurrentLevel)
        {  
            iconLevel.sprite = notunlockLevel;
            
        }
        else if(level <= Gamedata.I.CurrentLevel)
        {
            if (level < Gamedata.I.CurrentLevel && Gamedata.LevelPassed)
            {
                iconLevel.sprite = unlockLevel;
            }
            else
            {

                iconLevel.sprite = selectedLevel;
            }
        }
        if (Gamedata.IsLevelHard(level))
        {
            hardLevel.SetActive(true);
        }
        else
        {
            hardLevel.SetActive(false);

        }
    }


    public void LoadLevel()
    {
        GameAnalytics.LogButtonClick("select_level", "HomeScene");
        GameManager.levelSelected = level;
        GameManager.startFrom = "select_level";
        if (level > Constants.MAX_LEVEL)
        {           
            return;
        }
        else LoadSceceManager.Instance.LoadLevel();
    }
}

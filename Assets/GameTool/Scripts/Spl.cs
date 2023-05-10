using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Spl : MonoBehaviour
{
    [SerializeField] GameObject logoStudio;
    [SerializeField] GameObject logoGame;
    [SerializeField] Button tabToPlayButton;


    void Start()
    {
        tabToPlayButton.onClick.AddListener(() => TabToPlayClick());
    }

    void TabToPlayClick()
    {
        SceneManager.LoadScene(1);
    }

    public void EndAnimLogo()
    {
        logoStudio.SetActive(false);
        logoGame.SetActive(true);
    }
}

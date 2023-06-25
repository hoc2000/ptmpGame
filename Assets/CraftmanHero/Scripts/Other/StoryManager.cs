using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryManager : MonoBehaviour
{
    [SerializeField] GameObject[] story;
    int valueShow;
    private void OnEnable()
    {
        if (valueShow == 0)
        {
            valueShow++;
            EndStory1();
        }
        else
        {
            EndStory2();
        }
    }
    void EndStory1()
    {
        story[0].SetActive(false);
        story[1].SetActive(true);
    }
    void EndStory2()
    {
        SceneManager.LoadScene(2);
    }
}

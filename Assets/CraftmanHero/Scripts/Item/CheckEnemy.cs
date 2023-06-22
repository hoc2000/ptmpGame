using My.Tool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CheckEnemy : MonoBehaviour
{
    [SerializeField] TextMeshPro numberEnemy;


    private void OnEnable()
    {
        this.RegisterListener(EventID.EnemyDeath, (sender, param) => UpdateEemiesCoutText());
    }
    private void OnDisable()
    {
        this.RemoveListener(EventID.EnemyDeath, (sender, param) => UpdateEemiesCoutText());
    }
    private void Start()
    {
        UpdateEemiesCoutText();
    }
    void UpdateEemiesCoutText()
    {
        numberEnemy.text = LevelManager.Instance.enemiesDie + "/" + LevelManager.Instance.allEnemyInMap;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSetup : MonoBehaviour
{
    [Header("SETTING")]
    public Transform posStartPlayer;
    public static LevelSetup instance;
    public Transform posMin, posMax;
    public bool levelBoss;
    public int scoreComplete;

    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    private void Start()
    {
        Transform camera = Camera.main.transform;

    }
}

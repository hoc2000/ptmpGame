using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePlayerData : MonoBehaviour
{

    void Start()
    {

    }
    public void UpgradeData()
    {
        Gamedata.I.AtkPlayer += AtkData();
        Gamedata.I.HpPlayer += HpData();

        Gamedata.I.LevelUpgrade++;
    }
    public int CoinUpGrade()
    {
        return 50 + (35 * (Gamedata.I.LevelUpgrade - 1)) * ((int)Mathf.Floor((Gamedata.I.LevelUpgrade - 1) / 5) + 1);
    }
    public int AtkData()
    {
        return Gamedata.I.AtkPlayer * 10 / 100;
    }
    public int HpData()
    {
        return Gamedata.I.HpPlayer * 10 / 100;
    }
}
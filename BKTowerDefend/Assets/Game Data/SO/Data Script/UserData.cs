﻿using UnityEngine;
using System.Collections.Generic;

public class UserData : MonoBehaviour
{
    public int coin;
    public int gold;
    public int reachableLv;

    public string[] listTurretIds;
    public string[] listSkillIds;

    public void Awake()
    {
        listTurretIds = new string[3];
        listSkillIds = new string[3];
    }

    public void LoadData()
    {
        coin = PlayerPrefs.GetInt("Coin", 0);
        gold = PlayerPrefs.GetInt("Gold", 0);
        reachableLv = PlayerPrefs.GetInt("ReachableLv", 10);
    }

    public void ChangeCoin(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coin", coin);
    }

    public void ChangeGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("Gold", gold);
    }

    public void UnLockNewLevel()
    {
        reachableLv++;
        PlayerPrefs.SetInt("ReachableLv", reachableLv);
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("Coin", 0);
        PlayerPrefs.SetInt("Gold", 0);
        PlayerPrefs.SetInt("ReachableLv", 10);
    }
}

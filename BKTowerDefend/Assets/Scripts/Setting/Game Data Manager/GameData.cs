using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public static GameData instance;

    public int maxStagesReached;

    public List<TurretData> turretDataList;


    public GameData(int _maxStagesReached, List<TurretData> _turretDataList)
    {
        maxStagesReached = _maxStagesReached;
        turretDataList = _turretDataList;
    }
}

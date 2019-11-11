﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Class", menuName = "Turret/Turret Class")]
public class TurretClass : ScriptableObject
{
    public const int defaultMaxlv = 2;

    public string className;

    public Sprite avatar;

    public List<TurretBlueprint> turretList;

    public int reachableLv;

    #region SetupLv
    int maxLv
    {
        get
        {
            return turretList.Count;
        }
    }

    public void UnlockNewTurret()
    {
        reachableLv = defaultMaxlv;
    }

    public void ReachNewLv()
    {
        reachableLv++;
        reachableLv = Mathf.Clamp(reachableLv, defaultMaxlv, maxLv);
    }
    #endregion
}
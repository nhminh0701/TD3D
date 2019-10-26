﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Class", menuName = "Turret/Turret Class")]
public class TurretClass : ScriptableObject
{
    public string className;

    public List<TurretBlueprint> turretList;
}

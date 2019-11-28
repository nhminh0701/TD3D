using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffHolder Data", menuName = "Data/Data Asset/DebuffHolder Data")]
public class DebuffHolderData : ItemData
{
    [Header("DBH Pars.")]
    public List<TurretStyle> targetStyle;
}

[Serializable]
public class Debuff
{
    public DebuffType debuffType;
    public DebuffParams debuffParams;
}

public enum DebuffType
{
    SLow,
    DPS,
    Stun,
    Pierce,
    DamageRed
}

[Serializable]
public class DebuffParams
{
    public float duration;
    public float damagePerSeconds;
    [Range(0, 1)]
    public float msSlowPer;
    [Range(0, 1)]
    public float damageRedPer;
}

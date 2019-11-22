using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffHolder Asset", menuName = "Data/Data Asset/DebuffHolder Asset")]
public class DebuffHolderAsset : ScriptableObject
{
    public string debuffHolderID;
    [TextArea(2, 5)]
    public string description;
    public List<TurretStyle> targetStyle;
    //[SerializeField] int defaultTurretNumber;
    [HideInInspector]
    public int unLockDecode;

    public void UnLockThisDeBuff()
    {
        unLockDecode = 1;
        PlayerPrefs.SetInt(debuffHolderID, unLockDecode);
    }
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

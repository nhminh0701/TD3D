using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebuffHolder Data", menuName = "Data/Data Asset/DebuffHolder Data")]
public class DebuffHolderData : ScriptableObject
{
    public string itemName;
    [TextArea(2, 5)]
    public string description;
    public List<TurretStyle> targetStyle;
    //[SerializeField] int defaultTurretNumber;
    [HideInInspector]
    public int unlockStatusCode;
    public int appShopPurchasePrice;
    public const PurchaseType purchaseType = PurchaseType.Gold;

    public void UnLockThisDeBuff()
    {
        unlockStatusCode = 1;
        PlayerPrefs.SetInt(itemName, unlockStatusCode);
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

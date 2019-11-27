using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data", menuName = "Data/Data Asset/Turret Data")]
public class TurretData : ScriptableObject
{
    public string itemName;
    // Set on PlayerPrefs
    public int unlockStatusCode;
    public TurretStyle turretStyle;
    [TextArea(2,5)]
    public string description;
    public int appShopPurchasePrice;
    public const PurchaseType purchaseType = PurchaseType.Gold;
    public List<TurretDataPerLV> listTurretLV;

    public string equipedDebuffId;

    public void UnlockThisTurret()
    {
        unlockStatusCode = 4;
        PlayerPrefs.SetInt(itemName, unlockStatusCode);
    }

    public void LvUpThisTurret()
    {
        unlockStatusCode++;
        PlayerPrefs.SetInt(itemName, unlockStatusCode);
    }
}

[Serializable]
public class TurretDataPerLV
{
    public AttackParams attackParams;
    public float range;
    public float health;
    
    public int inGamePurchasePrice;
    public int sellPrice;
}

[Serializable]
public class AttackParams
{
    public float damage;
    public float fireRate;
}

public enum TurretStyle
{
    BulletAttack,
    LaserAttack,
}


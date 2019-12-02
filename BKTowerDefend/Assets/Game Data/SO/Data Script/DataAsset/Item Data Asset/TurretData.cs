using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Data", menuName = "Data/Data Asset/Turret Data")]
public class TurretData : ItemData
{
    [Header("Turret Pars.")]
    public TurretStyle turretStyle;
    
    public List<TurretDataPerLV> listTurretLV;

    public string equipedDebuffId;

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

/// <summary>
/// Hold parameters needed for turret attacking, i.e damage, damageRange, fireRate
/// </summary>
[Serializable]
public class AttackParams
{
    public float damage;
    public float damageRange;
    public float fireRate;
}

public enum TurretStyle
{
    BulletAttack,
    LaserAttack,
}


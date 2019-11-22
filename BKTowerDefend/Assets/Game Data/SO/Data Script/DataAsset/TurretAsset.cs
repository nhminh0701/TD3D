using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Asset", menuName = "Data/Data Asset/Turret Asset")]
public class TurretAsset : ScriptableObject
{
    public string turretID;
    // Set on PlayerPrefs
    public int reachableLv;
    public TurretStyle turretStyle;
    [TextArea(2,5)]
    public string description;
    public int appShopPurchasePrice;
    public List<TurretDataPerLV> listTurretLV;

    public DebuffHolderAsset equipedDebuff;

    public void UnlockThisTurret()
    {
        reachableLv = 3;
        PlayerPrefs.SetInt(turretID + "LV", reachableLv);
    }

    public void LvUpThisTurret()
    {
        reachableLv++;
        PlayerPrefs.SetInt(turretID + "LV", reachableLv);
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


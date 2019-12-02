using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holding necessary resource for turret as well as acting as their factories
/// </summary>
[CreateAssetMenu(fileName = "Turret Resources", menuName = "Data/Resource Data Asset/Resource Turret Asset")]
public class TurretResourceAsset : ItemResource
{
    #region Rework
    [Header("Game Object")]
    public GameObject prefab;
    public GameObject buildEffect;
    [Header("Material Region")]
    public Material mainMaterial;
    public DesignPerLevel[] listDesignPerLevel;


    /// <summary>
    /// Return a gameObject Turret with Design based on Lv and turretData
    /// </summary>
    /// <param name="turretData"></param>
    /// <param name="turretLv"></param>
    /// <param name="position"></param>
    /// <returns></returns>
    public GameObject CreateTurret(TurretData turretData, int turretLv, Vector3 position)
    {
        // Design new turret
        GameObject turretToReturn = SimplePool.Spawn(prefab, position, Quaternion.identity);
        turretToReturn.transform.localScale = Vector3.one * listDesignPerLevel[turretLv - 1].scaleSize;
        mainMaterial.color = listDesignPerLevel[turretLv - 1].materialColor;
        mainMaterial.SetColor("_EmissionColor", listDesignPerLevel[turretLv - 1].materialColor*5);


        // Set Pars
        turretToReturn.GetComponent<Turret>().turretAttackStyle = turretData.turretStyle;

        TurretDataPerLV parsThisLv = turretData.listTurretLV[turretLv - 1];
        TurretAttack turretAttack = turretToReturn.GetComponent<TurretAttack>();
        if (turretAttack != null)
        {
            turretAttack.InitiatePars(parsThisLv.attackParams, turretData.equipedDebuffId);
        }

        TurretMovement turretMovement = turretToReturn.GetComponent<TurretMovement>();
        if (turretMovement != null) turretMovement.range = parsThisLv.range;

        GameObject buildVFX = Instantiate(buildEffect, position, Quaternion.identity);
        Destroy(buildVFX, 2);

        return turretToReturn;
    }

    #endregion
}

[System.Serializable]
public class DesignPerLevel
{
    public float scaleSize;
    public Color materialColor;
}

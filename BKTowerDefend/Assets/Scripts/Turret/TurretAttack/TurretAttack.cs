using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Abstract class for implementing any kind of attack behaviour of turrets
/// </summary>
[RequireComponent(typeof(TurretMovement))]
public abstract class TurretAttack : MonoBehaviour
{
    protected DataGlobal dataGlobal;

    protected Enemy targetEnemy;
    public float damage;
    [SerializeField] protected Transform firePoint;
    protected AttackParams attackParams;

    #region DBH region
    // Holding effect pars
    protected DebuffHolderData debufHolderData;
    // Holding effect design
    protected DBHResourceAsset dBHResourceAsset;
    #endregion

    private void Start()
    {
        dataGlobal = DataGlobal.instance;
    }

    public void InitiatePars(AttackParams _attackParams, string dBHName = null)
    {
        attackParams = _attackParams;

        if (string.IsNullOrEmpty(dBHName)) return;
        debufHolderData = dataGlobal.dataAsset.GetDebuffHolderData(dBHName);
        dBHResourceAsset = dataGlobal.resourceDataAsset.GetDBHResourceAsset(dBHName);
    }

    public void SetTargetEnemy(Enemy _targetEnemy)
    {
        targetEnemy = _targetEnemy; 
    }

    public abstract void AttackEnemy();

    protected virtual void Update()
    {
        AttackEnemy();
    }
}

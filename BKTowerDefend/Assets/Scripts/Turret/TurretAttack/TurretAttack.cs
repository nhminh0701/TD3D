using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TurretMovement))]
public abstract class TurretAttack : MonoBehaviour
{
    protected Enemy targetEnemy;
    public float damage;
    [SerializeField] protected Transform firePoint;
    public AttackParams attackParams;

    public virtual void InitiateTurret()
    {
        SetParameters();
    }

    public void SetTargetEnemy(Enemy _targetEnemy)
    {
        targetEnemy = _targetEnemy; 
    }

    public virtual void SetParameters()
    {
        damage = attackParams.damage;
    }

    public virtual void AttackEnemy()
    {
        
    }

    protected virtual void Update()
    {
        AttackEnemy();
    }
}

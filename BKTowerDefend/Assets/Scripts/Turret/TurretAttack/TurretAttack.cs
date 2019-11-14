using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(TurretMovement))]
public abstract class TurretAttack : MonoBehaviour
{
    protected Enemy targetEnemy;
    [SerializeField] protected Transform firePoint;

    public void SetTargetEnemy(Enemy _targetEnemy)
    {
        targetEnemy = _targetEnemy; 
    }

    public virtual void AttackEnemy()
    {
        
    }

    protected virtual void Update()
    {
        AttackEnemy();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attack by acting as a bullet/missle factories
/// </summary>
public class TurretAttackShooting : TurretAttack
{
    public GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    // Fire Cycle = 1/fireRate
    float fireCountdown = 0f;

    public override void AttackEnemy()
    {
        if (fireCountdown <= 0f)
        {
            if (targetEnemy == null) return;

            Shoot();
            fireCountdown = 1f / attackParams.fireRate; // Therefore we dont need else for the next line
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        //GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletGO = (GameObject)SimplePool.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetBulletPars(attackParams.damage, attackParams.damageRange, debufHolderData, dBHResourceAsset);
        bullet.Seek(targetEnemy.transform);
    }
}

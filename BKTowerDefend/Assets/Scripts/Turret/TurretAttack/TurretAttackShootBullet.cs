using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackShootBullet : TurretAttack
{
    public GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    // Fire Cycle = 1/fireRate
    float fireCountdown = 0f;

    public override void InitiateTurret()
    {
        base.InitiateTurret();
        bulletPrefab.GetComponent<Bullet>().SetBulletDamage(damage);
    }

    public override void AttackEnemy()
    {
        base.AttackEnemy();

        if (fireCountdown <= 0f)
        {
            if (targetEnemy == null) return;

            Shoot();
            fireCountdown = 1f / fireRate; // Therefore we dont need else for the next line
        }

        fireCountdown -= Time.deltaTime;
    }

    private void Shoot()
    {
        //GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        GameObject bulletGO = (GameObject)SimplePool.Spawn(bulletPrefab, firePoint.position, Quaternion.identity);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetBulletDamage(damage);
        bullet.Seek(targetEnemy.transform);
    }

    public override void SetParameters()
    {
        base.SetParameters();
        fireRate = attackParams.fireRate;
    }
}

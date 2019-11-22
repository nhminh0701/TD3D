using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttackLaser : TurretAttack
{
    [Range(0, 1)] public float slowPct = .5f;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] Light impactLight;

    public override void AttackEnemy()
    {
        if (targetEnemy == null)
        {
            TurnOffLaser();
            return;
        }

        if (!lineRenderer.enabled)
        {
            TurnOnLaser();
        }

        targetEnemy.health.TakeDamage(damage * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, targetEnemy.transform.position);

        Vector3 dir = firePoint.position - targetEnemy.transform.position;

        impactEffect.transform.position = targetEnemy.transform.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);

    }

    private void TurnOffLaser()
    {
        lineRenderer.enabled = false;
        impactEffect.Stop();
        impactLight.enabled = false;
    }

    void TurnOnLaser()
    {
        lineRenderer.enabled = true;
        impactEffect.Play();
        impactLight.enabled = true;
    }
}

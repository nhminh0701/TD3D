using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] readonly float speed = 70f;
    [SerializeField] ExplosionVFX explosionVFX;
    public GameObject impactEffect;
    float damageRange;
    float damage = 20;
    Transform target;

    #region Bullet Skillart
    Sprite skillAvatar;
    DebuffHolderData debuffHolderData;
    // Effect Updated factory
    DBHResourceAsset dBHResourceAsset;
    #endregion

    public void SetBulletPars(float _damage = 20, float exploR = 0, DebuffHolderData turretArtData = null, DBHResourceAsset _dBHResourceAsset = null)
    {
        damage = _damage;
        damageRange = exploR;
        debuffHolderData = turretArtData;
        dBHResourceAsset = _dBHResourceAsset;
    }

    public void Seek(Transform _target)
    {
        target = _target;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        // Check if the bullet hit the "target" Enemy before translate the bullet
        if(dir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    private void HitTarget()
    {
        if (explosionVFX != null)
        {
            GameObject exploreVFX = (GameObject)SimplePool.Spawn(explosionVFX.gameObject, transform.position, Quaternion.identity);
            exploreVFX.GetComponent<ExplosionVFX>().ActivateVFX(damageRange);
        }

        GameObject effIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effIns, 5f);

        if (damageRange > 0f)
        {
            Explore();
        } else
        {
            Damage(target);
        }

        //Destroy(gameObject);
        SimplePool.Despawn(gameObject);
    }

    void Damage(Transform enemy)
    {
        IHealth e = enemy.GetComponentInParent<IHealth>();
        Enemy enemyComponent = enemy.GetComponent<Enemy>();
        if (e == null) return;
        e.TakeDamage(damage);
        if (debuffHolderData != null) enemyComponent.TriggerEffect(debuffHolderData);
    }

    void Explore()
    {

        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        foreach(Collider collider in colliders)
        {
            Damage(collider.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, damageRange);
    }
}

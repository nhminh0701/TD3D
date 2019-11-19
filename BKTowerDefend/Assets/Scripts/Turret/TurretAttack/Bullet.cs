using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 70f;
    public GameObject impactEffect;
    [SerializeField] float explosionRadius;
    [SerializeField] int damage = 20;
    Transform target;

    EffectAsigner effectAsigner;

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
        GameObject effIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effIns, 5f);

        if (explosionRadius > 0f)
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

        if (e != null) { e.TakeDamage(damage); }

        
    }

    void Explore()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach(Collider collider in colliders)
        {
                Damage(collider.transform);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}

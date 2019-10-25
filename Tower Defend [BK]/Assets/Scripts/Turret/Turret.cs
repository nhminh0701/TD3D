using System;
using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Tooltip("The one to be shooted (Debug Purpose)")]
    [SerializeField] Transform target;  // At the same time, every turret is supposed to have only one target
    Enemy targetEnemy;

    #region Shooting Par
    [Header("General")]
    [Tooltip("Range of Turret")]
    [SerializeField] float range = 15f;
    [Header("Use Bullets (default)")]
    public GameObject bulletPrefab;
    [SerializeField] float fireRate = 1f;
    // Fire Cycle = 1/fireRate
    float fireCountdown = 0f;

    [Header("Use Laser")]
    [SerializeField] bool useLaser = false;
    [Range(0, 1)] public float slowPct = .5f;
    public int damageOverTime = 1;
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem impactEffect;
    [SerializeField] Light impactLight;
    #endregion

    #region Attributes
    [Header("Unity Attributes")]
    [Tooltip("Enemy Mark")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] float turnSpeed = 4;
    [SerializeField] Transform partToRotate;
    
    [SerializeField] Transform firePoint;
    #endregion



    // Start is called before the first frame update
    void Start()
    {
        // Find Closest target, try not to use the method every frame because searching algo cost lost of resource
        // do the method 2 time a second, start from second 0th
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
    }

    void UpdateTarget()
    {
        // Get all existed enemies and look for the nearest one in range, store it to the target var
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach(GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance >= distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
            targetEnemy = target.GetComponent<Enemy>();
        }
        else target = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (useLaser)
            {
                if (lineRenderer.enabled)
                {
                    lineRenderer.enabled = false;
                    impactEffect.Stop();
                    impactLight.enabled = false;
                }
            }
            return;
        }

        LookOnTarget();
        AttackEnemy();
    }

    private void AttackEnemy()
    {
        if (useLaser)
        {
            Laser();
        }
        else
        {
            if (fireCountdown <= 0f)
            {
                Shoot();
                fireCountdown = 1f / fireRate; // Therefore we dont need else for the next line
            }

            fireCountdown -= Time.deltaTime;
        }
    }

    private void Laser()
    {
        targetEnemy.TakeDamage(damageOverTime * Time.deltaTime);
        targetEnemy.Slow(slowPct);

        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
            impactEffect.Play();
            impactLight.enabled = true;
        }

        lineRenderer.SetPosition(0, firePoint.position);
        lineRenderer.SetPosition(1, target.position);

        Vector3 dir = firePoint.position - target.position;

        impactEffect.transform.position = target.position + dir.normalized;

        impactEffect.transform.rotation = Quaternion.LookRotation(dir);
    }

    private void LookOnTarget()
    {
        // Target look on
        Vector3 dir = target.position - transform.position;
        // Create a quaternion represent the rotation
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        // Read the rotation represented from Quaternion to get the 
        // rotate angle for euler generation
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    private void Shoot()
    {
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();

        bullet.Seek(target);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

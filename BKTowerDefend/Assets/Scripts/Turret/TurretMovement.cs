using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Turret))]
public class TurretMovement : MonoBehaviour
{
    [Tooltip("The one to be shooted (Debug Purpose)")]
    public Transform target;  // At the same time, every turret is supposed to have only one target

    [Tooltip("Range of Turret")]
    [SerializeField] float range = 15f;

    [Header("Unity Attributes")]
    [Tooltip("Enemy Mark")]
    [SerializeField] string enemyTag = "Enemy";
    [SerializeField] float turnSpeed = 4;
    [SerializeField] Transform partToRotate;
    TurretAttack turretAttack;

    void Start()
    {
        // Find Closest target, try not to use the method every frame because searching algo cost lost of resource
        // do the method 2 time a second, start from second 0th
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        turretAttack = GetComponent<TurretAttack>();
    }

    void UpdateTarget()
    {
        // Get all existed enemies and look for the nearest one in range, store it to the target var
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        float shortestDistance = Mathf.Infinity;
        Enemy nearestEnemy = null;

        foreach (Enemy enemy in enemies)
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
            turretAttack.SetTargetEnemy(target.GetComponent<Enemy>());
        }
        else
        {
            target = null;
            turretAttack.SetTargetEnemy(null);
        }


    }

    private void Update()
    {
        if (target == null) return;

        LookOnTarget();
        //if (turretAttack != null)
        //{
        //    turretAttack.AttackEnemy();
        //}
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

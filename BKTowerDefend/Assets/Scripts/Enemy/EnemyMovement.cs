using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    Transform target;
    int wayPointIndex = 0;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
        target = Waypoints.wayPoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * enemy.speed * Time.deltaTime);

        if (dir.magnitude < 0.3f) {
            GetNextWayPoint();
        }

        // Reset Speed so after slow effect the speed remained
        enemy.speed = enemy.startSpeed;
    }
    #region Movement 
    private void GetNextWayPoint()
    {
        if (wayPointIndex >= Waypoints.wayPoints.Length - 1)
        {
            EndPath();
        }
        else
        {
            wayPointIndex++;
            target = Waypoints.wayPoints[wayPointIndex];
        }
    }

    private void EndPath()
    {
        PlayerStats.LoseLives(1);
        Destroy(gameObject);
    }
    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Enemy))]
public class EnemyMovement : MonoBehaviour
{
    [HideInInspector]
    public Transform[] movePath;
    Transform target;
    int wayPointIndex = 0;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<Enemy>();
    }


    // Update is called once per frame
    void Update()
    {
        // Rotation

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
        if (wayPointIndex >= movePath.Length - 1)
        {
            EndPath();
        }
        else
        {
            wayPointIndex++;
            target = movePath[wayPointIndex];
        }
    }

    private void EndPath()
    {
        WayPointEffector wpEffector = target.GetComponentInChildren<WayPointEffector>();
        if (wpEffector)
        {
            wpEffector.ObjectStateChange();
        }

        PlayerStats.LoseLives(1);
        //Destroy(gameObject);
        SimplePool.Despawn(gameObject);
    }

    // Use with 
    public void GetNewWave(Transform[] _movePath)
    {
        wayPointIndex = 1;
        movePath = _movePath;
        target = _movePath[wayPointIndex];
    }
    #endregion
}

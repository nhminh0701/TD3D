using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 12f;

    [HideInInspector] public float speed;

    public int worth = 5;

    [SerializeField] float startHealth = 100;
    public Health health;

    private void Start()
    {
        health.SetmaxHealth(startHealth);
        health.enabled = true;
        speed = startSpeed;
    }

    private void OnDestroy()
    {
        WaveSpawner.numberAliveEnemies--;
    }

    private void OnDisable()
    {
        PlayerStats.EarnMoney(worth);
        WaveSpawner.numberAliveEnemies--;
        EnemyMovement enemyMovement = gameObject.GetComponent<EnemyMovement>();
        if (enemyMovement != null)
        {
            enemyMovement.ResetPath();
        }
    }

    

    public void Slow(float percentage)
    {
        speed = startSpeed * (1 - percentage);
    }

}

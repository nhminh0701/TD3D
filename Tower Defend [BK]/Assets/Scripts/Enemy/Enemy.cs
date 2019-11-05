using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 12f;

    [HideInInspector] public float speed;

    public int worth = 5;
    [SerializeField] GameObject deathEffectPrefab;

    //[SerializeField] float startHealth = 100;
    [SerializeField] Health health;

    private void Start()
    {
        //health.SetmaxHealth(startHealth);
        speed = startSpeed;
    }

    private void OnDestroy()
    {
        WaveSpawner.numberAliveEnemies--;
    }

    #region Health
    public void TakeDamage(float damageAmount)
    {
        health.ChangeHealthValue(-damageAmount);

        if (health.isDeath ) { Die(); }
    }

    private void Die()
    {
        GameObject destroyEffect = (GameObject)Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
        Destroy(destroyEffect, 4f);

        PlayerStats.EarnMoney(worth);
        Destroy(gameObject);
    }

    public void Slow(float percentage)
    {
        speed = startSpeed * (1 - percentage);
    }
    #endregion
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 12f;

    [HideInInspector] public float speed;
    public float health = 150;
    public int worth = 5;
    [SerializeField] GameObject deathEffectPrefab;
    [SerializeField] HealthUI healthUI;

    private void Start()
    {
        speed = startSpeed;
        if (healthUI!=null) { healthUI.AssignMaxBarVal(health); }
    }

    private void OnDestroy()
    {
        WaveSpawner.numberAliveEnemies--;
    }

    #region Health
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        if (healthUI != null) { healthUI.ChangeBarVal(-damageAmount); }

        if (health <= 0 ) { Die(); }
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

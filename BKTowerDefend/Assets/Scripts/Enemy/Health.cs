using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealth
{
    [SerializeField] GameObject deathEffectPrefab;
    [SerializeField] GameObject healthUIPrefab;
    HealthUI thisHealthUI;

    float maxHealth;
    float currentHealth;

    public bool isDeath;

    public void SetmaxHealth(float value)
    {
        maxHealth = value;
    }

    private void Awake()
    {
        this.enabled = false;
    }

    private void OnEnable()
    {
        GameObject healthUIObject = Instantiate(healthUIPrefab, transform);
        thisHealthUI = healthUIObject.GetComponentInChildren<HealthUI>();
        thisHealthUI.AssignMaxBarVal(maxHealth);
        currentHealth = maxHealth;
    }

    //public void ChangeHealthValue(float value)
    //{
    //    currentHealth += value;
    //    thisHealthUI.ChangeBarVal(value);

    //    if (currentHealth <= 0) isDeath = true;
    //}

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        thisHealthUI.ChangeBarVal(-damage);

        if (currentHealth <= 0) Die();
    }

    public void Die()
    {
        if (deathEffectPrefab != null)
        {
            GameObject destroyEffect = (GameObject)Instantiate(deathEffectPrefab, transform.position, Quaternion.identity);
            Destroy(destroyEffect, 4f);
        }
        
        // Destroy(gameObject);
        SimplePool.Despawn(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] GameObject healthUIPrefab;
    HealthUI thisHealthUI;

    [SerializeField] float maxHealth;
    float currentHealth;

    public bool isDeath;

    //public void SetmaxHealth(float value)
    //{
    //    maxHealth = value;
    //}

    private void OnEnable()
    {
        isDeath = false;
        GameObject healthUIObject = Instantiate(healthUIPrefab, transform);
        thisHealthUI = healthUIObject.GetComponentInChildren<HealthUI>();
        thisHealthUI.AssignMaxBarVal(maxHealth);
        currentHealth = maxHealth;
    }

    public void ChangeHealthValue(float value)
    {
        currentHealth += value;
        thisHealthUI.ChangeBarVal(value);

        if (currentHealth <= 0) isDeath = true;
    }
}

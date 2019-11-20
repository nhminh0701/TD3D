﻿using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] Text livesText;
    public static LivesUI instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateLiveText();
        EventManager.changePlayerHP += OnHPChange;
    }

    private void OnDestroy()
    {
        EventManager.changePlayerHP -= OnHPChange;
    }

    void OnHPChange(int changeAmount)
    {
        UpdateLiveText(changeAmount);
    }


    void UpdateLiveText(int changeAmount = 0)
    {
        int newHp = PlayerStats.lives + changeAmount;
        if (newHp < 0) newHp = 0;
        livesText.text = newHp.ToString();
    }
}

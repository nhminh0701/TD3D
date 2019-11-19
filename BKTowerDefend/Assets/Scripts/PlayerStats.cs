using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    #region Events
    public delegate void OnEventEnter();
    public static event OnEventEnter OnGetDamaged;
    public static event OnEventEnter OnEnemyKilled;
    public static event OnEventEnter OnEarnLives;

    public static event OnEventEnter OnSpendMoney;
    public static event OnEventEnter OnEarnMoney;
    #endregion

    // Money across scene
    public static int money { get; private set; }
    public int startMoney = 50;

    public static int lives;
    public int startLives = 20;

    public static int rounds;

    private void Awake()
    {
        money = startMoney;
        lives = startLives;

        rounds = 0;
    }

    public static void SpendMoney(int spendAmount)
    {
        money -= spendAmount;

        if (money < 0) money = 0;

        OnSpendMoney();
    }

    public static void EarnMoney(int earnAmount)
    {
        money += earnAmount;
        OnEarnMoney();
    }

    public static void LoseLives(int loseAmount)
    {
        lives -= loseAmount;

        if (lives < 0) lives = 0;

        OnGetDamaged();
    }

    public static void IncreaseLives(int loseAmount)
    {
        lives += loseAmount;
        OnEarnMoney();
    }


}

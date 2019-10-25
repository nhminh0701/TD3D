using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    // Money across scene
    public static int money { get; private set; }
    public int startMoney = 50;

    public static int lives;
    public int startLives = 20;

    public static int rounds;

    private void Start()
    {
        money = startMoney;
        lives = startLives;

        rounds = 0;
    }

    public static void SpendMoney(int spendAmount)
    {
        money -= spendAmount;
    }

    public static void EarnMoney(int earnAmount)
    {
        money += earnAmount;
    }

    public static void LoseLives(int loseAmount)
    {
        lives -= loseAmount;
    }

    public static void IncreaseLives(int loseAmount)
    {
        lives += loseAmount;
    }


}

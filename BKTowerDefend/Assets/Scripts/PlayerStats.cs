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

    private void Awake()
    {
        money = startMoney;
        lives = startLives;

        rounds = 0;
    }

    private void Start()
    {
        EventManager.OnChangePlayerHP += ChangeHP;
        EventManager.OnChangePlayerInStageMoney += ChangeInStageMoney;
    }

    private void OnDestroy()
    {
        EventManager.OnChangePlayerHP -= ChangeHP;
        EventManager.OnChangePlayerInStageMoney -= ChangeInStageMoney;
    }

    void ChangeInStageMoney(int changeAmount)
    {
        money += changeAmount;

        if (money < 0) money = 0;
    }

    void ChangeHP(int changeAmount)
    {
        lives += changeAmount;
        if (lives < 0) lives = 0;

        GameManager.instance.CheckGameOver();
    }
}

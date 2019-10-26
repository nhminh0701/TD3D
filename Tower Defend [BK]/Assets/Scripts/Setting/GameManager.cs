using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsEnded = false;
    public static GameManager instance;
    [SerializeField] GameObject gameOverUI;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        // Reset status OnLoad so DontDestroyOnLoad may not be a good idea
        gameOverUI.SetActive(false);
        gameIsEnded = false;
    }

    private void Update()
    {
        if (gameIsEnded) { return; }

        if (PlayerStats.lives < 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsEnded = true;
        gameOverUI.SetActive(true);
    }
}

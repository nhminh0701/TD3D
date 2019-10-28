using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public Text roundsText;
    [SerializeField] SceneFader sceneFader;
    public string menuSceneName = "Main Menu";

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        // GameManager.gameIsEnded = false;
        // FindObjectOfType<CameraController>().enabled = true;
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuSceneName);
    }
}

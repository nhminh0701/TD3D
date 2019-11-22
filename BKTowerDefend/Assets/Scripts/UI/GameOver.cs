using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    //public Text roundsText;
    public TextMeshProUGUI roundsText;
    SceneFader sceneFader;
    public string menuSceneName = "Main Menu";

    private void OnEnable()
    {
        roundsText.text = PlayerStats.rounds.ToString();
        sceneFader = FindObjectOfType<SceneFader>();
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

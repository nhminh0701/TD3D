using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] SceneFader sceneFader;
    public string menuSceneName = "Main Menu";

    public void Toggle()
    {
        pauseUI.SetActive(!pauseUI.activeSelf);

        Time.timeScale = pauseUI.activeSelf ? 0 : 1;
    }

    public void ReLoad()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Toggle();
    }

    public void ReturnHome()
    {
        sceneFader.FadeTo(menuSceneName);
        Toggle();
    }
}

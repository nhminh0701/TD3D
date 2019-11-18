using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUI : MonoBehaviour
{
    [SerializeField] GameObject pauseUI;
    [SerializeField] SceneFader sceneFader;
    public string menuSceneName = "Main Menu";
    bool isActive;

    private void Start()
    {
        pauseUI.transform.localScale = Vector3.zero;
        isActive = false;
        sceneFader = FindObjectOfType<SceneFader>();
    }

    public void Toggle()
    {
        int scaleUI = isActive ? 0 : 1;

        Time.timeScale = 1;
        pauseUI.transform.DOScale(scaleUI, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
            isActive = !isActive;
            Time.timeScale = isActive ? 0 : 1;
        });
    }

    public void ReLoad()
    {
        int scaleUI = isActive ? 0 : 1;

        Time.timeScale = 1;
        pauseUI.transform.DOScale(scaleUI, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
            isActive = !isActive;
            Time.timeScale = isActive ? 0 : 1;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        
    }

    public void ReturnHome()
    {        
        int scaleUI = isActive ? 0 : 1;

        Time.timeScale = 1;
        pauseUI.transform.DOScale(scaleUI, 0.5f).SetEase(Ease.OutBack).OnComplete(() => {
            isActive = !isActive;
            Time.timeScale = isActive ? 0 : 1;
            sceneFader.FadeTo(menuSceneName);
        });
    }
}

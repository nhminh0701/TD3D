using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsEnded = false;
    public static GameManager instance;
    public int stageLevel;
    [SerializeField] GameObject gameOverUI;
    [SerializeField] GameObject stageCompleteCanvas;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        // Reset status OnLoad so DontDestroyOnLoad may not be a good idea
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
        gameOverUI.transform.DOMoveY(Screen.height / 2, 1);
    }

    public void ClearStage()
    {
        int limitLv = DataManager.instance.gameData.currentState.reachableLv;
        // stageCompleteCanvas.SetActive(true);
        stageCompleteCanvas.transform.DOMoveY(Screen.height/2, 1);
        GameManager.gameIsEnded = true;
        if (stageLevel == limitLv)
            DataManager.instance.gameData.currentState.reachableLv = stageLevel+ 1;
    }
}

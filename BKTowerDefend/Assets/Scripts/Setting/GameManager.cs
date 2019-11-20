using DG.Tweening;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool gameIsEnded = false;
    public static GameManager instance;
    public int stageLevel;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        //DontDestroyOnLoad(gameObject);

        // Reset status OnLoad so DontDestroyOnLoad may not be a good idea
        gameIsEnded = false;
    }


    public void CheckGameOver()
    {
        if (PlayerStats.lives <= 0)
        {
            EndGame();
        }
    }

    void EndGame()
    {
        gameIsEnded = true;
        UIManager.instance.SummonWindow(UIManager.instance.gameOverUI.transform);
    }

    public void ClearStage()
    {
        int limitLv = DataManager.instance.gameData.currentState.reachableLv;
        // stageCompleteCanvas.SetActive(true);
        UIManager.instance.SummonWindow(UIManager.instance.stageClearUI.transform);
        GameManager.gameIsEnded = true;
        if (stageLevel == limitLv)
            DataManager.instance.gameData.currentState.reachableLv = stageLevel+ 1;
    }
}

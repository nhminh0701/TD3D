using DG.Tweening;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameOver gameOverUI { get; private set; }
    public StageClearUI stageClearUI { get; private set; }

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        gameOverUI = FindObjectOfType<GameOver>();
        stageClearUI = FindObjectOfType<StageClearUI>();
    }

    public void SummonWindow(Transform windowTransform)
    {
        windowTransform.DOMoveY(Screen.height / 2, 1);
    }
}

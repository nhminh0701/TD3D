using UnityEngine.UI;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] Text livesText;
    public static LivesUI instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateLiveText();
        PlayerStats.OnEarnLives += UpdateLiveText;
        PlayerStats.OnGetDamaged += UpdateLiveText;
    }

    void UpdateLiveText()
    {
        livesText.text = PlayerStats.lives.ToString();
    }

    private void OnDestroy()
    {
        PlayerStats.OnEarnLives -= UpdateLiveText;
        PlayerStats.OnGetDamaged -= UpdateLiveText;
    }
}

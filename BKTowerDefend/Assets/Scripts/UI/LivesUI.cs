using TMPro;
using UnityEngine;

public class LivesUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI livesText;
    public static LivesUI instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        UpdateLiveText();
        EventManager.OnChangePlayerHP += OnHPChange;
    }

    private void OnDestroy()
    {
        EventManager.OnChangePlayerHP -= OnHPChange;
    }

    void OnHPChange(int changeAmount)
    {
        UpdateLiveText(changeAmount);
    }


    void UpdateLiveText(int changeAmount = 0)
    {
        int newHp = PlayerStats.lives + changeAmount;
        if (newHp < 0) newHp = 0;
        livesText.text = newHp.ToString();
    }
}

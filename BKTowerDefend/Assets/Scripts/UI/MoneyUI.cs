using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI moneyText;

    private void Start()
    {
        UpdateMoneyUI();

        EventManager.changePlayerInStageMoney += OnInStageMoneyChange;
    }

    // Update is called once per frame

    void OnInStageMoneyChange(int changeAmount) {
        UpdateMoneyUI(changeAmount);
    }

    public void UpdateMoneyUI(int changeAmount = 0)
    {
        int newCurrent = PlayerStats.money + changeAmount;
        if (newCurrent < 0) newCurrent = 0;

        moneyText.text = newCurrent.ToString();
    }

    private void OnDestroy()
    {
        EventManager.changePlayerInStageMoney -= OnInStageMoneyChange;
    }
}

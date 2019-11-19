using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text moneyText;

    private void Start()
    {
        UpdateMoneyUI();
        PlayerStats.OnSpendMoney += UpdateMoneyUI;
        PlayerStats.OnEarnMoney += UpdateMoneyUI;
    }

    // Update is called once per frame
    public void UpdateMoneyUI()
    {
        moneyText.text = PlayerStats.money.ToString();
    }

    private void OnDestroy()
    {
        PlayerStats.OnSpendMoney -= UpdateMoneyUI;
        PlayerStats.OnEarnMoney -= UpdateMoneyUI;
    }
}

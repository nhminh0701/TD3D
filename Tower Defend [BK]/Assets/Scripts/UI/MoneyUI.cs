using UnityEngine.UI;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] Text moneyText;

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "$" + PlayerStats.money;
    }
}

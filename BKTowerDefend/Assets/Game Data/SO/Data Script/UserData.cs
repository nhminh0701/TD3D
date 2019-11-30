using UnityEngine;

public class UserData : MonoBehaviour
{
    public int coin;
    public int gold;
    public int reachableLv;

    [Header("Equipment")]
    public string[] listTurretIds;
    public string[] listSkillIds;

    public delegate void OnCurrencyChange(int changeAmount);
    public static event OnCurrencyChange OnCoinChangeEvent;
    public static event OnCurrencyChange OnGoldChangeEvent;

    void CoiChangeEvent(int changeAmount)
    {
        if (OnCoinChangeEvent != null) OnCoinChangeEvent(changeAmount);
    }

    void GoldChangeEvent(int changeAmount)
    {
        if (OnGoldChangeEvent != null) OnGoldChangeEvent(changeAmount);
    }

    public void Awake()
    {
        listTurretIds = new string[3];
        listSkillIds = new string[3];
    }

    public void LoadData()
    {
        coin = PlayerPrefs.GetInt("Coin", 1000);
        gold = PlayerPrefs.GetInt("Gold", 100);
        reachableLv = PlayerPrefs.GetInt("ReachableLv", 10);
    }

    public void ChangeCoin(int amount)
    {
        coin += amount;
        PlayerPrefs.SetInt("Coin", coin);
        CoiChangeEvent(amount);
    }

    public void ChangeGold(int amount)
    {
        gold += amount;
        PlayerPrefs.SetInt("Gold", gold);
        GoldChangeEvent(amount);
    }

    public void UnLockNewLevel()
    {
        reachableLv++;
        PlayerPrefs.SetInt("ReachableLv", reachableLv);
    }

    public void ResetData()
    {
        PlayerPrefs.SetInt("Coin", 1000);
        PlayerPrefs.SetInt("Gold", 500);
        PlayerPrefs.SetInt("ReachableLv", 10);
    }

    public bool GetRequiredEquipment()
    {
        int numberAvailableTurret = DataGlobal.instance.dataAsset.GetAvailableTurrets().Count;
        return listTurretIds.Length <= numberAvailableTurret;
    }
}

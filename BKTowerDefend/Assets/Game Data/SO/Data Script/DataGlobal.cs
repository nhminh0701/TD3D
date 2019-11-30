using UnityEngine.SceneManagement;
using UnityEngine;

public class DataGlobal : MonoBehaviour
{
    public static DataGlobal instance;

    public ResourceDataAsset resourceDataAsset;
    public DataAsset dataAsset;
    public UserData userData;

    private void Awake()
    {
        LoadData();
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    void LoadData()
    {
        dataAsset.LoadData();
        userData.LoadData();
    }

    [ContextMenu("Reset")]
    public void ResetData()
    {
        dataAsset.ResetData();
        userData.ResetData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        LoadData();
    }

    public bool IsEnoughItem()
    {
        int numberOfTurret = dataAsset.GetAvailableTurrets().Count;
        int noEquipedTurret = 0;
        for (var i = 0; i < userData.listTurretIds.Length;i++)
        {
            if (!string.IsNullOrWhiteSpace(userData.listTurretIds[i])) noEquipedTurret++;
        }

        if (numberOfTurret <= userData.listTurretIds.Length) { return noEquipedTurret == numberOfTurret; }
        else return noEquipedTurret == userData.listTurretIds.Length;
    }

    public void PurchaseItem(ItemData item)
    {
        item.UnlockThisItem();

        switch (item.purchaseType)
        {
            case PurchaseType.Coin:
                userData.ChangeCoin(-item.appShopPurchasePrice);
                break;
            case PurchaseType.Gold:
                userData.ChangeGold(-item.appShopPurchasePrice);
                break;
        }
    }
}

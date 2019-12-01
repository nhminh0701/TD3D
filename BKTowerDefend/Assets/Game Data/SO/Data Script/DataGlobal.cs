using UnityEngine.SceneManagement;
using UnityEngine;

/// <summary>
/// Managing Game DataAsset, resource and userdata, i.e loading, reseting and conditioning with associated managed data
/// </summary>
public class DataGlobal : MonoBehaviour
{
    public static DataGlobal instance;

    public ResourceDataAsset resourceDataAsset;
    public DataAsset dataAsset;
    public UserData userData;
    public string currentStageName;

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

    /// <summary>
    /// Condition to be able to select stage to play, dataGlobal conduct this comparison since it maanges both dataAsset and userData
    /// </summary>
    /// <returns></returns>
    public bool IsEnoughItem()
    {
        int numberOfTurret = dataAsset.GetAvailableTurrets().Count;
        int noEquipedTurret = 0;
        for (var i = 0; i < userData.listTurretIds.Length;i++)
        {
            if (!string.IsNullOrWhiteSpace(userData.listTurretIds[i])) noEquipedTurret++;
        }

        if (numberOfTurret == 0) return false;

        if (numberOfTurret <= userData.listTurretIds.Length) {
            return noEquipedTurret == numberOfTurret; }
        else
        {
            Debug.Log(numberOfTurret);
            return noEquipedTurret == userData.listTurretIds.Length;
        }
    }

    /// <summary>
    /// Change user Money currency based on item PurchaseType
    /// </summary>
    /// <param name="item"></param>
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

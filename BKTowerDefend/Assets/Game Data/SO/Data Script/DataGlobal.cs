using System;
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

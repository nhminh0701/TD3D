using UnityEngine;

public abstract class ItemData : ScriptableObject
{
    [Header("General Pars.")]
    public string itemName;
    // Set on PlayerPrefs
    public int unlockStatusCode;
    [TextArea(2, 5)]
    public string description;
    public int appShopPurchasePrice;
    public PurchaseType purchaseType;

    public void UnlockThisItem()
    {
        unlockStatusCode = 4;
        PlayerPrefs.SetInt(itemName, unlockStatusCode);
    }
}

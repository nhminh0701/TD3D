using UnityEngine;

public class ItemData : ScriptableObject
{
    public string itemName;
    // Set on PlayerPrefs
    public int unlockStatusCode;
    public TurretStyle turretStyle;
    [TextArea(2, 5)]
    public string description;
    public int appShopPurchasePrice;
    public const PurchaseType purchaseType = PurchaseType.Gold;

    public void UnlockThisItem()
    {
        unlockStatusCode = 4;
        PlayerPrefs.SetInt(itemName, unlockStatusCode);
    }
}

using UnityEngine;

[CreateAssetMenu(fileName = "Player Skill Data", menuName = "Data/Data Asset/Player Skill Data")]
public class PlayerSkillData : ScriptableObject
{
    public string itemName;
    [TextArea(2, 10)]
    public string description;
    public int unlockStatusCode;
    public int appShopPurchasePrice;

    public const PurchaseType purchaseType = PurchaseType.Coin;
}

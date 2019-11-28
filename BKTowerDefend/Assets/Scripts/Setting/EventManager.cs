
public class EventManager
{
    #region InGame Event OnPlayerStat Change
    public delegate void GameEventInt(int par);
    public static event GameEventInt OnChangePlayerHP;

    public static void ChangePlayerHP(int changeAmount)
    {
        if (OnChangePlayerHP != null)
        {
            OnChangePlayerHP(changeAmount);
        }
    }


    public static event GameEventInt OnChangePlayerInStageMoney;

    public static void ChangePlayerInStageMoney(int changeAmount)
    {
        if (OnChangePlayerInStageMoney != null)
        {
            OnChangePlayerInStageMoney(changeAmount);
        }
    }
    #endregion

    #region Event OnChangingSelectedItem
    public delegate void OnSelectItemEvent(string itemId);

    public static void AddItem(ItemData item)
    {
        string itemName = item.itemName;
        
        switch (item.GetType().ToString())
        {
            case "TurretData":
                AddTurretItem(itemName);
                break;
            case "DebuffHolderData":
                AddDBHItem(itemName);
                break;
            case "PlayerSkillData":
                AddSkillItem(itemName);
                break;
        }
    }
    #region Change Turret Selection
    public static event OnSelectItemEvent OnAddingTurretItem;

    public static void AddTurretItem(string itemName)
    {
        if (OnAddingTurretItem != null)
        {
            OnAddingTurretItem(itemName);
        }
    }

    public static event OnSelectItemEvent OnPurchaseTurret;

    public static void PurchaseTurretEvent(string itemName)
    {
        if (OnPurchaseTurret != null) OnPurchaseTurret(itemName);
    }
    #endregion

    #region Change Skill Selection
    public static event OnSelectItemEvent OnAddingSkillItem;

    public static void AddSkillItem(string itemName)
    {
        if (OnAddingSkillItem != null)
        {
            OnAddingSkillItem(itemName);
        }
    }

    public static event OnSelectItemEvent OnPurchaseSkill;

    public static void PurchaseSkillEvent(string itemName)
    {
        if (OnPurchaseSkill != null) OnPurchaseSkill(itemName);
    }
    #endregion

    #region Change DBH Selection
    public static event OnSelectItemEvent OnAddingDBHItem;

    public static void AddDBHItem(string itemName)
    {
        if (OnAddingDBHItem != null)
        {
            OnAddingDBHItem(itemName);
        }
    }

    public static event OnSelectItemEvent OnPurchaseDBH;

    public static void PurchaseDBHEvent(string itemName)
    {
        if (OnPurchaseDBH != null) OnPurchaseDBH(itemName);
    }
    #endregion

    #endregion
}


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
    #region Change Turret Selection
    public static event OnSelectItemEvent OnSelectTurretItem;

    public static void AddTurretItem(string turretId)
    {
        if (OnSelectTurretItem != null)
        {
            OnSelectTurretItem(turretId);
        }
    }

    public static event OnSelectItemEvent OnPurchaseTurret;

    public static void PurchaseTurretEvent(string itemName)
    {
        if (OnPurchaseTurret != null) OnPurchaseTurret(itemName);
    }
    #endregion

    #region Change Skill Selection
    public static event OnSelectItemEvent OnSelectSkillItem;

    public static void SelectSkillItem(string skillId)
    {
        if (OnSelectSkillItem != null)
        {
            OnSelectSkillItem(skillId);
        }
    }

    public static event OnSelectItemEvent OnPurchaseSkill;

    public static void PurchaseSkillEvent(string itemName)
    {
        if (OnPurchaseSkill != null) OnPurchaseSkill(itemName);
    }
    #endregion

    #region Change DBH Selection
    public static event OnSelectItemEvent OnSelectDBHItem;

    public static void SelectDBHItem(string dBHId)
    {
        if (OnSelectDBHItem != null)
        {
            OnSelectDBHItem(dBHId);
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

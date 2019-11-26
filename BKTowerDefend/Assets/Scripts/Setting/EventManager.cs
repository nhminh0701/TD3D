
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

    public static void SelectTurretItem(string turretId)
    {
        if (OnSelectTurretItem != null)
        {
            OnSelectTurretItem(turretId);
        }
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
    #endregion

    #region Change Skill Selection
    public static event OnSelectItemEvent OnSelectDBHItem;

    public static void SelectDBHItem(string dBHId)
    {
        if (OnSelectDBHItem != null)
        {
            OnSelectDBHItem(dBHId);
        }
    }
    #endregion

    #endregion
}

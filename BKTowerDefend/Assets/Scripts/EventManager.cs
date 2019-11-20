

public class EventManager
{
    #region Events
    public delegate void OnEventEnter();
    public static event OnEventEnter onEnemyKilled;

    #endregion



    public static void OnEnemyKilled()
    {
        if (onEnemyKilled != null) onEnemyKilled();
    }


    public delegate void GameEventInt(int par);
    public static event GameEventInt changePlayerHP;

    public static void ChangePlayerHP(int changeAmount)
    {
        if (changePlayerHP != null)
        {
            changePlayerHP(changeAmount);
        }
    }


    public static event GameEventInt changePlayerInStageMoney;

    public static void ChangePlayerInStageMoney(int changeAmount)
    {
        if (changePlayerInStageMoney != null)
        {
            changePlayerInStageMoney(changeAmount);
        }
    }
}

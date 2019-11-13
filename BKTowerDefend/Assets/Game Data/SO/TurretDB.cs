using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Store all system turrets and usable turrets, i.e newly bought turret will be added to availableTurrets;
/// </summary>
[CreateAssetMenu(fileName = "TurretDB", menuName = "Game Database/Turret Database")]
public class TurretDB : ScriptableObject
{
    [SerializeField] List<TurretClass> allTurretClasses;

    /// <summary>
    /// Storing current available turret classes
    /// </summary>
    [Tooltip("Debug purpose")]
    public List<TurretClass> availableClasses;

    /// <summary>
    /// Get available turretClasses based on <paramref name="nameList"/> and store in availableClasses
    /// </summary>
    /// <param name="nameList"></param>
    /// <returns></returns>
    public void GetTurrets(List<string> nameList)
    {
        var newClassList = new List<TurretClass>();

        for (var i = 0; i < nameList.Count; i++)
        {
            for (var j = 0; j < allTurretClasses.Count; j++)
            {
                if (allTurretClasses[j].classID == nameList[i] )
                {
                    newClassList.Add(allTurretClasses[j]);
                    allTurretClasses[j].UnlockNewTurret();
                }
            }
        }

        availableClasses = newClassList;
    }
}

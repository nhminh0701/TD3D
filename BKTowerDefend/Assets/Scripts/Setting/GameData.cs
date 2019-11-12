using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Setup/Game Data")]
public class GameData : ScriptableObject
{

    public int maxStagesReached;

    /// <summary>
    /// Name of turrets that will be saved
    /// </summary>
    public List<string> turretNameList;

    /// <summary>
    /// Update turretNameList with a specific <paramref name="newTurretData"/>
    /// </summary>
    /// <param name="newTurretData"></param>
    public void UpdateTurretDataFromRT(List<TurretClass> newTurretData)
    {
        List<string> updatedList = new List<string>();

        for (var i = 0; i < newTurretData.Count; i++)
        {
            updatedList.Add(newTurretData[i].className);
            newTurretData[i].UnlockNewTurret();
        }

        turretNameList = updatedList;
    }

    /// <summary>
    /// Filter <paramref name="turretClassList"/> based on turretNameList, returning a filtered List
    /// </summary>
    /// <param name="turretClassList"></param>
    /// <returns></returns>
    public List<TurretClass> GetTurretClasses(List<TurretClass> turretClassList)
    {
        List<TurretClass> newList = new List<TurretClass>();

        for (var i = 0; i < turretClassList.Count; i++)
        {
            if (turretNameList.Contains(turretClassList[i].className))
            {
                newList.Add(turretClassList[i]);
            }
        }

        return newList;
    }

    /// <summary>
    /// Called when <paramref name="newTurretClass"/> is added to the DataSystem, the <paramref name="newTurretClass"/> will have its lv reset and its name added to the list
    /// </summary>
    /// <param name="newTurretClass"></param>
    public void GetNewTurretClass(TurretClass newTurretClass)
    {
        newTurretClass.UnlockNewTurret();
        turretNameList.Add(newTurretClass.className);
    }


}

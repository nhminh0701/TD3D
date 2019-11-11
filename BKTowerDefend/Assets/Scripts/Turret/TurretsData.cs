using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turrets Data", menuName = "Game Setup/Turrets Data")]
public class TurretsData : ScriptableObject
{
    public List<string> turretNameList;

    public void UpdateTurretDataFromRT(List<TurretClass> newTurretData)
    {
        List<string> updatedList = new List<string>();

        for (var i = 0; i < newTurretData.Count;i ++)
        {
            updatedList.Add(newTurretData[i].className);
            newTurretData[i].UnlockNewTurret();
        }

        turretNameList = updatedList;
    }

    public List<TurretClass> GetTurretClasses(List<TurretClass> turretClassList)
    {
        List<TurretClass> newList = new List<TurretClass>();

        for (var i = 0; i < turretClassList.Count; i ++)
        {
            if (turretNameList.Contains(turretClassList[i].className))
            {
                newList.Add(turretClassList[i]);
            }
        }

        return newList;
    }

    public void GetNewTurretClass(TurretClass newTurretClass)
    {
        newTurretClass.UnlockNewTurret();
        turretNameList.Add(newTurretClass.className);
    }
}

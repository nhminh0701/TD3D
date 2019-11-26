using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Data/Data Asset/Data Manager/Turret Data Manager")]
public class TurretDataManager : ScriptableObject
{
    public TurretData[] listTurretData;
    public int defaultNumberOfTurrets;

    public TurretData GetTurretById(string turretId)
    {
        TurretData turretToReturn = new TurretData();

        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (listTurretData[i].itemName == turretId)
            {
                turretToReturn = listTurretData[i];
                break;
            }
        }

        return turretToReturn;
    }

    public void LoadTurrets()
    {
        if (listTurretData.Length == 0) return;

        PreLoadTurretData();

        if (listTurretData.Length == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                listTurretData[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretData[i].itemName + "LV", 4);
            }
            else listTurretData[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretData[i].itemName + "LV", 0);
        }
    }

    public void PreLoadTurretData()
    {
        if (listTurretData.Length == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (PlayerPrefs.GetInt(listTurretData[i].itemName + "LV", 0) == 0)
            {
                if (i < defaultNumberOfTurrets)
                {
                    listTurretData[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretData[i].itemName + "LV", 4);
                }
                else listTurretData[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretData[i].itemName + "LV", 0);
            }
        }
    }

    public void ResetTurrets()
    {
        if (listTurretData.Length == 0) return;
        if (listTurretData.Length == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                PlayerPrefs.SetInt(listTurretData[i].itemName + "LV", 4);
            }
            else PlayerPrefs.SetInt(listTurretData[i].itemName + "LV", 0);
        }
    }

    public List<TurretData> GetAvailableTurrets()
    {
        List<TurretData> listToreturn = new List<TurretData>();

        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (PlayerPrefs.GetInt(listTurretData[i].itemName + "LV") != 0) listToreturn.Add(listTurretData[i]);
        }

        return listToreturn;
    }
}

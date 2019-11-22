using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Data/Data Asset/Data Asset Controller")]
public class DataAsset : ScriptableObject
{
    public void LoadData()
    {
        LoadTurrets();
        LoadDBHs();
    }

    public void ResetData()
    {
        ResetDBHs();
        ResetTurrets();
    }

    #region Turret Assets
    public List<TurretAsset> listTurretAsset;
    public int defaultNumberOfTurrets;

    void LoadTurrets()
    {
        PreLoadTurretData();

        if (listTurretAsset.Count == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                listTurretAsset[i].reachableLv = PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV", 4);
            }
            else listTurretAsset[i].reachableLv = PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV", 0);
        }
    }

    void PreLoadTurretData()
    {
        if (listTurretAsset.Count == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV", 0) == 0)
            {
                if (i < defaultNumberOfTurrets)
                {
                    listTurretAsset[i].reachableLv = PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV", 4);
                }
                else listTurretAsset[i].reachableLv = PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV", 0);
            }
        }
    }

    void ResetTurrets()
    {
        if (listTurretAsset.Count == 0) Debug.LogError("Empty List");
        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                PlayerPrefs.SetInt(listTurretAsset[i].turretID + "LV", 4);
            }
            else PlayerPrefs.SetInt(listTurretAsset[i].turretID + "LV", 0);
        }
    }

    public List<TurretAsset> GetAvailableTurrets()
    {
        List<TurretAsset> listToreturn = new List<TurretAsset>();
        Debug.Log("Start Scaning");

        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (PlayerPrefs.GetInt(listTurretAsset[i].turretID + "LV") != 0) listToreturn.Add(listTurretAsset[i]);
        }

        return listToreturn;
    }
    #endregion



    #region DBH Assets
    public List<DebuffHolderAsset> listDebuffHolderAssets;

    void LoadDBHs()
    {
        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            listDebuffHolderAssets[i].unLockDecode = PlayerPrefs.GetInt(listDebuffHolderAssets[i].debuffHolderID, 0);
        }
    }

    void ResetDBHs()
    {
        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            PlayerPrefs.SetInt(listDebuffHolderAssets[i].debuffHolderID, 0);
        }
    }
    #endregion
}

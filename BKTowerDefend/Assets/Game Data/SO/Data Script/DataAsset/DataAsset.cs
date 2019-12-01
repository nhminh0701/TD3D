using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Managing all dataAsset in the game, i.e loading, storing, reseting, accessing
/// </summary>
[CreateAssetMenu(fileName = "Data Asset", menuName = "Data/Data Asset/Data Asset Controller")]
public class DataAsset : ScriptableObject
{
    #region Game Data Manager
    #region Stage Assets
    public StageData[] listStageData;

    void LoadStages()
    {
        if (listStageData.Length == 0) return;
        listStageData[0].stageUnlockStatus = PlayerPrefs.GetInt(listStageData[0].stageName, 1);

        for (var i = 1; i < listStageData.Length; i++)
        {
            listStageData[i].stageUnlockStatus = PlayerPrefs.GetInt(listStageData[i].stageName, 0);
        }
    }

    void ResetStages()
    {
        if (listStageData.Length == 0) return;
        PlayerPrefs.SetInt(listStageData[0].stageName, 1);

        for (var i = 1; i < listStageData.Length; i++)
        {
            PlayerPrefs.SetInt(listStageData[i].stageName, 0);
        }
    }

    public void CheckOrUnlockStage(string currentStage)
    {
        StageData stageToCheck = (StageData)CreateInstance("StageData");

        for (var i = 0; i < listStageData.Length - 1; i++)
        {
            if (listStageData[i].stageName == currentStage) stageToCheck = listStageData[i + 1];
        }

        if (stageToCheck.stageUnlockStatus == 0) stageToCheck.UnLockThisStage();
    }

    public StageData GetStageData(string stageName)
    {
        for (var i = 0; i < listStageData.Length;i++)
        {
            if (listStageData[i].stageName == stageName) return listStageData[i];
        }
        return null;
    }
    #endregion

    #region Turret Assets
    public TurretData[] listTurretAsset;
    public int defaultNumberOfTurrets;

    void LoadTurrets()
    {
        if (listTurretAsset.Length == 0) return;

        for (var i = 0; i < listTurretAsset.Length; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                listTurretAsset[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretAsset[i].itemName, 4);
            }
            else listTurretAsset[i].unlockStatusCode = PlayerPrefs.GetInt(listTurretAsset[i].itemName, 0);
        }
    }

    void PreLoadTurretData()
    {
        for (var i = 0; i < listTurretAsset.Length; i++)
        {
            if (!PlayerPrefs.HasKey(listTurretAsset[i].itemName))
            {
                if (i <= defaultNumberOfTurrets)
                {
                    PlayerPrefs.SetInt(listTurretAsset[i].itemName, 4);
                }
                else PlayerPrefs.SetInt(listTurretAsset[i].itemName, 0);
            }
        }
    }

    void ResetTurrets()
    {
        if (listTurretAsset.Length == 0) return;
        for (var i = 0; i < listTurretAsset.Length; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                PlayerPrefs.SetInt(listTurretAsset[i].itemName, 4);
            }
            else PlayerPrefs.SetInt(listTurretAsset[i].itemName, 0);
        }
    }

    /// <summary>
    /// Get unlocked Turret as List of TurretData
    /// </summary>
    /// <returns></returns>
    public List<TurretData> GetAvailableTurrets()
    {
        List<TurretData> listToreturn = new List<TurretData>();

        for (var i = 0; i < listTurretAsset.Length; i++)
        {
            if (listTurretAsset[i].unlockStatusCode != 0) listToreturn.Add(listTurretAsset[i]);
        }

        return listToreturn;
    }

    /// <summary>
    /// Get TurretData by itemName
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public TurretData GetTurretData(string itemName)
    {
        TurretData turretData = (TurretData)CreateInstance("TurretData");

        for (var i = 0; i < listTurretAsset.Length; i++)
        {
            if (listTurretAsset[i].itemName == itemName)
            {
                turretData = listTurretAsset[i];
                break;
            }
        }
        return turretData;
    }
    #endregion

    #region DBH Assets
    public List<DebuffHolderData> listDebuffHolderAssets;

    void LoadDBHs()
    {
        if (listDebuffHolderAssets.Count == 0) return;

        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            listDebuffHolderAssets[i].unlockStatusCode = PlayerPrefs.GetInt(listDebuffHolderAssets[i].itemName, 0);
        }
    }

    void ResetDBHs()
    {
        if (listDebuffHolderAssets.Count == 0) return;
        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            PlayerPrefs.SetInt(listDebuffHolderAssets[i].itemName, 0);
        }
    }

    /// <summary>
    /// Get DBH data by itemName
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public DebuffHolderData GetDebuffHolderData(string itemName)
    {
        DebuffHolderData debuffHolderData = (DebuffHolderData)CreateInstance("DebuffHolderData");

        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            if (listDebuffHolderAssets[i].itemName == itemName)
            {
                debuffHolderData = listDebuffHolderAssets[i];
            }
        }
        return debuffHolderData;
    }
    #endregion

    #region PlayerSkill Assets
    public List<PlayerSkillData> listPlayerSkill;

    void LoadPlayerSKills()
    {
        if (listPlayerSkill.Count == 0) return;
        for (var i = 0; i < listPlayerSkill.Count; i++)
        {
            listPlayerSkill[i].unlockStatusCode = PlayerPrefs.GetInt(listPlayerSkill[i].itemName, 0);
        }
    }

    void ResetPlayerSkills()
    {
        if (listPlayerSkill.Count == 0) return;
        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            PlayerPrefs.SetInt(listPlayerSkill[i].itemName, 0);
        }
    }

    /// <summary>
    /// Get SKillData by itemName
    /// </summary>
    /// <param name="itemName"></param>
    /// <returns></returns>
    public PlayerSkillData GetPlayerSkillData(string itemName)
    {
        PlayerSkillData playerSkillData = (PlayerSkillData)CreateInstance("PlayerSkillData");
        for (var i = 0; i < listDebuffHolderAssets.Count; i++)
        {
            if (listPlayerSkill[i].itemName == itemName)
            {
                playerSkillData = listPlayerSkill[i];
            }
        }
        return playerSkillData;
    }
    #endregion
    #endregion



    #region public Methods
    /// <summary>
    /// Load PlayerPrefs storing data in term of unlockstatus
    /// </summary>
    public void LoadData()
    {
        LoadStages();
        LoadTurrets();
        LoadDBHs();
        LoadPlayerSKills();
    }

    /// <summary>
    /// Reset PlayerPrefs storing data in term of unlockstatus
    /// </summary>
    public void ResetData()
    {
        ResetStages();
        ResetDBHs();
        ResetTurrets();
        ResetPlayerSkills();
    }
    #endregion
}

public enum PurchaseType
{
    Coin,
    Gold,
}

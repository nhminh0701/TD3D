using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data Asset", menuName = "Data/Data Asset/Data Asset Controller")]
public class DataAsset : ScriptableObject
{
    #region Game Data Manager
    #region Stage Assets
    public List<StageData> listStageData;

    void LoadStages()
    {
        if (listStageData.Count == 0) return;
        listStageData[0].stageUnlockStatus = PlayerPrefs.GetInt(listStageData[0].stageId, 1);

        for (var i = 1; i < listStageData.Count; i++)
        {
            listStageData[i].stageUnlockStatus = PlayerPrefs.GetInt(listStageData[i].stageId, 0);
        }
    }

    void ResetStages()
    {
        if (listStageData.Count == 0) return;
        PlayerPrefs.SetInt(listStageData[0].stageId, 1);

        for (var i = 1; i < listStageData.Count; i++)
        {
            PlayerPrefs.SetInt(listStageData[i].stageId, 0);
        }
    }
    #endregion

    #region Turret Assets
    public List<TurretData> listTurretAsset;
    public int defaultNumberOfTurrets;

    void LoadTurrets()
    {
        if (listTurretAsset.Count == 0) return;

        for (var i = 0; i < listTurretAsset.Count; i++)
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
        for (var i = 0; i < listTurretAsset.Count; i++)
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
        if (listTurretAsset.Count == 0) return;
        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (i < defaultNumberOfTurrets)
            {
                PlayerPrefs.SetInt(listTurretAsset[i].itemName, 4);
            }
            else PlayerPrefs.SetInt(listTurretAsset[i].itemName, 0);
        }
    }

    public List<TurretData> GetAvailableTurrets()
    {
        List<TurretData> listToreturn = new List<TurretData>();

        for (var i = 0; i < listTurretAsset.Count; i++)
        {
            if (listTurretAsset[i].unlockStatusCode != 0) listToreturn.Add(listTurretAsset[i]);
        }

        return listToreturn;
    }

    public TurretData GetTurretData(string itemName)
    {
        TurretData turretData = (TurretData)CreateInstance("TurretData");

        for (var i = 0; i < listTurretAsset.Count; i++)
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
    public void LoadData()
    {
        LoadStages();
        LoadTurrets();
        LoadDBHs();
        LoadPlayerSKills();
    }

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

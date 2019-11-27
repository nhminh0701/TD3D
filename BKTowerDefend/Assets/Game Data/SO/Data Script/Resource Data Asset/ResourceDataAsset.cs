using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret Resources", menuName = "Data/Resource Data Asset/Resource Data Asset Controller")]
public class ResourceDataAsset : ScriptableObject
{
    #region TurretAsset
    public List<TurretResourceAsset> listTurretResourceAsset;
    public TurretResourceAsset GetTurretResourceAsset(string itemName)
    {
        //TurretResourceAsset itemToReturn = new TurretResourceAsset();
        TurretResourceAsset itemToReturn = (TurretResourceAsset)CreateInstance("TurretResourceAsset");

        for (var i = 0; i < listTurretResourceAsset.Count; i ++)
        {
            if (listTurretResourceAsset[i].itemName == itemName)
            {
                itemToReturn = listTurretResourceAsset[i];
                break;
            }
        }

        return itemToReturn;
    }
    #endregion


    #region PlayerSkillAsset
    public List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset;
    public PlayerSkillResourceAsset GetPlayerSkillResourceAsset(string itemName)
    {
        PlayerSkillResourceAsset itemToReturn = (PlayerSkillResourceAsset)CreateInstance("PlayerSkillResourceAsset");

        for (var i = 0; i < listPlayerSkillResourceAsset.Count; i++)
        {
            if (listPlayerSkillResourceAsset[i].itemName == itemName)
            {
                itemToReturn = listPlayerSkillResourceAsset[i];
                break;
            }
        }

        return itemToReturn;
    }
    #endregion

    #region DBHResourceAsset
    public List<DBHResourceAsset> listDBHResourceAssets;
    public DBHResourceAsset GetDBHResourceAsset(string itemName)
    {
        DBHResourceAsset itemToReturn = (DBHResourceAsset)CreateInstance("GetDBHResourceAsset");

        for (var i = 0; i < listDBHResourceAssets.Count; i++)
        {
            if (listDBHResourceAssets[i].itemName == itemName)
            {
                itemToReturn = listDBHResourceAssets[i];
                break;
            }
        }

        return itemToReturn;
    }
    #endregion


    #region OtherPrefabAsset
    public List<OtherPrefabAsset> listUIPrefabAssets;
    public OtherPrefabAsset GetOtherPrefabAsset(string itemName)
    {
        OtherPrefabAsset itemToReturn = new OtherPrefabAsset();

        for (var i = 0; i < listUIPrefabAssets.Count; i++)
        {
            if (listUIPrefabAssets[i].itemName == itemName)
            {
                itemToReturn = listUIPrefabAssets[i];
                break;
            }
        }

        return itemToReturn;
    }
    #endregion


    #region OtherSpriteAsset
    public List<OtherSpriteAsset> listOtherSpriteAssets;
    public OtherSpriteAsset GetOtherSpriteAsset(string itemName)
    {
        OtherSpriteAsset itemToReturn = new OtherSpriteAsset();

        for (var i = 0; i < listOtherSpriteAssets.Count; i++)
        {
            if (listOtherSpriteAssets[i].itemName == itemName)
            {
                itemToReturn = listOtherSpriteAssets[i];
                break;
            }
        }

        return itemToReturn;
    }
    #endregion
}

[System.Serializable]
public class OtherPrefabAsset
{
    public string itemName;
    public GameObject prefab;
}

[System.Serializable] 
public class OtherSpriteAsset
{
    public string itemName;
    public Sprite sprite;
}

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;

public class SelectedDataUIDisplayer : MonoBehaviour
{
    public static SelectedDataUIDisplayer instance;
    DataGlobal dataGlobal;
    [Header("Data Displayers On Scene")]
    [SerializeField] GameObject[] listSelectedTurretSlot;
    [SerializeField] GameObject[] listEquipedTurretArtSlot;
    [SerializeField] GameObject[] listSelectedSkillSlot;

    [Space(20)]

    [SerializeField] string empyIconId;

    UserData userData;

    #region Turret Data
    List<TurretResourceAsset> listTurretResourceAsset;
    TurretResourceAsset turretResourceAsset;
    TurretData[] listTurretData;
    TurretData turretData;
    #endregion
    string equipedArtName;

    List<DBHResourceAsset> listDBHResourceAssets;
    DBHResourceAsset dBHResourceAsset;
    List<DebuffHolderData> listDebuffHolderData;
    DebuffHolderData debuffHolderData;

    List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset;
    PlayerSkillResourceAsset playerSkillResourceAsset;
    List<PlayerSkillData> listPlayerSkillData;
    PlayerSkillData playerSkillData;

    Sprite empyIcon;

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    void GetGlobalData()
    {
        dataGlobal = DataGlobal.instance;
        userData = dataGlobal.userData;
        listTurretResourceAsset = dataGlobal.resourceDataAsset.listTurretResourceAsset;
        listTurretData = dataGlobal.dataAsset.listTurretAsset;

        listDBHResourceAssets = dataGlobal.resourceDataAsset.listDBHResourceAssets;
        listDebuffHolderData = dataGlobal.dataAsset.listDebuffHolderAssets;

        listPlayerSkillResourceAsset = dataGlobal.resourceDataAsset.listPlayerSkillResourceAsset;
        listPlayerSkillData = dataGlobal.dataAsset.listPlayerSkill;
    }

    void Start()
    {
        GetGlobalData();

        List<OtherSpriteAsset> listOtherSpriteAsset = dataGlobal.resourceDataAsset.listOtherSpriteAssets;
        for (var i = 0; i < listOtherSpriteAsset.Count; i++)
        {
            if (listOtherSpriteAsset[i].itemName == empyIconId) {
                empyIcon = listOtherSpriteAsset[i].sprite;
                break;
            }
        }

        DisplayerUserSelectedTurrets();
    }

    void DisplayerUserSelectedTurrets()
    {
        if (listSelectedTurretSlot.Length != userData.listTurretIds.Length) return;

        for (var i = 0; i < listSelectedTurretSlot.Length; i++)
        {
            int turretIndex = i;
            DisplayUserSelectedTurret(i);
            listSelectedTurretSlot[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                DataUIDisplayer.instance.slotIndexToChangeTurretData = turretIndex;
            });
        }
    }

    
    void DisplayUserSelectedTurret(int slotIndex)
    {
        GameObject selectedSlot = listSelectedTurretSlot[slotIndex];
        string turretId = userData.listTurretIds[slotIndex];

        if (string.IsNullOrEmpty(turretId))
        {
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = empyIcon;
            selectedSlot.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = empyIcon;
            selectedSlot.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty Art";
        } else
        {
            // Get avatar
            for (var j = 0; j < listTurretResourceAsset.Count; j++)
            {
                if (listTurretResourceAsset[j].itemName == turretId)
                {
                    turretResourceAsset = listTurretResourceAsset[j];
                    turretData = listTurretData[j];
                    break;
                }
            }
            // Asign new value
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = turretResourceAsset.listTurretsAvatar[0];

            DisplayUserEquipedTurretEffect(slotIndex);
        }
    }

    void DisplayUserEquipedTurretEffect(int slotIndex)
    {
        GameObject selectedSlot = listEquipedTurretArtSlot[slotIndex];
        string equipedTurret = userData.listTurretIds[slotIndex];
        string dBHId = null;

        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (listTurretData[i].itemName == equipedTurret)
            {
                dBHId = listTurretData[i].equipedDebuffId;
            }
        }

        if (string.IsNullOrEmpty(dBHId))
        {
            selectedSlot.transform.GetChild(1).GetComponent<Image>().sprite = empyIcon;
            selectedSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty Art";
        }
        else
        {
            // Get avatar and data
            for (var j = 0; j < listDBHResourceAssets.Count; j++)
            {
                if (listDBHResourceAssets[j].itemName == dBHId)
                {
                    dBHResourceAsset = listDBHResourceAssets[j];
                    debuffHolderData = listDebuffHolderData[j];
                    break;
                }
            }
            // Asign new value
            selectedSlot.transform.GetChild(1).GetComponent<Image>().sprite = dBHResourceAsset.avatar;
            selectedSlot.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = debuffHolderData.itemName;
        }
    }

    public void OnTurretSelectionSlotEnter(int slotIndex, string turretId = null)
    {
        for (var i = 0; i < userData.listTurretIds.Length; i++)
        {
            if (userData.listTurretIds[i] == turretId)
            {
                userData.listTurretIds[i] = null;

                DisplayUserSelectedTurret(i);
                return;
            }
        }

        // Reset Turret Equipment
        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (listTurretData[i].itemName == turretId)
            {
                listTurretData[i].equipedDebuffId = null;
                break;
            }
        }

        userData.listTurretIds[slotIndex] = turretId;
        DisplayUserSelectedTurret(slotIndex);

    }

    // Modification act on TurretAsset rather than UserData
    public void OnDBHSelectionSlotEnter(int slotIndex, string dBHId = null)
    {
        // Get Turret of the same Slot
        for (var i = 0; i < listTurretData.Length; i++)
        {
            if (listTurretData[i].itemName == userData.listTurretIds[slotIndex])
            {
                // Zeee we found our turretData
                turretData = listTurretData[i];
                break;
            }
        }

        if (turretData == null) return;

        // Use same trategy to take corresponding dBHId debuffHolderData to check equip condition
        for (var i = 0; i < listDebuffHolderData.Count; i++)
        {
            if (listDebuffHolderData[i].itemName == dBHId)
            {
                debuffHolderData = listDebuffHolderData[i];
                break;
            }
        }

        if (turretData.equipedDebuffId == dBHId)
        {
            // Reset data and reload the button
            turretData.equipedDebuffId = null;
            DisplayUserEquipedTurretEffect(slotIndex);
        }
        else
        {
            // Check Equip Condition, return if not stastify
            if (!debuffHolderData.targetStyle.Contains(turretData.turretStyle))
            {
                Debug.Log("Mismatch style");
                return;
            }

            turretData.equipedDebuffId = dBHId;
            DisplayUserEquipedTurretEffect(slotIndex);
        }
    }


    void DisplayerUserSelectedSkills()
    {
        if (listSelectedSkillSlot.Length != userData.listSkillIds.Length) return;

        for (var i = 0; i < listSelectedSkillSlot.Length; i++)
        {
            int skillIndex = i;
            DisplayUserSelectedSkill(i);
            listSelectedSkillSlot[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                DataUIDisplayer.instance.slotIndexToChangePSData = skillIndex;
            });
        }
    }


    /// <summary>
    /// Display Selected Turret from userData slot <param name="slotIndex"></param> into corresponding UISlot
    /// </summary>
    /// <param name="slotIndex"></param>
    void DisplayUserSelectedSkill(int slotIndex)
    {
        GameObject selectedSlot = listSelectedSkillSlot[slotIndex];
        string skillId = userData.listSkillIds[slotIndex];

        if (string.IsNullOrEmpty(skillId))
        {
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = empyIcon;
            //selectedSlot.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = empyIcon;
            //selectedSlot.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty Art";
        }
        else
        {
            // Get avatar
            for (var j = 0; j < listPlayerSkillResourceAsset.Count; j++)
            {
                if (listPlayerSkillResourceAsset[j].itemName == skillId)
                {
                    playerSkillResourceAsset = listPlayerSkillResourceAsset[j];
                    playerSkillData = listPlayerSkillData[j];
                    break;
                }
            }
            // Asign new value
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = playerSkillResourceAsset.avatar;
        }
    }

    public void OnPlayerSKillSelectionSlotEnter(int slotIndex, string skillName = null)
    {
        for (var i = 0; i < userData.listSkillIds.Length; i++)
        {
            if (userData.listSkillIds[i] == skillName)
            {
                userData.listSkillIds[i] = null;

                DisplayUserSelectedSkill(i);
                return;
            }
        }

        userData.listSkillIds[slotIndex] = skillName;
        DisplayUserSelectedSkill(slotIndex);

    }
}

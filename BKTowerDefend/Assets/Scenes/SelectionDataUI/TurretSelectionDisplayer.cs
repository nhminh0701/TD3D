using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretSelectionDisplayer : UISelectionDataDisplayer
{
    #region Turret variables
    List<TurretResourceAsset> listTurretResourceAsset;
    TurretResourceAsset turretResourceAsset;
    List<TurretData> listTurretData;
    TurretData turretData;
    #endregion

    #region DBH variables
    [Space(10)]
    [Header("Turret Equipment Data _DBH")]
    [SerializeField] GameObject[] listSelectingEquipment;

    List<DBHResourceAsset> listDBHResourceAssets;
    DBHResourceAsset dBHResourceAsset;
    List<DebuffHolderData> listDebuffHolderData;
    DebuffHolderData debuffHolderData;
    #endregion

    private void Awake()
    {
        ItemDataPopUpWindow.OnAddingTurretItem += OnSelectingTurretEnter;
        ItemDataPopUpWindow.OnAddingDBHItem += OnSelectingDBHEnter;
    }

    private void OnDestroy()
    {
        ItemDataPopUpWindow.OnAddingTurretItem -= OnSelectingTurretEnter;
        ItemDataPopUpWindow.OnAddingDBHItem -= OnSelectingDBHEnter;
    }

    protected override void GetGlobalData()
    {
        base.GetGlobalData();
        listTurretResourceAsset = dataGlobal.resourceDataAsset.listTurretResourceAsset;
        listTurretData = dataGlobal.dataAsset.listTurretAsset;
        listDBHResourceAssets = dataGlobal.resourceDataAsset.listDBHResourceAssets;
        listDebuffHolderData = dataGlobal.dataAsset.listDebuffHolderAssets;
    }

    protected override void DisplaySelectedDatas()
    {
        if (listSelectingItem.Length != userData.listTurretIds.Length) return;

        for (var i = 0; i < listSelectingItem.Length; i++)
        {
            int turretIndex = i;
            DisplaySelectedData(i);
            listSelectingItem[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                selectingSlotIndex = turretIndex;
            });
        }
    }

    protected override void DisplaySelectedData(int slotIndex)
    {
        GameObject selectedSlot = listSelectingItem[slotIndex];
        string turretId = userData.listTurretIds[slotIndex];

        if (string.IsNullOrEmpty(turretId))
        {
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = empyIcon;
            selectedSlot.transform.GetChild(1).transform.GetChild(1).GetComponent<Image>().sprite = empyIcon;
            selectedSlot.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Empty Art";
        }
        else
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

    /// <summary>
    /// Put turret with Id into current selected slot
    /// </summary>
    /// <param name="turretId"></param>
    public void OnSelectingTurretEnter(string turretId = null)
    {
        for (var i = 0; i < userData.listTurretIds.Length; i++)
        {
            if (userData.listTurretIds[i] == turretId)
            {
                userData.listTurretIds[i] = null;
                DisplaySelectedData(i);
                return;
            }
        }

        // Reset Turret Equipment
        for (var i = 0; i < listTurretData.Count; i++)
        {
            if (listTurretData[i].itemName == turretId)
            {
                listTurretData[i].equipedDebuffId = null;
                break;
            }
        }
        userData.listTurretIds[selectingSlotIndex] = turretId;
        DisplaySelectedData(selectingSlotIndex);
    }

    #region TurretEffect Manager
    void DisplayUserEquipedTurretEffect(int slotIndex)
    {
        GameObject selectedSlot = listSelectingEquipment[slotIndex];
        string equipedTurret = userData.listTurretIds[slotIndex];
        string dBHId = null;

        for (var i = 0; i < listTurretData.Count; i++)
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

    public void OnSelectingDBHEnter(string dBHId = null)
    {
        // Get Turret of the same Slot
        for (var i = 0; i < listTurretData.Count; i++)
        {
            if (listTurretData[i].itemName == userData.listTurretIds[selectingSlotIndex])
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
            DisplayUserEquipedTurretEffect(selectingSlotIndex);
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
            DisplayUserEquipedTurretEffect(selectingSlotIndex);
        }
    }
    #endregion
}

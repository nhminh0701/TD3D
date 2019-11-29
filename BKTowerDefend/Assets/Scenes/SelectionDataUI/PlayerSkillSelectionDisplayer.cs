using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSkillSelectionDisplayer : UISelectionDataDisplayer
{
    List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset;
    PlayerSkillResourceAsset playerSkillResourceAsset;
    List<PlayerSkillData> listPlayerSkillData;
    PlayerSkillData playerSkillData;

    private void Awake()
    {
        ItemDataPopUpWindow.OnAddingSkillItem += OnSelectingPlayerSkill;
    }

    private void OnDestroy()
    {
        ItemDataPopUpWindow.OnAddingSkillItem -= OnSelectingPlayerSkill;
    }

    protected override void GetGlobalData()
    {
        base.GetGlobalData();
        listPlayerSkillResourceAsset = dataGlobal.resourceDataAsset.listPlayerSkillResourceAsset;
        listPlayerSkillData = dataGlobal.dataAsset.listPlayerSkill;
    }

    protected override void DisplaySelectedDatas()
    {
        if (listSelectingItem.Length != userData.listSkillIds.Length) return;

        for (var i = 0; i < listSelectingItem.Length; i++)
        {
            int skillIndex = i;
            DisplaySelectedData(i);
            listSelectingItem[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                selectingSlotIndex = skillIndex;
            });
        }
    }

    protected override void DisplaySelectedData(int slotIndex)
    {
        GameObject selectedSlot = listSelectingItem[slotIndex];
        string skillId = userData.listSkillIds[slotIndex];

        if (string.IsNullOrEmpty(skillId))
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = empyIcon;
        else
        {
            for (var j = 0; j < listPlayerSkillResourceAsset.Count; j++)
            {
                if (listPlayerSkillResourceAsset[j].itemName == skillId)
                {
                    playerSkillResourceAsset = listPlayerSkillResourceAsset[j];
                    playerSkillData = listPlayerSkillData[j];
                    break;
                }
            }
            selectedSlot.transform.GetChild(0).GetComponent<Image>().sprite = playerSkillResourceAsset.avatar;
        }
    }

    public void OnSelectingPlayerSkill(string skillName = null)
    {
        for (var i = 0; i < userData.listSkillIds.Length; i++)
        {
            if (userData.listSkillIds[i] == skillName)
            {
                userData.listSkillIds[i] = null;

                DisplaySelectedData(i);
                return;
            }
        }
        userData.listSkillIds[selectingSlotIndex] = skillName;
        DisplaySelectedData(selectingSlotIndex);
    }
}

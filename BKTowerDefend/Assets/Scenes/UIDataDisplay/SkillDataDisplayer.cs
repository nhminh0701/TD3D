using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillDataDisplayer : DataDsiplayer
{
    List<PlayerSkillData> listPlayerSkillData;
    List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset;

    protected override void LoadData()
    {
        listPlayerSkillData = dataGlobal.dataAsset.listPlayerSkill;
        listPlayerSkillResourceAsset = dataGlobal.resourceDataAsset.listPlayerSkillResourceAsset;

        for (var i = 0; i < listPlayerSkillData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform.GetChild(0).transform.GetChild(0));

            string skillName = listPlayerSkillData[i].itemName;
            int skillUnLockCond = PlayerPrefs.GetInt(skillName, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listPlayerSkillData[i].itemName;

            if (skillUnLockCond >= 0)
            {
                displayButton.GetComponent<Button>().onClick.AddListener(()
                    => EventManager.SelectSkillItem(skillName));
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listPlayerSkillData[i].itemName;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listPlayerSkillResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listPlayerSkillResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listPlayerSkillData[i].appShopPurchasePrice.ToString();
            }
        }
    }

}

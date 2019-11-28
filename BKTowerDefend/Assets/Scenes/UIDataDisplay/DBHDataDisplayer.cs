using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DBHDataDisplayer : DataDsiplayer
{
    List<DebuffHolderData> listDebuffHolderData;
    List<DBHResourceAsset> listDBHResourceAsset;

    protected override void LoadData()
    {
        listDebuffHolderData = dataGlobal.dataAsset.listDebuffHolderAssets;
        listDBHResourceAsset = dataGlobal.resourceDataAsset.listDBHResourceAssets;

        for (var i = 0; i < listDebuffHolderData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform);

            string effectName = listDebuffHolderData[i].itemName;
            int effectUnlockCond = PlayerPrefs.GetInt(effectName, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listDebuffHolderData[i].itemName;

            if (effectUnlockCond != 0)
            {
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listDebuffHolderData[i].itemName;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listDBHResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listDBHResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listDebuffHolderData[i].appShopPurchasePrice.ToString();
            }

            displayButton.GetComponent<Button>().onClick.AddListener(() => {
                // Debug.Log(turretName);
                //No Calling error
                //TurretSelectionDisplayer.instance.OnSelectingTurretEnter(turretName);
                ItemDataPopUpWindow.instance.DisplayerData(displayButton.transform,
                    dataGlobal.dataAsset.GetDebuffHolderData(effectName));
            });
        }
    }
}

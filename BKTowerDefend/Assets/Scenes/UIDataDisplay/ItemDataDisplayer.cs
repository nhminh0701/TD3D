using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ItemDataDisplayer : DataDsiplayer
{
    [SerializeField] ItemType itemType;
    List<ItemData> listItemData;
    List<ItemResource> listItemResources;

    // Start is called before the first frame update
    protected override void Start()
    {
        GetData();
        base.Start();
    }

    protected override void LoadData()
    {
        ResetEventRegistration();

        for (var i = 0; i < listItemData.Count; i++)
        {
            ItemData itemData = GetItemData(listItemData[i].itemName);

            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform);

            int unlockStatus = itemData.unlockStatusCode;

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemData.itemName;
            displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listItemResources[i].avatar;

            if (unlockStatus != 0)
            {
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
                displayButton.GetComponent<Image>().color = Color.white;
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listItemData[i].appShopPurchasePrice.ToString();
            }

            displayButton.GetComponent<Button>().onClick.AddListener(() => {
                ItemDataPopUpWindow.OnWindowExit += ResetDataDisplay;
                // All button reloaded so no need to unregister
                ItemDataPopUpWindow.instance.DisplayerData(displayButton.transform, itemData);
            });
        }
    }

    private void ResetEventRegistration()
    {
        ItemDataPopUpWindow.OnWindowExit -= ResetDataDisplay;
    }

    private void OnDestroy()
    {
        if (ItemDataPopUpWindow.IsExitEventNull()) ResetEventRegistration();
    }

    ItemData GetItemData(string itemName)
    {
        switch (itemType)
        {
            case (ItemType.Turret):
                return dataGlobal.dataAsset.GetTurretData(itemName);
            case (ItemType.DebuffHolder):
                return dataGlobal.dataAsset.GetDebuffHolderData(itemName);
            case (ItemType.PlayerSkill):
                return dataGlobal.dataAsset.GetPlayerSkillData(itemName);
        }
        return null;
    }

    void GetData()
    {
        if (dataGlobal == null) dataGlobal = DataGlobal.instance;
        listItemData = new List<ItemData>();
        listItemResources = new List<ItemResource>();

        switch (itemType)
        {
            case (ItemType.Turret):
                List<TurretData> listTurretData = dataGlobal.dataAsset.listTurretAsset;
                List<TurretResourceAsset> listTurretResourceAssets = dataGlobal.resourceDataAsset.listTurretResourceAsset;
                
                for (var i = 0; i < listTurretData.Count; i++)
                {
                    listItemData.Add(listTurretData[i]);
                }
                for (var i = 0; i < listTurretResourceAssets.Count; i++)
                {
                    listItemResources.Add(listTurretResourceAssets[i]);
                }
                break;
            case (ItemType.DebuffHolder):
                List<DebuffHolderData> listDebuffHolderData = dataGlobal.dataAsset.listDebuffHolderAssets;
                List<DBHResourceAsset> listDBHResourceAsset = dataGlobal.resourceDataAsset.listDBHResourceAssets;

                for (var i = 0; i < listDebuffHolderData.Count; i++)
                {
                    listItemData.Add(listDebuffHolderData[i]);
                }
                for (var i = 0; i < listDBHResourceAsset.Count; i++)
                {
                    listItemResources.Add(listDBHResourceAsset[i]);
                }
                break;
            case (ItemType.PlayerSkill):
                List<PlayerSkillData> listPlayerSkillData = dataGlobal.dataAsset.listPlayerSkill;
                List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset = dataGlobal.resourceDataAsset.listPlayerSkillResourceAsset;

                for (var i = 0; i < listPlayerSkillData.Count; i++)
                {
                    listItemData.Add(listPlayerSkillData[i]);
                }
                for (var i = 0; i < listPlayerSkillResourceAsset.Count; i++)
                {
                    listItemResources.Add(listPlayerSkillResourceAsset[i]);
                }
                break;
        }
    }
}

public enum ItemType
{
    Turret,
    DebuffHolder,
    PlayerSkill,
}

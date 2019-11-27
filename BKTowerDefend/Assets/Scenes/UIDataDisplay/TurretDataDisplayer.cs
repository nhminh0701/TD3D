using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretDataDisplayer : DataDsiplayer
{
    List<TurretData> listTurretData;
    List<TurretResourceAsset> listTurretResourceAsset;

    private void Awake()
    {
        EventManager.OnPurchaseTurret += OnPurchaseTurretEvent;
    }

    private void OnDestroy()
    {
        EventManager.OnPurchaseTurret -= OnPurchaseTurretEvent;
    }

    void OnPurchaseTurretEvent(string itemName)
    {
        LoadData();
    }

    protected override void LoadData()
    {
        listTurretData = dataGlobal.dataAsset.listTurretAsset;
        listTurretResourceAsset = dataGlobal.resourceDataAsset.listTurretResourceAsset;

        for (var i = 0; i < listTurretData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform.GetChild(0).transform.GetChild(0));

            string turretName = listTurretData[i].itemName;
            int turretUnlockCond = PlayerPrefs.GetInt(turretName + "LV", 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listTurretData[i].itemName;

            if (turretUnlockCond != 0)
            {
                
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listTurretData[i].itemName;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listTurretResourceAsset[i].listTurretsAvatar[0];
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listTurretResourceAsset[i].listTurretsAvatar[0];
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listTurretData[i].appShopPurchasePrice.ToString();
            }

            displayButton.GetComponent<Button>().onClick.AddListener(()
                    => TurretPopupWindow.instance.DisplayerData(displayButton.transform,
                    dataGlobal.dataAsset.GetTurretData(turretName)));
        }
    }
}

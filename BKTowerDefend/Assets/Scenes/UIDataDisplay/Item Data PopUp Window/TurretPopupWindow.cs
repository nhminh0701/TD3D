using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TurretPopupWindow : ItemDataPopUpWindow
{
    public static TurretPopupWindow instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        targetWindow.transform.localScale = new Vector3(0, 0, 1);
    }

    public void DisplayerData(Transform pressedBtnTransform, TurretData turretData)
    {
        string itemName = turretData.itemName;

        PopUpWindow(pressedBtnTransform);

        Transform itemDataContent = targetWindow.transform.GetChild(0);
        itemDataContent.transform.GetChild(0).GetComponent<Image>().sprite =
            resourceDataAsset.GetTurretResourceAsset(itemName).listTurretsAvatar[0];
        itemDataContent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemName;
        itemDataContent.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            turretData.description;

        GameObject confirmationButton = targetWindow.transform.GetChild(1).transform.GetChild(0).gameObject;

        confirmationButton.GetComponent<Button>().onClick.AddListener(()
            => VisualizeButton(turretData, confirmationButton));
        // Close Button
        targetWindow.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>().
            onClick.AddListener(() => CloseWindow());
    }

    void VisualizeButton(TurretData turretData, GameObject buttonObject)
    {
        int unlockCodeStatus = PlayerPrefs.GetInt(turretData.itemName + "LV", 0);

        if (unlockCodeStatus == 0)
        {
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                turretData.appShopPurchasePrice.ToString();

            bool hasEnoughMoney = HasEnoughMoney(turretData.appShopPurchasePrice, "gold");
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color =
                hasEnoughMoney ? Color.black : Color.red;
            buttonObject.GetComponent<Button>().interactable = hasEnoughMoney;

            if (hasEnoughMoney)
            {
                buttonObject.GetComponent<Button>().onClick.AddListener(()
                    => PurchaseTurret(turretData, buttonObject));
                
            }
        }
        else
        {
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Add";
            buttonObject.GetComponent<Button>().onClick.AddListener(()
                => EventManager.AddTurretItem(turretData.itemName));
        }
    }

    void PurchaseTurret(TurretData turretData, GameObject buttonObject)
    {
        EventManager.PurchaseTurretEvent(turretData.itemName);
        VisualizeButton(turretData, buttonObject);
    }
}

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
        VisualizeButton(turretData, confirmationButton);
        // Close Button
        Button closeButton = targetWindow.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        closeButton.onClick.AddListener(() => CloseWindow(closeButton));
    }

    void VisualizeButton(TurretData turretData, GameObject buttonObject)
    {
        int unlockCodeStatus = turretData.unlockStatusCode;

        // Reset button events
        buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();

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
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Add/Remove";
            // buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                ButtonEvent(turretData, buttonObject.GetComponent<Button>());
            });
        }
    }

    private void ButtonEvent(TurretData turretData, Button eventedButton)
    {
        EventManager.AddTurretItem(turretData.itemName);
        CloseWindow(eventedButton);
    }

    void PurchaseTurret(TurretData turretData, GameObject buttonObject)
    {
        DataGlobal.instance.PurchaseTurret(turretData.itemName);
        TurretDataDisplayer.instance.ResetDataDisplay();
        VisualizeButton(turretData, buttonObject);
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemDataPopUpWindow : MonoBehaviour
{
    public static ItemDataPopUpWindow instance;

    [SerializeField] protected GameObject targetWindow;

    protected UserData userData;
    protected ResourceDataAsset resourceDataAsset;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        targetWindow.transform.localScale = new Vector3(0, 0, targetWindow.transform.localScale.z);
    }

    private void Start()
    {

        DataGlobal dataGlobal = DataGlobal.instance;
        userData = dataGlobal.userData;
        resourceDataAsset = dataGlobal.resourceDataAsset;
    }

    public virtual void CloseWindow(Button eventedButton)
    {
        targetWindow.transform.DOScale(0, 0.1f);
        eventedButton.onClick.RemoveAllListeners();
    }

    public void PopUpWindow(Transform pressedBtnTransform)
    {
        targetWindow.transform.position = pressedBtnTransform.position;
        targetWindow.transform.DOMove(new Vector3(Screen.width / 2, Screen.height / 2, targetWindow.transform.position.z), 0.1f);
        targetWindow.transform.DOScale(1, 0.1f);
    }

    protected bool HasEnoughMoney(ItemData itemData)
    {
        int cost = itemData.appShopPurchasePrice;
        switch (itemData.purchaseType)
        {
            case (PurchaseType.Coin):
                return cost <= userData.coin;
            case (PurchaseType.Gold):
                return cost <= userData.gold;
        }
        return false;
    }

    ItemResource itemResource;
    public void DisplayerData(Transform pressedBtnTransform, ItemData itemData)
    {
        itemResource = resourceDataAsset.GetItemResource(itemData);

        string itemName = itemData.itemName;

        PopUpWindow(pressedBtnTransform);

        Transform itemDataContent = targetWindow.transform.GetChild(0);
        itemDataContent.transform.GetChild(0).GetComponent<Image>().sprite = itemResource.avatar;
        itemDataContent.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = itemName;
        itemDataContent.transform.GetChild(2).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            itemData.description;

        GameObject confirmationButton = targetWindow.transform.GetChild(1).transform.GetChild(0).gameObject;
        VisualizeButton(itemData, confirmationButton);
        // Close Button
        Button closeButton = targetWindow.transform.GetChild(1).transform.GetChild(1).GetComponent<Button>();
        closeButton.onClick.AddListener(() => CloseWindow(closeButton));
    }

    void VisualizeButton(ItemData itemData, GameObject buttonObject)
    {
        int unlockCodeStatus = itemData.unlockStatusCode;

        // Reset button events
        buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();

        if (unlockCodeStatus == 0)
        {
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
                itemData.appShopPurchasePrice.ToString();

            bool hasEnoughMoney = HasEnoughMoney(itemData);
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().color =
                hasEnoughMoney ? Color.black : Color.red;
            buttonObject.GetComponent<Button>().interactable = hasEnoughMoney;

            if (hasEnoughMoney)
            {
                buttonObject.GetComponent<Button>().onClick.AddListener(()
                    => PurchaseTurret(itemData, buttonObject));
            }
        }
        else
        {
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Add/Remove";
            // buttonObject.GetComponent<Button>().onClick.RemoveAllListeners();
            buttonObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                ItemAddingEvent(itemData, buttonObject.GetComponent<Button>());
            });
        }
    }

    private void ItemAddingEvent(ItemData itemData, Button eventedButton)
    {
        // EventManager.AddTurretItem(turretData.itemName);
        EventManager.AddItem(itemData);
        CloseWindow(eventedButton);
    }

    void PurchaseTurret(ItemData itemData, GameObject buttonObject)
    {
        DataGlobal.instance.PurchaseItem(itemData);
        TurretDataDisplayer.instance.ResetDataDisplay();
        VisualizeButton(itemData, buttonObject);
    }
}

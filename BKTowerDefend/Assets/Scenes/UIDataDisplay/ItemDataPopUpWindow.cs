using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemDataPopUpWindow : MonoBehaviour
{
    public static ItemDataPopUpWindow instance;

    [SerializeField] GameObject targetWindow;
    [SerializeField] GameObject confirmationButton;

    UserData userData;
    ResourceDataAsset resourceDataAsset;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        targetWindow.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, targetWindow.transform.localScale.z);
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
        targetWindow.GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 0.1f);
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

            if (hasEnoughMoney) buttonObject.GetComponent<Button>().onClick.AddListener(() => PurchaseItem(itemData, buttonObject));
        }
        else
        {
            buttonObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Add/Remove";
            buttonObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                ItemAddingEvent(itemData, buttonObject.GetComponent<Button>());
            });
        }
    }

    private void ItemAddingEvent(ItemData itemData, Button eventedButton)
    {
        AddItem(itemData);
        CloseWindow(eventedButton);
    }

    void PurchaseItem(ItemData itemData, GameObject buttonObject)
    {
        DataGlobal.instance.PurchaseItem(itemData);
        CloseEvent();
        VisualizeButton(itemData, buttonObject);
    }

    #region Event OnChangingSelectedItem
    public delegate void OnSelectItemEvent(string itemId);
    void AddItem(ItemData item)
    {
        string itemName = item.itemName;

        switch (item.GetType().ToString())
        {
            case "TurretData":
                AddTurretItem(itemName);
                break;
            case "DebuffHolderData":
                AddDBHItem(itemName);
                break;
            case "PlayerSkillData":
                AddSkillItem(itemName);
                break;
        }
    }
    #region Change Turret Selection
    public static event OnSelectItemEvent OnAddingTurretItem;
    void AddTurretItem(string itemName)
    {
        if (OnAddingTurretItem != null) OnAddingTurretItem(itemName);
    }
    #endregion

    #region Change Skill Selection
    public static event OnSelectItemEvent OnAddingSkillItem;
    void AddSkillItem(string itemName)
    {
        if (OnAddingSkillItem != null) OnAddingSkillItem(itemName);
    }
    #endregion

    #region Change DBH Selection
    public static event OnSelectItemEvent OnAddingDBHItem;
    void AddDBHItem(string itemName)
    {
        if (OnAddingDBHItem != null) OnAddingDBHItem(itemName);
    }
    #endregion

    public delegate void OnButtonExitEvent();
    public static event OnButtonExitEvent OnWindowExit;
    void CloseEvent()
    {
        if (OnWindowExit != null) OnWindowExit();
    }

    public static bool IsExitEventNull() { return OnWindowExit == null; }
    #endregion
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ItemDataPopUpWindow : MonoBehaviour
{
    [SerializeField] GameObject targetWindow;

    
    UserData userData;
    ResourceDataAsset resourceDataAsset;

    private void Start()
    {
        DataGlobal dataGlobal = DataGlobal.instance;
        userData = dataGlobal.userData;
        resourceDataAsset = dataGlobal.resourceDataAsset;
    }

    public void CloseWindow()
    {
        targetWindow.transform.DOScale(0, 0.1f);
    }

    public void DisplayerData(Transform pressedBtnTransform, TurretData turretData)
    {
        string itemName = turretData.itemName;

        targetWindow.transform.position = pressedBtnTransform.position;
        targetWindow.transform.DOScale(1, 0.1f);

        Transform itemDataContent = targetWindow.transform.GetChild(0).transform.GetChild(0);
        itemDataContent.transform.GetChild(0).GetComponent<Image>().sprite = 
            resourceDataAsset.GetTurretResourceAsset(itemName).listTurretsAvatar[0];
        itemDataContent.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = itemName;
        itemDataContent.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text =
            turretData.description;

        GameObject confirmationButton = targetWindow.transform.GetChild(0).transform.GetChild(0).gameObject;

        // Check Money
    }
}

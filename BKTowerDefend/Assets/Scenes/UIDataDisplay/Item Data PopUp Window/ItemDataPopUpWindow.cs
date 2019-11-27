using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public abstract class ItemDataPopUpWindow : MonoBehaviour
{
    [SerializeField] protected GameObject targetWindow;

    protected UserData userData;
    protected ResourceDataAsset resourceDataAsset;

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

    protected bool HasEnoughMoney(int cost, string userCurrency)
    {
        if (userCurrency == "coin")
        {
            return cost <= userData.coin;
        } else
        {
            return cost <= userData.gold;
        }
    }
}

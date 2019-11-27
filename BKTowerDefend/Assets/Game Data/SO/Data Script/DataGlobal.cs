using System;
using UnityEngine;

public class DataGlobal : MonoBehaviour
{
    public static DataGlobal instance;

    public ResourceDataAsset resourceDataAsset;
    public DataAsset dataAsset;
    public UserData userData;

    private void Awake()
    {
        LoadData();
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        RegisterPurchaseEvent();
    }

    private void RegisterPurchaseEvent()
    {
        EventManager.OnPurchaseTurret += PurchaseTurret;
        EventManager.OnPurchaseDBH += PurchaseDBH;
        EventManager.OnPurchaseSkill += PurchaseSkill;
    }

    private void OnDestroy()
    {
        EventManager.OnPurchaseTurret -= PurchaseTurret;
        EventManager.OnPurchaseDBH -= PurchaseDBH;
        EventManager.OnPurchaseSkill -= PurchaseSkill;
    }

    void LoadData()
    {
        dataAsset.LoadData();
        userData.LoadData();
    }

    [ContextMenu("Reset")]
    public void ResetData()
    {
        dataAsset.ResetData();
        userData.ResetData();
    }

    public void PurchaseTurret(string itemName)
    {
        TurretData turretData = dataAsset.GetTurretData(itemName);
        turretData.UnlockThisTurret();
        userData.ChangeGold(-turretData.appShopPurchasePrice);
    }

    void PurchaseDBH(string itemName)
    {
        DebuffHolderData debuffHolderData = dataAsset.GetDebuffHolderData(itemName);
        userData.ChangeCoin(-debuffHolderData.appShopPurchasePrice);
    }

    void PurchaseSkill(string itemName)
    {
        PlayerSkillData playerSkillData = dataAsset.GetPlayerSkillData(itemName);
        userData.ChangeCoin(-playerSkillData.appShopPurchasePrice);
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DataUIDisplayer : MonoBehaviour
{
    public static DataUIDisplayer instance;
    DataGlobal dataGlobal;
    // [1]: Turret Data
    // [2]: Turret Equipment Data
    // [3]: Player Skill Data
    [SerializeField] GameObject[] listItemDataDisplayers;
    [SerializeField] string itemUIPrefabId;
    [SerializeField] Color lockedColor;

    GameObject dataUIPrefab;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        dataGlobal = DataGlobal.instance;
        List<OtherPrefabAsset> listOtherPrefabAsset = dataGlobal.resourceDataAsset.listUIPrefabAssets;
        for (var i = 0; i < listOtherPrefabAsset.Count; i ++)
        {
            if (listOtherPrefabAsset[i].itemName == itemUIPrefabId) { dataUIPrefab = listOtherPrefabAsset[i].prefab; }
        }

        ExtractDataToDisplayers();
    }

    private void ExtractDataToDisplayers()
    {
        // Show Turret Data
        ExtracTurretData();
        ExtracPlayerDBHData();
        ExtracPlayerSKillData();
    }

    #region Turret DataUI transfer
    List<TurretData> listTurretData;
    List<TurretResourceAsset> listTurretResourceAsset;
    //[HideInInspector]
    public int slotIndexToChangeTurretData;

    private void ExtracTurretData()
    {
        if (listItemDataDisplayers[0] == null) return;

        listTurretData = dataGlobal.dataAsset.listTurretAsset;
        listTurretResourceAsset = dataGlobal.resourceDataAsset.listTurretResourceAsset;

        for (var i = 0; i < listTurretData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, listItemDataDisplayers[0].transform.position, Quaternion.identity);
            displayButton.transform.SetParent(listItemDataDisplayers[0].transform.GetChild(0).transform.GetChild(0));

            string turretName = listTurretData[i].itemName;
            int turretUnlockCond = PlayerPrefs.GetInt(turretName + "LV", 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listTurretData[i].itemName;

            if (turretUnlockCond != 0)
            {
                displayButton.GetComponent<Button>().onClick.AddListener(()
                    => SelectedDataUIDisplayer.instance.OnTurretSelectionSlotEnter(slotIndexToChangeTurretData, turretName));
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listTurretData[i].itemName;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listTurretResourceAsset[i].listTurretsAvatar[0];
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                Debug.Log(listTurretData[i].itemName);
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listTurretResourceAsset[i].listTurretsAvatar[0];
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listTurretData[i].appShopPurchasePrice.ToString();
            }
        }
    }
    #endregion


    #region DBH DataUI transfer
    List<DebuffHolderData> listDebuffHolderData;
    List<DBHResourceAsset> listDBHResourceAsset;

    private void ExtracPlayerDBHData()
    {
        if (listItemDataDisplayers[1] == null) return;

        listDebuffHolderData = dataGlobal.dataAsset.listDebuffHolderAssets;
        listDBHResourceAsset = dataGlobal.resourceDataAsset.listDBHResourceAssets;

        for (var i = 0; i < listDebuffHolderData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, listItemDataDisplayers[1].transform.position, Quaternion.identity);
            displayButton.transform.SetParent(listItemDataDisplayers[1].transform.GetChild(0).transform.GetChild(0));

            string effectName = listDebuffHolderData[i].itemName;
            int effectUnlockCond = PlayerPrefs.GetInt(effectName, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listDebuffHolderData[i].itemName;

            if (effectUnlockCond >= 0)
            {
                displayButton.GetComponent<Button>().onClick.AddListener(()
                    => SelectedDataUIDisplayer.instance.OnDBHSelectionSlotEnter(slotIndexToChangeTurretData, effectName));
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
        }
    }
    #endregion


    #region PlayerSkill DataUI transfer
    List<PlayerSkillData> listPlayerSkillData;
    List<PlayerSkillResourceAsset> listPlayerSkillResourceAsset;
    [HideInInspector]
    public int slotIndexToChangePSData;

    private void ExtracPlayerSKillData()
    {
        if (listItemDataDisplayers[2] == null) return;

        listPlayerSkillData = dataGlobal.dataAsset.listPlayerSkill;
        listPlayerSkillResourceAsset = dataGlobal.resourceDataAsset.listPlayerSkillResourceAsset;

        for (var i = 0; i < listPlayerSkillData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, listItemDataDisplayers[2].transform.position, Quaternion.identity);
            displayButton.transform.SetParent(listItemDataDisplayers[2].transform.GetChild(0).transform.GetChild(0));

            string skillName = listPlayerSkillData[i].itemName;
            int skillUnLockCond = PlayerPrefs.GetInt(skillName, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listPlayerSkillData[i].itemName;

            if (skillUnLockCond >= 0)
            {
                displayButton.GetComponent<Button>().onClick.AddListener(()
                    => SelectedDataUIDisplayer.instance.OnPlayerSKillSelectionSlotEnter(slotIndexToChangePSData, skillName));
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listPlayerSkillData[i].itemName;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listPlayerSkillResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(false);
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.transform.GetChild(1).GetComponent<Image>().sprite = listPlayerSkillResourceAsset[i].avatar;
                displayButton.transform.GetChild(2).gameObject.SetActive(true);
                displayButton.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = listPlayerSkillData[i].appShopPurchasePrice.ToString();
            }
        }
    }
    #endregion

}

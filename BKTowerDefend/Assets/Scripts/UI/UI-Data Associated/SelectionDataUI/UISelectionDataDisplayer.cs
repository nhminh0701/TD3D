using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UISelectionDataDisplayer : MonoBehaviour
{
    [SerializeField] string empyIconId;
    [SerializeField] protected GameObject[] listSelectingItem;

    protected DataGlobal dataGlobal;
    protected UserData userData;
    protected Sprite empyIcon;

    public int selectingSlotIndex;

    // Start is called before the first frame update
    void Start()
    {
        GetGlobalData();

        List<OtherSpriteAsset> listOtherSpriteAsset = dataGlobal.resourceDataAsset.listOtherSpriteAssets;
        for (var i = 0; i < listOtherSpriteAsset.Count; i++)
        {
            if (listOtherSpriteAsset[i].itemName == empyIconId)
            {
                empyIcon = listOtherSpriteAsset[i].sprite;
                break;
            }
        }

        DisplaySelectedDatas();
    }

    virtual protected void GetGlobalData()
    {
        dataGlobal = DataGlobal.instance;
        userData = dataGlobal.userData;
    }

    protected abstract void DisplaySelectedDatas();

    protected abstract void DisplaySelectedData(int slotIndex);
}

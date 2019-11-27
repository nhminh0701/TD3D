using System.Collections.Generic;
using UnityEngine;

public abstract class DataDsiplayer : MonoBehaviour
{
    protected DataGlobal dataGlobal;
    [SerializeField] protected GameObject itemsDisplayer;
    [SerializeField] string itemUIPrefabId;
    [SerializeField] protected Color lockedColor;

    protected GameObject dataUIPrefab;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        dataGlobal = DataGlobal.instance;
        List<OtherPrefabAsset> listOtherPrefabAsset = dataGlobal.resourceDataAsset.listUIPrefabAssets;
        for (var i = 0; i < listOtherPrefabAsset.Count; i++)
        {
            if (listOtherPrefabAsset[i].itemName == itemUIPrefabId) { dataUIPrefab = listOtherPrefabAsset[i].prefab; }
        }

        LoadData();
    }

    protected abstract void LoadData();

    public void ResetDataDisplay()
    {
        for (var i = 0; i < itemsDisplayer.transform.childCount; i ++)
        {
            SimplePool.Despawn(itemsDisplayer.transform.GetChild(i).gameObject);
        }

        LoadData();
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDataDisplayer : DataDsiplayer
{
    SceneFader sceneFader;
    List<StageData> listStageData;

    // Start is called before the first frame update
    protected override void Start()
    {
        sceneFader = SceneFader.instance;
        base.Start();
    }

    protected override void LoadData()
    {
        listStageData = dataGlobal.dataAsset.listStageData;

        for (var i = 0; i < listStageData.Count; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform);

            string stageId = listStageData[i].stageId;
            int stageUnLockCond = PlayerPrefs.GetInt(stageId, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listStageData[i].stageId;

            if (stageUnLockCond >= 0)
            {
                displayButton.GetComponent<Button>().onClick.AddListener(()
                    => sceneFader.FadeTo(listStageData[i].stageId));
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listStageData[i].stageId;
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageDataDisplayer : DataDsiplayer
{
    SceneFader sceneFader;
    StageData[] listStageData;

    // Start is called before the first frame update
    protected override void Start()
    {
        sceneFader = SceneFader.instance;
        base.Start();
    }

    protected override void LoadData()
    {
        listStageData = dataGlobal.dataAsset.listStageData;

        for (var i = 0; i < listStageData.Length; i++)
        {
            GameObject displayButton = SimplePool.Spawn(dataUIPrefab, itemsDisplayer.transform.position, Quaternion.identity);
            displayButton.transform.SetParent(itemsDisplayer.transform);

            string stageId = listStageData[i].stageName;
            int stageUnLockCond = PlayerPrefs.GetInt(stageId, 0);

            displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listStageData[i].stageName;

            if (stageUnLockCond >= 0)
            {
                int stageIndex = i;
                displayButton.GetComponent<Button>().onClick.AddListener(()=>
                {
                    sceneFader.FadeTo(listStageData[stageIndex].stageName.ToString());
                    dataGlobal.currentStageName = listStageData[stageIndex].stageName;
                });
                displayButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = listStageData[i].stageName;
            }
            else
            {
                displayButton.GetComponent<Image>().color = lockedColor;
                displayButton.GetComponent<Button>().interactable = false;
            }
        }
    }
}

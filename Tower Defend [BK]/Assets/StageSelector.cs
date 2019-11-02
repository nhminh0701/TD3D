using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    Button[] stageButtonList;

    SceneFader sceneFader;

    int reachedLv;

    // Start is called before the first frame update
    void Start()
    {
        sceneFader = FindObjectOfType<SceneFader>();

        reachedLv = PlayerPrefs.GetInt("levelReached", 1);

        stageButtonList = new Button[transform.childCount];

        for (var index = 0; index < transform.childCount; index++)
        {
            stageButtonList[index] = transform.GetChild(index).gameObject.GetComponent<Button>();
            int fadeLv = index + 1; 
            stageButtonList[index].onClick.AddListener(() => sceneFader.FadeTo("Lv " + fadeLv));
            stageButtonList[index].GetComponentInChildren<Text>().text = "Lv " + fadeLv;

            if (index >= reachedLv)
            {
                stageButtonList[index].interactable = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using UnityEngine;
using UnityEngine.UI;

public class StageSelector : MonoBehaviour
{
    Button[] stageButtonList;
    
    SceneFader sceneFader;

    UserData userData;
    DataGlobal dataGlobal;

    void Start()
    {
        dataGlobal = DataGlobal.instance;
        userData = dataGlobal.userData;

        sceneFader = FindObjectOfType<SceneFader>();

        stageButtonList = new Button[transform.childCount];

        for (var index = 0; index < transform.childCount; index++)
        {
            stageButtonList[index] = transform.GetChild(index).gameObject.GetComponent<Button>();
            int fadeLv = index + 1; 
            stageButtonList[index].onClick.AddListener(() => sceneFader.FadeTo("Lv " + fadeLv));
            stageButtonList[index].GetComponentInChildren<Text>().text = "Lv " + fadeLv;

            if (index >= userData.reachableLv)
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

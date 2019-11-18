using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUI : MonoBehaviour
{
    SceneFader sceneFader;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        // gameObject.SetActive(false);
        sceneFader = FindObjectOfType<SceneFader>();
    }

    public void ReturnHomeScene(string homeScene)
    {
        sceneFader.FadeTo(homeScene);
    }

    public void MoveToNextScene(string sceneName)
    {
        sceneFader.FadeTo(sceneName);
    }
}

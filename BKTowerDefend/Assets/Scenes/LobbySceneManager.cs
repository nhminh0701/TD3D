using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] RectTransform itemListingWindow;
    [SerializeField] RectTransform stageListingWindow;

    UserData userData;

    // Start is called before the first frame update
    void Start()
    {
        userData = DataGlobal.instance.userData;
        itemListingWindow.position = Vector2.zero;
        PreInitiateWindowPos();
        AnimateListingWindow();
    }

    private void AnimateListingWindow()
    {
        
    }

    private void PreInitiateWindowPos()
    {
        GameObject invenvoryUI = itemListingWindow.gameObject.transform.GetChild(0).gameObject;
        for (var i = 0; i < invenvoryUI.transform.childCount; i++)
        {
            invenvoryUI.transform.GetChild(0).transform.localScale = new Vector3(0, 0, 1);
        }
        invenvoryUI.gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition
            = new Vector2(0, 551);

        stageListingWindow.position = new Vector2(-8000, 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

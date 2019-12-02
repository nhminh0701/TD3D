using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;

public class LobbySceneManager : MonoBehaviour
{
    [SerializeField] HomeMenuTweener homeMenuTweener;
    [SerializeField] StageSelectionTweener stageSelectionTweener;
    [SerializeField] ItemSelectionTweener itemSelectionTweener;
    [SerializeField] Transform warningText;

    WindowTweener currentWindowTweener;
    DataGlobal dataGlobal;

    private void Start()
    {
        dataGlobal = DataGlobal.instance;
        currentWindowTweener = homeMenuTweener;
    }

    public void ToStageWindow()
    {
        if (!dataGlobal.IsEnoughItem()) TweenWarningText();
        else SwitchWindow(stageSelectionTweener);
    }

    public void ToItemWindow() { SwitchWindow(itemSelectionTweener); } 

    private void TweenWarningText()
    {
        warningText.DOScale(Vector3.one, 0.5f).OnComplete(() =>
        StartCoroutine(WarningTextCoroutine())
        );
        
    }

    public void BackToHome() { SwitchWindow(homeMenuTweener); }

    IEnumerator WarningTextCoroutine()
    {
        yield return new WaitForSeconds(1);
        warningText.DOScale(new Vector3(0, 0, 1), 0.5f);
    }



    void SwitchWindow(WindowTweener windowToOpen) 
    {
        StartCoroutine(SwtichWindowCoroutine(windowToOpen));
    }

    IEnumerator SwtichWindowCoroutine(WindowTweener windowToOpen)
    {
        currentWindowTweener.SwitchOutWindow();
        // Max tween Dur = 1
        yield return new WaitForSeconds(1);

        windowToOpen.SwitchInWindow();
        currentWindowTweener = windowToOpen;
    }
}

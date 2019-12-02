using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StageSelectionTweener : WindowTweener
{
    [ContextMenu("Switch in")]
    public override void SwitchInWindow()
    {
        tweeningWindow.DOAnchorPos(Vector2.zero, 0.5f).OnComplete(() => {
            tweeningWindow.transform.GetChild(0).GetComponent<RectTransform>().
            DOAnchorPos(Vector2.zero, 0.5f);
        });
    }

    [ContextMenu("Switch out")]
    public override void SwitchOutWindow()
    {
        tweeningWindow.transform.GetChild(0).GetComponent<RectTransform>().
            DOAnchorPos(new Vector2(0, -400), 1).SetEase(Ease.InElastic).OnComplete(() =>
            {
                tweeningWindow.DOAnchorPos(new Vector2(-3000, 0), 0.5f);
            });
    }

    // Start is called before the first frame update
    void Start()
    {
        tweeningWindow.anchoredPosition = new Vector2(-3000, 0);
        tweeningWindow.transform.GetChild(0).GetComponent<RectTransform>().
            anchoredPosition = new Vector2(0, -400);
    }
}

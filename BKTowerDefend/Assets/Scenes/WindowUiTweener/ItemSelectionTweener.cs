using UnityEngine;
using DG.Tweening;

public class ItemSelectionTweener : WindowTweener
{
    GameObject invenvoryUI;

    private void Start()
    {
        invenvoryUI = tweeningWindow.gameObject.transform.GetChild(0).gameObject;
        for (var i = 0; i < invenvoryUI.transform.childCount; i++)
        {
            invenvoryUI.transform.GetChild(i).transform.localScale = new Vector3(0, 0, 1);
        }
        tweeningWindow.gameObject.transform.GetChild(1).GetComponent<RectTransform>().anchoredPosition
            = new Vector2(0, 551);
    }

    [ContextMenu("Switch in")]
    public override void SwitchInWindow()
    {
        for (var i = 0; i < invenvoryUI.transform.childCount; i++)
        {
            invenvoryUI.transform.GetChild(i).transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        tweeningWindow.gameObject.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos(Vector2.zero, 1);
    }

    [ContextMenu("Switch out")]
    public override void SwitchOutWindow()
    {
        for (var i = 0; i < invenvoryUI.transform.childCount; i++)
        {
            invenvoryUI.transform.GetChild(i).transform.DOScale(new Vector3(0, 0, 1), 0.5f);
        }
        tweeningWindow.gameObject.transform.GetChild(1).GetComponent<RectTransform>().DOAnchorPos(new Vector2(0, 551), 1).SetEase(Ease.InExpo);
    }
}

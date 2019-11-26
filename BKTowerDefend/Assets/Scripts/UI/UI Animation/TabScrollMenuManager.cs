using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class TabScrollMenuManager : MonoBehaviour
{
    public Transform scrollWindowHolder;
    public List<Transform> scrolledWindows;
    [SerializeField] List<Button> tabSwitchButton;
    public Transform displayPos;

    // Start is called before the first frame update
    void Start()
    {
        for (var i = 0; i < tabSwitchButton.Count; i++)
        {
            int windowIndex = i;
            tabSwitchButton[i].onClick.AddListener(() => ScrollWindow(scrolledWindows[windowIndex]));
        }
    }

    void ScrollWindow(Transform windowTransform)
    {
        float targetXPos = displayPos.position.x - windowTransform.localPosition.x-displayPos.localPosition.x;
        scrollWindowHolder.DOMoveX(targetXPos, 0.3f);
    }
}

using TMPro;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;


public class HomeMenuTweener : WindowTweener
{
    Transform buttons;
    private void Start()
    {
        tweeningWindow.GetChild(0).GetComponent<TextMeshProUGUI>().alpha = 0;
        buttons = tweeningWindow.GetChild(1);
        for (var i = 0; i < buttons.childCount; i++)
        {
            buttons.GetChild(i).GetComponent<Animator>().enabled = false;
            buttons.GetChild(i).localScale = Vector3.zero;
        }

        SwitchInWindow();
    }

    public override void SwitchInWindow()
    {
        tweeningWindow.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(1, 1f).OnComplete(() =>
        {
            buttons.GetChild(0).DOScale(Vector3.one, 0.3f).OnComplete(() =>
            {
                buttons.GetChild(1).DOScale(Vector3.one, 0.3f).OnComplete(() => {
                    buttons.GetChild(0).GetComponent<Animator>().enabled = true;
                    buttons.GetChild(1).GetComponent<Animator>().enabled = true;
                });
            });
        });
    }

    public override void SwitchOutWindow()
    {
        buttons.GetChild(0).GetComponent<Animator>().enabled = false;
        buttons.GetChild(1).GetComponent<Animator>().enabled = false;

        tweeningWindow.GetChild(0).GetComponent<TextMeshProUGUI>().DOFade(0, 0.3f).OnComplete(() =>
        {
            buttons.GetChild(1).DOScale(Vector3.zero, 0.3f).OnComplete(() =>
            {
                buttons.GetChild(0).DOScale(Vector3.zero, 0.3f);
            });
        });
    }

}

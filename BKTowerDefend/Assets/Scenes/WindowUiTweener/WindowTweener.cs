using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Fade in and out animation for window using DoTween
/// </summary>
public abstract class WindowTweener : MonoBehaviour
{
    [SerializeField] protected RectTransform tweeningWindow;

    /// <summary>
    /// Pop in Window
    /// </summary>
    public abstract void SwitchInWindow();

    /// <summary>
    /// Pop out Window
    /// </summary>
    public abstract void SwitchOutWindow();
}

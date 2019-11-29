using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPos : MonoBehaviour
{
    [ContextMenu("Get Pos")]
    void GetUIObjectPos()
    {
        Debug.Log(gameObject.GetComponent<RectTransform>().anchoredPosition);
    }

    [ContextMenu("Get Screen Width")]
    void GetScreenWidth()
    {
        Debug.Log(Screen.width);
        Debug.Log(Screen.height);
    }

    [ContextMenu("Move To Center")]  
    void MoveToCenter()
    {
        gameObject.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0);
    }
}

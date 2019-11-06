using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void ResetData()
    {
        DataManager.instance.ClearSaveData();
    }
}

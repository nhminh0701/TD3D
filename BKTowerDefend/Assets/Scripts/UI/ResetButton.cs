using UnityEngine;

public class ResetButton : MonoBehaviour
{
    public void ResetData()
    {
        DataGlobal.instance.ResetData();
    }
}

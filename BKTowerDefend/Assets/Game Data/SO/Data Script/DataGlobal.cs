using UnityEngine;

public class DataGlobal : MonoBehaviour
{
    public static DataGlobal instance;

    public ResourceDataAsset resourceDataAsset;
    public DataAsset dataAsset;
    public UserData userData;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        LoadData();
    }

    void LoadData()
    {
        dataAsset.LoadData();
        userData.LoadData();
    }

    public void ResetData()
    {
        dataAsset.ResetData();
        userData.ResetData();
    }

    private void OnApplicationQuit()
    {
        // :v :v
        ResetData();
    }
}

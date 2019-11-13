using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Newtonsoft.Json;
using System;

public class DataManager : MonoBehaviour
{
    
    public static DataManager instance;
    [Header("Default data")]
    public GameData gameData;
    public GameDatabase gameDatabase;


    #region MonoBehavior CallBack
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(this);

    }

    private void Start()
    {
        LoadData();
    }

    private void OnApplicationQuit()
    {
        SaveGame();
    }
    #endregion

    public void LoadData(int dataIndex = 0)
    {
        gameData.LoadSaveGame();
        gameDatabase.turretDB.GetTurrets(gameData.currentState.turretState.turretIdList);
    }

    public void SaveGame()
    {
        gameData.LoadSaveGame();
    }

    public void ClearSaveData()
    {
        gameData.ClearData();
    }

    
}

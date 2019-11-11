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
    [SerializeField] GameData defaultSaveGame;
    [SerializeField] string saveGameName = "GameData";

    GameData currentData;
    List<GameData> loadedSaveGameList;
    #region Data access member
    [Header("Real-time data")]
    public int rtStageData;
    public List<TurretClass> rtTurretData;

    [Tooltip("Assigned based on rtTurretData")]
    public List<TurretClass> allTurretClasses;
    #endregion


    #region MonoBehavior CallBack
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else instance = this;

        DontDestroyOnLoad(this);

        currentData = new GameData();
        fileDestination = Path.Combine(Application.persistentDataPath, "GameData.json");
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


    #region JSON Data Processing

    string fileDestination;

    /// <summary>
    /// Load a save game and extract its components
    /// </summary>
    /// <param name="dataIndex"></param>
    public void LoadData(int dataIndex = 0)
    {
        Debug.Log("Try to load file at " + fileDestination);
        // if no savegame available, use default
        if (!File.Exists(fileDestination))
        {
            Debug.Log("Destination not existed so we use default");
            currentData = defaultSaveGame;
            ExtractGameData();
            Debug.Log(currentData.maxStagesReached);
            loadedSaveGameList = new List<GameData>();
            return;
        }

        Debug.Log("File save at " + fileDestination);
        string serializedData = File.ReadAllText(fileDestination);
        loadedSaveGameList = JsonConvert.DeserializeObject< List<GameData>>(serializedData);
        currentData = loadedSaveGameList[dataIndex];
        ExtractGameData();
    }

    /// <summary>
    /// First update loadSaveGameList with currentData (storing progress)
    /// Can overwrite slot with index or add with negative index
    /// Updated loadSaveGameList then written as savegame file
    /// </summary>
    /// <param name="index"></param>
    public void SaveGame(int index = 0)
    {
        UpdateProgress();

        // Add new save game
        if (index < 0 || loadedSaveGameList.Count==0) { loadedSaveGameList.Add(currentData); }
        // overwrite
        else { loadedSaveGameList[index] = currentData; }

        List<GameData> catchLoadedSG = loadedSaveGameList;

        string serializedObject = JsonConvert.SerializeObject(catchLoadedSG);
        File.WriteAllText(fileDestination, serializedObject);
        Debug.Log("Game saved at " + fileDestination);
    }

    public void ClearSaveData()
    {
        File.Delete(fileDestination);
        currentData = defaultSaveGame;
        ExtractGameData();
    }
    #endregion


    #region Data communication 
    private void ExtractGameData()
    {
        // Extract currentData to accessable components
        rtStageData = currentData.maxStagesReached;
        rtTurretData = currentData.turretsData.GetTurretClasses(allTurretClasses);
    }

    public void UpdateProgress()
    {
        // Write current status back to currentData
        // currentData is to be saved 
        currentData.maxStagesReached = rtStageData;
        currentData.turretsData.UpdateTurretDataFromRT(rtTurretData);
    }
    #endregion

    
}

using System.Collections;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEngine;
using System;

[CreateAssetMenu(fileName = "Game Data", menuName = "Game Setup/Game Data")]
public class GameData : ScriptableObject
{
    [SerializeField] GameState defaultState;
    [Tooltip("State to Save, Debug Purpose")]
    public GameState currentState = new GameState();
    GameState tempState;
    [SerializeField] string fileName;

    string destination;

    public void SaveCurrentState()
    {
        string destination = Path.Combine(Application.persistentDataPath, fileName + ".json");

        string fileContent = JsonConvert.SerializeObject(currentState);

        File.WriteAllText(destination, fileContent);
    }

    public void LoadSaveGame()
    {
        tempState = new GameState(defaultState.reachableLv, defaultState.turretState);

        string destination = Path.Combine(Application.persistentDataPath, fileName + ".json");
        if (File.Exists(destination))
        {
            string serializedObject = File.ReadAllText(destination);
            currentState = JsonConvert.DeserializeObject<GameState>(serializedObject);
        }
        else currentState = tempState;
    }

    public void ClearData()
    {
        tempState = new GameState(defaultState.reachableLv, defaultState.turretState);
        currentState = tempState;
    }
}

[System.Serializable] 
public class GameState
{
    public int reachableLv;

    public TurretState turretState;

    public GameState (int _reachableLv = 1, TurretState _turretState = null)
    {
        reachableLv = _reachableLv;
        turretState = _turretState;
    }
}

[System.Serializable]
public class TurretState
{
    public List<string> turretIdList;

    public TurretClass GetTurretFromID(List<TurretClass> originalList, string turretID)
    {
        for (var i = 0; i < originalList.Count; i ++)
        {
            if (originalList[i].classID == turretID)
                return originalList[i];
        }

        return null;
    }

    public List<TurretClass> GetTurretsFromIDs(List<TurretClass> originalList)
    {
        List<TurretClass> listToReturn = new List<TurretClass>();

        for (var i = 0; i < turretIdList.Count; i++)
        {
            TurretClass turretElement = GetTurretFromID(originalList, turretIdList[i]);
            if (turretElement != null)
            {
                listToReturn.Add(turretElement);
            }
        }

        return listToReturn;
    }
}

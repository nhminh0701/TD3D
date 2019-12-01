using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage Data", menuName = "Data/Data Asset/Stage Data")]
public class StageData : ScriptableObject
{
    public string stageName;
    public int stageUnlockStatus;
    public PlayerStatsData playerStatsData;
    public List<EnemyWaveData> listEnemyWaveData;

    [SerializeField] float minWaveSpawnRate;
    [SerializeField] float maxWaveSpawnRate;

    public float GetWaveSpawnRate()
    {
        return Random.Range(minWaveSpawnRate, maxWaveSpawnRate);
    }

    public List<string> listEnemyId;

    public void UnLockThisStage()
    {
        stageUnlockStatus = 4;
        PlayerPrefs.SetInt(stageName, stageUnlockStatus);
    }
}

[System.Serializable] 
public class PlayerStatsData
{
    public int coin;
    public int health;
}

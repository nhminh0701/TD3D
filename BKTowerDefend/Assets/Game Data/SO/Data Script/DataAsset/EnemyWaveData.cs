using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy Wave Data", menuName = "Data/Data Asset/Enemy Wave Data")]
public class EnemyWaveData : ScriptableObject
{
    public List<EnemyPathData> listEnemyPathData;
}

[System.Serializable] 
public class EnemyPathData
{
    public string pathId;
    [SerializeField] int minNumberEnemies;
    [SerializeField] int maxNumberEnemies;

    public int GetNumberEnemies()
    {
        return Random.Range(minNumberEnemies, maxNumberEnemies);
    }

    [SerializeField] float minEnemySpawnRate;
    [SerializeField] float maxEnemySpawnRate;

    public float GetEnemySpawnRate()
    {
        return Random.Range(minEnemySpawnRate, maxEnemySpawnRate);
    }
}

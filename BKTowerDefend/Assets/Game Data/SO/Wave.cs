using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Enemy Wave")]
public class Wave : ScriptableObject
{
    // To register Transform[] movePath of EnemyMovement via WaveSpawner
    public string waveName;

    public Enemy[] enemiesPrefab;

    public int amountOfEnemies;

    public float spawnRate;

    // public Transform[] movePath;

}

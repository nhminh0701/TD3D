using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave", menuName = "Enemy Wave")]
public class Wave : ScriptableObject
{
    public Transform[] enemiesPrefab;

    public int amountOfEnemies;

    public float spawnRate;

}

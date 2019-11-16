using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    public static int numberAliveEnemies;

    [SerializeField] Wave[] waveTemplates;
    [SerializeField] Transform spawnPoint;

    [SerializeField] float timeBtwnWave = 5f;
    [SerializeField] Text waveCountdownText;
    float countdown;

    Wave currentWave;
    Transform[] currentWavePath;
    int waveIndex = 0;
    GameManager gameManager;

    private void Start()
    {
        numberAliveEnemies = 0;
        countdown = timeBtwnWave;
        gameManager = GameManager.instance;
    }

    /// <summary>
    /// The next wave will be spawn after clearing
    /// the previous wave
    /// </summary>
    private void Update()
    {
        if (numberAliveEnemies > 0 ) { return; }

        if (waveIndex == waveTemplates.Length)
        {
            ClearStage();
            return;
        }

        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBtwnWave;
            return;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private void ClearStage()
    {
        gameManager.ClearStage();
        // this.enabled = false;
    }

    IEnumerator SpawnWave()
    {
        Debug.Log(numberAliveEnemies);
        currentWave = waveTemplates[waveIndex];

        SetupWavePath();

        numberAliveEnemies += currentWave.amountOfEnemies;

        waveIndex++;
        PlayerStats.rounds++;

        for (var index = 0; index < currentWave.amountOfEnemies; index ++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1/currentWave.spawnRate);
        }
        Debug.Log(numberAliveEnemies);
    }

    private void SetupWavePath()
    {
        GameObject wavePathGO = GameObject.Find(currentWave.waveName);

        if (!wavePathGO)
        {
            Debug.LogError("Please create wavePath with name " + currentWave.waveName);
            return;
        }

        currentWavePath = new Transform[wavePathGO.transform.childCount];

        for (var i = 0; i < currentWavePath.Length; i++)
        {
            currentWavePath[i] = wavePathGO.transform.GetChild(i);
        }
    }

    private void SpawnEnemy()
    {
        spawnPoint = currentWavePath[0];

        Enemy newEnemy = SimplePool.Spawn(currentWave.enemiesPrefab[UnityEngine.Random.Range(0, currentWave.enemiesPrefab.Length - 1)], spawnPoint.position, Quaternion.identity);
        //Instantiate(currentWave.enemiesPrefab[Random.Range(0, currentWave.enemiesPrefab.Length - 1)], spawnPoint.position, Quaternion.identity);
        WayPointEffector wpEffector = spawnPoint.GetComponentInChildren<WayPointEffector>();
        if (wpEffector)
        {
            wpEffector.ObjectStateChange();
        }

        newEnemy.GetComponent<EnemyMovement>().GetNewWave(currentWavePath);
    }
}

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
    int waveIndex = 0;
    GameManager gameManager;

    private void Start()
    {
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
        PlayerPrefs.SetInt("levelReached", gameManager.stageLevel + 1);
        // this.enabled = false;
    }

    IEnumerator SpawnWave()
    {
        currentWave = waveTemplates[waveIndex];
        numberAliveEnemies += currentWave.amountOfEnemies;

        waveIndex++;
        PlayerStats.rounds++;

        for (var index = 0; index < currentWave.amountOfEnemies; index ++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(1/currentWave.spawnRate);
        }
    }



    private void SpawnEnemy()
    {
        Instantiate(currentWave.enemiesPrefab[Random.Range(0, currentWave.enemiesPrefab.Length - 1)], spawnPoint);
    }
}

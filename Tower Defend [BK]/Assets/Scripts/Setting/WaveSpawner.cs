using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WaveSpawner : MonoBehaviour
{
    [SerializeField] Transform enemyPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] float timeBtwnWave = 5f;
    [SerializeField] Text waveCountdownText;
    float countdown = 2f;

    int waveIndex = 4;

    private void Update()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBtwnWave;
        }

        countdown -= Time.deltaTime;

        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    IEnumerator SpawnWave()
    {
        // waveIndex++;
        PlayerStats.rounds++;

        for (var index = 0; index < waveIndex; index ++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnEnemy()
    {
        Instantiate(enemyPrefab, spawnPoint);
    }
}

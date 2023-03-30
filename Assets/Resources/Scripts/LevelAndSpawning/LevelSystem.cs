using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private List<EnemyWave> enemyWaves; // A list of enemy waves
    [SerializeField] private float timeBetweenWaves; // The time in seconds between waves
    private int currentWaveIndex = 0; // The current wave index
    private bool isSpawning = false; // Whether or not enemies are currently spawning

    // Start the first wave when the script is enabled
    private void OnEnable()
    {
        StartNextWave();
    }

    // Update is called once per frame
    private void Update()
    {
        // If all enemies in the current wave have been killed, start the next wave
        if (!isSpawning && EnemyManager.Instance.GetActiveEnemiesCount() == 0)
        {
            StartCoroutine(StartNextWaveAfterDelay());
        }
    }

    // Start the next wave after a delay
    private IEnumerator StartNextWaveAfterDelay()
    {
        isSpawning = true;
        yield return new WaitForSeconds(timeBetweenWaves);
        StartNextWave();
    }

    // Start the next wave of enemies
    private void StartNextWave()
    {
        if (currentWaveIndex < enemyWaves.Count)
        {
            EnemyWave wave = enemyWaves[currentWaveIndex];
            StartCoroutine(EnemyManager.Instance.SpawnEnemiesInWave(wave));
            currentWaveIndex++;
        }
        else
        {
            // If there are no more waves, end the game or do something else
            Debug.Log("No more waves!");
        }
    }
}

[System.Serializable]
public class EnemyWave
{
    [SerializeField] private List<GameObject> enemies; // A list of enemy prefabs to spawn
    [SerializeField] private int numEnemies; // The number of enemies to spawn in this wave
    [SerializeField] private float timeBetweenSpawns; // The time in seconds between enemy spawns

}

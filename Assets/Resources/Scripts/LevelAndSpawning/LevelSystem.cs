using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to be instantiated
    public Transform[] spawnPoints; // Array of spawn points for the enemies
    public int enemiesPerLevel = 10; // Number of enemies to spawn per level
    public int currentLevel = 1; // Current level
    public float spawnDelay = 2f; // Delay between enemy spawns
    private int enemiesSpawned = 0; // Number of enemies spawned in the current level
    public float maxSpawnRadius = 10f; // Maximum distance from the spawn point that an enemy can spawn

    void Start()
    {
        SpawnEnemies();
    }

    void SpawnEnemies()
    {
        // Loop through each spawn point
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            // Instantiate a random enemy prefab at the current spawn point
            GameObject enemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)], spawnPoints[i].position, Quaternion.identity);

            // Set the enemy's name to include its type and spawn point index
            enemy.name = enemy.name + " " + i;

            // Increase the number of enemies spawned in the current level
            enemiesSpawned++;

            // If we've spawned the required number of enemies for this level, move on to the next level
            if (enemiesSpawned >= enemiesPerLevel)
            {
                currentLevel++;
                enemiesSpawned = 0;
                enemiesPerLevel += 5; // Increase the number of enemies required for the next level
                spawnDelay *= 0.9f; // Decrease the delay between enemy spawns for the next level
                StartCoroutine(DelayedSpawn()); // Call SpawnEnemies() again after a delay
                return;
            }
       
            // Generate a random point within a sphere around the current spawn point
            Vector3 randomPos = Random.insideUnitSphere * maxSpawnRadius + spawnPoints[i].position;

            // Clamp the random point to the maximum spawn distance from the spawn point
            randomPos = Vector3.ClampMagnitude(randomPos - spawnPoints[i].position, maxSpawnRadius) + spawnPoints[i].position;
        }

        // Call SpawnEnemies() again after a delay
        StartCoroutine(DelayedSpawn());
    }
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        SpawnEnemies();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to be instantiated
    public int enemiesPerLevel = 10; // Number of enemies to spawn per level
    public int currentLevel = 1; // Current level
    public float spawnDelay = 2f; // Delay between enemy spawns
    private int enemiesSpawned = 0; // Number of enemies spawned in the current level
    public float maxSpawnRadius = 10f; // Maximum distance from the spawn point that an enemy can spawn
    public List<int> SpawnEnemiesInWave_one = new List<int>();
    public List<int> SpawnEnemiesInWave_two = new List<int>() { 1, 1, 2, 1, 1, 2, 1, 1 };
    private int _enemyCounter = 0;
    // spawn area variables
    public Transform[] spawnpointPrefabs; // the prefab for the spawnpoints
    public int numberOfSpawnpoints; // how many spawnpoints to create
    public Vector3 spawnAreaCenter; // the center of the spawn area
    public float spawnAreaWidth; // the width of the spawn area
    public float spawnAreaDepth; // the depth of the spawn area


    void Start()
    {
        spawnpointPrefabs = new Transform[numberOfSpawnpoints];
         // loop through the number of spawnpoints to create
        for (int i = 0; i < numberOfSpawnpoints; i++)
        {
            // create a random position within the spawn area
            Vector3 spawnPosition = new Vector3(
                Random.Range(spawnAreaCenter.x - spawnAreaWidth / 2, spawnAreaCenter.x + spawnAreaWidth / 2),
                0,
                Random.Range(spawnAreaCenter.z - spawnAreaDepth / 2, spawnAreaCenter.z + spawnAreaDepth / 2)
            );

            // select a random spawnpoint prefab from the array
            //GameObject spawnpointPrefab = spawnpointPrefabs[Random.Range(0, spawnpointPrefabs.Length)].gameObject;

            // instantiate the selected spawnpoint prefab at the random position
            GameObject spawnpoint = Instantiate(new GameObject(), spawnPosition, Quaternion.identity);
            spawnpointPrefabs[i] = spawnpoint.transform;
            spawnpoint.transform.parent = transform;
        }
        _enemyCounter = 0;
        SpawnEnemies(SpawnEnemiesInWave_one, _enemyCounter);
    }
    

    void Update()
    {

    }

    void SpawnEnemies(List<int> wave, int counter)
    {
        // Loop through each spawn point
        for (int i = 0; i < spawnpointPrefabs.Length; i++)
        {
            Debug.Log("SpawnEnemies: " + counter);
            if(counter >= wave.Count)
            {
                return;
            }


            // Instantiate a random enemy prefab at the current spawn point
            GameObject enemy = Instantiate(enemyPrefabs[wave[counter]], spawnpointPrefabs[i].position, Quaternion.identity);

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
            Vector3 randomPos = Random.insideUnitSphere * maxSpawnRadius + spawnpointPrefabs[i].position;

            // Clamp the random point to the maximum spawn distance from the spawn point
            randomPos = Vector3.ClampMagnitude(randomPos - spawnpointPrefabs[i].position, maxSpawnRadius) + spawnpointPrefabs[i].position;
        }

        // Call SpawnEnemies() again after a delay
        StartCoroutine(DelayedSpawn());
    }
    IEnumerator DelayedSpawn()
    {
        yield return new WaitForSeconds(spawnDelay);
        //SpawnEnemies(SpawnEnemiesInWave_two, _enemyCounter++);
    }

    }
    
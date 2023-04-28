using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to be instantiated
    public float maxSpawnRadius = 10f; // Maximum distance from the spawn point that an enemy can spawn
    public List<int> SpawnEnemiesInWave_one = new List<int>() { 0, 0, 0, 0, 0 };
    public List<int> SpawnEnemiesInWave_two = new List<int>() { 1, 1, 2, 1, 1, 2, 1, 1 };
    public List<int> SpawnEnemiesInWave_three = new List<int>() { 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1 };
    public List<int> SpawnEnemiesInWave_four = new List<int>() { 1, 1, 2, 1, 1, 2, 1, 1, 2, 1, 1 };
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
        }
        _enemyCounter = 0;
    }


    void FixedUpdate()
    {
        //print(GameObject.FindObjectsOfType<Enemy>().Length);
        //Check how many ememies are alive
        //If none, spawn new wave
        if (GameObject.FindObjectsOfType<Enemy>().Length == 0)
        {
            // make a delay between spawns;
            if (_enemyCounter == 0)
            {
                SpawnEnemies(SpawnEnemiesInWave_one);
                _enemyCounter++;
            }
            else if (_enemyCounter == 1)
            {
                SpawnEnemies(SpawnEnemiesInWave_two);
                _enemyCounter++;
            }
            else
            {
                Debug.Log("No more waves");
            }
        }
    }

    void SpawnEnemies(List<int> wave)
    {
        //Loop through wave, and spawn at random spawnpoint

        foreach (int i in wave)
        {
            // makeing a delay between spawns
            Instantiate(enemyPrefabs[i], GetRandomPosition(), Quaternion.identity);
            
    }
    }
    //Get random spawnpoint
    private Vector3 GetRandomPosition()
    {
        // Generate a random point within a sphere around the current spawn point
        int randomSpawnpoint = Random.Range(0, spawnpointPrefabs.Length);
        print("Spawn point" + spawnpointPrefabs[randomSpawnpoint].position);
        Vector3 randomPos = Random.insideUnitSphere * maxSpawnRadius + spawnpointPrefabs[Random.Range(0, spawnpointPrefabs.Length)].position;
        

        print(randomPos);
        return randomPos;
    }
}

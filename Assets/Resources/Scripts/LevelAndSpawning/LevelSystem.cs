using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to be instantiated
    public float maxSpawnRadius = 10f; // Maximum distance from the spawn point that an enemy can spawn
    public List<int> SpawnEnemiesInWave_one = new List<int>() {}; // The number of enemies to spawn in the first wave, make this in the editor
    public List<int> SpawnEnemiesInWave_two = new List<int>() {}; 
    public List<int> SpawnEnemiesInWave_three = new List<int>() {};
    public List<int> SpawnEnemiesInWave_four = new List<int>() {};
    public List<int> SpawnEnemiesInWave_five = new List<int>() {};
    public List<int> SpawnEnemiesInWave_six = new List<int>() {};
    public List<int> SpawnEnemiesInWave_seven = new List<int>() {};
    public List<int> SpawnEnemiesInWave_eight = new List<int>() {};
    public List<int> SpawnEnemiesInWave_nine = new List<int>() {};
    public List<int> SpawnEnemiesInWave_ten = new List<int>() {};
    private int _enemyCounter = 0;
    private float _spawnTimerSinceLastWave = 0f;
    private float maxTime = 30f;

    // timer variables
    public bool timerActive = false;
    public float timer = 0f;
    public Text TimerTxt;
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
        timerActive = true;
    }


    void FixedUpdate()
    {
        _spawnTimerSinceLastWave += Time.deltaTime;
        updateTimer(timer);
         if (timerActive == true)
        {
             timer += Time.deltaTime;
        }
        
        //print(GameObject.FindObjectsOfType<Enemy>().Length);
        //Check how many ememies are alive
        //If none, spawn new wave
        if (GameObject.FindObjectsOfType<Enemy>().Length == 0 || _spawnTimerSinceLastWave >= maxTime)
        {
            // make a delay between spawns;
            if (_enemyCounter == 0)
            {
                SpawnEnemies(SpawnEnemiesInWave_one);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 1)
            {
                SpawnEnemies(SpawnEnemiesInWave_two);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 2)
            {
                SpawnEnemies(SpawnEnemiesInWave_three);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 3)
            {
                SpawnEnemies(SpawnEnemiesInWave_four);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 4)
            {
                SpawnEnemies(SpawnEnemiesInWave_five);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 5)
            {
                SpawnEnemies(SpawnEnemiesInWave_six);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 6)
            {
                SpawnEnemies(SpawnEnemiesInWave_seven);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 7)
            {
                SpawnEnemies(SpawnEnemiesInWave_eight);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 8)
            {
                SpawnEnemies(SpawnEnemiesInWave_nine);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 9)
            {
                SpawnEnemies(SpawnEnemiesInWave_ten);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else if (_enemyCounter == 10)
            {
                SpawnEnemies(SpawnEnemiesInWave_ten);
                _enemyCounter++;
                _spawnTimerSinceLastWave = 0f;
            }
            else
            {
                Debug.Log("No more waves, now entering endless mode");
                timerActive = false;
                // spawn from the last list in a random order

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
        randomPos.y = 0f;

        print(randomPos);
        return randomPos;
    }
    void updateTimer(float currentime)
    {
        currentime += 1;

        float minutes = Mathf.FloorToInt(currentime / 60);
        float seconds = Mathf.FloorToInt(currentime % 60);

        TimerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}

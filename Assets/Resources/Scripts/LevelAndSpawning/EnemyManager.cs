using UnityEngine;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; // Singleton instance of the class

    public Dictionary<int, List<int>> Waves = new Dictionary<int, List<int>>();
    public List<int> SpawnEnemiesInWave_one = new List<int>() {1, 1, 2, 1, 1 };
    public List<int> SpawnEnemiesInWave_two = new List<int>() { 1, 1, 2, 1, 1, 2, 1, 1 };

    public int currentLevel = 1; // Current level

    public Player p;
    public GameObject Ranger; // The first enemy prefab
    public GameObject HighLander;

    public int maxEnemies = 10; // The maximum number of active enemies

    private List<GameObject> activeEnemies = new List<GameObject>(); // A list of currently active enemies

    // Initialize the singleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }


    public void SpawnEnemy(int enemyType)
    {
        if (activeEnemies.Count < maxEnemies)
        {
            GameObject prefab;
            switch (enemyType)
            {
                case 1:
                    prefab = Ranger;
                    break;
                case 2:
                    prefab = HighLander;
                    break;
                default:
                    prefab = Ranger;
                    break;
            }
            GameObject enemy = Instantiate(prefab, GetRandomPosition(), Quaternion.identity);
            activeEnemies.Add(enemy);
        }
    }
    // Spawn an enemy after a delay
    public IEnumerator<WaitForSeconds> SpawnEnemyAfterDelay(GameObject prefab, float delay)
    {
        yield return new WaitForSeconds(delay);
        if (SpawnEnemiesInWave_one.Count > 0)
        {
            int enem = SpawnEnemiesInWave_one[0];
            SpawnEnemy(enem);
            SpawnEnemiesInWave_one.RemoveAt(0);

        }

    }

    // Despawn an enemy
    public void DespawnEnemy(GameObject enemy)
    {
        if (activeEnemies.Contains(enemy))
        {
            activeEnemies.Remove(enemy);
            Destroy(enemy);
        }
    }

    // Get a random position within a circle around the player
    private Vector3 GetRandomPosition()
    {
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(5f, 10f);
        Vector3 position = p.transform.position + new Vector3(Mathf.Cos(angle), 0f, Mathf.Sin(angle)) * distance;
        position.y = 0f;
        return position;
    }

    // Get the number of active enemies
    public int GetActiveEnemiesCount()
    {
        return activeEnemies.Count;
    }

}

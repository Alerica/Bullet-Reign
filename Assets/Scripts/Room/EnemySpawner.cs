using UnityEngine;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawning Settings")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private int minEnemies = 2;
    [SerializeField] private int maxEnemies = 5;
    [SerializeField] private Transform[] spawnPoints; 

    public List<GameObject> SpawnEnemies(Room room)
    {
        List<GameObject> spawnedEnemies = new List<GameObject>();
        int enemyCount = Random.Range(minEnemies, maxEnemies + 1);

        for (int i = 0; i < enemyCount; i++)
        {
            if (spawnPoints.Length == 0 || enemyPrefabs.Length == 0) return spawnedEnemies;

            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            GameObject randomEnemy = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];

            GameObject enemyInstance = Instantiate(randomEnemy, randomSpawnPoint.position, Quaternion.identity);
            enemyInstance.GetComponent<EnemyAI>()?.SetRoom(room); 
            spawnedEnemies.Add(enemyInstance);
        }

        return spawnedEnemies;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempSpawner1 : MonoBehaviour
{
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private int maxEnemies = 15;
    [SerializeField] private float spawnDelay = 2f;
    [SerializeField] private int enemiesPerIncrease = 2;
    [SerializeField] private float minY = -0.27f;
    [SerializeField] private float maxY = 2.97f;

    private int currentEnemies = 0;
    private float nextSpawnTime = 0f;

    private void Start()
    {
        nextSpawnTime = Time.time + spawnDelay;
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnDelay;
        }
    }

    private void SpawnEnemy()
    {
        GameObject randomEnemyPrefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        GameObject enemy = Instantiate(randomEnemyPrefab, spawnPosition, Quaternion.identity);

        if (currentEnemies % enemiesPerIncrease == 0 && currentEnemies < maxEnemies)
        {
            currentEnemies += enemiesPerIncrease;
        }
    }
}

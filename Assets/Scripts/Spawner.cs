using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Slider UpgradeSlider;
    [SerializeField] private List<Enemy> enemyPrefabs;
    [SerializeField] private int maxEnemies;
    [SerializeField] private float forBigMinY = -0.27f;
    [SerializeField] private float forBigMaxY = 2.97f;    
    [SerializeField] private float forSmallMinY = -0.27f;
    [SerializeField] private float forSmallMaxY = -1.9f;
    [SerializeField] private int enemiesKillCountToOpenPanel;
    [SerializeField] SkillPanel panel;

    [SerializeField] TMP_Text WaveNumberText;

    //[SerializeField] private float _secondsBetweenSpawn;
    public static int WaveNumber = 1;

    private int currentEnemies = 0;
    private float nextSpawnTime = 5f;
    private float _elapsedTime = 0;
    private bool isGolemSpawned = false;
    private int enemyKillCount;
    private bool isWaiting;

    private void Start()
    {
        _elapsedTime = nextSpawnTime;
        isWaiting = false;
        if (PlayerPrefs.GetInt("WaveNumber") > 1)
        {
            WaveNumber = PlayerPrefs.GetInt("WaveNumber");
            IncreaseEnemyPower(WaveNumber);
        }

        WaveNumberText.text += WaveNumber;
        UpgradeSlider.minValue = 0;
        UpgradeSlider.maxValue = enemiesKillCountToOpenPanel;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (!isWaiting) {
            if (_elapsedTime >= nextSpawnTime)
            {
                if (currentEnemies < maxEnemies && !isGolemSpawned)
                {
                    int enemyIndex = Random.Range(1, 11);

                    if (enemyIndex >= 9)
                    {
                        SpawnEnemy(2, forBigMinY, forBigMaxY); //SHIELD
                        currentEnemies++;
                        _elapsedTime = 0;
                    }

                    if (enemyIndex <= 8 && enemyIndex > 6)
                    {
                        SpawnEnemy(1, forSmallMinY, forSmallMaxY); //FLYING
                        currentEnemies++;
                        _elapsedTime = 0;
                    }

                    if (enemyIndex <= 6)
                    {
                        SpawnEnemy(0, forSmallMinY, forSmallMaxY); //FAST
                        currentEnemies++;
                        _elapsedTime = 0;
                    }
                }
                else
                {
                    SpawnEnemy(3, forBigMinY, forBigMaxY); //BOSS
                    isGolemSpawned = true;
                    currentEnemies++;
                    _elapsedTime = 0;
                    isWaiting = true;
                    Debug.Log(isWaiting);
                }
            }

            if (enemyKillCount >= enemiesKillCountToOpenPanel)
            {
                ShowAbilityPanel();
                enemyKillCount = 0;
                UpgradeSlider.value = enemyKillCount;
            }

            //Debug.Log("enemyKillCount: " + enemyKillCount);
        }
    }

    public void IncreaseKilledEnemyCount()
    {
        enemyKillCount++;
        UpgradeSlider.value = enemyKillCount;
    }

    public void ShowAbilityPanel()
    {
        panel.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void IncreaseEnemyPower()
    {
        foreach (var enemy in enemyPrefabs)
        {
            enemy.IncreasePower();
        }
    }

    public void IncreaseEnemyPower(int waveNumber)
    {
        for (int i = 1; i < waveNumber; i++)
        {
            foreach (var enemy in enemyPrefabs)
            {
                enemy.IncreasePower();
            }

            if (i % 5 == 0)
            {
                maxEnemies++;
            }
        }
    }

    private void SpawnEnemy(int number, float minY, float maxY)
    {
        GameObject enemyPrefab = enemyPrefabs[number].gameObject;
        float randomY = Random.Range(minY, maxY);
        Vector3 spawnPosition = new Vector3(transform.position.x, randomY, transform.position.z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    private GameObject GetPrefabByTag(string tag)
    {
        foreach (Enemy prefab in enemyPrefabs)
        {
            if (prefab.CompareTag(tag))
            {
                return prefab.gameObject;
            }
        }

        return null;
    }

    public void NewWave()
    {
        isGolemSpawned = false;
        IncreaseEnemyPower();
        currentEnemies = 0;
        WaveNumber++;

        if(WaveNumber % 5 == 0)
        {
            PlayerPrefs.SetInt("WaveNumber", WaveNumber);
            maxEnemies++;
        }

        isWaiting = true;
        StartCoroutine(NewWaveTimer());

        WaveNumberText.text = "Волна №" + WaveNumber;
    }

    IEnumerator NewWaveTimer()
    {
        yield return new WaitForSeconds(3.0F);
        isWaiting = false;
    }
}
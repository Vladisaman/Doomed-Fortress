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
    private List<Enemy> AllowedEnemies;
    [SerializeField] private int maxEnemies;
    [SerializeField] private int tempMaxEnemies;
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
    public float nextSpawnTime = 3f;
    private float _elapsedTime = 0;
    private bool isGolemSpawned = false;
    private int enemyKillCount;
    public bool isWaiting;
    public static bool isBossAlive;

    public static PlayerData playerData;

    private void Awake()
    {
        AllowedEnemies = new List<Enemy>();
        ChangeEnemies();
        isBossAlive = false;

        if (!isWaiting)
        {
            string json = System.IO.File.ReadAllText(CurrencyManager.filePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);

            if (playerData.skillAmount > 0)
            {
                panel.gameObject.SetActive(true);
                panel.GiveSkills(0);
                Time.timeScale = 0;
            }
        }
    }

    private void Start()
    {
        _elapsedTime = nextSpawnTime;
        if (playerData.waveNumber > 1)
        {
            WaveNumber = playerData.waveNumber;
            IncreaseEnemyPower(WaveNumber);
        }

        WaveNumberText.text += WaveNumber;
        UpgradeSlider.minValue = 0;
        UpgradeSlider.maxValue = enemiesKillCountToOpenPanel;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            SpawnEnemy(0, forSmallMinY, forSmallMaxY); //FAST
        }
        if (Input.GetKeyDown(KeyCode.F2))
        {
            SpawnEnemy(1, forSmallMinY, forSmallMaxY); //FLYING
        }
        if (Input.GetKeyDown(KeyCode.F3))
        {
            SpawnEnemy(2, forBigMinY, forBigMaxY); //SHIELD
        }
        if (Input.GetKeyDown(KeyCode.F4))
        {
            SpawnEnemy(3, forBigMinY, forBigMaxY); //GOLEM
        }
        if (Input.GetKeyDown(KeyCode.F5))
        {
            SpawnEnemy(4, forSmallMinY, forSmallMaxY); //ICE SKELETON
        }
        if (Input.GetKeyDown(KeyCode.F6))
        {
            SpawnEnemy(5, forSmallMinY, forSmallMaxY); //POISON TREANT
        }
        if (Input.GetKeyDown(KeyCode.F7))
        {
            SpawnEnemy(6, forSmallMinY, forSmallMaxY); //BOSS
        }

        _elapsedTime += Time.deltaTime;
        if (!isWaiting)
        {
            if (_elapsedTime >= nextSpawnTime)
            {
                if (WaveNumber != 50)
                {
                    if (currentEnemies < maxEnemies && !isGolemSpawned)
                    {
                        int enemyIndex = Random.Range(0, AllowedEnemies.Count);
                        SpawnEnemy(enemyIndex, forBigMinY, forBigMaxY);
                        currentEnemies++;
                        _elapsedTime = 0;
                    }
                    else
                    {
                        SpawnEnemy(3, forBigMinY, forBigMaxY); //GOLEM
                        isGolemSpawned = true;
                        currentEnemies++;
                        _elapsedTime = 0;
                        isWaiting = true;
                        Debug.Log(isWaiting);
                    }
                }
                else if (WaveNumber == 100)
                {
                    if (isBossAlive == false)
                    {
                        SpawnEnemy(6, 0, 0); //BOSS
                        isBossAlive = true;
                    }
                    else
                    {
                        int enemyIndex = Random.Range(1, AllowedEnemies.Count+1);
                        SpawnEnemy(enemyIndex, forBigMinY, forBigMaxY);
                        currentEnemies++;
                        _elapsedTime = 0;
                    }
                }

            }

            if (enemyKillCount >= enemiesKillCountToOpenPanel)
            {
                ShowAbilityPanel();
                playerData.increaseSkillAmount();
                enemyKillCount = 0;
                UpgradeSlider.value = enemyKillCount;
            }

            //Debug.Log("enemyKillCount: " + enemyKillCount);
        }
    }

    public void BossKilled()
    {
        isWaiting = true;
    }

    public void IncreaseKilledEnemyCount()
    {
        enemyKillCount++;
        UpgradeSlider.value = enemyKillCount;
    }

    public void ShowAbilityPanel()
    {
        panel.gameObject.SetActive(true);
        panel.CreateSkills();
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
            playerData.setWaveNumber(WaveNumber);
            maxEnemies++;
        }

        ChangeEnemies();

        isWaiting = true;
        StartCoroutine(NewWaveTimer());

        WaveNumberText.text = "Волна №" + WaveNumber;
    }

    IEnumerator NewWaveTimer()
    {
        yield return new WaitForSeconds(3.0F);
        isWaiting = false;
    }

    private void OnDestroy()
    {
        string json = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(CurrencyManager.filePath, json);
    }
    private void ChangeEnemies()
    {
        AllowedEnemies.Clear();
        AllowedEnemies.Capacity = 0;

        switch (WaveNumber)
        {
            case 1 or 2 or 3 or 4 or 5:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                break;
            case 6 or 7 or 8 or 9 or 10:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
            case 11 or 12 or 13 or 14 or 15:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
            case 16 or 17 or 18 or 19 or 20:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                break;
            case 21 or 22 or 23 or 24 or 25:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                AllowedEnemies.Add(enemyPrefabs[4]);
                break;
            case 26 or 27 or 28 or 29 or 30:
                AllowedEnemies.Add(enemyPrefabs[2]);
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
            case 31 or 32 or 33 or 34 or 35:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[4]);
                break;
            case 36 or 37 or 38 or 39 or 40:
                AllowedEnemies.Add(enemyPrefabs[2]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
            case 41 or 42 or 43 or 44 or 45:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                AllowedEnemies.Add(enemyPrefabs[2]);
                AllowedEnemies.Add(enemyPrefabs[4]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
            case 46 or 47 or 48 or 50:
                AllowedEnemies.Add(enemyPrefabs[0]);
                AllowedEnemies.Add(enemyPrefabs[1]);
                AllowedEnemies.Add(enemyPrefabs[2]);
                AllowedEnemies.Add(enemyPrefabs[3]);
                AllowedEnemies.Add(enemyPrefabs[4]);
                AllowedEnemies.Add(enemyPrefabs[5]);
                break;
        }
    }
}
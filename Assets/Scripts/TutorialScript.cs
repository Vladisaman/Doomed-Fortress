using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private Slider UpgradeSlider;
    [SerializeField] TMP_Text text;
    [SerializeField] Button nextButton;
    [SerializeField] private int enemiesKillCountToOpenPanel;
    [SerializeField] public int currEnemiesKillCount;
    [SerializeField] Player player;
    private int currIndex;
    TutorialText tutText;

    [SerializeField] Mortar mortar;
    [SerializeField] Crossbow crossbow;
    [SerializeField] FireGun firegun;

    [SerializeField] GameObject[] TestWave1;
    [SerializeField] GameObject[] TestWave2;

    [SerializeField] Transform mobSpawnPos;
    [SerializeField] private List<Enemy> enemyPrefabs;
    private UI ui;
    [SerializeField] SkillPanel panel;

    public int currentMobKillAmount;
    public int targetMobKillAmount;

    bool panelOnce;

    void Start()
    {
        panelOnce = false;
        ui = GetComponent<UI>();
        currIndex = 0;
        tutText = new TutorialText();
        text.text = tutText.tutorialText[currIndex];
        enemiesKillCountToOpenPanel = 20;

        mortar.gameObject.SetActive(false);
        crossbow.gameObject.SetActive(false);
        firegun.gameObject.SetActive(false);

        UpgradeSlider.minValue = 0;
        UpgradeSlider.maxValue = enemiesKillCountToOpenPanel;
    }

    private void Update()
    {
        if(targetMobKillAmount != 0)
        {
            nextButton.gameObject.SetActive(false);

            if(currentMobKillAmount == targetMobKillAmount)
            {
                nextButton.gameObject.SetActive(true);
                targetMobKillAmount = 0;
                currentMobKillAmount = 0;
            }
        }

        UpgradeSlider.value = currEnemiesKillCount;

        if (!panelOnce && currEnemiesKillCount >= enemiesKillCountToOpenPanel)
        {
            ShowAbilityPanel();
            panelOnce = true;
        }
    }

    public void Next()
    {
        currIndex++;
        text.text = tutText.tutorialText[currIndex];

        switch (currIndex)
        {
            case 4:
                HideAllButCrossbow();
                break;
            case 5 or 9:
                SpawnTargets();
                targetMobKillAmount = 4;
                break;
            case 7:
                SpawnTargets2();
                targetMobKillAmount = 4;
                break;
            case 6:
                HideAllButMortar();
                break;
            case 8:
                HideAllButFiregun();
                break;
            case 10:
                mortar.gameObject.SetActive(true);
                crossbow.gameObject.SetActive(true);
                firegun.gameObject.SetActive(true);
                player.canSwap = true;
                break;
            case 11:
                StartCoroutine("SpawnFakeWave1");
                targetMobKillAmount = 8;
                break;
            case 12:
                StartCoroutine("SpawnFakeWave2");
                targetMobKillAmount = 12;
                break;
            case 14:
                ui.ToMainMenu();
                break;
        }
    }

    public void SpawnTargets()
    {
        List<GameObject> objects = new List<GameObject>();

        objects.Add(Instantiate(enemyPrefabs[0], new Vector2(0, 2.6f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[1], new Vector2(-1.19f, -3.05f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[2], new Vector2(2.61f, 1.31f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[3], new Vector2(1.32f, -1.07f), Quaternion.identity).gameObject);

        foreach(GameObject obj in objects)
        {
            obj.GetComponent<Enemy>().speed = 0;
            obj.GetComponent<Animator>().enabled = false;
            obj.AddComponent<TutorialTargetScript>().script = this;
        }
    }
    public void SpawnTargets2() //для мортиры
    {
        List<GameObject> objects = new List<GameObject>();

        objects.Add(Instantiate(enemyPrefabs[0], new Vector2(0, 2.6f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[1], new Vector2(-1.19f, -3.05f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[2], new Vector2(2.61f, 1.31f), Quaternion.identity).GetComponent<Enemy>().gameObject);
        objects.Add(Instantiate(enemyPrefabs[1], new Vector2(1.32f, -1.07f), Quaternion.identity).gameObject);

        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Enemy>().speed = 0;
            obj.GetComponent<Animator>().enabled = false;
            obj.AddComponent<TutorialTargetScript>().script = this;
        }
    }
    public void HideAllButCrossbow()
    {
        firegun.gameObject.transform.position = new Vector2(-8, 3.5f);
        crossbow.gameObject.transform.position = new Vector2(-7, 0);
        mortar.gameObject.transform.position = new Vector2(-8, -3.5f);

        player.topGunObj = firegun.gameObject;
        player.midGunObj = crossbow.gameObject;
        player.botGunObj = mortar.gameObject;

        player.topGun = Player.Weapon.FireGun;
        player.midGun = Player.Weapon.Crossbow;
        player.botGun = Player.Weapon.Mortar;

        player.canSwap = false;

        mortar.gameObject.SetActive(false);
        crossbow.gameObject.SetActive(true);
        firegun.gameObject.SetActive(false);
    }
    public void HideAllButMortar()
    {
        firegun.gameObject.transform.position = new Vector2(-8, 3.5f);
        mortar.gameObject.transform.position = new Vector2(-7, 0);
        crossbow.gameObject.transform.position = new Vector2(-8, -3.5f);


        player.topGunObj = firegun.gameObject;
        player.midGunObj = mortar.gameObject;
        player.botGunObj = crossbow.gameObject;

        player.midGun = Player.Weapon.Mortar;
        player.topGun = Player.Weapon.FireGun;
        player.botGun = Player.Weapon.Crossbow;

        player.canSwap = false;

        mortar.gameObject.SetActive(true);
        crossbow.gameObject.SetActive(false);
        firegun.gameObject.SetActive(false);
    }
    public void HideAllButFiregun()
    {
        crossbow.gameObject.transform.position = new Vector2(-8, 3.5f);
        firegun.gameObject.transform.position = new Vector2(-7, 0);
        mortar.gameObject.transform.position = new Vector2(-8, -3.5f);

        player.topGunObj = crossbow.gameObject;
        player.midGunObj = firegun.gameObject;
        player.botGunObj = mortar.gameObject;

        player.topGun = Player.Weapon.Crossbow;
        player.midGun = Player.Weapon.FireGun; 
        player.botGun = Player.Weapon.Mortar;

        player.canSwap = false;

        mortar.gameObject.SetActive(false);
        crossbow.gameObject.SetActive(false);
        firegun.gameObject.SetActive(true);
    }
    public IEnumerator SpawnFakeWave1()
    {
        for (int i = 0; i < TestWave1.Length; i++)
        {
            Instantiate(TestWave1[i], new Vector3(mobSpawnPos.position.x, Random.Range(-0.27f, 2.97f), mobSpawnPos.position.z), Quaternion.identity).AddComponent<TutorialTargetScript>().script = this;
            yield return new WaitForSeconds(3.0f);
        }
    }
    public IEnumerator SpawnFakeWave2()
    {
        for (int i = 0; i < TestWave2.Length; i++)
        {
            Instantiate(TestWave2[i], new Vector3(mobSpawnPos.position.x, Random.Range(-0.27f, 2.97f), mobSpawnPos.position.z), Quaternion.identity).AddComponent<TutorialTargetScript>().script = this;
            yield return new WaitForSeconds(3.0f);
        }
    }
    public void ShowAbilityPanel()
    {
        panel.gameObject.SetActive(true);
        panel.CreateTutorialSkills();
        Time.timeScale = 0;
    }
}
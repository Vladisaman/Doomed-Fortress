using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PrototypeManager : MonoBehaviour
{
    
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] GameObject movingEnemyPrefab;
    [SerializeField] GameObject prototypeMenu;


    
    public void HidePrototypeMenu()
    {
        prototypeMenu.SetActive(false);
        Time.timeScale = 1.0f;
    }

    public void ShowPrototypeMenu()
    {
        prototypeMenu.SetActive(true);
        Time.timeScale = 0f;
    }




    public void SpawnThreeEnemies()
    {
        Instantiate(enemyPrefab, new Vector3(2f, 0f, 0), Quaternion.identity);
        Instantiate(enemyPrefab, new Vector3(2f, 3f, 0), Quaternion.identity);
        Instantiate(enemyPrefab, new Vector3(2f, -3f, 0), Quaternion.identity);
    }

    public void SpawnMovingEnemy()
    {
        Instantiate(movingEnemyPrefab, new Vector3(5f, 0f, 0), Quaternion.identity);
        
    }

    public void ResetGame()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(0);
    }


}

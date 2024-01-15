using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    [SerializeField] Scene panelScene;
    private void Start()
    {
        Time.timeScale = 0;
    }

    public void CloseStory1()
    {
        PlayerPrefs.SetInt("ClosedStory", 0);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("ClosedStory1", 1);
        gameObject.SetActive(false);
    }
    public void CloseStory2()
    {
        PlayerPrefs.SetInt("ClosedStory", 1);
        Time.timeScale = 1;
        PlayerPrefs.SetInt("ClosedStory2", 1);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && panelScene == Scene.zero)
        {
            if (PlayerPrefs.GetInt("ClosedStory1") == 1)
            {
                gameObject.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 && panelScene == Scene.one)
        {
            if (PlayerPrefs.GetInt("ClosedStory2") == 1)
            {
                gameObject.SetActive(false);
            }
        }
    }
}

enum Scene {
    zero,
    one
}
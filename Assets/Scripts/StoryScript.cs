using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    [SerializeField] Scene panelScene;

    public void CloseStory1()
    {
        PlayerPrefs.SetInt("ClosedStory", 0);
        gameObject.SetActive(false);
    }
    public void CloseStory2()
    {
        PlayerPrefs.SetInt("ClosedStory", 1);
        gameObject.SetActive(false);
    }

    private void Awake()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0 && panelScene == Scene.zero)
        {
            if (PlayerPrefs.GetInt("ClosedStory") == 0)
            {
                gameObject.SetActive(false);
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 1 && panelScene == Scene.one)
        {
            if (PlayerPrefs.GetInt("ClosedStory") == 1)
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
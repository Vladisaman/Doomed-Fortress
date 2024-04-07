using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    public int SceneNumber;

    public void CloseStory(StoryScript story)
    {
        PlayerPrefs.SetInt("ClosedStory" + story.SceneNumber, 1);
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    private void Awake()
    {
        if(PlayerPrefs.GetInt("ClosedStory" + SceneNumber) == 1)
        {
            Time.timeScale = 1;
            gameObject.SetActive(false);
        } 
        else
        {
            Time.timeScale = 0;
            gameObject.SetActive(true);
        }
    }
}
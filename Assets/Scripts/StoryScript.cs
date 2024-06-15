using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class StoryScript : MonoBehaviour
{
    public int WaveRequired;
    public bool isRelevant;
    private Image image;
    [SerializeField] private GameObject text;

    public void CloseStory(StoryScript story)
    {
        PlayerPrefs.SetInt("ClosedStory" + WaveRequired, 1);
        Time.timeScale = 1;
        image.enabled = false;
        text.SetActive(false);
        isRelevant = false;
    }

    private void Awake()
    {
        image = GetComponent<Image>();
        isRelevant = true;
        image.enabled = false;
        text.SetActive(false);

        if (WaveRequired == -1)
        {
            image.enabled = true;
            text.SetActive(true);
        }
        if (WaveRequired == 0)
        {
            image.enabled = true;
            text.SetActive(true);
            Time.timeScale = 0;
        }

        if (PlayerPrefs.GetInt("ClosedStory" + WaveRequired) == 1)
        {
            Time.timeScale = 1;
            image.enabled = false;
            text.SetActive(false);
            isRelevant = false;
        }
    }

    private void FixedUpdate()
    {
        if (isRelevant)
        {
            if (Spawner.WaveNumber == WaveRequired)
            {
                image.enabled = true;
                text.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
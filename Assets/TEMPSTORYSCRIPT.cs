using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEMPSTORYSCRIPT : MonoBehaviour
{
    [SerializeField] GameObject StoryPanel;

    private void Update()
    {
        if(Spawner.WaveNumber == 10)
        {
            Time.timeScale = 0;
            StoryPanel.SetActive(true);
        }
    }

    public void CloseStory3()
    {
        StoryPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

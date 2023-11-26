using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoluemValue : MonoBehaviour
{
    private AudioSource audioScr;
    [SerializeField] private Slider SoundSlider;

    private void Awake()
    {
        audioScr = GetComponent<AudioSource>();
        try
        {
            float volume = PlayerPrefs.GetFloat("Volume");
            audioScr.volume = volume;
            SoundSlider.value = volume;
        }
        catch
        { }
    }

    void Update()
    {
        if (SoundSlider.gameObject.activeSelf)
        {
            audioScr.volume = SoundSlider.value;
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Volume", audioScr.volume);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat("Volume", audioScr.volume);
    }
}

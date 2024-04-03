using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using UnityEngine;
using System.Collections.Generic;

public class UI : MonoBehaviour
{
    [SerializeField] private bool isPauseActive = false;
    [SerializeField] private GameObject pauseObject;

    private DateTime SessionTimeMetric;

    private CurrencyManager _currencyManager;

    private void Start()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();
    }

    public void ToMainMenu()
    {
        isPauseActive = false;
        Time.timeScale = 1;

        string json = JsonUtility.ToJson(Spawner.playerData);
        System.IO.File.WriteAllText(CurrencyManager.filePath, json);

        TimeSpan secondsInGame = SessionTimeMetric - DateTime.Now;
        Dictionary<string, object> parameters = new Dictionary<string, object>() { { "time_seconds", secondsInGame.TotalSeconds } };
        AppMetrica.Instance.ReportEvent("session_play_time", parameters);
        AppMetrica.Instance.SendEventsBuffer();

        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        AppMetrica.Instance.ReportEvent("application_exit_button_click");
        AppMetrica.Instance.SendEventsBuffer();

        Application.Quit();
    }
    public void StartGame()
    {
        //if (_currencyManager.UseEnergy())
        //{
        SceneManager.LoadScene(1);

        AppMetrica.Instance.ReportEvent("start_game_button_click");
        AppMetrica.Instance.SendEventsBuffer();
        //}
    }
    public void RestartGame()
    {
        //if (_currencyManager.UseEnergy())
        //{
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        AppMetrica.Instance.ReportEvent("restart_game_button_click");
        AppMetrica.Instance.SendEventsBuffer();
        //}
    }

    public void OpenPauseMenu()
    {
        if (!isPauseActive)
        {
            pauseObject.SetActive(true);
            isPauseActive = true;
            Time.timeScale = 0;

            AppMetrica.Instance.PauseSession();
        }
        else
        {
            pauseObject.SetActive(false);
            isPauseActive = false;
            Time.timeScale = 1;

            AppMetrica.Instance.ResumeSession();
        }
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void SettingsMenuMetric()
    {
        AppMetrica.Instance.ReportEvent("open_settings_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void ShopMetric()
    {
        AppMetrica.Instance.ReportEvent("open_shop_button_click");
        AppMetrica.Instance.SendEventsBuffer();
    }

    public void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
        CurrencyManager.coins = 0;

        System.IO.File.Delete(CurrencyManager.filePath);
    }
}

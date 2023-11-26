using System.Collections;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] public float MinsForEnergy;
    //[SerializeField] public int EnergyPerTick;
    //[SerializeField] public int MaxEnergy;
    //[SerializeField] public int EnergyForRun;
    //public int CurrEnergy;
    [SerializeField] private int coins;
    private Coroutine myCoroutine;
    //EnergyTextUpdater _energyTextUpdated;

    private DateTime SessionTimeMetric;

    private void Awake()
    {
        SessionTimeMetric = DateTime.Now;

        //_energyTextUpdated = FindObjectOfType<EnergyTextUpdater>();
        string appExitTimeStr = PlayerPrefs.GetString("AppExitTime", "");
        //CurrEnergy = PlayerPrefs.GetInt("AppExitEnergy");
        if (!string.IsNullOrEmpty(appExitTimeStr))
        {
            DateTime appExitTime = DateTime.Parse(appExitTimeStr);
            TimeSpan timeSpentOutside = DateTime.Now - appExitTime;
            //CurrEnergy += (int)((int)timeSpentOutside.TotalMinutes / MinsForEnergy * EnergyPerTick);
        }
        //BalanceEnergy();
    }

    private void Start()
    {
        var otherCurrencyManagers = FindObjectsOfType<CurrencyManager>();
        if(otherCurrencyManagers.Length > 1)
        {
            Debug.LogError("currencyManager error log");
            Destroy(this.gameObject);
        }
        //myCoroutine = StartCoroutine(MyCoroutine());
        DontDestroyOnLoad(gameObject);
    }

    //public void StopMyCoroutine()
    //{
    //    if (myCoroutine != null)
    //    {
    //        StopCoroutine(myCoroutine);
    //        myCoroutine = null;
    //    }
    //}

    //IEnumerator MyCoroutine()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(MinsForEnergy * 60);
    //        if (CurrEnergy + EnergyPerTick <= MaxEnergy)
    //        {
    //            CurrEnergy += EnergyPerTick;
    //        }
    //        else
    //        {
    //            CurrEnergy = MaxEnergy;
    //        }
    //        _energyTextUpdated.UpdateText();
    //    }
    //}

    //public int GetEnergy()
    //{
    //    return CurrEnergy;
    //}

    //public bool UseEnergy()
    //{
    //    if (CurrEnergy >= EnergyForRun)
    //    {
    //        CurrEnergy -= EnergyForRun;
    //        _energyTextUpdated.UpdateText();
    //        return true;
    //    } else
    //    {
    //        return false;
    //    }
    //}

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            //PlayerPrefs.SetString("AppExitTime", DateTime.Now.ToString());
            //PlayerPrefs.SetInt("AppExitEnergy", CurrEnergy);
            PlayerPrefs.SetInt("AppExitCoins", coins);
        }
        else
        {
            string appExitTimeStr = PlayerPrefs.GetString("AppExitTime", "");
            //CurrEnergy = PlayerPrefs.GetInt("AppExitEnergy");
            if (!string.IsNullOrEmpty(appExitTimeStr))
            {
                DateTime appExitTime = DateTime.Parse(appExitTimeStr);
                TimeSpan timeSpentOutside = DateTime.Now - appExitTime;
                //CurrEnergy += (int)(timeSpentOutside.TotalSeconds / (MinsForEnergy * 60)) * EnergyPerTick;
                //BalanceEnergy();
                //_energyTextUpdated.UpdateText();
            }
            coins = PlayerPrefs.GetInt("AppExitCoins");
        }
    }

    private void OnApplicationQuit()
    {
        PlayerPrefs.SetString("AppExitTime", DateTime.Now.ToString());
        //PlayerPrefs.SetInt("AppExitEnergy", CurrEnergy);
        PlayerPrefs.SetInt("AppExitCoins", coins);
    }

    public void AddCoins(int coinsToAdd)
    {
        coins += coinsToAdd;
    }

    public void SpendCoins(int spentCoins)
    {
        coins -= spentCoins;
    }

    public int GetCoins()
    {
        return coins;
    }

    //public void BalanceEnergy()
    //{
    //    if (CurrEnergy >= MaxEnergy)
    //    {
    //        CurrEnergy = MaxEnergy;
    //    }
    //    if(CurrEnergy < 0)
    //    {
    //        CurrEnergy = 0;
    //    }
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScript : MonoBehaviour
{
    [SerializeField] UpgradesMailmanSO mailman;
    [SerializeField] ShopUI shop;

    private void Start()
    {
        Setup();
    }

    public void Setup()
    {
        if (System.IO.File.Exists(CurrencyManager.filePath) == false)
        {
            PlayerData data = new PlayerData(8, 10, 1, 9, 10, 1, 7, 10, 1);
            string jayson = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(CurrencyManager.filePath, jayson);
            Debug.Log("BALLS");
        }

        string json = System.IO.File.ReadAllText(CurrencyManager.filePath);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(json);

        mailman.CrossbowDamageLevel = playerData.crossbowLevel == 0 ? 1 : playerData.crossbowLevel;
        mailman.MortarDamageLevel = playerData.mortarLevel == 0 ? 1 : playerData.mortarLevel;
        mailman.FiregunDamageLevel = playerData.firegunLevel == 0 ? 1 : playerData.firegunLevel;

        mailman.CrossbowDamage = playerData.crossbowDamage == 0 ? 8 : playerData.crossbowDamage;
        mailman.MortarDamage = playerData.mortarDamage == 0 ? 9 : playerData.mortarDamage;
        mailman.FiregunDamage = playerData.firegunDamage == 0 ? 7 : playerData.firegunDamage;
                  
        shop.SetPrices(playerData.crossbowCost, playerData.mortarCost, playerData.firegunCost);

        mailman.isCrossbowAbilityBought = PlayerPrefs.GetInt("CrossbowAbility", 0) == 1? true : false;
        mailman.isMortarAbilityBought = PlayerPrefs.GetInt("MortarAbility", 0) == 1 ? true : false;
        mailman.isFireGunAbilityBought = PlayerPrefs.GetInt("FiregunAbility", 0) == 1 ? true : false;

        json = JsonUtility.ToJson(playerData);
        System.IO.File.WriteAllText(CurrencyManager.filePath, json);
    }
}

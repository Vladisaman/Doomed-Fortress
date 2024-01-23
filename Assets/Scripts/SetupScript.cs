using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupScript : MonoBehaviour
{
    [SerializeField] UpgradesMailmanSO mailman;
    [SerializeField] ShopUI shop;

    private void Awake()
    {
        Setup();
    }

    public void Setup()
    {
        mailman.CrossbowDamageLevel = PlayerPrefs.GetInt("CrossbowLevel", 1);
        mailman.MortarDamageLevel = PlayerPrefs.GetInt("MortarLevel", 1);
        mailman.FlamethrowerDamageLevel = PlayerPrefs.GetInt("FiregunLevel", 1);
                  
        mailman.CrossbowDamage = PlayerPrefs.GetFloat("CrossbowDamage", 8);
        mailman.MortarDamage = PlayerPrefs.GetFloat("MortarDamage", 9);
        mailman.FlamethrowerDamage = PlayerPrefs.GetFloat("FiregunDamage", 7);
                  
        shop.SetPrices(PlayerPrefs.GetInt("CrossbowCost", 10), PlayerPrefs.GetInt("MortarCost", 10), PlayerPrefs.GetInt("FiregunCost", 10));

        mailman.isCrossbowAbilityBought = PlayerPrefs.GetInt("CrossbowAbility", 0) == 1? true : false;
        mailman.isMortarAbilityBought = PlayerPrefs.GetInt("MortarAbility", 0) == 1 ? true : false;
        mailman.isFireGunAbilityBought = PlayerPrefs.GetInt("FiregunAbility", 0) == 1 ? true : false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

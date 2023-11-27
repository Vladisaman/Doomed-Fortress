using UnityEngine;
using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private UpgradesMailmanSO upgradesMailman;

    [SerializeField] private GameObject crossBowAbility;
    [SerializeField] private GameObject firegunAbility;
    [SerializeField] private GameObject mortarAbility;

    public enum Weapon { Mortar = 1, Crossbow = 0, FireGun = -1 };

    [SerializeField] public Weapon activeGun;

     Weapon topGun;
     Weapon midGun;
     Weapon botGun;

     public GameObject topGunObj;
     public GameObject midGunObj;
     public GameObject botGunObj;

    private bool hasFiregunAbility;
    private bool hasCrossbowAbility;
    private bool hasMortarAbility;

    private DateTime WeaponUseMetric;

    private bool once = true;

    private void Awake()
    {
        Setup();
    }

    private void Start()
    {
        SwipeDetection.OnSwipeEvent += SwapWeapon;
    }

    private void Setup()
    {
        crossBowAbility.SetActive(false);
        firegunAbility.SetActive(false);
        mortarAbility.SetActive(false);

        topGunObj = FindObjectOfType<Mortar>().gameObject;
        midGunObj = FindObjectOfType<Crossbow>().gameObject;
        botGunObj = FindObjectOfType<FireGun>().gameObject;

        topGun = Weapon.Mortar;
        midGun = Weapon.Crossbow;
        botGun = Weapon.FireGun;

        activeGun = midGun;

        WeaponUseMetric = DateTime.Now;

        topGunObj.GetComponent<Mortar>().HandleUpgrading(upgradesMailman.MortarDamage, upgradesMailman.MortarDamageLevel);
        midGunObj.GetComponent<Crossbow>().HandleUpgrading(upgradesMailman.CrossbowDamage, upgradesMailman.CrossbowDamageLevel);
        botGunObj.GetComponent<FireGun>().HandleUpgrading(upgradesMailman.FlamethrowerDamage, upgradesMailman.FlamethrowerDamageLevel);
        CheckBallistaSkill(upgradesMailman.isCrossbowAbilityBought);
        CheckFiregunSkill(upgradesMailman.isFireGunAbilityBought);
        CheckMortarSkill(upgradesMailman.isMortarAbilityBought);
    }

    private void Update()
    {
        activeGun = midGun; 
    }

    public void CheckAndStopCoroutine(Coroutine coroutine)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }
    }

    private void SwapWeapon(int i)
    {
        TimeSpan WeaponUseTime = WeaponUseMetric - DateTime.Now;
        Dictionary<string, object> parameters = new Dictionary<string, object>() { { "time_seconds", WeaponUseTime.TotalSeconds } };
        AppMetrica.Instance.ReportEvent(activeGun + "_use_time", parameters);
        AppMetrica.Instance.SendEventsBuffer();

        WeaponUseMetric = DateTime.Now;

        if (i == 1) //SwipeUP
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = topGun;
            topGun = midGun;
            midGun = botGun;
            botGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = topGunObj.transform.position;
            topGunObj.transform.position = botGunObj.transform.position;
            botGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;
            

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = topGunObj;
            topGunObj = midGunObj;
            midGunObj = botGunObj;
            botGunObj = bridgeObj;

        } else //SwipeDOWN
        {
            //Changing WEAPON
            Weapon bridge;
            bridge = botGun;
            botGun = midGun;
            midGun = topGun;
            topGun = bridge;

            //Changing OBJECTS' POSITION
            Vector3 bridgeObjPos;
            bridgeObjPos = botGunObj.transform.position;
            botGunObj.transform.position = topGunObj.transform.position;
            topGunObj.transform.position = midGunObj.transform.position;
            midGunObj.transform.position = bridgeObjPos;

            //Changing OBJECT
            GameObject bridgeObj;
            bridgeObj = botGunObj;
            botGunObj = midGunObj;
            midGunObj = topGunObj;
            topGunObj = bridgeObj;
        }
    }

    public bool CheckAbility(Weapon weapon)
    {
        return weapon switch
        {
            Weapon.FireGun => hasFiregunAbility,
            Weapon.Crossbow => hasCrossbowAbility,
            Weapon.Mortar => hasMortarAbility,
            _ => false,
        };
    }

    public void ActivateAbility(Weapon weapon)
    {
        switch (weapon) {
            case Weapon.FireGun:
                hasFiregunAbility = true;
                break;
            case Weapon.Crossbow:
                hasCrossbowAbility = true;
                break;
            case Weapon.Mortar:
                hasMortarAbility = true;
                break;
        }
    }

    public void CheckBallistaSkill(bool isActive)
    {
        hasCrossbowAbility = isActive;
        crossBowAbility.SetActive(isActive);
    }

    public void CheckFiregunSkill(bool isActive)
    {
        hasFiregunAbility = isActive;
        firegunAbility.SetActive(isActive);
    }

    public void CheckMortarSkill(bool isActive)
    {
        hasMortarAbility = isActive;
        mortarAbility.SetActive(isActive);
    }
}

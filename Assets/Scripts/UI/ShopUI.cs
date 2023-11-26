using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ShopUI : MonoBehaviour
{
    [SerializeField] private UpgradesMailmanSO upgradesMailman;

    [SerializeField] private TextMeshProUGUI coinsText;

    [SerializeField] private Button fireGunAbilityBuy;
    [SerializeField] private TextMeshProUGUI fireGunAbilityBuyText;
    [SerializeField] private Button crossbowAbilityBuy;
    [SerializeField] private TextMeshProUGUI crossbowAbilityBuyText;
    [SerializeField] private Button mortarAbilityBuy;
    [SerializeField] private TextMeshProUGUI mortarAbilityBuyText;

    [SerializeField] private GameObject firegunBuyAbilityUI;
    [SerializeField] private GameObject mortarBuyAbilityUI;
    [SerializeField] private GameObject crossbowBuyAbilityUI;

    [SerializeField] private GameObject firegunUpgradeAbilityUI;
    [SerializeField] private GameObject mortarUpgradeAbilityUI;
    [SerializeField] private GameObject crossbowUpgradeAbilityUI;

    public event EventHandler OnFiregunAbilityBought;
    public event EventHandler OnCrossbowAbilityBought;
    public event EventHandler OnMortarAbilityBought;

    [Header("----------PRICES----------")]
    [SerializeField] private int mortarAbilityPrice;
    [SerializeField] private int firegunAbilityPrice;
    [SerializeField] private int crossbowAbilityPrice;
    [Space(5)]
    private int firegunDamagePrice;
    [Space(10)]
    private int crossbowDamagePrice;
    [Space(10)]
    private int mortarDamagePrice;


    [Header("----------FIREGUN----------")]
    [SerializeField] private FireGun firegun;
    [SerializeField] private Button upgradeFiregunDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeFiregunDamageText;
    [SerializeField] private TextMeshProUGUI upgradeFiregunDamagePriceText;

    [Header("----------CROSSBOW----------")]
    [SerializeField] private Crossbow crossbow;
    [SerializeField] private Button upgradeCrossbowDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeCrossbowDamageText;
    [SerializeField] private TextMeshProUGUI upgradeCrossbowDamagePriceText;

    [Header("----------MORTAR----------")]
    [SerializeField] private Mortar mortar;
    [SerializeField] private Button upgradeMortarDamageButton;
    [SerializeField] private TextMeshProUGUI upgradeMortarDamageText;
    [SerializeField] private TextMeshProUGUI upgradeMortarDamagePriceText;

    CurrencyManager _currencyManager;

    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        mortar.HandleUpgrading(upgradesMailman.MortarDamage, upgradesMailman.MortarDamageLevel);
        crossbow.HandleUpgrading(upgradesMailman.CrossbowDamage, upgradesMailman.CrossbowDamageLevel);
        firegun.HandleUpgrading(upgradesMailman.FlamethrowerDamage, upgradesMailman.FlamethrowerDamageLevel);

        firegunDamagePrice = 10;
        crossbowDamagePrice = 10;
        mortarDamagePrice = 10;

        UpdateAllButtons();
    }

    private void OnEnable()
    {
        UpdateCoinsAmount();
    }

    private void Start()
    {
        fireGunAbilityBuyText.text = $"{firegunAbilityPrice}";
        crossbowAbilityBuyText.text = $"{crossbowAbilityPrice}";
        mortarAbilityBuyText.text = $"{mortarAbilityPrice}";

        fireGunAbilityBuy.onClick.AddListener(() =>
        {
            TryToBuyAbility(Player.Weapon.FireGun);
            upgradesMailman.isFireGunAbilityBought = true;
            UpdateEverything();
        });
        mortarAbilityBuy.onClick.AddListener(() =>
        {
            TryToBuyAbility(Player.Weapon.Mortar);
            upgradesMailman.isMortarAbilityBought = true;
            UpdateEverything();
        });
        crossbowAbilityBuy.onClick.AddListener(() =>
        {
            TryToBuyAbility(Player.Weapon.Crossbow);
            upgradesMailman.isCrossbowAbilityBought = true;
            UpdateEverything();
        });

        upgradeFiregunDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= firegunDamagePrice)
            {
                _currencyManager.SpendCoins(firegunDamagePrice);
                firegun.UpgradeDamageLevel();
                firegunDamagePrice += 2 * firegun.GetCurrentDamageLevel();
                UpdateEverything();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        });

        upgradeCrossbowDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= crossbowDamagePrice)
            {
                _currencyManager.SpendCoins(crossbowDamagePrice);
                crossbow.UpgradeDamageLevel();
                crossbowDamagePrice += 2 * crossbow.GetCurrentDamageLevel();
                UpdateEverything();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        });

        upgradeMortarDamageButton.onClick.AddListener(() =>
        {
            if (_currencyManager.GetCoins() >= mortarDamagePrice)
            {
                _currencyManager.SpendCoins(mortarDamagePrice);
                mortar.UpgradeDamageLevel();
                mortarDamagePrice += 2 * mortar.GetCurrentDamageLevel();
                UpdateEverything();
            }
            else
            {
                Debug.Log("Not enough money");
            }
        });
    }

    private void UpdateEverything()
    {
        UpdateAllButtons();
        UpdateCoinsAmount();
    }

    private void UpdateCoinsAmount()
    {
        coinsText.text = $"Монет: {_currencyManager.GetCoins()}";
    }

    private void TryToBuyAbility(Player.Weapon weapon)
    {
        switch (weapon)
        {
            case Player.Weapon.FireGun:
                if (_currencyManager.GetCoins() >= firegunAbilityPrice)
                {
                    OnFiregunAbilityBought?.Invoke(this, EventArgs.Empty);
                    _currencyManager.SpendCoins(firegunAbilityPrice);

                    //Show(firegunUpgradeAbilityUI);
                    Hide(fireGunAbilityBuy.gameObject);
                }
                else
                {
                    Debug.Log("No enough money");
                }
                break;
            case Player.Weapon.Mortar:
                if (_currencyManager.GetCoins() >= mortarAbilityPrice)
                {
                    OnMortarAbilityBought?.Invoke(this, EventArgs.Empty);
                    _currencyManager.SpendCoins(mortarAbilityPrice);

                    //Show(mortarUpgradeAbilityUI);
                    Hide(mortarAbilityBuy.gameObject);
                }
                else
                {
                    Debug.Log("No enough money");
                }
                break;
            case Player.Weapon.Crossbow:
                if (_currencyManager.GetCoins() >= crossbowAbilityPrice)
                {
                    OnCrossbowAbilityBought?.Invoke(this, EventArgs.Empty);
                    _currencyManager.SpendCoins(crossbowAbilityPrice);

                    //Show(crossbowUpgradeAbilityUI);
                    Hide(crossbowAbilityBuy.gameObject);
                }
                else
                {
                    Debug.Log("No enough money");
                }
                break;
        }
    }

    private void Hide(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    private void UpdateAllButtons()
    {
        firegunDamagePrice += firegun.GetCurrentDamageLevel() * 2;
        upgradeFiregunDamageText.text = $"Базовая атака: {firegun.GetCurrentDamage()}";
        upgradeFiregunDamagePriceText.text = $"{firegunDamagePrice}";

        crossbowDamagePrice += crossbow.GetCurrentDamageLevel() * 2;
        upgradeCrossbowDamageText.text = $"Базовая атака: {crossbow.GetCurrentDamage()}";
        upgradeCrossbowDamagePriceText.text = $"{crossbowDamagePrice}";

        mortarDamagePrice += mortar.GetCurrentDamageLevel() * 2;
        upgradeMortarDamageText.text = $"Базовая атака: {mortar.GetCurrentDamage()}";
        upgradeMortarDamagePriceText.text = $"{mortarDamagePrice}";
    }

    private void OnDisable()
    {
        upgradesMailman.CrossbowDamageLevel = crossbow.GetCurrentDamageLevel();
        upgradesMailman.MortarDamageLevel = mortar.GetCurrentDamageLevel();
        upgradesMailman.FlamethrowerDamageLevel = firegun.GetCurrentDamageLevel();

        upgradesMailman.CrossbowDamage = crossbow.GetCurrentDamage();
        upgradesMailman.MortarDamage = mortar.GetCurrentDamage();
        upgradesMailman.FlamethrowerDamage = firegun.GetCurrentDamage();
    }
}

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
    [SerializeField] private int firegunDamagePrice;
    [Space(10)]
    [SerializeField] private int crossbowDamagePrice;
    [Space(10)]
    [SerializeField] private int mortarDamagePrice;


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
    [SerializeField] SetupScript setupScript;

    private void Awake()
    {
        _currencyManager = FindObjectOfType<CurrencyManager>();

        mortar.HandleUpgrading(upgradesMailman.MortarDamage, upgradesMailman.MortarDamageLevel);
        crossbow.HandleUpgrading(upgradesMailman.CrossbowDamage, upgradesMailman.CrossbowDamageLevel);
        firegun.HandleUpgrading(upgradesMailman.FlamethrowerDamage, upgradesMailman.FlamethrowerDamageLevel);

        //firegunDamagePrice = 10;
        //crossbowDamagePrice = 10;
        //mortarDamagePrice = 10;

        setupScript.Setup();
        UpdateAllButtons();
    }

    private void OnEnable()
    {
        setupScript.Setup();
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
        upgradeFiregunDamageText.text = $"Базовая атака: { Math.Round(firegun.GetCurrentDamage(), 2)}";
        upgradeFiregunDamagePriceText.text = $"{firegunDamagePrice}";

        upgradeCrossbowDamageText.text = $"Базовая атака: {Math.Round(crossbow.GetCurrentDamage(), 2)}";
        upgradeCrossbowDamagePriceText.text = $"{crossbowDamagePrice}";

        upgradeMortarDamageText.text = $"Базовая атака: {Math.Round(mortar.GetCurrentDamage(), 2)}";
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

        PlayerPrefs.SetInt("CrossbowLevel", crossbow.GetCurrentDamageLevel());
        PlayerPrefs.SetInt("MortarLevel", mortar.GetCurrentDamageLevel());
        PlayerPrefs.SetInt("FiregunLevel", firegun.GetCurrentDamageLevel());

        PlayerPrefs.SetFloat("CrossbowDamage", crossbow.GetCurrentDamage());
        PlayerPrefs.SetFloat("MortarDamage", mortar.GetCurrentDamage());
        PlayerPrefs.SetFloat("FiregunDamage", firegun.GetCurrentDamage());

        PlayerPrefs.SetInt("CrossbowCost", crossbowDamagePrice);
        PlayerPrefs.SetInt("MortarCost", mortarDamagePrice);
        PlayerPrefs.SetInt("FiregunCost", firegunDamagePrice);

        PlayerPrefs.SetInt("CrossbowAbility", upgradesMailman.isCrossbowAbilityBought ? 1 : 2);
        PlayerPrefs.SetInt("MortarAbility", upgradesMailman.isMortarAbilityBought ? 1 : 2);
        PlayerPrefs.SetInt("FiregunAbility", upgradesMailman.isFireGunAbilityBought ? 1 : 2);
    }

    public void SetPrices(int ballista, int mortar, int firegun)
    {
        crossbowDamagePrice = ballista;
        mortarDamagePrice = mortar;
        firegunDamagePrice = firegun;

        UpdateAllButtons();
    }
}

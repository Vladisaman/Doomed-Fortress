using TMPro;
using UnityEngine;

public class EnergyTextUpdater : MonoBehaviour
{
    [SerializeField] TMP_Text EnergyText;
    CurrencyManager _currencyManager;
    //private void Awake()
    //{
    //    _currencyManager = FindObjectOfType<CurrencyManager>();
    //}
    //// Start is called before the first frame update
    //void Start()
    //{
    //    UpdateText();
    //}
    //public void UpdateText()
    //{
    //    EnergyText.text = _currencyManager.GetEnergy().ToString();
    //}
}

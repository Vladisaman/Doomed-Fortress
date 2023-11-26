using TMPro;
using UnityEngine;

public class CrossbowAbilityButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button abilityButton;
    private Crossbow crossbow;
    private float abilityCooldown;
    private bool isCooldown;
    [SerializeField] TextMeshProUGUI cooldownText;

    private void Start()
    {
        crossbow = FindObjectOfType<Crossbow>();
        crossbow.OnAbilityAction += CrossBow_OnAbilityAction;
        abilityCooldown = crossbow.GetAbilityCooldown();
        HideCooldownText();
    }

    private void CrossBow_OnAbilityAction()
    {
        isCooldown = true;
    }

    private void Update()
    {
        if (isCooldown) {
            abilityCooldown -= Time.deltaTime;
            if (abilityCooldown <= 0) {
                HideCooldownText();
                abilityButton.interactable = true;
                abilityCooldown = crossbow.GetAbilityCooldown();
                isCooldown = false;
            } else {
                abilityButton.interactable = false;
                ShowCooldownText();
                cooldownText.text = Mathf.Ceil(abilityCooldown).ToString();
            }
        }
        
    }

    public void ShowCooldownText()
    {
        cooldownText.gameObject.SetActive(true);
    }

    public void HideCooldownText()
    {
        cooldownText.gameObject.SetActive(false);
    }

    public bool IsCooldown()
    {
        return isCooldown;
    }
}

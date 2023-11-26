using TMPro;
using UnityEngine;

public class FiregunAbilityButton : MonoBehaviour
{
    [SerializeField] private UnityEngine.UI.Button abilityButton;
    [SerializeField] private FireGun fireGun;
    private float abilityCooldown;
    private bool isCooldown;
    [SerializeField] TextMeshProUGUI cooldownText;

    private void Start()
    {
        var fireGun = FindObjectOfType<FireGun>();
        fireGun.OnAbilityAction += FireGun_OnAbilityAction;
        abilityCooldown = fireGun.GetAbilityCooldown();
        HideCooldownText();
    }

    private void FireGun_OnAbilityAction()
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
                abilityCooldown = fireGun.GetAbilityCooldown();
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

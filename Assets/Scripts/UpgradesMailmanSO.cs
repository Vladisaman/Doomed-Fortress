using UnityEngine;

[CreateAssetMenu()]
public class UpgradesMailmanSO : ScriptableObject
{
    public int FiregunDamageLevel;
    public float FiregunDamage;

    public int CrossbowDamageLevel;
    public float CrossbowDamage;

    public int MortarDamageLevel;
    public float MortarDamage;

    public bool isFireGunAbilityBought;
    public bool isCrossbowAbilityBought;
    public bool isMortarAbilityBought;
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Shield : Enemy
{
    public string NOTE = "CHANGE SHIELD PROPERTIES IN ENEMY WITH SHIELD PREFAB";

    [SerializeField] ShieldEnemy parentEnemy;

    private void Start()
    {
        parentEnemy = gameObject.GetComponentInParent<ShieldEnemy>();
        health = parentEnemy.shieldHealth;
        healthBar.maxValue = health;
        healthBar.value = health;
        armor = parentEnemy.shieldArmor;
        arrowDamageResist = parentEnemy.arrowDamageResist;
        bombDamageResist = parentEnemy.bombDamageResist;
        fireDamageResist = parentEnemy.fireDamageResist;
    }
    private void Update()
    {
        CheckDeath();
    }

    public override void Die()
    {
        gameObject.GetComponentInParent<ShieldEnemy>().isShieldAlive = false;
        Destroy(gameObject);
    }
}

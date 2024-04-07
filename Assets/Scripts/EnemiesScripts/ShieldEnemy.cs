using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShieldEnemy : Enemy
{

    public bool isShieldAlive = true;

    [Header("----------SHIELD PROPERTIES----------")]
    [SerializeField] public float shieldHealth;
    [SerializeField] public float shieldArmor;
    [SerializeField] public Slider shieldBar;

    private void Start()
    {
        shieldBar.maxValue = shieldHealth;
        shieldBar.value = shieldHealth;
    }

    public override void TakeDamage(float weaponDamage, string weaponName)
    {
        currentDamageResist = 1;
        switch (weaponName)
        {
            case "BALLISTA":
                switch (arrowDamageResist)
                {
                    case DamageResistance.WEAK:
                        currentDamageResist = 1.5F;
                        break;
                    case DamageResistance.RESIST:
                        currentDamageResist = 0.5F;
                        break;
                    case DamageResistance.IMMUNE:
                        currentDamageResist = 0.0F;
                        break;
                }
                break;
            case "MORTAR":
                switch (bombDamageResist)
                {
                    case DamageResistance.WEAK:
                        currentDamageResist = 1.5F;
                        break;
                    case DamageResistance.RESIST:
                        currentDamageResist = 0.5F;
                        break;
                    case DamageResistance.IMMUNE:
                        currentDamageResist = 0.0F;
                        break;
                }
                break;
            case "FIREGUN":
                switch (fireDamageResist)
                {
                    case DamageResistance.WEAK:
                        currentDamageResist = 1.5F;
                        break;
                    case DamageResistance.RESIST:
                        currentDamageResist = 0.5F;
                        break;
                    case DamageResistance.IMMUNE:
                        currentDamageResist = 0.0F;
                        break;
                }
                break;
        }

        actualDamage = weaponDamage * currentDamageResist;


        if (isShieldAlive)
        {
            shieldHealth -= actualDamage;
            shieldBar.value = shieldHealth;

            if (shieldHealth <= 0)
            {
                isShieldAlive = false;
            }

            if(weaponName == "MORTAR")
            {
                health -= actualDamage;
            }

        } else
        {
            health -= actualDamage;
        }

        Debug.Log(shieldHealth + " " + isShieldAlive);

        healthBar.value = health;
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }
        if (isShieldAlive)
        {
            animator.SetBool("IsAttacking", true);
        }

        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public override void Die()
    {
        speed = 0;
        //shield.Die();

        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDead", true);

        if (once)
        {
            var tempSpawner = FindObjectOfType<Spawner>();
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 0.5F);
    }
}

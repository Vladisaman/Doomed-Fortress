using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class ShieldEnemy : Enemy
{

    public bool isShieldAlive = true;

    [Header("----------SHIELD PROPERTIES----------")]
    [SerializeField] public float shieldHealth;
    [SerializeField] public float shieldArmor;
    [SerializeField] private float shieldBombDamageXAxisReduce = 0.2f;

    public virtual void TakeDamage(float weaponDamage, string weaponName, GameObject projectile)
    {
        currentDamageResist = 1;
        switch (weaponName)
        {
            case BALLISTA:
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
            case MORTAR:
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
            case FIREGUN:
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
        health -= actualDamage;

        if (weaponName == MORTAR)
        {
            if (isShieldAlive)
            {
                if (projectile.transform.position.x > transform.position.x - shieldBombDamageXAxisReduce)
                {
                    health -= actualDamage;
                    healthBar.value = health;
                }
            }
            else
            {
                health -= actualDamage;
                healthBar.value = health;
            }

        }

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

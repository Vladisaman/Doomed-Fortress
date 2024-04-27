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

        if (health <= 0 && weaponName == "BALLISTA" && skillManager.Vampirism && isAlive == true)
        {
            wall._health += wall.maxhealth * 0.05f;
            isAlive = false;
        }
        if ((skillManager.PoisonArrow && weaponName == "BALLISTA") || (skillManager.PoisonYadro && weaponName == "MORTAR"))
        {
            //Stagger += actualDamage * 0.2f;
            PoisonStacks++;
            if (PoisonStacks >= 3)
            {
                PoisonStacks = 0;
                TakeDamage(5.0f);

                StartCoroutine(BlinkPoison());
            }
        }
        if (skillManager.ColdArrow && weaponName == "BALLISTA")
        {
            SlowStacks += 1;
            if (Slow != null)
            {
                StopCoroutine(Slow);
                Slow = StartCoroutine(SlowDown());
            }
            else
            {
                Slow = StartCoroutine(SlowDown());
            }

            if (SlowStacks == 5)
            {
                SlowStacks = 0;
                StopCoroutine(Slow);

                if (Stun != null)
                {
                    StopCoroutine(Stun);
                    Stun = StartCoroutine(Freeze());
                }
                else
                {
                    Stun = StartCoroutine(Freeze());
                }
            }
        }
        if (skillManager.ColdYadro && weaponName == "MORTAR")
        {
            if (Stun != null)
            {
                StopCoroutine(Stun);
                Stun = StartCoroutine(Freeze());
            }
            else
            {
                Stun = StartCoroutine(Freeze());
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
        GetComponent<BoxCollider2D>().enabled = false;
        speed = 0;
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

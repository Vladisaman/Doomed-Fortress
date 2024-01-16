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

    private bool triggerHit = false;


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


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall" && triggerHit) {
            isAttacking = true;
        }
        triggerHit = true;

        if (skillManager.BlackFire)
        {
            if (collision.CompareTag("Burning"))
            {
                burning.Play();
                burn.enabled = true;
            }
            else
            {
                burn.enabled = false;
            }
        }
        if (skillManager.ColdArrow)
        {
            if (collision.CompareTag("ColdProjectile"))
            {
                countHit++;
                StartCoroutine(SpeedDown());
                if (countHit == 5)
                {
                    StartCoroutine(Coldarrow());
                }
            }
        }
        if (skillManager.PoisonArrow)
        {
            StartCoroutine(PoisonProjectle());
        }
        if (skillManager.PoisonYadro)
        {
            StartCoroutine(PoisonProjectle());
        }
    }
    public override void OnTriggerStay2D(Collider2D collision)
    {
        if (skillManager.BlackFire)
        {
            if (burn.enabled == true)
            {
                health -= 5f * Time.deltaTime;
            }
            else
            {
                health = healthBar.value;
            }
        }
        if (skillManager.ColdYadro)
        {
            if (collision.CompareTag("ColdYadro"))
            {
                StartCoroutine(Coldyadro());
            }
        }
    }

    public override IEnumerator Coldyadro()
    {
        GetStunned();
        yield return new WaitForSeconds(time += 1f);
        if (time > 1f)
        {
            GetUnstunned();
            time = 0;
            yield break; 
        }
    }
    public override IEnumerator Coldarrow()
    {
        GetStunned();
        yield return new WaitForSeconds(time += 2f);
        if (time > 1f)
        {
            GetUnstunned();
            countHit = 0;
            time = 0;
            yield break;
        }
    }
    public override IEnumerator SpeedDown()
    {
        speed = 0.3f;
        yield return new WaitForSeconds(1f);
        speed = 0.5f;
        if (speed == 0)
        {
            yield break;
        }
    }
    public override IEnumerator PoisonProjectle()
    {
        yield return new WaitForSeconds(0.4f);
        health -= StaggerDamage % 10;
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
            gameManager.UpdateScore(score);
            var tempSpawner = FindObjectOfType<TempSpawner>();
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 0.5F);
    }
}

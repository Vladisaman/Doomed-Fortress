using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceEnemy : Enemy
{
    private Player player;
    private bool hasFrozen;

    private void Start()
    {
        hasFrozen = false;
        player = FindObjectOfType<Player>();
    }

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
        if (!hasFrozen)
        {
            player.midGunObj.GetComponent<Weapon>().Freeze();
            hasFrozen = true;
        }
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

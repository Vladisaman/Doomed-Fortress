using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonEnemy : Enemy
{
    [SerializeField] GameObject poisonExplosion;

    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
        StartCoroutine(KermitSudoku());
    }

    IEnumerator KermitSudoku()
    {
        yield return new WaitForSeconds(1.0f);
        GetComponent<SpriteRenderer>().enabled = false;
        Destroy(Instantiate(poisonExplosion, transform.position, Quaternion.identity), 0.5f);
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
        wall.GetComponent<WallBehavior>().Poison();
        Die();
    }

    public override void Die()
    {
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

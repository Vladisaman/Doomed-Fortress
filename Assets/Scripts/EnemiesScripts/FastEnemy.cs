using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public override void Die()
    {
        speed = 0;
        animator.SetBool("IsDead", true);

        if (once)
        {
            var tempSpawner = FindObjectOfType<TempSpawner>();
            gameManager.UpdateScore(score);
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 0.5F);
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
        speed = 0.7f;
        yield return new WaitForSeconds(1f);
        speed = 1f;
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
}

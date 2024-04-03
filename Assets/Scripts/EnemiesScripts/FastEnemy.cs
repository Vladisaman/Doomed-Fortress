using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    [SerializeField] Sprite[] Sprites;
    [SerializeField] Animator[] Animators;

    private void Start()
    {
        int random = Random.Range(0, 3);
        //GetComponent<SpriteRenderer>().sprite = Sprites[random];
        animator = Animators[random];
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

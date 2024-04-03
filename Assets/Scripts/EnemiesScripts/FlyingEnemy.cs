using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class FlyingEnemy : Enemy
{
    [Header("----------FLY PROPERTIES----------")]
    //[SerializeField] float amplitude = 1f;
    [SerializeField] int upOrDown;
    [SerializeField] private float waveSpeed = 1f; // Higher make the wave faster
    [SerializeField] private float bonusHeight = 1f; // Set higher if you want more wave intensity
    
    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1
    private Vector3 basePosition; // This variable maintains the location of the object without applying sine changes
    private Transform target;

    private void Start()
    {
        target = wall.transform;
        basePosition = transform.position;
    }
    public override void Move()
    {
        cycle += Time.deltaTime * waveSpeed;
        transform.position = basePosition + (Vector3.up * bonusHeight) * upOrDown * Mathf.Sin(cycle);
        if (target)
            basePosition = Vector3.MoveTowards(basePosition, target.position, Time.deltaTime * speed);


        //transform.position = startPosition + new Vector3(-1 * time * speed, amplitude * Mathf.Sin(time), 0);
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


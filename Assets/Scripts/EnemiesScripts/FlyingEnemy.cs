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

    Vector3 startPosition;
    new private float time;

    
    
    private float cycle; // This variable increases with time and allows the sine to produce numbers between -1 and 1
    private Vector3 basePosition; // This variable maintains the location of the object without applying sine changes
    private Transform target;

    private void Start()
    {
        target = wall.transform;
        basePosition = transform.position;
        
    }
    private void Update()
    {
        
        if (!isAttacking && !isAttracted) 
        {
            Move();
        } 
        else if (isAttacking) 
        {
            Attack();
        }
        else if (isAttracted)
        {
            BeAttracted();
        }

        CheckDeath();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
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
    }
    new public void OnTriggerStay2D(Collider2D collision)
    {
        if (skillManager.Thorns)
        {
            if (collision.CompareTag("Thorns"))
            {
                health = maxHealth;
            }
        }
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


using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneEnemy : Enemy
{
    [SerializeField] private GameObject _rockPrefab;
    private float rockDamage;
    private float defaultSpeed;
    private Coroutine rock;
    private bool isAlive;

    private void Start()
    {
        isAlive = true;
        defaultSpeed = speed;
        rock = StartCoroutine(RockThrowCoroutine());
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

    private IEnumerator RockThrowCoroutine()
    {
        while (!isAttacking && isAlive)
        {
            speed = 0;
            animator.SetBool("IsThrowing", true);
            GameObject rock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
            rock.GetComponent<Rock>().damage = rockDamage;
            Debug.Log("THROW");
            Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();
            rockRigidbody.AddForce(Vector2.left * 500f);
            yield return new WaitForSeconds(0.5f);
            animator.SetBool("IsThrowing", false);
            speed = defaultSpeed;
            yield return new WaitForSeconds(2);
        }

        StopCoroutine(rock);
    }

    public override void Die()
    {
        GetComponent<BoxCollider2D>().enabled = false;
        isAlive = false;
        speed = 0;
        animator.SetBool("IsAttacking", false);
        animator.SetBool("IsDead", true);

        if (once)
        {
            var tempSpawner = FindObjectOfType<Spawner>();
            tempSpawner.NewWave();
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 1.5f);
    }

    public override void IncreasePower()
    {
        maxHealth += 0.25f * Spawner.WaveNumber;
        damage += 0.25f * Spawner.WaveNumber;
        damage += 0.05f * Spawner.WaveNumber;
        coinsForDestroy += Spawner.WaveNumber * 5;
    }
}
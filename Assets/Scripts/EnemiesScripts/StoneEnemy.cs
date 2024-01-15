using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class StoneEnemy : Enemy
{
    [Header("----------DO !NOT! TOUCH----------")]
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private Transform _throwPoint;

    [Header("----------MOVEMENT PROPERTIES----------")]
    [SerializeField] private int _stopSteps = 600;
    [SerializeField] private float _stopDuration = 7f;
    [SerializeField] private int _steps = 0;
    private bool _isStopping = false;
    private float _stopTime = 0f;

    private bool isAllowedThrowing = true;

    private void Update()
    {
        healthBar.value = health;
        //if (!_isStopping)
        //{
            Move();
            _steps++;

        if (isAllowedThrowing)
        {
            Invoke("Stop", 2);
            isAllowedThrowing = false;
        }

        //    if (_steps >= _stopSteps)
        //    {

        //    }
        //}
        //else
        //{
        //    if (Time.time >= _stopTime)
        //    {
        //        _isStopping = false;
        //    }
        //}

        CheckDeath();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            speed = 0;
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
        if (skillManager.ColdYadro)
        {
            if (collision.CompareTag("ColdYadro"))
            {
                StartCoroutine(Coldyadro());
            }
        }
    }

    private void Stop()
    {
        animator.SetBool("IsThrowing", true);
        _isStopping = true;
        _steps = 0;
        _stopTime = Time.time + _stopDuration;
        ThrowRock();
    }

    private void ThrowRock()
    {
        GameObject rock = Instantiate(_rockPrefab, transform.position, Quaternion.identity);
        Rigidbody2D rockRigidbody = rock.GetComponent<Rigidbody2D>();
        rockRigidbody.AddForce(Vector2.left * 500f);
        animator.SetBool("IsThrowing", false);
        isAllowedThrowing = true;
    }

    public override void Die()
    {
        speed = 0;
        animator.SetBool("IsDead", true);

        if (once)
        {
            gameManager.UpdateScore(score);
            var tempSpawner = FindObjectOfType<TempSpawner>();
            tempSpawner.NewWave();
            tempSpawner.IncreaseKilledEnemyCount();
            once = false;
        }

        Destroy(gameObject, 1.5f);
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

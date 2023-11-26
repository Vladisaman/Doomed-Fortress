using UnityEngine;

public class StoneEnemy : Enemy
{

    [Header("----------DO !NOT! TOUCH----------")]
    [SerializeField] private GameObject _rockPrefab;
    [SerializeField] private Transform _throwPoint;

    [Header("----------MOVEMENT PROPERTIES----------")]
    [SerializeField] private int _stopSteps = 600; 
    [SerializeField] private float _stopDuration = 7f;
    private int _steps = 0; 
    private bool _isStopping = false; 
    private float _stopTime = 0f;

    private void Update()
    {
        healthBar.value = health;
        if (!_isStopping)
        {
            Move();
            _steps++;

            if (_steps >= _stopSteps)
            {
                animator.SetBool("IsThrowing", true);
                Stop();
                animator.SetBool("IsThrowing", false);
            }
        }
        else
        {
            if (Time.time >= _stopTime)
            {
                _isStopping = false;
            }
        }

        CheckDeath();
    }

    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            speed = 0;
        }
    }

    private void Stop()
    {
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
}

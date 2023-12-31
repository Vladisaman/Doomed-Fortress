using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    protected bool once = true;
    [SerializeField] public Slider healthBar;
    [HideInInspector] public WallBehavior wall;
    protected float lastAttackTime;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isAttracted = false;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;
    [SerializeField] public int weight;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField]  protected int score;
    [SerializeField] protected GameManager gameManager;
    [SerializeField] protected TempSpawner enemySpawner;
    [SerializeField] private AnimationClip walkAnimationClip;
    [SerializeField] private int coinsForDestroy;
    [SerializeField] private int persentCount;

    private Animation animationComponent;
    [SerializeField] protected Animator animator;

    [Header("----------DAMAGE RESIST----------")]
    [SerializeField] public float armor;
    [SerializeField] public DamageResistance arrowDamageResist;
    [SerializeField] public DamageResistance bombDamageResist;
    [SerializeField] public DamageResistance fireDamageResist;
    [SerializeField] public GameObject centerObject;
    public float currentDamageResist;
    public const string BALLISTA = "Ballista";
    public const string MORTAR = "Mortar";
    public const string FIREGUN= "FireGun";

    float speedValue;

    [HideInInspector] public float damageReduce;
    [HideInInspector] public float actualDamage;

    private void OnDestroy()
    {
        //(murakhtin): �������� �� �� ��� �� ������� �� play mode � �����
#if UNITY_EDITOR
        if (!EditorApplication.isPlayingOrWillChangePlaymode &&
             EditorApplication.isPlaying)
        {
            return;
        }
#endif
        var currencyManager= FindObjectOfType<CurrencyManager>();
        currencyManager.AddCoins(coinsForDestroy);
    }

    private void Awake()
    {
        wall = GameObject.Find("Wall").GetComponent<WallBehavior>();

        health = maxHealth;
        healthBar.maxValue = maxHealth;
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        enemySpawner = GameObject.Find("Spawner").GetComponent<TempSpawner>();

        animator = GetComponent<Animator>();
        speedValue = speed;
        animationComponent = gameObject.AddComponent<Animation>();
        animationComponent.AddClip(walkAnimationClip, "Walk");
    }

    private void Update()
    {
        if (!isAttacking && !isAttracted) 
        {
            Move();
        } 
        else if(isAttacking) 
        {
            Attack();
        }
        else if(isAttracted)
        {
            BeAttracted();
        }

        CheckDeath();
        
        healthBar.value = health;
    }

    protected void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public  void GetStunned()
    {
        speed = 0;
    }
    public void GetAttracted()
    {
        isAttracted = true;
    }

    public void BeAttracted()
    {
        Vector3 targetPosition = centerObject.transform.position;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    public void GetUnattracted()
    {
        isAttracted = false;
    }

    public void GetUnstunned()
    {
        speed = speedValue;
    }

    public virtual void TakeDamage(float weaponDamage, string weaponName)
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
        healthBar.value = health;
    }
    
    public virtual void Die()
    {
        animator.SetBool("IsDead", true);

        if (once)
        {
            gameManager.UpdateScore(score);
            //enemySpawner.DecreaseEnemiesCount();
            once = false;
        }

        Destroy(gameObject, 3);
    }

    public virtual void Move()
    {
        //animationComponent.Play("Walk");
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wall" ) {
            isAttacking = true;
        }
    }

    public virtual void Attack()
    {
        if(Time.time - lastAttackTime < attackCooldown) {
            return;
        }
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public virtual int GetWeight()
    {
        return weight;
    }
    public void IncreasePower()
    {
        maxHealth += 5 * TempSpawner.WaveNumber;
        damage += 5 * TempSpawner.WaveNumber;
    }
}

public enum DamageResistance
{
    NORMAL,
    RESIST,
    WEAK,
    IMMUNE
}
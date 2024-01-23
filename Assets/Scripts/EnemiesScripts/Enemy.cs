using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    private bool isAlive = true;
    protected bool once = true;
    [SerializeField] public Slider healthBar;
    [HideInInspector] public WallBehavior wall;
    public SkillManager skillManager;
    [SerializeField] public ParticleSystem burning;
    [SerializeField] public BoxCollider2D burn;
    protected float lastAttackTime;
    public float time;
    [HideInInspector] public bool isAttacking = false;
    [HideInInspector] public bool isAttracted = false;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float StaggerDamage = 0f;
    [SerializeField] public int countHit;
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;
    [SerializeField] public int weight;
    [SerializeField] protected float attackCooldown = 2f;
    [SerializeField] private int coinsForDestroy;
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

    private Coroutine Slow;
    private Coroutine Stun;
    private float Stagger;
    private int SlowStacks;

    float DefaultSpeed;

    [HideInInspector] public float damageReduce;
    [HideInInspector] public float actualDamage;

    private void OnDestroy()
    {
        //(murakhtin): проверка на то что мы выходим из play mode в юнити
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
        skillManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
        wall = GameObject.Find("Wall").GetComponent<WallBehavior>();

        health = maxHealth;
        healthBar.maxValue = maxHealth;

        animator = GetComponent<Animator>();
        DefaultSpeed = speed;

        StartCoroutine(PoisonDamage());
        Stagger = 0;
        SlowStacks = 0;
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

    public void GetStunned()
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
        speed = DefaultSpeed;
    }

    public virtual void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
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

        if (skillManager.PoisonArrow && weaponName == "BALLISTA")
        {
            Stagger += actualDamage * 0.2f;
        }
        if (skillManager.PoisonYadro && weaponName == "MORTAR")
        {
            Stagger += actualDamage * 0.2f;
        }
        if (skillManager.ColdArrow && weaponName == "BALLISTA")
        {
            SlowStacks += 1;
            if (Slow != null)
            {
                StopCoroutine(Slow);
                Slow = StartCoroutine(SlowDown());
            }
            else
            {
                Slow = StartCoroutine(SlowDown());
            }

            if (SlowStacks == 5)
            {
                SlowStacks = 0;
                StopCoroutine(Slow);

                if(Stun != null)
                {
                    StopCoroutine(Stun);
                    Stun = StartCoroutine(Freeze());
                } else
                {
                    Stun = StartCoroutine(Freeze());
                }
            }
        }
        if(skillManager.ColdYadro && weaponName == "MORTAR")
        {
            if (Stun != null)
            {
                StopCoroutine(Stun);
                Stun = StartCoroutine(Freeze());
            }
            else
            {
                Stun = StartCoroutine(Freeze());
            }
        }
    }
    
    public virtual void Die()
    {
        isAlive = false;
        animator.SetBool("IsDead", true);
        Destroy(gameObject, 3);
    }

    public virtual void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            isAttacking = true;
            speed = 0;
        }

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
    }

    public virtual void OnTriggerStay2D(Collider2D collision)
    {
        if (skillManager.Thorns)
        {
            if (collision.CompareTag("Thorns"))
            {
                health -= 2 * Time.deltaTime;
            }
        }
        healthBar.value = health;

        if(skillManager.BlackFire)
        {
            if (collision.CompareTag("Burning"))
            {
                if (burn.enabled == true)
                {
                    health -= 5f * Time.deltaTime;
                }
            }
        }
    }

    public IEnumerator Freeze()
    {
        bool wasAttacking = isAttacking;
        speed = 0;
        isAttacking = false;
        GetComponent<SpriteRenderer>().color = new Color(0, 165, 222);

        yield return new WaitForSeconds(1f);

        GetComponent<SpriteRenderer>().color = Color.white;
        isAttacking = wasAttacking;
        speed = DefaultSpeed;
    }

    public IEnumerator SlowDown()
    {
        GetComponent<SpriteRenderer>().color = new Color(0, 165, 222);
        speed *= 0.75f;

        yield return new WaitForSeconds(2F);

        GetComponent<SpriteRenderer>().color = Color.white;
        speed = DefaultSpeed;
    }

    public IEnumerator PoisonDamage()
    {
        while (isAlive)
        {
            yield return new WaitForSeconds(0.5f);
            if (Stagger > 0.5f)
            {
                TakeDamage(Stagger % 10f);
                Stagger -= Stagger % 10f;
                healthBar.GetComponent<Image>().color = Color.green;
            }
            else
            {
                TakeDamage(Stagger);
                Stagger = 0;
                healthBar.GetComponent<Image>().color = new Color(255, 0, 0);
            }
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

    public void IncreasePower()
    {
        maxHealth += 5 * Spawner.WaveNumber;
        damage += 5 * Spawner.WaveNumber;
    }
}

public enum DamageResistance
{
    NORMAL,
    RESIST,
    WEAK,
    IMMUNE
}
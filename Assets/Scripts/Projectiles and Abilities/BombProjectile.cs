using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    public Action OnBombExplosion;
    private SkillManager skillManager;
    [SerializeField] GameObject holyAoe;
    [SerializeField] GameObject cursedAoe;
    [SerializeField] GameObject fireAoe;

    Mortar mortar;
    private string NAME_OF_WEAPON = "MORTAR";

    private bool once = true;

    
    private void Start()
    {
        mortar = FindFirstObjectByType<Mortar>();
        skillManager = GameObject.FindGameObjectWithTag("SkillManager").GetComponent<SkillManager>();
        damage = mortar.projectileDamage;
        Destroy(gameObject, 10.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            transform.GetChild(0).GetComponent<Renderer>().enabled = false;
            transform.GetComponent<Animator>().SetTrigger("Explosion");

            if (once)
            {
                OnBombExplosion?.Invoke();
                once = false;
                GetComponent<CircleCollider2D>().enabled = true;

                if (skillManager.holyBomb)
                {
                    Instantiate(holyAoe, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                }

                if (skillManager.fireBomb)
                {
                    Instantiate(fireAoe, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity);
                }
            }

            enemy.TakeDamage(damage, NAME_OF_WEAPON);

            if (skillManager.knockbackCannonball == true)
            {
                StartCoroutine(KnockbackEnemeis());
            }

            if (skillManager.cursedBomb)
            {
                Instantiate(cursedAoe, new Vector3(transform.position.x, transform.position.y, 1), Quaternion.identity).GetComponent<CursedAoeScript>().FollowedObject = enemy.transform;
            }
        }
    }

    public void DestroyAfterAnimation()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;

        Destroy(gameObject, 0.5f);
    }

    public IEnumerator KnockbackEnemeis()
    {
        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        List<Collider2D> hitColliders = new List<Collider2D>();
        GetComponent<CircleCollider2D>().OverlapCollider(filter2D, hitColliders);
        List<Enemy> enemies = new List<Enemy>();
        foreach (var hitCollider in hitColliders)
        {
            if(hitCollider.GetComponent<Enemy>() != null)
            {
                enemies.Add(hitCollider.GetComponent<Enemy>());
            }
        }

        GetComponent<CircleCollider2D>().enabled = false;

        foreach (Enemy enemy in enemies)
        {
            enemy.GetComponent<Enemy>().speed = 0;

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = enemy.transform.position - transform.position;
                rb.AddForce(direction.normalized * 2.5f, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(0.25F);

        foreach (Enemy enemy in enemies)
        {
            enemy.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (enemy.GetComponent<ShieldEnemy>() != null)
            {
                enemy.GetComponent<Enemy>().speed = 0.5F;
            }
            else
            {
                enemy.GetComponent<Enemy>().speed = 1;
            }
        }
    }
}

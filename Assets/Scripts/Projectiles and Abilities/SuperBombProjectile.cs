using System;
using System.Collections;
using UnityEngine;

public class SuperBombProjectile : Projectile
{
    Mortar mortar;
    Vector3 projectileTarget;
    [SerializeField] float magnetRadius = 5f;
    [SerializeField] float stunRadius = 2f;
    [SerializeField] float destroyTime = 2f;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Sprite ActivatedBombSprite;

    public Action OnSuperBombActivation;
    bool once = true;


    private void Start()
    {
        mortar = FindObjectOfType<Mortar>();
        damage = mortar.projectileDamage;
        projectileTarget = mortar.target;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        if(mortar != null)
        {
            if(gameObject.transform.position != projectileTarget) 
            {
                transform.position = Vector3.MoveTowards(transform.position, projectileTarget, mortar.projectileSpeed * Time.deltaTime);
            } 
            else 
            {
                spriteRenderer.sprite = ActivatedBombSprite;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine(DestroyCoroutine(destroyTime));
                if (once) {
                    OnSuperBombActivation?.Invoke();
                    once = false;
                }
                
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stunRadius);
    }

    private IEnumerator DestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.GetUnstunned();
            enemy.GetUnattracted();
        }

        Destroy(gameObject);
    }
}

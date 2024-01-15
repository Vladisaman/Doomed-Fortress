using System;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    public Action OnArrowHit;

    private readonly string NAME_OF_WEAPON = "Ballista";
    private bool hasEntered = false;

    private void Start()
    {
        Destroy(gameObject, 10f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && !hasEntered) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);

            hasEntered = true;
            Destroy(gameObject);
        }
        

        if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("Rock"))
        {
            Destroy(collision.gameObject);
        }
    }
}

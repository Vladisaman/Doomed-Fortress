using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SuperArrowProjectile : Projectile
{
    Crossbow crossbow;
    private string NAME_OF_WEAPON = "Ballista";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);
        }

        if (collision.CompareTag("Shield"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Rock")
        {
            Destroy(collision.gameObject);

        }

        if (collision.CompareTag("Fast Enemy") )
        {
            var dieComponent = collision.GetComponent<FastEnemy>();

            if (dieComponent != null)
            {
                dieComponent.Die();
            }
        }

        if ( collision.CompareTag("FlyingEnemy"))
        {
            var dieComponent = collision.GetComponent<FlyingEnemy>();

            if (dieComponent != null)
            {
                dieComponent.Die();
            }
        }

        if (collision.tag == "Border")
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        crossbow = GameObject.Find("Crossbow").GetComponent<Crossbow>();
        damage = crossbow.projectileDamage;
        Destroy(gameObject, 10f);
    }
}

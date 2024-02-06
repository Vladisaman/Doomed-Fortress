using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            StartCoroutine(KnockbackEnemeis());
        }
    }

    public IEnumerator KnockbackEnemeis()
    {
        ContactFilter2D filter2D = new ContactFilter2D().NoFilter();
        List<Collider2D> hitColliders = new List<Collider2D>();
        GetComponent<BoxCollider2D>().OverlapCollider(filter2D, hitColliders);
        List<Enemy> enemies = new List<Enemy>();

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.GetComponent<Enemy>() != null)
            {
                enemies.Add(hitCollider.GetComponent<Enemy>());
            }
        }

        foreach (Enemy enemy in enemies)
        {
            enemy.GetComponent<Enemy>().speed = 0;

            Rigidbody2D rb = enemy.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = enemy.transform.position - transform.position;
                rb.AddForce(direction * 5f, ForceMode2D.Impulse);
            }
        }

        yield return new WaitForSeconds(0.05F);

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

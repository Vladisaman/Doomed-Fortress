using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAoeScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("EndZone", 2.5F);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(0.1f);
        }
    }

    public void EndZone()
    {
        GetComponent<CircleCollider2D>().enabled = false;
        GetComponent<Animator>().SetBool("Die", true);

        Destroy(gameObject, 0.5f);
    }
}

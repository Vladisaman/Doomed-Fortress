using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : Projectile
{
    private Animator animator;
    private readonly string NAME_OF_WEAPON = "FIREGUN";
    [SerializeField] private GameObject burnEffect;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        damage = FindObjectOfType<FireGun>().projectileDamage;
        StartCoroutine(Grow());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {

            if (collision.tag == "Enemy With Shield")
            {
                if (collision.GetComponent<ShieldEnemy>().isShieldAlive == false)
                {
                    collision.GetComponent<ShieldEnemy>().TakeDamage(damage, NAME_OF_WEAPON);

                    if (!collision.GetComponent<FireDot>())
                    {
                        collision.gameObject.AddComponent<FireDot>();
                    }
                }
            }
            else
            {
                collision.GetComponent<Enemy>().TakeDamage(damage, NAME_OF_WEAPON);

                if (!collision.GetComponent<FireDot>())
                {
                    collision.gameObject.AddComponent<FireDot>();
                    Instantiate(burnEffect, collision.transform);
                }
            }

            //animator.SetTrigger("isDead");
        }

        if (collision.CompareTag("Border") || collision.CompareTag("Rock"))
        {
            animator.SetTrigger("isDead");
        }
    }

    public IEnumerator Grow()
    {
        int i = 0;
        while (i <= 4)
        {
            yield return new WaitForSeconds(0.2f);
            transform.localScale += new Vector3(0.15f, 0.15f, 0);
            i++;
        }
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}

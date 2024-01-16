using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedAoeScript : MonoBehaviour
{
    public Transform FollowedObject;
    private Coroutine firstBomb;

    // Start is called before the first frame update
    void Start()
    {
        firstBomb = StartCoroutine(FirstBomb());
    }

    // Update is called once per frame
    void Update()
    {
        if (FollowedObject)
        {
            transform.position = FollowedObject.position;
        }
    }

    public IEnumerator FirstBomb()
    {
        yield return new WaitForSeconds(1.25f);
        GetComponent<SpriteRenderer>().enabled = true;
        GetComponent<CircleCollider2D>().enabled = true;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(5f);
        }
    }
}

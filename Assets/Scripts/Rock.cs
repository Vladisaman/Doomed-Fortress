using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    public Transform target;
    public float speed = 5f;
    [SerializeField] protected GameObject _wall;
    [SerializeField] public float damage;

    private void Awake()
    {
        GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,3)];

        target = FindObjectOfType<WallBehavior>().transform;
        _wall = FindObjectOfType<WallBehavior>().gameObject;
    }

    private void Update()
    {
        if (target != null)
        {
            Vector2 targetPosition = target.position;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall")
        {
            Attack();
            Destroy(gameObject);
        }
    }

    public void Attack()
    {
        _wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }
}

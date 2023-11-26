using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractZoneBehavior : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
            if(collision.CompareTag("Fast Enemy") || collision.CompareTag("FlyingEnemy"))
            {
                enemy.GetAttracted();
            }
        }
    }
}

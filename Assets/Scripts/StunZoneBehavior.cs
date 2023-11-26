using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunZoneBehavior : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null)
        {
                enemy.GetStunned();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the other collider has a Rigidbody2D component
        if (other.CompareTag("Enemy"))
        {
            // Calculate the direction from this object to the other object
            Vector2 pushDirection = other.transform.position - transform.position;
            pushDirection.Normalize();

            // Set a force to push the other object away
            float pushForce = 5f; // Adjust the force as needed
            Rigidbody2D otherRigidbody = other.GetComponent<Rigidbody2D>();
            otherRigidbody.AddForce(pushDirection * pushForce, ForceMode2D.Impulse);
        }
    }
}

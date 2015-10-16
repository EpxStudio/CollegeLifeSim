using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D coll)
    {
        // If the Collider2D component is enabled on the object we collided with
        if (coll.collider == true)
        {
            // Disables the Collider2D component
            coll.collider.enabled = false;
        }
    }
}
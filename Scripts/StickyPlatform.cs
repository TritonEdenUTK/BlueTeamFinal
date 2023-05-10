using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Properties of the sticky platform
public class StickyPlatform : MonoBehaviour
{
    // On collision with the platform the player becomes a child of the platform
    // This means the player moves with the platform
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    // When the player moves off of the platform it is not longer a child of the platform
    // This means the player will no longer move with the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}

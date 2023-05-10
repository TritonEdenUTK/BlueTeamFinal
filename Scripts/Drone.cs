using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Drone script figuring out to see how to damage the player
// WIP
public class Drone : MonoBehaviour
{
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Damageable>().Health -= 10;
        }
    }
}

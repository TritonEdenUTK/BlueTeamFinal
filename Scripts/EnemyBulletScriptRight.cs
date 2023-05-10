using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    public float speed = 12f;
    public Rigidbody2D rb;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // if the bullet is on the screen for more than 15 seconds it is destroyed
        if(timer > 15)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // destroy the bullet if it hits the player
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Damageable>().Health -= 20;
            Destroy(gameObject);
        }
    }
}

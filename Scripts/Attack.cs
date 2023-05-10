using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// checks if the current character being hit (using collider) is damageable, if it is, use the hit function to damage
public class Attack : MonoBehaviour
{
    Collider2D attackCollider;
    public int attackDamage = 10;
    public Vector2 knockback = Vector2.zero;

    private void Awake()
    {
        attackCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // See if it can be hit
        Damageable damageable = collision.GetComponent<Damageable>();

        if (damageable != null)
        {
            // Change knockback direction based on where they are facing
            Vector2 deliveredKnockback = transform.parent.localScale.x > 0 ? knockback : new Vector2(-knockback.x, knockback.y);
            // Hit the target
            bool gotHit = damageable.Hit(attackDamage, deliveredKnockback);
            if (gotHit)
                Debug.Log(collision.name + " hit for " + attackDamage);
        }
    }

}

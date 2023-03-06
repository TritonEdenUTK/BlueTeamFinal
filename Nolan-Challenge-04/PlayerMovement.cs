// C# Script used for moving the player left and right and changing directions
// from Josh Code's tutorial: https://www.youtube.com/watch?v=lYYa27baRSk&list=PLyVF7UrIzKrl1eEipJGd-w9U3ssq5qKPq&index=9

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

public class PlayerMovement : MonoBehaviour
{
    // These are necessary for animations and physics
    private Rigidbody2D rb2d;
    private Animator myAnimator;

    // Variables to play with
    public float speed = 2.0f;
    public float horizMovement; // 1 OR -1 OR 0
    public bool facingRight = true;

    // Start is called before the first frame update
    private void Start() 
    {
        // Define the game objects found on the player
        rb2d = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();        
    }

    // handles input for physics
    private void Update()
    {
       // check if the player has input movement
       // check direction given by player 
       horizMovement = Input.GetAxisRaw("Horizontal");
    }
    // handles running the physics
    private void FixedUpdate() {
        // move the character left and right
        rb2d.velocity = new Vector2(horizMovement*speed, rb2d.velocity.y);
        Flip(horizMovement);
        myAnimator.SetFloat("speed", Mathf.Abs(horizMovement));
    }

    // Flipping function
    private void Flip(float horizontal) {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight) {
           facingRight = !facingRight;
           Vector3 theScale = transform.localScale;
           theScale.x *= -1;
           transform.localScale = theScale; 
        }             
    }
}

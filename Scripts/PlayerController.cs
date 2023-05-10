using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// Player controller script where it determines what the player can do and cannot do (setting all of the player's stats as well)
[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Damageable))]

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float airWalkSpeed = 6f;
    public float jumpImpulse = 13f;

    // double jump
    public bool canDoubleJump = false;
    private bool hasDoubleJumped = false;

    Vector2 moveInput;
    TouchingDirections touchingDirections;
    Damageable damageable;

    // determines the move speed of the player
    // Determines what the player can do to move given touching directions and current state of the player
    public float CurrentMoveSpeed { get
        {
            if(CanMove)
            {
                if(IsMoving && !touchingDirections.IsOnWall)
                {
                    if (touchingDirections.IsGrounded)
                    {
                        return walkSpeed;
                    } else
                    {
                        // Air Move
                        return airWalkSpeed;
                    }
                }
                else
                {
                    // Idle spped 0
                    return 0;
                }
            } else 
            {
                // Movement Locked
                return 0;
            }
        }
    }

    // Get boolean values to use for other functions
    [SerializeField]
    private bool _isMoving = false;

    public bool IsMoving { get 
        {
            return _isMoving;
        }
        private set
        {
            _isMoving = value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight { get { return _isFacingRight; } private set {
        if (_isFacingRight != value)
        {
            // Flip the local scale to make the player face the opposite direction
            transform.localScale *= new Vector2(-1, 1);
        }
        
        _isFacingRight = value;

    }}

    public bool CanMove { get
        {
            return animator.GetBool(AnimationStrings.canMove);
        } }

    public bool isAlive
    {
        get
        {
            return animator.GetBool(AnimationStrings.isAlive);
        }
    }

    Rigidbody2D rb;
    Animator animator;

    // Initializes the players rigid body, animator, collisions, and damageable component
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
        damageable = GetComponent<Damageable>();
    }

    // Applies a knockback when the player is hit, otherwise, set the player velocity to normal values
    private void FixedUpdate()
    {
        if (!damageable.IsHit)
            rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    /*void OnAttack()
    {

    }*/

    // function for left and right movement 
    /*void OnAttack()
    {

    }*/

    // OnMove allows for the player to move only when the player is alive and set facing direction to match with the input
    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if (isAlive)
        {
            IsMoving = moveInput != Vector2.zero;
            SetFacingDirection(moveInput);
        }
        else
        {
            IsMoving = false;
        }
    }

    // function to make player face the direction they are moving
    // Check for moveinput x and set the player's direction based on that 
    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            // Face the right
            IsFacingRight = true;
        }
        else if (moveInput.x < 0 && IsFacingRight)
        {
            // Face the left
            IsFacingRight = false;
        }
    }

    // Jump handled in double jump script
    /*public void OnJump(InputAction.CallbackContext context)
    {
        //TODO Check if alive as well
        if(context.started && touchingDirections.IsGrounded && CanMove)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
        }
    }*/

    // Allows the player to jump when the player is grounded and apply double jump if available
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded && CanMove) {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            hasDoubleJumped = false;
        }

        else if (context.started && canDoubleJump && !hasDoubleJumped && CanMove)
         {
            //animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpImpulse);
            hasDoubleJumped = true;
         }
    }

    // function called for the animation while melee attacking
    // sword swing
    public void OnAttack(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            animator.SetTrigger(AnimationStrings.attack);
        }
    }

    // function called for the animation while shooting the gun
    // gun fire
    public void OnRangedAttack(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            animator.SetTrigger(AnimationStrings.rangedAttack);
        }
    }

    // function called when the player is hit by the knights
    // knocback when hit
    public void OnHit(int damage, Vector2 knockback)
    {
        rb.velocity = new Vector2(knockback.x, rb.velocity.y + knockback.y);
    }

    // used for doublejump
    public TouchingDirections TouchingDirections
    {
    get { return touchingDirections; }
    }
}

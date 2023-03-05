using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(TouchingDirections))] 
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpPower = 10f;
    public float airWalkSpeed = 5f;
    Vector2 moveInput;
    TouchingDirections touchingDirections;
    public float CurrentMoveSpeed { 
        get
        {
            if (IsMoving && !touchingDirections.IsOnWall)
            {
                if (touchingDirections.IsGrounded)
                {
                    if (IsRunning)
                    {
                        return runSpeed;
                    }
                    else
                    {
                        return walkSpeed;
                    }
                }
                else
                {
                    // Air move
                    return airWalkSpeed;
                }
            }
            else
            {
                return 0; // not moving (idle)
            }
        } 
    }

    [SerializeField]
    private bool _isMoving = false;
    public bool IsMoving { 
        get
        {
            return _isMoving;
        }
        private set
        {
            _isMoving= value;
            animator.SetBool(AnimationStrings.isMoving, value);
        }
    }

    [SerializeField]
    private bool _isRunning = false;
    public bool IsRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value;
            animator.SetBool(AnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;
    public bool IsFacingRight { get { return _isFacingRight; } private set
        {
            if(_isFacingRight != value)
            {
                // Flip player to opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight= value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.velocity.y);

        animator.SetFloat(AnimationStrings.yVelocity, rb.velocity.y);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        IsMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if(moveInput.x > 0 && !IsFacingRight)
        {
            // Face right
            IsFacingRight = true;
        }
        else if(moveInput.x < 0 && IsFacingRight)
        {
            // Face left
            IsFacingRight = false;
        }
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if(context.started) { 
            IsRunning= true;
        }else if (context.canceled)
        {
            IsRunning= false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        // TODO Check if alive as well
        if(context.started && touchingDirections.IsGrounded)
        {
            animator.SetTrigger(AnimationStrings.jump);
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
        }
    }

}

using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerDash : MonoBehaviour
{
    public float dashSpeed = 20f;
    public float dashDuration = 0.15f;
    public float dashCooldown = 1f;

    private float dashCooldownRemaining;

    private PlayerController playerController;
    private Rigidbody2D rb;
    private Coroutine currentDashRoutine;
    private EchoEffect echoEffect;
    
    // used for dashing during slowed time
    private TimeSlowAbility timeSlowAbility;

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        rb = GetComponent<Rigidbody2D>();
        echoEffect = GetComponent<EchoEffect>();
        timeSlowAbility = GetComponent<TimeSlowAbility>();
    }

    private void Update()
    {
        if (timeSlowAbility.IsTimeSlowActive())
        {
            dashSpeed = 150;
            //Debug.Log(timeSlowAbility.timeScale);
            //dashSpeed /= timeSlowAbility.timeScale;
        }
        else {
            dashSpeed = 20;
         //   dashSpeed *= timeSlowAbility.timeScale;
        }
        if (dashCooldownRemaining > 0)
        {
            dashCooldownRemaining -= Time.unscaledDeltaTime;
        }

    }

    public void OnDash(InputAction.CallbackContext context/*Input.GetKeyDown(KeyCode.B) /*&& canDash*/)
    {
        if (/*Input.GetKeyDown(KeyCode.LeftShift)*/context.started && dashCooldownRemaining <= 0)
        {
            //Debug.Log("Shift pressed!");
            if (currentDashRoutine != null)
            {
                StopCoroutine(currentDashRoutine);
            }
            currentDashRoutine = StartCoroutine(Dash());
        }
    }

    private IEnumerator Dash()
    {
        dashCooldownRemaining = dashCooldown;
        float dashTime = dashDuration;
        playerController.enabled = false;

        while (dashTime > 0)
        {
            rb.velocity = new Vector2(playerController.IsFacingRight ? dashSpeed : -dashSpeed, rb.velocity.y);
            echoEffect.CreateEcho();

            // altered from deltaTime to unscaledDeltaTime
            dashTime -= Time.unscaledDeltaTime;
            yield return null;
        }

        playerController.enabled = true;
        currentDashRoutine = null;
    }

    public float GetDashCooldownRemaining()
    {
    return dashCooldownRemaining;
    }

}
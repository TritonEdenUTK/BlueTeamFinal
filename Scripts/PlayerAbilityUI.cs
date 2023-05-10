using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityUI : MonoBehaviour
{
    public PlayerDash playerDash;
    public Animator dashIconAnimator;

    private void Update()
    {
        UpdateDashIcon();
    }

    private void UpdateDashIcon()
    {
        float dashCooldownRemaining = playerDash.GetDashCooldownRemaining();
        //float dashCooldownDuration = playerDash.dashCooldown;

        if (dashCooldownRemaining > 0)
        {
            dashIconAnimator.SetBool("OnCooldown", true);
        }
        else
        {
            // Make the icon fully opaque if the dash ability is available
            dashIconAnimator.SetBool("OnCooldown", false);
        }
    }
}


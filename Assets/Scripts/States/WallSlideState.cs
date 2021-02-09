using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallSlideState : PlayerState
{
    bool wallOnRight;

    public WallSlideState(bool wallOnRight)
    {
        Debug.Log("wall slide state");
        this.wallOnRight = wallOnRight;
    }

    public void handleInput(PlayerController player)
    {
        if ((Input.GetKey(SettingsInputManager.SIM.right) && !wallOnRight) || (Input.GetKey(SettingsInputManager.SIM.left) && wallOnRight))
        {
            player.hasDoubleJumped = false;
            player.state = new FallingState(false);
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
        {
            player.hasDoubleJumped = false;
            player.state = new JumpingState(wallOnRight);
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.hasDoubleJumped = false;
            player.state = new DashState();
            player.resetDashCooldown();
        }
    }

    public void update(PlayerController player)
    {
        if (wallOnRight)
        {
            if (player.isFacingRight)
            {
                player.flipDirection();
            }
        }
        else
        {
            if (!player.isFacingRight)
            {
                player.flipDirection();
            }
        }
        if (player.isGrounded)
        {
            player.state = new IdleState();
        }
    }
}

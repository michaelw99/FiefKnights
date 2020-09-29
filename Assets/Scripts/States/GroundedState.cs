using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedState : PlayerState
{
    public virtual void handleInput(PlayerController player)
    {
        
    }

    public virtual void update(PlayerController player)
    {

    }

    protected void handleSkillInput(PlayerController player)
    {
        if (Input.GetKey(KeyCode.A))
        {
            // to skill state
            if (Input.GetKey(KeyCode.RightArrow))
            {
                player.state = new SliceNDice_State();
            } 
            else
            {
                player.state = new LeapStrike_State();
            }
        }
    }

    protected void handleBasicInput(PlayerController player)
    {
        // move, jump, dash
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.state = new RunningState();
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            player.state = new JumpingState();
            player.isGrounded = false;
            return;
        }
        else if (Input.GetKeyDown(KeyCode.V) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
            return;
        }
    }
}

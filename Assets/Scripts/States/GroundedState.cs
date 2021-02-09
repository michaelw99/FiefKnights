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
        if (Input.GetKey(SettingsInputManager.SIM.skill) && player.stats.hasMask)
        {
            // to skill state
            if (Input.GetKey(SettingsInputManager.SIM.right))
            {
                player.state = new SliceNDice_State();
            }
            else if (Input.GetKey(SettingsInputManager.SIM.down))
            {
                player.state = new DodgeRoll_State();
            }
            else if (Input.GetKey(SettingsInputManager.SIM.left))
            {
                player.state = new LeapStrike_State();
            }
            else if (Input.GetKey(SettingsInputManager.SIM.up))
            {
                player.state = new BackFlipKick_State();
            }
            else
            {
                player.state = new Counter_State();
            }
        }
    }

    protected void handleBasicInput(PlayerController player)
    {
        // move, jump, dash
        if (Input.GetKey(SettingsInputManager.SIM.right) || Input.GetKey(SettingsInputManager.SIM.left))
        {
            player.state = new RunningState();
            return;
        }
        else if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
        {
            player.state = new JumpingState();
            player.isGrounded = false;
            return;
        }
        else if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
            return;
        }
    }

    protected void checkForFinisher(PlayerController player)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(player.transform.position, 2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (Input.GetKeyDown(SettingsInputManager.SIM.finisher))
                {
                    player.state = new FinisherState(hitCollider.gameObject);
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    
    public RunningState()
    {
        // Debug.Log("running state");
    }

    public override void handleInput(PlayerController player)
    {
        handleSkillInput(player);
        checkForFinisher(player);
        // Combat states
        if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack))
        {
            // light attack state L
            player.state = player.currentMask.getLState();
        }
        else if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack))
        {
            // heavy attack state H
            player.state = player.currentMask.getHState();
        }

        if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
        {
            player.state = new JumpingState();
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        else if (Input.GetKey(SettingsInputManager.SIM.right))
        {
            //player.rb.velocity = (Vector3.right * player.MOVE_SPEED * Time.deltaTime);
            player.rb.AddForce(Vector3.right * player.MOVE_SPEED * Time.deltaTime);
            if (!player.isFacingRight)
            {
                player.flipDirection();
            }
        } else if (Input.GetKey(SettingsInputManager.SIM.left))
        {
            //player.rb.velocity = (Vector3.left * player.MOVE_SPEED * Time.deltaTime);
            player.rb.AddForce(Vector3.left * player.MOVE_SPEED * Time.deltaTime);
            if (player.isFacingRight)
            {
                player.flipDirection();
            }
        } else
        {
            player.state = new IdleState();
        }
    }

    public void update(PlayerController player)
    {

    }

}

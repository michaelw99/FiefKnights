using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{

    public IdleState()
    {
        // Debug.Log("Idle state");
    }

    public override void handleInput(PlayerController player)
    {
        // skill input
        handleSkillInput(player);
        checkForFinisher(player);
        // Combat states
        if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack))
        {
            // light attack state L
            player.state = player.currentMask.getLState();
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack))
        {
            // heavy attack state H
            player.state = player.currentMask.getHState();
        }

        // Movement states
        player.rb.velocity = new Vector3();
        if (Input.GetKey(SettingsInputManager.SIM.right) || Input.GetKey(SettingsInputManager.SIM.left))
        {
            player.state = new RunningState();
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
        {
            player.state = new JumpingState();
        } else if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
    }

    public void update(PlayerController player)
    {

        // do nothing
    }

}

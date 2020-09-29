using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : GroundedState
{

    public IdleState()
    {
        Debug.Log("Idle state");
    }

    public override void handleInput(PlayerController player)
    {
        // skill input
        handleSkillInput(player);
        // Combat states
        if (Input.GetKeyDown(KeyCode.X))
        {
            // light attack state L
            player.state = new L_State();
        } else if (Input.GetKeyDown(KeyCode.C))
        {
            // heavy attack state H
            player.state = new H_State();
        }

        // Movement states
        player.rb.velocity = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.state = new RunningState();
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            player.state = new JumpingState();
        } else if (Input.GetKeyDown(KeyCode.V) && player.canDash())
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

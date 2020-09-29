using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : GroundedState
{
    
    public RunningState()
    {
        Debug.Log("running state");
    }

    public override void handleInput(PlayerController player)
    {
        handleSkillInput(player);
        // Combat states
        if (Input.GetKeyDown(KeyCode.X))
        {
            // light attack state L
            player.state = new L_State();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            // heavy attack state H
            player.state = new H_State();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.state = new JumpingState();
        } else if (Input.GetKeyDown(KeyCode.V) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            //player.rb.velocity = (Vector3.right * player.MOVE_SPEED * Time.deltaTime);
            player.rb.AddForce(Vector3.right * player.MOVE_SPEED * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            //player.rb.velocity = (Vector3.left * player.MOVE_SPEED * Time.deltaTime);
            player.rb.AddForce(Vector3.left * player.MOVE_SPEED * Time.deltaTime);
        } else
        {
            player.state = new IdleState();
        }
    }

    public void update(PlayerController player)
    {

    }

}

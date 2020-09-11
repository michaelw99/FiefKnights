using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningState : PlayerState
{
    public void handleInput(PlayerController player)
    {
        Debug.Log("Running state");
        if (Input.GetKeyDown(KeyCode.Z))
        {
            player.state = new JumpingState();
            player.isGrounded = false;
        } else if (Input.GetKeyDown(KeyCode.V) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            player.rb.velocity = (Vector3.right * player.MOVE_SPEED * Time.deltaTime);
        } else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.rb.velocity = (Vector3.left * player.MOVE_SPEED * Time.deltaTime);
        } else
        {
            player.state = new IdleState();
        }
    }

    public void update()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : PlayerState
{
    public void handleInput(PlayerController player)
    {
        Debug.Log("Idle state");
        player.rb.velocity = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
        {
            player.state = new RunningState();
        } else if (Input.GetKeyDown(KeyCode.Z))
        {
            player.state = new JumpingState();
            player.isGrounded = false;
        } else if (Input.GetKeyDown(KeyCode.V))
        {
            player.state = new DashState();
        }
    }

    public void update()
    {
        // do nothing
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    public void handleInput(PlayerController player)
    {
        //Debug.Log("Falling state");
        if (player.isGrounded)
        {
            player.state = new IdleState();
            player.hasDoubleJumped = false;
        }
        if (Input.GetKeyDown(KeyCode.Z) && !player.hasDoubleJumped)
        {
            player.state = new JumpingState();
            player.hasDoubleJumped = true;
        }
        if (Input.GetKeyDown(KeyCode.V) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        Vector3 vector = new Vector3();
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector += Vector3.right * player.MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector += Vector3.left * player.MOVE_SPEED * Time.deltaTime;
        }
        vector += Vector3.down * player.MOVE_SPEED * Time.deltaTime;
        player.rb.velocity = vector;
    }

    public void update()
    {

    }
}

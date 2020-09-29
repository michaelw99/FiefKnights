using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    private float jumpTime = 0.3f;

    public JumpingState()
    {
        Debug.Log("Jumping state");
    }

    public void handleInput(PlayerController player)
    {
        Vector3 vector = new Vector3();
        if (Input.GetKeyUp(KeyCode.Z) || jumpTime <= 0)
        {
            player.state = new FallingState();
        }
        if (Input.GetKeyDown(KeyCode.V) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            vector += Vector3.right * player.MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            vector += Vector3.left * player.MOVE_SPEED * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Z))
        {
            vector += Vector3.up * player.JUMP_SPEED * Time.deltaTime;
            //player.rb.velocity = vector;
            player.rb.AddForce(vector);
        } 
    }

    public void update(PlayerController player)
    {
        jumpTime -= Time.deltaTime;
    }
}

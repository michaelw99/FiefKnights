using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private float dashTime = 0.2f;
    private float DASH_SPEED = 10000f;

    public void handleInput(PlayerController player)
    {
        if (dashTime <= 0f)
        {
            if (!player.isGrounded)
            {
                player.state = new FallingState();
            } else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.LeftArrow))
            {
                player.state = new RunningState();
            }
            else
            {
                player.state = new IdleState();
            }
        } else
        {
            if (player.isFacingRight)
            {
                player.rb.velocity = (Vector3.right * DASH_SPEED * Time.deltaTime);
            } else
            {
                player.rb.velocity = (Vector3.left * DASH_SPEED * Time.deltaTime);
            }
            
        }
    }

    public void update()
    {
        dashTime -= Time.deltaTime;
    }
}

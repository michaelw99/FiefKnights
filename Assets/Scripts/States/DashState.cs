using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private float stateTime = 0f;
    private float dashStopTime = 0.15f;
    private float velocityStopTime = 0.18f;
    private float stateEndTime = 0.2f;
    private bool dashStarted = false;

    public DashState()
    {
        Debug.Log("dash state");
    }

    public void handleInput(PlayerController player)
    {
        if (stateTime >= stateEndTime)
        {
            if (!player.isGrounded)
            {
                player.state = new FallingState();
            } else if (Input.GetAxisRaw("Horizontal") != 0)
            {
                player.state = new RunningState();
            }
            else
            {
                player.state = new IdleState();
            }
        }
    }

    public void update(PlayerController player)
    {
        stateTime += Time.deltaTime;

        if (stateTime >= velocityStopTime)
        {
            player.rb.velocity = Vector3.zero;
        }
        else
        {
            // stop all momentum first
            if (!dashStarted)
            {
                player.rb.velocity = Vector3.zero;
                dashStarted = true;
            }

            if (player.isFacingRight)
            {
                player.rb.velocity = (Vector3.right * player.DASH_SPEED);
                //player.rb.AddForce(Vector3.right * player.DASH_SPEED * Time.deltaTime);
            }
            else
            {
                player.rb.velocity = (Vector3.left * player.DASH_SPEED);
                //player.rb.AddForce(Vector3.left * player.DASH_SPEED * Time.deltaTime);
            }

        }
    }
}

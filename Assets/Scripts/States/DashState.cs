using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : PlayerState
{
    private float stateTime = 0f;
    private float dashStopTime = 0.2f;
    private float velocityStopTime = 0.18f;

    public DashState()
    {
        Debug.Log("dash state");
    }

    public void handleInput(PlayerController player)
    {
        if (stateTime >= dashStopTime)
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
        }
        else if (stateTime >= velocityStopTime) {
            player.rb.velocity = Vector3.zero;
        }
        else
        {
            if (player.isFacingRight)
            {
                player.rb.velocity = (Vector3.right * player.DASH_SPEED * Time.deltaTime);
                //player.rb.AddForce(Vector3.right * player.DASH_SPEED * Time.deltaTime, ForceMode2D.Impulse);
            } else
            {
                player.rb.velocity = (Vector3.left * player.DASH_SPEED * Time.deltaTime);
                //player.rb.AddForce(Vector3.left * player.DASH_SPEED * Time.deltaTime, ForceMode2D.Impulse);
            }

        }
    }

    public void update(PlayerController player)
    {
        stateTime += Time.deltaTime;
    }
}

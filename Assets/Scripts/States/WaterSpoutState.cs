using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpoutState : PlayerState
{

    GameObject waterSpout;
    WaterSpoutController waterSpoutController;

    public bool hasHighJumped = false;

    public WaterSpoutState(GameObject waterSpout)
    {
        this.waterSpout = waterSpout;
        this.waterSpoutController = waterSpout.GetComponent<WaterSpoutController>();
        Debug.Log("WaterSpoutState state");
    }

    public void handleInput(PlayerController player)
    {
        // can move only dash or short jump if haven't triggered high jump
        if (!hasHighJumped)
        {
            if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
            {
                player.transform.parent = null;
                player.resetDashCooldown();
                player.state = new DashState();
            }
            if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
            {
                // when water spout in jump window, player can jump high
                player.transform.parent = null;
                if (waterSpout.GetComponent<WaterSpoutController>().isJumpWindow())
                {
                    hasHighJumped = true;
                }
                else
                {
                    player.state = new ShortJumpingState();
                }
            }
        }
        
        // 
        Vector3 vector = new Vector3();
        if (Input.GetKey(SettingsInputManager.SIM.right))
        {
            vector += Vector3.right * player.MOVE_SPEED * Time.deltaTime;
            if (!player.isFacingRight)
            {
                player.flipDirection();
            }
        }
        if (Input.GetKey(SettingsInputManager.SIM.left))
        {
            vector += Vector3.left * player.MOVE_SPEED * Time.deltaTime;
            if (player.isFacingRight)
            {
                player.flipDirection();
            }
        }

        player.rb.AddForce(vector);
    }

    public void update(PlayerController player)
    {
        if (hasHighJumped)
        {
            if (waterSpoutController.currTime >= waterSpoutController.highJumpTime)
            {
                player.transform.parent = null;
                player.state = new HighJumpingState();
            }
        }
    }
}

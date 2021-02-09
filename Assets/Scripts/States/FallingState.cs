using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingState : PlayerState
{
    private bool willFloat = true;
    private float floatTime = 0.15f;
    private float currTime = 0f;
    private float wallJumpResetTime = 0.5f; // half a second wait
    private float FLOAT_SPEED = 100f;

    public FallingState()
    {
        // Debug.Log("Falling state");
    }

    public FallingState(bool willFloat) // for when we dont want float, like leaving a wall without jumping
    {
        Debug.Log("Falling state with no float");
        this.willFloat = willFloat;
    }

    public void handleInput(PlayerController player)
    {
        if (player.isGrounded)
        {
            player.state = new IdleState();
            player.hasDoubleJumped = false;
        }
        if (Input.GetKeyDown(SettingsInputManager.SIM.jump) && !player.hasDoubleJumped && player.stats.hasDoubleJump)
        {
            player.state = new JumpingState();
            player.hasDoubleJumped = true;
        }
        if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
        if (Input.GetKey(SettingsInputManager.SIM.lightattack) && Input.GetKey(SettingsInputManager.SIM.down))
        {
            player.state = new PlungeAttack_State();
        }
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
        if (currTime <= floatTime && willFloat)
        {
            vector += Vector3.down * FLOAT_SPEED * Time.deltaTime;
        }
        else
        {
            vector += Vector3.down * player.JUMP_SPEED * Time.deltaTime;
        }
        
        //player.rb.velocity = vector;
        player.rb.AddForce(vector);
    }

    public void update(PlayerController player)
    {
        currTime += Time.deltaTime;
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighJumpingState : PlayerState
{

    private float jumpTime = 0.4f; 
    private bool hasJumped = false;

    public HighJumpingState()
    {
        Debug.Log("HighJumpingState state");
    }

    public void handleInput(PlayerController player)
    {
        Vector3 vector = new Vector3();
        if (Input.GetKeyUp(SettingsInputManager.SIM.jump) || jumpTime <= 0)
        {
            player.state = new FallingState();
        }
        if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.state = new DashState();
            player.resetDashCooldown();
        }
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
        if (Input.GetKey(SettingsInputManager.SIM.lightattack) && Input.GetKey(SettingsInputManager.SIM.down))
        {
            player.state = new PlungeAttack_State();
            return;
        }
        if (Input.GetKey(SettingsInputManager.SIM.jump))
        {
            vector += Vector3.up * player.JUMP_SPEED * Time.deltaTime;
            //player.rb.velocity = vector;
            if (!hasJumped)
            {
                // normal jump launch
                player.rb.AddForce(Vector3.up * 15, ForceMode2D.Impulse);
                hasJumped = true;
            }
            else
            {
                player.rb.AddForce(vector);
            }
        }
    }

    public void update(PlayerController player)
    {
        jumpTime -= Time.deltaTime;
    }

    private float getJumpVelocity(float t)
    {
        // some  equations lol
        return 0f;
    }
}
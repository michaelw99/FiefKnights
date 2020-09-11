﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingState : PlayerState
{
    bool hasDoubleJumped;
    private float jumpTime = 0.5f;
    public JumpingState()
    {
        hasDoubleJumped = false;
    }

    public JumpingState(bool hasDoubleJumped)
    {
        this.hasDoubleJumped = hasDoubleJumped;
    }

    public void handleInput(PlayerController player)
    {
        Debug.Log("Jumping state");
        Vector3 vector = new Vector3();
        if (Input.GetKeyUp(KeyCode.Z) || jumpTime <= 0)
        {
            player.state = new FallingState(hasDoubleJumped);
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
            vector += Vector3.up * player.MOVE_SPEED * Time.deltaTime;
            player.rb.velocity = vector;
        } 
    }

    public void update()
    {
        jumpTime -= Time.deltaTime;
    }
}

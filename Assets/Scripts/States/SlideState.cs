using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideState : PlayerState
{

    public float slantFactor;
    public float slideSpeed = 10f;

    private Vector3 slideVector;

    public SlideState()
    {
        Debug.Log("slidestate state");
        slideVector = new Vector3(1.0f, slantFactor);
    }

    public void handleInput(PlayerController player)
    {
        // can jump, coming off slide with momentum
        // can dash off as well
        // left and right don't do anything (maybe kinda control speed?)
        if (Input.GetKeyDown(SettingsInputManager.SIM.dash) && player.canDash())
        {
            player.resetDashCooldown();
            player.state = new DashState();
        }
        if (Input.GetKeyDown(SettingsInputManager.SIM.jump))
        {
            // jump launches in forward direction
            player.state = new JumpingState();
        }
    }

    public void update(PlayerController player)
    {
        player.rb.AddForce(slideVector * slideSpeed * Time.deltaTime);
    }
}

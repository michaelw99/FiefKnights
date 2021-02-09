using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter_State : GroundedState
{
    float MP_COST = 25;
    float animationEnd = 0.4f; 

    float stateTime = 0f;
    float counterStart = 0.05f;
    float counterEnd = 0.3f;

    bool hasUsedMp = false;

    public Counter_State()
    {
        Debug.Log("Counter_State state");
    }

    public override void handleInput(PlayerController player)
    {
       // counter does not allow buffering
       
    }

    public override void update(PlayerController player)
    {
        stateTime += Time.deltaTime;

        // deduct MP cost
        if (!hasUsedMp)
        {
            player.useMp(MP_COST);
            hasUsedMp = true;
        }

        if (stateTime >= counterStart && !player.isCountering)
        {
            player.isCountering = true;
        } else if (stateTime >= counterEnd)
        {
            player.isCountering = false;
            player.state = new IdleState(); // player will go back to idle state after counter exits
        }
    }
}

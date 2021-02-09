using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitState : GroundedState
{
    float LAUNCH_FORCE = 4f;
    private float normalStunDuration = 0.2f;
    private float environmentStunDuration = 0.9f;
    private float invinciblityTime = 1f;
    private float stateTime = 0f;
    private bool hasLaunched = false;
    private bool hitFromRight;
    private bool environmentHit;

    public HitState(bool hitFromRight, bool environmentHit)
    {
        this.hitFromRight = hitFromRight;
        this.environmentHit = environmentHit;
        Debug.Log("HitState state");
        Debug.Log(hitFromRight);
    }

    public override void handleInput(PlayerController player)
    {
        // player can't act after being hit
    }

    public override void update(PlayerController player)
    {
        // launch player backwards
        if (!hasLaunched)
        {
            Debug.Log("hurt launching");
            hasLaunched = true;
            if (environmentHit)
            {
                // do nothing
            } else
            {
                if (hitFromRight)
                {
                    player.rb.AddForce(new Vector3(-1f, 0.3f) * LAUNCH_FORCE, ForceMode2D.Impulse);
                }
                else
                {
                    player.rb.AddForce(new Vector3(1f, 0.3f) * LAUNCH_FORCE, ForceMode2D.Impulse);
                }
            }
            
        }

        // if player is not grounded, fall
        if (!player.isGrounded)
        {
            player.rb.AddForce(Vector3.down * player.JUMP_SPEED * Time.deltaTime);
        }

        stateTime += Time.deltaTime;
        player.setInvincibilty(invinciblityTime);

        // check if is enviro damage or enemy damage
        float stunDuration;
        if (environmentHit)
        {
            stunDuration = environmentStunDuration;
        } else
        {
            stunDuration = normalStunDuration;
        }

        if (stateTime >= stunDuration)
        {
            if (player.isGrounded)
            {
                player.state = new IdleState();
            } else
            {
                player.state = new FallingState(true);
            }
        }
    }
}

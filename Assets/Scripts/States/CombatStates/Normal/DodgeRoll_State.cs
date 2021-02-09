using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeRoll_State : GroundedState
{
    float ROLL_FORCE = 5f;
    float MP_COST = 20;

    float bufferedInputStart = 0.85f;
    float bufferedInputTrigger = 0.95f;
    float animationEnd = 1.0f;

    float invincibilityStart = 0.05f;
    float invincibilityEnd = 0.7f;
    float rollStart = 0.1f;
    float stopVelocityTime = 0.95f;
    float stateTime = 0f;

    bool hasRolled = false;
    bool hasUsedMp = false;

    public DodgeRoll_State()
    {
        Debug.Log("DodgeRoll_State state");
    }

    public override void handleInput(PlayerController player)
    {
        if (stateTime < bufferedInputStart)
        {
            // do nothing, or make sure buffer is empty
        }
        else if (stateTime < bufferedInputTrigger)
        {
            // buffered inputs are attacks only
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L, L
            {
                player.buffer.takeInput(SettingsInputManager.SIM.lightattack);
            }
        }
        else if (stateTime >= bufferedInputTrigger && stateTime <= animationEnd)
        {
            KeyCode bufferedInput = KeyCode.None;
            if (player.buffer.bufferNotEmpty())
            {
                Debug.Log("Buffer not empty");
                bufferedInput = player.buffer.getInput();
                player.buffer.clearBuffer();
            }

            if (bufferedInput == SettingsInputManager.SIM.lightattack) // L, L
            {
                player.state = new LL_State();
                return;
            }

            // start listening for any input
            handleBasicInput(player);
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L, L
            {
                player.state = new LL_State();
            }
        }
        else if (stateTime >= animationEnd)
        {
            player.state = new IdleState();
        }

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

        // launch the player
        if (stateTime >= invincibilityStart && !player.isInvincible())
        {
            player.setInvincibilty(0.7f);
        }
        else if (stateTime >= rollStart && !hasRolled)
        {
            hasRolled = true;
            player.ignoreEnemyCollision();
            player.rb.velocity = Vector3.zero;
            if (player.isFacingRight)
            {
                player.rb.AddForce(new Vector3(1, 0) * ROLL_FORCE, ForceMode2D.Impulse);
            }
            else
            {
                player.rb.AddForce(new Vector3(-1, 0) * ROLL_FORCE, ForceMode2D.Impulse);
            }
        }
        else if (stateTime >= invincibilityEnd)
        {
            player.allowEnemyCollision();
        }
        else if (stateTime >= stopVelocityTime)
        {
            //player.rb.velocity = Vector3.zero;

        }

    }
}

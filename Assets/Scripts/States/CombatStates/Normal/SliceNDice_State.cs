using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceNDice_State : GroundedState
{
    float ATK_MOVE = 500f;
    float LAUNCH_FORCE = 18f;
    float MP_COST = 25;

    float bufferedInputStart = 0.5f;
    float bufferedInputTrigger = 0.65f;
    float animationEnd = 0.8f; 

    float launchStart = 0.1f;
    float hitboxStart = 0.4f;
    float hitboxDuration = 0.2f;
    float stopVelocityTime = 0.6f;
    float stateTime = 0f;
    float attackDistance = 1.7f;

    GameObject hitbox;
    bool hitboxHasSpawned = false;
    bool hasLaunched = false;
    bool hasUsedMp = false;

    public SliceNDice_State()
    {
        Debug.Log("SliceNDice_State state");
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
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L
            {
                player.buffer.takeInput(SettingsInputManager.SIM.lightattack);
            }
            else if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack)) // H
            {
                player.buffer.takeInput(SettingsInputManager.SIM.heavyattack);
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

            if (bufferedInput == SettingsInputManager.SIM.lightattack) // L
            {
                player.state = player.currentMask.getLState();
                return;
            }
            else if (bufferedInput == SettingsInputManager.SIM.heavyattack) // H
            {
                player.state = player.currentMask.getHState();
                return;
            }

            // start listening for any input
            handleBasicInput(player);
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L
            {
                player.state = player.currentMask.getLState();
            }
            else if (bufferedInput == SettingsInputManager.SIM.heavyattack) // H
            {
                player.state = player.currentMask.getHState();
                return;
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
        if (stateTime >= launchStart && !hasLaunched)
        {
            player.ignoreEnemyCollision();
            player.rb.velocity = Vector3.zero;
            hasLaunched = true;
            if (player.isFacingRight)
            {
                player.rb.AddForce(new Vector3(1, 0) * LAUNCH_FORCE, ForceMode2D.Impulse);
            }
            else
            {
                player.rb.AddForce(new Vector3(-1, 0) * LAUNCH_FORCE, ForceMode2D.Impulse);
            }
        }
        // spawn hitbox nest to player and move player slightly forward
        if (stateTime >= hitboxStart && !hitboxHasSpawned)
        {
            hitboxHasSpawned = true;
            if (player.isFacingRight)
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("NormalHitBoxes/SliceNDice_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.right * ATK_MOVE * Time.deltaTime);
                //player.rb.AddForce(Vector3.right * ATK_MOVE, ForceMode2D.Impulse);
            }
            else
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("NormalHitBoxes/SliceNDice_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.left * ATK_MOVE * Time.deltaTime);
                //player.rb.AddForce(Vector3.left * ATK_MOVE, ForceMode2D.Impulse);
            }
        }
        else if (stateTime >= stopVelocityTime)
        {
            //player.rb.velocity = Vector3.zero;
            player.allowEnemyCollision();
        }

    }
}
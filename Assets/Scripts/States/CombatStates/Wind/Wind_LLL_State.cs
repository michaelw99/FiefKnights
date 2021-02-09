using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind_LLL_State : GroundedState
{
    float ATK_MOVE = 0.9f;

    float bufferedInputStart = 0.2f;
    float bufferedInputTrigger = 0.4f;
    float animationEnd = 0.6f;

    float hitboxStart = 0.2f;
    float hitboxDuration = 0.3f;
    float hitbox2Start = 0.25f;
    float hitbox2Duration = 0.3f;

    float stopVelocityTime = 0.15f;
    float stateTime = 0f;
    float attackDistance = 0.35f;
    float attackDistance2 = 0.45f;

    GameObject hitbox;
    GameObject hitbox2;
    bool hitboxHasSpawned = false;
    bool hitbox2HasSpawned = false;

    public Wind_LLL_State()
    {
        Debug.Log("Wind_LLL_State state");
    }

    public override void handleInput(PlayerController player)
    {
        // handle skill cancels
        handleSkillInput(player);
        checkForFinisher(player);

        if (stateTime < bufferedInputStart)
        {
            // do nothing, or make sure buffer is empty
        }
        else if (stateTime < bufferedInputTrigger)
        {
            // buffered inputs are attacks only
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L, L, L, L
            {
                player.buffer.takeInput(SettingsInputManager.SIM.lightattack);
            }
            else if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack)) // L, L, L, H
            {
                player.buffer.takeInput(SettingsInputManager.SIM.heavyattack);
            }
        }
        else if (stateTime >= bufferedInputTrigger && stateTime <= animationEnd)
        {
            KeyCode bufferedInput = KeyCode.None;
            if (player.buffer.bufferNotEmpty())
            {
                bufferedInput = player.buffer.getInput();
                player.buffer.clearBuffer();
            }
            if (bufferedInput == SettingsInputManager.SIM.lightattack) // L, L, L, L
            {
                player.state = player.currentMask.getLLLLState();
                return;
            }
            else if (bufferedInput == SettingsInputManager.SIM.heavyattack) // L, L, L, H
            {
                player.state = player.currentMask.getLLLHState();
                return;
            }

            // start listening for any input
            handleBasicInput(player);
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L, L, L, L
            {
                player.state = player.currentMask.getLLLLState();
            }
            else if (bufferedInput == SettingsInputManager.SIM.heavyattack) // L, L, L, H
            {
                player.state = player.currentMask.getLLLHState();
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

        // spawn hitbox nest to player and move player slightly forward
        if (stateTime >= hitboxStart && !hitboxHasSpawned)
        {
            hitboxHasSpawned = true;
            if (player.isFacingRight)
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("WindHitBoxes/Wind_LLL_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.right * ATK_MOVE * Time.deltaTime);
                player.rb.AddForce(Vector3.right * ATK_MOVE, ForceMode2D.Impulse);
            }
            else
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("WindHitBoxes/Wind_LLL_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.left * ATK_MOVE * Time.deltaTime);
                player.rb.AddForce(Vector3.left * ATK_MOVE, ForceMode2D.Impulse);
            }
        }
        if (stateTime >= hitbox2Start && !hitbox2HasSpawned)
        {
            hitbox2HasSpawned = true;
            if (player.isFacingRight)
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(attackDistance2, 0, 0f);
                player.spawnHitbox(Resources.Load("WindHitBoxes/Wind_LLL_HitBox2"), spawnPoint, player.gameObject.transform.rotation, hitbox2Duration);
            }
            else
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance2, 0, 0f);
                player.spawnHitbox(Resources.Load("WindHitBoxes/Wind_LLL_HitBox2"), spawnPoint, player.gameObject.transform.rotation, hitbox2Duration);
            }
        }
        else if (stateTime >= stopVelocityTime)
        {
            //player.rb.velocity = Vector3.zero;
        }

    }
}

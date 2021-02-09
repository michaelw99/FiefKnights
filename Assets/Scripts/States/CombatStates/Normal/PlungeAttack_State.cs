using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlungeAttack_State : GroundedState
{
    float PLUNGE_SPEED = 6500f;

    float bufferedInputStart = 0.6f;
    float bufferedInputTrigger = 0.8f;
    float animationEnd = 0.9f;
    float hitboxStart = 0.3f;
    float hitboxDuration = 0.35f;
    float stateTime = 0f;
    float floatTime = 0.3f;
    float attackDistance = 0.45f;

    GameObject hitbox;
    bool hitboxHasSpawned = false;

    public PlungeAttack_State()
    {
        Debug.Log("PlungeAttack_State state");
    }

    public override void handleInput(PlayerController player)
    {
        // can't skill midair
        if (stateTime < bufferedInputStart)
        {
            // do nothing, or make sure buffer is empty
        }
        else if (stateTime < bufferedInputTrigger)
        {
            // buffered inputs are attacks only
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L
            {
                // light attack state L
                player.buffer.takeInput(SettingsInputManager.SIM.lightattack);
            }
            else if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack)) // H
            {
                player.buffer.takeInput(SettingsInputManager.SIM.heavyattack);
            }
        }
        else if (stateTime >= bufferedInputTrigger && player.isGrounded)
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

            // start listening for any input AND skill input
            handleSkillInput(player);
            handleBasicInput(player);
            if (Input.GetKeyDown(SettingsInputManager.SIM.lightattack)) // L
            {
                player.state = player.currentMask.getLState();
            }
            if (Input.GetKeyDown(SettingsInputManager.SIM.heavyattack)) // H
            {
                player.state = player.currentMask.getHState();
            }
        }
        

    }

    public override void update(PlayerController player)
    {
        stateTime += Time.deltaTime;
        if (stateTime <= floatTime)
        {
            player.rb.velocity = Vector3.zero;
        }

        // spawn hitbox nest to player and move player slightly forward
        //if (stateTime >= hitboxStart && !hitboxHasSpawned)
        //{
            //hitboxHasSpawned = true;
            //if (player.isFacingRight)
            //{
            //    Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(attackDistance, 0, 0f);
            //    player.spawnHitbox(Resources.Load("NormalHitBoxes/H_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
            //}
            //else
            //{
            //    Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance, 0, 0f);
            //    player.spawnHitbox(Resources.Load("NormalHitBoxes/H_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
            //}
        //}
        if (stateTime >= floatTime)
        {
            player.rb.AddForce(Vector3.down * PLUNGE_SPEED * Time.deltaTime);
        }
        if (player.isGrounded)
        {
            Debug.Log("hitboxes comin");
            if (player.isFacingRight)
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("NormalHitBoxes/H_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
            }
            else
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("NormalHitBoxes/H_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
            }
            player.state = new IdleState();
        }

    }
}

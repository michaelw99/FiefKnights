using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LLL_State : GroundedState
{
    float ATK_MOVE = 0.3f;

    float bufferedInputStart = 0.05f;
    float bufferedInputTrigger = 0.3f;
    float animationEnd = 0.4f;
    float hitboxStart = 0.05f;
    float hitboxDuration = 0.15f;
    float stopVelocityTime = 0.1f;
    float stateTime = 0f;
    float attackDistance = 0.4f;

    GameObject hitbox;
    bool hitboxHasSpawned = false;

    public LLL_State()
    {
        Debug.Log("LLL_State state");
    }


    public override void handleInput(PlayerController player)
    {
        // handle skill cancels
        handleSkillInput(player);

        if (stateTime < bufferedInputStart)
        {
            // do nothing, or make sure buffer is empty
        }
        else if (stateTime < bufferedInputTrigger)
        {
            // buffered inputs are attacks only
            if (Input.GetKeyDown(KeyCode.X)) // L, L, L, L
            {
                player.buffer.takeInput(KeyCode.X);
            }
            else if (Input.GetKeyDown(KeyCode.C)) // L, L, L, H
            {
                player.buffer.takeInput(KeyCode.C);
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

            if (bufferedInput == KeyCode.X) // L, L, L, L
            {
                player.state = new LLLL_State(); 
                return;
            }
            else if (bufferedInput == KeyCode.C) // L, L, L, H
            {
                player.state = new LLLH_State();
                return;
            }

            // start listening for any input
            handleBasicInput(player);
            if (Input.GetKeyDown(KeyCode.X)) // L, L, L, L
            {
                player.state = new LLLL_State();
            }
            else if (Input.GetKeyDown(KeyCode.C)) // L, L, L, H
            {
                player.state = new LLLH_State();
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
                player.spawnHitbox(Resources.Load("LLL_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.right * ATK_MOVE * Time.deltaTime);
                player.rb.AddForce(Vector3.right * ATK_MOVE, ForceMode2D.Impulse);
            }
            else
            {
                Vector3 spawnPoint = player.gameObject.transform.position + new Vector3(-attackDistance, 0, 0f);
                player.spawnHitbox(Resources.Load("LLL_HitBox"), spawnPoint, player.gameObject.transform.rotation, hitboxDuration);
                //player.rb.velocity = (Vector3.left * ATK_MOVE * Time.deltaTime);
                player.rb.AddForce(Vector3.left * ATK_MOVE, ForceMode2D.Impulse);
            }
        }
        else if (stateTime >= stopVelocityTime)
        {
            //player.rb.velocity = Vector3.zero;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class FinisherState : PlayerState
{
    private GameObject enemy;
    private bool noCollisionTriggered = false;
    private float stateTime = 0;
    private float animationLength = 1f;

    public FinisherState(GameObject enemy)
    {
        this.enemy = enemy;
        Debug.Log("Finisher state");

        CustomEvent.Trigger(enemy, "Death");
    }

    public void handleInput(PlayerController player)
    {
        // no inputs taken
    }

    public void update(PlayerController player)
    {
        stateTime += Time.deltaTime;
        if (!noCollisionTriggered)
        {
            player.ignoreEnemyCollision();
            noCollisionTriggered = true;
        }
        player.gameObject.transform.position = Vector2.MoveTowards(player.gameObject.transform.position, enemy.transform.position, 0.2f);
        if (stateTime >= animationLength)
        {
            player.allowEnemyCollision();
            player.state = new IdleState();
        }
    }


}

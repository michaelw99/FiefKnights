using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpoutController2 : MonoBehaviour
{

    public GameObject player;

    public float rotation;
    public float riseRate = 5f;
    public float jumpWindowStartTime = 2.3f;
    public float highJumpTime = 2.5f; // when the player will high jump (probably same as drop time)
    public float jumpWindowEndTime = 2.6f;
    public float idleTime = 2f;
    public float dropTime = 2.5f;
    public float currTime = 0;
    private Vector3 startingPosition;

    private bool playerTouching = false;

    // Start is called before the first frame update
    void Start()
    {
        startingPosition = gameObject.transform.localScale;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime <= dropTime && currTime >= idleTime) // move spout object
        {
            Vector3 currScale = gameObject.transform.localScale;
            gameObject.transform.localScale = new Vector3(currScale.x, currScale.y + riseRate * Time.deltaTime);
        }
        else if (currTime >= jumpWindowEndTime)
        {
            // if player is still on top after jump window, need to remove parent transform and let player fall
            if (playerTouching)
            {
                Debug.Log("falling from water");
                player.transform.parent = null;
                player.GetComponent<PlayerController>().state = new FallingState(true);
            }
            gameObject.transform.localScale = startingPosition;
            currTime = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerTouching = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerTouching = true;
        }
    }

    public bool isJumpWindow()
    {
        if (currTime >= jumpWindowStartTime && currTime <= jumpWindowEndTime)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

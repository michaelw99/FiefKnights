using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MOVE_SPEED = 1500f;
    public float JUMP_SPEED = 4000f;
    public float DASH_COOLDOWN = 0.8f;
    public float DASH_SPEED = 2000f;
    public float MASK_COOLDOWN = 0.5f;

    public bool isGrounded = true;
    public bool isFacingRight = true;
    public bool hasDoubleJumped = false;
    public bool isCountering = false;

    public float currMaskCooldown = 0f;
    public float currDashCooldown = 0f;
    public float invinciblityTime = 0f;

    public PlayerState state;
    public PlayerStats stats;
    public Rigidbody2D rb;
    public BufferedInputListener buffer;
    public Mask currentMask;
    public Vector3 lastCheckpointPosition;

    private Dictionary<GameObject, float> hitBoxStore;

    private void Awake()
    {
        state = new IdleState();
        rb = GetComponent<Rigidbody2D>();
        buffer = new BufferedInputListener();
        hitBoxStore = new Dictionary<GameObject, float>();
        currentMask = new NormalMask();
        stats = new PlayerStats();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleMaskChangeInput(); // mask can be switched at any time
        state.handleInput(this);
        state.update(this);

        currDashCooldown -= Time.deltaTime;
        currMaskCooldown -= Time.deltaTime;
        invinciblityTime -= Time.deltaTime;

        // invincibility color or blinking

        // destroy existing hitboes
        destroyHitboxes();
    }

    /* flipping sprite */
    public void flipDirection()
    {
        isFacingRight = !isFacingRight;
        Vector3 scale = gameObject.transform.localScale;
        scale.x *= -1;
        gameObject.transform.localScale = scale;
    }


    /* Handling grounded check, hitting ceiling, wall jump */
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground") && collisionBelow(collision))
        {
            isGrounded = true;
            hasDoubleJumped = false;
        } else if (collision.gameObject.layer == LayerMask.NameToLayer("Ceiling") && collisionAbove(collision))
        {
            state = new FallingState(false);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") && collisionSides(collision))
        {
            if (!isGrounded && stats.hasWallJump)
            {
                if (collisionRight(collision))
                {
                    state = new WallSlideState(true); // wall sliding
                } else
                {
                    state = new WallSlideState(false);
                }
            }
        } else if (collision.gameObject.tag == "WaterSpout" && collisionBelow(collision))
        {
            transform.parent = collision.gameObject.transform; // set waterspout transform to be parent
            state = new WaterSpoutState(collision.gameObject);
        }
        else if (collision.gameObject.tag == "SlideBlock")
        {
            state = new SlideState();
        }


    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            isGrounded = false;
            Debug.Log("left ground");
            if (!(state is JumpingState) && !(state is DashState))
            {
                state = new FallingState(false);
            }

        }
        else if (collision.gameObject.tag == "WaterSpout" && state is WaterSpoutState) // exiting water spout
        {
            transform.parent = null;
            if (!isGrounded)
            {
                state = new FallingState(false);
            }
            else
            {
                state = new IdleState();
            }
        }
    }


    /* This section is for dash related things */
    public bool canDash()
    {
        return currDashCooldown <= 0 && stats.hasDash;
    }
    
    public void resetDashCooldown()
    {
        currDashCooldown = DASH_COOLDOWN;
    }


    /* This section is for mask related things */
    public bool canSwitchMask()
    {
        return currMaskCooldown <= 0 && stats.hasWindMask;
    }

    public void resetMaskCooldown()
    {
        currMaskCooldown = MASK_COOLDOWN;
    }

    private void handleMaskChangeInput()
    {
        if (Input.GetKeyDown(SettingsInputManager.SIM.mask) && canSwitchMask())
        {
            // pull up mask select or quick swap to next
            if (currentMask is NormalMask)
            {
                Debug.Log("switching normal mask to wind mask");
                currentMask = new WindMask();
                gameObject.GetComponent<SpriteRenderer>().color = new Color(0f, 4f, 255f, 1f); // changing colors for testing
            }
            else
            {
                Debug.Log("switching wind mask to normal mask");
                currentMask = new NormalMask();
                gameObject.GetComponent<SpriteRenderer>().color = new Color(255f, 242f, 0f, 1f);
            }
            resetMaskCooldown();
        }
    }


    /* This section is for handling player hitboxes */
    public void spawnHitbox(Object prefab, Vector3 position, Quaternion rotation, float duration)  
    {
        GameObject hitBox = (GameObject)Instantiate(prefab, position, rotation);
        if (!isFacingRight)
        {
            Vector3 localScale = hitBox.transform.localScale;
            localScale.x *= -1;
            hitBox.transform.localScale = localScale;
        }
        hitBoxStore.Add(hitBox, duration);
    }

    public void destroyHitboxes()
    {
        List<GameObject> keys = new List<GameObject>(hitBoxStore.Keys);
        foreach (GameObject hitBox in keys)
        {
            float duration = hitBoxStore[hitBox];
            duration -= Time.deltaTime;
            if (duration <= 0f)
            {
                hitBoxStore.Remove(hitBox);
                Destroy(hitBox);
            } else
            {
                hitBoxStore[hitBox] = duration;
            }
        }
    }


    /* Setting enemy collision */
    public void ignoreEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(10, 9); // ignore enemy layer
    }

    public void allowEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(10, 9, false);
    }


    /* Player invincibilty */
    public void setInvincibilty(float seconds)
    {
        invinciblityTime = seconds;
    }

    public bool isInvincible()
    {
        return invinciblityTime > 0;
    }


    /* This section deals with player stats */
    public void takeDamage(float damage, Vector3 otherPos)
    {
        if (isInvincible())
        {
            return;
        } else if (isCountering)
        {
            state = new CounterStrike_State();
            isCountering = false;
        } else
        {
            stats.takeDamage(damage);
            if (otherPos.x > gameObject.transform.position.x)
            {
                state = new HitState(true, false);
            }
            else
            {
                state = new HitState(false, false);
            }
        }
    }

    public float getHP()
    {
        return stats.currHp;
    }
    
    public void takeDamageAndRespawn(float damage)
    {
        stats.takeDamage(damage);
        state = new HitState(false, true);
        StartCoroutine(respawnWaiter());
    }

    IEnumerator respawnWaiter()
    {
        //Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);
        gameObject.transform.position = lastCheckpointPosition;
    }

    public void useMp(float cost)
    {
        stats.useMp(cost);
        Debug.Log("using mp");
        Debug.Log(stats.currMp);
    }

    /* helper functions */
    private bool collisionBelow(Collision2D collision)
    {
        // detect side of collision
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 0))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool collisionAbove(Collision2D collision)
    {
        // detect side of collision
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Approximately(angle, 180))
        {
            return true;
        } else
        {
            return false;
        }
    }

    private bool collisionRight(Collision2D collision)
    {
        // detect side of collision
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Abs(angle - 90) <= 1)
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y <= 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        } else
        {
            return false;
        }
    }

    private bool collisionLeft(Collision2D collision)
    {
        // detect side of collision
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Abs(angle - 90) <= 1)
        {
            Vector3 cross = Vector3.Cross(Vector3.forward, hit);
            if (cross.y > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private bool collisionSides(Collision2D collision)
    {
        // detect side of collision
        Vector3 hit = collision.contacts[0].normal;
        float angle = Vector3.Angle(hit, Vector3.up);
        if (Mathf.Abs(angle - 90) <= 1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }


}

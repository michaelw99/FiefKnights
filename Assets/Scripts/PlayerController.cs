using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MOVE_SPEED = 1500f;
    public float JUMP_SPEED = 4000f;
    public float DASH_COOLDOWN = 0.8f;
    public float DASH_SPEED = 2000f;

    public bool isGrounded = true;
    public bool isFacingRight = true;
    public bool hasDoubleJumped = false;
    public float currDashCooldown = 0f;

    public PlayerState state;
    public Rigidbody2D rb;
    public BufferedInputListener buffer;

    private Dictionary<GameObject, float> hitBoxStore;

    private void Awake()
    {
        state = new IdleState();
        rb = GetComponent<Rigidbody2D>();
        buffer = new BufferedInputListener();
        hitBoxStore = new Dictionary<GameObject, float>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state.handleInput(this);
        state.update(this);

        if (rb.velocity.x < 0 && isFacingRight || rb.velocity.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = gameObject.transform.localScale;
            scale.x *= -1;
            gameObject.transform.localScale = scale;
        }

        currDashCooldown -= Time.deltaTime;

        // destroy existing hitboes
        destroyHitboxes();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }

    public bool canDash()
    {
        return currDashCooldown <= 0;
    }

    public void resetDashCooldown()
    {
        currDashCooldown = DASH_COOLDOWN;
    }

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

    public void ignoreEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(0, 9);
    }

    public void allowEnemyCollision()
    {
        Physics2D.IgnoreLayerCollision(0, 9, false);
    }
}

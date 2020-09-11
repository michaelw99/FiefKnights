using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float MOVE_SPEED = 1500f;
    public float JUMP_SPEED = 4000f;
    public float DASH_COOLDOWN = 0.8f;

    public bool isGrounded = true;
    public bool isFacingRight = true;
    public bool hasDoubleJumped = false;
    public float currDashCooldown = 0f;

    public PlayerState state;
    public Rigidbody2D rb;

    private void Awake()
    {
        state = new IdleState();
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        state.handleInput(this);
        state.update();

        if (rb.velocity.x < 0 && isFacingRight || rb.velocity.x > 0 && !isFacingRight)
        {
            isFacingRight = !isFacingRight;
            Vector3 scale = gameObject.transform.localScale;
            scale.x *= -1;
            gameObject.transform.localScale = scale;
        }

        currDashCooldown -= Time.deltaTime;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    public bool canDash()
    {
        return currDashCooldown <= 0;
    }

    public void resetDashCooldown()
    {
        currDashCooldown = DASH_COOLDOWN;
    }
}

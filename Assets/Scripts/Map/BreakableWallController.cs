using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWallController : MonoBehaviour
{

    public int health;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0)
        {
            breakDown();
        }
    }

    public void takeDamage()
    {
        health -= 1;
    }

    public void breakDown()
    {
        // play breaking animation
        Destroy(gameObject);
    }
}

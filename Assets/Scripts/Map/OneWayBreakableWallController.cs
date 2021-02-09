using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneWayBreakableWallController : MonoBehaviour
{
    public int health;
    public bool right;
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        if (right)
        {
            if (player.transform.position.x > gameObject.transform.position.x)
            {
                Debug.Log("hitting right wall");
                health -= 1;
            }
        } else
        {
            if (player.transform.position.x < gameObject.transform.position.x)
            {
                Debug.Log("hitting left wall");
                health -= 1;
            }
        }
        
    }

    public void breakDown()
    {
        // play breaking animation
        Destroy(gameObject.transform.parent.gameObject);
    }
}

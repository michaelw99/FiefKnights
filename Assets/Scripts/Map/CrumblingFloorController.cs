using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrumblingFloorController : MonoBehaviour
{

    public float secondsToDestroy = 0.3f;
    private bool playerHasTouched = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerHasTouched)
        {
            secondsToDestroy -= Time.deltaTime;
        }

        if (secondsToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            playerHasTouched = true;
        }
    }
}

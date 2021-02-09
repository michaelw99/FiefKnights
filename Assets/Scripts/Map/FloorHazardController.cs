using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorHazardController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            PlayerController playerController = collider.gameObject.GetComponent<PlayerController>();
            playerController.takeDamageAndRespawn(20);
        }
    }
    
}

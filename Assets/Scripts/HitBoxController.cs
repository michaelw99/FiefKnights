using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class HitBoxController : MonoBehaviour
{
    public int damage;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Breakable"))
        {
            collider.gameObject.GetComponent<BreakableWallController>().takeDamage();
        }
        if (collider.gameObject.layer == LayerMask.NameToLayer("OneWayBreakable"))
        {
            collider.gameObject.GetComponent<OneWayBreakableWallController>().takeDamage();
        }
    }

}

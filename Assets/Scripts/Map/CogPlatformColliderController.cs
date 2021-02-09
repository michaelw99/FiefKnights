using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogPlatformColliderController : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("entering moving plat");
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("leaving moving plat");
            collision.gameObject.transform.parent = null;
        }
    }
}

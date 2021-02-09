using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CogPlatformController : MonoBehaviour
{
    public float rotateSpeed = 5f;
    public float radius = 0.1f;
    public float startingAngle = 0f;

    private Vector2 center;
    private float angle;

    // Start is called before the first frame update
    void Start()
    {
        center = transform.position;
        angle = startingAngle;
    }

    // Update is called once per frame
    void Update()
    {
        angle += rotateSpeed * Time.deltaTime;

        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * radius;
        transform.position = center + offset;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // have player stick to platform
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            collision.gameObject.transform.parent = null;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(center, 0.1f);
        Gizmos.DrawLine(center, transform.position);
    }
}

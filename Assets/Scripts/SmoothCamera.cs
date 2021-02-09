using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothCamera : MonoBehaviour
{

    public GameObject player;
    public float smoothTime = 1.4f;
    private Vector3 velocity = new Vector3(0, 0, 0);
    public PlayerController playerController;
    public bool clamp = true;

    public float leftLimit = -5;
    public float rightLimit = 5;
    public float topLimit = 2;
    public float bottomLimit = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition;
        // Define a target position above and behind the target transform
        targetPosition = player.transform.TransformPoint(new Vector3(6, 2, -10));

        // Smoothly move the camera towards that target position
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
        //transform.position = Vector3.Lerp(startPosition, targetPosition, smoothTime * Time.deltaTime);

        if (clamp)
        {
            transform.position = new Vector3(
                Mathf.Clamp(transform.position.x, leftLimit, rightLimit),
                Mathf.Clamp(transform.position.y, bottomLimit, topLimit),
                transform.position.z
            );
        }
    }

    private void OnDrawGizmos()
    {
        // draw box on camera boundary
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(rightLimit, topLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, bottomLimit), new Vector2(rightLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(leftLimit, topLimit), new Vector2(leftLimit, bottomLimit));
        Gizmos.DrawLine(new Vector2(rightLimit, topLimit), new Vector2(rightLimit, bottomLimit));
    }
}

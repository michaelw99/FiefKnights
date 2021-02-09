using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SludgeSpawnController : MonoBehaviour
{

    public float sludgeInterval = 1f;
    private Vector3 spawnPoint;
    private float currTime;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (currTime >= sludgeInterval)
        {
            spawnSludgeDrop();
            currTime = 0;

        }
    }

    void spawnSludgeDrop()
    {
        Instantiate(Resources.Load("MapStuff/toxicDrop"), spawnPoint, gameObject.transform.rotation);
    }
}

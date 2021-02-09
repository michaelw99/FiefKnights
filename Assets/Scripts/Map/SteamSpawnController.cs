using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamSpawnController : MonoBehaviour
{
    public float steamInterval = 2f;
    public float steamStartDelay = 0f;
    public float steamStayTime = 1f;
    public float scaleFactor = 1f;

    private bool hasDelayed = false;
    private Vector3 spawnPoint;
    private float currTime = 0;
    private bool steamActive = false;
    private GameObject steam;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        currTime += Time.deltaTime;
        if (hasDelayed)
        {
            if (!steamActive)
            {
                if (currTime >= steamInterval)
                {
                    steamActive = true;
                    spawnSteam();
                    currTime = 0;

                }
            }
            else
            {
                if (currTime >= steamStayTime)
                {
                    steamActive = false;
                    destroySteam();
                }
            }
        } else
        {
            if (currTime >= steamStartDelay)
            {
                hasDelayed = true;
                currTime = 0;
            }
        }
    }

    void spawnSteam()
    {
        steam = (GameObject)Instantiate(Resources.Load("MapStuff/dangerSteam"), spawnPoint, gameObject.transform.rotation);
        Vector3 currScale = steam.transform.localScale;
        Vector3 newScale = new Vector3(currScale.x, currScale.y * scaleFactor, currScale.z);
        steam.transform.localScale = newScale;
    }

    void destroySteam()
    {
        if (steam != null)
        {
            Destroy(steam);
        }
    }
}

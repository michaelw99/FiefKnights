using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretWallController : MonoBehaviour
{

    public GameObject player;
    public bool canFade;
    private Color fadedColor;
    private Color normalColor;
    private float fadeSpeed = 8f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        canFade = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (canFade)
        {
            fadeOut();
        }
        else
        {
            fadeIn();
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFade = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            canFade = false;
        }
    }

    void fadeOut()
    {
        Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
        float fadeAmount = Mathf.Max(objectColor.a - (fadeSpeed * Time.deltaTime), 0);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<SpriteRenderer>().material.color = objectColor;
    }

    void fadeIn()
    {
        Color objectColor = this.GetComponent<SpriteRenderer>().material.color;
        float fadeAmount = Mathf.Min(objectColor.a + (fadeSpeed * Time.deltaTime), 1);
        objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmount);
        this.GetComponent<SpriteRenderer>().material.color = objectColor;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public GameObject player;
    public GameObject healthBar;
    public GameObject manaBar;

    PlayerController playerController;
    PlayerStats playerStats;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        playerStats = playerController.stats;
    }

    // Update is called once per frame
    void Update()
    {
        updateHealthBar();
        updateManaBar();
    }

    private void updateHealthBar()
    {
        healthBar.GetComponent<Slider>().value = playerStats.currHp / playerStats.maxHp;
    }

    private void updateManaBar()
    {
        manaBar.GetComponent<Slider>().value = playerStats.currMp / playerStats.maxMp;
    }
}

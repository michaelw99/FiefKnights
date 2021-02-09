using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats
{
    public float maxHp { get; set; }
    public float currHp { get; set; }
    public float maxMp { get; set; }
    public float currMp { get; set; }
    public float damageMultiplier { get; set; }

    // movement abilities
    public bool hasDash { get; set; }
    public bool hasWallJump { get; set; }
    public bool hasDoubleJump { get; set; }

    // masks
    public bool hasMask { get; set; }
    public bool hasWindMask { get; set; }

    public PlayerStats()
    {
        maxHp = 1000f;
        currHp = 1000f;
        maxMp = 100f;
        currMp = 100f;
        damageMultiplier = 1.0f;
        hasDash = true;
        hasWallJump = false;
        hasDoubleJump = false;
        hasMask = true;
        hasWindMask = true;
    }

    public void takeDamage(float damage)
    {
        currHp -= damage;
        Debug.Log(string.Format("Player HP: {0}", currHp));
    }

    public float currentHP()
    {
        return currHp;
    }

    public void useMp(float cost)
    {
        currMp -= cost;
    }

}

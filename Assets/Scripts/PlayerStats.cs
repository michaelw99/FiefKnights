using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats 
{
    private int maxHp;
    private int currHp;
    private float damageMultiplier;

    public PlayerStats()
    {
        maxHp = 100;
        currHp = 100;
        damageMultiplier = 1.0f;
    }

    public void takeDamage(int damage)
    {
        currHp -= damage;
    }

}

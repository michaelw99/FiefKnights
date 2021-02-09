using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    private int maxHp;
    private int currHp;

    public EnemyStats()
    {
        maxHp = 700;
        currHp = maxHp;
    }

    public void takeDamage(int damage)
    {
        currHp -= damage;
        Debug.Log(string.Format("Enemy HP: {0}", currHp));
    }

    public int currentHP()
    {
        return currHp;
    }
}
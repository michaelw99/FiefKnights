using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats
{
    private int maxHp;
    private int currHp;

    public EnemyStats()
    {
        maxHp = 500;
        currHp = maxHp;
    }

    public void takeDamage(int damage)
    {
        currHp -= damage;
        Debug.Log(string.Format("Hp is {0}", currHp));
    }
}

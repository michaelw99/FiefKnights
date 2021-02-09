using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerStatsSave
{
    // basic stats
    public float maxHp;
    public float currHp;
    public float maxMp;
    public float currMp;
    public float damageMultiplier;

    // movement abilities
    public bool hasDash;
    public bool hasWallJump;
    public bool hasDoubleJump;

    // masks
    public bool hasMask;
    public bool hasWindMask;
}

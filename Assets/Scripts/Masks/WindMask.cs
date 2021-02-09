using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindMask : Mask
{

    HashSet<int> ALTERED_SKILLS_ARRAY = new HashSet<int>(new int[] { 0, 1 });

    public PlayerState getLState()
    {
        return new Wind_L_State();
    }

    public PlayerState getLLState()
    {
        return new Wind_LL_State();
    }

    public PlayerState getLLLState()
    {
        return new Wind_LLL_State();
    }

    public PlayerState getLLLLState()
    {
        return new Wind_LLLL_State();
    }

    public PlayerState getLLLLLState()
    {
        return new Wind_LLLLL_State();
    }

    public PlayerState getLLLHState()
    {
        return new Wind_LLLH_State();
    }

    public PlayerState getHState()
    {
        return new Wind_H_State();
    }

    public PlayerState getHHState()
    {
        return new Wind_HH_State();
    }

    public PlayerState getHHHState()
    {
        return new Wind_HHH_State();
    }

    public bool altersSkill(int id)
    {
        if (ALTERED_SKILLS_ARRAY.Contains(id))
        {
            return true;
        }
        return false;
    }

    public PlayerState getAlteredSkill(int id)
    {
        return null;
    }
}

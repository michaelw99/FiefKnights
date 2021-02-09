using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalMask : Mask
{
    public PlayerState getLState()
    {
        return new L_State();
    }

    public PlayerState getLLState()
    {
        return new LL_State();
    }

    public PlayerState getLLLState()
    {
        return new LLL_State();
    }

    public PlayerState getLLLLState()
    {
        return new LLLL_State();
    }

    public PlayerState getLLLLLState()
    {
        return new LLLLL_State();
    } 

    public PlayerState getLLLHState()
    {
        return new LLLH_State();
    }

    public PlayerState getHState()
    {
        return new H_State();
    }

    public PlayerState getHHState()
    {
        return new HH_State();
    }

    public PlayerState getHHHState()
    {
        return new HHH_State();
    }

    public bool altersSkill(int id)
    {
        return false;
    }

    public PlayerState getAlteredSkill(int id)
    {
        return null;
    }
}

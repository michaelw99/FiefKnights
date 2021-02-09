using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Mask 
{
    PlayerState getLState();

    PlayerState getLLState();

    PlayerState getLLLState();

    PlayerState getLLLLState();

    PlayerState getLLLLLState();

    PlayerState getLLLHState();

    PlayerState getHState();

    PlayerState getHHState();

    PlayerState getHHHState();

    bool altersSkill(int id);

    PlayerState getAlteredSkill(int id);
}

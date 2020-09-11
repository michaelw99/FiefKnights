﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    void handleInput(PlayerController player);

    void update();
}

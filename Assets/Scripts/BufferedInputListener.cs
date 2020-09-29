using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BufferedInputListener
{
    public KeyCode input;
   
    // Update is called once per frame
    public void takeInput(KeyCode input)
    {
        // validation?
        this.input = input;
    }

    public KeyCode getInput()
    {
        // validation?
        return input;
    }

    public KeyCode triggerBuffer()
    {
        KeyCode ret = input;
        clearBuffer();
        return ret;
    }

    public void clearBuffer()
    {
        input = KeyCode.None;
    }

    public bool bufferNotEmpty()
    {
        return input != KeyCode.None;
    }
}

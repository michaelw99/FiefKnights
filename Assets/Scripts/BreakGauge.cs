using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakGauge : MonoBehaviour
{
    public float BREAK_RATE = 10f; // per update

    private int stage;
    private float breakPoint;
    private float currBreak;

    
    public BreakGauge(float breakPoint)
    {
        stage = 1;
        this.breakPoint = breakPoint;
        currBreak = 0;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        decreaseBreak(BREAK_RATE * Time.deltaTime);
    }

    public void decreaseBreak(float amount)
    {
        currBreak = Mathf.Max(0, currBreak - amount);
    }

    public void increaseBreak(int amount)
    {
        currBreak += amount;
        if (currBreak >= breakPoint)
        {
            stage += 1;
            resetBreak();
        }
    }

    public void resetBreak()
    {
        currBreak = 0;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BreakGauge
{
    private int stage;
    private float breakPoint;
    private float currBreak;
    private GameObject breakBar;

    
    public BreakGauge(float breakPoint)
    {
        stage = 1;
        this.breakPoint = breakPoint;
        currBreak = 0f;
    }

    public void decreaseBreak(float amount)
    {
        currBreak = Mathf.Max(0, currBreak - amount * Time.deltaTime);
    }

    public void increaseBreak(int amount)
    {
        currBreak += amount;
        if (currBreak >= breakPoint)
        {
            stage += 1;
            if (stage == 2)
            {
                breakBar.GetComponent<Image>().color = new Color32(255, 148, 41, 100);
            } else if (stage == 3)
            {
                breakBar.GetComponent<Image>().color = new Color32(255, 48, 41, 100);
            }
        }
    }

    public void resetBreak()
    {
        currBreak = 0;
    }

    public void resetStage()
    {
        stage = 0;
    }

    public float getBreakPercent()
    {
        return currBreak / breakPoint;
    }

    public int getBreakStage()
    {
        return stage;
    }

    public void setBreakBar(GameObject bar)
    {
        breakBar = bar;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using Ludiq;

public class EnemyController : MonoBehaviour
{
    EnemyStats stats;
    BreakGauge breakGauge;
    Canvas enemyCanvas;

    public GameObject breakBar;
    public Vector2 startingPosition;
    public Vector2 currentPosition;

    public float barVerticalDistance;
    public float barHorizontalDistance;
    public float breakRate;

    public bool finisherState;
    public int outOfBoundsTimer = 0;

    private void Awake()
    {
        stats = new EnemyStats();
        startingPosition = GameObject.Find(gameObject.name).transform.position;
        
        breakGauge = new BreakGauge(100f);
        barVerticalDistance = 0.5f;
        barHorizontalDistance = -0.5f;
        enemyCanvas = GameObject.Find("EnemyCanvas").GetComponent<Canvas>();
        breakRate = 10f;
        finisherState = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        spawnBreakBar();
        breakGauge.setBreakBar(breakBar);
    }

    // Update is called once per frame
    void Update()
    {
        currentPosition = GameObject.Find(gameObject.name).transform.position;
        if (breakBar != null)
        {
            breakGauge.decreaseBreak(breakRate);
            updateBreakBar();
        }
    }

    public void takeDamage(int damage)
    {
        stats.takeDamage(damage);
        if (!finisherState)
        {
            breakGauge.increaseBreak(damage);
        }
    }

    private void spawnBreakBar()
    {
        Vector3 spawnPoint = gameObject.transform.position + new Vector3(barHorizontalDistance, barVerticalDistance, 0f);
        breakBar = (GameObject)Instantiate(Resources.Load("BreakBar"), spawnPoint, gameObject.transform.rotation);
        breakBar.transform.SetParent(enemyCanvas.transform);
    }

    private void updateBreakBar()
    {
        breakBar.transform.position = gameObject.transform.position + new Vector3(barHorizontalDistance, barVerticalDistance, 0f);
        breakBar.transform.localScale = new Vector3(breakGauge.getBreakPercent() * 100, breakBar.transform.localScale.y, breakBar.transform.localScale.z);
    }

    public float getBreakPercent()
    {
        return breakGauge.getBreakPercent();
    }

    public int getBreakStage()
    {
        return breakGauge.getBreakStage();
    }

    public int getHP()
    {
        return stats.currentHP();
    }

    public void resetBreak()
    {
        breakGauge.resetBreak();
    }

    public void resetStage()
    {
        breakGauge.resetStage();
    }
}

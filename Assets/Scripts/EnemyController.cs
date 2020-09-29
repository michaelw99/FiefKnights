using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    EnemyStats stats;
    BreakGauge breakGauge;

    private void Awake()
    {
        stats = new EnemyStats();
        breakGauge = new BreakGauge(100f);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void takeDamage(int damage)
    {
        stats.takeDamage(damage);
        breakGauge.increaseBreak(damage);
    }
}

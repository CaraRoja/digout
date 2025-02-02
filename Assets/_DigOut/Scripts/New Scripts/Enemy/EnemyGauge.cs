using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGauge : MonoBehaviour
{
    public float gauge = 100f;
    public float gaugeTime = 0.5f;
    public bool gaugeIsWorking;
    public bool IsDecreasing = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateGaugeInTime();
    }

    public void UpdateGaugeInTime()
    {
        if (gauge < 100f && gaugeIsWorking)
        {
            gauge = Mathf.MoveTowards(gauge, 100f, gaugeTime * Time.deltaTime);
        }

        if (gauge <= 0f)
        {
            gaugeIsWorking = false;
            gauge = 0f;
        }
    }

    public void DecreaseGauge(float value)
    {
        gauge -= value;
    }

    public bool GaugeIsWorking()
    {
        return gaugeIsWorking;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class Timer : MonoBehaviour
{
    private TextMeshProUGUI clock;
    // Start is called before the first frame update

    void TimerTick()
    {
        clock.SetText(Math.Round(Time.timeSinceLevelLoad, 0).ToString());
    }

    void Start()
    {
        clock = this.GetComponent<TextMeshProUGUI>();
        InvokeRepeating("TimerTick", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
    }
}

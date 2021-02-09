using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class TimerSample1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var timer = Observable.Timer(dueTime: TimeSpan.FromSeconds(1));

        Debug.Log("Subscribled Time : " + Time.time);

        timer.Subscribe(x => 
        {
            Debug.Log("OnNext fired at : " + Time.time + ", value = " + x);
        },
        () => Debug.Log("OnCompleted"));

        var timer2 = Observable.Timer(
            dueTime: TimeSpan.FromSeconds(1),
            period: TimeSpan.FromSeconds(0.5F));

        timer2.Subscribe(
                x => Debug.Log($"[{x}]:{Time.time}"),
                () => Debug.Log("OnCompleted")
            ).AddTo(this);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

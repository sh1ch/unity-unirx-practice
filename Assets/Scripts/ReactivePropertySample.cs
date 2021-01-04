using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ReactivePropertySample : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var health = new ReactiveProperty<int>(100);

        Debug.Log("current value: " + health.Value);

        health.Subscribe(
            x => Debug.Log("notified value: " + x),
            () => Debug.Log("OnCompleted"));

        health.Value = 50;

        Debug.Log("current value: " + health.Value);

        health.Dispose();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

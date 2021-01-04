using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ReactivePropertySample2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var health = new ReactiveProperty<int>(100);

        health.Subscribe(x => Debug.Log("notified value: " + x));

        Debug.Log("value set to 100.");

        health.Value = 100;

        Debug.Log("value force overwrite.");

        health.SetValueAndForceNotify(100);

        health.Dispose();
    }
}

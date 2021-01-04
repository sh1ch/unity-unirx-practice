using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ReactivePropertySample3 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var health = new ReactiveProperty<int>(40);

        health
            .SkipLatestValueOnSubscribe()
            .Subscribe(x => Debug.Log("notified value : " + x));

        health.Dispose();
    }

}

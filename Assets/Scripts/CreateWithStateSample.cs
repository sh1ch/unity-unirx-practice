using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class CreateWithStateSample : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        CreateCountObservable(10).Subscribe(x => Debug.Log(x));
    }

    private IObservable<int> CreateCountObservable(int count)
    {
        if (count <= 0) return Observable.Empty<int>();

        return Observable.CreateWithState<int, int>(state: count, subscribe: (max, observer) => 
        {
            for (var i = 0; i < max; i++)
            {
                observer.OnNext(max);
            }

            observer.OnCompleted();

            return Disposable.Empty;
        });
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System.Threading.Tasks;

public class CreateSample : MonoBehaviour
{
    private void Start()
    {
        var observable = Observable.Create<char>(observer =>
        {
            var disposable = new CancellationDisposable();

            Task.Run(async () => 
            {
                for (var i = 0; i < 26; i++)
                {
                    await Task.Delay(TimeSpan.FromSeconds(1), disposable.Token);

                    observer.OnNext((char)('A' + i));
                }
            }, disposable.Token);

            return disposable;
        });

        observable.Subscribe(
            x => Debug.Log("OnNext: " + x),
            ex => Debug.LogError("OnError: " + ex.Message),
            () => Debug.Log("OnCompleted")
        ).AddTo(this);

    }
}

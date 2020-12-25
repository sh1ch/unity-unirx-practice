using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

public class HotExchangeText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var originalSubject = new Subject<string>();

        // Publish ‚·‚é‚Æ Hot ‚É‚È‚é
        var appendStringObservable = originalSubject.Scan((prev, cur) => prev + " " + cur)
                                                    .Last()
                                                    .Publish();

        var disposable = appendStringObservable.Connect();

        originalSubject.OnNext("I");
        originalSubject.OnNext("have");

        appendStringObservable.Subscribe(x => Debug.Log(x));

        originalSubject.OnNext("a");
        originalSubject.OnNext("pen");

        originalSubject.OnCompleted();

        disposable.Dispose();
        originalSubject.Dispose();

        Debug.Log($"Unity Main Thread ID1: {Thread.CurrentThread.ManagedThreadId}");

        var s2 = new Subject<Unit>();

        s2.AddTo(this);

        s2.ObserveOn(Scheduler.Immediate)
              .Subscribe(_ =>
              {
                  Debug.Log($"Unity Main Thread ID2: {Thread.CurrentThread.ManagedThreadId}");
              });

        s2.OnNext(Unit.Default);

        Task.Run(() => s2.OnNext(Unit.Default));

        s2.OnCompleted();

        Debug.Log($"Unity Main Thread ID3: {Thread.CurrentThread.ManagedThreadId}");
    }
}

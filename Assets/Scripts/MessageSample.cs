using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MessageSample : MonoBehaviour
{
    [SerializeField]
    private float _CountTimeSeconds = 30F;

    public IObservable<Unit> OnTimeUpAsyncSubject => _OnTimeUpAsyncSubject;

    private readonly AsyncSubject<Unit> _OnTimeUpAsyncSubject = new AsyncSubject<Unit>();

    private IDisposable _Disposable = null;

    // Start is called before the first frame update
    void Start()
    {
        _Disposable = Observable.Timer(TimeSpan.FromSeconds(_CountTimeSeconds))
                                .Subscribe(_ => 
                                {
                                    _OnTimeUpAsyncSubject.OnNext(Unit.Default);
                                    _OnTimeUpAsyncSubject.OnCompleted();
                                });


        var subject1 = new Subject<string>();

        var appendObservable1 = subject1.Scan((prev, current) => prev + " " + current).Last();

        appendObservable1.Subscribe(x => Debug.Log(x));

        subject1.OnNext("I");
        subject1.OnNext("have");
        subject1.OnNext("a");
        subject1.OnNext("pen.");

        subject1.OnCompleted();
        subject1.Dispose();

        var subject2 = new Subject<string>();

        var appendObservable2 = subject2.Scan((prev, current) => prev + " " + current).Last();

        subject2.OnNext("I");
        subject2.OnNext("have");

        appendObservable2.Subscribe(x => Debug.Log(x));

        subject2.OnNext("a");
        subject2.OnNext("pen.");

        subject2.OnCompleted();
        subject2.Dispose();

        var subject3 = new Subject<int>();

        var observable3 = subject3.Do(p => Debug.Log("Do:" + p));

        observable3.Subscribe(p => Debug.Log("Sub1:" + p));
        observable3.Subscribe(p => Debug.Log("Sub2:" + p));

        subject3.OnNext(1);
        subject3.OnCompleted();
        subject3.Dispose();
    }

    // Update is called once per frame
    void OnDestory()
    {
        _Disposable?.Dispose();

        _OnTimeUpAsyncSubject.Dispose();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CountDownEventProvider : MonoBehaviour
{
    [SerializeField]
    private int _CountSeconds = 10;

    private MySubject<int> _Subject = null;

    public IObservable<int> CountDownObservable => _Subject;


    void Awake()
    {
        _Subject = new MySubject<int>();

        StartCoroutine(CountCoroutine());
    }


    private IEnumerator CountCoroutine()
    {
        var current = _CountSeconds;

        while (current > 0)
        {
            _Subject.OnNext(current);

            current -= 1;

            yield return new WaitForSeconds(1.0f);
        }

        _Subject.OnNext(0);
        _Subject.OnCompleted();
    }

    private void OnDestroy()
    {
        _Subject.Dispose();
    }
}

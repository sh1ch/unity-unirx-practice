using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObserveEventComponent : MonoBehaviour
{
    [SerializeField]
    private CountDownEventProvider _CountDownEventProvider = null;

    private PrintLogObserver<int> _PrintLogObserver = null;

    private IDisposable _Disposable = null;

    // Start is called before the first frame update
    void Start()
    {
        _PrintLogObserver = new PrintLogObserver<int>();

        _Disposable = _CountDownEventProvider
            .CountDownObservable
            .Subscribe(_PrintLogObserver);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _Disposable?.Dispose();
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ObserveEventComponent2 : MonoBehaviour
{
    [SerializeField]
    private CountDownEventProvider _CountDownEventProvider = null;

    // private PrintLogObserver<int> _PrintLogObserver = null;

    private IDisposable _Disposable = null;

    // Start is called before the first frame update
    void Start()
    {
        // _PrintLogObserver = new PrintLogObserver<int>();

        // UniRx 名前空間に拡張が定義されている
        _Disposable = _CountDownEventProvider
            .CountDownObservable
            .Subscribe(
                x => Debug.Log(x),
                ex => Debug.LogError(ex),
                () => Debug.Log("OnCompleted!")
            );
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _Disposable?.Dispose();
    }
}

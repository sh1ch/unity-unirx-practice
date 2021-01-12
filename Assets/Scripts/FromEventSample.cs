using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class FromEventSample : MonoBehaviour
{
    public sealed class MyEventArgs : EventArgs
    {
        public int MyProperty { get; set; }
    }

    public event EventHandler<MyEventArgs> _OnEvent = null;

    public event Action<int> _CallBackAction = null;

    [SerializeField]
    private Button _UiButton = null;

    private readonly CompositeDisposable _Disposables = new CompositeDisposable();

    // Start is called before the first frame update
    void Start()
    {
        Observable.FromEventPattern<EventHandler<MyEventArgs>, MyEventArgs>(h => h.Invoke, h => _OnEvent += h, h => _OnEvent -= h)
            .Subscribe(x => Debug.Log($"Call from event pattern. value = {x.EventArgs.MyProperty}"))
            .AddTo(_Disposables);

        Observable.FromEvent<EventHandler<MyEventArgs>, MyEventArgs>(h => (sender, e) => h(e), h => _OnEvent += h, h => _OnEvent -= h)
            .Subscribe(x => Debug.Log($"Call from event. value = {x.MyProperty}"))
            .AddTo(_Disposables);

        Observable.FromEvent<int>(h => _CallBackAction += h, h => _CallBackAction -= h)
            .Subscribe(x => Debug.Log($"Call from event. (action int) value = {x}"))
            .AddTo(_Disposables);

        Observable.FromEvent<UnityAction>(
                h => new UnityAction(h),
                h => _UiButton.onClick.AddListener(h),
                h => _UiButton.onClick.RemoveListener(h)
            ).Subscribe(_ => 
            {
                Debug.Log("Call from unity action.");
                _OnEvent?.Invoke(this, new MyEventArgs() { MyProperty = 5 });
                _CallBackAction?.Invoke(10);
            })
            .AddTo(_Disposables);

        _UiButton.onClick.AsObservable()
            .Subscribe(_ => Debug.Log("Call from uiButton."))
            .AddTo(_Disposables);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        _Disposables.Dispose();
    }

}

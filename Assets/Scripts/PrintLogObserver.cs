using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintLogObserver<T> : IObserver<T>
{
    public void OnCompleted() => UnityEngine.Debug.Log(nameof(OnCompleted));
    public void OnError(Exception error) => UnityEngine.Debug.LogError(error);
    public void OnNext(T value) => UnityEngine.Debug.Log(value);
}

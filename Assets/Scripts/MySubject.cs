using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class MySubject<T> : ISubject<T>, IDisposable
{
    public bool IsStopped { get; } = false;
    public bool IsDisposed { get; } = false;

    private readonly object _LockObject = new object();
    private Exception Error = null;
    private List<IObserver<T>> Observers = null;

    public MySubject()
    {
        Observers = new List<IObserver<T>>();
    }

    public void OnNext(T value)
    {
        if (IsStopped) return;

        lock (_LockObject)
        {
            ThrowIfDisposed();

            foreach (var observer in Observers)
            {
                observer.OnNext(value);
            }
        }
    }

    public void OnError(Exception error)
    {
        lock (_LockObject)
        {
            ThrowIfDisposed();

            if (IsStopped) return;

            this.Error = error;

            try
            {
                foreach (var observer in Observers)
                {
                    observer.OnError(error);
                }
            }
            finally
            {
                Dispose();
            }
        }
    }

    public void OnCompleted()
    {
        lock (_LockObject)
        {
            ThrowIfDisposed();

            if (IsStopped) return;

            try
            {
                foreach (var observer in Observers)
                {
                    observer.OnCompleted();
                }
            }
            finally
            {
                Dispose();
            }
        }
    }

    public IDisposable Subscribe(IObserver<T> observer)
    {
        lock (_LockObject)
        {
            if (IsStopped)
            {
                if (Error != null)
                {
                    observer.OnError(Error);
                }
                else
                {
                    observer.OnCompleted();
                }

                return Disposable.Empty;
            }

            Observers.Add(observer);

            return new Subscription(this, observer);
        }
    }

    private void ThrowIfDisposed()
    {
        if (IsDisposed) throw new ObjectDisposedException(typeof(MySubject<T>).Name);
    }

    public void Dispose()
    {
        lock (_LockObject)
        {
            if (!IsDisposed)
            {
                Observers?.Clear();

                Observers = null;
                Error = null;
            }
        }
    }

    private sealed class Subscription : IDisposable
    {
        private readonly IObserver<T> _Observer = null;
        private readonly MySubject<T> _Parent = null;

        public Subscription(MySubject<T> parent, IObserver<T> observer)
        {
            _Parent = parent;
            _Observer = observer;
        }

        public void Dispose()
        {
            _Parent?.Observers?.Remove(_Observer);
        }
    }

}

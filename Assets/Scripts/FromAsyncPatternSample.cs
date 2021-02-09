using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

/// <summary>
/// <see cref="FromAsyncPatternSample"/> クラスは、<seealso cref="MonoBehaviour"/> クラスを継承した Unity スクリプト クラスです。
/// <para>
/// <see cref="MonoBehaviour"/> クラスは、すべての Unity スクリプト クラスの基底クラスです。
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class FromAsyncPatternSample : MonoBehaviour
{
    /// <summary>
    /// <see cref="Start"/> メソッドは、スクリプトの初期化を実行します。
    /// <para>
    /// <see cref="FromAsyncPatternSample"/> クラスが有効化されたとき、<seealso cref="Update"/> メソッドが最初に呼び出される直前のフレームで実行されます。
    /// </para>
    /// </summary>
    private void Start()
    {
        // P101
        Func<string, string> readFile = fileName =>
        {
            using (var reader = new StreamReader(fileName))
            {
                return reader.ReadToEnd();
            }
        };

        Func<string, IObservable<string>> temp =
            Observable.FromAsyncPattern<string, string>(readFile.BeginInvoke, readFile.EndInvoke);

        IObservable<string> readAsync = temp(@"Assets\Texts\data1.txt");

        readAsync.Subscribe(x => Debug.Log(x));

        // P103
        Func<IObservable<string>> fileReadAsync;

        fileReadAsync = Observable.ToAsync(() =>
        {
            using (var reader = new StreamReader(@"Assets\Texts\data1.txt"))
            {
                return reader.ReadToEnd();
            }
        }, Scheduler.ThreadPool);

        fileReadAsync().Subscribe(x => Debug.Log(x));

        // 途中でキャンセルできない非同期
        fileReadAsync.Invoke().Subscribe();

        // P104
        // Invoke をすぐにするなら、一番簡単な書き方
        Observable.Start(() =>
        {
            using (var reader = new StreamReader(@"Assets\Texts\data1.txt"))
            {
                return reader.ReadToEnd();
            }
        }, Scheduler.ThreadPool)
        .Subscribe(x => Debug.Log(x));
    }

    /// <summary>
    /// <see cref="Update"/> メソッドは、フレーム毎のロジックを実行します。
    /// <para>
    /// <see cref="FromAsyncPatternSample"/> クラスが有効であるとき、フレーム毎で実行されます。
    /// </para>
    /// </summary>
    private void Update()
    {

    }
}

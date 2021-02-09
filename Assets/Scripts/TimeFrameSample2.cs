using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;

/// <summary>
/// <see cref="TimeFrameSample2"/> クラスは、<seealso cref="MonoBehaviour"/> クラスを継承した Unity スクリプト クラスです。
/// <para>
/// <see cref="MonoBehaviour"/> クラスは、すべての Unity スクリプト クラスの基底クラスです。
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class TimeFrameSample2 : MonoBehaviour
{
    /// <summary>
    /// <see cref="Start"/> メソッドは、スクリプトの初期化を実行します。
    /// <para>
    /// <see cref="TimeFrameSample2"/> クラスが有効化されたとき、<seealso cref="Update"/> メソッドが最初に呼び出される直前のフレームで実行されます。
    /// </para>
    /// </summary>
    private void Start()
    {
        var timer = Observable.TimerFrame(dueTimeFrameCount: 5, periodFrameCount: 10);

        Debug.Log("SubscribeTime: " + Time.frameCount);

        timer.Subscribe(
            x => Debug.Log($"{x} - {Time.frameCount}"),
            () => Debug.Log("OnCompleted")
        ).AddTo(this);
    }

    /// <summary>
    /// <see cref="Update"/> メソッドは、フレーム毎のロジックを実行します。
    /// <para>
    /// <see cref="TimeFrameSample2"/> クラスが有効であるとき、フレーム毎で実行されます。
    /// </para>
    /// </summary>
    private void Update()
    {
        
    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// <see cref="SubScribeToTextTest"/> クラスは、<seealso cref="MonoBehaviour"/> クラスを継承した Unity スクリプト クラスです。
/// <para>
/// <see cref="MonoBehaviour"/> クラスは、すべての Unity スクリプト クラスの基底クラスです。
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class SubScribeToTextTest : MonoBehaviour
{
    [SerializeField]
    private Text Text = null;

    [SerializeField]
    private Toggle Toggle = null;

    /// <summary>
    /// <see cref="Start"/> メソッドは、スクリプトの初期化を実行します。
    /// <para>
    /// <see cref="SubScribeToTextTest"/> クラスが有効化されたとき、<seealso cref="Update"/> メソッドが最初に呼び出される直前のフレームで実行されます。
    /// </para>
    /// </summary>
    private void Start()
    {
        var observable = Toggle.OnValueChangedAsObservable();

        observable.SubscribeToText(Text);
    }
 
}

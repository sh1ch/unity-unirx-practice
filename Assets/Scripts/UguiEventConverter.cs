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
/// <see cref="UguiEventConverter"/> クラスは、<seealso cref="MonoBehaviour"/> クラスを継承した Unity スクリプト クラスです。
/// <para>
/// <see cref="MonoBehaviour"/> クラスは、すべての Unity スクリプト クラスの基底クラスです。
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class UguiEventConverter : MonoBehaviour
{
    [SerializeField]
    private Toggle ToggleUI = null;

    /// <summary>
    /// <see cref="Start"/> メソッドは、スクリプトの初期化を実行します。
    /// <para>
    /// <see cref="UguiEventConverter"/> クラスが有効化されたとき、<seealso cref="Update"/> メソッドが最初に呼び出される直前のフレームで実行されます。
    /// </para>
    /// </summary>
    private void Start()
    {
        if (ToggleUI == null) return;

        ToggleUI.isOn = false;

        ToggleUI.onValueChanged
            .AsObservable()
            .Subscribe(x => Debug.Log("現在の状態 AsObservable:" + x));

        ToggleUI.OnValueChangedAsObservable()
            .Subscribe(x => Debug.Log("現在の状態 拡張メソッド:" + x));

        Debug.Log("--");

        ToggleUI.isOn = true;
    }
}

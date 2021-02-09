using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// <see cref="UnityWebRequestToObservable"/> クラスは、<seealso cref="MonoBehaviour"/> クラスを継承した Unity スクリプト クラスです。
/// <para>
/// <see cref="MonoBehaviour"/> クラスは、すべての Unity スクリプト クラスの基底クラスです。
/// </para>
/// <see href="https://docs.unity3d.com/ScriptReference/MonoBehaviour.html">Unity Documentation - MonoBehaviour</see>
/// </summary>
public class UnityWebRequestToObservable : MonoBehaviour
{
    /// <summary>
    /// <see cref="Start"/> メソッドは、スクリプトの初期化を実行します。
    /// <para>
    /// <see cref="UnityWebRequestToObservable"/> クラスが有効化されたとき、<seealso cref="Update"/> メソッドが最初に呼び出される直前のフレームで実行されます。
    /// </para>
    /// </summary>
    private void Start()
    {
        FetchAsync("https://unity.com/ja")
            .ToObservable()
            .Subscribe(x => Debug.Log(x));
    }

    private async UniTask<string> FetchAsync(string uri)
    {
        using (var request = UnityWebRequest.Get(uri))
        {
            await request.SendWebRequest();
            return request.downloadHandler.text;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class OperatorTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var subject = new Subject<int>();

        subject.Subscribe(p => Debug.Log("subscribed value: " + p));

        subject.Where(p => p > 0)
               .Subscribe(p => Debug.Log("where subscribed value: " + p));

        // メッセージのテスト
        subject.OnNext(1);
        subject.OnNext(0);
        subject.OnNext(-1);
        subject.OnNext(2);
        subject.OnNext(-2);

        subject.OnCompleted();
        subject.Dispose();
    }

}

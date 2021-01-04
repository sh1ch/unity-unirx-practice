using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

public class BehaviorSubjectTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var behaviorSubject = new BehaviorSubject<int>(0);

        //behaviorSubject.OnNext(1);
        //behaviorSubject.OnNext(2);
        //behaviorSubject.OnNext(3);

        behaviorSubject.Subscribe(
            x => Debug.Log("OnNext:" + x),
            ex => Debug.LogError("OnError:" + ex),
            () => Debug.Log("OnCompleted"));

        behaviorSubject.OnNext(4);
        behaviorSubject.OnCompleted();

        behaviorSubject.Dispose();
    }
}

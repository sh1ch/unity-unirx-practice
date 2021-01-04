using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class ReactivePropertyOnInspectorWindow : MonoBehaviour
{
    public ReactiveProperty<int> A;
    public IntReactiveProperty B;
}

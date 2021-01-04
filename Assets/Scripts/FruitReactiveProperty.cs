using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;


public enum Fruit
{
    Apple,
    Banana,
    Peach,
    Melon,
    Orange
}

[Serializable]
public class FruitReactiveProperty : ReactiveProperty<Fruit>
{
    public FruitReactiveProperty(Fruit init) : base(init) { }
}

[UnityEditor.CustomPropertyDrawer(typeof(FruitReactiveProperty))]
public class ExtendInspectorDisplayDrawer : InspectorDisplayDrawer
{

}


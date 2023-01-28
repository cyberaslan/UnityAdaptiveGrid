using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public abstract class ScaleStrategy : Strategy<ScaleStrategy.MethodType, ScaleStrategy>
{
    static ScaleStrategy() {
        InitializeMap();
    }
    public override System.Enum GetKey() => Method;

    public enum MethodType  { Stretch = 0, Fit = 1, FitWidth = 2, FitHeight = 3 }
    [SerializeField] public abstract MethodType Method { get; }
    public abstract void Scale(List<RectTransform> gridChildren);
    
   
}

[Serializable]
public class StretchScale : ScaleStrategy
{
    [SerializeField] public override MethodType Method => MethodType.Stretch;
    public override void Scale(List<RectTransform> gridChildren) {

    }
}



[Serializable]
public class FitScale : ScaleStrategy
{
    [SerializeField] public override MethodType Method => MethodType.Fit;
    public override void Scale(List<RectTransform> gridChildren) {

    }
}


[Serializable]
public class FitWidthScale : ScaleStrategy
{
    [SerializeField] public override MethodType Method => MethodType.FitWidth;
    public override void Scale(List<RectTransform> gridChildren) {

    }
}


[Serializable]
public class FitHeightScale : ScaleStrategy
{
    [SerializeField] public override MethodType Method => MethodType.FitHeight;
    public override void Scale(List<RectTransform> gridChildren) {

    }
}


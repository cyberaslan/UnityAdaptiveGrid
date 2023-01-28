using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class StretchScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Stretch;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
[Serializable]
public class FitScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Fit;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
[Serializable]
public class FitWidthScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitWidth;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
[Serializable]
public class FitHeightScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitHeight;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class ScaleStretch : Algorithm
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Stretch;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}

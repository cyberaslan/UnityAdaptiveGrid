using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[Serializable]
public class ArrangeFill : AdaptivePreset
{

    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Fill;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        
    }
}
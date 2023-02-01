using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CyberAslan.AdaptiveGrid
{
    [Serializable]
    public class ScaleNone : AdaptivePreset
    {
        public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.None;
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding) {

        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace CyberAslan.AdaptiveGrid
{
    [Serializable]
    public class ScaleStretch : AdaptivePreset
    {
        public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Stretch;
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding) {

        }
    }
}
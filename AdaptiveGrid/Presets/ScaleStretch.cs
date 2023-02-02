using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdaptiveGrid
{
    [Serializable]
    public class ScaleNone : AdaptivePreset
    {
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding)
        {
            //This strategy implements do nothing with content
            return;
        }

        public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.None;
    }
}
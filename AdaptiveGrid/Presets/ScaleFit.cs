using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdaptiveGrid
{
    [Serializable]
    public class ScaleFit : AdaptivePreset
    {
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding)
        {
            foreach (RectTransform element in elements)
            {
                if (element.TryGetComponent(out Image image))
                {
                    if (image.sprite != null)
                    {
                        Vector2 contentSize = image.sprite.bounds.size;
                        Vector2 scaledContentSize = LayoutTools.FitContent(contentSize, element.rect, cellPadding);
                        element.sizeDelta = scaledContentSize;
                        element.anchoredPosition = new Vector2(element.anchoredPosition.x, element.anchoredPosition.y);
                    }
                }
            }
        }

        public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitImage;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;
namespace CyberAslan.AdaptiveGrid
{
    [Serializable]
    public class ArrangeFill : AdaptivePreset
    {
        public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Fill;
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding) {

            //Collect elements' content and calculate its average size
            List<Image> images = new List<Image>();
            Vector2 accumulatedSize = Vector2.zero;

            foreach (RectTransform element in elements) {
                if (element.TryGetComponent(out Image image)) {
                    images.Add(image);
                    accumulatedSize += new Vector2(image.sprite.rect.width, image.sprite.rect.height);
                }
            }

            Vector2 averageContentSize = accumulatedSize / elements.Count;

            //Calculate optimal grid size to arrange elements with minimum empty space
            GridSize optimalGridSize = LayoutTools.OptimalGridSize(elements, grid.rect, averageContentSize, gridMargin, cellPadding);
            
            LayoutTools.ArrangeElements(elements, grid.rect, optimalGridSize, gridMargin, cellPadding);
        }
    }
}
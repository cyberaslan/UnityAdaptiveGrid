using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace AdaptiveGrid
{
    [Serializable]
    public class ArrangeFill : AdaptivePreset
    {
        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding) {

            //Collect elements' content and calculate its average size
            List<Image> images = new List<Image>();
            Vector2 accumulatedSize = Vector2.zero;


            int imageWithSpriteCounter = 0;
            foreach (RectTransform element in elements) {
                if (element.TryGetComponent(out Image image)) {
                    images.Add(image);
                    if (image.sprite != null) {
                        imageWithSpriteCounter++;
                    }
                    accumulatedSize += image.sprite != null ? new Vector2(image.sprite.rect.width, image.sprite.rect.height) : Vector2.zero;
                }
            }

            Vector2 averageContentSize = imageWithSpriteCounter > 0 ? accumulatedSize / elements.Count : Vector2.one;



            //Calculate optimal grid size to arrange elements with min  imum empty space
            GridSize optimalGridSize = LayoutTools.OptimalGridSize(elements, grid.rect, averageContentSize, gridMargin, cellPadding);

            LayoutTools.ArrangeElements(elements, grid.rect, optimalGridSize, gridMargin, cellPadding);
        }

        public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Fill;
    }
}
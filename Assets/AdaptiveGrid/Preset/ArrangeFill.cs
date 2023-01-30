using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using UnityEngine.UI;

[Serializable]
public class ArrangeFill : AdaptivePreset
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Fill;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        List<Image> images = new List<Image>();

        Vector2 accumatedSize = Vector2.zero;

        foreach (RectTransform element in elements) {
            if (element.TryGetComponent(out Image image)) {
                images.Add(image);
                accumatedSize += new Vector2(image.sprite.rect.width, image.sprite.rect.height);
            }
        }

        Vector2 averageContentSize = accumatedSize / elements.Count;

        int q = elements.Count;
        int cols = 1;
        int rows = 1;

        float minEmptySpace = Single.MaxValue;
        int optimalRowColunt = 1;
        int optimalColCount = 1;

        Debug.Log($"Min: {minEmptySpace}");

        for (cols = 1; cols <= q; cols++) {
            for(rows=1; rows <= Mathf.Ceil((float)q / cols); rows++) {
                int emptyCells = cols * rows - q;
                if (emptyCells>=0) {
                    float cellWidth = (grid.rect.width / cols);
                    float cellHeight = (grid.rect.height / rows);
                    float eventualCellSize =  cellWidth * cellHeight;

                    Vector2 scaledAverageContent = ScaleFit.FitSize(averageContentSize, new Rect(0,0,cellWidth, cellHeight));
                    

                    float eventualCellEmptySpace = eventualCellSize - scaledAverageContent.magnitude;
                    float eventualEmptySpace = eventualCellEmptySpace * q + emptyCells * eventualCellSize;
                    Debug.Log($"{cols}x{rows} {eventualEmptySpace}");

                    if (eventualEmptySpace < minEmptySpace) {
                        minEmptySpace = eventualEmptySpace;
                        optimalRowColunt = rows;
                        optimalColCount = cols;
                    }
                }
            }
        }
        Debug.Log($"{optimalColCount}x{optimalRowColunt} {minEmptySpace}");
        Debug.Log("");
        ArrangeGrid.ArrangeElements(elements, new GridSize(optimalRowColunt, optimalColCount), true, grid.rect);
    }
}
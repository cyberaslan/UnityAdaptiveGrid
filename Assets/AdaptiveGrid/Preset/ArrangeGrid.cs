using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[Serializable]
public class ArrangeGrid : AdaptivePreset
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Grid;
    [SerializeField] GridSize _gridSize = new GridSize(10,5) ;
    [SerializeField] bool _fitParent = true;

    public override void Apply(List<RectTransform> elements, RectTransform grid) {

        GridSize gridSize = _gridSize;
        if (gridSize.Cols == 0 && gridSize.Rows == 0) {
            Debug.LogWarning($"You are trying to arrange elements in 0x0 grid");
            return; 
        }
        if (gridSize.Rows == 0) gridSize.Rows = (int)Mathf.Ceil((float)elements.Count / gridSize.Cols) ;
        if (gridSize.Cols == 0) gridSize.Cols = (int)Mathf.Ceil((float)elements.Count / gridSize.Rows) ;

        ArrangeElements(elements, gridSize, _fitParent, grid.rect);
    }
    public static void ArrangeElements(List<RectTransform> elements, GridSize gridSize, bool fitParent, Rect gridRect) {
        Debug.Log($"Arrange {gridSize}");
        float cellWidth = gridRect.width / gridSize.Cols;
        float cellHeight = fitParent ? gridRect.height / gridSize.Rows : cellWidth;
        for (int i = 0; i < elements.Count; i++) {
            RectTransform element = elements[i];

            int rowNum = i / gridSize.Cols;
            int colNum = i % gridSize.Cols;

            //normalize pivot and anchors
            element.pivot = new Vector2(0.5f, 0.5f);
            element.anchorMin = element.anchorMax = new Vector2(0f, 1f);
            element.sizeDelta = new Vector2(cellWidth, cellHeight);
            element.anchoredPosition = new Vector2(colNum * cellWidth + cellWidth / 2, -rowNum * cellHeight - cellHeight / 2);
        }
    }
}
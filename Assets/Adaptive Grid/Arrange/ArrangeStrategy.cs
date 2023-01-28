using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;

[Serializable]
public class ArrangeFill : Strategy
{

    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Fill;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        
    }
}
public class ArrangePack : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Pack;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
public class ArrangeGrid : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Grid;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
public class FixedRowsArrange : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.FixedRows;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
public class FixedColumnsArrange : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.FixedColumns;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}


/*
[Serializable]
public class PackArrange : Strategy
{
    public override LayoutType Layout => LayoutType.Pack;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}



[Serializable]
public class GridArrange : Strategy
{
    public override LayoutType Layout => LayoutType.Grid;
    [SerializeField] private Vector2 test;
    [SerializeField] public GridSize _gridSize;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) {
        float colWidth = gridRect.rect.width / _gridSize.Columns;
        float rowHeight = colWidth;


        for (int i = 0; i < gridChildren.Count; i++) {
            int rowNum = i / _gridSize.Columns;
            int colNum = i % _gridSize.Columns;

            //normalize pivot and anchors
            gridChildren[i].pivot = gridChildren[i].anchorMin = gridChildren[i].anchorMax = new Vector2(0.5f, 0.5f);
            gridChildren[i].anchoredPosition = new Vector2(colNum * colWidth, rowNum * rowHeight);
        }
    }
}

[Serializable]
public class FixedRowsArrange : Strategy
{
    public override LayoutType Layout => LayoutType.FixedRows;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}

public class FixedColumnsArrange : Strategy
{
    public override LayoutType Layout => LayoutType.FixedColumns;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}*/
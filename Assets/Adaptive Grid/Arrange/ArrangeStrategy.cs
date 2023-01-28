using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using System;
using System.Linq;


[Serializable]
public abstract class ArrangeStrategy : Strategy<ArrangeStrategy.LayoutType, ArrangeStrategy>
{
    public override System.Enum GetKey() => Layout;
    static ArrangeStrategy() {
        InitializeMap();
    }
    public enum LayoutType { Fill = 0, Pack = 1, Grid = 2, FixedRows = 3, FixedColumns = 4 }
    public abstract LayoutType Layout { get; }
    public abstract void Arrange(List<RectTransform> gridChildren, RectTransform gridRect);    
}
[Serializable]
public class FillArrange : ArrangeStrategy {
    public override LayoutType Layout => LayoutType.Fill;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}

[Serializable]
public class PackArrange : ArrangeStrategy
{
    public override LayoutType Layout => LayoutType.Pack;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}



[Serializable]
public class GridArrange : ArrangeStrategy
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
public class FixedRowsArrange : ArrangeStrategy
{
    public override LayoutType Layout => LayoutType.FixedRows;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}

public class FixedColumnsArrange : ArrangeStrategy
{
    public override LayoutType Layout => LayoutType.FixedColumns;
    public override void Arrange(List<RectTransform> gridChildren, RectTransform gridRect) { }

}
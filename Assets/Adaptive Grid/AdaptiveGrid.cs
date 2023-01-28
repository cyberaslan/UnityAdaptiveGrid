using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AdaptiveGrid : UIBehaviour
{
    private RectTransform _gridRect;
    List<RectTransform> _gridChildren = new List<RectTransform>();

    private Vector2 _cellSize;

    /* Grid arrange and margins*/
    [SerializeField] private Strategy _arrangeStrategy;
    public enum ArrangeLayout { Fill = 0, Pack = 1, Grid = 2, FixedRows = 3, FixedColumns = 4 }
    [Header("Grid settings")]
    [SerializeField] private ArrangeLayout _arrangeLayout;
    [SerializeField] Offset _gridMargin;
    public void SetArrangeLayout(ArrangeLayout newArrangeLayout) {
        _arrangeLayout = newArrangeLayout;
        UpdateStrategies();
    }

    /* Cell content scaling, padding and spacing */
    [SerializeField] private Strategy _scaleStrategy;
    public enum ScaleMethod { Stretch = 0, Fit = 1, FitWidth = 2, FitHeight = 3 }
    [Header("Cell content")]
    [SerializeField] private ScaleMethod _scaleMethod;
    public void SetScaleMethod(ScaleMethod newScaleMethod) {
        _scaleMethod = newScaleMethod;
        UpdateStrategies();
    }
    [SerializeField] Offset _cellPadding;
    [SerializeField] Offset _cellSpacing;
    


    protected override void Awake() {
        Activator.CreateInstance(typeof(FitScale));
        base.Awake();
        _gridRect = GetComponent<RectTransform>();
        CollectChildren();
        ArrangeChildren();
        
    }

    void CollectChildren() {
        _gridChildren.Clear();
        foreach (RectTransform child in transform) _gridChildren.Add(child);
    }

    void OnTransformChildrenChanged() {
        CollectChildren();
    }

    void ArrangeChildren() {
        _arrangeStrategy.Apply(_gridChildren, _gridRect);
        _scaleStrategy.Apply(_gridChildren, _gridRect);
    }

    private void UpdateStrategies() {
        StrategyMap.SetStrategy(ref _arrangeStrategy, _arrangeLayout);
        StrategyMap.SetStrategy(ref _scaleStrategy, _scaleMethod);
    }

#if UNITY_EDITOR

    protected override void OnValidate() {
        base.OnValidate();
        UpdateStrategies();
    }
#endif

    protected override void OnRectTransformDimensionsChange() { 
        if(IsActive()) ArrangeChildren(); 
    }
}
[Serializable]
public class GridSize
{
    public int Rows;
    public int Columns;
}
[Serializable]
public class Offset
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;
}
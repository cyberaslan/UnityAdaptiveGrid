using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
public class AdaptiveGrid : UIBehaviour
{
    private event Action SettingsChanged;

    [SerializeField] private RectTransform _gridRect;
    [SerializeField] List<RectTransform> _gridChildren = new List<RectTransform>();

    private Vector2 _cellSize;

    /* Grid arrange and margins*/
    
    public enum ArrangeLayout { Fill = 0, Pack = 1, Grid = 2, FixedRows = 3, FixedColumns = 4 }
    [Header("Grid settings")]
    [SerializeField] private ArrangeLayout _arrangeLayout;
    [SerializeField][SerializeReference] private Strategy _arrangeStrategy;
    [SerializeField] Offset _gridMargin;
    public void SetArrangeLayout(ArrangeLayout newArrangeLayout) {
        if (newArrangeLayout == _arrangeLayout) return; 
        _arrangeLayout = newArrangeLayout; 
        SettingsChanged?.Invoke();
    }

    /* Cell content scaling, padding and spacing */
    public enum ScaleMethod { Stretch = 0, Fit = 1, FitWidth = 2, FitHeight = 3 }
    [Header("Cell content")]
    [SerializeField] private ScaleMethod _scaleMethod;
    [SerializeField] [SerializeReference] private Strategy _scaleStrategy;
    public void SetScaleMethod(ScaleMethod newScaleMethod) {
        if (newScaleMethod == _scaleMethod) return;
        _scaleMethod = newScaleMethod;
        SettingsChanged?.Invoke();
    }
    [SerializeField] Offset _cellPadding;
    [SerializeField] Offset _cellSpacing;


    protected override void Awake() {
        base.Awake();
        _gridRect = GetComponent<RectTransform>();
        CollectChildren();
        UpdateStrategies();
    }
    protected override void Start() {
        ArrangeChildren();
    }
    private void CollectChildren() {
        _gridChildren.Clear();
        foreach (RectTransform child in transform) _gridChildren.Add(child);
    }

    private void ArrangeChildren() {
        _arrangeStrategy.Apply(_gridChildren, _gridRect);
        _scaleStrategy.Apply(_gridChildren, _gridRect);
    }

    private void UpdateStrategies() {
        StrategyMap.SetStrategy(ref _arrangeStrategy, _arrangeLayout);
        StrategyMap.SetStrategy(ref _scaleStrategy, _scaleMethod);
    }


    protected override void OnRectTransformDimensionsChange() {
        Debug.Log("OnRectTransformDimensionsChange");
        ArrangeChildren();
    }

    public void OnTransformChildrenChanged() {
        Debug.Log("OnTransformChildrenChanged");
        CollectChildren();
        ArrangeChildren();
    }

#if UNITY_EDITOR
    protected override void OnValidate() {
        base.OnValidate();
        UpdateStrategies();
        ArrangeChildren();
    }
#endif


}
[Serializable]
public struct GridSize  
{
    public int Rows;
    public int Cols;
    public GridSize(int rows, int columns) {
        Rows = rows;
        Cols = columns;
    }
    public override string ToString() {
        return $"GridSize {Cols}x{Rows}";
    }
}
[Serializable]
public class Offset
{
    public float Left;
    public float Top;
    public float Right;
    public float Bottom;
}
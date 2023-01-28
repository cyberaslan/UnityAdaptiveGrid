using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class AdaptiveGrid : UIBehaviour
{
    private Vector2 _cellSize;
    private RectTransform _gridRect;
    List<RectTransform> _gridChildren = new List<RectTransform>();
    [HideInInspector] [SerializeField] private ArrangeStrategy _arrangeStrategy;
    [HideInInspector][SerializeField] private ScaleStrategy _scaleStrategy;

    /* Settings */
    [Header("Grid settings")]
    [SerializeField] private ArrangeStrategy.LayoutType _arrangeLayout;
    [SerializeField] Offset _gridMargin;
    [Header("Cell content")]
    [SerializeField] private ScaleStrategy.MethodType _scaleMethod;
    [SerializeField] Offset _cellPadding;
    [SerializeField] Offset _cellSpacing;

#if UNITY_EDITOR

    protected override void OnValidate() {
        base.OnValidate();
        if (_arrangeStrategy == null) {
            _arrangeStrategy = ArrangeStrategy.GetStrategy(_arrangeLayout);
        } else if (_arrangeStrategy.Layout != _arrangeLayout) {
            _arrangeStrategy = ArrangeStrategy.GetStrategy(_arrangeLayout);
        }
        if (_scaleStrategy == null) {
            _scaleStrategy = ScaleStrategy.GetStrategy(_scaleMethod);
        } else if (_scaleStrategy.Method != _scaleMethod) {
            _scaleStrategy = ScaleStrategy.GetStrategy(_scaleMethod);
        }
        Debug.Log($"Onvalidate: {_arrangeStrategy} {_scaleMethod}");
    }
#endif
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
        _arrangeStrategy.Arrange(_gridChildren, _gridRect);
        _scaleStrategy.Scale(_gridChildren);
    }


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
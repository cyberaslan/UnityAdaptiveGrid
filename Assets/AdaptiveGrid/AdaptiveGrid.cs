using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

[ExecuteInEditMode]
public class AdaptiveGrid : UIBehaviour
{
    private event Action SettingsChanged;

    private RectTransform _gridRect;
    List<RectTransform> _gridChildren = new List<RectTransform>();

    private Vector2 _cellSize;

    /* Grid arrange and margins*/
    
    public enum ArrangeLayout { Fill = 0, Pack = 1, Grid = 2 }
    [Header("Grid settings")]
    [SerializeField] private ArrangeLayout _arrangeLayout;
    [SerializeField][SerializeReference] private AdaptivePreset _arrangePreset;
    [SerializeField] Offset _gridMargin;

    /* Cell content scaling, padding and spacing */
    public enum ScaleMethod { FitImages = 0, Stretch = 1 }
    [Header("Cell content")]
    [SerializeField] private ScaleMethod _scaleMethod;
    [SerializeField] [SerializeReference] private AdaptivePreset _scalePreset;
    [SerializeField] Offset _cellPadding;
    [SerializeField] Offset _cellSpacing;


    protected override void Awake() {
        base.Awake();
        _gridRect = GetComponent<RectTransform>();
        CollectChildren();
        OnSettingsChanged();
    }
    protected override void Start() {
        base.Start();
        ArrangeChildren();
    }

    public void SetArrangeLayout(ArrangeLayout newArrangeLayout) {
        if (newArrangeLayout == _arrangeLayout) return;
        _arrangeLayout = newArrangeLayout;
        SettingsChanged?.Invoke();
    }

    public void SetScaleMethod(ScaleMethod newScaleMethod) {
        if (newScaleMethod == _scaleMethod) return;
        _scaleMethod = newScaleMethod;
        SettingsChanged?.Invoke();
    }

    private void CollectChildren() {
        _gridChildren.Clear();
        foreach (RectTransform child in transform) _gridChildren.Add(child);
    }

    private void ArrangeChildren() {
        _arrangePreset.Apply(_gridChildren, _gridRect);
        _scalePreset.Apply(_gridChildren, _gridRect);
    }

    private void OnSettingsChanged() {
        AdaptivePreset.ChangeValue(ref _arrangePreset, _arrangeLayout);
        AdaptivePreset.ChangeValue(ref _scalePreset, _scaleMethod);
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
        OnSettingsChanged();
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
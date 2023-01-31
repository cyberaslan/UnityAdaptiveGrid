using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace CyberAslan.AdaptiveGrid
{
    [ExecuteInEditMode]
    public class AdaptiveGrid : UIBehaviour
    {

        private RectTransform _gridRect;
        List<RectTransform> _gridChildren = new List<RectTransform>();

        private Vector2 _cellSize;

        /* Grid arrange and margins*/
        public enum ArrangeLayout { Fill = 0, Pack = 1, Grid = 2 }
        [Header("Grid settings")]
        [SerializeField] private ArrangeLayout _arrangeLayout;
        [SerializeField] [SerializeReference] private AdaptivePreset _arrangePreset;
        private event Action ArrangePresetChanged;
        [SerializeField] Offset _gridMargin;

        /* Cell content scaling, padding and spacing */
        public enum ScaleMethod { FitImages = 0, Stretch = 1 }
        [Header("Cell content")]
        [SerializeField] private ScaleMethod _scaleMethod;
        [SerializeField] [SerializeReference] private AdaptivePreset _scalePreset;
        private event Action ScalePresetChanged;
        [SerializeField] Offset _cellPadding;

        protected override void Awake() {
            base.Awake();
            _gridRect = GetComponent<RectTransform>();
            CollectElements();
            ArrangePresetChanged += OnArrangePresetChanged;
            ScalePresetChanged += OnScalePresetChanged;
        }
        protected override void OnDestroy() {
            ArrangePresetChanged -= OnArrangePresetChanged;
            ScalePresetChanged -= OnScalePresetChanged;
        }

        protected override void Start() {
            base.Start();
            ArrangeElements();
        }

        public void SetArrangeLayout(ArrangeLayout newArrangeLayout) {
            if (newArrangeLayout == _arrangeLayout) return;
            _arrangeLayout = newArrangeLayout;
            ArrangePresetChanged?.Invoke();
        }

        public void SetScaleMethod(ScaleMethod newScaleMethod) {
            if (newScaleMethod == _scaleMethod) return;
            _scaleMethod = newScaleMethod;
            ScalePresetChanged?.Invoke();
        }

        private void CollectElements() {
            _gridChildren.Clear();
            foreach (RectTransform child in transform) _gridChildren.Add(child);
        }

        private void ArrangeElements() {
            _arrangePreset.Apply(_gridChildren, _gridRect, _gridMargin, _cellPadding);
            _scalePreset.Apply(_gridChildren, _gridRect, _gridMargin, _cellPadding);
        }

        private void OnArrangePresetChanged() {
            AdaptivePreset.ChangeValue(ref _arrangePreset, _arrangeLayout);
        }

        private void OnScalePresetChanged() {
            AdaptivePreset.ChangeValue(ref _scalePreset, _scaleMethod);
        }

        protected override void OnRectTransformDimensionsChange() {
            ArrangeElements();
        }

        public void OnTransformChildrenChanged() {
            CollectElements();
            ArrangeElements();
        }

#if UNITY_EDITOR
        protected override void OnValidate() {
            base.OnValidate();
 
            if (!_arrangePreset.SelectorInInspector.Equals(_arrangeLayout)) OnArrangePresetChanged();
            if (!_scalePreset.SelectorInInspector.Equals(_scaleMethod)) OnScalePresetChanged();

            ArrangeElements();
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

}
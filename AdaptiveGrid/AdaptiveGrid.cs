using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace AdaptiveGrid
{
    [ExecuteInEditMode]
    public class AdaptiveGrid : UIBehaviour
    {
        private Vector2 _cellSize;
        private RectTransform _gridRect;
        private List<RectTransform> _gridChildList = new List<RectTransform>();

        [Header("Grid settings")]
        [SerializeField] private ArrangeLayout _arrangeLayout;
        [SerializeField] [SerializeReference] private AdaptivePreset _arrangePreset;
        [SerializeField] Offset _gridMargin;

        
        [Header("Cell content")]
        [SerializeField] private ScaleMethod _scaleMethod;
        [SerializeField] [SerializeReference] private AdaptivePreset _scalePreset;
        [SerializeField] Offset _cellPadding;

        private event Action ArrangePresetChanged;
        private event Action ScalePresetChanged;

        private void AdjustElements()
        {
            _arrangePreset.Apply(_gridChildList, _gridRect, _gridMargin, _cellPadding);
            _scalePreset.Apply(_gridChildList, _gridRect, _gridMargin, _cellPadding);
        }
        private bool ApproptiatePresetKey(AdaptivePreset preset, System.Enum keyValue)
        {
            if (preset == null)
            {
                return false;
            }
            return preset.SelectorInInspector.Equals(keyValue);
        }

        private void CollectElements()
        {
            _gridChildList.Clear();
            foreach (RectTransform child in transform)
            {
                _gridChildList.Add(child);
            }
        }
        private void OnArrangePresetChanged()
        {
            AdaptivePreset.ChangeValue(ref _arrangePreset, _arrangeLayout);
        }

        private void OnScalePresetChanged()
        {
            AdaptivePreset.ChangeValue(ref _scalePreset, _scaleMethod);
        }

        protected override void Awake()
        {
            base.Awake();

            _gridRect = GetComponent<RectTransform>();
            CollectElements();

            ArrangePresetChanged += OnArrangePresetChanged;
            ScalePresetChanged += OnScalePresetChanged;
        }

        protected override void OnDestroy()
        {
            ArrangePresetChanged -= OnArrangePresetChanged;
            ScalePresetChanged -= OnScalePresetChanged;
        }

        protected override void OnRectTransformDimensionsChange()
        {
            AdjustElements();
        }

        protected override void Start()
        {
            base.Start();
            AdjustElements();
        }

        public void OnTransformChildrenChanged()
        {
            CollectElements();
            AdjustElements();
        }

        public void SetArrangePreset(ArrangeLayout newArrangeLayout)
        {
            if (newArrangeLayout == _arrangeLayout)
            {
                return;
            }
            _arrangeLayout = newArrangeLayout;
            ArrangePresetChanged?.Invoke();
        }

        public void SetScalePreset(ScaleMethod newScaleMethod)
        {
            if (newScaleMethod == _scaleMethod)
            {
                return;
            }
            _scaleMethod = newScaleMethod;
            ScalePresetChanged?.Invoke();
        }

        /* Grid arrange and margins*/
        public enum ArrangeLayout { Fill = 0, Grid = 1, PackByImage = 2 }

        /* Cell content scaling, padding and spacing */
        public enum ScaleMethod { FitImage = 0, None = 1 }

#if UNITY_EDITOR
        //Safe initialization in editor mode & runtime both
        protected override void OnCanvasHierarchyChanged()
        {
            _gridRect = GetComponent<RectTransform>();
            CollectElements();
        }
#endif


#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            CollectElements();
            if (!ApproptiatePresetKey(_arrangePreset, _arrangeLayout))
            {
                OnArrangePresetChanged();
            }
            if (!ApproptiatePresetKey(_scalePreset, _scaleMethod))
            {
                OnScalePresetChanged();
            }

            //  Fixes the warning
            //  "SendMessage cannot be called during Awake, CheckConsistency, or OnValidate"
            UnityEditor.EditorApplication.delayCall += OnValidateCallback;
        }
        private void OnValidateCallback()
        {
            UnityEditor.EditorApplication.delayCall -= OnValidateCallback;
            AdjustElements();
        }
#endif

    }
}
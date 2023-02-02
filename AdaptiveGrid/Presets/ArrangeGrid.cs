using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AdaptiveGrid
{
    [Serializable]
    public class ArrangeGrid : AdaptivePreset
    {
        [SerializeField] GridSize _gridSize = new GridSize(2, 2);

        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding)
        {

            GridSize gridSize = _gridSize;
            if (gridSize.Cols == 0 && gridSize.Rows == 0)
            {
                Debug.LogWarning($"You are trying to arrange elements in 0x0 grid");
                return;
            }
            if (gridSize.Rows == 0)
            {
                gridSize.Rows = (int)Mathf.Ceil((float)elements.Count / gridSize.Cols);
            }
            if (gridSize.Cols == 0)
            {
                gridSize.Cols = (int)Mathf.Ceil((float)elements.Count / gridSize.Rows);
            }

            LayoutTools.ArrangeElements(elements, grid.rect, gridSize, gridMargin, cellPadding);
        }

        public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Grid;

    }
}
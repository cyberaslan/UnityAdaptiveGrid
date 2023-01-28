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
public class ArrangeFixedRows : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.FixedRows;
    [SerializeField] int Rows;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
public class ArrangeFixedColumns : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.FixedColumns;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}

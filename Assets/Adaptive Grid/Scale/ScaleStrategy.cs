using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class StretchScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Stretch;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}
[Serializable]
public class FitScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.Fit;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        foreach(RectTransform element in elements) {
            if (element.TryGetComponent(out Image image)) {
                if (image.sprite != null) {
                    Vector3 spriteSize = image.sprite.bounds.size;

                    float contentAspectRatio = spriteSize.x / spriteSize.y;
                    float containerAspectRatio = element.sizeDelta.x / element.sizeDelta.y;
                    float fitRatio = containerAspectRatio / contentAspectRatio;
                    Vector2 newSizeDelta = element.sizeDelta;
                    

                    if (containerAspectRatio / contentAspectRatio >= 1.0f) {
                        //content is too high
                        newSizeDelta = new Vector2(newSizeDelta.x/ fitRatio, newSizeDelta.y);
                    } else {
                        //content is too wide
                        newSizeDelta = new Vector2(newSizeDelta.x, newSizeDelta.y * fitRatio);
                    }

                    element.sizeDelta = newSizeDelta;
                }
            }
        }
    }
}
[Serializable]
public class FitWidthScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitWidth;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        
    }
}
[Serializable]
public class FitHeightScale : Strategy
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitHeight;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {

    }
}


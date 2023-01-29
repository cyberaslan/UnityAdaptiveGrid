using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

[Serializable]
public class ScaleFit : AdaptivePreset
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ScaleMethod.FitImages;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        foreach (RectTransform element in elements) {
            if (element.TryGetComponent(out Image image)) {
                if (image.sprite != null) {
                    //source image content
                    Vector3 contentSize = image.sprite.bounds.size; 
                    float contentAspectRatio = contentSize.x / contentSize.y;

                    float containerAspectRatio = element.sizeDelta.x / element.sizeDelta.y;

                    float fitRatio = containerAspectRatio / contentAspectRatio;
                    
                    //Calculate new size fitted 
                    Vector2 newSizeDelta = element.sizeDelta;

                    if (containerAspectRatio / contentAspectRatio >= 1.0f) {
                        //content is too high
                        newSizeDelta = new Vector2(newSizeDelta.x / fitRatio, newSizeDelta.y);
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
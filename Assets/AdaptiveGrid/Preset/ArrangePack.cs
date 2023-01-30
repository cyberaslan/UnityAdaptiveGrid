using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using msmshazan.TexturePacker;
using System;
using System.Linq;
public class ArrangePack : AdaptivePreset
{
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Pack;

    //Bin packing
    [SerializeField] private MaxRectsBinPack.FreeRectChoiceHeuristic PackAlgorithm;
    private enum ScalePrecision
    {
        High = 1,
        Medium = 10,
        Low = 50
    }
    [SerializeField] private ScalePrecision _precisionLevel;
    private float _scalePrecision;
    private float _scaleFactor;

    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        //Set base values each Apply call needs cause preset object used as serialized reference
        _scalePrecision = (float)_precisionLevel / 1000;
        _scaleFactor = 1.0f;

        //Search for elements content
        List<Component> contentList = new List<Component>();
        foreach (RectTransform element in elements) {
            if (element.childCount > 1) Debug.LogWarning($"{element.name} driven by AdaptiveGrid has >1 child and might be arranged incorrect");
            if (element.TryGetComponent(out Image image)) {
                contentList.Add(image);
            } else if (element.TryGetComponent(out RectTransform childRect)) {
                contentList.Add(childRect);
            }
        }

        Rect[] rects = PackTextures(grid, contentList, (int)grid.rect.width, (int)grid.rect.height, (int)grid.rect.width);

        //Arrange elements by calculated rects
        for(int i=0; i<rects.Length; i++) {
            elements[i].anchorMin = elements[i].anchorMax = Vector2.zero;
            elements[i].anchoredPosition = new Vector2(rects[i].x + rects[i].width/2, rects[i].y + rects[i].height/2);
            elements[i].sizeDelta = new Vector2(rects[i].width, rects[i].height); 
        }
        
    }   


    public Rect[] PackTextures(RectTransform grid, List<Component> contentList, float width, float height, int maxSize) {

        MaxRectsBinPack binPacker = new MaxRectsBinPack((int)width, (int)height);
        Rect[] packedRects = new Rect[contentList.Count];

        for (int i = 0; i < contentList.Count; i++) {

            RectSize rectSize = contentList[i].ToRectSize();
            int scaledRectWidth = (int)(rectSize.width * _scaleFactor);
            int scaledRectHeight = (int)(rectSize.height * _scaleFactor);

            //Try to insert content into container
            BinRect rect = binPacker.Insert(scaledRectWidth, scaledRectHeight, PackAlgorithm);

            //If content cant be packed, make re-pack with smaller scale
            if (rect.width == 0 || rect.height == 0) {
                _scaleFactor -= _scalePrecision;
                return PackTextures(grid, contentList, (width), (height), maxSize);
            }

            packedRects[i] = (Rect)rect;
        }
        return packedRects;

    }
}

public static class ComponentExtensions
{
    public static RectSize ToRectSize(this Component component) {
        if (component is Image img) return new RectSize(img.sprite.rect);
        if (component is RectTransform rt) return new RectSize(rt.rect);
        return new RectSize();
    }
}

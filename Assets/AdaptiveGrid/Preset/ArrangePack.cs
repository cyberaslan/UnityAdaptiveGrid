using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TexturePacker;
using System;
using System.Runtime.InteropServices;
using UnityEngine.UI;
public class ArrangePack : AdaptivePreset
{

    //public void Pack(List<PackerBitmap> bitmaps, bool rotate) {
    public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.Pack;
    [SerializeField] private TexturePacker.MaxRectsBinPack.FreeRectChoiceHeuristic PackAlgorithm;
    public override void Apply(List<RectTransform> elements, RectTransform grid) {
        _scaleFactor = 1.0f;
        escapeCounter = 0;
        _scaleLimit = 19;
        List<Image> images = new();
        foreach (RectTransform element in elements) if (element.TryGetComponent(out Image image)) images.Add(image);

        Rect[] rects = PackTextures(grid, images.ToArray(), (int)grid.rect.width, (int)grid.rect.height, (int)grid.rect.width);
        for(int i=0; i<rects.Length; i++) {
            elements[i].anchorMin = elements[i].anchorMax = Vector2.zero;
            elements[i].anchoredPosition = new Vector2(rects[i].x + rects[i].width/2, rects[i].y + rects[i].height/2);
            elements[i].sizeDelta = new Vector2(rects[i].width, rects[i].height); 
        }
        
    }   
    
    private float _scaleFactor;
    private float _scaleFactorStep = 0.05f;
    private float _scaleLimit;

    private int escapeCounter;
    public Rect[] PackTextures(RectTransform grid, Image[] textures, int width, int height, int maxSize) {
        escapeCounter++;
        if (escapeCounter > _scaleLimit) { Debug.Log("Escape"); return null; }
        
        Debug.Log($"#{escapeCounter} {_scaleFactor} ({width}x{height})");

        if (width > maxSize && height > maxSize) {
            Debug.Log("SizeLimit"); return null;
        }
        if (width > maxSize || height > maxSize) { int temp = width; width = height; height = temp; }

        TexturePacker.MaxRectsBinPack bp = new TexturePacker.MaxRectsBinPack(width, height);
        Rect[] rects = new Rect[textures.Length];

        for (int i = 0; i < textures.Length; i++) {
            Sprite tex = textures[i].sprite;
            int texWidth = (int)(tex.rect.width * _scaleFactor);
            int texHeight = (int)(tex.rect.height * _scaleFactor);
            //Debug.Log($"Bin pack {texWidth}x{texHeight}");
            BinRect bRect = bp.Insert(texWidth, texHeight, PackAlgorithm);
            if (bRect.width == 0 || bRect.height == 0) {
                _scaleFactor -= _scaleFactorStep;
                return PackTextures(grid, textures, (int)(width), (int)(height), maxSize);
            }
            Rect rect = new Rect(bRect.x, bRect.y, bRect.width, bRect.height);
            rects[i] = rect;
        }
        Debug.Log($"Output: {rects}");
        return rects;

        /*for(int i = 0; i < textures.Length; i++) {
            Sprite tex = textures[i].sprite;
            Rect rect = rects[i];



            texture.SetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height, colors);
            rect.x /= width;
            rect.y /= height;
            rect.width /= width;
            rect.height /= height;
            rects[i] = rect;
        }*/


    }
}
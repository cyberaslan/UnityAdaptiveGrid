using msmshazan.TexturePacker;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace AdaptiveGrid
{
    public class ArrangePack : AdaptivePreset
    {

        //Bin packing
        [SerializeField] private MaxRectsBinPack.FreeRectChoiceHeuristic _packAlgorithm;
        [Range(1, 5)] [SerializeField] private int _precisionLevel = 5;
        private float _scalePrecision;

        public override void Apply(List<RectTransform> elements, RectTransform grid, Offset gridMargin, Offset cellPadding)
        {

            //Set base values each Apply call needs cause preset object used as serialized reference
            _scalePrecision = (float)(6 - _precisionLevel) / 100;

            //Search for elements content
            List<Image> contentList = new List<Image>();

            foreach (RectTransform element in elements)
            {
                if (element.childCount > 1)
                {
                    Debug.LogWarning($"{element.name} driven by AdaptiveGrid has >1 child and might be arranged incorrect");
                }

                if (element.TryGetComponent(out Image image))
                {
                    contentList.Add(image);
                }
            }

            if (contentList.Count == 0)
            {
                Debug.LogWarning($"AdaptiveGrid have no childs with Image.sprite and cant be arranged with Bin Packing algorithm");
            }

            try
            {
                Rect[] rects = LayoutTools.PackRects(grid, contentList, _packAlgorithm, _scalePrecision);
                //Arrange elements by calculated rects
                for (int i = 0; i < rects.Length; i++)
                {
                    elements[i].anchorMin = elements[i].anchorMax = new Vector2(0, 1);
                    elements[i].anchoredPosition = new Vector2(rects[i].x + rects[i].width / 2, -rects[i].y - rects[i].height / 2);
                    elements[i].sizeDelta = new Vector2(rects[i].width, rects[i].height);
                }
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Bin packing AdaptiveGrid content error \n {e}");
            }
        }

        public override System.Enum SelectorInInspector => AdaptiveGrid.ArrangeLayout.PackByImage;
    }
}
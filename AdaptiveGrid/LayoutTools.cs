using msmshazan.TexturePacker;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace AdaptiveGrid
{
    [Serializable]
    public struct GridSize
    {
        public int Rows;
        public int Cols;

        public GridSize(int columns, int rows)
        {
            Rows = rows;
            Cols = columns;
        }

        public override string ToString()
        {
            return $"GridSize {Cols}x{Rows}";
        }
    }

    [Serializable]
    public struct Offset
    {
        [Range(0, 0.9f)] public float Horizontal;
        [Range(0, 0.9f)] public float Vertical;
    }

    public static class LayoutTools
    {

        // Place elements in gridRect
        public static void ArrangeElements(List<RectTransform> elements, Rect gridRect, GridSize gridSize, Offset gridMargin, Offset cellPadding)
        {

            float gridWidth = gridRect.width * (1 - gridMargin.Horizontal);
            float gridHeight = gridRect.height * (1 - gridMargin.Vertical);

            float cellWidth = gridWidth / gridSize.Cols;
            float cellWidthOffseted = cellWidth * (1 - cellPadding.Horizontal);

            float cellHeigth = gridHeight / gridSize.Rows;
            float cellHeigthOffseted = cellHeigth * (1 - cellPadding.Vertical);

            for (int i = 0; i < elements.Count; i++)
            {
                RectTransform element = elements[i];

                int rowNum = i / gridSize.Cols;
                int colNum = i % gridSize.Cols;

                //normalize pivot and anchors
                element.pivot = new Vector2(0.5f, 0.5f);
                element.anchorMin = element.anchorMax = new Vector2(0.5f, 0.5f);
                element.sizeDelta = new Vector2(cellWidthOffseted, cellHeigthOffseted);
                element.anchoredPosition = new Vector2(
                    colNum * (cellWidth + cellPadding.Horizontal / 2) + cellWidth * element.pivot.x - gridWidth / 2,
                    -rowNum * (cellHeigth + cellPadding.Vertical / 2) - cellHeigth * element.pivot.y + gridHeight / 2);
            }
        }

        //Size of content fitted in container with const aspect ratio
        public static Vector2 FitContent(Vector2 contentSize, Rect container, Offset cellPadding)
        {
            float contentAspectRatio = contentSize.x / contentSize.y;

            float containerAspectRatio = (container.width) / (container.height);

            float fitRatio = containerAspectRatio / contentAspectRatio;

            //Calculate new size fitted 
            Vector2 newSizeDelta = new Vector2(container.width, container.height);

            if (containerAspectRatio / contentAspectRatio >= 1.0f)
            {
                //content is too high
                newSizeDelta = new Vector2(newSizeDelta.x / fitRatio, newSizeDelta.y);
            }
            else
            {
                //content is too wide
                newSizeDelta = new Vector2(newSizeDelta.x, newSizeDelta.y * fitRatio);
            }
            newSizeDelta = new Vector2(newSizeDelta.x, newSizeDelta.y);
            return newSizeDelta;
        }

        //Calculate optimal grid size for minimum empty space in container
        public static GridSize OptimalGridSize(List<RectTransform> elements, Rect gridRect, Vector2 contentSize, Offset gridMargin, Offset cellPadding)
        {

            int q = elements.Count;

            float minTotalEmptySpace = System.Single.MaxValue;
            int optimalRowColunt = 1;
            int optimalColCount = 1;

            for (int cols = 1; cols <= q; cols++)
            {
                for (int rows = 1; rows <= Mathf.Ceil((float)q / cols); rows++)
                {
                    int emptyCells = cols * rows - q;
                    if (emptyCells >= 0)
                    {

                        Vector2 eventualCellSize = new Vector2(gridRect.width / cols, gridRect.height / rows);
                        float eventualCellSpace = eventualCellSize.x * eventualCellSize.y;

                        Vector2 averageScaledContentSize = LayoutTools.FitContent(contentSize, new Rect(0, 0, eventualCellSize.x, eventualCellSize.y), cellPadding);
                        float eventualCellEmptySpace = eventualCellSpace - averageScaledContentSize.magnitude;
                        float eventualTotalEmptySpace = eventualCellEmptySpace * q + emptyCells * eventualCellSpace;

                        if (eventualTotalEmptySpace < minTotalEmptySpace)
                        {
                            minTotalEmptySpace = eventualTotalEmptySpace;
                            optimalRowColunt = rows;
                            optimalColCount = cols;
                        }
                    }
                }
            }
            return new GridSize(optimalColCount, optimalRowColunt);
        }

        //Pack content in grid container (recursive)
        public static Rect[] PackRects(RectTransform grid, List<UnityEngine.UI.Image> contentList, MaxRectsBinPack.FreeRectChoiceHeuristic packAlgorithm, float scalePrecision, float scaleFactor = 1.0f)
        {

            int width = (int)grid.rect.width;
            int height = (int)grid.rect.height;
            MaxRectsBinPack binPacker = new MaxRectsBinPack(width, height);

            Rect[] packedRects = new Rect[contentList.Count];

            for (int i = 0; i < contentList.Count; i++)
            {

                RectSize rectSize = contentList[i].sprite != null ? new RectSize(contentList[i].sprite.rect) : new RectSize(contentList[i].GetComponent<RectTransform>().rect);
                int scaledRectWidth = (int)(rectSize.width * scaleFactor);
                int scaledRectHeight = (int)(rectSize.height * scaleFactor);
                if (scaledRectHeight == 0)
                {
                    scaledRectHeight = 1;
                }
                if (scaledRectWidth == 0)
                {
                    scaledRectWidth = 1;
                }

                //Try to insert content into container
                BinRect rect = binPacker.Insert(scaledRectWidth, scaledRectHeight, packAlgorithm);

                //If content cant be packed, make re-pack with smaller scale
                if (rect.width == 0 || rect.height == 0)
                {
                    scaleFactor -= scalePrecision;
                    return PackRects(grid, contentList, packAlgorithm, scalePrecision, scaleFactor);
                }

                packedRects[i] = (Rect)rect;
            }
            return packedRects;
        }
    }

}
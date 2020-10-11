using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FitType
{
    Width,
    Heigth
}
public class FlexibleGridLayout : LayoutGroup
{
    public int rows;
    public int columns;
    public Vector2 cellSize;
    public Vector2 spacing;
    public FitType fitType;

    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = 0f;
        float cellHeight = 0f;

        columns = transform.childCount;
        rows = 1;

        cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);


        cellSize.y = cellHeight;
        cellSize.x = cellWidth;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = 0;
            columnCount = i;


            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = 0f;
        float cellHeight = 0f;

        columns = 1;
        rows = transform.childCount;

        cellWidth = parentWidth / (float)columns - ((spacing.x / (float)columns) * 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        cellHeight = parentHeight / (float)rows - ((spacing.y / (float)rows) * 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);


        cellSize.y = cellHeight;
        cellSize.x = cellWidth;

        int columnCount = 0;
        int rowCount = 0;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            rowCount = i;
            columnCount = 0;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    public override void SetLayoutHorizontal()
    {
        fitType = FitType.Width;
        CalculateLayoutInputHorizontal();
    }
    public override void SetLayoutVertical()
    {
        fitType = FitType.Heigth;
        CalculateLayoutInputVertical();
    }
}

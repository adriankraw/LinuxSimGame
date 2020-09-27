using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FitType
{
    Uniform,
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
        
    float srqrt = 0;

        if(fitType == FitType.Uniform)
        {
            srqrt = Mathf.Sqrt(transform.childCount);
            rows = Mathf.CeilToInt(srqrt);
            columns = Mathf.CeilToInt(srqrt);
        }
        if(fitType == FitType.Heigth)
        {
            columns = transform.childCount;
            rows = 1;
        }
        if(fitType == FitType.Width)
        {
            columns = transform.childCount;
            rows = 1;
        }


        float parentWidth = rectTransform.rect.width;
        float parentHeight = rectTransform.rect.height;

        float cellWidth = parentWidth / (float)columns - ((spacing.x/(float)columns)* 2) - (padding.left / (float)columns) - (padding.right / (float)columns);
        float cellHeight = parentHeight / (float)rows - ((spacing.y/(float)rows)* 2) - (padding.top / (float)rows) - (padding.bottom / (float)rows);

        if(rectChildren.Count == rows)
        {
            cellHeight = parentHeight - (spacing.y * 2) - padding.top - padding.bottom;
        }


        cellSize.y = cellHeight;
        cellSize.x = cellWidth;

        int columnCount = 0;
        int rowCount = 0;

        for (int i  = 0; i < rectChildren.Count; i++)
        {
            rowCount = i / rows;
            columnCount = i % columns;

            var item = rectChildren[i];

            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + padding.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + padding.top;

            // if(i == rectChildren.Count -1)
            // {
            //     // fill width
            //     cellSize.x += ((columns - rectChildren.Count % columns ) * (parentWidth / (float)columns)) - padding.right;
            // }

            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    public override void CalculateLayoutInputVertical()
    {
        
    }
    public override void SetLayoutHorizontal()
    {
        
    }
    public override void SetLayoutVertical()
    {
        
    }
}
